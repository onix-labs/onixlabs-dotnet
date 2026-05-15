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

internal readonly partial struct Ed25519Point
{
    /// <summary>
    /// Encodes the point into 32 little-endian bytes per RFC 8032 §5.1.2 — the y-coordinate's
    /// canonical form, with the most significant bit of byte 31 set to x's sign (LSB of x).
    /// </summary>
    /// <param name="output">The 32-byte destination buffer that receives the canonical encoding.</param>
    public void Encode(Span<byte> output)
    {
        Ed25519FieldElement zInverse = Ed25519FieldElement.Invert(z);
        Ed25519FieldElement xAffine = x * zInverse;
        Ed25519FieldElement yAffine = y * zInverse;

        yAffine.WriteBytes(output);
        if (xAffine.IsNegative) output[31] |= 0x80;
    }

    /// <summary>
    /// Attempts to decode a 32-byte Ed25519 point encoding per RFC 8032 §5.1.3.
    /// </summary>
    /// <param name="encoded">The 32-byte little-endian point encoding.</param>
    /// <param name="point">
    /// When this method returns <see langword="true"/>, contains the decoded curve point;
    /// when this method returns <see langword="false"/>, contains the curve identity.
    /// </param>
    /// <returns>
    /// Returns <see langword="false"/> if the input does not represent a valid point — including
    /// non-canonical y (y ≥ p), no square root of x^2 = (y^2 - 1) / (d*y^2 + 1), or the
    /// sign-bit/zero-x edge case — otherwise, <see langword="true"/>.
    /// </returns>
    public static bool TryDecode(ReadOnlySpan<byte> encoded, out Ed25519Point point)
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

        Ed25519FieldElement yField = Ed25519FieldElement.FromBytes(yBytes);
        Ed25519FieldElement ySquared = Ed25519FieldElement.Square(yField);
        Ed25519FieldElement u = ySquared - Ed25519FieldElement.One;
        Ed25519FieldElement v = (ySquared * D) + Ed25519FieldElement.One;

        // x_candidate = u * v^3 * (u * v^7)^((p-5)/8)
        Ed25519FieldElement v2 = Ed25519FieldElement.Square(v);
        Ed25519FieldElement v3 = v2 * v;
        Ed25519FieldElement v7 = Ed25519FieldElement.Square(v3) * v;
        Ed25519FieldElement uv7 = u * v7;
        Ed25519FieldElement pow = Ed25519FieldElement.PowP58(uv7);
        Ed25519FieldElement xCandidate = (u * v3) * pow;

        Ed25519FieldElement xSquared = Ed25519FieldElement.Square(xCandidate);
        Ed25519FieldElement check = v * xSquared;

        if (check.ValueEquals(u))
        {
            // x_candidate is the correct square root
        }
        else if (check.ValueEquals(-u))
            xCandidate *= SqrtMinusOne;
        else return false;

        // Reject (0, sign=1) as a non-canonical encoding of the identity.
        if (xCandidate.IsZero && signBit == 1) return false;

        if (xCandidate.IsNegative != (signBit == 1))
            xCandidate = -xCandidate;

        point = new Ed25519Point(
            xCandidate,
            yField,
            Ed25519FieldElement.One,
            xCandidate * yField
        );

        return true;
    }

    /// <summary>
    /// Determines whether the specified 32-byte little-endian y-coordinate (with the sign bit
    /// already cleared) is strictly less than the field prime p, that is, in canonical form.
    /// </summary>
    /// <param name="yBytes">The 32-byte little-endian y-coordinate with bit 255 cleared.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="yBytes"/> is strictly less than p; otherwise, <see langword="false"/>.</returns>
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
}
