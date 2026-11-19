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

namespace OnixLabs.Security.Cryptography.UnitTests;

/// <summary>
/// Tests the static one-shot methods that the SHA-3 hash classes share with .NET's <c>SHA3_256</c> and its siblings, against each class's
/// own <see cref="HashAlgorithm.ComputeHash(byte[])"/>, whose known answers <see cref="Sha3Tests"/> verifies, and the agreement of the
/// managed code with the platform's implementation where the platform has one.
/// </summary>
public sealed class Sha3HashDataTests
{
    [Theory(DisplayName = "Sha3 static HashData methods should match ComputeHash")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(135)]
    [InlineData(136)]
    [InlineData(1000)]
    public async Task Sha3StaticHashDataShouldMatchComputeHash(int length)
    {
        // Given
        byte[] data = new byte[length];
        new Random(length).NextBytes(data);

        // When / Then
        await AssertStaticMethodsMatch(new Sha3Hash224(), data, Sha3Hash224.HashSizeInBytes, Sha3Hash224.HashData, Sha3Hash224.HashData,
            Sha3Hash224.TryHashData, Sha3Hash224.HashData, Sha3Hash224.HashDataAsync);
        await AssertStaticMethodsMatch(new Sha3Hash256(), data, Sha3Hash256.HashSizeInBytes, Sha3Hash256.HashData, Sha3Hash256.HashData,
            Sha3Hash256.TryHashData, Sha3Hash256.HashData, Sha3Hash256.HashDataAsync);
        await AssertStaticMethodsMatch(new Sha3Hash384(), data, Sha3Hash384.HashSizeInBytes, Sha3Hash384.HashData, Sha3Hash384.HashData,
            Sha3Hash384.TryHashData, Sha3Hash384.HashData, Sha3Hash384.HashDataAsync);
        await AssertStaticMethodsMatch(new Sha3Hash512(), data, Sha3Hash512.HashSizeInBytes, Sha3Hash512.HashData, Sha3Hash512.HashData,
            Sha3Hash512.TryHashData, Sha3Hash512.HashData, Sha3Hash512.HashDataAsync);
    }

    [Theory(DisplayName = "Sha3 should give the same hash with the managed code and the platform's implementation")]
    [InlineData(0)]
    [InlineData(136)]
    [InlineData(4096)]
    public void Sha3ShouldAgreeAcrossImplementations(int length)
    {
        // Given
        byte[] data = new byte[length];
        new Random(length).NextBytes(data);

        // When / Then
        Assert.Equal(Hash(new Sha3Hash256(usePlatform: false), data), Hash(new Sha3Hash256(usePlatform: true), data));
        Assert.Equal(Hash(new Sha3Hash384(usePlatform: false), data), Hash(new Sha3Hash384(usePlatform: true), data));
        Assert.Equal(Hash(new Sha3Hash512(usePlatform: false), data), Hash(new Sha3Hash512(usePlatform: true), data));

        if (SHA3_256.IsSupported) Assert.Equal(SHA3_256.HashData(data), Hash(new Sha3Hash256(usePlatform: false), data));
        if (SHA3_384.IsSupported) Assert.Equal(SHA3_384.HashData(data), Hash(new Sha3Hash384(usePlatform: false), data));
        if (SHA3_512.IsSupported) Assert.Equal(SHA3_512.HashData(data), Hash(new Sha3Hash512(usePlatform: false), data));
    }

    [Fact(DisplayName = "Sha3 should restart cleanly when Initialize is called part-way through a hash, with either implementation")]
    public void Sha3ShouldRestartAfterInitialize()
    {
        foreach (bool usePlatform in (bool[])[false, true])
        {
            // Given
            using Sha3Hash256 algorithm = new(usePlatform);
            byte[] expected = Hash(new Sha3Hash256(usePlatform), [4, 5, 6]);

            // When
            algorithm.TransformBlock([1, 2, 3], 0, 3, null, 0);
            algorithm.Initialize();
            byte[] actual = algorithm.ComputeHash([4, 5, 6]);

            // Then
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Sha3 static HashData should report a short destination as .NET's SHA-3 classes do")]
    public void Sha3StaticHashDataShouldReportShortDestination()
    {
        // Given
        byte[] destination = new byte[Sha3Hash256.HashSizeInBytes - 1];

        // When
        bool result = Sha3Hash256.TryHashData([1, 2, 3], destination, out int bytesWritten);

        // Then
        Assert.True(Sha3Hash256.IsSupported);
        Assert.False(result);
        Assert.Equal(0, bytesWritten);
        Assert.Throws<ArgumentException>(() => Sha3Hash256.HashData([1, 2, 3], destination));
        Assert.Throws<ArgumentNullException>(() => Sha3Hash256.HashData((byte[])null!));
    }

    /// <summary>
    /// Computes the hash of the specified data with the specified algorithm, then disposes it.
    /// </summary>
    /// <param name="algorithm">The algorithm.</param>
    /// <param name="data">The data to hash.</param>
    /// <returns>Returns the hash.</returns>
    private static byte[] Hash(HashAlgorithm algorithm, byte[] data)
    {
        using (algorithm) return algorithm.ComputeHash(data);
    }

    /// <summary>
    /// Asserts that every static one-shot method of a SHA-3 hash class gives the same hash as the class's own ComputeHash.
    /// </summary>
    private static async Task AssertStaticMethodsMatch(
        HashAlgorithm algorithm,
        byte[] data,
        int hashSizeInBytes,
        Func<byte[], byte[]> hashArray,
        HashSpanToSpan hashSpan,
        TryHashSpan tryHash,
        Func<Stream, byte[]> hashStream,
        Func<Stream, System.Threading.CancellationToken, ValueTask<byte[]>> hashStreamAsync)
    {
        byte[] expected = Hash(algorithm, data);
        byte[] destination = new byte[hashSizeInBytes];
        byte[] tryDestination = new byte[hashSizeInBytes + 8];

        Assert.Equal(hashSizeInBytes, expected.Length);
        Assert.Equal(expected, hashArray(data));
        Assert.Equal(hashSizeInBytes, hashSpan(data, destination));
        Assert.Equal(expected, destination);
        Assert.True(tryHash(data, tryDestination, out int bytesWritten));
        Assert.Equal(expected, tryDestination[..bytesWritten]);
        Assert.Equal(expected, hashStream(new MemoryStream(data)));
        Assert.Equal(expected, await hashStreamAsync(new MemoryStream(data), default));
    }

    /// <summary>
    /// Computes a hash from a span into a span.
    /// </summary>
    private delegate int HashSpanToSpan(ReadOnlySpan<byte> source, Span<byte> destination);

    /// <summary>
    /// Attempts to compute a hash from a span into a span.
    /// </summary>
    private delegate bool TryHashSpan(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten);
}
