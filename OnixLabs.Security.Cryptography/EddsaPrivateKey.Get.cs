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
using System.Threading;

namespace OnixLabs.Security.Cryptography;

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EddsaPrivateKey
{
    /// <inheritdoc/>
    public EddsaPublicKey GetPublicKey()
    {
        Span<byte> seed = stackalloc byte[Ed25519.PrivateKeySizeInBytes];
        RequireSeedLength();

        try
        {
            CopyTo(seed);
            return new EddsaPublicKey(GetOrDerivePublicKey(seed));
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <summary>
    /// Gets the encoded public key of the current private key, deriving it from the specified seed the first time it is needed.
    /// </summary>
    /// <param name="seed">The decrypted seed of the current private key.</param>
    /// <returns>Returns the 32-byte encoded public key of the current private key.</returns>
    /// <remarks>
    /// Concurrent first calls may each derive the key, which is harmless because they derive identical bytes; the array is fully written
    /// before it is published, so no caller can observe a partially written key.
    /// </remarks>
    private ReadOnlySpan<byte> GetOrDerivePublicKey(ReadOnlySpan<byte> seed)
    {
        byte[]? result = Volatile.Read(ref publicKey);

        if (result is not null) return result;

        using Ed25519 algorithm = Ed25519.ImportPrivateKey(seed);
        result = algorithm.ExportPublicKey();
        Volatile.Write(ref publicKey, result);

        return result;
    }

    /// <summary>
    /// Checks that the current private key is a 32-byte Ed25519 seed.
    /// </summary>
    /// <exception cref="CryptographicException">Thrown when the current private key is not 32 bytes.</exception>
    /// <remarks>
    /// Why: <see cref="Ed25519"/> reports a wrong-length key as <see cref="ArgumentException"/>, but the key-type API reports invalid keys
    /// as <see cref="CryptographicException"/>.
    /// </remarks>
    private void RequireSeedLength()
    {
        if (Length != Ed25519.PrivateKeySizeInBytes)
            throw new CryptographicException($"Ed25519 private key must be exactly {Ed25519.PrivateKeySizeInBytes} bytes.");
    }
}
