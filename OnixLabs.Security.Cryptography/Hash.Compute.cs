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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="T:System.Byte[]"/> data from which to compute a hash.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data)
    {
        byte[] value = algorithm.ComputeHash(data);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="T:System.Byte[]"/> data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int rounds)
    {
        byte[] value = algorithm.ComputeHash(data, rounds);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="T:System.Byte[]"/> data from which to compute a hash.</param>
    /// <param name="offset">The offset into the <see cref="T:System.Byte[]"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int offset, int count)
    {
        byte[] value = algorithm.ComputeHash(data, offset, count);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="T:System.Byte[]"/> data from which to compute a hash.</param>
    /// <param name="offset">The offset into the <see cref="T:System.Byte[]"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int offset, int count, int rounds)
    {
        byte[] value = algorithm.ComputeHash(data, offset, count, rounds);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash of the specified <see cref="Stream"/>, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, Stream stream)
    {
        byte[] value = algorithm.ComputeHash(stream);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash of the specified <see cref="Stream"/>, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, Stream stream, int rounds)
    {
        byte[] value = algorithm.ComputeHash(stream, rounds);
        return new Hash(value);
    }

    /// <summary>
    /// Computes the hash value for the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to convert the specified <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, ReadOnlySpan<char> data, Encoding? encoding = null, int rounds = 1)
    {
        byte[] value = algorithm.ComputeHash(data, encoding, rounds);
        return new Hash(value);
    }

    /// <summary>
    /// Asynchronously computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="token">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static async Task<Hash> ComputeAsync(HashAlgorithm algorithm, Stream stream, CancellationToken token = default)
    {
        byte[] value = await algorithm.ComputeHashAsync(stream, token);
        return new Hash(value);
    }

    /// <summary>
    /// Asynchronously computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <param name="token">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static async Task<Hash> ComputeAsync(HashAlgorithm algorithm, Stream stream, int rounds, CancellationToken token = default)
    {
        byte[] value = await algorithm.ComputeHashAsync(stream, rounds, token);
        return new Hash(value);
    }
}
