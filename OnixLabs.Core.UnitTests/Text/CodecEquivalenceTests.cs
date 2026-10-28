// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using OnixLabs.Core.Text;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests.Text;

/// <summary>
/// Verifies the Base-16, Base-32 and Base-64 codecs and the span formatting of every Base-N type against independent oracles:
/// the library's original Base-32 algorithm, and the BCL conversions the Base-16 and Base-64 codecs previously called.
/// </summary>
public sealed class CodecEquivalenceTests
{
    private const int RandomCaseCount = 400;

    public static TheoryData<string> Base32Providers()
    {
        TheoryData<string> result = new();

        foreach (Base32FormatProvider provider in Base32FormatProvider.GetAll())
            result.Add(provider.Name);

        return result;
    }

    public static TheoryData<string> Base16Providers()
    {
        TheoryData<string> result = new();

        foreach (Base16FormatProvider provider in Base16FormatProvider.GetAll())
            result.Add(provider.Name);

        return result;
    }

    [Theory(DisplayName = "Base32Codec should match the original algorithm for encoding and round trips")]
    [MemberData(nameof(Base32Providers))]
    public void Base32CodecShouldMatchTheOriginalAlgorithmForEncodingAndRoundTrips(string providerName)
    {
        // Given
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);
        Random random = new(Seed: 32);
        List<string> failures = [];

        // When
        foreach (int length in Enumerable.Range(0, 81).Append(1000))
        {
            byte[] input = new byte[length];
            random.NextBytes(input);

            string expected = Base32Reference.Encode(input, provider.Alphabet, provider.IsPadded);
            string actual = IBaseCodec.Base32.Encode(input, provider);

            if (expected != actual)
                failures.Add($"encode of {length} bytes: expected {expected} but was {actual}");

            if (!IBaseCodec.Base32.Decode(actual, provider).AsSpan().SequenceEqual(input))
                failures.Add($"round trip of {length} bytes did not reproduce the input");
        }

