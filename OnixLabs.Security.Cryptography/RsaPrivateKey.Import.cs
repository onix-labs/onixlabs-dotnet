// Copyright 2020-2022 ONIXLabs
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

public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(ReadOnlySpan<byte> data, HashAlgorithmType type, RSASignaturePadding padding)
    {
        RSA privateKey = RSA.Create();

        privateKey.ImportPkcs8PrivateKey(data, out int _);
        byte[] bytes = privateKey.ExportRSAPrivateKey();

        return FromByteArray(bytes, type, padding);
    }

    /// <summary>
    /// Imports a PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(byte[] data, HashAlgorithmType type, RSASignaturePadding padding)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        ReadOnlySpan<byte> data,
        ReadOnlySpan<char> password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        RSA privateKey = RSA.Create();

        privateKey.ImportEncryptedPkcs8PrivateKey(password, data, out int _);
        byte[] bytes = privateKey.ExportRSAPrivateKey();

        return FromByteArray(bytes, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        ReadOnlySpan<byte> data,
        char[] password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        ReadOnlySpan<char> characters = password.AsSpan();
        return ImportPkcs8Key(data, characters, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        ReadOnlySpan<byte> data,
        string password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        ReadOnlySpan<char> characters = password.AsSpan();
        return ImportPkcs8Key(data, characters, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        byte[] data,
        ReadOnlySpan<char> password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        byte[] data,
        char[] password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type, padding);
    }

    /// <summary>
    /// Imports an encrypted PKCS #8 formatted key.
    /// </summary>
    /// <param name="data">The key data to import.</param>
    /// <param name="password">The password to decrypt the key data.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified PKCS #8 key data.</returns>
    public static RsaPrivateKey ImportPkcs8Key(
        byte[] data,
        string password,
        HashAlgorithmType type,
        RSASignaturePadding padding)
    {
        ReadOnlySpan<byte> bytes = data.AsSpan();
        return ImportPkcs8Key(bytes, password, type, padding);
    }
}
