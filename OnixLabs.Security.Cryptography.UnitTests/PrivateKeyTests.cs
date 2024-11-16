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

using OnixLabs.Security.Cryptography.UnitTests.Data;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class PrivateKeyTests
{
    [Fact(DisplayName = "PrivateKey should be constructable from bytes")]
    public void PrivateKeyShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        PrivateKey candidate = new TestPrivateKey(value);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "PrivateKey value should not be modified when altering the original byte array")]
    public void PrivateKeyValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        PrivateKey candidate = new TestPrivateKey(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "PrivateKey value should not be modified when altering the obtained byte array")]
    public void PrivateKeyValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        PrivateKey candidate = new TestPrivateKey([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical public key values should be considered equal")]
    public void IdenticalPrivateKeyValuesShouldBeConsideredEqual()
    {
        // Given
        PrivateKey left = new TestPrivateKey([1, 2, 3, 4]);
        PrivateKey right = new TestPrivateKey([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.True(left.Equals(right));
        Assert.True(left == right);
    }

    [Fact(DisplayName = "Different public key values should not be considered equal")]
    public void DifferentPrivateKeyValuesShouldNotBeConsideredEqual()
    {
        // Given
        PrivateKey left = new TestPrivateKey([1, 2, 3, 4]);
        PrivateKey right = new TestPrivateKey([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical public key values should produce identical hash codes")]
    public void IdenticalPrivateKeyValuesShouldProduceIdenticalPrivateKeyCodes()
    {
        // Given
        PrivateKey left = new TestPrivateKey([1, 2, 3, 4]);
        PrivateKey right = new TestPrivateKey([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different public key values should produce different hash codes")]
    public void DifferentPrivateKeyValuesShouldProduceDifferentPrivateKeyCodes()
    {
        // Given
        PrivateKey left = new TestPrivateKey([1, 2, 3, 4]);
        PrivateKey right = new TestPrivateKey([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "PrivateKey.ToNamedPrivateKey should produce the expected result")]
    public void PrivateKeyToNamedPrivateKeyShouldProduceExpectedResult()
    {
        // Given
        PrivateKey privateKey = new TestPrivateKey([1, 2, 3, 4]);

        // When
        NamedPrivateKey namedPrivateKey = privateKey.ToNamedPrivateKey();

        // Then
        Assert.Equal("TEST", namedPrivateKey.AlgorithmName);
        Assert.Equal(privateKey, namedPrivateKey.PrivateKey);
    }
}
