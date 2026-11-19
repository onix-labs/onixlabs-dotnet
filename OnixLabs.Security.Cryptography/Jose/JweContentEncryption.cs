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

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents JWE content encryption with AES-GCM (RFC 7518 §5.3) or AES-CBC-HMAC-SHA2 (RFC 7518 §5.2).
/// </summary>
internal static class JweContentEncryption
{
    /// <summary>
    /// Encrypts the specified plaintext.
    /// </summary>
    /// <param name="encryption">The JWE <c>enc</c> value.</param>
    /// <param name="key">The content encryption key, of the algorithm's key length.</param>
    /// <param name="iv">The initialization vector, of the algorithm's IV length.</param>
    /// <param name="plaintext">The plaintext.</param>
    /// <param name="associatedData">The additional authenticated data.</param>
    /// <returns>Returns the ciphertext and the authentication tag.</returns>
    public static (
        byte[] Ciphertext,
        byte[] Tag) Encrypt(string encryption,
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> iv,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> associatedData)
    {
        (bool isCbcHmac, _, _, int tagLength) = JweAlgorithm.GetContentEncryption(encryption);
        byte[] tag = new byte[tagLength];

        if (isCbcHmac)
        {
            using AesCbcHmacSha2 cipher = new(key);
            return (cipher.Encrypt(iv, plaintext, tag, associatedData), tag);
        }

        using AesGcm gcm = new(key, tagLength);
        byte[] ciphertext = new byte[plaintext.Length];
        gcm.Encrypt(iv, plaintext, ciphertext, tag, associatedData);
        return (ciphertext, tag);
    }

    /// <summary>
    /// Authenticates and decrypts the specified ciphertext.
    /// </summary>
    /// <param name="encryption">The JWE <c>enc</c> value.</param>
    /// <param name="key">The content encryption key.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <param name="ciphertext">The ciphertext.</param>
    /// <param name="tag">The authentication tag.</param>
    /// <param name="associatedData">The additional authenticated data.</param>
    /// <returns>Returns the plaintext.</returns>
    /// <exception cref="AuthenticationTagMismatchException">
    /// Thrown when the key, IV or tag has the wrong length for the algorithm, or the tag does not authenticate the ciphertext and
    /// associated data.
    /// </exception>
    public static byte[] Decrypt(
        string encryption,
        ReadOnlySpan<byte> key,
        ReadOnlySpan<byte> iv,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> tag,
        ReadOnlySpan<byte> associatedData)
    {
        (bool isCbcHmac, int keyLength, int ivLength, int tagLength) = JweAlgorithm.GetContentEncryption(encryption);

        // Why: a malformed JWE is reported the same way as a forged one, so that its failure reveals nothing more.
        if (key.Length != keyLength || iv.Length != ivLength || tag.Length != tagLength) throw new AuthenticationTagMismatchException();

        if (isCbcHmac)
        {
            using AesCbcHmacSha2 cipher = new(key);
            return cipher.Decrypt(iv, ciphertext, tag, associatedData);
        }

        using AesGcm gcm = new(key, tagLength);
        byte[] plaintext = new byte[ciphertext.Length];

        try
        {
            gcm.Decrypt(iv, ciphertext, tag, plaintext, associatedData);
            return plaintext;
        }
        catch
        {
            CryptographicOperations.ZeroMemory(plaintext);
            throw;
        }
    }
}
