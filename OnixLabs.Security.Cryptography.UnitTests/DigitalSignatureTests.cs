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
using System.Buffers;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class DigitalSignatureTests
{
    [Fact(DisplayName = "DigitalSignature should be constructable from bytes")]
    public void DigitalSignatureShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        DigitalSignature candidate = new(value);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "DigitalSignature value should not be modified when altering the original byte array")]
    public void DigitalSignatureValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        DigitalSignature candidate = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "DigitalSignature value should not be modified when altering the obtained byte array")]
    public void DigitalSignatureValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        DigitalSignature candidate = new([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical default signature values should be considered equal")]
    public void IdenticalDefaultDigitalSignatureValuesShouldBeConsideredEqual()
    {
        // Given
        DigitalSignature left = new();
        DigitalSignature right = default;

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Identical signature values should be considered equal")]
    public void IdenticalDigitalSignatureValuesShouldBeConsideredEqual()
    {
        // Given
        DigitalSignature left = new([1, 2, 3, 4]);
        DigitalSignature right = new([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Different signature values should not be considered equal")]
    public void DifferentDigitalSignatureValuesShouldNotBeConsideredEqual()
    {
        // Given
        DigitalSignature left = new([1, 2, 3, 4]);
        DigitalSignature right = new([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        Assert.False(left.Equals(right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical signature values should produce identical hash codes")]
    public void IdenticalDigitalSignatureValuesShouldProduceIdenticalDigitalSignatureCodes()
    {
        // Given
        DigitalSignature left = new([1, 2, 3, 4]);
        DigitalSignature right = new([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different signature values should produce different hash codes")]
    public void DifferentDigitalSignatureValuesShouldProduceDifferentDigitalSignatureCodes()
    {
        // Given
        DigitalSignature left = new([1, 2, 3, 4]);
        DigitalSignature right = new([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "DigitalSignature constructed from ReadOnlyMemory and ReadOnlySequence should equal the byte-array form")]
    public void DigitalSignatureFromMemoryAndSequenceShouldEqualByteArrayForm()
    {
        // Given
        byte[] value = [1, 2, 3, 4, 5, 6, 7, 8];
        DigitalSignature expected = new(value);

        // When
        DigitalSignature fromMemory = new(new ReadOnlyMemory<byte>(value));
        DigitalSignature fromSequence = new(new ReadOnlySequence<byte>(value));

        // Then
        Assert.Equal(expected, fromMemory);
        Assert.Equal(expected, fromSequence);
    }

    [Fact(DisplayName = "DigitalSignature.Parse should round-trip through ToString")]
    public void DigitalSignatureParseShouldRoundTripThroughToString()
    {
        // Given
        DigitalSignature expected = new([10, 20, 30, 40, 50, 60, 70, 80]);

        // When
        DigitalSignature fromString = DigitalSignature.Parse(expected.ToString());
        DigitalSignature fromSpan = DigitalSignature.Parse(expected.ToString().AsSpan());

        // Then
        Assert.Equal(expected, fromString);
        Assert.Equal(expected, fromSpan);
    }

    [Fact(DisplayName = "DigitalSignature.TryParse should succeed for a valid value and fail for an invalid value")]
    public void DigitalSignatureTryParseShouldProduceExpectedResult()
    {
        // Given
        DigitalSignature expected = new([1, 2, 3, 4]);

        // When
        bool valid = DigitalSignature.TryParse(expected.ToString(), null, out DigitalSignature parsed);
        bool invalid = DigitalSignature.TryParse("not-hex!", null, out DigitalSignature defaulted);

        // Then
        Assert.True(valid);
        Assert.Equal(expected, parsed);
        Assert.False(invalid);
        Assert.Equal(default, defaulted);
    }

    [Fact(DisplayName = "DigitalSignature.AsReadOnlyMemory and AsReadOnlySpan should expose the underlying value")]
    public void DigitalSignatureAsReadOnlyShouldExposeUnderlyingValue()
    {
        // Given
        byte[] value = [9, 8, 7, 6, 5];
        DigitalSignature candidate = new(value);

        // When / Then
        Assert.True(candidate.AsReadOnlyMemory().Span.SequenceEqual(value));
        Assert.True(candidate.AsReadOnlySpan().SequenceEqual(value));
    }
}
