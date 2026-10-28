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
        byte[] seed = KeyData;
        try
        {
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

        result = new byte[Ed25519.PublicKeyLength];
        Ed25519.DerivePublicKey(seed, result);
        Volatile.Write(ref publicKey, result);

        return result;
    }
}
