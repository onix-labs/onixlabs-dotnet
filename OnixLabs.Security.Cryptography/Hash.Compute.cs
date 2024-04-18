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

using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="byte"/> array data from which to compute a hash.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data) => algorithm
        .ComputeHash(data).Let(hash => new Hash(hash));

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="byte"/> array data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int rounds) => algorithm
        .ComputeHash(data, rounds).Let(hash => new Hash(hash));

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="byte"/> array data from which to compute a hash.</param>
    /// <param name="offset">The offset into the <see cref="byte"/> array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int offset, int count) => algorithm
        .ComputeHash(data, offset, count).Let(hash => new Hash(hash));

    /// <summary>
    /// Computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="data">The <see cref="byte"/> array data from which to compute a hash.</param>
    /// <param name="offset">The offset into the <see cref="byte"/> array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, byte[] data, int offset, int count, int rounds) => algorithm
        .ComputeHash(data, offset, count, rounds).Let(hash => new Hash(hash));

    /// <summary>
    /// Computes the hash of the specified <see cref="Stream"/>, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, Stream stream) => algorithm
        .ComputeHash(stream).Let(hash => new Hash(hash));

    /// <summary>
    /// Computes the hash of the specified <see cref="Stream"/>, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static Hash Compute(HashAlgorithm algorithm, Stream stream, int rounds) => algorithm
        .ComputeHash(stream, rounds).Let(hash => new Hash(hash));

    /// <summary>
    /// Asynchronously computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="token">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static async Task<Hash> ComputeAsync(HashAlgorithm algorithm, Stream stream, CancellationToken token = default) => await algorithm
        .ComputeHashAsync(stream, token).LetAsync(hash => new Hash(hash));

    /// <summary>
    /// Asynchronously computes the hash of the specified data, using the specified <see cref="HashAlgorithm"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="stream">The <see cref="Stream"/> data from which to compute a hash.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <param name="token">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a cryptographic hash of the specified data.</returns>
    public static async Task<Hash> ComputeAsync(HashAlgorithm algorithm, Stream stream, int rounds, CancellationToken token = default) => await algorithm
        .ComputeHashAsync(stream, rounds, token).LetAsync(hash => new Hash(hash));
}