        // Then
        Assert.Empty(failures);
    }

    [Theory(DisplayName = "Base32Codec should accept and reject exactly what the original algorithm does")]
    [MemberData(nameof(Base32Providers))]
    public void Base32CodecShouldAcceptAndRejectExactlyWhatTheOriginalAlgorithmDoes(string providerName)
    {
        // Given
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);
        Random random = new(Seed: 23);
        string characters = provider.Alphabet + provider.Alphabet.ToLowerInvariant() + provider.Alphabet.ToUpperInvariant() + "=!0é ";
        List<string> failures = [];

        // When
        for (int index = 0; index < RandomCaseCount; index++)
        {
            int length = random.Next(0, 25);
            string candidate = new(Enumerable.Range(0, length).Select(_ => characters[random.Next(characters.Length)]).ToArray());

            byte[]? expected = Base32Reference.Decode(candidate, provider.Alphabet, provider.IsPadded);
            bool succeeded = IBaseCodec.Base32.TryDecode(candidate, provider, out byte[] actual);

            if (succeeded != expected is not null)
                failures.Add($"'{candidate}': original {Describe(expected is not null)} but codec {Describe(succeeded)}");
            else if (expected is not null && !actual.AsSpan().SequenceEqual(expected))
                failures.Add($"'{candidate}': decoded bytes differ from the original algorithm");
        }

        // Then
        Assert.Empty(failures);
    }

    [Theory(DisplayName = "Base16Codec should accept and reject exactly what the provider rules and Convert.FromHexString do")]
    [MemberData(nameof(Base16Providers))]
    public void Base16CodecShouldAcceptAndRejectExactlyWhatTheProviderRulesAndConvertDo(string providerName)
    {
        // Given
        Base16FormatProvider provider = Base16FormatProvider.FromName(providerName);
        Random random = new(Seed: 16);
        const string characters = "0123456789abcdefABCDEFgG x!é";
        List<string> failures = [];

        // When
        for (int index = 0; index < RandomCaseCount; index++)
        {
            int length = random.Next(0, 13);
            string candidate = new(Enumerable.Range(0, length).Select(_ => characters[random.Next(characters.Length)]).ToArray());

            byte[]? expected = DecodeHexLikeTheOriginal(candidate, provider);
            bool succeeded = IBaseCodec.Base16.TryDecode(candidate, provider, out byte[] actual);

            if (succeeded != expected is not null)
                failures.Add($"'{candidate}': original {Describe(expected is not null)} but codec {Describe(succeeded)}");
            else if (expected is not null && !actual.AsSpan().SequenceEqual(expected))
                failures.Add($"'{candidate}': decoded bytes differ from Convert.FromHexString");
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "Base64Codec should accept and reject exactly what Convert.FromBase64String does")]
    public void Base64CodecShouldAcceptAndRejectExactlyWhatConvertFromBase64StringDoes()
    {
        // Given
        Random random = new(Seed: 64);
        List<string> failures = [];

        // When
        for (int index = 0; index < RandomCaseCount; index++)
        {
            byte[] bytes = new byte[random.Next(0, 300)];
            random.NextBytes(bytes);
            string candidate = Mutate(Convert.ToBase64String(bytes), random);

            byte[]? expected = DecodeBase64LikeTheOriginal(candidate);
            bool succeeded = IBaseCodec.Base64.TryDecode(candidate, Base64FormatProvider.Rfc4648, out byte[] actual);

            if (succeeded != expected is not null)
                failures.Add($"'{candidate}': Convert.FromBase64String {Describe(expected is not null)} but codec {Describe(succeeded)}");
            else if (expected is not null && !actual.AsSpan().SequenceEqual(expected))
                failures.Add($"'{candidate}': decoded bytes differ from Convert.FromBase64String");
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "Base-N TryFormat should match ToString, report short destinations, and reject invalid providers")]
    public void BaseNTryFormatShouldMatchToStringReportShortDestinationsAndRejectInvalidProviders()
    {
        // Given
        Random random = new(Seed: 1664);
        List<string> failures = [];
        List<(string Name, Func<byte[], ISpanFormattable> Create, IFormatProvider[] Providers)> types =
        [
            ("Base16", bytes => new Base16(bytes), [..Base16FormatProvider.GetAll()]),
            ("Base32", bytes => new Base32(bytes), [..Base32FormatProvider.GetAll()]),
            ("Base58", bytes => new Base58(bytes), [..Base58FormatProvider.GetAll()]),
            ("Base64", bytes => new Base64(bytes), [..Base64FormatProvider.GetAll()])
        ];

        // When
        foreach ((string name, Func<byte[], ISpanFormattable> create, IFormatProvider[] providers) in types)
        foreach (IFormatProvider provider in providers)
        foreach (int length in new[] { 0, 1, 2, 5, 31, 32, 33, 200 })
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            ISpanFormattable value = create(bytes);
            string expected = value.ToString(null, provider);

            char[] larger = new char[expected.Length + 10];
            bool fitted = value.TryFormat(larger, out int charsWritten, default, provider);
            bool fittedExactly = value.TryFormat(new char[expected.Length], out int exactCharsWritten, default, provider);

            if (!fitted || new string(larger, 0, charsWritten) != expected)
                failures.Add($"{name} {provider} {length}: TryFormat did not match ToString");

            if (!fittedExactly || exactCharsWritten != expected.Length)
                failures.Add($"{name} {provider} {length}: TryFormat did not fit an exactly sized destination");

            if (expected.Length > 0 && (value.TryFormat(new char[expected.Length - 1], out int shortCharsWritten, default, provider) || shortCharsWritten != 0))
                failures.Add($"{name} {provider} {length}: TryFormat did not report a short destination");
        }

        // Then
        Assert.Empty(failures);
        Assert.All(types, type => Assert.Throws<FormatException>(() =>
            type.Create([1, 2, 3]).TryFormat(new char[100], out int _, default, InvalidFormatProvider.Instance)));
    }

    /// <summary>
    /// Decodes hexadecimal text the way the original codec did: provider case rules, then <see cref="Convert.FromHexString(string)"/>.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <param name="provider">The format provider whose case rules apply.</param>
    /// <returns>Returns the decoded bytes, or <see langword="null"/> if the original codec rejected the text.</returns>
    private static byte[]? DecodeHexLikeTheOriginal(string value, Base16FormatProvider provider)
    {
        if (value.Length == 0) return [];
        if (provider == Base16FormatProvider.Uppercase && value.Any(character => character is >= 'a' and <= 'f')) return null;
        if (provider == Base16FormatProvider.Lowercase && value.Any(character => character is >= 'A' and <= 'F')) return null;

        try
        {
            return Convert.FromHexString(value);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    /// <summary>
    /// Decodes Base-64 text the way the original codec did, with <see cref="Convert.FromBase64String"/>.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <returns>Returns the decoded bytes, or <see langword="null"/> if <see cref="Convert.FromBase64String"/> rejected the text.</returns>
    private static byte[]? DecodeBase64LikeTheOriginal(string value)
    {
        if (value.Length == 0) return [];

        try
        {
            return Convert.FromBase64String(value);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    /// <summary>
    /// Applies a random mutation to valid Base-64 text: none, whitespace, a dropped or extra padding character, an invalid character,
    /// or truncation.
    /// </summary>
    /// <param name="value">The valid Base-64 text.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>Returns the possibly mutated text.</returns>
    private static string Mutate(string value, Random random)
    {
        int position = random.Next(value.Length + 1);

        return random.Next(6) switch
        {
            0 => value,
            1 => value.Insert(position, random.Next(2) == 0 ? " " : "\r\n"),
            2 => value.TrimEnd('='),
            3 => value + "=",
            4 => value.Insert(position, "*"),
            _ => value[..position]
        };
    }

    /// <summary>
    /// Describes whether a decoder accepted its input.
    /// </summary>
    /// <param name="accepted">A value indicating whether the decoder accepted its input.</param>
    /// <returns>Returns "accepted" or "rejected".</returns>
    private static string Describe(bool accepted) => accepted ? "accepted" : "rejected";
}
