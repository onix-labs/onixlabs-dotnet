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
    /// Writes the canonical (fully reduced) field element into <paramref name="output"/>
    /// as 32 little-endian bytes. The most significant bit of <paramref name="output"/>[31]
    /// is always zero — callers may overwrite it with a sign bit during point encoding.
    /// </summary>
    /// <param name="output">The 32-byte destination buffer that receives the canonical little-endian encoding.</param>
    public void WriteBytes(Span<byte> output)
    {
        ReduceCanonical(out ulong r0, out ulong r1, out ulong r2, out ulong r3, out ulong r4);

        ulong b0 = r0 | (r1 << 51);
        ulong b1 = (r1 >> 13) | (r2 << 38);
        ulong b2 = (r2 >> 26) | (r3 << 25);
        ulong b3 = (r3 >> 39) | (r4 << 12);

        BinaryPrimitives.WriteUInt64LittleEndian(output[..8], b0);
        BinaryPrimitives.WriteUInt64LittleEndian(output[8..16], b1);
        BinaryPrimitives.WriteUInt64LittleEndian(output[16..24], b2);
        BinaryPrimitives.WriteUInt64LittleEndian(output[24..32], b3);
    }

    /// <summary>
    /// Reduces this element to its canonical representative in [0, p) and returns the five 51-bit limbs.
    /// </summary>
    /// <param name="r0">When this method returns, contains the least significant 51-bit limb of the canonical representative.</param>
    /// <param name="r1">When this method returns, contains the second 51-bit limb of the canonical representative.</param>
    /// <param name="r2">When this method returns, contains the third 51-bit limb of the canonical representative.</param>
    /// <param name="r3">When this method returns, contains the fourth 51-bit limb of the canonical representative.</param>
    /// <param name="r4">When this method returns, contains the most significant 51-bit limb of the canonical representative.</param>
    private void ReduceCanonical(out ulong r0, out ulong r1, out ulong r2, out ulong r3, out ulong r4)
    {
        // @formatter:off
        r0 = h0;
        r1 = h1;
        r2 = h2;
        r3 = h3;
        r4 = h4;

        ulong c;
        c = r0 >> 51; r0 &= Mask51; r1 += c;
        c = r1 >> 51; r1 &= Mask51; r2 += c;
        c = r2 >> 51; r2 &= Mask51; r3 += c;
        c = r3 >> 51; r3 &= Mask51; r4 += c;
        c = r4 >> 51; r4 &= Mask51; r0 += c * 19;
        c = r0 >> 51; r0 &= Mask51; r1 += c;

        // After one full pass the value is in [0, 2p); if it is in [p, 2p) we still need a final subtraction.
        // Test (r + 19) >= 2^255 — if so, r >= p.
        ulong t0 = r0 + 19;
        ulong t1 = r1 + (t0 >> 51); t0 &= Mask51;
        ulong t2 = r2 + (t1 >> 51); t1 &= Mask51;
        ulong t3 = r3 + (t2 >> 51); t2 &= Mask51;
        ulong t4 = r4 + (t3 >> 51); t3 &= Mask51;
        ulong overflow = t4 >> 51; t4 &= Mask51;

        ulong mask = (ulong)-(long)overflow;
        r0 = (r0 & ~mask) | (t0 & mask);
        r1 = (r1 & ~mask) | (t1 & mask);
        r2 = (r2 & ~mask) | (t2 & mask);
        r3 = (r3 & ~mask) | (t3 & mask);
        r4 = (r4 & ~mask) | (t4 & mask);
        // @formatter:on
    }
}
