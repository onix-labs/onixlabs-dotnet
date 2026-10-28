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
using System.Security.Cryptography;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an in-memory data protection mechanism for sensitive, long-lived cryptographic data.
/// </summary>
/// <remarks>
/// Each instance owns a random AES-256 key and IV, and is expected to encrypt exactly one value, which it may then decrypt any number of times.
/// Instances are safe for concurrent use.
/// </remarks>
// ReSharper disable HeapView.ObjectAllocation.Evident
internal sealed partial class ProtectedData : IDisposable
{
    private const int KeyLength = 32;
    private const int IvLength = 16;

    private readonly byte[] key = RandomNumberGenerator.GetBytes(KeyLength);

    // Why: a fixed IV for the lifetime of the key is acceptable only because each instance encrypts a single value.
    // Encrypting a second value under the same key and IV would leak equality of their leading blocks; if that is ever
    // needed, generate a fresh IV per call and store it alongside the ciphertext instead.
    private readonly byte[] iv = RandomNumberGenerator.GetBytes(IvLength);

    private bool isDisposed;

    /// <summary>
    /// Encrypts the specified data.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <returns>Returns the encrypted data.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] Encrypt(byte[] data)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        if (data.IsEmpty()) return data;

        using Aes algorithm = CreateAlgorithm();
        return algorithm.EncryptCbc(data, iv, PaddingMode.PKCS7);
    }

    /// <summary>
    /// Decrypts the specified data.
    /// </summary>
    /// <param name="data">The data to decrypt.</param>
    /// <returns>Returns the decrypted data.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] Decrypt(byte[] data)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        if (data.IsEmpty()) return data;

        using Aes algorithm = CreateAlgorithm();
        return algorithm.DecryptCbc(data, iv, PaddingMode.PKCS7);
    }

    /// <summary>
    /// Decrypts the specified data into the specified destination, without allocating plaintext on the heap.
    /// </summary>
    /// <param name="data">The data to decrypt.</param>
    /// <param name="destination">
    /// The buffer that receives the decrypted data, which must be at least as long as the decrypted data.
    /// </param>
    /// <returns>Returns the number of bytes written to <paramref name="destination"/>.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is shorter than the decrypted data.</exception>
    public int Decrypt(byte[] data, Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        if (data.IsEmpty()) return 0;

        using Aes algorithm = CreateAlgorithm();

        if (!algorithm.TryDecryptCbc(data, iv, destination, out int bytesWritten, PaddingMode.PKCS7))
            throw new ArgumentException("The destination is shorter than the decrypted data.", nameof(destination));

        return bytesWritten;
    }

    /// <summary>
    /// Creates a new <see cref="Aes"/> instance keyed with the current instance's key.
    /// </summary>
    /// <returns>Returns a new <see cref="Aes"/> instance keyed with the current instance's key.</returns>
    /// <remarks>
    /// Why: an <see cref="Aes"/> instance per call, rather than one per <see cref="ProtectedData"/>, for two measured reasons (GH #156).
    /// First, a keyed <see cref="Aes"/> owns a native key handle, so holding one would leave a finalizer behind for every
    /// <see cref="PrivateKey"/> or <see cref="Secret"/> that is not disposed; scoping it to the call releases it deterministically.
    /// Second, from .NET 11 a shared <see cref="Aes"/> throws <see cref="CryptographicException"/> on concurrent use instead of waiting.
    /// The one-shot <see cref="SymmetricAlgorithm.EncryptCbc(ReadOnlySpan{byte}, ReadOnlySpan{byte}, PaddingMode)"/> and
    /// <see cref="SymmetricAlgorithm.DecryptCbc(ReadOnlySpan{byte}, ReadOnlySpan{byte}, PaddingMode)"/> methods create no
    /// <see cref="ICryptoTransform"/>, so nothing is left for the finalizer; a CryptoStream-based pipeline must dispose its transform explicitly.
    /// </remarks>
    private Aes CreateAlgorithm()
    {
        Aes result = Aes.Create();
        result.Key = key;
        return result;
    }
}
