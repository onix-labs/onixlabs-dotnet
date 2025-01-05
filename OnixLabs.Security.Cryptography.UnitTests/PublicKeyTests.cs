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

using System.Security.Cryptography;
using OnixLabs.Security.Cryptography.UnitTests.Data;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class PublicKeyTests
{
    [Fact(DisplayName = "PublicKey should be constructable from bytes")]
    public void PublicKeyShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        PublicKey candidate = new TestPublicKey(value);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "PublicKey value should not be modified when altering the original byte array")]
    public void PublicKeyValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        PublicKey candidate = new TestPublicKey(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "PublicKey value should not be modified when altering the obtained byte array")]
    public void PublicKeyValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        PublicKey candidate = new TestPublicKey([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical public key values should be considered equal")]
    public void IdenticalPublicKeyValuesShouldBeConsideredEqual()
    {
        // Given
        PublicKey left = new TestPublicKey([1, 2, 3, 4]);
        PublicKey right = new TestPublicKey([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.True(left.Equals(right));
        Assert.True(left == right);
    }

    [Fact(DisplayName = "Different public key values should not be considered equal")]
    public void DifferentPublicKeyValuesShouldNotBeConsideredEqual()
    {
        // Given
        PublicKey left = new TestPublicKey([1, 2, 3, 4]);
        PublicKey right = new TestPublicKey([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical public key values should produce identical hash codes")]
    public void IdenticalPublicKeyValuesShouldProduceIdenticalPublicKeyCodes()
    {
        // Given
        PublicKey left = new TestPublicKey([1, 2, 3, 4]);
        PublicKey right = new TestPublicKey([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different public key values should produce different hash codes")]
    public void DifferentPublicKeyValuesShouldProduceDifferentPublicKeyCodes()
    {
        // Given
        PublicKey left = new TestPublicKey([1, 2, 3, 4]);
        PublicKey right = new TestPublicKey([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "PublicKey.GetHash should produce the expected result")]
    public void PublicKeyGetHashShouldProduceTheExpectedResult()
    {
        // Given
        const string expected = "9f64a747e1b97f131fabb6b447296c9b6f0201e79fb3c5356e6c77e89b6a806a";
        PublicKey publicKey = new TestPublicKey([1, 2, 3, 4]);

        // When
        Hash hash = publicKey.GetHash(SHA256.Create());
        string actual = hash.ToString();

        // then
        Assert.Equal(expected, actual);
    }
}
