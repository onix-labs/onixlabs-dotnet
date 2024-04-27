// Copyright 2020-2024 ONIXLabs
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

using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdsaPrivateKeyTests
{
    [Fact(DisplayName = "EcdsaPrivateKey should be constructable from bytes")]
    public void EcdsaPrivateKeyShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        EcdsaPrivateKey privateKey = new(value);
        string actual = privateKey.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey value should not be modified when altering the original byte array")]
    public void EcdsaPrivateKeyValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        EcdsaPrivateKey privateKey = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = privateKey.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey value should not be modified when altering the obtained byte array")]
    public void EcdsaPrivateKeyValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        EcdsaPrivateKey privateKey = new([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        byte[] value = privateKey.ToByteArray();
        value[0] = 0;
        string actual = privateKey.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical signature values should be considered equal")]
    public void IdenticalEcdsaPrivateKeyValuesShouldBeConsideredEqual()
    {
        // Given
        EcdsaPrivateKey left = new([1, 2, 3, 4]);
        EcdsaPrivateKey right = new([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.True(left.Equals(right));
        Assert.True(left == right);
    }

    [Fact(DisplayName = "Different signature values should not be considered equal")]
    public void DifferentEcdsaPrivateKeyValuesShouldNotBeConsideredEqual()
    {
        // Given
        EcdsaPrivateKey left = new([1, 2, 3, 4]);
        EcdsaPrivateKey right = new([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical signature values should produce identical hash codes")]
    public void IdenticalEcdsaPrivateKeyValuesShouldProduceIdenticalEcdsaPrivateKeyCodes()
    {
        // Given
        EcdsaPrivateKey left = new([1, 2, 3, 4]);
        EcdsaPrivateKey right = new([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different signature values should produce different hash codes")]
    public void DifferentEcdsaPrivateKeyValuesShouldProduceDifferentEcdsaPrivateKeyCodes()
    {
        // Given
        EcdsaPrivateKey left = new([1, 2, 3, 4]);
        EcdsaPrivateKey right = new([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }
}
