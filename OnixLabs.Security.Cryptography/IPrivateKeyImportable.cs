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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines a cryptographic private key that can be imported.
/// </summary>
/// <typeparam name="T">The underlying type of <see cref="PrivateKey"/> that the import functions will return.</typeparam>
public interface IPrivateKeyImportable<out T> where T : PrivateKey
{
    /// <summary>
    /// Imports the cryptographic private key data.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T Import(ReadOnlySpan<byte> data);

    /// <summary>
    /// Imports the cryptographic private key data.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T Import(ReadOnlySpan<byte> data, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T Import(IBinaryConvertible data);

    /// <summary>
    /// Imports the cryptographic private key data.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T Import(IBinaryConvertible data, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data);

    /// <summary>
    /// Imports the cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data);

    /// <summary>
    /// Imports the cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password, out int bytesRead);

    /// <summary>
    /// Imports the cryptographic private key data in PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password);

    /// <summary>
    /// Imports the cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password);
}
