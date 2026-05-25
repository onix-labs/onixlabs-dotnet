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

/// <summary>
/// Provides helpers that import RFC 7468 PEM-encoded key material into a <see cref="AsymmetricAlgorithm"/>,
/// normalizing the malformed-input exception so the import surface fails uniformly.
/// </summary>
/// <remarks>
/// The BCL <see cref="AsymmetricAlgorithm.ImportFromPem(ReadOnlySpan{char})"/> family throws
/// <see cref="ArgumentException"/> when the input contains no PEM or a malformed label, whereas the rest
/// of the key import surface throws <see cref="CryptographicException"/>. These helpers translate the former
/// into the latter. A wrong decryption password already surfaces as <see cref="CryptographicException"/> and
/// is therefore left untouched.
/// </remarks>
internal static class PemKeyImporter
{
    private const string InvalidPemData = "The specified input does not contain a valid PEM-encoded key.";

    /// <summary>
    /// Imports the PEM-encoded key material in <paramref name="data"/> into <paramref name="algorithm"/>.
    /// </summary>
    /// <param name="algorithm">The algorithm into which the key material is imported.</param>
    /// <param name="data">The RFC 7468 PEM-encoded key material to import.</param>
    /// <exception cref="CryptographicException">Thrown when <paramref name="data"/> does not contain a valid PEM-encoded key.</exception>
    public static void Import(AsymmetricAlgorithm algorithm, ReadOnlySpan<char> data)
    {
        try
        {
            algorithm.ImportFromPem(data);
        }
        catch (ArgumentException exception)
        {
            throw new CryptographicException(InvalidPemData, exception);
        }
    }

    /// <summary>
    /// Imports the encrypted PEM-encoded key material in <paramref name="data"/> into <paramref name="algorithm"/>.
    /// </summary>
    /// <param name="algorithm">The algorithm into which the key material is imported.</param>
    /// <param name="data">The encrypted RFC 7468 PEM-encoded key material to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <exception cref="CryptographicException">Thrown when <paramref name="data"/> does not contain a valid PEM-encoded key.</exception>
    public static void Import(AsymmetricAlgorithm algorithm, ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        try
        {
            algorithm.ImportFromEncryptedPem(data, password);
        }
        catch (ArgumentException exception)
        {
            throw new CryptographicException(InvalidPemData, exception);
        }
    }

    /// <summary>
    /// Imports the encrypted PEM-encoded key material in <paramref name="data"/> into <paramref name="algorithm"/>.
    /// </summary>
    /// <param name="algorithm">The algorithm into which the key material is imported.</param>
    /// <param name="data">The encrypted RFC 7468 PEM-encoded key material to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <exception cref="CryptographicException">Thrown when <paramref name="data"/> does not contain a valid PEM-encoded key.</exception>
    public static void Import(AsymmetricAlgorithm algorithm, ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        try
        {
            algorithm.ImportFromEncryptedPem(data, password);
        }
        catch (ArgumentException exception)
        {
            throw new CryptographicException(InvalidPemData, exception);
        }
    }
}
