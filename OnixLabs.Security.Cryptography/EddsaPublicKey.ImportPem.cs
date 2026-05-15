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

public sealed partial class EddsaPublicKey
{
    /// <inheritdoc/>
    public static EddsaPublicKey ImportPem(ReadOnlySpan<char> data)
    {
        PemFields fields = PemEncoding.Find(data);
        string label = data[fields.Label].ToString();

        if (label != PublicKeyLabel)
            throw new CryptographicException($"Unsupported PEM label for Ed25519 public key: '{label}'.");

        byte[] der = new byte[fields.DecodedDataLength];

        if (!Convert.TryFromBase64Chars(data[fields.Base64Data], der, out int written) || written != der.Length)
            throw new CryptographicException("Invalid Base64 content in PEM.");

        byte[] publicKey = Ed25519Pkcs8.DecodePublicKey(der, out _);
        return new EddsaPublicKey(publicKey);
    }

    /// <inheritdoc/>
    public static EddsaPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password) => ImportPem(data);

    /// <inheritdoc/>
    public static EddsaPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password) => ImportPem(data);
}
