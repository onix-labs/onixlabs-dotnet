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
/// Represents ASN.1, PKCS#8, and SubjectPublicKeyInfo helpers for X25519 per RFC 5208, RFC 5280,
/// RFC 5958, and RFC 8410. The X25519 algorithm identifier is the OID 1.3.101.110 with absent parameters.
/// </summary>
internal static class X25519Pkcs8
{
    /// <summary>
    /// The X25519 algorithm OID assigned by RFC 8410.
    /// </summary>
    private const string X25519Oid = "1.3.101.110";

    /// <summary>
    /// The name of the algorithm, used in exception messages.
    /// </summary>
    private const string Name = "X25519";

    /// <summary>
    /// Encodes the 32-byte X25519 private key as a PKCS#8 <c>PrivateKeyInfo</c> per RFC 8410 §7.
    /// </summary>
    /// <param name="privateKey">The 32-byte X25519 private key to encode.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded PKCS#8 PrivateKeyInfo.</returns>
    public static byte[] EncodePrivateKey(ReadOnlySpan<byte> privateKey) => Rfc8410KeyEncoding.EncodePrivateKey(X25519Oid, privateKey);

    /// <summary>
    /// Decodes a PKCS#8 <c>PrivateKeyInfo</c> for X25519 and returns the 32-byte private key.
    /// </summary>
    /// <param name="data">The DER-encoded PKCS#8 PrivateKeyInfo to decode.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes consumed from <paramref name="data"/>.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte X25519 private key.</returns>
    /// <exception cref="CryptographicException">Thrown when the algorithm OID is not X25519, the structure is malformed, or the key length
    /// is incorrect.</exception>
    public static byte[] DecodePrivateKey(ReadOnlySpan<byte> data, out int bytesRead) =>
        Rfc8410KeyEncoding.DecodePrivateKey(X25519Oid, Name, X25519.PrivateKeySizeInBytes, data, out bytesRead);

    /// <summary>
    /// Encodes the 32-byte X25519 public key as a SubjectPublicKeyInfo per RFC 5280 and RFC 8410.
    /// </summary>
    /// <param name="publicKey">The 32-byte X25519 public key to encode.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    public static byte[] EncodePublicKey(ReadOnlySpan<byte> publicKey) => Rfc8410KeyEncoding.EncodePublicKey(X25519Oid, publicKey);

    /// <summary>
    /// Decodes a SubjectPublicKeyInfo for X25519 and returns the 32-byte public key.
    /// </summary>
    /// <param name="data">The DER-encoded SubjectPublicKeyInfo to decode.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes consumed from <paramref name="data"/>.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte X25519 public key.</returns>
    /// <exception cref="CryptographicException">Thrown when the algorithm OID is not X25519, the structure is malformed, or the public-key
    /// length is incorrect.</exception>
    public static byte[] DecodePublicKey(ReadOnlySpan<byte> data, out int bytesRead) =>
        Rfc8410KeyEncoding.DecodePublicKey(X25519Oid, Name, X25519.PublicKeySizeInBytes, data, out bytesRead);
}
