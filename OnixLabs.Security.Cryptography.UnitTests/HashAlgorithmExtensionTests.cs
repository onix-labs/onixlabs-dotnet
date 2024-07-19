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
using OnixLabs.Core;
using OnixLabs.Security.Cryptography.UnitTests.Data;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class HashAlgorithmExtensionTests
{
    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce the expected result with two rounds")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultWithTwoRounds()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash("abc123", rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce the expected result with one round")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultWithOneRound()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        const string expected = "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090";

        // When
        byte[] bytes = algorithm.ComputeHash("abc123", rounds: 1);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should throw CryptographicException for zero rounds")]
    public void HashAlgorithmComputeHashShouldThrowForZeroRounds()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();

        // Then
        Assert.Throws<CryptographicException>(() => algorithm.ComputeHash("abc123", rounds: 0));
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash with offset and count should produce the expected result")]
    public void HashAlgorithmComputeHashWithOffsetAndCountShouldProduceExpectedResult()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        byte[] data = "abcdefghijklmnopqrstuvwxyz".ToByteArray();
        const string expected = "c8507de92a6160c0437f47cfa8d1bfab6fa77137b466d8f0685e393b06ea5089";

        // When
        byte[] bytes = algorithm.ComputeHash(data, offset: 1, count: 24, rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce expected result from ReadOnlySpan<char>")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultFromReadOnlySpanChar()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        const string input = "abc123";
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash(input.AsSpan(), rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce expected result from IBinaryConvertible")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultFromIBinaryConvertible()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        BinaryConvertible data = new("abc123".ToByteArray());
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash(data, rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce expected result from ISpanBinaryConvertible")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultFromISpanBinaryConvertible()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        BinaryConvertible data = new("abc123".ToByteArray());
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash(data, rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce expected result from Stream")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultFromStream()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        using MemoryStream stream = new("abc123".ToByteArray());
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash(stream, rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.TryComputeHash should return true and produce expected result")]
    public void HashAlgorithmTryComputeHashShouldReturnTrueAndProduceExpectedResult()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        byte[] data = "abc123".ToByteArray();
        Span<byte> destination = stackalloc byte[32];
        const string expected = "6ca13d52ca70c883e0f0bb101e425a89e8624de51db2d2392593af6a84118090";

        // When
        bool result = algorithm.TryComputeHash(data, destination, 0, data.Length, 1, out int bytesWritten);
        string actual = Convert.ToHexString(destination).ToLower();

        // Then
        Assert.True(result);
        Assert.Equal(32, bytesWritten);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.TryComputeHash should return false for insufficient destination length")]
    public void HashAlgorithmTryComputeHashShouldReturnFalseForInsufficientDestinationLength()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        byte[] data = "abc123".ToByteArray();
        Span<byte> destination = stackalloc byte[16]; // Insufficient length

        // When
        bool result = algorithm.TryComputeHash(data, destination, 0, data.Length, 1, out int bytesWritten);

        // Then
        Assert.False(result);
        Assert.Equal(0, bytesWritten);
    }
}
