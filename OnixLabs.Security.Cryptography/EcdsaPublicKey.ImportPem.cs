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
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPublicKey
{
    /// <inheritdoc/>
    public static EcdsaPublicKey ImportPem(ReadOnlySpan<char> data)
    {
        if (TryImportSecp256k1Pem(data, out EcdsaPublicKey? secp256k1Key)) return secp256k1Key;

        using ECDsa algorithm = ECDsa.Create();
        PemKeyImporter.Import(algorithm, data);
        return new EcdsaPublicKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdsaPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        if (TryImportSecp256k1Pem(data, out EcdsaPublicKey? secp256k1Key)) return secp256k1Key;

        using ECDsa algorithm = ECDsa.Create();
        PemKeyImporter.Import(algorithm, data, password);
        return new EcdsaPublicKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdsaPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        if (TryImportSecp256k1Pem(data, out EcdsaPublicKey? secp256k1Key)) return secp256k1Key;

        using ECDsa algorithm = ECDsa.Create();
        PemKeyImporter.Import(algorithm, data, password);
        return new EcdsaPublicKey(algorithm);
    }

    /// <summary>
    /// Attempts to import the specified PEM-encoded data as a secp256k1 public key, which is decoded by managed code on every platform.
    /// </summary>
    /// <param name="data">The PEM-encoded data.</param>
    /// <param name="key">When this method returns, contains the imported key, if the data is a secp256k1 <c>PUBLIC KEY</c>.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the data is a <c>PUBLIC KEY</c> whose SubjectPublicKeyInfo names secp256k1; otherwise,
    /// <see langword="false"/>, so that the platform imports it.
    /// </returns>
    /// <exception cref="CryptographicException">Thrown when the data names secp256k1, but is not a valid point on the curve.</exception>
    private static bool TryImportSecp256k1Pem(ReadOnlySpan<char> data, [NotNullWhen(true)] out EcdsaPublicKey? key)
    {
        key = null;

        if (!PemEncoding.TryFind(data, out PemFields fields) || data[fields.Label] is not "PUBLIC KEY") return false;

        byte[] der = new byte[fields.DecodedDataLength];

        if (!Convert.TryFromBase64Chars(data[fields.Base64Data], der, out int written) || written != der.Length) return false;
        if (!Secp256k1.IsSecp256k1SubjectPublicKeyInfo(der)) return false;

        key = Import(der);
        return true;
    }
}
