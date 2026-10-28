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
/// Represents a cryptographic private key.
/// </summary>
public abstract partial class PrivateKey : ICryptoPrimitive<PrivateKey>, IDisposable
{
    /// <summary>
    /// The largest key length, in bytes, that internal operations decrypt into a stack buffer rather than a heap buffer.
    /// </summary>
    /// <remarks>
    /// Why: 256 bytes covers every elliptic-curve and Ed25519 key, so those never put plaintext on the heap. RSA private keys are larger
    /// and use a heap buffer, which is zeroed after use.
    /// </remarks>
    private protected const int MaxStackKeyDataLength = 256;

    // ReSharper disable once HeapView.ObjectAllocation.Evident
    private readonly ProtectedData protectedData = new();
    private readonly byte[] encryptedKeyData;
    private readonly Hash digest;
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is intentionally marked <see langword="private"/> and is used exclusively to initialize the underlying <see cref="byte"/> array.
    /// Because <see cref="PrivateKey"/> is designed to be immutable, external array references are not permitted.
    /// The overloaded constructors below ensure immutability by creating defensive copies of the provided arrays.
    /// </remarks>
    /// <param name="keyData">The underlying key data of the cryptographic private key, which is zeroed once it has been encrypted.</param>
    private PrivateKey(byte[] keyData)
    {
        try
        {
            Span<byte> keyDigest = stackalloc byte[SHA256.HashSizeInBytes];
            SHA256.HashData(keyData, keyDigest);

            Length = keyData.Length;
            digest = new Hash(keyDigest);
            encryptedKeyData = protectedData.Encrypt(keyData);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(keyData);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlySpan<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlyMemory<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the cryptographic private key.</param>
    protected PrivateKey(ReadOnlySequence<byte> keyData) : this(keyData.ToArray())
    {
    }

    /// <summary>
    /// Gets the length of the cryptographic private key data, in bytes.
    /// </summary>
    /// <value>The length of the cryptographic private key data, in bytes.</value>
    public int Length { get; }

    /// <summary>
    /// Gets the cryptographic private key data.
    /// </summary>
    /// <value>The decrypted byte array containing the cryptographic private key data.</value>
    /// <remarks>
    /// Each read decrypts into a new array that the caller should zero with <see cref="CryptographicOperations.ZeroMemory"/> when done.
    /// Prefer <see cref="CopyTo"/>, which decrypts into a buffer the caller controls.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="PrivateKey"/> has been disposed.</exception>
    protected byte[] KeyData => protectedData.Decrypt(encryptedKeyData);

    /// <summary>
    /// Copies the decrypted cryptographic private key data into the specified destination, without placing plaintext on the heap.
    /// </summary>
    /// <param name="destination">The buffer that receives the key data, which must be at least <see cref="Length"/> bytes long.</param>
    /// <remarks>
    /// Only the first <see cref="Length"/> bytes of <paramref name="destination"/> are written. Unlike <see cref="AsReadOnlySpan"/> and
    /// <see cref="AsReadOnlyMemory"/>, which return a new array that stays on the heap until it is collected, the plaintext exists only in
    /// <paramref name="destination"/>. Zero it with <see cref="CryptographicOperations.ZeroMemory"/> when done.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="PrivateKey"/> has been disposed.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than <see cref="Length"/>.</exception>
    public void CopyTo(Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        if (destination.Length < Length)
            throw new ArgumentException($"The destination must be at least {Length} bytes long.", nameof(destination));

        // Why: decrypting into a destination longer than the plaintext also writes the padding block past it, so the slice is required.
        protectedData.Decrypt(encryptedKeyData, destination[..Length]);
    }
}
