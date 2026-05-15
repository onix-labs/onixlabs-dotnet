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

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Computes the product of the specified <see cref="Ed25519FieldElement"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to multiply.</param>
    /// <param name="right">The <paramref name="right"/> value to multiply by.</param>
    /// <returns>Returns the product of the specified <see cref="Ed25519FieldElement"/> values.</returns>
    public static Ed25519FieldElement operator *(in Ed25519FieldElement left, in Ed25519FieldElement right) => Multiply(left, right);

    /// <summary>
    /// Multiplies two field elements modulo p using schoolbook multiplication.
    /// Cross-terms whose total index exceeds 4 are reduced by multiplying the b-side limb by 19,
    /// which is reduction relation 2^255 ≡ 19 (mod p).
    /// </summary>
    /// <param name="a">The first field element factor.</param>
    /// <param name="b">The second field element factor.</param>
    /// <returns>Returns the field element representing <paramref name="a"/> * <paramref name="b"/> modulo p.</returns>
    private static Ed25519FieldElement Multiply(in Ed25519FieldElement a, in Ed25519FieldElement b)
    {
        ulong b119 = 19 * b.h1;
        ulong b219 = 19 * b.h2;
        ulong b319 = 19 * b.h3;
        ulong b419 = 19 * b.h4;

        UInt128 c0 = (UInt128)a.h0 * b.h0 + (UInt128)a.h1 * b419 + (UInt128)a.h2 * b319 + (UInt128)a.h3 * b219 + (UInt128)a.h4 * b119;
        UInt128 c1 = (UInt128)a.h0 * b.h1 + (UInt128)a.h1 * b.h0 + (UInt128)a.h2 * b419 + (UInt128)a.h3 * b319 + (UInt128)a.h4 * b219;
        UInt128 c2 = (UInt128)a.h0 * b.h2 + (UInt128)a.h1 * b.h1 + (UInt128)a.h2 * b.h0 + (UInt128)a.h3 * b419 + (UInt128)a.h4 * b319;
        UInt128 c3 = (UInt128)a.h0 * b.h3 + (UInt128)a.h1 * b.h2 + (UInt128)a.h2 * b.h1 + (UInt128)a.h3 * b.h0 + (UInt128)a.h4 * b419;
        UInt128 c4 = (UInt128)a.h0 * b.h4 + (UInt128)a.h1 * b.h3 + (UInt128)a.h2 * b.h2 + (UInt128)a.h3 * b.h1 + (UInt128)a.h4 * b.h0;

        return CarryReduce(c0, c1, c2, c3, c4);
    }

    /// <summary>
    /// Propagates carries through five 128-bit accumulators and returns a 51-bit-limbed field element.
    /// The final low-limb reduction handles the wrap from limb 4's overflow back into limb 0 via × 19.
    /// All arithmetic stays in <see cref="UInt128"/> until the masked limbs are guaranteed to fit in
    /// <see cref="ulong"/> — a worst-case <c>(c4 &gt;&gt; 51) * 19</c> can exceed 2^64 and must not be
    /// performed in <see cref="ulong"/>.
    /// </summary>
    /// <param name="c0">The least significant 128-bit limb accumulator.</param>
    /// <param name="c1">The second 128-bit limb accumulator.</param>
    /// <param name="c2">The third 128-bit limb accumulator.</param>
    /// <param name="c3">The fourth 128-bit limb accumulator.</param>
    /// <param name="c4">The most significant 128-bit limb accumulator.</param>
    /// <returns>Returns a new <see cref="Ed25519FieldElement"/> whose limbs are bounded by ~2^52.</returns>
    private static Ed25519FieldElement CarryReduce(UInt128 c0, UInt128 c1, UInt128 c2, UInt128 c3, UInt128 c4)
    {
        c1 += c0 >> 51;
        c0 &= Mask51;
        c2 += c1 >> 51;
        c1 &= Mask51;
        c3 += c2 >> 51;
        c2 &= Mask51;
        c4 += c3 >> 51;
        c3 &= Mask51;
        c0 += 19 * (c4 >> 51);
        c4 &= Mask51;
        c1 += c0 >> 51;
        c0 &= Mask51;

        return new Ed25519FieldElement((ulong)c0, (ulong)c1, (ulong)c2, (ulong)c3, (ulong)c4);
    }
}
