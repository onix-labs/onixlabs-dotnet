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
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Provides SEC 1 v2 §2.3.3–2.3.4 point compression and decompression for the NIST P-256, P-384 and P-521 curves.
/// </summary>
/// <remarks>
/// <see cref="BigInteger"/> arithmetic is not constant-time, which is acceptable here because the inputs are public keys.
/// </remarks>
internal static class EcPointCompression
{
    private const byte EvenPrefix = 0x02;
    private const byte OddPrefix = 0x03;
    private const string UnsupportedCurve = "Only P-256, P-384 and P-521 public keys can be compressed.";
    private const string InvalidPoint = "The data is not a valid compressed point on the P-256, P-384 or P-521 curve.";
    private const string CurveMismatch = "The compressed point is not on the specified curve.";

    // Why: the field primes and the b coefficients are from SEC 2 v2 §2.4.2, §2.5.1 and §2.6.1; each curve has a = p − 3.
    private static readonly NistCurve[] Curves =
    [
        new(
            Curve: ECCurve.NamedCurves.nistP256,
            Size: 32,
            PrimeHex: "ffffffff00000001000000000000000000000000ffffffffffffffffffffffff",
            BHex: "5ac635d8aa3a93e7b3ebbd55769886bc651d06b0cc53b0f63bce3c3e27d2604b"
        ),
        new(
            Curve: ECCurve.NamedCurves.nistP384,
            Size: 48,
            PrimeHex: "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffeffffffff0000000000000000ffffffff",
            BHex: "b3312fa7e23ee7e4988e056be3f82d19181d9c6efe8141120314088f5013875ac656398d8a2ed19d2a85c8edd3ec2aef"
        ),
        new(
            Curve: ECCurve.NamedCurves.nistP521,
            Size: 66,
            PrimeHex: "01ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff",
            BHex: "0051953eb9618e1c9a1f929a21a0b68540eea2da725b99b315f3b8b489918ef109e156193951ec7e937b1652c0bd3bb1bf073573df883d2c34f1ef451fd46b503f00"
        )
    ];

    /// <summary>
    /// Compresses the public point of the specified elliptic curve parameters.
    /// </summary>
    /// <param name="parameters">The elliptic curve parameters containing the public point.</param>
    /// <returns>Returns the compressed point: the parity prefix, followed by the big-endian x-coordinate.</returns>
    /// <exception cref="CryptographicException">Thrown when the parameters are not for the P-256, P-384 or P-521 curve.</exception>
    internal static byte[] Compress(ECParameters parameters)
    {
        string? oid = parameters.Curve.Oid?.Value;
        NistCurve curve = Curves.FirstOrDefault(candidate => candidate.Oid == oid) ?? throw new CryptographicException(UnsupportedCurve);

        byte[] result = new byte[curve.Size + 1];
        result[0] = (parameters.Q.Y![^1] & 1) == 0 ? EvenPrefix : OddPrefix;
        parameters.Q.X!.CopyTo(result.AsSpan(1));
        return result;
    }

    /// <summary>
    /// Decompresses the specified point, determining the curve from its length.
    /// </summary>
    /// <param name="data">The compressed point.</param>
    /// <returns>Returns elliptic curve parameters containing the named curve and the decompressed public point.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the data has an unsupported length or prefix, an x-coordinate that is not less than the field prime,
    /// or an x-coordinate that is not on the curve.
    /// </exception>
    internal static ECParameters Decompress(ReadOnlySpan<byte> data)
    {
        int length = data.Length;
        NistCurve curve = Curves.FirstOrDefault(candidate => candidate.Size + 1 == length) ?? throw new CryptographicException(InvalidPoint);

        if (data[0] is not (EvenPrefix or OddPrefix)) throw new CryptographicException(InvalidPoint);

        BigInteger x = new(data[1..], isUnsigned: true, isBigEndian: true);

        if (x >= curve.Prime) throw new CryptographicException(InvalidPoint);

        BigInteger alpha = BigInteger.Remainder(x * x * x - 3 * x + curve.B, curve.Prime);
        if (alpha.Sign < 0) alpha += curve.Prime;

        // Why: p ≡ 3 (mod 4) for all three curves, so α^((p+1)/4) is a square root of α whenever one exists; squaring it back detects x values
        // that are not on the curve.
        BigInteger beta = BigInteger.ModPow(alpha, (curve.Prime + 1) / 4, curve.Prime);

        if (BigInteger.ModPow(beta, 2, curve.Prime) != alpha) throw new CryptographicException(InvalidPoint);

        bool isOdd = data[0] == OddPrefix;
        BigInteger y = beta.IsEven != isOdd ? beta : curve.Prime - beta;

        if (y >= curve.Prime) throw new CryptographicException(InvalidPoint);

        byte[] yBytes = new byte[curve.Size];
        y.TryWriteBytes(yBytes.AsSpan(curve.Size - y.GetByteCount(isUnsigned: true)), out int _, isUnsigned: true, isBigEndian: true);

        return new ECParameters
        {
            Curve = curve.Curve,
            Q = new ECPoint { X = data[1..].ToArray(), Y = yBytes }
        };
    }

    /// <summary>
    /// Decompresses the specified point, which must be on the specified curve.
    /// </summary>
    /// <param name="data">The compressed point.</param>
    /// <param name="curve">The named curve that the point must be on.</param>
    /// <returns>Returns elliptic curve parameters containing the named curve and the decompressed public point.</returns>
    /// <exception cref="CryptographicException">Thrown when the data is not a valid compressed point, or is not on the specified curve.</exception>
    internal static ECParameters Decompress(ReadOnlySpan<byte> data, ECCurve curve)
    {
        ECParameters result = Decompress(data);

        if (result.Curve.Oid.Value != curve.Oid?.Value) throw new CryptographicException(CurveMismatch);

        return result;
    }

    /// <summary>
    /// Represents a NIST prime curve and the constants needed to decompress its points.
    /// </summary>
    /// <param name="Curve">The named curve.</param>
    /// <param name="Size">The size of a field element, in bytes.</param>
    /// <param name="PrimeHex">The field prime, as big-endian hexadecimal.</param>
    /// <param name="BHex">The b coefficient of the curve equation, as big-endian hexadecimal.</param>
    private sealed record NistCurve(ECCurve Curve, int Size, string PrimeHex, string BHex)
    {
        /// <summary>
        /// Gets the object identifier of the curve.
        /// </summary>
        /// <value>The dotted-decimal object identifier of the curve.</value>
        public string? Oid => Curve.Oid.Value;

        /// <summary>
        /// Gets the field prime of the curve.
        /// </summary>
        /// <value>The field prime p.</value>
        public BigInteger Prime { get; } = new(Convert.FromHexString(PrimeHex), isUnsigned: true, isBigEndian: true);

        /// <summary>
        /// Gets the b coefficient of the curve equation y² = x³ − 3x + b.
        /// </summary>
        /// <value>The b coefficient.</value>
        public BigInteger B { get; } = new(Convert.FromHexString(BHex), isUnsigned: true, isBigEndian: true);
    }
}
