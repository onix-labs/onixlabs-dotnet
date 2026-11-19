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
        // Why: an interface with static abstract members cannot be a type argument, so the check is made on the key as an object.
        RequireNotNull<object>(publicKey);

        ReadOnlySpan<byte> publicKeyData = publicKey.AsReadOnlySpan();

        if (publicKeyData.Length != X25519.PublicKeySizeInBytes)
            throw new CryptographicException($"X25519 public key must be exactly {X25519.PublicKeySizeInBytes} bytes.");

        using X25519 algorithm = ImportKeyData();
        Span<byte> sharedSecret = stackalloc byte[X25519.SecretAgreementSizeInBytes];

        try
        {
            algorithm.DeriveRawSecretAgreement(publicKeyData, sharedSecret);
            return new Secret(sharedSecret);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(sharedSecret);
        }
    }
}
