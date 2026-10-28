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
    /// Squares a field element modulo p.
    /// </summary>
    /// <param name="a">The field element to square.</param>
    /// <returns>Returns the field element representing <paramref name="a"/>^2 modulo p.</returns>
    /// <remarks>
    /// Why: this is <see cref="Multiply"/> specialised to a·a. Each cross term a_i·a_j appears twice in the product,
    /// so it is computed once with one factor doubled, needing 15 limb products instead of 25. The accumulated sums are
    /// the same integers <see cref="Multiply"/> produces, so the result is identical to <c>a * a</c>.
    /// Squaring dominates inversion and point doubling.
    /// </remarks>
    public static Ed25519FieldElement Square(in Ed25519FieldElement a)
    {
        ulong a0Twice = 2 * a.h0;
        ulong a1Twice = 2 * a.h1;
        ulong a2Twice = 2 * a.h2;
        ulong a3Twice = 2 * a.h3;
        ulong a3Times19 = 19 * a.h3;
        ulong a4Times19 = 19 * a.h4;

        Accumulator c0 = default;
        c0.MultiplyAdd(a.h0, a.h0);
        c0.MultiplyAdd(a1Twice, a4Times19);
        c0.MultiplyAdd(a2Twice, a3Times19);

        Accumulator c1 = default;
        c1.MultiplyAdd(a0Twice, a.h1);
        c1.MultiplyAdd(a.h3, a3Times19);
        c1.MultiplyAdd(a2Twice, a4Times19);

        Accumulator c2 = default;
        c2.MultiplyAdd(a0Twice, a.h2);
        c2.MultiplyAdd(a.h1, a.h1);
        c2.MultiplyAdd(a3Twice, a4Times19);

        Accumulator c3 = default;
        c3.MultiplyAdd(a0Twice, a.h3);
        c3.MultiplyAdd(a1Twice, a.h2);
        c3.MultiplyAdd(a.h4, a4Times19);

        Accumulator c4 = default;
        c4.MultiplyAdd(a0Twice, a.h4);
        c4.MultiplyAdd(a1Twice, a.h3);
        c4.MultiplyAdd(a.h2, a.h2);

        return CarryReduce(ref c0, ref c1, ref c2, ref c3, ref c4);
    }

    /// <summary>
    /// Squares a field element modulo p the specified number of <paramref name="times"/>.
    /// Equivalent to applying <see cref="Square"/> repeatedly; used to compress addition chains
    /// such as the one in <see cref="Invert"/> and <see cref="PowP58"/>.
    /// </summary>
    /// <param name="element">The field element to square.</param>
    /// <param name="times">The number of times to apply the squaring operation.</param>
    /// <returns>Returns the field element representing <paramref name="element"/>^(2^<paramref name="times"/>) modulo p.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Ed25519FieldElement RepeatedSquare(in Ed25519FieldElement element, int times)
    {
        Ed25519FieldElement result = element;
        for (int i = 0; i < times; i++) result = Square(result);
        return result;
    }
}
