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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines a cryptographic private key that can be imported from RFC 7468 PEM-encoded data.
/// </summary>
/// <typeparam name="T">The underlying type of <see cref="PrivateKey"/> that the import functions will return.</typeparam>
public interface IPrivateKeyPemImportable<out T> where T : PrivateKey
{
    /// <summary>
    /// Imports the cryptographic private key data from RFC 7468 PEM-encoded text. The PEM label
    /// determines the format that is decoded (for example, PKCS #8 <c>PRIVATE KEY</c>,
    /// SEC1 <c>EC PRIVATE KEY</c>, or PKCS #1 <c>RSA PRIVATE KEY</c>).
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data);

    /// <summary>
    /// Imports the cryptographic private key data from encrypted RFC 7468 PEM-encoded text
    /// (<c>ENCRYPTED PRIVATE KEY</c>).
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password);

    /// <summary>
    /// Imports the cryptographic private key data from encrypted RFC 7468 PEM-encoded text
    /// (<c>ENCRYPTED PRIVATE KEY</c>).
    /// </summary>
    /// <param name="data">The cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new cryptographic private key from the imported data.</returns>
    public static abstract T ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password);
}
