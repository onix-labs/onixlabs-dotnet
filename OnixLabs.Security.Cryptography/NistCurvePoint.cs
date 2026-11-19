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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a check that a public point lies on one of the NIST prime curves P-256, P-384 or P-521, as SEC 1 v2 §3.2.2.1 requires of
/// public key validation.
/// </summary>
/// <remarks>
/// The curve constants p, a and b are copied from SEC 2 v2: §2.4.2 (secp256r1, P-256), §2.5.1 (secp384r1, P-384) and §2.6.1 (secp521r1,
/// P-521). The check uses <see cref="BigInteger"/>, which is not constant time; that is acceptable because it only ever handles public
/// values.
/// </remarks>
internal static class NistCurvePoint
{
    /// <summary>
    /// The OID of the P-256 curve (secp256r1).
    /// </summary>
    private const string P256Oid = "1.2.840.10045.3.1.7";

    /// <summary>
    /// The OID of the P-384 curve (secp384r1).
    /// </summary>
    private const string P384Oid = "1.3.132.0.34";

    /// <summary>
    /// The OID of the P-521 curve (secp521r1).
    /// </summary>
    private const string P521Oid = "1.3.132.0.35";

    /// <summary>
    /// The P-256 curve constants from SEC 2 v2 §2.4.2.
    /// </summary>
    private static readonly Curve P256 = new(
        "FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF",
        "FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC",
        "5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B");

    /// <summary>
    /// The P-384 curve constants from SEC 2 v2 §2.5.1.
    /// </summary>
    private static readonly Curve P384 = new(
        "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF",
        "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC",
        "B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF");

    /// <summary>
    /// The P-521 curve constants from SEC 2 v2 §2.6.1.
    /// </summary>
    private static readonly Curve P521 = new(
        "01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" +
        "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF",
        "01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" +
        "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC",
        "0051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109" +
        "E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00");

    /// <summary>
    /// Determines whether the public point of the specified parameters lies on its named curve.
    /// </summary>
    /// <param name="parameters">The exported public parameters, whose curve must be P-256, P-384 or P-521.</param>
    /// <returns>
    /// Returns <see langword="true"/> if both coordinates are in [0, p) and satisfy y^2 = x^3 + ax + b (mod p); otherwise,
    /// <see langword="false"/>, including when the curve is not one of P-256, P-384 or P-521.
    /// </returns>
    public static bool IsOnCurve(ECParameters parameters)
    {
        Curve? curve = parameters.Curve.Oid.Value switch
        {
            P256Oid => P256,
            P384Oid => P384,
            P521Oid => P521,
            _ => null
        };

        if (curve is null || parameters.Q.X is null || parameters.Q.Y is null) return false;

        BigInteger x = new(parameters.Q.X, isUnsigned: true, isBigEndian: true);
        BigInteger y = new(parameters.Q.Y, isUnsigned: true, isBigEndian: true);

        return curve.Contains(x, y);
    }

    /// <summary>
    /// Represents the constants of a short Weierstrass curve y^2 = x^3 + ax + b over GF(p).
    /// </summary>
    /// <param name="prime">The field prime p, as big-endian hexadecimal.</param>
    /// <param name="a">The coefficient a, as big-endian hexadecimal.</param>
    /// <param name="b">The coefficient b, as big-endian hexadecimal.</param>
    private sealed class Curve(string prime, string a, string b)
    {
        /// <summary>
        /// The field prime p.
        /// </summary>
        private readonly BigInteger p = Parse(prime);

        /// <summary>
        /// The coefficient a.
        /// </summary>
        private readonly BigInteger a = Parse(a);

        /// <summary>
        /// The coefficient b.
        /// </summary>
        private readonly BigInteger b = Parse(b);

        /// <summary>
        /// Determines whether the specified affine point lies on the current curve.
        /// </summary>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        /// <returns>Returns <see langword="true"/> if both coordinates are in [0, p) and satisfy the curve equation; otherwise,
        /// <see langword="false"/>.</returns>
        public bool Contains(BigInteger x, BigInteger y)
        {
            if (x.Sign < 0 || x >= p || y.Sign < 0 || y >= p) return false;

            BigInteger left = y * y % p;
            BigInteger right = ((x * x % p * x) + (a * x) + b) % p;

            return left == right;
        }

        /// <summary>
        /// Parses big-endian hexadecimal as a non-negative integer.
        /// </summary>
        /// <param name="hex">The big-endian hexadecimal to parse.</param>
        /// <returns>Returns the non-negative integer that <paramref name="hex"/> represents.</returns>
        private static BigInteger Parse(string hex) => new(Convert.FromHexString(hex), isUnsigned: true, isBigEndian: true);
    }
}
