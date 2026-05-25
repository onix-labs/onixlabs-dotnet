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
    /// Returns <paramref name="a"/> if the low bit of <paramref name="condition"/> is 0, or <paramref name="b"/>
    /// if the low bit of <paramref name="condition"/> is 1. Selection is driven solely by the low bit, so
    /// <paramref name="condition"/> must be 0 or 1; higher bits are ignored. Implemented branch-free.
    /// </summary>
    /// <param name="a">The field element returned when the low bit of <paramref name="condition"/> is 0.</param>
    /// <param name="b">The field element returned when the low bit of <paramref name="condition"/> is 1.</param>
    /// <param name="condition">The selection condition; expected to be 0 or 1. The low bit selects: 0 selects <paramref name="a"/>, 1 selects <paramref name="b"/>.</param>
    /// <returns>Returns either <paramref name="a"/> or <paramref name="b"/> selected in constant time without branching on <paramref name="condition"/>.</returns>
    public static Ed25519FieldElement ConditionalSelect(in Ed25519FieldElement a, in Ed25519FieldElement b, ulong condition)
    {
        ulong mask = (ulong)-(long)(condition & 1UL);

        return new Ed25519FieldElement(
            a.h0 ^ (mask & (a.h0 ^ b.h0)),
            a.h1 ^ (mask & (a.h1 ^ b.h1)),
            a.h2 ^ (mask & (a.h2 ^ b.h2)),
            a.h3 ^ (mask & (a.h3 ^ b.h3)),
            a.h4 ^ (mask & (a.h4 ^ b.h4))
        );
    }
}
