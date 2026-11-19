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

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the elliptic curves that JOSE names in a JWK <c>crv</c> member (RFC 7518 §6.2.1.1 and RFC 8812 §3.1), and the DER encodings
/// of their keys: SubjectPublicKeyInfo (RFC 5480) and ECPrivateKey (RFC 5915).
/// </summary>
/// <remarks>
/// Why: the encodings are written here rather than by the platform, so that conversion between a JWK and a library key never depends on
/// the platform supporting the curve; macOS, for example, does not support secp256k1.
/// </remarks>
internal static class EcKeyEncoding
{
    /// <summary>
    /// The OID of id-ecPublicKey (RFC 5480 §2.1.1).
    /// </summary>
    private const string EcPublicKeyOid = "1.2.840.10045.2.1";

    /// <summary>
    /// The curves, with their JOSE names, OIDs and field lengths in bytes.
    /// </summary>
    private static readonly (string Name, string Oid, int Length)[] Curves =
    [
        ("P-256", "1.2.840.10045.3.1.7", 32),
        ("P-384", "1.3.132.0.34", 48),
        ("P-521", "1.3.132.0.35", 66),
        ("secp256k1", "1.3.132.0.10", 32)
    ];

    /// <summary>
    /// Gets the field length in bytes of the curve with the specified JOSE name.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <returns>Returns the field length in bytes, which is the length of each coordinate and of the private scalar.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is not one of P-256, P-384, P-521 or secp256k1.</exception>
    public static int GetFieldLength(string curve) => Find(curve).Length;

    /// <summary>
    /// Gets the OID of the curve with the specified JOSE name.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <returns>Returns the OID of the named curve.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is not one of P-256, P-384, P-521 or secp256k1.</exception>
    public static string GetOid(string curve) => Find(curve).Oid;

    /// <summary>
    /// Encodes an uncompressed public point as a DER SubjectPublicKeyInfo.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <param name="x">The x-coordinate, of the curve's field length.</param>
    /// <param name="y">The y-coordinate, of the curve's field length.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    public static byte[] EncodeSubjectPublicKeyInfo(string curve, ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
    {
        AsnWriter writer = new(AsnEncodingRules.DER);

        using (writer.PushSequence())
        {
            using (writer.PushSequence())
            {
                writer.WriteObjectIdentifier(EcPublicKeyOid);
                writer.WriteObjectIdentifier(GetOid(curve));
            }

            writer.WriteBitString([0x04, ..x, ..y]);
        }

        return writer.Encode();
    }

    /// <summary>
    /// Decodes a DER SubjectPublicKeyInfo with a named curve and an uncompressed point.
    /// </summary>
    /// <param name="data">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <returns>Returns the JOSE curve name and the coordinates.</returns>
    /// <exception cref="CryptographicException">Thrown when the data is malformed, names an unsupported curve, or has a compressed
    /// point.</exception>
    public static (string Curve, byte[] X, byte[] Y) DecodeSubjectPublicKeyInfo(ReadOnlySpan<byte> data)
    {
        try
        {
            AsnReader reader = new(data.ToArray(), AsnEncodingRules.DER);
            AsnReader sequence = reader.ReadSequence();
            AsnReader algorithm = sequence.ReadSequence();

            if (algorithm.ReadObjectIdentifier() != EcPublicKeyOid)
                throw new CryptographicException("The SubjectPublicKeyInfo is not an elliptic curve public key.");

            (string name, int length) = FindByOid(algorithm.ReadObjectIdentifier());
            byte[] point = sequence.ReadBitString(out int _);
            (byte[] x, byte[] y) = SplitUncompressedPoint(point, length);

            return (name, x, y);
        }
        catch (AsnContentException exception)
        {
            throw new CryptographicException("The SubjectPublicKeyInfo is malformed.", exception);
        }
    }

    /// <summary>
    /// Encodes a private scalar and its public point as a DER ECPrivateKey (RFC 5915), with the curve and public key included.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <param name="d">The private scalar, of the curve's field length.</param>
    /// <param name="x">The x-coordinate of the public point.</param>
    /// <param name="y">The y-coordinate of the public point.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded ECPrivateKey, which the caller should zero after
    /// use.</returns>
    public static byte[] EncodeECPrivateKey(string curve, ReadOnlySpan<byte> d, ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
    {
        AsnWriter writer = new(AsnEncodingRules.DER);

        using (writer.PushSequence())
        {
            writer.WriteInteger(1);
            writer.WriteOctetString(d);

            using (writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 0)))
                writer.WriteObjectIdentifier(GetOid(curve));

            using (writer.PushSequence(new Asn1Tag(TagClass.ContextSpecific, 1)))
                writer.WriteBitString([0x04, ..x, ..y]);
        }

