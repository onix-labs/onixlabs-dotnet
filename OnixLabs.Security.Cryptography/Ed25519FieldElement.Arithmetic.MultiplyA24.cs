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
    /// The X25519 ladder constant a24 = (486662 - 2) / 4 = 121665, from RFC 7748 §5.
    /// </summary>
    private const ulong A24 = 121665;

    /// <summary>
    /// Multiplies a field element by the X25519 ladder constant a24 = 121665 modulo p.
    /// </summary>
    /// <param name="a">The field element to multiply.</param>
    /// <returns>Returns the field element representing 121665 · <paramref name="a"/> modulo p.</returns>
    /// <remarks>
    /// Why: a24 fits in one limb, so this needs five limb products rather than the twenty-five of <see cref="Multiply"/>. It runs once per
    /// bit of the X25519 ladder. The products and the carry reduction are the same as <see cref="Multiply"/> by a field element holding
    /// a24.
    /// </remarks>
    public static Ed25519FieldElement MultiplyA24(in Ed25519FieldElement a)
    {
        Accumulator c0 = default;
        c0.MultiplyAdd(a.h0, A24);

        Accumulator c1 = default;
        c1.MultiplyAdd(a.h1, A24);

        Accumulator c2 = default;
        c2.MultiplyAdd(a.h2, A24);

        Accumulator c3 = default;
        c3.MultiplyAdd(a.h3, A24);

        Accumulator c4 = default;
        c4.MultiplyAdd(a.h4, A24);

        return CarryReduce(ref c0, ref c1, ref c2, ref c3, ref c4);
    }
}
