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
using OnixLabs.Security.Cryptography.UnitTests.Data;

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

    [Fact(DisplayName = "PrivateKey should throw ObjectDisposedException when read after being disposed")]
    public void PrivateKeyShouldThrowObjectDisposedExceptionWhenReadAfterBeingDisposed()
    {
        // Given
        PrivateKey candidate = new TestPrivateKey([1, 2, 3, 4]);

        // When
        candidate.Dispose();

        // Then
        Assert.Throws<ObjectDisposedException>(() => candidate.AsReadOnlyMemory());
    }

    [Fact(DisplayName = "PrivateKey should not throw when disposed more than once")]
    public void PrivateKeyShouldNotThrowWhenDisposedMoreThanOnce()
    {
        // Given
        PrivateKey candidate = new TestPrivateKey([1, 2, 3, 4]);

        // When / Then
        candidate.Dispose();
        candidate.Dispose();
    }

    [Theory(DisplayName = "PrivateKey.CopyTo should write exactly the original key data for lengths around the AES block size")]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(31)]
    [InlineData(32)]
    [InlineData(33)]
    [InlineData(300)]
    public void PrivateKeyCopyToShouldWriteExactlyTheOriginalKeyData(int length)
    {
        // Given
        byte[] expected = CreateKeyData(length);
        using PrivateKey candidate = new TestPrivateKey(expected);
        byte[] exact = new byte[length];
        byte[] larger = new byte[length + 40];
        larger.AsSpan().Fill(0xAA);

        // When
        candidate.CopyTo(exact);
        candidate.CopyTo(larger);

        // Then
        Assert.Equal(length, candidate.Length);
        Assert.Equal(expected, exact);
        Assert.Equal(expected, larger[..length]);
        Assert.All(larger[length..], value => Assert.Equal(0xAA, value));
    }

    [Fact(DisplayName = "PrivateKey.CopyTo should throw ArgumentException when the destination is too short")]
    public void PrivateKeyCopyToShouldThrowArgumentExceptionWhenTheDestinationIsTooShort()
    {
        // Given
        using PrivateKey candidate = new TestPrivateKey(CreateKeyData(32));

        // When / Then
        Assert.Throws<ArgumentException>(() => candidate.CopyTo(new byte[31]));
    }

    [Fact(DisplayName = "PrivateKey.CopyTo should throw ObjectDisposedException after disposal")]
    public void PrivateKeyCopyToShouldThrowObjectDisposedExceptionAfterDisposal()
    {
        // Given
        PrivateKey candidate = new TestPrivateKey(CreateKeyData(32));

        // When
        candidate.Dispose();

        // Then
        Assert.Throws<ObjectDisposedException>(() => candidate.CopyTo(new byte[32]));
    }

    [Fact(DisplayName = "PrivateKey construction should not modify the caller's key data")]
    public void PrivateKeyConstructionShouldNotModifyTheCallersKeyData()
    {
        // Given
        byte[] keyData = CreateKeyData(32);
        byte[] expected = [..keyData];

        // When
        using PrivateKey candidate = new TestPrivateKey(keyData);

        // Then
        Assert.Equal(expected, keyData);
    }

    [Fact(DisplayName = "PrivateKey.ToString should produce the Base16 key data for keys longer than the stack buffer")]
    public void PrivateKeyToStringShouldProduceTheBase16KeyDataForKeysLongerThanTheStackBuffer()
    {
        // Given
        byte[] keyData = CreateKeyData(300);
        using PrivateKey candidate = new TestPrivateKey(keyData);
        string expected = Convert.ToHexString(keyData).ToLowerInvariant();

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "PrivateKey equality should hold for distinct instances of the same key and type")]
    public void PrivateKeyEqualityShouldHoldForDistinctInstancesOfTheSameKeyAndType()
    {
        // Given
        using PrivateKey left = new TestPrivateKey(CreateKeyData(48));
        using PrivateKey right = new TestPrivateKey(CreateKeyData(48));

        // When / Then
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact(DisplayName = "PrivateKey equality should not hold for different private key types with the same key data")]
    public void PrivateKeyEqualityShouldNotHoldForDifferentTypesWithTheSameKeyData()
    {
        // Given
        using PrivateKey left = new TestPrivateKey(CreateKeyData(32));
        using PrivateKey right = new EcdsaPrivateKey(CreateKeyData(32));

        // When / Then
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "PrivateKey equality and hash codes should throw ObjectDisposedException after disposal")]
    public void PrivateKeyEqualityAndHashCodesShouldThrowObjectDisposedExceptionAfterDisposal()
    {
        // Given
        PrivateKey disposed = new TestPrivateKey(CreateKeyData(32));
        using PrivateKey other = new TestPrivateKey(CreateKeyData(32));
        disposed.Dispose();

        // When / Then
        Assert.Throws<ObjectDisposedException>(() => disposed.Equals(other));
        Assert.Throws<ObjectDisposedException>(() => other.Equals(disposed));
        Assert.Throws<ObjectDisposedException>(() => disposed.GetHashCode());
        Assert.True(disposed.Equals(disposed));
    }

    /// <summary>
    /// Creates deterministic key data of the specified length.
    /// </summary>
    /// <param name="length">The length of the key data.</param>
    /// <returns>Returns key data of the specified length, where byte i is (i * 7 + 3) mod 256.</returns>
    private static byte[] CreateKeyData(int length)
    {
        byte[] result = new byte[length];

        for (int index = 0; index < length; index++)
            result[index] = (byte)((index * 7 + 3) & 0xFF);

        return result;
    }
}
