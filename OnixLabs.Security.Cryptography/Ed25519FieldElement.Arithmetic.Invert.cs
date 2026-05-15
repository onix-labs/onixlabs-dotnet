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
    /// Computes the modular inverse of a field element via Fermat's little theorem: <paramref name="z"/>^(p - 2) mod p.
    /// Uses the standard 11-multiplication, 254-squaring addition chain.
    /// </summary>
    /// <param name="z">The field element to invert.</param>
    /// <returns>Returns the field element representing the multiplicative inverse of <paramref name="z"/> modulo p.</returns>
    public static Ed25519FieldElement Invert(in Ed25519FieldElement z) => Pow22523ThenChain(z, multiplyZ11AtEnd: true);

    /// <summary>
    /// Computes the shared addition chain used by both <see cref="Invert"/> and <see cref="PowP58"/>.
    /// The chain computes z^(2^250 - 1) as t1, then multiplies by 2^5 squares to get z^(2^255 - 32).
    /// For inversion (p - 2 = 2^255 - 21), the result is multiplied by t0 = z^11.
    /// For PowP58 ((p - 5) / 8 = 2^252 - 3), the chain stops earlier, two extra squares plus z.
    /// </summary>
    /// <param name="z">The base field element.</param>
    /// <param name="multiplyZ11AtEnd"><see langword="true"/> to terminate the chain as required by <see cref="Invert"/>; <see langword="false"/> to terminate as required by <see cref="PowP58"/>.</param>
    /// <returns>Returns the resulting field element corresponding to the requested exponent.</returns>
    private static Ed25519FieldElement Pow22523ThenChain(in Ed25519FieldElement z, bool multiplyZ11AtEnd)
    {
        Ed25519FieldElement t0 = Square(z); // z^2
        Ed25519FieldElement t1 = Square(t0); // z^4
        t1 = Square(t1); // z^8
        t1 = z * t1; // z^9
        t0 = t0 * t1; // z^11
        Ed25519FieldElement t2 = Square(t0); // z^22
        t1 = t1 * t2; // z^(2^5 - 1)

        t2 = RepeatedSquare(t1, 5); // z^(2^10 - 2^5)
        t1 = t2 * t1; // z^(2^10 - 1)

        t2 = RepeatedSquare(t1, 10); // z^(2^20 - 2^10)
        Ed25519FieldElement t3 = t2 * t1; // z^(2^20 - 1)

        t2 = RepeatedSquare(t3, 20); // z^(2^40 - 2^20)
        t2 = t2 * t3; // z^(2^40 - 1)

        t2 = RepeatedSquare(t2, 10); // z^(2^50 - 2^10)
        t1 = t2 * t1; // z^(2^50 - 1)

        t2 = RepeatedSquare(t1, 50); // z^(2^100 - 2^50)
        t2 = t2 * t1; // z^(2^100 - 1)

        t3 = RepeatedSquare(t2, 100); // z^(2^200 - 2^100)
        t2 = t3 * t2; // z^(2^200 - 1)

        t2 = RepeatedSquare(t2, 50); // z^(2^250 - 2^50)
        t1 = t2 * t1; // z^(2^250 - 1)

        if (!multiplyZ11AtEnd)
        {
            // PowP58: z^((p-5)/8) = z^(2^252 - 3) = (z^(2^250 - 1))^4 * z
            t1 = RepeatedSquare(t1, 2);
            return t1 * z;
        }

        // Invert: z^(p-2) = z^(2^255 - 21) = (z^(2^250 - 1))^32 * z^11
        t1 = RepeatedSquare(t1, 5);
        return t1 * t0;
    }
}
