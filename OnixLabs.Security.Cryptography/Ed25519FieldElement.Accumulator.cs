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
using System.Runtime.CompilerServices;

namespace OnixLabs.Security.Cryptography;

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Represents an unsigned 128-bit accumulator of limb products, held as two 64-bit halves.
    /// </summary>
    /// <remarks>
    /// Why: <see cref="UInt128"/> multiplication and addition are not lowered to single machine instructions. Accumulating
    /// <see cref="Math.BigMul(ulong, ulong, out ulong)"/> products with explicit carries made Ed25519 signing about 10% and verification
    /// about 18% faster than the same code using <see cref="UInt128"/>, on both .NET 8 and .NET 10. Every operation is exact 128-bit
    /// arithmetic, wrapping modulo 2^128 exactly as <see cref="UInt128"/> does, so results are identical to the
    /// <see cref="UInt128"/> formulation.
    /// The gain depends on accumulators staying in registers; see <see cref="CarryReduce"/>.
    /// </remarks>
    private struct Accumulator
    {
        /// <summary>
        /// The most significant 64 bits of the accumulated value.
        /// </summary>
        private ulong high;

        /// <summary>
        /// The least significant 64 bits of the accumulated value.
        /// </summary>
        private ulong low;

        /// <summary>
        /// Gets the least significant 51 bits of the accumulated value: the limb that remains once its carry has moved up.
        /// </summary>
        /// <value>The least significant 51 bits of the accumulated value.</value>
        public readonly ulong Limb => low & Mask51;

        /// <summary>
        /// Gets the least significant 64 bits of the accumulated value shifted right by 51 bits: the carry out of a 51-bit limb.
        /// </summary>
        /// <value>The least significant 64 bits of the carry out of a 51-bit limb.</value>
        private readonly ulong CarryLow => (high << 13) | (low >> 51);

        /// <summary>
        /// Gets the most significant 64 bits of the accumulated value shifted right by 51 bits.
        /// </summary>
        /// <value>The most significant 64 bits of the carry out of a 51-bit limb.</value>
        private readonly ulong CarryHigh => high >> 51;

        /// <summary>
        /// Adds the full 128-bit product of the specified values.
        /// </summary>
        /// <param name="left">The left-hand factor.</param>
        /// <param name="right">The right-hand factor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MultiplyAdd(ulong left, ulong right)
        {
            ulong productHigh = Math.BigMul(left, right, out ulong productLow);
            Add(productHigh, productLow);
        }

        /// <summary>
        /// Adds the carry out of the specified accumulator's 51-bit limb, which is its value shifted right by 51 bits.
        /// </summary>
        /// <param name="source">The accumulator whose carry to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCarry(in Accumulator source) => Add(source.CarryHigh, source.CarryLow);

        /// <summary>
        /// Creates an accumulator holding nineteen times the carry out of the specified accumulator's 51-bit limb,
        /// plus the specified value.
        /// </summary>
        /// <param name="source">The accumulator whose carry to multiply by nineteen.</param>
        /// <param name="addend">The value to add.</param>
        /// <returns>Returns an accumulator holding 19 · (<paramref name="source"/> &gt;&gt; 51) + <paramref name="addend"/>.</returns>
        /// <remarks>
        /// This wraps the carry out of the top limb around to the bottom limb, because 2^255 ≡ 19 (mod p).
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Accumulator WrapCarry(in Accumulator source, ulong addend)
        {
            Accumulator result = default;
            result.MultiplyAdd(source.CarryLow, 19);
            result.high += source.CarryHigh * 19;
            result.Add(0, addend);
            return result;
        }

        /// <summary>
        /// Gets the least significant 64 bits of the sum of the specified limb and the carry out of the current accumulator's 51-bit limb.
        /// </summary>
        /// <param name="limb">The limb to which the carry is added.</param>
        /// <returns>Returns the least significant 64 bits of <paramref name="limb"/> + (this &gt;&gt; 51).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong AddCarryTo(ulong limb) => limb + CarryLow;

        /// <summary>
        /// Adds the specified 128-bit value, given as two 64-bit halves.
        /// </summary>
        /// <param name="valueHigh">The most significant 64 bits of the value to add.</param>
        /// <param name="valueLow">The least significant 64 bits of the value to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(ulong valueHigh, ulong valueLow)
        {
            low += valueLow;
            high += valueHigh + (low < valueLow ? 1UL : 0UL);
        }
    }
}
