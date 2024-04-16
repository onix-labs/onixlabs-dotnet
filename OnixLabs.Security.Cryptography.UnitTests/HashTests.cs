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

using System;
using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class HashTests
{
    [Fact(DisplayName = "Hash should be constructable from bytes")]
    public void HashShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        Hash hash = new(value);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash should be constructable from byte and length")]
    public void HashShouldBeConstructableFromByteAndLength()
    {
        // Given
        const byte value = 0xF0;
        const int length = 16;
        const string expected = "f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0f0";

        // When
        Hash hash = new(value, length);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash.Empty should produce an empty hash value")]
    public void HashEmptyShouldProduceAnEmptyHashValue()
    {
        // Given
        const string expected = "";

        // When
        Hash hash = Hash.Empty;
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash value should not be modified when altering the original byte array")]
    public void HashValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Hash hash = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash value should not be modified when altering the obtained byte array")]
    public void HashValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        Hash hash = new([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        byte[] value = hash.ToByteArray();
        value[0] = 0;
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical hash values should be considered equal")]
    public void IdenticalHashesShouldBeConsideredEqual()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.True(left.Equals(right));
        Assert.True(left == right);
    }

    [Fact(DisplayName = "Different hash values should not be considered equal")]
    public void DifferentHashValuesShouldNotBeConsideredEqual()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical hash values should produce identical hash codes")]
    public void IdenticalHashValuesShouldProduceIdenticalHashCodes()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different hash values should produce different hash codes")]
    public void DifferentHashValuesShouldProduceDifferentHashCodes()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Hashes should produce a negative-one sort order when the left-hand hash is lesser than the right-hand hash")]
    public void HashesShouldProduceANegativeOneSortOrderWhenTheLeftHandHashIsLesserThanTheRightHandHash()
    {
        // Given
        Hash left = new([1]);
        Hash right = new([2]);
        const int expected = -1;

        // When
        int actual = left.CompareTo(right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hashes should produce a positive-one sort order when the left-hand hash is greater than the right-hand hash")]
    public void HashesShouldProduceAPositiveOneSortOrderWhenTheLeftHandHashIsGreaterThanTheRightHandHash()
    {
        // Given
        Hash left = new([2]);
        Hash right = new([1]);
        const int expected = 1;

        // When
        int actual = left.CompareTo(right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hashes should produce a zero sort order when the left-hand hash is equal to the right-hand hash")]
    public void HashesShouldProduceAZeroSortOrderWhenTheLeftHandHashIsEqualToTheRightHandHash()
    {
        // Given
        Hash left = new([1]);
        Hash right = new([1]);
        const int expected = 0;

        // When
        int actual = left.CompareTo(right);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a byte array")]
    [InlineData("abc123", "MD5", "e99a18c428cb38d5f260853678922e03")]
    [InlineData("abc123", "SHA1", "6367c48dd193d56ea7b0baad25b19455e529f5ee")]
    [InlineData("abc123", "SHA256", "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090")]
    [InlineData("abc123", "SHA384", "a31d79891919cad24f3264479d76884f581bee32e86778373db3a124de975dd86a40fc7f399b331133b281ab4b11a6ca")]
    [InlineData("abc123", "SHA512", "c70b5dd9ebfb6f51d09d4132b7170c9d20750a7852f00680f65658f0310e810056e6763c34c9a00b0e940076f54495c169fc2302cceb312039271c43469507dc")]
    public void HashComputeShouldProduceTheExpectedHashUsingAByteArray(string data, string algorithmName, string expected)
    {
        // Given
        byte[] bytes = data.ToByteArray();
        HashAlgorithm algorithm = algorithmName switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"Unknown hash algorithm name: {algorithmName}.")
        };

        // When
        Hash hash = Hash.Compute(algorithm, bytes);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a byte array with an offset and count")]
    [InlineData("abc123", 1, 3, "MD5", "b79f52be223290bd34f94e92aa8b0bdd")]
    [InlineData("abc123", 1, 3, "SHA1", "be4a30dd01a93831a222262d9fa288c4f016b822")]
    [InlineData("abc123", 1, 3, "SHA256", "fa54bf6e8e528001735fcc222c3ef5b99c46f469d9340deae3d9577818a6fe5a")]
    [InlineData("abc123", 1, 3, "SHA384", "3cfd879e784ed23f3e9142775218bbaf636bd5413d32583a10f79f6b63028cbe9e241273dabe293c27876db2ecbaa594")]
    [InlineData("abc123", 1, 3, "SHA512", "65f2fea7da1b1e470169d7f861000047ac78e00a024f5973322e5850d5fd61ceb94b7252629426bfa4beb3dafc9f55c747b5b2a8374f545e19148e61ef0057cc")]
    public void HashComputeShouldProduceTheExpectedHashUsingAByteArrayWithAnOffsetAndCount(string data, int offset, int count, string algorithmName, string expected)
    {
        // Given
        byte[] bytes = data.ToByteArray();
        HashAlgorithm algorithm = algorithmName switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"Unknown hash algorithm name: {algorithmName}.")
        };

        // When
        Hash hash = Hash.Compute(algorithm, bytes, offset, count);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a stream")]
    [InlineData("abc123", "MD5", "e99a18c428cb38d5f260853678922e03")]
    [InlineData("abc123", "SHA1", "6367c48dd193d56ea7b0baad25b19455e529f5ee")]
    [InlineData("abc123", "SHA256", "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090")]
    [InlineData("abc123", "SHA384", "a31d79891919cad24f3264479d76884f581bee32e86778373db3a124de975dd86a40fc7f399b331133b281ab4b11a6ca")]
    [InlineData("abc123", "SHA512", "c70b5dd9ebfb6f51d09d4132b7170c9d20750a7852f00680f65658f0310e810056e6763c34c9a00b0e940076f54495c169fc2302cceb312039271c43469507dc")]
    public void HashComputeShouldProduceTheExpectedHashUsingAStream(string data, string algorithmName, string expected)
    {
        // Given
        Stream stream = new MemoryStream(data.ToByteArray());
        HashAlgorithm algorithm = algorithmName switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"Unknown hash algorithm name: {algorithmName}.")
        };

        // When
        Hash hash = Hash.Compute(algorithm, stream);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.ComputeAsync should produce the expected hash using a stream")]
    [InlineData("abc123", "MD5", "e99a18c428cb38d5f260853678922e03")]
    [InlineData("abc123", "SHA1", "6367c48dd193d56ea7b0baad25b19455e529f5ee")]
    [InlineData("abc123", "SHA256", "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090")]
    [InlineData("abc123", "SHA384", "a31d79891919cad24f3264479d76884f581bee32e86778373db3a124de975dd86a40fc7f399b331133b281ab4b11a6ca")]
    [InlineData("abc123", "SHA512", "c70b5dd9ebfb6f51d09d4132b7170c9d20750a7852f00680f65658f0310e810056e6763c34c9a00b0e940076f54495c169fc2302cceb312039271c43469507dc")]
    public async void HashComputeAsyncShouldProduceTheExpectedHashUsingAStream(string data, string algorithmName, string expected)
    {
        // Given
        Stream stream = new MemoryStream(data.ToByteArray());
        HashAlgorithm algorithm = algorithmName switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"Unknown hash algorithm name: {algorithmName}.")
        };

        // When
        Hash hash = await Hash.ComputeHashAsync(algorithm, stream);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
