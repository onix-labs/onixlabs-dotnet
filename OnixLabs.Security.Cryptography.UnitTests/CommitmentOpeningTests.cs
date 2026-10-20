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
using System.Linq;
using System.Security.Cryptography;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class CommitmentOpeningTests
{
    private static readonly byte[] Seed = Enumerable.Range(0, 32).Select(index => (byte)index).ToArray();

    [Fact(DisplayName = "CommitmentOpening.ToString should not reveal the opening")]
    public void CommitmentOpeningToStringShouldNotRevealTheOpening()
    {
        // Given
        CommitmentOpening opening = new(Seed);
        string hex = Convert.ToHexString(Seed);

        // When
        string actual = opening.ToString();

        // Then
        Assert.Equal("[redacted]", actual);
        Assert.DoesNotContain(hex, actual, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(hex, $"{opening}", StringComparison.OrdinalIgnoreCase);
    }

    [Fact(DisplayName = "CommitmentOpening.ToString with a format provider should export the opening")]
    public void CommitmentOpeningToStringWithFormatProviderShouldExportTheOpening()
    {
        // Given
        CommitmentOpening opening = new(Seed);
        string expected = Convert.ToHexString(Seed);

        // When
        string actual = opening.ToString(Base16FormatProvider.Uppercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "CommitmentOpening should reject values that are not exactly 32 bytes")]
    [InlineData(0)]
    [InlineData(31)]
    [InlineData(33)]
    [InlineData(64)]
    public void CommitmentOpeningShouldRejectValuesThatAreNotExactly32Bytes(int length)
    {
        // Given
        byte[] value = new byte[length];

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => new CommitmentOpening(value));

        // Then
        Assert.StartsWith("A commitment opening must be exactly 32 bytes.", exception.Message);
    }

    [Fact(DisplayName = "CommitmentOpening value should not be modified when altering the original byte array")]
    public void CommitmentOpeningValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = Seed.ToArray();
        CommitmentOpening opening = new(value);

        // When
        value[0] = 0xFF;

        // Then
        Assert.Equal(Seed, opening.AsReadOnlySpan().ToArray());
    }

    [Fact(DisplayName = "CommitmentOpening.Create should produce distinct 32 byte openings")]
    public void CommitmentOpeningCreateShouldProduceDistinct32ByteOpenings()
    {
        // When
        CommitmentOpening first = CommitmentOpening.Create();
        CommitmentOpening second = CommitmentOpening.Create();

        // Then
        Assert.Equal(CommitmentOpening.Length, first.AsReadOnlySpan().Length);
        Assert.NotEqual(first, second);
    }

    [Fact(DisplayName = "Identical commitment openings should be considered equal")]
    public void IdenticalCommitmentOpeningsShouldBeConsideredEqual()
    {
        // Given
        CommitmentOpening left = new(Seed);
        CommitmentOpening right = new(Seed);

        // Then
        Assert.Equal(left, right);
        Assert.True(left == right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Theory(DisplayName = "CommitmentOpening.Expand should produce the RFC 5869 SHA-256 test vectors")]
    [InlineData("077709362c2e32df0ddc3f0dc47bba6390b6c73bb50f9c3122ec844ad7c2b3e5", "f0f1f2f3f4f5f6f7f8f9", "3cb25f25faacd57a90434f64d0362f2a2d2d0a90cf1a5a4c5db02d56ecc4c5bf")]
    [InlineData(
        "06a6b88c5853361a06104c9ceb35b45cef760014904671014a193f40c15fc244",
        "b0b1b2b3b4b5b6b7b8b9babbbcbdbebfc0c1c2c3c4c5c6c7c8c9cacbcccdcecfd0d1d2d3d4d5d6d7d8d9dadbdcdddedfe0e1e2e3e4e5e6e7e8e9eaebecedeeeff0f1f2f3f4f5f6f7f8f9fafbfcfdfeff",
        "b11e398dc80327a1c8e7f78c596a49344f012eda2d4efad8a050cc4c19afa97c")]
    [InlineData("19ef24a32c717b167f33a91d6f648bdf96596776afdb6377ac434c1c293ccb04", "", "8da4e775a563c18f715f802a063c5a31b8a11f5c5ee1879ec3454e5f3c738d2d")]
    public void CommitmentOpeningExpandShouldProduceTheRfc5869TestVectors(string pseudorandomKey, string info, string expected)
    {
        // Given
        // Why: RFC 5869 test cases 1–3 list longer outputs; HKDF output blocks do not depend on the requested length, so their first 32 bytes are the expected opening.
        byte[] key = Convert.FromHexString(pseudorandomKey);
        byte[] context = Convert.FromHexString(info);

        // When
        CommitmentOpening opening = CommitmentOpening.Expand(key, context);

        // Then
        Assert.Equal(expected, Convert.ToHexString(opening.AsReadOnlySpan()), ignoreCase: true);
    }

    [Theory(DisplayName = "CommitmentOpening.Derive should match HKDF Extract-then-Expand computed directly with HMAC")]
    [InlineData("SHA256", "")]
    [InlineData("SHA256", "aero/outcome/1")]
    [InlineData("SHA384", "aero/outcome/1")]
    [InlineData("SHA512", "aero/outcome/1")]
    public void CommitmentOpeningDeriveShouldMatchHkdfComputedDirectlyWithHmac(string algorithmName, string info)
    {
        // Given
        HashAlgorithmName algorithm = new(algorithmName);
        byte[] context = System.Text.Encoding.UTF8.GetBytes(info);
        byte[] expected = ComputeExtractThenExpand(algorithmName, Seed, context);

        // When
        CommitmentOpening opening = CommitmentOpening.Derive(Seed, context, algorithm);

        // Then
        Assert.Equal(expected, opening.AsReadOnlySpan().ToArray());
    }

    [Fact(DisplayName = "CommitmentOpening.Derive should be deterministic, and a different info should produce a different opening")]
    public void CommitmentOpeningDeriveShouldBeDeterministicAndBoundToInfo()
    {
        // When
        CommitmentOpening first = CommitmentOpening.Derive(Seed, "a"u8);
        CommitmentOpening second = CommitmentOpening.Derive(Seed, "a"u8);
        CommitmentOpening other = CommitmentOpening.Derive(Seed, "b"u8);

        // Then
        Assert.Equal(first, second);
        Assert.NotEqual(first, other);
    }

    [Fact(DisplayName = "CommitmentOpening.Derive should reject a seed shorter than 32 bytes")]
    public void CommitmentOpeningDeriveShouldRejectASeedShorterThan32Bytes()
    {
        // Given
        byte[] seed = new byte[31];

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => CommitmentOpening.Derive(seed, "a"u8));

        // Then
        Assert.StartsWith("The seed must be at least 32 bytes.", exception.Message);
    }

    [Fact(DisplayName = "CommitmentOpening.Expand should reject a pseudorandom key shorter than the hash output")]
    public void CommitmentOpeningExpandShouldRejectAPseudorandomKeyShorterThanTheHashOutput()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => CommitmentOpening.Expand(Seed, "a"u8, HashAlgorithmName.SHA512));

        // Then
        Assert.StartsWith("The pseudorandom key must be at least as long as the output of the hash algorithm.", exception.Message);
    }

    [Theory(DisplayName = "CommitmentOpening should reject unsupported hash algorithms")]
    [InlineData("SHA1")]
    [InlineData("MD5")]
    [InlineData("SHA3-256")]
    public void CommitmentOpeningShouldRejectUnsupportedHashAlgorithms(string algorithmName)
    {
        // Given
        HashAlgorithmName algorithm = new(algorithmName);

        // Then
        Assert.Throws<ArgumentException>(() => CommitmentOpening.Derive(Seed, "a"u8, algorithm));
        Assert.Throws<ArgumentException>(() => CommitmentOpening.Expand(Seed, "a"u8, algorithm));
    }

    /// <summary>
    /// Computes the first 32 bytes of HKDF Extract-then-Expand with an empty salt, directly from HMAC as RFC 5869 §2.2–2.3 defines it.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm.</param>
    /// <param name="seed">The input key material.</param>
    /// <param name="info">The context.</param>
    /// <returns>Returns the first 32 bytes of the output keying material.</returns>
    private static byte[] ComputeExtractThenExpand(string algorithmName, byte[] seed, byte[] info)
    {
        using HMAC extract = CreateHmac(algorithmName, []);
        byte[] pseudorandomKey = extract.ComputeHash(seed);

        using HMAC expand = CreateHmac(algorithmName, pseudorandomKey);
        byte[] firstBlock = expand.ComputeHash([.. info, 0x01]);

        return firstBlock[..32];
    }

    /// <summary>
    /// Creates the BCL HMAC implementation for the specified hash algorithm.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm.</param>
    /// <param name="key">The HMAC key.</param>
    /// <returns>Returns the BCL HMAC implementation for the specified hash algorithm.</returns>
    private static HMAC CreateHmac(string algorithmName, byte[] key) => algorithmName switch
    {
        "SHA256" => new HMACSHA256(key),
        "SHA384" => new HMACSHA384(key),
        "SHA512" => new HMACSHA512(key),
        _ => throw new ArgumentOutOfRangeException(nameof(algorithmName))
    };
}
