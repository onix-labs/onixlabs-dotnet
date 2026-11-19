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
using System.Numerics;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents ECDSA signature verification on the secp256k1 curve, as JOSE's <c>ES256K</c> requires (RFC 8812 §3.2), implemented in managed
/// code because the platform cryptography on macOS, and on some other platforms, does not support the curve.
/// </summary>
/// <remarks>
/// <para>
/// The curve constants are those of SEC 2 v2 §2.4.1. Verification follows SEC 1 v2 §4.1.4. Signatures with a high S value are accepted,
/// as RFC 8812 §3.2 neither requires nor forbids low S.
/// </para>
/// <para>
/// Why: only verification is provided, and it handles only public values, so it is not constant time. Signing would need constant-time
/// arithmetic on a secret scalar; this code must not be adapted to sign.
/// </para>
/// </remarks>
internal static class Secp256k1
{
    /// <summary>
    /// The length in bytes of a field element, a coordinate, and each of r and s.
    /// </summary>
    public const int ElementLength = 32;

    /// <summary>
    /// The OID of the secp256k1 named curve (SEC 2 v2 §A.2.1).
    /// </summary>
    public const string CurveOid = "1.3.132.0.10";

    /// <summary>
    /// The OID of id-ecPublicKey (RFC 5480 §2.1.1), the algorithm of an elliptic curve SubjectPublicKeyInfo.
    /// </summary>
    private const string EcPublicKeyOid = "1.2.840.10045.2.1";

