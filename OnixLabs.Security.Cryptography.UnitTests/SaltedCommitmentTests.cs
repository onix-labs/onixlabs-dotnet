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
using System.Buffers.Binary;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class SaltedCommitmentTests
{
    private const string Domain = "aero/outcome";
    private static readonly byte[] OpeningBytes = Enumerable.Range(0, 32).Select(index => (byte)index).ToArray();
    private static readonly CommitmentOpening Opening = new(OpeningBytes);
    private static readonly byte[] Value = "approved"u8.ToArray();

    [Theory(DisplayName = "SaltedCommitment.Create should match HMAC computed directly over the domain-separated message")]
    [InlineData("SHA256", "aero/outcome", 0)]
    [InlineData("SHA256", "aero/outcome", 8)]
    [InlineData("SHA256", "aero/outcome", 1048576)]
    [InlineData("SHA256", "a", 8)]
    [InlineData("SHA256", "Ünïcødé domain", 8)]
    [InlineData("SHA384", "aero/outcome", 0)]
    [InlineData("SHA384", "aero/outcome", 8)]
    [InlineData("SHA384", "aero/outcome", 1048576)]
    [InlineData("SHA512", "aero/outcome", 0)]
    [InlineData("SHA512", "aero/outcome", 8)]
    [InlineData("SHA512", "aero/outcome", 1048576)]
    public void SaltedCommitmentCreateShouldMatchHmacComputedDirectly(string algorithmName, string domain, int valueLength)
    {
        // Given
        byte[] value = Enumerable.Range(0, valueLength).Select(index => (byte)(index * 31)).ToArray();
        byte[] expected = ComputeExpected(algorithmName, OpeningBytes, domain, value);

        // When
        SaltedCommitment commitment = SaltedCommitment.Create(value, domain, Opening, new HashAlgorithmName(algorithmName));

        // Then
        Assert.Equal(expected, commitment.AsReadOnlySpan().ToArray());
        Assert.Equal(algorithmName, commitment.AlgorithmName);
    }

    [Fact(DisplayName = "SaltedCommitment.Create should use HMAC-SHA-256 by default")]
    public void SaltedCommitmentCreateShouldUseHmacSha256ByDefault()
    {
        // Given
        byte[] expected = ComputeExpected("SHA256", OpeningBytes, Domain, Value);

        // When
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // Then
        Assert.Equal(expected, commitment.AsReadOnlySpan().ToArray());
        Assert.Equal("SHA256", commitment.AlgorithmName);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should return true for the committed value, domain and opening")]
    public void SaltedCommitmentVerifyShouldReturnTrueForTheCommittedValueDomainAndOpening()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, out CommitmentOpening opening);

        // When
        bool result = commitment.Verify(Value, Domain, opening);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should return false for a wrong value")]
    public void SaltedCommitmentVerifyShouldReturnFalseForAWrongValue()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // When
        bool result = commitment.Verify("rejected"u8, Domain, Opening);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should return false for a wrong opening")]
    public void SaltedCommitmentVerifyShouldReturnFalseForAWrongOpening()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // When
        bool result = commitment.Verify(Value, Domain, CommitmentOpening.Create());

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should return false for a wrong domain")]
    public void SaltedCommitmentVerifyShouldReturnFalseForAWrongDomain()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // When
        bool result = commitment.Verify(Value, "aero/other", Opening);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should not confuse the boundary between domain and value")]
    public void SaltedCommitmentVerifyShouldNotConfuseTheBoundaryBetweenDomainAndValue()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create("bc"u8, "a", Opening);

        // When
        bool result = commitment.Verify("c"u8, "ab", Opening);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "SaltedCommitment.Verify should return false for a default opening or a default commitment")]
    public void SaltedCommitmentVerifyShouldReturnFalseForADefaultOpeningOrADefaultCommitment()
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // Then
        Assert.False(commitment.Verify(Value, Domain, default));
        Assert.False(default(SaltedCommitment).Verify(Value, Domain, Opening));
    }

    [Fact(DisplayName = "SaltedCommitment.Create should reject a default opening")]
    public void SaltedCommitmentCreateShouldRejectADefaultOpening()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => SaltedCommitment.Create(Value, Domain, default(CommitmentOpening)));

        // Then
        Assert.StartsWith("The commitment opening must be exactly 32 bytes.", exception.Message);
    }

    [Theory(DisplayName = "SaltedCommitment should reject a null, empty or whitespace domain")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SaltedCommitmentShouldRejectANullEmptyOrWhitespaceDomain(string? domain)
    {
        // Given
        SaltedCommitment commitment = SaltedCommitment.Create(Value, Domain, Opening);

        // Then
        Assert.Throws<ArgumentException>(() => SaltedCommitment.Create(Value, domain!, Opening));
        Assert.Throws<ArgumentException>(() => commitment.Verify(Value, domain!, Opening));
    }

    [Theory(DisplayName = "SaltedCommitment.Create should reject unsupported hash algorithms")]
    [InlineData("SHA1")]
    [InlineData("MD5")]
    [InlineData("SHA3-256")]
    public void SaltedCommitmentCreateShouldRejectUnsupportedHashAlgorithms(string algorithmName)
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => SaltedCommitment.Create(Value, Domain, Opening, new HashAlgorithmName(algorithmName)));

        // Then
        Assert.StartsWith("The hash algorithm must be SHA-256, SHA-384 or SHA-512.", exception.Message);
    }

    [Fact(DisplayName = "Two random openings for the same value should produce different commitments")]
    public void TwoRandomOpeningsForTheSameValueShouldProduceDifferentCommitments()
    {
        // When
        SaltedCommitment first = SaltedCommitment.Create(Value, Domain, out CommitmentOpening firstOpening);
        SaltedCommitment second = SaltedCommitment.Create(Value, Domain, out CommitmentOpening secondOpening);

        // Then
        Assert.NotEqual(firstOpening, secondOpening);
        Assert.NotEqual(first, second);
    }

    [Theory(DisplayName = "SaltedCommitment should round-trip through its binary representation")]
    [InlineData("SHA256")]
    [InlineData("SHA384")]
    [InlineData("SHA512")]
    public void SaltedCommitmentShouldRoundTripThroughItsBinaryRepresentation(string algorithmName)
    {
        // Given
        HashAlgorithmName algorithm = new(algorithmName);
        SaltedCommitment expected = SaltedCommitment.Create(Value, Domain, Opening, algorithm);

        // When
        SaltedCommitment actual = new(new Hash(expected.AsReadOnlySpan()), algorithm);

        // Then
        Assert.Equal(expected, actual);
        Assert.True(actual.Verify(Value, Domain, Opening));
    }

    [Theory(DisplayName = "SaltedCommitment should round-trip through its string representation")]
    [InlineData("SHA256")]
    [InlineData("SHA384")]
    [InlineData("SHA512")]
    public void SaltedCommitmentShouldRoundTripThroughItsStringRepresentation(string algorithmName)
    {
        // Given
        SaltedCommitment expected = SaltedCommitment.Create(Value, Domain, Opening, new HashAlgorithmName(algorithmName));

        // When
        SaltedCommitment fromDefault = SaltedCommitment.Parse(expected.ToString());
        SaltedCommitment fromBase64 = SaltedCommitment.Parse(expected.ToString(Base64FormatProvider.UnpaddedRfc4648Url), Base64FormatProvider.UnpaddedRfc4648Url);

        // Then
        Assert.StartsWith($"{algorithmName}:", expected.ToString());
        Assert.Equal(expected, fromDefault);
        Assert.Equal(expected, fromBase64);
    }

    [Theory(DisplayName = "SaltedCommitment.TryParse should reject malformed values")]
    [InlineData("")]
    [InlineData("SHA256")]
    [InlineData("SHA256:")]
    [InlineData(":0011")]
    [InlineData("MD5:00112233445566778899aabbccddeeff")]
    [InlineData("SHA256:00112233445566778899aabbccddeeff")]
    [InlineData("SHA384:000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f")]
    [InlineData("SHA256:not hex")]
    public void SaltedCommitmentTryParseShouldRejectMalformedValues(string value)
    {
        // When
        bool result = SaltedCommitment.TryParse(value, null, out SaltedCommitment _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "SaltedCommitment constructor should reject a hash whose length does not match the algorithm")]
    public void SaltedCommitmentConstructorShouldRejectAHashWhoseLengthDoesNotMatchTheAlgorithm()
    {
        // Given
        Hash hash = new(0, 32);

        // When
        Exception exception = Assert.Throws<ArgumentException>(() => new SaltedCommitment(hash, HashAlgorithmName.SHA512));

        // Then
        Assert.StartsWith("The length of the commitment does not match the output of the hash algorithm.", exception.Message);
    }

    /// <summary>
    /// Computes the expected commitment directly with the BCL HMAC, building the message from its parts.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm.</param>
    /// <param name="key">The opening, used as the HMAC key.</param>
    /// <param name="domain">The domain.</param>
    /// <param name="value">The committed value.</param>
    /// <returns>Returns <c>HMAC(key, UInt32BE(len(UTF8(domain))) ‖ UTF8(domain) ‖ value)</c>.</returns>
    private static byte[] ComputeExpected(string algorithmName, byte[] key, string domain, byte[] value)
    {
        byte[] domainBytes = Encoding.UTF8.GetBytes(domain);
        byte[] length = new byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(length, (uint)domainBytes.Length);
        byte[] message = [.. length, .. domainBytes, .. value];

        using HMAC hmac = algorithmName switch
        {
            "SHA256" => new HMACSHA256(key),
            "SHA384" => new HMACSHA384(key),
            "SHA512" => new HMACSHA512(key),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithmName))
        };

        return hmac.ComputeHash(message);
    }
}
