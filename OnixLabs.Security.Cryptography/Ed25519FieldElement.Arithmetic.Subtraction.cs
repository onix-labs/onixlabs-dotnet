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

namespace OnixLabs.Security.Cryptography;

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Computes the difference of the specified <see cref="Ed25519FieldElement"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to subtract from.</param>
    /// <param name="right">The <paramref name="right"/> value to subtract.</param>
    /// <returns>Returns the difference of the specified <see cref="Ed25519FieldElement"/> values.</returns>
    public static Ed25519FieldElement operator -(in Ed25519FieldElement left, in Ed25519FieldElement right) => Subtract(left, right);

    /// <summary>
    /// Subtracts one field element from another modulo p.
    /// </summary>
    /// <param name="a">The minuend field element.</param>
    /// <param name="b">The subtrahend field element.</param>
    /// <returns>Returns the field element representing <paramref name="a"/> - <paramref name="b"/> modulo p.</returns>
    private static Ed25519FieldElement Subtract(in Ed25519FieldElement a, in Ed25519FieldElement b)
    {
        // Add 8p before subtracting so that intermediate limbs never underflow. To remain
        // safe under chained Subtract operations (e.g. Negate(Subtract(...))), propagate the carry
        // chain once so that the output limbs are bounded by Mask51 + small, matching the
        // Multiply/Square output contract. Without this normalization, an input limb from a prior
        // Subtract can exceed EightP_limb and the following subtraction underflows.
        ulong h0 = a.h0 + EightP0 - b.h0;
        ulong h1 = a.h1 + EightPRest - b.h1;
        ulong h2 = a.h2 + EightPRest - b.h2;
        ulong h3 = a.h3 + EightPRest - b.h3;
        ulong h4 = a.h4 + EightPRest - b.h4;

        ulong c;

        // @formatter:off
        c = h0 >> 51; h0 &= Mask51; h1 += c;
        c = h1 >> 51; h1 &= Mask51; h2 += c;
        c = h2 >> 51; h2 &= Mask51; h3 += c;
        c = h3 >> 51; h3 &= Mask51; h4 += c;
        c = h4 >> 51; h4 &= Mask51; h0 += 19 * c;
        // @formatter:on

        return new Ed25519FieldElement(h0, h1, h2, h3, h4);
    }
}
