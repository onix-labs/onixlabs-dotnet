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
    /// Returns <paramref name="a"/> if <paramref name="condition"/> is false (0), or <paramref name="b"/>
    /// if <paramref name="condition"/> is true (any non-zero value). Implemented branch-free.
    /// </summary>
    /// <param name="a">The field element returned when <paramref name="condition"/> is zero.</param>
    /// <param name="b">The field element returned when <paramref name="condition"/> is non-zero.</param>
    /// <param name="condition">The selection condition; zero selects <paramref name="a"/>, non-zero selects <paramref name="b"/>.</param>
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
