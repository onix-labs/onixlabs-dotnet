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

namespace OnixLabs.Security.Cryptography;

public sealed partial class XdhPrivateKey
{
    /// <inheritdoc/>
    public Secret DeriveSharedSecret(IXdhPublicKey publicKey)
    {
        using Secret rawSecret = DeriveRawSharedSecret(publicKey);
        Span<byte> secretData = stackalloc byte[SHA256.HashSizeInBytes];

        try
        {
            SHA256.HashData(rawSecret.AsReadOnlySpan(), secretData);
            return new Secret(secretData);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(secretData);
        }
    }

    /// <inheritdoc/>
    public Secret DeriveRawSharedSecret(IXdhPublicKey publicKey)
    {
        ArgumentNullException.ThrowIfNull(publicKey);

        ReadOnlySpan<byte> publicKeyData = publicKey.AsReadOnlySpan();

        if (publicKeyData.Length != X25519.KeyLength)
            throw new CryptographicException($"X25519 public key must be exactly {X25519.KeyLength} bytes.");

        Span<byte> keyData = Length <= MaxStackKeyDataLength ? stackalloc byte[MaxStackKeyDataLength] : new byte[Length];
        keyData = keyData[..Length];
        Span<byte> sharedSecret = stackalloc byte[X25519.KeyLength];

        try
        {
            CopyTo(keyData);

            if (!X25519.TryDeriveSharedSecret(keyData, publicKeyData, sharedSecret))
                throw new CryptographicException("The X25519 shared secret is all zeros, so the public key is a point of small order.");

            return new Secret(sharedSecret);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(keyData);
            CryptographicOperations.ZeroMemory(sharedSecret);
        }
    }
}
