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
using System.Buffers.Binary;

namespace OnixLabs.Security.Cryptography;

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Loads a field element from a 32-byte little-endian buffer. The most significant bit of
    /// <paramref name="bytes"/>[31] is masked off — point decoding uses it as a sign bit and the
    /// field value itself is at most 255 bits.
    /// </summary>
    /// <param name="bytes">The 32-byte little-endian buffer to decode.</param>
    /// <returns>Returns a new <see cref="Ed25519FieldElement"/> decoded from <paramref name="bytes"/>.</returns>
    public static Ed25519FieldElement FromBytes(ReadOnlySpan<byte> bytes)
    {
        ulong b0 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[..8]);
        ulong b1 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[8..16]);
        ulong b2 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[16..24]);
        ulong b3 = BinaryPrimitives.ReadUInt64LittleEndian(bytes[24..32]);

        ulong h0 = b0 & Mask51;
        ulong h1 = ((b0 >> 51) | (b1 << 13)) & Mask51;
        ulong h2 = ((b1 >> 38) | (b2 << 26)) & Mask51;
        ulong h3 = ((b2 >> 25) | (b3 << 39)) & Mask51;
        ulong h4 = (b3 >> 12) & Mask51;

        return new Ed25519FieldElement(h0, h1, h2, h3, h4);
    }
}
