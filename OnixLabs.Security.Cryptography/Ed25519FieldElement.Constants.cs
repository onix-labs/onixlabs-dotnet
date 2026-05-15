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
    /// The 51-bit limb mask, equal to 2^51 - 1.
    /// </summary>
    private const ulong Mask51 = (1UL << 51) - 1;

    // 8 * p packed in 51-bit limbs, added before subtraction so that intermediate limbs
    // never underflow even after chained Add/Subtract operations. (a + 8p - b ≡ a - b mod p.)
    // 8 * (2^51 - 19) = 2^54 - 152 for limb 0; 8 * (2^51 - 1) = 2^54 - 8 for limbs 1..4.

    /// <summary>
    /// Eight times the low limb of the field prime p, used to avoid underflow during subtraction.
    /// </summary>
    private const ulong EightP0 = (1UL << 54) - 152;

    /// <summary>
    /// Eight times each of the upper four limbs of the field prime p, used to avoid underflow during subtraction.
    /// </summary>
    private const ulong EightPRest = (1UL << 54) - 8;

    /// <summary>
    /// The additive identity (zero) of the field.
    /// </summary>
    public static readonly Ed25519FieldElement Zero = new(0, 0, 0, 0, 0);

    /// <summary>
    /// The multiplicative identity (one) of the field.
    /// </summary>
    public static readonly Ed25519FieldElement One = new(1, 0, 0, 0, 0);
}
