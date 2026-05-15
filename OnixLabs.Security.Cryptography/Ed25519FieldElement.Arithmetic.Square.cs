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
    public static Ed25519FieldElement Square(in Ed25519FieldElement a) => a * a;

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
