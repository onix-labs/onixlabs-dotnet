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

internal readonly partial struct Ed25519Point
{
    // Curve constant d = -121,665 / 121,666 mod p, little-endian canonical encoding.
    // Declared before BasePoint because the base-point initializer references this via TryDecode.

    /// <summary>
    /// The Edwards25519 curve constant d = -121,665 / 121,666 mod p, expressed in canonical little-endian form.
    /// </summary>
    private static readonly Ed25519FieldElement D = Ed25519FieldElement.FromBytes([
        0xa3, 0x78, 0x59, 0x13, 0xca, 0x4d, 0xeb, 0x75,
        0xab, 0xd8, 0x41, 0x41, 0x4d, 0x0a, 0x70, 0x00,
        0x98, 0xe8, 0x79, 0x77, 0x79, 0x40, 0xc7, 0x8c,
        0x73, 0xfe, 0x6f, 0x2b, 0xee, 0x6c, 0x03, 0x52,
    ]);

    /// <summary>
    /// Twice the curve constant <see cref="D"/>, precomputed for use in the point addition formulas.
    /// </summary>
    private static readonly Ed25519FieldElement TwoD = D + D;

    /// <summary>
    /// A square root of -1 modulo p, equal to 2^((p - 1) / 4) mod p, used during point decoding.
    /// </summary>
    private static readonly Ed25519FieldElement SqrtMinusOne = Ed25519FieldElement.FromBytes([
        0xb0, 0xa0, 0x0e, 0x4a, 0x27, 0x1b, 0xee, 0xc4,
        0x78, 0xe4, 0x2f, 0xad, 0x06, 0x18, 0x43, 0x2f,
        0xa7, 0xd7, 0xfb, 0x3d, 0x99, 0x00, 0x4d, 0x2b,
        0x0b, 0xdf, 0xc1, 0x4f, 0x80, 0x24, 0x83, 0x2b,
    ]);

    /// <summary>
    /// The neutral element of the curve group, affine (0, 1).
    /// </summary>
    private static readonly Ed25519Point Identity = new(
        Ed25519FieldElement.Zero,
        Ed25519FieldElement.One,
        Ed25519FieldElement.One,
        Ed25519FieldElement.Zero
    );

    /// <summary>
    /// The standard Ed25519 base point B per RFC 8032 §5.1. Decoded from its 32-byte canonical
    /// encoding, so we do not maintain a duplicate set of 51-bit limb constants. Must be declared
    /// after <see cref="D"/> and <see cref="SqrtMinusOne"/> so those are initialized first.
    /// </summary>
    public static readonly Ed25519Point BasePoint = DecodeBasePoint();

    /// <summary>
    /// Decodes the standard Ed25519 base point B from its canonical RFC 8032 §5.1 encoding.
    /// </summary>
    /// <returns>Returns the Ed25519 base point B.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the base-point encoding fails to decode, which indicates a programmer error in the curve constants.</exception>
    private static Ed25519Point DecodeBasePoint()
    {
        // Encoded base point B per RFC 8032 §5.1: y = 4/5 mod p (little-endian 0x58 followed by
        // 31 bytes of 0x66), sign bit 0.
        ReadOnlySpan<byte> encoded =
        [
            0x58, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
            0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66,
        ];

        return TryDecode(encoded, out Ed25519Point point)
            ? point
            : throw new InvalidOperationException("Ed25519 base point failed to decode (implementation bug).");
    }
}
