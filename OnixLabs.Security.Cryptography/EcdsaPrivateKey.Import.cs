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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Imports the ECDSA cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(ReadOnlySpan<byte> data) =>
        ImportPkcs8PrivateKey(data, out int _);

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(ReadOnlySpan<byte> data, out int bytesRead)
    {
        using ECDsa key = ECDsa.Create();
        key.ImportPkcs8PrivateKey(data, out bytesRead);
        byte[] keyData = key.ExportECPrivateKey();
        return new EcdsaPrivateKey(keyData);
    }

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(IBinaryConvertible data) =>
        ImportPkcs8PrivateKey(data, out int _);

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(IBinaryConvertible data, out int bytesRead) =>
        ImportPkcs8PrivateKey(data.ToByteArray(), out bytesRead);

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(ReadOnlySpan<byte> data, ReadOnlySpan<char> password) =>
        ImportPkcs8PrivateKey(data, password, out int _);

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead)
    {
        using ECDsa key = ECDsa.Create();
        key.ImportEncryptedPkcs8PrivateKey(password, data, out bytesRead);
        byte[] keyData = key.ExportECPrivateKey();
        return new EcdsaPrivateKey(keyData);
    }

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(IBinaryConvertible data, ReadOnlySpan<char> password) =>
        ImportPkcs8PrivateKey(data, password, out int _);

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new <see cref="EcdsaPrivateKey"/> instance from the imported cryptographic private key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8PrivateKey(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead) =>
        ImportPkcs8PrivateKey(data.ToByteArray(), password, out bytesRead);

    /// <summary>
    /// Imports the key data into a new <see cref="ECDsa"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="ECDsa"/> instance containing the imported key data.</returns>
    private ECDsa ImportKeyData()
    {
        ECDsa algorithm = ECDsa.Create();
        algorithm.ImportECPrivateKey(KeyData, out int _);
        return algorithm;
    }
}