        return writer.Encode();
    }

    /// <summary>
    /// Decodes a DER ECPrivateKey (RFC 5915) that includes its curve and public key, as the library's EC private keys do.
    /// </summary>
    /// <param name="data">The DER-encoded ECPrivateKey.</param>
    /// <returns>Returns the JOSE curve name, the private scalar, and the public point's coordinates.</returns>
    /// <exception cref="CryptographicException">Thrown when the data is malformed, names an unsupported curve, or omits the curve or public
    /// key.</exception>
    public static (string Curve, byte[] D, byte[] X, byte[] Y) DecodeECPrivateKey(ReadOnlySpan<byte> data)
    {
        try
        {
            AsnReader reader = new(data.ToArray(), AsnEncodingRules.BER);
            AsnReader sequence = reader.ReadSequence();

            if (!sequence.TryReadInt32(out int version) || version != 1)
                throw new CryptographicException("The ECPrivateKey has an unsupported version.");

            byte[] d = sequence.ReadOctetString();
            Asn1Tag parametersTag = new(TagClass.ContextSpecific, 0);
            Asn1Tag publicKeyTag = new(TagClass.ContextSpecific, 1);

            if (!sequence.HasData || !sequence.PeekTag().HasSameClassAndValue(parametersTag))
                throw new CryptographicException("The ECPrivateKey does not name its curve.");

            (string name, int length) = FindByOid(sequence.ReadSequence(parametersTag).ReadObjectIdentifier());

            if (!sequence.HasData || !sequence.PeekTag().HasSameClassAndValue(publicKeyTag))
                throw new CryptographicException("The ECPrivateKey does not include its public key.");

            byte[] point = sequence.ReadSequence(publicKeyTag).ReadBitString(out int _);
            (byte[] x, byte[] y) = SplitUncompressedPoint(point, length);

            if (d.Length > length) throw new CryptographicException("The ECPrivateKey scalar is too long for its curve.");

            byte[] padded = new byte[length];
            d.CopyTo(padded.AsSpan(length - d.Length));
            CryptographicOperations.ZeroMemory(d);

            return (name, padded, x, y);
        }
        catch (AsnContentException exception)
        {
            throw new CryptographicException("The ECPrivateKey is malformed.", exception);
        }
    }

    /// <summary>
    /// Splits an uncompressed SEC 1 point, <c>04 ‖ X ‖ Y</c>, into its coordinates.
    /// </summary>
    /// <param name="point">The encoded point.</param>
    /// <param name="length">The field length of the curve, in bytes.</param>
    /// <returns>Returns the x and y coordinates.</returns>
    /// <exception cref="CryptographicException">Thrown when the point is not an uncompressed point of the expected length.</exception>
    private static (byte[] X, byte[] Y) SplitUncompressedPoint(ReadOnlySpan<byte> point, int length)
    {
        if (point.Length != 1 + (2 * length) || point[0] != 0x04)
            throw new CryptographicException("The elliptic curve point is not an uncompressed point on its curve.");

        return (point.Slice(1, length).ToArray(), point.Slice(1 + length, length).ToArray());
    }

    /// <summary>
    /// Finds the curve with the specified JOSE name.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <returns>Returns the curve's OID and field length.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is not supported.</exception>
    private static (string Oid, int Length) Find(string curve)
    {
        foreach ((string name, string oid, int length) in Curves)
            if (name == curve) return (oid, length);

        throw new CryptographicException($"The elliptic curve '{curve}' is not supported.");
    }

    /// <summary>
    /// Finds the curve with the specified OID.
    /// </summary>
    /// <param name="oid">The curve OID.</param>
    /// <returns>Returns the curve's JOSE name and field length.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve is not supported.</exception>
    private static (string Name, int Length) FindByOid(string oid)
    {
        foreach ((string name, string curveOid, int length) in Curves)
            if (curveOid == oid) return (name, length);

        throw new CryptographicException($"The elliptic curve with OID '{oid}' is not supported.");
    }
}
