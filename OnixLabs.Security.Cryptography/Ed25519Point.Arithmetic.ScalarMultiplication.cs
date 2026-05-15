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
    /// Computes the scalar multiple of the specified <see cref="Ed25519Point"/> by the specified scalar.
    /// </summary>
    /// <param name="scalar">The 32-byte little-endian scalar multiplier.</param>
    /// <param name="point">The curve point to multiply.</param>
    /// <returns>Returns the curve point representing [<paramref name="scalar"/>] * <paramref name="point"/>.</returns>
    public static Ed25519Point operator *(ReadOnlySpan<byte> scalar, in Ed25519Point point) => ScalarMultiply(scalar, point);

    /// <summary>
    /// Performs constant-time scalar multiplication: result = [scalar] * point. Implemented as a
    /// Montgomery-style ladder over the 256-bit scalar so that every iteration performs exactly
    /// one <see cref="Add"/> and one <see cref="Double"/> on the same projective representations
    /// regardless of bit value. Conditional swap is masked, so neither the control flow nor the
    /// per-iteration memory access pattern reveals any scalar bit. The HWCD Add/Double formulas
    /// in this struct are unified (no special cases for P + Q = identity), making the ladder safe
    /// even when the scalar's leading bits put R0 = identity.
    /// </summary>
    /// <param name="scalar">The 32-byte little-endian scalar multiplier.</param>
    /// <param name="point">The curve point to multiply.</param>
    /// <returns>Returns the curve point representing [<paramref name="scalar"/>] * <paramref name="point"/>.</returns>
    private static Ed25519Point ScalarMultiply(ReadOnlySpan<byte> scalar, in Ed25519Point point)
    {
        // Joye ladder with cswap. Invariant after each step: R0 = [k']P where k' is the prefix
        // of scalar processed so far (MSB → LSB), R1 = R0 + P.
        Ed25519Point r0 = Identity;
        Ed25519Point r1 = point;

        for (int i = 255; i >= 0; i--)
        {
            ulong bit = (ulong)((scalar[i >> 3] >> (i & 7)) & 1);
            ConditionalSwap(ref r0, ref r1, bit);
            r1 = r0 + r1;
            r0 = Double(r0);
            ConditionalSwap(ref r0, ref r1, bit);
        }

        return r0;
    }

    /// <summary>
    /// Performs a branch-free conditional swap. If <paramref name="condition"/> is 1 the inputs are exchanged;
    /// if 0 they pass through unchanged. The same memory writes happen in both cases, so the
    /// observable instruction trace does not depend on <paramref name="condition"/>.
    /// </summary>
    /// <param name="a">The first curve point that is potentially swapped with <paramref name="b"/>.</param>
    /// <param name="b">The second curve point that is potentially swapped with <paramref name="a"/>.</param>
    /// <param name="condition">The swap condition; zero leaves the inputs unchanged, non-zero exchanges them.</param>
    private static void ConditionalSwap(ref Ed25519Point a, ref Ed25519Point b, ulong condition)
    {
        Ed25519FieldElement newAx = Ed25519FieldElement.ConditionalSelect(a.x, b.x, condition);
        Ed25519FieldElement newBx = Ed25519FieldElement.ConditionalSelect(b.x, a.x, condition);
        Ed25519FieldElement newAy = Ed25519FieldElement.ConditionalSelect(a.y, b.y, condition);
        Ed25519FieldElement newBy = Ed25519FieldElement.ConditionalSelect(b.y, a.y, condition);
        Ed25519FieldElement newAz = Ed25519FieldElement.ConditionalSelect(a.z, b.z, condition);
        Ed25519FieldElement newBz = Ed25519FieldElement.ConditionalSelect(b.z, a.z, condition);
        Ed25519FieldElement newAt = Ed25519FieldElement.ConditionalSelect(a.t, b.t, condition);
        Ed25519FieldElement newBt = Ed25519FieldElement.ConditionalSelect(b.t, a.t, condition);

        a = new Ed25519Point(newAx, newAy, newAz, newAt);
        b = new Ed25519Point(newBx, newBy, newBz, newBt);
    }
}
