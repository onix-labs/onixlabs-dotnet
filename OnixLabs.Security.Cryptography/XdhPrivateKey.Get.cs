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
public sealed partial class XdhPrivateKey
{
    /// <inheritdoc/>
    /// <remarks>
    /// Why: the key data is copied out on every call, as in <see cref="EddsaPrivateKey.GetPublicKey"/>, even when the public key has
    /// already been derived, so that a disposed key throws <see cref="ObjectDisposedException"/>.
    /// </remarks>
    public XdhPublicKey GetPublicKey()
    {
        if (Length != X25519.PrivateKeySizeInBytes)
            throw new CryptographicException($"X25519 private key must be exactly {X25519.PrivateKeySizeInBytes} bytes.");

        Span<byte> keyData = stackalloc byte[X25519.PrivateKeySizeInBytes];

        try
        {
            CopyTo(keyData);
            return new XdhPublicKey(GetOrDerivePublicKey(keyData));
        }
        finally
        {
            CryptographicOperations.ZeroMemory(keyData);
        }
    }

    /// <summary>
    /// Gets the public key of the current private key, deriving it from the specified private key data the first time it is needed.
    /// </summary>
    /// <param name="keyData">The 32-byte private key data of the current private key.</param>
    /// <returns>Returns the 32-byte public key of the current private key.</returns>
    /// <remarks>
    /// Concurrent first calls may each derive the key, which is harmless because they derive identical bytes; the array is fully written
    /// before it is published, so no caller can observe a partially written key.
    /// </remarks>
    private ReadOnlySpan<byte> GetOrDerivePublicKey(ReadOnlySpan<byte> keyData)
    {
        byte[]? result = Volatile.Read(ref publicKey);

        if (result is not null) return result;

        using X25519 algorithm = X25519.ImportPrivateKey(keyData);
        result = algorithm.ExportPublicKey();
        Volatile.Write(ref publicKey, result);

        return result;
    }
}
