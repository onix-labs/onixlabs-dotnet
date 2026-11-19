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
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using OnixLabs.Security.Cryptography.UnitTests.Data;

namespace OnixLabs.Security.Cryptography.UnitTests;

/// <summary>
/// Verifies the managed secp256k1 field, point and ECDSA verification code, against Project Wycheproof and against independent
/// <see cref="BigInteger"/> arithmetic.
/// </summary>
public sealed class Secp256k1Tests
{
    private const int RandomCaseCount = 1000;

    /// <summary>
    /// The field prime p = 2^256 - 2^32 - 977, from SEC 2 v2 §2.4.1.
    /// </summary>
    private static readonly BigInteger P = BigInteger.Pow(2, 256) - BigInteger.Pow(2, 32) - 977;

    /// <summary>
    /// Verifies every case of Project Wycheproof's <c>testvectors_v1/ecdsa_secp256k1_sha256_test.json</c> (DER) and
    /// <c>ecdsa_secp256k1_sha256_p1363_test.json</c> (IEEE P1363), against the internal verification code.
    /// </summary>
    [Theory(DisplayName = "Secp256k1.Verify must satisfy Project Wycheproof secp256k1 ECDSA SHA-256 test vectors")]
    [WycheproofSecp256k1Data(DSASignatureFormat.Rfc3279DerSequence)]
    [WycheproofSecp256k1Data(DSASignatureFormat.IeeeP1363FixedFieldConcatenation)]
    public void Secp256k1VerifyMustSatisfyWycheproofTestVectors(
        int tcId, DSASignatureFormat format, byte[] publicKey, byte[] message, byte[] signature, string result, string flags)
    {
        // Given
        _ = (tcId, flags);
        bool decoded = Secp256k1.TryDecodeSubjectPublicKeyInfo(publicKey, out Secp256k1Point point, out int bytesRead);

        // When
        bool actual = Secp256k1.Verify(point, SHA256.HashData(message), signature, format);

        // Then
        Assert.True(decoded);
        Assert.Equal(publicKey.Length, bytesRead);
        Assert.Equal(result == "valid", actual);
    }

    /// <summary>
    /// Verifies the same Project Wycheproof cases through the public <see cref="EcdsaPublicKey"/> API, which recognises a secp256k1
    /// SubjectPublicKeyInfo and verifies it in managed code on every platform. Each key must also export unchanged.
    /// </summary>
    [Theory(DisplayName = "EcdsaPublicKey must satisfy Project Wycheproof secp256k1 ECDSA SHA-256 test vectors")]
    [WycheproofSecp256k1Data(DSASignatureFormat.Rfc3279DerSequence)]
    [WycheproofSecp256k1Data(DSASignatureFormat.IeeeP1363FixedFieldConcatenation)]
    public void EcdsaPublicKeyMustSatisfyWycheproofSecp256k1TestVectors(
        int tcId, DSASignatureFormat format, byte[] publicKey, byte[] message, byte[] signature, string result, string flags)
    {
        // Given
        _ = (tcId, flags);
        EcdsaPublicKey key = EcdsaPublicKey.Import(publicKey);
        bool expected = result == "valid";

        // When
        bool dataValid = key.IsDataValid(signature, message, HashAlgorithmName.SHA256, format);
        bool hashValid = key.IsHashValid(signature, SHA256.HashData(message), format);

        // Then
        Assert.Equal(expected, dataValid);
        Assert.Equal(expected, hashValid);
        Assert.Equal(publicKey, key.Export());
    }