    /// <summary>
    /// The order n of the base point G, from SEC 2 v2 §2.4.1.
    /// </summary>
    private static readonly BigInteger N = new(
        Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141"), isUnsigned: true, isBigEndian: true);

    /// <summary>
    /// The field prime p = 2^256 - 2^32 - 977, from SEC 2 v2 §2.4.1.
    /// </summary>
    private static readonly BigInteger P = new(
        Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F"), isUnsigned: true, isBigEndian: true);

    /// <summary>
    /// The base point G, from its uncompressed encoding in SEC 2 v2 §2.4.1.
    /// </summary>
    private static readonly Secp256k1Point G = Secp256k1Point.TryDecode(Convert.FromHexString(
        "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798" +
        "483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8"), out Secp256k1Point point)
        ? point
        : throw new InvalidOperationException("The secp256k1 base point is not on the curve.");

    /// <summary>
    /// Determines whether the specified SubjectPublicKeyInfo names the secp256k1 curve.
    /// </summary>
    /// <param name="subjectPublicKeyInfo">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the data is an id-ecPublicKey SubjectPublicKeyInfo whose parameters name secp256k1; otherwise,
    /// <see langword="false"/>, including when the data is malformed.
    /// </returns>
    public static bool IsSecp256k1SubjectPublicKeyInfo(ReadOnlySpan<byte> subjectPublicKeyInfo) =>
        TryReadSubjectPublicKeyInfo(subjectPublicKeyInfo, out bool isSecp256k1, out _, out _) && isSecp256k1;

    /// <summary>
    /// Attempts to decode a secp256k1 public key from a DER-encoded SubjectPublicKeyInfo (RFC 5480), checking that the point is on the
    /// curve.
    /// </summary>
    /// <param name="subjectPublicKeyInfo">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <param name="publicKey">When this method returns, contains the public point, if the data is a valid secp256k1 public key.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes of the SubjectPublicKeyInfo.</param>
    /// <returns>Returns <see langword="true"/> if the data is a valid secp256k1 public key; otherwise, <see langword="false"/>.</returns>
    public static bool TryDecodeSubjectPublicKeyInfo(
        ReadOnlySpan<byte> subjectPublicKeyInfo,
        out Secp256k1Point publicKey,
        out int bytesRead)
    {
        publicKey = Secp256k1Point.Infinity;

        return TryReadSubjectPublicKeyInfo(subjectPublicKeyInfo, out bool isSecp256k1, out byte[] encodedPoint, out bytesRead)
               && isSecp256k1
               && Secp256k1Point.TryDecode(encodedPoint, out publicKey);
    }

    /// <summary>
    /// Encodes a secp256k1 public key as a DER SubjectPublicKeyInfo (RFC 5480) with an uncompressed point (SEC 1 v2 §2.3.3).
    /// </summary>
    /// <param name="publicKey">The public point, which must not be the point at infinity.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    /// <remarks>Why: the uncompressed form is what the platform exports for the other curves, so every <see cref="EcdsaPublicKey"/> is
    /// alike.</remarks>
    public static byte[] EncodeSubjectPublicKeyInfo(in Secp256k1Point publicKey)
    {
        Span<byte> encodedPoint = stackalloc byte[1 + (2 * ElementLength)];
        encodedPoint[0] = 0x04;
        publicKey.GetAffineX().WriteBytes(encodedPoint[1..(1 + ElementLength)]);
        publicKey.GetAffineY().WriteBytes(encodedPoint[(1 + ElementLength)..]);

        AsnWriter writer = new(AsnEncodingRules.DER);

        using (writer.PushSequence())
        {
            using (writer.PushSequence())
            {
                writer.WriteObjectIdentifier(EcPublicKeyOid);
                writer.WriteObjectIdentifier(CurveOid);
            }

            writer.WriteBitString(encodedPoint);
        }

        return writer.Encode();
    }

    /// <summary>
    /// Verifies an ECDSA signature on secp256k1, per SEC 1 v2 §4.1.4.
    /// </summary>
    /// <param name="publicKey">The signer's public point.</param>
    /// <param name="hash">The hash of the signed message; only its leftmost 256 bits are used, as SEC 1 v2 §4.1.3 step 5 requires.</param>
    /// <param name="signature">The signature, in the specified format.</param>
    /// <param name="format">The signature format: IEEE P1363 (r ‖ s, 64 bytes), as JOSE uses, or DER, per RFC 3279.</param>
    /// <returns>Returns <see langword="true"/> if the signature is well formed and valid; otherwise, <see langword="false"/>.</returns>
    public static bool Verify(
        in Secp256k1Point publicKey,
        ReadOnlySpan<byte> hash,
        ReadOnlySpan<byte> signature,
        System.Security.Cryptography.DSASignatureFormat format)
    {
        if (publicKey.IsInfinity || !TryDecodeSignature(signature, format, out BigInteger r, out BigInteger s)) return false;

        if (r.Sign <= 0 || r >= N || s.Sign <= 0 || s >= N) return false;

        BigInteger e = HashToInteger(hash);
        BigInteger w = BigInteger.ModPow(s, N - 2, N);
        BigInteger u1 = e * w % N;
        BigInteger u2 = r * w % N;

        Secp256k1Point point = MultiplyAddVariableTime(u1, G, u2, publicKey);

        if (point.IsInfinity) return false;

        // Why: verification asks whether x(R) mod n = r. Since n < p, that holds when x(R) is r, or is r + n if r + n < p; comparing in
        // Jacobian coordinates avoids the field inversion that computing x(R) would need.
        return point.HasAffineX(ToFieldElement(r)) || r + N < P && point.HasAffineX(ToFieldElement(r + N));
    }

    /// <summary>
    /// Computes u1 · P + u2 · Q with Shamir's trick, a single run of doubling and adding that branches on the bits of the scalars.
    /// </summary>
    /// <param name="u1">The first public scalar, in [0, n).</param>
    /// <param name="p">The first point.</param>
    /// <param name="u2">The second public scalar, in [0, n).</param>
    /// <param name="q">The second point.</param>
    /// <returns>Returns u1 · P + u2 · Q.</returns>
    /// <remarks>Why: the name records that this leaks the scalars through timing; it is only for verification, where they are
    /// public.</remarks>
    private static Secp256k1Point MultiplyAddVariableTime(BigInteger u1, in Secp256k1Point p, BigInteger u2, in Secp256k1Point q)
    {
        Span<byte> k1 = stackalloc byte[ElementLength];
        Span<byte> k2 = stackalloc byte[ElementLength];
        WriteScalar(u1, k1);
        WriteScalar(u2, k2);

        Secp256k1Point pq = p + q;
        Secp256k1Point result = Secp256k1Point.Infinity;

        for (int i = 0; i < 8 * ElementLength; i++)
        {
            result = Secp256k1Point.Double(result);

            int shift = 7 - (i & 7);
            bool bit1 = ((k1[i >> 3] >> shift) & 1) == 1;
            bool bit2 = ((k2[i >> 3] >> shift) & 1) == 1;

            if (bit1 && bit2) result += pq;
            else if (bit1) result += p;
            else if (bit2) result += q;
        }

        return result;
    }

    /// <summary>
    /// Writes a scalar in [0, 2^256) as 32 big-endian bytes.
    /// </summary>
    /// <param name="value">The scalar to write.</param>
    /// <param name="destination">The 32-byte destination buffer.</param>
    private static void WriteScalar(BigInteger value, Span<byte> destination)
    {
        destination.Clear();
        int length = value.GetByteCount(isUnsigned: true);
        value.TryWriteBytes(destination[(ElementLength - length)..], out int _, isUnsigned: true, isBigEndian: true);
    }

    /// <summary>
    /// Converts an integer in [0, p) to a field element.
    /// </summary>
    /// <param name="value">The integer to convert.</param>
    /// <returns>Returns the field element.</returns>
    private static Secp256k1FieldElement ToFieldElement(BigInteger value)
    {
        Span<byte> bytes = stackalloc byte[ElementLength];
        WriteScalar(value, bytes);
        Secp256k1FieldElement.TryFromBytes(bytes, out Secp256k1FieldElement element);
        return element;
    }

    /// <summary>
    /// Converts a hash to the integer e of SEC 1 v2 §4.1.3 step 5: the leftmost 256 bits of the hash, or all of it if it is shorter.
    /// </summary>
    /// <param name="hash">The hash to convert.</param>
    /// <returns>Returns the integer e.</returns>
    private static BigInteger HashToInteger(ReadOnlySpan<byte> hash) =>
        new(hash.Length > ElementLength ? hash[..ElementLength] : hash, isUnsigned: true, isBigEndian: true);

    /// <summary>
    /// Attempts to decode the integers r and s from a signature in the specified format.
    /// </summary>
    /// <param name="signature">The signature to decode.</param>
    /// <param name="format">The signature format.</param>
    /// <param name="r">When this method returns, contains r, if the signature is well formed.</param>
    /// <param name="s">When this method returns, contains s, if the signature is well formed.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the signature is exactly 64 bytes (IEEE P1363), or a strict DER SEQUENCE of two INTEGERs with no
    /// trailing data (RFC 3279); otherwise, <see langword="false"/>.
    /// </returns>
    private static bool TryDecodeSignature(
        ReadOnlySpan<byte> signature, System.Security.Cryptography.DSASignatureFormat format, out BigInteger r, out BigInteger s)
    {
        r = BigInteger.Zero;
        s = BigInteger.Zero;

        if (format == System.Security.Cryptography.DSASignatureFormat.IeeeP1363FixedFieldConcatenation)
        {
            if (signature.Length != 2 * ElementLength) return false;

            r = new BigInteger(signature[..ElementLength], isUnsigned: true, isBigEndian: true);
            s = new BigInteger(signature[ElementLength..], isUnsigned: true, isBigEndian: true);
            return true;
        }

        if (format != System.Security.Cryptography.DSASignatureFormat.Rfc3279DerSequence) return false;

        try
        {
            AsnReader reader = new(signature.ToArray(), AsnEncodingRules.DER);
            AsnReader sequence = reader.ReadSequence();
            reader.ThrowIfNotEmpty();
            r = sequence.ReadInteger();
            s = sequence.ReadInteger();
            sequence.ThrowIfNotEmpty();
            return true;
        }
        catch (AsnContentException)
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to read an elliptic curve SubjectPublicKeyInfo (RFC 5480): its curve and its encoded point.
    /// </summary>
    /// <param name="data">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <param name="isSecp256k1">When this method returns, indicates whether the parameters name the secp256k1 curve.</param>
    /// <param name="encodedPoint">When this method returns, contains the encoded point from the subjectPublicKey BIT STRING.</param>
    /// <param name="bytesRead">When this method returns, contains the number of bytes of the SubjectPublicKeyInfo.</param>
    /// <returns>Returns <see langword="true"/> if the data is a well-formed id-ecPublicKey SubjectPublicKeyInfo with a named curve;
    /// otherwise, <see langword="false"/>.</returns>
    private static bool TryReadSubjectPublicKeyInfo(
        ReadOnlySpan<byte> data,
        out bool isSecp256k1,
        out byte[] encodedPoint,
        out int bytesRead)
    {
        isSecp256k1 = false;
        encodedPoint = [];
        bytesRead = 0;

        try
        {
            AsnReader reader = new(data.ToArray(), AsnEncodingRules.DER);
            int length = reader.PeekEncodedValue().Length;
            AsnReader sequence = reader.ReadSequence();
            AsnReader algorithm = sequence.ReadSequence();

            if (algorithm.ReadObjectIdentifier() != EcPublicKeyOid) return false;

            string curve = algorithm.ReadObjectIdentifier();
            algorithm.ThrowIfNotEmpty();

            byte[] point = sequence.ReadBitString(out int unusedBitCount);
            sequence.ThrowIfNotEmpty();

            if (unusedBitCount != 0) return false;

            isSecp256k1 = curve == CurveOid;
            encodedPoint = point;
            bytesRead = length;
            return true;
        }
        catch (AsnContentException)
        {
            return false;
        }
    }
}
