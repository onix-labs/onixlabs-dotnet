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
using System.Buffers.Binary;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an element of the prime field GF(2^255 - 19) used by Ed25519.
/// Values are stored as five 51-bit limbs in little-endian order, that is:
/// value = h0 + h1 * 2^51 + h2 * 2^102 + h3 * 2^153 + h4 * 2^204 (mod p).
/// Limbs are allowed to exceed 51 bits between operations; methods that produce
/// a result normalise the carry chain so that limbs are bounded by ~2^52.
/// </summary>
internal readonly struct Edwards25519FieldElement
{
    private const ulong Mask51 = (1UL << 51) - 1;

    // 8 * p packed in 51-bit limbs, added before subtraction so that intermediate limbs
    // never underflow even after chained Add/Sub operations. (a + 8p - b ≡ a - b mod p.)
    // 8 * (2^51 - 19) = 2^54 - 152 for limb 0; 8 * (2^51 - 1) = 2^54 - 8 for limbs 1..4.
    private const ulong EightP0 = (1UL << 54) - 152;
    private const ulong EightPRest = (1UL << 54) - 8;

    private readonly ulong h0;
    private readonly ulong h1;
    private readonly ulong h2;
    private readonly ulong h3;
    private readonly ulong h4;

    public static readonly Edwards25519FieldElement Zero = new(0, 0, 0, 0, 0);
    public static readonly Edwards25519FieldElement One = new(1, 0, 0, 0, 0);

    public Edwards25519FieldElement(ulong h0, ulong h1, ulong h2, ulong h3, ulong h4)
    {
        this.h0 = h0;
        this.h1 = h1;
        this.h2 = h2;
        this.h3 = h3;
        this.h4 = h4;
    }

    /// <summary>
    /// Loads a field element from a 32-byte little-endian buffer. The most significant bit of
    /// <paramref name="bytes"/>[31] is masked off — point decoding uses it as a sign bit and the
    /// field value itself is at most 255 bits.
    /// </summary>
    public static Edwards25519FieldElement FromBytes(ReadOnlySpan<byte> bytes)
    {
        ulong b0 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[..8]);
        ulong b1 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[8..16]);
        ulong b2 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[16..24]);
        ulong b3 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[24..32]);

        ulong h0 = b0 & Mask51;
        ulong h1 = ((b0 >> 51) | (b1 << 13)) & Mask51;
        ulong h2 = ((b1 >> 38) | (b2 << 26)) & Mask51;
        ulong h3 = ((b2 >> 25) | (b3 << 39)) & Mask51;
        ulong h4 = (b3 >> 12) & Mask51;

        return new Edwards25519FieldElement(h0, h1, h2, h3, h4);
    }

    /// <summary>
    /// Writes the canonical (fully reduced) field element into <paramref name="output"/>
    /// as 32 little-endian bytes. The most significant bit of <paramref name="output"/>[31]
    /// is always zero — callers may overwrite it with a sign bit during point encoding.
    /// </summary>
    public void ToBytes(Span<byte> output)
    {
        ReduceCanonical(out ulong r0, out ulong r1, out ulong r2, out ulong r3, out ulong r4);

        ulong b0 = r0 | (r1 << 51);
        ulong b1 = (r1 >> 13) | (r2 << 38);
        ulong b2 = (r2 >> 26) | (r3 << 25);
        ulong b3 = (r3 >> 39) | (r4 << 12);

        BinaryPrimitives.WriteUInt64LittleEndian(output[..8], b0);
        BinaryPrimitives.WriteUInt64LittleEndian(output[8..16], b1);
        BinaryPrimitives.WriteUInt64LittleEndian(output[16..24], b2);
        BinaryPrimitives.WriteUInt64LittleEndian(output[24..32], b3);
    }

    public static Edwards25519FieldElement Add(in Edwards25519FieldElement a, in Edwards25519FieldElement b) =>
        new(a.h0 + b.h0, a.h1 + b.h1, a.h2 + b.h2, a.h3 + b.h3, a.h4 + b.h4);

    public static Edwards25519FieldElement Sub(in Edwards25519FieldElement a, in Edwards25519FieldElement b)
    {
        // Add 8p before subtracting so that intermediate limbs never underflow. To remain
        // safe under chained Subs (e.g. Negate(Sub(...))), propagate the carry chain once so
        // that the output limbs are bounded by Mask51 + small, matching the Mul/Square output
        // contract. Without this normalisation, an input limb from a prior Sub can exceed
        // EightP_limb and the following subtraction underflows.
        ulong h0 = a.h0 + EightP0 - b.h0;
        ulong h1 = a.h1 + EightPRest - b.h1;
        ulong h2 = a.h2 + EightPRest - b.h2;
        ulong h3 = a.h3 + EightPRest - b.h3;
        ulong h4 = a.h4 + EightPRest - b.h4;

        ulong c;
        c = h0 >> 51; h0 &= Mask51; h1 += c;
        c = h1 >> 51; h1 &= Mask51; h2 += c;
        c = h2 >> 51; h2 &= Mask51; h3 += c;
        c = h3 >> 51; h3 &= Mask51; h4 += c;
        c = h4 >> 51; h4 &= Mask51; h0 += 19 * c;

        return new Edwards25519FieldElement(h0, h1, h2, h3, h4);
    }

    public static Edwards25519FieldElement Negate(in Edwards25519FieldElement a) => Sub(Zero, a);

    /// <summary>
    /// Schoolbook multiplication of two field elements with reduction modulo p = 2^255 - 19.
    /// Cross terms whose total index exceeds 4 are reduced by multiplying the b-side limb by 19,
    /// which is the reduction relation 2^255 ≡ 19 (mod p).
    /// </summary>
    public static Edwards25519FieldElement Mul(in Edwards25519FieldElement a, in Edwards25519FieldElement b)
    {
        ulong b1_19 = 19 * b.h1;
        ulong b2_19 = 19 * b.h2;
        ulong b3_19 = 19 * b.h3;
        ulong b4_19 = 19 * b.h4;

        UInt128 c0 = (UInt128)a.h0 * b.h0 + (UInt128)a.h1 * b4_19 + (UInt128)a.h2 * b3_19 + (UInt128)a.h3 * b2_19 + (UInt128)a.h4 * b1_19;
        UInt128 c1 = (UInt128)a.h0 * b.h1 + (UInt128)a.h1 * b.h0 + (UInt128)a.h2 * b4_19 + (UInt128)a.h3 * b3_19 + (UInt128)a.h4 * b2_19;
        UInt128 c2 = (UInt128)a.h0 * b.h2 + (UInt128)a.h1 * b.h1 + (UInt128)a.h2 * b.h0 + (UInt128)a.h3 * b4_19 + (UInt128)a.h4 * b3_19;
        UInt128 c3 = (UInt128)a.h0 * b.h3 + (UInt128)a.h1 * b.h2 + (UInt128)a.h2 * b.h1 + (UInt128)a.h3 * b.h0 + (UInt128)a.h4 * b4_19;
        UInt128 c4 = (UInt128)a.h0 * b.h4 + (UInt128)a.h1 * b.h3 + (UInt128)a.h2 * b.h2 + (UInt128)a.h3 * b.h1 + (UInt128)a.h4 * b.h0;

        return CarryReduce(c0, c1, c2, c3, c4);
    }

    public static Edwards25519FieldElement Square(in Edwards25519FieldElement a) => Mul(a, a);

    /// <summary>
    /// Returns <paramref name="a"/> if <paramref name="condition"/> is false (0), or <paramref name="b"/>
    /// if <paramref name="condition"/> is true (any non-zero value). Implemented branch-free.
    /// </summary>
    public static Edwards25519FieldElement ConditionalSelect(in Edwards25519FieldElement a, in Edwards25519FieldElement b, ulong condition)
    {
        ulong mask = (ulong)-(long)(condition & 1UL);
        return new Edwards25519FieldElement(
            a.h0 ^ (mask & (a.h0 ^ b.h0)),
            a.h1 ^ (mask & (a.h1 ^ b.h1)),
            a.h2 ^ (mask & (a.h2 ^ b.h2)),
            a.h3 ^ (mask & (a.h3 ^ b.h3)),
            a.h4 ^ (mask & (a.h4 ^ b.h4)));
    }

    /// <summary>
    /// Modular inverse via Fermat's little theorem: a^(p - 2) mod p.
    /// Uses the standard 11-multiplication, 254-squaring addition chain.
    /// </summary>
    public static Edwards25519FieldElement Invert(in Edwards25519FieldElement z) => Pow22523ThenChain(z, multiplyZ11AtEnd: true);

    /// <summary>
    /// Returns z^((p - 5) / 8) mod p, used during square-root computation for point decoding.
    /// </summary>
    public static Edwards25519FieldElement PowP58(in Edwards25519FieldElement z) => Pow22523ThenChain(z, multiplyZ11AtEnd: false);

    /// <summary>
    /// Returns true if this field element represents zero modulo p.
    /// </summary>
    public bool IsZero
    {
        get
        {
            Span<byte> bytes = stackalloc byte[32];
            ToBytes(bytes);
            int accumulator = 0;
            for (int i = 0; i < 32; i++) accumulator |= bytes[i];
            return accumulator == 0;
        }
    }

    /// <summary>
    /// Returns the parity (least-significant bit) of the canonical representative.
    /// Used as the sign bit during Ed25519 point encoding per RFC 8032 §5.1.2.
    /// </summary>
    public bool IsNegative
    {
        get
        {
            Span<byte> bytes = stackalloc byte[32];
            ToBytes(bytes);
            return (bytes[0] & 1) == 1;
        }
    }

    /// <summary>
    /// Returns true if the two field elements represent the same residue class modulo p.
    /// </summary>
    public bool ValueEquals(in Edwards25519FieldElement other)
    {
        Span<byte> a = stackalloc byte[32];
        Span<byte> b = stackalloc byte[32];
        ToBytes(a);
        other.ToBytes(b);
        return a.SequenceEqual(b);
    }

    /// <summary>
    /// Reduces this element to its canonical representative in [0, p) and returns the five 51-bit limbs.
    /// </summary>
    private void ReduceCanonical(out ulong r0, out ulong r1, out ulong r2, out ulong r3, out ulong r4)
    {
        r0 = h0;
        r1 = h1;
        r2 = h2;
        r3 = h3;
        r4 = h4;

        ulong c;
        c = r0 >> 51; r0 &= Mask51; r1 += c;
        c = r1 >> 51; r1 &= Mask51; r2 += c;
        c = r2 >> 51; r2 &= Mask51; r3 += c;
        c = r3 >> 51; r3 &= Mask51; r4 += c;
        c = r4 >> 51; r4 &= Mask51; r0 += c * 19;
        c = r0 >> 51; r0 &= Mask51; r1 += c;

        // After one full pass the value is in [0, 2p); if it is in [p, 2p) we still need a final subtraction.
        // Test (r + 19) >= 2^255 — if so, r >= p.
        ulong t0 = r0 + 19;
        ulong t1 = r1 + (t0 >> 51); t0 &= Mask51;
        ulong t2 = r2 + (t1 >> 51); t1 &= Mask51;
        ulong t3 = r3 + (t2 >> 51); t2 &= Mask51;
        ulong t4 = r4 + (t3 >> 51); t3 &= Mask51;
        ulong overflow = t4 >> 51;
        t4 &= Mask51;

        ulong mask = (ulong)-(long)overflow;
        r0 = (r0 & ~mask) | (t0 & mask);
        r1 = (r1 & ~mask) | (t1 & mask);
        r2 = (r2 & ~mask) | (t2 & mask);
        r3 = (r3 & ~mask) | (t3 & mask);
        r4 = (r4 & ~mask) | (t4 & mask);
    }

    /// <summary>
    /// Propagates carries through five 128-bit accumulators and returns a 51-bit-limbed field element.
    /// The final low-limb reduction handles the wrap from limb 4's overflow back into limb 0 via × 19.
    /// All arithmetic stays in <see cref="UInt128"/> until the masked limbs are guaranteed to fit in
    /// <see cref="ulong"/> — a worst-case <c>(c4 &gt;&gt; 51) * 19</c> can exceed 2^64 and must not be
    /// performed in <see cref="ulong"/>.
    /// </summary>
    private static Edwards25519FieldElement CarryReduce(UInt128 c0, UInt128 c1, UInt128 c2, UInt128 c3, UInt128 c4)
    {
        c1 += c0 >> 51; c0 &= Mask51;
        c2 += c1 >> 51; c1 &= Mask51;
        c3 += c2 >> 51; c2 &= Mask51;
        c4 += c3 >> 51; c3 &= Mask51;
        c0 += 19 * (c4 >> 51); c4 &= Mask51;
        c1 += c0 >> 51; c0 &= Mask51;
        return new Edwards25519FieldElement(
            (ulong)c0,
            (ulong)c1,
            (ulong)c2,
            (ulong)c3,
            (ulong)c4);
    }

    /// <summary>
    /// Shared addition chain used by both <see cref="Invert"/> and <see cref="PowP58"/>.
    /// The chain computes z^(2^250 - 1) as t1, then multiplies by 2^5 squares to get z^(2^255 - 32).
    /// For inversion (p - 2 = 2^255 - 21), the result is multiplied by t0 = z^11.
    /// For PowP58 ((p - 5) / 8 = 2^252 - 3), the chain stops earlier, two extra squares plus z.
    /// </summary>
    private static Edwards25519FieldElement Pow22523ThenChain(in Edwards25519FieldElement z, bool multiplyZ11AtEnd)
    {
        Edwards25519FieldElement t0 = Square(z);             // z^2
        Edwards25519FieldElement t1 = Square(t0);            // z^4
        t1 = Square(t1);                                     // z^8
        t1 = Mul(z, t1);                                     // z^9
        t0 = Mul(t0, t1);                                    // z^11
        Edwards25519FieldElement t2 = Square(t0);            // z^22
        t1 = Mul(t1, t2);                                    // z^(2^5 - 1)

        t2 = Square(t1);
        for (int i = 1; i < 5; i++) t2 = Square(t2);          // z^(2^10 - 2^5)
        t1 = Mul(t2, t1);                                    // z^(2^10 - 1)

        t2 = Square(t1);
        for (int i = 1; i < 10; i++) t2 = Square(t2);         // z^(2^20 - 2^10)
        Edwards25519FieldElement t3 = Mul(t2, t1);           // z^(2^20 - 1)

        t2 = Square(t3);
        for (int i = 1; i < 20; i++) t2 = Square(t2);         // z^(2^40 - 2^20)
        t2 = Mul(t2, t3);                                    // z^(2^40 - 1)

        for (int i = 0; i < 10; i++) t2 = Square(t2);         // z^(2^50 - 2^10)
        t1 = Mul(t2, t1);                                    // z^(2^50 - 1)

        t2 = Square(t1);
        for (int i = 1; i < 50; i++) t2 = Square(t2);         // z^(2^100 - 2^50)
        t2 = Mul(t2, t1);                                    // z^(2^100 - 1)

        t3 = Square(t2);
        for (int i = 1; i < 100; i++) t3 = Square(t3);        // z^(2^200 - 2^100)
        t2 = Mul(t3, t2);                                    // z^(2^200 - 1)

        for (int i = 0; i < 50; i++) t2 = Square(t2);         // z^(2^250 - 2^50)
        t1 = Mul(t2, t1);                                    // z^(2^250 - 1)

        if (!multiplyZ11AtEnd)
        {
            // PowP58: z^((p-5)/8) = z^(2^252 - 3) = (z^(2^250 - 1))^4 * z
            t1 = Square(t1);
            t1 = Square(t1);
            return Mul(t1, z);
        }

        // Invert: z^(p-2) = z^(2^255 - 21) = (z^(2^250 - 1))^32 * z^11
        for (int i = 0; i < 5; i++) t1 = Square(t1);
        return Mul(t1, t0);
    }
}
