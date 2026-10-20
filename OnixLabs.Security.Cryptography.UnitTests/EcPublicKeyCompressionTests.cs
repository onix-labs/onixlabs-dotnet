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
using System.Numerics;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcPublicKeyCompressionTests
{
    private const int KeysPerCurve = 32;

    [Theory(DisplayName = "EcdsaPublicKey should round-trip random keys through compressed points, covering both parities")]
    [InlineData("nistP256", 33)]
    [InlineData("nistP384", 49)]
    [InlineData("nistP521", 67)]
    public void EcdsaPublicKeyShouldRoundTripRandomKeysThroughCompressedPoints(string curveName, int expectedLength) =>
        AssertRoundTripsRandomKeys<EcdsaPublicKey>(curveName, expectedLength, curve => ECDsa.Create(curve));

    [Theory(DisplayName = "EcdhPublicKey should round-trip random keys through compressed points, covering both parities")]
    [InlineData("nistP256", 33)]
    [InlineData("nistP384", 49)]
    [InlineData("nistP521", 67)]
    public void EcdhPublicKeyShouldRoundTripRandomKeysThroughCompressedPoints(string curveName, int expectedLength) =>
        AssertRoundTripsRandomKeys<EcdhPublicKey>(curveName, expectedLength, curve => ECDiffieHellman.Create(curve));

    [Theory(DisplayName = "ImportCompressed with a curve should accept a point on that curve and reject one on another")]
    [InlineData("nistP256", "nistP384")]
    [InlineData("nistP384", "nistP521")]
    [InlineData("nistP521", "nistP256")]
    public void ImportCompressedWithACurveShouldAcceptAMatchingPointAndRejectAnother(string curveName, string otherCurveName)
    {
        // Given
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        ECCurve otherCurve = ECCurve.CreateFromFriendlyName(otherCurveName);
        using ECDsa algorithm = ECDsa.Create(curve);
        byte[] compressed = EcdsaPublicKey.Import(algorithm.ExportSubjectPublicKeyInfo()).ExportCompressed();

        // When
        EcdsaPublicKey ecdsa = EcdsaPublicKey.ImportCompressed(compressed, curve);
        EcdhPublicKey ecdh = EcdhPublicKey.ImportCompressed(compressed, curve);

        // Then
        Assert.Equal(compressed, ecdsa.ExportCompressed());
        Assert.Equal(compressed, ecdh.ExportCompressed());
        Assert.Throws<CryptographicException>(() => EcdsaPublicKey.ImportCompressed(compressed, otherCurve));
        Assert.Throws<CryptographicException>(() => EcdhPublicKey.ImportCompressed(compressed, otherCurve));
    }

    [Theory(DisplayName = "ImportCompressed should reject prefixes other than 0x02 and 0x03")]
    [InlineData("nistP256", 0x00)]
    [InlineData("nistP256", 0x01)]
    [InlineData("nistP256", 0x04)]
    [InlineData("nistP256", 0x05)]
    [InlineData("nistP384", 0x04)]
    [InlineData("nistP521", 0x00)]
    public void ImportCompressedShouldRejectPrefixesOtherThan02And03(string curveName, byte prefix)
    {
        // Given
        using ECDsa algorithm = ECDsa.Create(ECCurve.CreateFromFriendlyName(curveName));
        byte[] compressed = EcdsaPublicKey.Import(algorithm.ExportSubjectPublicKeyInfo()).ExportCompressed();
        compressed[0] = prefix;

        // Then
        AssertRejected(compressed);
    }

    [Theory(DisplayName = "ImportCompressed should reject lengths that are not 33, 49 or 67 bytes")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(32)]
    [InlineData(34)]
    [InlineData(65)]
    [InlineData(66)]
    [InlineData(68)]
    public void ImportCompressedShouldRejectUnsupportedLengths(int length)
    {
        // Given
        byte[] data = new byte[length];
        if (length > 0) data[0] = 0x02;

        // Then
        AssertRejected(data);
    }

    [Theory(DisplayName = "ImportCompressed should reject an x-coordinate that is not less than the field prime")]
    [InlineData("nistP256", 0)]
    [InlineData("nistP256", 1)]
    [InlineData("nistP384", 0)]
    [InlineData("nistP521", 0)]
    public void ImportCompressedShouldRejectAnXCoordinateNotLessThanTheFieldPrime(string curveName, int offset)
    {
        // Given
        Curve curve = Curve.FromName(curveName);

        // Then
        AssertRejected(Compress(0x02, curve.Prime + offset, curve.Size));
        AssertRejected(Compress(0x03, curve.Prime + offset, curve.Size));
    }

    [Theory(DisplayName = "ImportCompressed should reject an x-coordinate that is not on the curve")]
    [InlineData("nistP256")]
    [InlineData("nistP384")]
    [InlineData("nistP521")]
    public void ImportCompressedShouldRejectAnXCoordinateThatIsNotOnTheCurve(string curveName)
    {
        // Given
        Curve curve = Curve.FromName(curveName);
        BigInteger x = curve.FirstXNotOnCurve();

        // Then
        AssertRejected(Compress(0x02, x, curve.Size));
        AssertRejected(Compress(0x03, x, curve.Size));
    }

    /// <summary>
    /// Asserts that random keys round-trip through compressed points, and that both y-coordinate parities occur.
    /// </summary>
    /// <param name="curveName">The friendly name of the curve.</param>
    /// <param name="expectedLength">The expected length of a compressed point.</param>
    /// <param name="createAlgorithm">Creates a BCL algorithm with a new random key on the specified curve.</param>
    /// <typeparam name="T">The public key type under test.</typeparam>
    private static void AssertRoundTripsRandomKeys<T>(string curveName, int expectedLength, Func<ECCurve, ECAlgorithm> createAlgorithm)
        where T : PublicKey, IPublicKeyRawImportable<T>, IPublicKeyRawExportable, IPublicKeyCompressedImportable<T>, IPublicKeyCompressedExportable
    {
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        bool sawEven = false;
        bool sawOdd = false;

        for (int index = 0; index < KeysPerCurve; index++)
        {
            // Given
            using ECAlgorithm algorithm = createAlgorithm(curve);
            byte[] subjectPublicKeyInfo = algorithm.ExportSubjectPublicKeyInfo();
            ECPoint point = algorithm.ExportParameters(false).Q;
            byte expectedPrefix = (point.Y![^1] & 1) == 0 ? (byte)0x02 : (byte)0x03;

            // When
            byte[] compressed = T.Import(subjectPublicKeyInfo).ExportCompressed();
            T imported = T.ImportCompressed(compressed);

            // Then
            Assert.Equal(expectedLength, compressed.Length);
            Assert.Equal(expectedPrefix, compressed[0]);
            Assert.Equal(point.X, compressed[1..]);
            Assert.Equal(subjectPublicKeyInfo, imported.Export());

            sawEven |= expectedPrefix == 0x02;
            sawOdd |= expectedPrefix == 0x03;
        }

        Assert.True(sawEven && sawOdd, "Both y-coordinate parities should occur across the random keys.");
    }

    /// <summary>
    /// Asserts that both EC public key types reject the specified compressed point.
    /// </summary>
    /// <param name="data">The compressed point that should be rejected.</param>
    private static void AssertRejected(byte[] data)
    {
        Assert.Throws<CryptographicException>(() => EcdsaPublicKey.ImportCompressed(data));
        Assert.Throws<CryptographicException>(() => EcdhPublicKey.ImportCompressed(data));
    }

    /// <summary>
    /// Builds a compressed point from the specified prefix and x-coordinate.
    /// </summary>
    /// <param name="prefix">The prefix byte.</param>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="size">The size of a field element, in bytes.</param>
    /// <returns>Returns the prefix followed by the big-endian x-coordinate, truncated or zero-padded to the field size.</returns>
    private static byte[] Compress(byte prefix, BigInteger x, int size)
    {
        byte[] xBytes = x.ToByteArray(isUnsigned: true, isBigEndian: true);
        byte[] result = new byte[size + 1];
        result[0] = prefix;
        xBytes.AsSpan(Math.Max(0, xBytes.Length - size)).CopyTo(result.AsSpan(1 + Math.Max(0, size - xBytes.Length)));
        return result;
    }

    /// <summary>
    /// Represents the constants of a NIST prime curve, from SEC 2 v2, used to build invalid points independently of the implementation.
    /// </summary>
    /// <param name="Size">The size of a field element, in bytes.</param>
    /// <param name="Prime">The field prime.</param>
    /// <param name="B">The b coefficient of y² = x³ − 3x + b.</param>
    private sealed record Curve(int Size, BigInteger Prime, BigInteger B)
    {
        /// <summary>
        /// Gets the constants of the curve with the specified friendly name.
        /// </summary>
        /// <param name="name">The friendly name of the curve.</param>
        /// <returns>Returns the constants of the curve.</returns>
        public static Curve FromName(string name) => name switch
        {
            "nistP256" => new(32, Hex("ffffffff00000001000000000000000000000000ffffffffffffffffffffffff"),
                Hex("5ac635d8aa3a93e7b3ebbd55769886bc651d06b0cc53b0f63bce3c3e27d2604b")),
            "nistP384" => new(48, Hex("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffeffffffff0000000000000000ffffffff"),
                Hex("b3312fa7e23ee7e4988e056be3f82d19181d9c6efe8141120314088f5013875ac656398d8a2ed19d2a85c8edd3ec2aef")),
            "nistP521" => new(66, BigInteger.Pow(2, 521) - 1,
                Hex("0051953eb9618e1c9a1f929a21a0b68540eea2da725b99b315f3b8b489918ef109e156193951ec7e937b1652c0bd3bb1bf073573df883d2c34f1ef451fd46b503f00")),
            _ => throw new ArgumentOutOfRangeException(nameof(name))
        };

        /// <summary>
        /// Finds the smallest positive x-coordinate that is not on the curve, using Euler's criterion.
        /// </summary>
        /// <returns>Returns the smallest positive x for which x³ − 3x + b is not a quadratic residue modulo the field prime.</returns>
        public BigInteger FirstXNotOnCurve()
        {
            for (BigInteger x = 1; ; x++)
            {
                BigInteger alpha = ((x * x * x - 3 * x + B) % Prime + Prime) % Prime;
                if (BigInteger.ModPow(alpha, (Prime - 1) / 2, Prime) == Prime - 1) return x;
            }
        }

        /// <summary>
        /// Parses a big-endian hexadecimal string as an unsigned integer.
        /// </summary>
        /// <param name="value">The hexadecimal string.</param>
        /// <returns>Returns the parsed unsigned integer.</returns>
        private static BigInteger Hex(string value) => new(Convert.FromHexString(value), isUnsigned: true, isBigEndian: true);
    }
}
