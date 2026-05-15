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
    /// Computes the sum of the specified <see cref="Ed25519FieldElement"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to add to.</param>
    /// <param name="right">The <paramref name="right"/> value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Ed25519FieldElement"/> values.</returns>
    public static Ed25519FieldElement operator +(in Ed25519FieldElement left, in Ed25519FieldElement right) => Add(left, right);

    /// <summary>
    /// Adds two field elements modulo p.
    /// </summary>
    /// <param name="a">The first field element addend.</param>
    /// <param name="b">The second field element addend.</param>
    /// <returns>Returns the field element representing <paramref name="a"/> + <paramref name="b"/> modulo p.</returns>
    private static Ed25519FieldElement Add(in Ed25519FieldElement a, in Ed25519FieldElement b) =>
        new(a.h0 + b.h0, a.h1 + b.h1, a.h2 + b.h2, a.h3 + b.h3, a.h4 + b.h4);
}
