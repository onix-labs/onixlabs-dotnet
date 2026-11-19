// Copyright © 2020 ONIXLabs
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
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the FIPS 202 SHA-3 384-bit hash algorithm.
/// </summary>
public sealed class Sha3Hash384 : Sha3
{
    /// <summary>
    /// The rate in bytes of the sponge state.
    /// </summary>
    private const int RateBytes = 104;

    /// <summary>
    /// The length of the hash in bits.
    /// </summary>
    private const int BitLength = 384;

    /// <summary>
    /// A value indicating whether the platform's <see cref="SHA3_384"/> is supported, so that it is used rather than the managed code.
    /// </summary>
    private static readonly bool IsPlatformSupported = SHA3_384.IsSupported;

    /// <summary>
    /// Initializes a new instance of the <see cref="Sha3Hash384"/> class.
    /// </summary>
    /// <remarks>
    /// Where the platform's <see cref="SHA3_384"/> is supported, it computes the hash, because it is faster; elsewhere, as on macOS, the
    /// managed code does. Both implement FIPS 202, so the results are identical.
    /// </remarks>
    public Sha3Hash384() : this(IsPlatformSupported)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sha3Hash384"/> class, with the specified implementation.
    /// </summary>
    /// <param name="usePlatform">Whether to use the platform's implementation, where it is supported.</param>
    /// <remarks>Why: the tests run every case through both implementations.</remarks>
    internal Sha3Hash384(bool usePlatform)
        : base(RateBytes, HashDelimiter, BitLength, HashAlgorithmName.SHA3_384, usePlatform && IsPlatformSupported)
    {
    }

    /// <summary>
    /// The hash size of SHA3-384, in bits.
    /// </summary>
    public const int HashSizeInBits = 384;

    /// <summary>
    /// The hash size of SHA3-384, in bytes.
    /// </summary>
    public const int HashSizeInBytes = 48;

    /// <summary>
    /// Gets a value indicating whether SHA3-384 is supported on the current platform.
    /// </summary>
    /// <value>Always <see langword="true"/>, because the managed code is used wherever the platform's implementation is not.</value>
    public static bool IsSupported => true;

    /// <summary>
    /// Computes the SHA3-384 hash of the specified data.
    /// </summary>
    /// <param name="source">The data to hash.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the hash.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    public static byte[] HashData(byte[] source) => HashData(new ReadOnlySpan<byte>(RequireNotNull(source)));

    /// <summary>
    /// Computes the SHA3-384 hash of the specified data.
    /// </summary>
    /// <param name="source">The data to hash.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the hash.</returns>
    public static byte[] HashData(ReadOnlySpan<byte> source) => HashData(CreateInstance, HashSizeInBytes, source);

    /// <summary>
    /// Computes the SHA3-384 hash of the specified data into the specified destination.
    /// </summary>
    /// <param name="source">The data to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than
    /// <see cref="HashSizeInBytes"/>.</exception>
    public static int HashData(ReadOnlySpan<byte> source, Span<byte> destination) =>
        HashData(CreateInstance, HashSizeInBytes, source, destination);

    /// <summary>
    /// Attempts to compute the SHA3-384 hash of the specified data into the specified destination.
    /// </summary>
    /// <param name="source">The data to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination"/>.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="destination"/> is long enough; otherwise,
    /// <see langword="false"/>.</returns>
    public static bool TryHashData(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten) =>
        TryHashData(CreateInstance, HashSizeInBytes, source, destination, out bytesWritten);

    /// <summary>
    /// Computes the SHA3-384 hash of the specified stream.
    /// </summary>
    /// <param name="source">The stream to hash.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the hash.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    public static byte[] HashData(Stream source) => HashData(CreateInstance, source);

    /// <summary>
    /// Computes the SHA3-384 hash of the specified stream into the specified destination.
    /// </summary>
    /// <param name="source">The stream to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than
    /// <see cref="HashSizeInBytes"/>.</exception>
    public static int HashData(Stream source, Span<byte> destination) => HashData(CreateInstance, HashSizeInBytes, source, destination);

    /// <summary>
    /// Asynchronously computes the SHA3-384 hash of the specified stream.
    /// </summary>
    /// <param name="source">The stream to hash.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a task whose result is a new <see cref="byte"/> array containing the hash.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    public static ValueTask<byte[]> HashDataAsync(Stream source, CancellationToken cancellationToken = default) =>
        HashDataAsync(CreateInstance, source, cancellationToken);

    /// <summary>
    /// Asynchronously computes the SHA3-384 hash of the specified stream into the specified destination.
    /// </summary>
    /// <param name="source">The stream to hash.</param>
    /// <param name="destination">The buffer that receives the hash.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Returns a task whose result is the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than
    /// <see cref="HashSizeInBytes"/>.</exception>
    public static ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default) =>
        HashDataAsync(CreateInstance, HashSizeInBytes, source, destination, cancellationToken);

    /// <summary>
    /// Creates a new <see cref="Sha3Hash384"/> instance, for the static methods.
    /// </summary>
    /// <returns>Returns a new <see cref="Sha3Hash384"/> instance.</returns>
    private static Sha3Hash384 CreateInstance() => new();
}
