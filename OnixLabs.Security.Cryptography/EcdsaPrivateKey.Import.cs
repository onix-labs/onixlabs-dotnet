// Copyright 2020-2023 ONIXLabs
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

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> data, HashAlgorithmType type)
    {
        ECDsa privateKey = ECDsa.Create();
        privateKey.ImportPkcs8PrivateKey(data, out int _);
        byte[] bytes = privateKey.ExportECPrivateKey();
        return Create(bytes, type);
    }

    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(byte[] data, HashAlgorithmType type)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, HashAlgorithmType type)
    {
        ECDsa privateKey = ECDsa.Create();
        privateKey.ImportEncryptedPkcs8PrivateKey(password, data, out int _);
        byte[] bytes = privateKey.ExportECPrivateKey();
        return Create(bytes, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> data, char[] password, HashAlgorithmType type)
    {
        ReadOnlySpan<char> characters = password.AsSpan();
        return ImportPkcs8Key(data, characters, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> data, string password, HashAlgorithmType type)
    {
        ReadOnlySpan<char> characters = password.AsSpan();
        return ImportPkcs8Key(data, characters, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(byte[] data, ReadOnlySpan<char> password, HashAlgorithmType type)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(byte[] data, char[] password, HashAlgorithmType type)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an <see cref="EcdsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static EcdsaPrivateKey ImportPkcs8Key(byte[] data, string password, HashAlgorithmType type)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type);
    }
}
