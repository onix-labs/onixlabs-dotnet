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
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography;

public abstract partial class Sha3
{
    /// <summary>
    /// Computes the hash of the specified data into the specified destination, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="hashSizeInBytes">The hash size of the algorithm, in bytes.</param>
    /// <param name="source">The data to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="destination"/> is long enough; otherwise,
    /// <see langword="false"/>.</returns>
    /// <remarks>
    /// Why: these helpers give each SHA-3 hash class the static one-shot methods of .NET's <c>SHA3_256</c> and its siblings, implemented
    /// once.
    /// </remarks>
    private protected static bool TryHashData(
        Func<Sha3> create,
        int hashSizeInBytes,
        ReadOnlySpan<byte> source,
        Span<byte> destination,
        out int bytesWritten)
    {
        if (destination.Length < hashSizeInBytes)
        {
            bytesWritten = 0;
            return false;
        }

        using Sha3 algorithm = create();
        algorithm.HashCore(source);
        return algorithm.TryHashFinal(destination, out bytesWritten);
    }

    /// <summary>
    /// Computes the hash of the specified data, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="hashSizeInBytes">The hash size of the algorithm, in bytes.</param>
    /// <param name="source">The data to hash.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the hash.</returns>
    private protected static byte[] HashData(Func<Sha3> create, int hashSizeInBytes, ReadOnlySpan<byte> source)
    {
        byte[] result = new byte[hashSizeInBytes];
        TryHashData(create, hashSizeInBytes, source, result, out int _);
        return result;
    }

    /// <summary>
    /// Computes the hash of the specified data into the specified destination, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="hashSizeInBytes">The hash size of the algorithm, in bytes.</param>
    /// <param name="source">The data to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is too short.</exception>
    private protected static int HashData(Func<Sha3> create, int hashSizeInBytes, ReadOnlySpan<byte> source, Span<byte> destination) =>
        TryHashData(create, hashSizeInBytes, source, destination, out int bytesWritten)
            ? bytesWritten
            : throw new ArgumentException("Destination is too short.", nameof(destination));

    /// <summary>
    /// Computes the hash of the specified stream, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="source">The stream to hash.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the hash.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    private protected static byte[] HashData(Func<Sha3> create, Stream source)
    {
        RequireNotNull(source);

        using Sha3 algorithm = create();
        return algorithm.ComputeHash(source);
    }

    /// <summary>
    /// Computes the hash of the specified stream into the specified destination, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="hashSizeInBytes">The hash size of the algorithm, in bytes.</param>
    /// <param name="source">The stream to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is too short.</exception>
    private protected static int HashData(Func<Sha3> create, int hashSizeInBytes, Stream source, Span<byte> destination)
    {
        RequireNotNull(source);
        Require(destination.Length >= hashSizeInBytes, "Destination is too short.", nameof(destination));

        byte[] hash = HashData(create, source);
        hash.CopyTo(destination);
        return hash.Length;
    }

    /// <summary>
    /// Asynchronously computes the hash of the specified stream, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="source">The stream to hash.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a task whose result is a new <see cref="byte"/> array containing the hash.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    private protected static async ValueTask<byte[]> HashDataAsync(Func<Sha3> create, Stream source, CancellationToken cancellationToken)
    {
        RequireNotNull(source);

        using Sha3 algorithm = create();
        return await algorithm.ComputeHashAsync(source, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously computes the hash of the specified stream into the specified destination, with a new hash algorithm instance.
    /// </summary>
    /// <param name="create">Creates the hash algorithm.</param>
    /// <param name="hashSizeInBytes">The hash size of the algorithm, in bytes.</param>
    /// <param name="source">The stream to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a task whose result is the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is too short.</exception>
    private protected static async ValueTask<int> HashDataAsync(
        Func<Sha3> create,
        int hashSizeInBytes,
        Stream source,
        Memory<byte> destination,
        CancellationToken cancellationToken)
    {
        RequireNotNull(source);
        Require(destination.Length >= hashSizeInBytes, "Destination is too short.", nameof(destination));

        byte[] hash = await HashDataAsync(create, source, cancellationToken).ConfigureAwait(false);
        hash.CopyTo(destination);
        return hash.Length;
    }
}
