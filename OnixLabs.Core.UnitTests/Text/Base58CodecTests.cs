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
using System.Text;
using OnixLabs.Core.Text;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base58CodecTests
{
    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Bitcoin)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58CodecEncodeShouldProduceExpectedResultBitcoin(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Bitcoin)")]
    [InlineData("", "")]
    [InlineData("2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3i37NcgooY8f1S", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultBitcoin(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Flickr)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E")]
    [InlineData("0123456789", "3H37nBFNNx8E1r")]
    public void Base58CodecEncodeShouldProduceExpectedResultFlickr(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base58FormatProvider.Flickr);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Flickr)")]
    [InlineData("", "")]
    [InlineData("2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3H37nBFNNx8E1r", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultFlickr(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value, Base58FormatProvider.Flickr);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Ripple)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC")]
    [InlineData("0123456789", "s5sf4cgooY3CrS")]
    public void Base58CodecEncodeShouldProduceExpectedResultRipple(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base58FormatProvider.Ripple);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Ripple)")]
    [InlineData("", "")]
    [InlineData("pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("s5sf4cgooY3CrS", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultRipple(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value, Base58FormatProvider.Ripple);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58Codec round-trip should preserve leading zero bytes (Bitcoin)")]
    public void Base58CodecRoundTripShouldPreserveLeadingZeroBytesBitcoin()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] expected = [0x00, 0x00, 0x61];

        // When
        string encoded = codec.Encode(expected, Base58FormatProvider.Bitcoin);
        byte[] actual = codec.Decode(encoded, Base58FormatProvider.Bitcoin);

        // Then
        Assert.Equal("112g", encoded);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58Codec round-trip should preserve leading zero bytes (Flickr)")]
    public void Base58CodecRoundTripShouldPreserveLeadingZeroBytesFlickr()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] expected = [0x00, 0x00, 0x61];

        // When
        string encoded = codec.Encode(expected, Base58FormatProvider.Flickr);
        byte[] actual = codec.Decode(encoded, Base58FormatProvider.Flickr);

        // Then
        Assert.Equal("112F", encoded);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58Codec round-trip should preserve leading zero bytes using the provider's zero character (Ripple)")]
    public void Base58CodecRoundTripShouldPreserveLeadingZeroBytesRipple()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] expected = [0x00, 0x00, 0x61];

        // When
        string encoded = codec.Encode(expected, Base58FormatProvider.Ripple);
        byte[] actual = codec.Decode(encoded, Base58FormatProvider.Ripple);

        // Then
        Assert.Equal("rrpg", encoded);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58Codec.Encode should throw FormatException when the format provider is invalid")]
    public void Base58CodecEncodeShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Encode(bytes, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.Decode should throw FormatException when the format provider is invalid")]
    public void Base58CodecDecodeShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        const string value = "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.Decode should throw FormatException when the value is invalid")]
    public void Base58CodecDecodeShouldThrowFormatExceptionWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        const string value = "*INVALID_VALUE*";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.TryEncode should return false when the format provider is invalid")]
    public void Base58CodecTryEncodeShouldReturnFalseWhenValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        bool result = codec.TryEncode(bytes, InvalidFormatProvider.Instance, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base58Codec.TryEncode should pad zero values")]
    public void Base58CodecTryEncodeShouldPadZeroValues()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = [0];
        const string expected = "1";

        // When
        bool result = codec.TryEncode(bytes, Base58FormatProvider.Bitcoin, out string actual);

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<string> Alphabets() => new()
    {
        Base58FormatProvider.Bitcoin.Name,
        Base58FormatProvider.Flickr.Name,
        Base58FormatProvider.Ripple.Name
    };

    [Theory(DisplayName = "Base58Codec should match the BigInteger reference encoder and decoder for random inputs")]
    [MemberData(nameof(Alphabets))]
    public void Base58CodecShouldMatchTheBigIntegerReferenceForRandomInputs(string providerName)
    {
        // Given
        Base58FormatProvider provider = Base58FormatProvider.FromName(providerName);
        Random random = new(Seed: 58);
        List<string> failures = [];
        List<byte[]> inputs = [[], [0], [0, 0, 0], [255], [0, 0, 1], [..new byte[64]]];

        for (int length = 1; length <= 300; length++)
            inputs.Add(CreateRandomInput(random, length));

        inputs.Add(CreateRandomInput(random, 1024));
        inputs.Add(CreateRandomInput(random, 3000));

        // When
        foreach (byte[] input in inputs)
        {
            string expected = Base58Reference.Encode(input, provider.Alphabet);
            string actual = IBaseCodec.Base58.Encode(input, provider);
            byte[] roundTrip = IBaseCodec.Base58.Decode(actual, provider);

            if (expected != actual)
                failures.Add($"encode of {input.Length} bytes: expected {expected} but was {actual}");

            if (!roundTrip.AsSpan().SequenceEqual(input))
                failures.Add($"round trip of {input.Length} bytes did not reproduce the input");
        }

        // Then
        Assert.Empty(failures);
    }

    [Theory(DisplayName = "Base58Codec should reject exactly the inputs the BigInteger reference rejects")]
    [MemberData(nameof(Alphabets))]
    public void Base58CodecShouldRejectExactlyTheInputsTheReferenceRejects(string providerName)
    {
        // Given
        Base58FormatProvider provider = Base58FormatProvider.FromName(providerName);
        Random random = new(Seed: 85);
        char[] candidates = ['0', 'O', 'I', 'l', '+', '/', '=', ' ', '\u00e9', '\u0141', provider.Alphabet[0], provider.Alphabet[57]];
        List<string> failures = [];

        // When
        for (int index = 0; index < 500; index++)
        {
            string valid = IBaseCodec.Base58.Encode(CreateRandomInput(random, random.Next(1, 40)), provider);
            int position = random.Next(valid.Length + 1);
            string candidate = valid.Insert(position, candidates[random.Next(candidates.Length)].ToString());

            byte[]? expected = Base58Reference.Decode(candidate, provider.Alphabet);
            bool succeeded = IBaseCodec.Base58.TryDecode(candidate, provider, out byte[] actual);

            if (succeeded != expected is not null)
                failures.Add($"{candidate}: reference {(expected is null ? "rejected" : "accepted")} but codec {(succeeded ? "accepted" : "rejected")}");
            else if (expected is not null && !actual.AsSpan().SequenceEqual(expected))
                failures.Add($"{candidate}: decoded bytes differ from the reference");
        }

        // Then
        Assert.Empty(failures);
    }

    /// <summary>
    /// Creates random input of the specified length, sometimes with leading zero bytes.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <param name="length">The length of the input.</param>
    /// <returns>Returns random input of the specified length.</returns>
    private static byte[] CreateRandomInput(Random random, int length)
    {
        byte[] result = new byte[length];
        random.NextBytes(result);

        int leadingZeroCount = Math.Min(length, random.Next(4));
        result.AsSpan(0, leadingZeroCount).Clear();

        return result;
    }
}