    [Fact(DisplayName = "EcdsaPublicKey should import a secp256k1 PEM, verify a signature, and export the same PEM")]
    public void EcdsaPublicKeyShouldImportSecp256k1Pem()
    {
        // Given (Wycheproof ecdsa_secp256k1_sha256_p1363_test.json, the first test group and its tcId 1)
        const string pem =
            "-----BEGIN PUBLIC KEY-----\n" +
            "MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEuDj/ROW8F3vyEYnQdmCC/J2EMiaIf8l2\n" +
            "A3EQC37iCm/wyddb+6ezGmvKGXRJbutW3jVwcZVdg8Sxutqgshgy6Q==\n" +
            "-----END PUBLIC KEY-----";
        byte[] message = Convert.FromHexString("313233343030");
        byte[] signature = Convert.FromHexString(
            "813ef79ccefa9a56f7ba805f0e478584fe5f0dd5f567bc09b5123ccbc9832365" +
            "900e75ad233fcc908509dbff5922647db37c21f4afd3203ae8dc4ae7794b0f87");

        // When
        EcdsaPublicKey key = EcdsaPublicKey.ImportPem(pem);

        // Then
        Assert.True(key.IsDataValid(signature, message, HashAlgorithmName.SHA256));
        Assert.NotNull(Record.Exception(() => key.VerifyData(signature, [1, 2, 3], HashAlgorithmName.SHA256)));
        Assert.Equal(pem, key.ExportPem().Replace("\r\n", "\n").TrimEnd());
    }

    [Fact(DisplayName = "EcdsaPublicKey.Import should accept a compressed secp256k1 point and store it uncompressed")]
    public void EcdsaPublicKeyImportShouldAcceptCompressedSecp256k1Point()
    {
        // Given (SEC 2 v2 §2.4.1: the base point G, compressed and uncompressed, in a SubjectPublicKeyInfo)
        byte[] compressed = Convert.FromHexString(
            "3036301006072A8648CE3D020106052B8104000A032200" + "0279BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798");
        byte[] uncompressed = Convert.FromHexString(
            "3056301006072A8648CE3D020106052B8104000A034200" +
            "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F817" +
            "98483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4" +
            "B8");

        // When
        EcdsaPublicKey key = EcdsaPublicKey.Import(compressed, out int bytesRead);

        // Then
        Assert.Equal(compressed.Length, bytesRead);
        Assert.Equal(uncompressed, key.Export());
    }

    [Fact(DisplayName = "EcdsaPublicKey.Import should throw CryptographicException for a secp256k1 point that is not on the curve")]
    public void EcdsaPublicKeyImportShouldThrowForOffCurveSecp256k1Point()
    {
        // Given
        byte[] offCurve = Convert.FromHexString(
            "3056301006072A8648CE3D020106052B8104000A034200" +
            "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F817" +
            "98483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4" +
            "B9");

        // When / Then
        Assert.Throws<CryptographicException>(() => EcdsaPublicKey.Import(offCurve));
        Assert.Throws<CryptographicException>(() => new EcdsaPublicKey(offCurve).IsHashValid(new byte[64], new byte[32]));
    }

