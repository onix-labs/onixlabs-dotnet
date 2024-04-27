// Copyright 2020-2024 ONIXLabs
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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="value">The key data to import.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> value) =>
        ImportPkcs8Key(value, out int _);

    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="value">The key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the source value.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> value, out int bytesRead)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportPkcs8PrivateKey(value, out bytesRead);
        return new EcdsaPrivateKey(algorithm.ExportECPrivateKey());
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="value">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> value, ReadOnlySpan<char> password) =>
        ImportPkcs8Key(value, password, out int _);

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="value">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="bytesRead">The number of bytes read from the source value.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> value, ReadOnlySpan<char> password, out int bytesRead)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportEncryptedPkcs8PrivateKey(password, value, out bytesRead);
        return new EcdsaPrivateKey(algorithm.ExportECPrivateKey());
    }

    /// <summary>
    /// Creates an <see cref="ECDsa"/> elliptic curve algorithm and imports the cryptographic private key data.
    /// </summary>
    /// <returns>Returns an <see cref="ECDsa"/> elliptic curve algorithm with the imported cryptographic private key data.</returns>
    private ECDsa ImportPrivateKey() => ECDsa.Create().Apply(algorithm => algorithm.ImportECPrivateKey(Value, out int _));
}
