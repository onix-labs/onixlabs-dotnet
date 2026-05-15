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
/// A point on the twisted Edwards curve -x^2 + y^2 = 1 + d*x^2*y^2 in extended coordinates
/// (X : Y : Z : T) where x = X/Z, y = Y/Z, T = XY/Z. The curve is the one used by Ed25519
/// per RFC 8032 §5.1.
/// </summary>
internal readonly struct Edwards25519Point
{
    private readonly Edwards25519FieldElement x;
    private readonly Edwards25519FieldElement y;
    private readonly Edwards25519FieldElement z;
    private readonly Edwards25519FieldElement t;

    // Curve constant d = -121665 / 121666 mod p, little-endian canonical encoding.
    // Declared before BasePoint because the base-point initializer references this via TryDecode.
    private static readonly Edwards25519FieldElement D = Edwards25519FieldElement.FromBytes([
        0xa3, 0x78, 0x59, 0x13, 0xca, 0x4d, 0xeb, 0x75,
        0xab, 0xd8, 0x41, 0x41, 0x4d, 0x0a, 0x70, 0x00,
        0x98, 0xe8, 0x79, 0x77, 0x79, 0x40, 0xc7, 0x8c,
        0x73, 0xfe, 0x6f, 0x2b, 0xee, 0x6c, 0x03, 0x52,
    ]);

    // 2 * d, used in the point addition formulas.
    private static readonly Edwards25519FieldElement TwoD = Edwards25519FieldElement.Add(D, D);

    // sqrt(-1) mod p = 2^((p-1)/4) mod p, used during point decoding.
    private static readonly Edwards25519FieldElement SqrtMinusOne = Edwards25519FieldElement.FromBytes([
        0xb0, 0xa0, 0x0e, 0x4a, 0x27, 0x1b, 0xee, 0xc4,
        0x78, 0xe4, 0x2f, 0xad, 0x06, 0x18, 0x43, 0x2f,
        0xa7, 0xd7, 0xfb, 0x3d, 0x99, 0x00, 0x4d, 0x2b,
        0x0b, 0xdf, 0xc1, 0x4f, 0x80, 0x24, 0x83, 0x2b,
    ]);

    /// <summary>
    /// The neutral element of the curve group, affine (0, 1).
    /// </summary>
    private static readonly Edwards25519Point Identity = new(
        Edwards25519FieldElement.Zero,
        Edwards25519FieldElement.One,
        Edwards25519FieldElement.One,
        Edwards25519FieldElement.Zero
    );

    /// <summary>
    /// The standard Ed25519 base point B per RFC 8032 §5.1. Decoded from its 32-byte canonical
    /// encoding, so we do not maintain a duplicate set of 51-bit limb constants. Must be declared
    /// after D / SqrtMinusOne so those are initialized first.
    /// </summary>
    public static readonly Edwards25519Point BasePoint = DecodeBasePoint();

    private Edwards25519Point(in Edwards25519FieldElement x, in Edwards25519FieldElement y, in Edwards25519FieldElement z, in Edwards25519FieldElement t)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.t = t;
    }

    /// <summary>
    /// Adds two Edwards points using the standard twisted-Edwards "add-2008-hwcd" formulas
    /// specialized for a = -1 (Hisil-Wong-Carter-Dawson).
    /// </summary>
    public static Edwards25519Point Add(in Edwards25519Point p1, in Edwards25519Point p2)
    {
        Edwards25519FieldElement a = Edwards25519FieldElement.Mul(
            Edwards25519FieldElement.Sub(p1.y, p1.x),
            Edwards25519FieldElement.Sub(p2.y, p2.x)
        );

        Edwards25519FieldElement b = Edwards25519FieldElement.Mul(
            Edwards25519FieldElement.Add(p1.y, p1.x),
            Edwards25519FieldElement.Add(p2.y, p2.x)
        );

        Edwards25519FieldElement c = Edwards25519FieldElement.Mul(
            Edwards25519FieldElement.Mul(p1.t, TwoD),
            p2.t
        );

        Edwards25519FieldElement zProduct = Edwards25519FieldElement.Mul(p1.z, p2.z);
        Edwards25519FieldElement d = Edwards25519FieldElement.Add(zProduct, zProduct);

        Edwards25519FieldElement e = Edwards25519FieldElement.Sub(b, a);
        Edwards25519FieldElement f = Edwards25519FieldElement.Sub(d, c);
        Edwards25519FieldElement g = Edwards25519FieldElement.Add(d, c);
        Edwards25519FieldElement h = Edwards25519FieldElement.Add(b, a);

        return new Edwards25519Point(
            Edwards25519FieldElement.Mul(e, f),
            Edwards25519FieldElement.Mul(g, h),
            Edwards25519FieldElement.Mul(f, g),
            Edwards25519FieldElement.Mul(e, h)
        );
    }

    /// <summary>
    /// Doubles an Edwards point using "dbl-2008-hwcd" for a = -1. Cheaper than Add(p, p) because
    /// the four input multiplications collapse to squarings and the curve constant d is not used.
    /// </summary>
    public static Edwards25519Point Double(in Edwards25519Point p)
    {
        Edwards25519FieldElement a = Edwards25519FieldElement.Square(p.x); // A = X^2
        Edwards25519FieldElement b = Edwards25519FieldElement.Square(p.y); // B = Y^2
        Edwards25519FieldElement zSquared = Edwards25519FieldElement.Square(p.z);
        Edwards25519FieldElement c = Edwards25519FieldElement.Add(zSquared, zSquared); // C = 2*Z^2

        Edwards25519FieldElement xPlusYSquared = Edwards25519FieldElement.Square(
            Edwards25519FieldElement.Add(p.x, p.y));

        Edwards25519FieldElement e = Edwards25519FieldElement.Sub(xPlusYSquared,
            Edwards25519FieldElement.Add(a, b)); // E = (X+Y)^2 - A - B
        Edwards25519FieldElement g = Edwards25519FieldElement.Sub(b, a); // G = B - A   (a = -1)
        Edwards25519FieldElement f = Edwards25519FieldElement.Sub(g, c); // F = G - C
        Edwards25519FieldElement h = Edwards25519FieldElement.Negate(
            Edwards25519FieldElement.Add(a, b)); // H = -(A + B)

        return new Edwards25519Point(
            Edwards25519FieldElement.Mul(e, f),
            Edwards25519FieldElement.Mul(g, h),
            Edwards25519FieldElement.Mul(f, g),
            Edwards25519FieldElement.Mul(e, h));
    }

    /// <summary>
    /// Negates the point. For twisted Edwards with a = -1, -P = (-X : Y : Z : -T).
    /// </summary>
    public static Edwards25519Point Negate(in Edwards25519Point p) =>
        new(Edwards25519FieldElement.Negate(p.x), p.y, p.z, Edwards25519FieldElement.Negate(p.t));

    /// <summary>
    /// Branch-free conditional swap. If <paramref name="condition"/> is 1 the inputs are exchanged;
    /// if 0 they pass through unchanged. The same memory writes happen in both cases, so the
    /// observable instruction trace does not depend on <paramref name="condition"/>.
    /// </summary>
    public static void ConditionalSwap(ref Edwards25519Point a, ref Edwards25519Point b, ulong condition)
    {
        Edwards25519FieldElement newAx = Edwards25519FieldElement.ConditionalSelect(a.x, b.x, condition);
        Edwards25519FieldElement newBx = Edwards25519FieldElement.ConditionalSelect(b.x, a.x, condition);
        Edwards25519FieldElement newAy = Edwards25519FieldElement.ConditionalSelect(a.y, b.y, condition);
        Edwards25519FieldElement newBy = Edwards25519FieldElement.ConditionalSelect(b.y, a.y, condition);
        Edwards25519FieldElement newAz = Edwards25519FieldElement.ConditionalSelect(a.z, b.z, condition);
        Edwards25519FieldElement newBz = Edwards25519FieldElement.ConditionalSelect(b.z, a.z, condition);
        Edwards25519FieldElement newAt = Edwards25519FieldElement.ConditionalSelect(a.t, b.t, condition);
        Edwards25519FieldElement newBt = Edwards25519FieldElement.ConditionalSelect(b.t, a.t, condition);
        a = new Edwards25519Point(newAx, newAy, newAz, newAt);
        b = new Edwards25519Point(newBx, newBy, newBz, newBt);
    }

    /// <summary>
    /// Constant-time scalar multiplication: result = [scalar] * point. Implemented as a
    /// Montgomery-style ladder over the 256-bit scalar so that every iteration performs exactly
    /// one Add and one Double on the same projective representations regardless of bit value.
    /// Conditional swap is masked, so neither the control flow nor the per-iteration memory
    /// access pattern reveals any scalar bit. The HWCD Add/Double formulas in this struct are
    /// unified (no special cases for P + Q = identity), making the ladder safe even when the
    /// scalar's leading bits put R0 = identity.
    /// </summary>
    public static Edwards25519Point ScalarMultiply(ReadOnlySpan<byte> scalar, in Edwards25519Point point)
    {
        // Joye ladder with cswap. Invariant after each step: R0 = [k']P where k' is the prefix
        // of scalar processed so far (MSB → LSB), R1 = R0 + P.
        Edwards25519Point r0 = Identity;
        Edwards25519Point r1 = point;

        for (int i = 255; i >= 0; i--)
        {
            ulong bit = (ulong)((scalar[i >> 3] >> (i & 7)) & 1);
            ConditionalSwap(ref r0, ref r1, bit);
            r1 = Add(r0, r1);
            r0 = Double(r0);
            ConditionalSwap(ref r0, ref r1, bit);
        }

        return r0;
    }

    /// <summary>
    /// Encodes the point into 32 little-endian bytes per RFC 8032 §5.1.2 — the y-coordinate's
    /// canonical form, with the most significant bit of byte 31 set to x's sign (LSB of x).
    /// </summary>
    public void Encode(Span<byte> output)
    {
        Edwards25519FieldElement zInverse = Edwards25519FieldElement.Invert(z);
        Edwards25519FieldElement xAffine = Edwards25519FieldElement.Mul(x, zInverse);
        Edwards25519FieldElement yAffine = Edwards25519FieldElement.Mul(y, zInverse);

        yAffine.ToBytes(output);
        if (xAffine.IsNegative) output[31] |= 0x80;
    }

    /// <summary>
    /// Decodes a 32-byte Ed25519 point encoding per RFC 8032 §5.1.3. Returns false if the input
    /// does not represent a valid point, including non-canonical y (y ≥ p), no square-root of
    /// x^2 = (y^2 - 1) / (d*y^2 + 1), or the sign-bit/zero-x edge case.
    /// </summary>
    public static bool TryDecode(ReadOnlySpan<byte> encoded, out Edwards25519Point point)
    {
        point = Identity;
        if (encoded.Length != 32) return false;

        int signBit = (encoded[31] & 0x80) >> 7;

        Span<byte> yBytes = stackalloc byte[32];
        encoded.CopyTo(yBytes);
        yBytes[31] &= 0x7F;

        // Reject non-canonical y (y ≥ p). With the sign bit masked off the input is in [0, 2^255);
        // any value in [p, 2^255) — that is, [p, p+18] — is non-canonical and must be rejected.
        if (!IsCanonicalY(yBytes)) return false;

        Edwards25519FieldElement yField = Edwards25519FieldElement.FromBytes(yBytes);
        Edwards25519FieldElement ySquared = Edwards25519FieldElement.Square(yField);
        Edwards25519FieldElement u = Edwards25519FieldElement.Sub(ySquared, Edwards25519FieldElement.One);
        Edwards25519FieldElement v = Edwards25519FieldElement.Add(Edwards25519FieldElement.Mul(ySquared, D), Edwards25519FieldElement.One);

        // x_candidate = u * v^3 * (u * v^7)^((p-5)/8)
        Edwards25519FieldElement v2 = Edwards25519FieldElement.Square(v);
        Edwards25519FieldElement v3 = Edwards25519FieldElement.Mul(v2, v);
        Edwards25519FieldElement v7 = Edwards25519FieldElement.Mul(Edwards25519FieldElement.Square(v3), v);
        Edwards25519FieldElement uv7 = Edwards25519FieldElement.Mul(u, v7);
        Edwards25519FieldElement pow = Edwards25519FieldElement.PowP58(uv7);
        Edwards25519FieldElement xCandidate = Edwards25519FieldElement.Mul(Edwards25519FieldElement.Mul(u, v3), pow);

        Edwards25519FieldElement xSquared = Edwards25519FieldElement.Square(xCandidate);
        Edwards25519FieldElement check = Edwards25519FieldElement.Mul(v, xSquared);

        if (check.ValueEquals(u))
        {
            // x_candidate is the correct square root
        }
        else if (check.ValueEquals(Edwards25519FieldElement.Negate(u)))
        {
            xCandidate = Edwards25519FieldElement.Mul(xCandidate, SqrtMinusOne);
        }
        else
        {
            return false;
        }

        // Reject (0, sign=1) as a non-canonical encoding of the identity.
        if (xCandidate.IsZero && signBit == 1) return false;

        if (xCandidate.IsNegative != (signBit == 1))
        {
            xCandidate = Edwards25519FieldElement.Negate(xCandidate);
        }

        point = new Edwards25519Point(
            xCandidate,
            yField,
            Edwards25519FieldElement.One,
            Edwards25519FieldElement.Mul(xCandidate, yField));
        return true;
    }

    /// <summary>
    /// Compares two points for equality on the curve. The internal projective representations
    /// may differ even when the affine points coincide; this method canonicalises by encoding.
    /// </summary>
    public bool EncodingEquals(in Edwards25519Point other)
    {
        Span<byte> a = stackalloc byte[32];
        Span<byte> b = stackalloc byte[32];
        Encode(a);
        other.Encode(b);
        return a.SequenceEqual(b);
    }

    private static bool IsCanonicalY(ReadOnlySpan<byte> yBytes)
    {
        // p = 2^255 - 19. yBytes with top bit cleared is in [0, 2^255). Reject if y >= p,
        // i.e. yBytes >= [0xed, 0xff, ..., 0xff, 0x7f] in little-endian.
        if (yBytes[31] < 0x7F) return true;
        if (yBytes[31] > 0x7F) return false;
        for (int i = 30; i >= 1; i--)
        {
            if (yBytes[i] < 0xFF) return true;
            if (yBytes[i] > 0xFF) return false;
        }

        return yBytes[0] < 0xED;
    }

    private static Edwards25519Point DecodeBasePoint()
    {
        // Encoded base point B per RFC 8032 §5.1: y = 4/5 mod p (little-endian 0x58 followed by
        // 31 bytes of 0x66), sign bit 0.
        ReadOnlySpan<byte> encoded =
        [
            0x58, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
        ];
        if (!TryDecode(encoded, out Edwards25519Point b))
        {
            throw new InvalidOperationException("Ed25519 base point failed to decode (implementation bug).");
        }

        return b;
    }
}