    [Fact(DisplayName = "Secp256k1FieldElement arithmetic should match BigInteger arithmetic modulo p for random and edge-case inputs")]
    public void FieldArithmeticShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 256);
        List<BigInteger> values =
        [
            0, 1, 2, P - 1, P - 2, (P - 1) / 2, BigInteger.Pow(2, 255), BigInteger.Pow(2, 192) - 1, BigInteger.Pow(2, 64)
        ];
        for (int i = 0; i < RandomCaseCount; i++) values.Add(RandomElement(random));
        List<string> failures = [];

        // When
        for (int i = 0; i < values.Count; i++)
        {
            BigInteger a = values[i];
            BigInteger b = values[(i * 7 + 3) % values.Count];
            Secp256k1FieldElement fa = ToElement(a);
            Secp256k1FieldElement fb = ToElement(b);

            Check(failures, i, "a+b", a + b, fa + fb);
            Check(failures, i, "a-b", a - b, fa - fb);
            Check(failures, i, "a*b", a * b, fa * fb);
            Check(failures, i, "a^2", a * a, Secp256k1FieldElement.Square(fa));
            if (!a.IsZero) Check(failures, i, "a^-1", BigInteger.ModPow(a, P - 2, P), Secp256k1FieldElement.Invert(fa));
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "Secp256k1FieldElement.TrySquareRoot should find roots of squares and reject non-squares")]
    public void FieldSquareRootShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 3);

        for (int i = 0; i < 200; i++)
        {
            BigInteger a = RandomElement(random);
            BigInteger square = a * a % P;
            bool isResidue = BigInteger.ModPow(a, (P - 1) / 2, P) == 1;

            // When
            bool squareHasRoot = Secp256k1FieldElement.TrySquareRoot(ToElement(square), out Secp256k1FieldElement root);
            bool valueHasRoot = Secp256k1FieldElement.TrySquareRoot(ToElement(a), out Secp256k1FieldElement _);

            // Then
            Assert.True(squareHasRoot);
            Assert.Equal(square, ToInteger(Secp256k1FieldElement.Square(root)));
            Assert.Equal(isResidue || a.IsZero, valueHasRoot);
        }
    }

    [Fact(DisplayName = "Secp256k1FieldElement.TryFromBytes should reject values of p and above")]
    public void FieldFromBytesShouldRejectValuesOfPAndAbove()
    {
        // When / Then
        Assert.False(Secp256k1FieldElement.TryFromBytes(ToBytes(P), out _));
        Assert.False(Secp256k1FieldElement.TryFromBytes(ToBytes(BigInteger.Pow(2, 256) - 1), out _));
        Assert.True(Secp256k1FieldElement.TryFromBytes(ToBytes(P - 1), out _));
        Assert.False(Secp256k1FieldElement.TryFromBytes(new byte[31], out _));
    }

    /// <summary>
    /// Verifies point addition and doubling against affine <see cref="BigInteger"/> formulas, by comparing k · G for random k computed both
    /// ways, starting from the base point of SEC 2 v2 §2.4.1.
    /// </summary>
    [Fact(DisplayName = "Secp256k1Point arithmetic should match an affine BigInteger reference")]
    public void PointArithmeticShouldMatchBigIntegerReference()
    {
        // Given
        BigInteger gx = BigInteger.Parse(
            "079BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F8179" +
            "8", System.Globalization.NumberStyles.HexNumber);
        BigInteger gy = BigInteger.Parse(
            "0483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B" +
            "8", System.Globalization.NumberStyles.HexNumber);
        Assert.True(Secp256k1Point.TryFromAffine(ToElement(gx), ToElement(gy), out Secp256k1Point g));
        Random random = new(Seed: 11);

        for (int i = 0; i < 20; i++)
        {
            int k = random.Next(2, 5000);

            // When
            Secp256k1Point actual = Secp256k1Point.Infinity;
            for (int bit = 31 - int.LeadingZeroCount(k); bit >= 0; bit--)
            {
                actual = Secp256k1Point.Double(actual);
                if (((k >> bit) & 1) == 1) actual += g;
            }

            (BigInteger x, BigInteger y) expected = (gx, gy);
            for (int j = 1; j < k; j++) expected = AffineAdd(expected, (gx, gy));

            // Then
            Assert.Equal(expected.x, ToInteger(actual.GetAffineX()));
            Assert.Equal(expected.y, ToInteger(actual.GetAffineY()));
        }
    }

    [Fact(DisplayName = "Secp256k1Point.TryDecode should accept compressed and uncompressed forms of one point, and reject others")]
    public void PointDecodeShouldAcceptCompressedAndUncompressed()
    {
        // Given
        byte[] uncompressed = Convert.FromHexString(
            "0479BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F817" +
            "98483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4" +
            "B8");
        byte[] compressed = Convert.FromHexString("0279BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798");
        byte[] wrongParity = Convert.FromHexString("0379BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798");
        byte[] offCurve = [..uncompressed];
        offCurve[^1] ^= 1;

        // When
        bool uncompressedDecoded = Secp256k1Point.TryDecode(uncompressed, out Secp256k1Point fromUncompressed);
        bool compressedDecoded = Secp256k1Point.TryDecode(compressed, out Secp256k1Point fromCompressed);
        bool wrongParityDecoded = Secp256k1Point.TryDecode(wrongParity, out Secp256k1Point fromWrongParity);

        // Then
        Assert.True(uncompressedDecoded);
        Assert.True(compressedDecoded);
        Assert.True(wrongParityDecoded);
        Assert.Equal(ToInteger(fromUncompressed.GetAffineY()), ToInteger(fromCompressed.GetAffineY()));
        Assert.Equal(P - ToInteger(fromUncompressed.GetAffineY()), ToInteger(fromWrongParity.GetAffineY()));
        Assert.False(Secp256k1Point.TryDecode(offCurve, out _));
        Assert.False(Secp256k1Point.TryDecode([0x00], out _));
        Assert.False(Secp256k1Point.TryDecode(uncompressed.AsSpan(0, 64), out _));
    }

    /// <summary>
    /// Adds two distinct affine points with the textbook chord formulas over <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>Returns the sum of the points.</returns>
    private static (BigInteger x, BigInteger y) AffineAdd((BigInteger x, BigInteger y) left, (BigInteger x, BigInteger y) right)
    {
        BigInteger lambda = left.x == right.x
            ? 3 * left.x * left.x * BigInteger.ModPow(2 * left.y, P - 2, P)
            : (right.y - left.y) * BigInteger.ModPow(((right.x - left.x) % P + P) % P, P - 2, P);
        lambda = (lambda % P + P) % P;
        BigInteger x = ((lambda * lambda - left.x - right.x) % P + P) % P;
        BigInteger y = ((lambda * (left.x - x) - left.y) % P + P) % P;
        return (x, y);
    }

    /// <summary>
    /// Records a failure when the specified field element does not equal the specified integer modulo p.
    /// </summary>
    /// <param name="failures">The list to which failures are added.</param>
    /// <param name="index">The index of the case.</param>
    /// <param name="operation">A description of the operation that was checked.</param>
    /// <param name="expected">The expected value, before reduction modulo p.</param>
    /// <param name="actual">The field element produced by the operation.</param>
    private static void Check(List<string> failures, int index, string operation, BigInteger expected, Secp256k1FieldElement actual)
    {
        BigInteger reduced = (expected % P + P) % P;
        BigInteger actualValue = ToInteger(actual);

        if (reduced != actualValue)
            failures.Add($"case {index}: {operation} expected {reduced} but was {actualValue}");
    }

    /// <summary>
    /// Creates a random integer in [0, p).
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <returns>Returns a random integer in [0, p).</returns>
    private static BigInteger RandomElement(Random random)
    {
        byte[] bytes = new byte[32];
        random.NextBytes(bytes);
        return new BigInteger(bytes, isUnsigned: true) % P;
    }

    /// <summary>
    /// Converts an integer in [0, p) to a field element.
    /// </summary>
    /// <param name="value">The integer to convert.</param>
    /// <returns>Returns the field element.</returns>
    private static Secp256k1FieldElement ToElement(BigInteger value) =>
        Secp256k1FieldElement.TryFromBytes(ToBytes(value), out Secp256k1FieldElement element) ? element
            : throw new ArgumentOutOfRangeException(nameof(value));

    /// <summary>
    /// Converts a field element to the integer it represents.
    /// </summary>
    /// <param name="element">The field element.</param>
    /// <returns>Returns the integer, in [0, p).</returns>
    private static BigInteger ToInteger(Secp256k1FieldElement element)
    {
        byte[] bytes = new byte[32];
        element.WriteBytes(bytes);
        return new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
    }

    /// <summary>
    /// Writes a non-negative integer less than 2^256 as 32 big-endian bytes.
    /// </summary>
    /// <param name="value">The integer to write.</param>
    /// <returns>Returns the 32-byte big-endian encoding.</returns>
    private static byte[] ToBytes(BigInteger value)
    {
        byte[] result = new byte[32];
        int length = value.GetByteCount(isUnsigned: true);
        value.TryWriteBytes(result.AsSpan(32 - length), out int _, isUnsigned: true, isBigEndian: true);
        return result;
    }
}
