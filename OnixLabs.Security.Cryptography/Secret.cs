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
using System.Buffers;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic secret.
/// </summary>
/// <remarks>
/// This type implements <see cref="IDisposable"/> to securely zero key material from memory.
/// Because this is a struct, be aware that copies share the same underlying <see cref="ProtectedData"/> instance.
/// Disposing any copy will invalidate all copies. Avoid passing <see cref="Secret"/> by value after creation
/// if you intend to dispose it.
/// </remarks>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Secret : ICryptoPrimitive<Secret>, ISpanParsable<Secret>, IDisposable
{
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    private readonly ProtectedData protectedData = new();
    private readonly byte[] encryptedValue;
    private readonly Hash hash;
    private readonly int length;

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="Secret"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="value">
    /// The value from which to initialize a new <see cref="Secret"/> instance, which is zeroed once it has been encrypted.
    /// </param>
    private Secret(byte[] value)
    {
        try
        {
            Span<byte> digest = stackalloc byte[SHA256.HashSizeInBytes];
            SHA256.HashData(value, digest);

            encryptedValue = protectedData.Encrypt(value);
            hash = new Hash(digest);
            length = value.Length;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    /// <exception cref="CryptographicException">Thrown when the hash of the secret value could not be computed.</exception>
    public Secret(ReadOnlySpan<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    /// <exception cref="CryptographicException">Thrown when the hash of the secret value could not be computed.</exception>
    public Secret(ReadOnlyMemory<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The value from which to initialize a new <see cref="Secret"/> instance.</param>
    /// <exception cref="CryptographicException">Thrown when the hash of the secret value could not be computed.</exception>
    public Secret(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }

    /// <summary>
    /// Gets the length of the secret value, in bytes.
    /// </summary>
    /// <value>The length of the secret value, in bytes, or zero for a default <see cref="Secret"/>.</value>
    public int Length => length;

    /// <summary>
    /// Copies the decrypted secret value into the specified destination, without placing plaintext on the heap.
    /// </summary>
    /// <param name="destination">The buffer that receives the secret value, which must be at least <see cref="Length"/> bytes long.</param>
    /// <remarks>
    /// Only the first <see cref="Length"/> bytes of <paramref name="destination"/> are written. Unlike <see cref="AsReadOnlySpan"/> and
    /// <see cref="AsReadOnlyMemory"/>, which return a new array that stays on the heap until it is collected, the plaintext exists only in
    /// <paramref name="destination"/>. Zero it with <see cref="CryptographicOperations.ZeroMemory"/> when done.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="Secret"/> has been disposed.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than <see cref="Length"/>.</exception>
    public void CopyTo(Span<byte> destination)
    {
        if (destination.Length < Length)
            throw new ArgumentException($"The destination must be at least {Length} bytes long.", nameof(destination));

        // Why: decrypting into a destination longer than the plaintext also writes the padding block past it, so the slice is required.
        protectedData?.Decrypt(encryptedValue, destination[..Length]);
    }
}
