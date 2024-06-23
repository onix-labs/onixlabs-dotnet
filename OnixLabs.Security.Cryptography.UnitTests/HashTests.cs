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
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
        Hash candidate = new(value);
        string actual = candidate.ToString();

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
        Hash candidate = new(value, length);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash value should not be modified when altering the original byte array")]
    public void HashValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Hash candidate = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash value should not be modified when altering the obtained byte array")]
    public void HashValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        Hash candidate = new([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        byte[] value = candidate.ToByteArray();
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical default hash values should be considered equal")]
    public void IdenticalDefaultHashValuesShouldBeConsideredEqual()
    {
        // Given
        Hash left = new();
        Hash right = default;

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Identical hash values should be considered equal")]
    public void IdenticalHashValuesShouldBeConsideredEqual()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Different hash values should not be considered equal")]
    public void DifferentHashValuesShouldNotBeConsideredEqual()
    {
        // Given
        Hash left = new([1, 2, 3, 4]);
        Hash right = new([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        Assert.False(left.Equals(right));
        Assert.False(left == right);
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
        Hash candidate = Hash.Compute(algorithm, bytes);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a byte array with two rounds")]
    [InlineData("abc123", "MD5", "b106dc6352e5ec1f8aafd8c406d34d92")]
    [InlineData("abc123", "SHA1", "6691484ea6b50ddde1926a220da01fa9e575c18a")]
    [InlineData("abc123", "SHA256", "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8")]
    [InlineData("abc123", "SHA384", "d58e9a112b8c637df5d2e33af03ce738dd1c57657243d70d2fa8f76a99fa9a0e2f4abf50d9a88e8958f2d5f6fa002190")]
    [InlineData("abc123", "SHA512", "c2c9d705d7a1ed34247649bbe64c6edd2035e0a4c9ae1c063170f5ee2aeca09125cc0a8b30593c07a18801d6e0570de22e8dc40a59bc1f59a49834c05ed49949")]
    public void HashComputeShouldProduceTheExpectedHashUsingAByteArrayWithTwoRounds(string data, string algorithmName, string expected)
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
        Hash candidate = Hash.Compute(algorithm, bytes, 2);
        string actual = candidate.ToString();

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
        Hash candidate = Hash.Compute(algorithm, bytes, offset, count);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a byte array with an offset, count and two rounds")]
    [InlineData("abc123", 1, 3, "MD5", "05787e59f464916f6dfdf5bf5996e152")]
    [InlineData("abc123", 1, 3, "SHA1", "33dcbf41c6e49b12ad37b5dab4d95dcc6ee71f49")]
    [InlineData("abc123", 1, 3, "SHA256", "5a9828edeba2a57521b2d40648cc69a4e0c236111dfae612075399ba588eee91")]
    [InlineData("abc123", 1, 3, "SHA384", "5691be426e2e501f5598cfc0355fc7de2c1c15637daf98ee09bf7d1da75463bf33a96a2164facbd535515c54e5d56920")]
    [InlineData("abc123", 1, 3, "SHA512", "74da074abe82913bc91a3079ac7d55b8bb1e111d10647b31d6c881a93427ebc57a6c4aeca5efba612e9b71c38e6601a13df0e7d73e1530ed65453c8926404186")]
    public void HashComputeShouldProduceTheExpectedHashUsingAByteArrayWithAnOffsetCountAndTwoRounds(string data, int offset, int count, string algorithmName, string expected)
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
        Hash candidate = Hash.Compute(algorithm, bytes, offset, count, 2);
        string actual = candidate.ToString();

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
        Hash candidate = Hash.Compute(algorithm, stream);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.Compute should produce the expected hash using a stream and two rounds")]
    [InlineData("abc123", "MD5", "b106dc6352e5ec1f8aafd8c406d34d92")]
    [InlineData("abc123", "SHA1", "6691484ea6b50ddde1926a220da01fa9e575c18a")]
    [InlineData("abc123", "SHA256", "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8")]
    [InlineData("abc123", "SHA384", "d58e9a112b8c637df5d2e33af03ce738dd1c57657243d70d2fa8f76a99fa9a0e2f4abf50d9a88e8958f2d5f6fa002190")]
    [InlineData("abc123", "SHA512", "c2c9d705d7a1ed34247649bbe64c6edd2035e0a4c9ae1c063170f5ee2aeca09125cc0a8b30593c07a18801d6e0570de22e8dc40a59bc1f59a49834c05ed49949")]
    public void HashComputeShouldProduceTheExpectedHashUsingAStreamAndTwoRounds(string data, string algorithmName, string expected)
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
        Hash candidate = Hash.Compute(algorithm, stream, 2);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.ComputeAsync should produce the expected hash using a stream")]
    [InlineData("abc123", "MD5", "e99a18c428cb38d5f260853678922e03")]
    [InlineData("abc123", "SHA1", "6367c48dd193d56ea7b0baad25b19455e529f5ee")]
    [InlineData("abc123", "SHA256", "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090")]
    [InlineData("abc123", "SHA384", "a31d79891919cad24f3264479d76884f581bee32e86778373db3a124de975dd86a40fc7f399b331133b281ab4b11a6ca")]
    [InlineData("abc123", "SHA512", "c70b5dd9ebfb6f51d09d4132b7170c9d20750a7852f00680f65658f0310e810056e6763c34c9a00b0e940076f54495c169fc2302cceb312039271c43469507dc")]
    public async Task HashComputeAsyncShouldProduceTheExpectedHashUsingAStream(string data, string algorithmName, string expected)
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
        Hash candidate = await Hash.ComputeAsync(algorithm, stream);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Hash.ComputeAsync should produce the expected hash using a stream and two rounds")]
    [InlineData("abc123", "MD5", "b106dc6352e5ec1f8aafd8c406d34d92")]
    [InlineData("abc123", "SHA1", "6691484ea6b50ddde1926a220da01fa9e575c18a")]
    [InlineData("abc123", "SHA256", "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8")]
    [InlineData("abc123", "SHA384", "d58e9a112b8c637df5d2e33af03ce738dd1c57657243d70d2fa8f76a99fa9a0e2f4abf50d9a88e8958f2d5f6fa002190")]
    [InlineData("abc123", "SHA512", "c2c9d705d7a1ed34247649bbe64c6edd2035e0a4c9ae1c063170f5ee2aeca09125cc0a8b30593c07a18801d6e0570de22e8dc40a59bc1f59a49834c05ed49949")]
    public async Task HashComputeAsyncShouldProduceTheExpectedHashUsingAStreamAndTwoRounds(string data, string algorithmName, string expected)
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
        Hash candidate = await Hash.ComputeAsync(algorithm, stream, 2);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash.ToNamedHash should produce the expected result")]
    public void HashToNamedHashShouldProduceExpectedResult()
    {
        // Given
        const string expected = "Sha256:9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
        Hash hash = Hash.Compute(SHA256.Create(), "test");
        NamedHash namedHash = hash.ToNamedHash("Sha256");

        // When
        string actual = namedHash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Hash.Concatenate should produce the expected result")]
    public void HashConcatenateShouldProduceExpectedResult()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        Hash left = Hash.Compute(algorithm, "abc");
        Hash right = Hash.Compute(algorithm, "123");
        const string expected = "c1702b0f46b4a0d3ef1e19feef861727cc583a157594e264571126818c820592";

        // When
        Hash concatenated = left.Concatenate(algorithm, right);
        string actual = concatenated.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
