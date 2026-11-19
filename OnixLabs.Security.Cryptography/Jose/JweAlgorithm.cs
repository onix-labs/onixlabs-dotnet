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
/// Represents the JWE key management (<c>alg</c>) and content encryption (<c>enc</c>) algorithms supported.
/// </summary>
/// <remarks>
/// DIDComm Messaging v2.1 uses <see cref="EcdhEsA256Kw"/> and <see cref="Ecdh1PuA256Kw"/> with <see cref="A256CbcHs512"/> or
/// <see cref="A256Gcm"/>. The 128-bit and 192-bit variants are also supported, because the published JOSE examples use them. Direct key
/// agreement (<c>ECDH-ES</c> and <c>ECDH-1PU</c> without key wrapping) and <c>XC20P</c> are not supported.
/// </remarks>
public static class JweAlgorithm
{
    /// <summary>
    /// ECDH-ES with Concat KDF, wrapping the CEK with A128KW (RFC 7518 §4.6).
    /// </summary>
    public const string EcdhEsA128Kw = "ECDH-ES+A128KW";

    /// <summary>
    /// ECDH-ES with Concat KDF, wrapping the CEK with A192KW (RFC 7518 §4.6).
    /// </summary>
    public const string EcdhEsA192Kw = "ECDH-ES+A192KW";

    /// <summary>
    /// ECDH-ES with Concat KDF, wrapping the CEK with A256KW (RFC 7518 §4.6): DIDComm anonymous encryption ("anoncrypt").
    /// </summary>
    public const string EcdhEsA256Kw = "ECDH-ES+A256KW";

    /// <summary>
    /// ECDH-1PU with Concat KDF, wrapping the CEK with A128KW (draft-madden-jose-ecdh-1pu-04 §2).
    /// </summary>
    public const string Ecdh1PuA128Kw = "ECDH-1PU+A128KW";

    /// <summary>
    /// ECDH-1PU with Concat KDF, wrapping the CEK with A192KW (draft-madden-jose-ecdh-1pu-04 §2).
    /// </summary>
    public const string Ecdh1PuA192Kw = "ECDH-1PU+A192KW";

    /// <summary>
    /// ECDH-1PU with Concat KDF, wrapping the CEK with A256KW (draft-madden-jose-ecdh-1pu-04 §2): DIDComm authenticated encryption
    /// ("authcrypt").
    /// </summary>
    public const string Ecdh1PuA256Kw = "ECDH-1PU+A256KW";

    /// <summary>
    /// AES-128-GCM content encryption (RFC 7518 §5.3).
    /// </summary>
    public const string A128Gcm = "A128GCM";

    /// <summary>
    /// AES-192-GCM content encryption (RFC 7518 §5.3).
    /// </summary>
    public const string A192Gcm = "A192GCM";

    /// <summary>
    /// AES-256-GCM content encryption (RFC 7518 §5.3).
    /// </summary>
    public const string A256Gcm = "A256GCM";

    /// <summary>
    /// AES-128-CBC with HMAC-SHA-256 content encryption (RFC 7518 §5.2.3).
    /// </summary>
    public const string A128CbcHs256 = "A128CBC-HS256";

    /// <summary>
    /// AES-192-CBC with HMAC-SHA-384 content encryption (RFC 7518 §5.2.4).
    /// </summary>
    public const string A192CbcHs384 = "A192CBC-HS384";

    /// <summary>
    /// AES-256-CBC with HMAC-SHA-512 content encryption (RFC 7518 §5.2.5).
    /// </summary>
    public const string A256CbcHs512 = "A256CBC-HS512";

    /// <summary>
    /// Gets the properties of the specified key management algorithm.
    /// </summary>
    /// <param name="algorithm">The JWE <c>alg</c> value.</param>
    /// <returns>Returns whether the algorithm is ECDH-1PU, and the key wrapping key length in bytes.</returns>
    /// <exception cref="CryptographicException">Thrown when the algorithm is not supported.</exception>
    internal static (bool IsOnePassUnified, int KeyWrapLength) GetKeyManagement(string algorithm) => algorithm switch
    {
        EcdhEsA128Kw => (false, 16),
        EcdhEsA192Kw => (false, 24),
        EcdhEsA256Kw => (false, 32),
        Ecdh1PuA128Kw => (true, 16),
        Ecdh1PuA192Kw => (true, 24),
        Ecdh1PuA256Kw => (true, 32),
        _ => throw new CryptographicException($"The JWE key management algorithm '{algorithm}' is not supported.")
    };

    /// <summary>
    /// Gets the properties of the specified content encryption algorithm.
    /// </summary>
    /// <param name="encryption">The JWE <c>enc</c> value.</param>
    /// <returns>Returns whether it is an AES-CBC-HMAC algorithm, and its key, IV and tag lengths in bytes.</returns>
    /// <exception cref="NotSupportedException">Thrown when the algorithm is not supported, such as XC20P.</exception>
    internal static (
        bool IsCbcHmac,
        int KeyLength,
        int IvLength,
        int TagLength) GetContentEncryption(string encryption) =>
        encryption switch
    {
        A128Gcm => (false, 16, 12, 16),
        A192Gcm => (false, 24, 12, 16),
        A256Gcm => (false, 32, 12, 16),
        A128CbcHs256 => (true, 32, 16, 16),
        A192CbcHs384 => (true, 48, 16, 24),
        A256CbcHs512 => (true, 64, 16, 32),
        _ => throw new NotSupportedException($"The JWE content encryption algorithm '{encryption}' is not supported.")
    };
}
