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
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Provides extension methods for hash algorithms.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class HashAlgorithmExtensions
{
    private const string ComputeHashFailed = "Failed to compute a hash for the specified input data.";

    /// <summary>
    /// Computes the hash value for the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ReadOnlySpan<byte> data, int rounds = 1)
    {
        Span<byte> destination = stackalloc byte[algorithm.HashSize / 8];

        if (!algorithm.TryComputeHash(data, destination, 0, data.Length, rounds, out int _))
            throw new CryptographicException(ComputeHashFailed);

        return destination.ToArray();
    }

    /// <summary>
    /// Computes the hash value for the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="offset">The offset into the input from which to begin using data.</param>
    /// <param name="count">The number of bytes in the input to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ReadOnlySpan<byte> data, int offset, int count, int rounds = 1)
    {
        ReadOnlySpan<byte> source = data.Slice(offset, count);
        Span<byte> destination = stackalloc byte[algorithm.HashSize / 8];

        if (!algorithm.TryComputeHash(source, destination, 0, source.Length, rounds, out int _))
            throw new CryptographicException(ComputeHashFailed);

        return destination.ToArray();
    }

    /// <summary>
    /// Computes the hash value for the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to convert the specified <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ReadOnlySpan<char> data, Encoding? encoding = null, int rounds = 1) =>
        algorithm.ComputeHash(encoding.GetOrDefault().GetBytes(data.ToArray()), rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="IBinaryConvertible"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, IBinaryConvertible data, int rounds = 1) =>
        algorithm.ComputeHash(data.ToByteArray(), rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="IBinaryConvertible"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="offset">The offset into the input from which to begin using data.</param>
    /// <param name="count">The number of bytes in the input to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, IBinaryConvertible data, int offset, int count, int rounds = 1) =>
        algorithm.ComputeHash(data.ToByteArray(), offset, count, rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="ISpanBinaryConvertible"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ISpanBinaryConvertible data, int rounds = 1) =>
        algorithm.ComputeHash(data.ToReadOnlySpan(), rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="ISpanBinaryConvertible"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="data">The input data to compute the hash for.</param>
    /// <param name="offset">The offset into the input from which to begin using data.</param>
    /// <param name="count">The number of bytes in the input to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, ISpanBinaryConvertible data, int offset, int count, int rounds = 1) =>
        algorithm.ComputeHash(data.ToReadOnlySpan(), offset, count, rounds);

    /// <summary>
    /// Computes the hash value for the specified <see cref="Stream"/> value.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="stream">The input data to compute the hash for.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <returns>Returns the computed hash value.</returns>
    /// <exception cref="CryptographicException">If the hash could not be computed for the specified input data.</exception>
    public static byte[] ComputeHash(this HashAlgorithm algorithm, Stream stream, int rounds) => rounds is 1
        ? algorithm.ComputeHash(stream)
        : algorithm.ComputeHash(algorithm.ComputeHash(stream), rounds - 1);

    /// <summary>
    /// Attempts to compute the hash value for the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute a hash value.</param>
    /// <param name="source">The input to compute the hash code for.</param>
    /// <param name="destination">The buffer to receive the hash value.</param>
    /// <param name="offset">The offset into the input from which to begin using data.</param>
    /// <param name="count">The number of bytes in the input to use as data.</param>
    /// <param name="rounds">The number of rounds that the input data should be hashed.</param>
    /// <param name="bytesWritten">When this method returns, the total number of bytes written into <paramref name="destination" />. This parameter is treated as uninitialized.</param>
    /// <returns>Returns <see langword="true" /> if <paramref name="destination" /> is long enough to receive the hash value; otherwise, <see langword="false" />.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool TryComputeHash(this HashAlgorithm algorithm, ReadOnlySpan<byte> source, Span<byte> destination, int offset, int count, int rounds, out int bytesWritten)
    {
        try
        {
            if (rounds < 1 || destination.Length < algorithm.HashSize / 8)
            {
                bytesWritten = default;
                return false;
            }

            source = source.Slice(offset, count);
            int result = default;

            while (rounds-- > 0)
            {
                if (!algorithm.TryComputeHash(source, destination, out result))
                {
                    bytesWritten = default;
                    return false;
                }

                source = destination;
            }

            bytesWritten = result;
            return true;
        }
        catch
        {
            bytesWritten = default;
            return false;
        }
    }
}
