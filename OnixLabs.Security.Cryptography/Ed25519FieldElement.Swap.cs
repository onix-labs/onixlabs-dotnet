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
    /// Exchanges <paramref name="a"/> and <paramref name="b"/> if the low bit of <paramref name="swap"/> is 1, and leaves them unchanged
    /// if it is 0. Implemented branch-free, as the <c>cswap</c> function of RFC 7748 §5.
    /// </summary>
    /// <param name="a">The first field element, which receives <paramref name="b"/> when the elements are exchanged.</param>
    /// <param name="b">The second field element, which receives <paramref name="a"/> when the elements are exchanged.</param>
    /// <param name="swap">The swap condition; expected to be 0 or 1. The low bit selects: 0 leaves the elements, 1 exchanges them.</param>
    /// <remarks>
    /// Why: the X25519 Montgomery ladder swaps its working points on each bit of the secret scalar, so the swap must not branch on,
    /// or access memory according to, that bit.
    /// </remarks>
    public static void ConditionalSwap(ref Ed25519FieldElement a, ref Ed25519FieldElement b, ulong swap)
    {
        Ed25519FieldElement first = ConditionalSelect(a, b, swap);
        Ed25519FieldElement second = ConditionalSelect(b, a, swap);

        a = first;
        b = second;
    }
}
