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

using System.Runtime.CompilerServices;

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

        Accumulator c0 = default;
        c0.MultiplyAdd(a.h0, b.h0);
        c0.MultiplyAdd(a.h1, b419);
        c0.MultiplyAdd(a.h2, b319);
        c0.MultiplyAdd(a.h3, b219);
        c0.MultiplyAdd(a.h4, b119);

        Accumulator c1 = default;
        c1.MultiplyAdd(a.h0, b.h1);
        c1.MultiplyAdd(a.h1, b.h0);
        c1.MultiplyAdd(a.h2, b419);
        c1.MultiplyAdd(a.h3, b319);
        c1.MultiplyAdd(a.h4, b219);

        Accumulator c2 = default;
        c2.MultiplyAdd(a.h0, b.h2);
        c2.MultiplyAdd(a.h1, b.h1);
        c2.MultiplyAdd(a.h2, b.h0);
        c2.MultiplyAdd(a.h3, b419);
        c2.MultiplyAdd(a.h4, b319);

        Accumulator c3 = default;
        c3.MultiplyAdd(a.h0, b.h3);
        c3.MultiplyAdd(a.h1, b.h2);
        c3.MultiplyAdd(a.h2, b.h1);
        c3.MultiplyAdd(a.h3, b.h0);
        c3.MultiplyAdd(a.h4, b419);

        Accumulator c4 = default;
        c4.MultiplyAdd(a.h0, b.h4);
        c4.MultiplyAdd(a.h1, b.h3);
        c4.MultiplyAdd(a.h2, b.h2);
        c4.MultiplyAdd(a.h3, b.h1);
        c4.MultiplyAdd(a.h4, b.h0);

        return CarryReduce(ref c0, ref c1, ref c2, ref c3, ref c4);
    }

    /// <summary>
    /// Propagates carries through five 128-bit accumulators and returns a 51-bit-limbed field element.
    /// The carry out of the top limb wraps around to the bottom limb multiplied by 19, because 2^255 ≡ 19 (mod p).
    /// That wrapped carry can exceed 64 bits, so it is computed in 128 bits before the bottom limb's own carry is propagated once more.
    /// </summary>
    /// <param name="c0">The least significant 128-bit limb accumulator.</param>
    /// <param name="c1">The second 128-bit limb accumulator.</param>
    /// <param name="c2">The third 128-bit limb accumulator.</param>
    /// <param name="c3">The fourth 128-bit limb accumulator.</param>
    /// <param name="c4">The most significant 128-bit limb accumulator.</param>
    /// <returns>Returns a new <see cref="Ed25519FieldElement"/> whose limbs are bounded by ~2^52.</returns>
    /// <remarks>
    /// Why: this must be inlined. Passing the accumulators by reference to a separate call forces them into memory, which made signing 50%
    /// and verification 50% slower than inlining, and slower than the <see cref="System.UInt128"/> code the accumulators replaced.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Ed25519FieldElement CarryReduce(
        ref Accumulator c0,
        ref Accumulator c1,
        ref Accumulator c2,
        ref Accumulator c3,
        ref Accumulator c4)
    {
        c1.AddCarry(c0);
        c2.AddCarry(c1);
        c3.AddCarry(c2);
        c4.AddCarry(c3);

        Accumulator wrapped = Accumulator.WrapCarry(c4, c0.Limb);

        return new Ed25519FieldElement(wrapped.Limb, wrapped.AddCarryTo(c1.Limb), c2.Limb, c3.Limb, c4.Limb);
    }
}
