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
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Provides extension methods for hash algorithms.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class HashAlgorithmExtensions
{
    /// <summary>
    /// Computes the hash value for the specified byte array.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, byte[] data, int rounds) =>
        algorithm.ComputeHash(new MemoryStream(data), rounds);

    /// <summary>
    /// Computes the hash value for the specified byte array.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, byte[] data, int offset, int count, int rounds) =>
        algorithm.ComputeHash(data.Copy(offset, count), rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to convert the specified <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ReadOnlySpan<char> data, Encoding? encoding = null, int rounds = 1) =>
        algorithm.ComputeHash((encoding ?? Encoding.Default).GetBytes(data.ToArray()), rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="Stream"/> object.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="stream">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, Stream stream, int rounds)
    {
        Require(rounds > 0, "Rounds must be greater than zero", nameof(rounds));

        byte[] data = algorithm.ComputeHash(stream);
        while (--rounds > 0) data = algorithm.ComputeHash(data);
        return data;
    }

    /// <summary>
    /// Asynchronously computes the hash value for the specified <see cref="Stream"/> object.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="stream">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <param name="token">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a task that represents the asynchronous compute hash operation and wraps the computed hash value.</returns>
    public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, Stream stream, int rounds, CancellationToken token = default)
    {
        Require(rounds > 0, "Rounds must be greater than zero", nameof(rounds));

        MemoryStream memoryStream = new(await algorithm.ComputeHashAsync(stream, token));
        while (--rounds > 0) memoryStream = new MemoryStream(await algorithm.ComputeHashAsync(memoryStream, token));
        return memoryStream.ToArray();
    }
}
