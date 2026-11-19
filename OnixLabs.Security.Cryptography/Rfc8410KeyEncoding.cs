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
using System.Formats.Asn1;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents ASN.1, PKCS#8, and SubjectPublicKeyInfo helpers for the fixed-length curve keys of RFC 8410 (Ed25519 and X25519),
/// per RFC 5208, RFC 5280, RFC 5958, and RFC 8410. The algorithm identifier is the curve's OID with absent parameters.
/// </summary>
internal static class Rfc8410KeyEncoding
{
    /// <summary>
    /// Encodes the specified private key as a PKCS#8 <c>PrivateKeyInfo</c> per RFC 8410 §7.
    /// </summary>
    /// <param name="oid">The algorithm OID assigned to the curve by RFC 8410.</param>
    /// <param name="privateKey">The private key to encode.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded PKCS#8 PrivateKeyInfo.</returns>
    public static byte[] EncodePrivateKey(string oid, ReadOnlySpan<byte> privateKey)
    {
        // RFC 8410: inside the outer privateKey OCTET STRING, the actual key material is itself
        // wrapped as a DER OCTET STRING (CurvePrivateKey ::= OCTET STRING).
        AsnWriter inner = new(AsnEncodingRules.DER);
        inner.WriteOctetString(privateKey);
        byte[] innerEncoded = inner.Encode();

        AsnWriter writer = new(AsnEncodingRules.DER);
        using (writer.PushSequence())
        {
            writer.WriteInteger(0); // version v1

            using (writer.PushSequence())
                writer.WriteObjectIdentifier(oid);

            writer.WriteOctetString(innerEncoded);
        }

        try
        {
            return writer.Encode();
        }
        finally
        {
            CryptographicOperations.ZeroMemory(innerEncoded);
        }
    }

    /// <summary>
    /// Decodes a PKCS#8 <c>PrivateKeyInfo</c> for the specified curve and returns the private key.
    /// </summary>
    /// <param name="oid">The algorithm OID assigned to the curve by RFC 8410.</param>
    /// <param name="name">The name of the curve algorithm, used in exception messages.</param>
    /// <param name="keyLength">The required length of the private key, in bytes.</param>
    /// <param name="data">The DER-encoded PKCS#8 PrivateKeyInfo to decode.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes consumed from <paramref name="data"/>.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the private key.</returns>
    /// <exception cref="CryptographicException">Thrown when the algorithm OID is not the expected one, the structure is malformed, or the
    /// key length is incorrect.</exception>
    /// <remarks>
    /// Why: the copy of <paramref name="data"/> and the outer OCTET STRING both hold the private key, so both are zeroed before returning,
    /// as is the decoded key when it is the wrong length.
    /// </remarks>
    public static byte[] DecodePrivateKey(string oid, string name, int keyLength, ReadOnlySpan<byte> data, out int bytesRead)
    {
        byte[] source = data.ToArray();
        byte[]? outer = null;

        try
        {
            AsnReader reader = new(source, AsnEncodingRules.BER);
            ReadOnlyMemory<byte> encodedSequence = reader.PeekEncodedValue();
            AsnReader sequence = reader.ReadSequence();
            bytesRead = encodedSequence.Length;

            if (!sequence.TryReadInt32(out int version) || version is not (0 or 1))
                throw new CryptographicException("PKCS#8 PrivateKeyInfo has unsupported version.");

            AsnReader algorithm = sequence.ReadSequence();
            string actualOid = algorithm.ReadObjectIdentifier();

            if (actualOid != oid)
                throw new CryptographicException($"PKCS#8 PrivateKeyInfo algorithm OID is not {name} (got {actualOid}).");

            if (algorithm.HasData)
                throw new CryptographicException($"{name} algorithm identifier must have no parameters.");

            outer = sequence.ReadOctetString();
            AsnReader innerReader = new(outer, AsnEncodingRules.BER);
            byte[] privateKey = innerReader.ReadOctetString();

            if (!innerReader.HasData && privateKey.Length == keyLength) return privateKey;

            CryptographicOperations.ZeroMemory(privateKey);

            throw innerReader.HasData
                ? new CryptographicException("Failed to decode PKCS#8 PrivateKeyInfo.")
                : new CryptographicException($"{name} private key must be {keyLength} bytes (got {privateKey.Length}).");
        }
        catch (AsnContentException e)
        {
            throw new CryptographicException("Failed to decode PKCS#8 PrivateKeyInfo.", e);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(source);
            if (outer is not null) CryptographicOperations.ZeroMemory(outer);
        }
    }

    /// <summary>
    /// Encodes the specified public key as a SubjectPublicKeyInfo per RFC 5280 and RFC 8410.
    /// </summary>
    /// <param name="oid">The algorithm OID assigned to the curve by RFC 8410.</param>
    /// <param name="publicKey">The public key to encode.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    public static byte[] EncodePublicKey(string oid, ReadOnlySpan<byte> publicKey)
    {
        AsnWriter writer = new(AsnEncodingRules.DER);

        using (writer.PushSequence())
        {
            using (writer.PushSequence())
                writer.WriteObjectIdentifier(oid);

            writer.WriteBitString(publicKey);
        }

        return writer.Encode();
    }

    /// <summary>
    /// Decodes a SubjectPublicKeyInfo for the specified curve and returns the public key.
    /// </summary>
    /// <param name="oid">The algorithm OID assigned to the curve by RFC 8410.</param>
    /// <param name="name">The name of the curve algorithm, used in exception messages.</param>
    /// <param name="keyLength">The required length of the public key, in bytes.</param>
    /// <param name="data">The DER-encoded SubjectPublicKeyInfo to decode.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes consumed from <paramref name="data"/>.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the public key.</returns>
    /// <exception cref="CryptographicException">Thrown when the algorithm OID is not the expected one, the structure is malformed, or the
    /// key length is incorrect.</exception>
    public static byte[] DecodePublicKey(string oid, string name, int keyLength, ReadOnlySpan<byte> data, out int bytesRead)
    {
        try
        {
            ReadOnlyMemory<byte> source = data.ToArray();
            AsnReader reader = new(source, AsnEncodingRules.BER);
            ReadOnlyMemory<byte> encodedSequence = reader.PeekEncodedValue();
            AsnReader sequence = reader.ReadSequence();
            bytesRead = encodedSequence.Length;

            AsnReader algorithm = sequence.ReadSequence();
            string actualOid = algorithm.ReadObjectIdentifier();

            if (actualOid != oid)
                throw new CryptographicException($"SubjectPublicKeyInfo algorithm OID is not {name} (got {actualOid}).");

            if (algorithm.HasData)
                throw new CryptographicException($"{name} algorithm identifier must have no parameters.");

            byte[] publicKey = sequence.ReadBitString(out int unusedBitCount);

            if (unusedBitCount != 0)
                throw new CryptographicException($"{name} public-key BIT STRING must have zero unused bits.");

            return publicKey.Length == keyLength
                ? publicKey
                : throw new CryptographicException($"{name} public key must be {keyLength} bytes (got {publicKey.Length}).");
        }
        catch (AsnContentException e)
        {
            throw new CryptographicException("Failed to decode SubjectPublicKeyInfo.", e);
        }
    }
}
