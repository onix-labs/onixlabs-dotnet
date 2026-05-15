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

using System;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an element of the prime field GF(2^255 - 19) used by Ed25519.
/// Values are stored as five 51-bit limbs in little-endian order, that is:
/// value = h0 + h1 * 2^51 + h2 * 2^102 + h3 * 2^153 + h4 * 2^204 (mod p).
/// Limbs are allowed to exceed 51 bits between operations; methods that produce
/// a result normalize the carry chain so that limbs are bounded by ~2^52.
/// </summary>
internal readonly partial struct Ed25519FieldElement : IEquatable<Ed25519FieldElement>
{
    /// <summary>
    /// The least significant 51-bit limb of the field element.
    /// </summary>
    private readonly ulong h0;

    /// <summary>
    /// The second 51-bit limb of the field element.
    /// </summary>
    private readonly ulong h1;

    /// <summary>
    /// The third 51-bit limb of the field element.
    /// </summary>
    private readonly ulong h2;

    /// <summary>
    /// The fourth 51-bit limb of the field element.
    /// </summary>
    private readonly ulong h3;

    /// <summary>
    /// The most significant 51-bit limb of the field element.
    /// </summary>
    private readonly ulong h4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ed25519FieldElement"/> struct.
    /// </summary>
    /// <param name="h0">The least significant 51-bit limb.</param>
    /// <param name="h1">The second 51-bit limb.</param>
    /// <param name="h2">The third 51-bit limb.</param>
    /// <param name="h3">The fourth 51-bit limb.</param>
    /// <param name="h4">The most significant 51-bit limb.</param>
    private Ed25519FieldElement(ulong h0, ulong h1, ulong h2, ulong h3, ulong h4)
    {
        this.h0 = h0;
        this.h1 = h1;
        this.h2 = h2;
        this.h3 = h3;
        this.h4 = h4;
    }
}
