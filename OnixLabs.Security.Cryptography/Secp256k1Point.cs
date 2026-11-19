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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a point on the secp256k1 curve y^2 = x^3 + 7 (SEC 2 v2 §2.4.1), in Jacobian coordinates (X, Y, Z), which represent the affine
/// point (X / Z^2, Y / Z^3). The point at infinity has Z = 0.
/// </summary>
/// <remarks>
/// Why: like <see cref="Secp256k1FieldElement"/>, this type only handles public values, and its operations branch on them; it must not be
/// used with secret scalars.
/// </remarks>
internal readonly struct Secp256k1Point
{
    /// <summary>
    /// The point at infinity, the identity of the group.
    /// </summary>
    public static readonly Secp256k1Point Infinity = new(Secp256k1FieldElement.One, Secp256k1FieldElement.One, Secp256k1FieldElement.Zero);

    /// <summary>
    /// The curve coefficient b = 7.
    /// </summary>
    private static readonly Secp256k1FieldElement B = FromHex("0000000000000000000000000000000000000000000000000000000000000007");

    /// <summary>
    /// The Jacobian X coordinate.
    /// </summary>
    private readonly Secp256k1FieldElement x;

    /// <summary>
    /// The Jacobian Y coordinate.
    /// </summary>
    private readonly Secp256k1FieldElement y;

    /// <summary>
    /// The Jacobian Z coordinate, which is zero for the point at infinity.
    /// </summary>
    private readonly Secp256k1FieldElement z;

    /// <summary>
    /// Initializes a new instance of the <see cref="Secp256k1Point"/> struct.
    /// </summary>
    /// <param name="x">The Jacobian X coordinate.</param>
    /// <param name="y">The Jacobian Y coordinate.</param>
    /// <param name="z">The Jacobian Z coordinate.</param>
    private Secp256k1Point(Secp256k1FieldElement x, Secp256k1FieldElement y, Secp256k1FieldElement z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// Gets a value indicating whether this point is the point at infinity.
    /// </summary>
    /// <value><see langword="true"/> if this point is the point at infinity; otherwise, <see langword="false"/>.</value>
    public bool IsInfinity => z.IsZero;

    /// <summary>
    /// Creates a point from affine coordinates, after checking that it lies on the curve.
    /// </summary>
    /// <param name="affineX">The affine x-coordinate.</param>
    /// <param name="affineY">The affine y-coordinate.</param>
    /// <param name="point">When this method returns, contains the point, if it lies on the curve.</param>
    /// <returns>Returns <see langword="true"/> if (x, y) satisfies y^2 = x^3 + 7; otherwise, <see langword="false"/>.</returns>
    public static bool TryFromAffine(in Secp256k1FieldElement affineX, in Secp256k1FieldElement affineY, out Secp256k1Point point)
    {
        point = Infinity;

        if (Secp256k1FieldElement.Square(affineY) != (Secp256k1FieldElement.Square(affineX) * affineX) + B) return false;

        point = new Secp256k1Point(affineX, affineY, Secp256k1FieldElement.One);
        return true;
    }

    /// <summary>
    /// Attempts to decode a point from its SEC 1 v2 §2.3.3 encoding, uncompressed (<c>04 ‖ X ‖ Y</c>) or compressed (<c>02 ‖ X</c> or
    /// <c>03 ‖ X</c>), per §2.3.4.
    /// </summary>
    /// <param name="encoded">The encoded point.</param>
    /// <param name="point">When this method returns, contains the decoded point, if the encoding is valid.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the encoding is well formed, its coordinates are in [0, p), and the point lies on the curve;
    /// otherwise, <see langword="false"/>. The point at infinity (<c>00</c>) is not accepted.
    /// </returns>
    public static bool TryDecode(ReadOnlySpan<byte> encoded, out Secp256k1Point point)
    {
        point = Infinity;

        switch (encoded.Length)
        {
            case 65 when encoded[0] == 0x04:
                return Secp256k1FieldElement.TryFromBytes(encoded[1..33], out Secp256k1FieldElement x)
                       && Secp256k1FieldElement.TryFromBytes(encoded[33..], out Secp256k1FieldElement y)
                       && TryFromAffine(x, y, out point);

            case 33 when encoded[0] is 0x02 or 0x03:
                if (!Secp256k1FieldElement.TryFromBytes(encoded[1..], out Secp256k1FieldElement compressedX)) return false;

                Secp256k1FieldElement alpha = (Secp256k1FieldElement.Square(compressedX) * compressedX) + B;
                if (!Secp256k1FieldElement.TrySquareRoot(alpha, out Secp256k1FieldElement beta)) return false;

                bool wantOdd = encoded[0] == 0x03;
                Secp256k1FieldElement compressedY = beta.IsOdd == wantOdd ? beta : Secp256k1FieldElement.Zero - beta;
                return TryFromAffine(compressedX, compressedY, out point);

            default:
                return false;
        }
    }

    /// <summary>
    /// Computes the sum of the specified points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>Returns the sum of the specified points.</returns>
    /// <remarks>
    /// The general Jacobian addition formulas, falling back to <see cref="Double"/> when the points are equal, and returning the point at
    /// infinity when they are inverses.
    /// </remarks>
    public static Secp256k1Point operator +(in Secp256k1Point left, in Secp256k1Point right)
    {
        if (left.IsInfinity) return right;
        if (right.IsInfinity) return left;

        Secp256k1FieldElement z1Squared = Secp256k1FieldElement.Square(left.z);
        Secp256k1FieldElement z2Squared = Secp256k1FieldElement.Square(right.z);
        Secp256k1FieldElement u1 = left.x * z2Squared;
        Secp256k1FieldElement u2 = right.x * z1Squared;
        Secp256k1FieldElement s1 = left.y * z2Squared * right.z;
        Secp256k1FieldElement s2 = right.y * z1Squared * left.z;

        if (u1 == u2) return s1 == s2 ? Double(left) : Infinity;

        Secp256k1FieldElement h = u2 - u1;
        Secp256k1FieldElement r = s2 - s1;
        Secp256k1FieldElement hSquared = Secp256k1FieldElement.Square(h);
        Secp256k1FieldElement hCubed = hSquared * h;
        Secp256k1FieldElement u1HSquared = u1 * hSquared;

        Secp256k1FieldElement x3 = Secp256k1FieldElement.Square(r) - hCubed - u1HSquared - u1HSquared;
        Secp256k1FieldElement y3 = (r * (u1HSquared - x3)) - (s1 * hCubed);
        Secp256k1FieldElement z3 = h * left.z * right.z;

        return new Secp256k1Point(x3, y3, z3);
    }

    /// <summary>
    /// Computes twice the specified point, with the Jacobian doubling formulas for curves with a = 0.
    /// </summary>
    /// <param name="point">The point to double.</param>
    /// <returns>Returns twice the specified point.</returns>
    public static Secp256k1Point Double(in Secp256k1Point point)
    {
        if (point.IsInfinity || point.y.IsZero) return Infinity;

        Secp256k1FieldElement a = Secp256k1FieldElement.Square(point.x);
        Secp256k1FieldElement b = Secp256k1FieldElement.Square(point.y);
        Secp256k1FieldElement c = Secp256k1FieldElement.Square(b);
        Secp256k1FieldElement xPlusB = point.x + b;
        Secp256k1FieldElement d = Secp256k1FieldElement.Square(xPlusB) - a - c;
        d += d;
        Secp256k1FieldElement e = a + a + a;
        Secp256k1FieldElement f = Secp256k1FieldElement.Square(e);
        Secp256k1FieldElement eightC = c + c;
        eightC += eightC;
        eightC += eightC;

        Secp256k1FieldElement x3 = f - d - d;
        Secp256k1FieldElement y3 = (e * (d - x3)) - eightC;
        Secp256k1FieldElement z3 = point.y * point.z;
        z3 += z3;

        return new Secp256k1Point(x3, y3, z3);
    }

    /// <summary>
    /// Gets the affine x-coordinate of this point, X / Z^2.
    /// </summary>
    /// <returns>Returns the affine x-coordinate of this point.</returns>
    /// <exception cref="InvalidOperationException">Thrown when this point is the point at infinity, which has no affine
    /// coordinates.</exception>
    public Secp256k1FieldElement GetAffineX()
    {
        if (IsInfinity) throw new InvalidOperationException("The point at infinity has no affine coordinates.");

        return z == Secp256k1FieldElement.One ? x : x * Secp256k1FieldElement.Invert(Secp256k1FieldElement.Square(z));
    }

    /// <summary>
    /// Gets the affine y-coordinate of this point, Y / Z^3.
    /// </summary>
    /// <returns>Returns the affine y-coordinate of this point.</returns>
    /// <exception cref="InvalidOperationException">Thrown when this point is the point at infinity, which has no affine
    /// coordinates.</exception>
    public Secp256k1FieldElement GetAffineY()
    {
        if (IsInfinity) throw new InvalidOperationException("The point at infinity has no affine coordinates.");

        return z == Secp256k1FieldElement.One ? y : y * Secp256k1FieldElement.Invert(Secp256k1FieldElement.Square(z) * z);
    }

    /// <summary>
    /// Determines whether the affine x-coordinate of this point equals the specified field element, without computing the affine
    /// coordinate.
    /// </summary>
    /// <param name="affineX">The field element to compare with.</param>
    /// <returns>
    /// Returns <see langword="true"/> if X / Z^2 = <paramref name="affineX"/>; otherwise, <see langword="false"/>, including for the point
    /// at infinity.
    /// </returns>
    /// <remarks>Why: X / Z^2 = x exactly when X = x · Z^2, which needs two multiplications instead of a field inversion.</remarks>
    public bool HasAffineX(in Secp256k1FieldElement affineX) => !IsInfinity && x == affineX * Secp256k1FieldElement.Square(z);

    /// <summary>
    /// Decodes a field element from 64 hexadecimal digits.
    /// </summary>
    /// <param name="hex">The 64 big-endian hexadecimal digits.</param>
    /// <returns>Returns the decoded field element.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="hex"/> does not represent a field element.</exception>
    internal static Secp256k1FieldElement FromHex(string hex) =>
        Secp256k1FieldElement.TryFromBytes(Convert.FromHexString(hex), out Secp256k1FieldElement element)
            ? element
            : throw new ArgumentException("The value is not a field element.", nameof(hex));
}
