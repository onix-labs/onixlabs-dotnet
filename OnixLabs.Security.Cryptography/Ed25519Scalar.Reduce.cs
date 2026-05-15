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

internal static partial class Ed25519Scalar
{
    /// <summary>
    /// Reduces a 64-byte little-endian integer modulo L and writes the result as 32 little-endian bytes.
    /// </summary>
    /// <param name="wide">The 64-byte little-endian integer to reduce.</param>
    /// <param name="output">The 32-byte destination buffer that receives the little-endian canonical scalar.</param>
    public static void ReduceFromWideBytes(ReadOnlySpan<byte> wide, Span<byte> output)
    {
        // @formatter:off
        long s0 = 2097151 & Load3(wide[0..]);
        long s1 = 2097151 & (Load4(wide[2..]) >> 5);
        long s2 = 2097151 & (Load3(wide[5..]) >> 2);
        long s3 = 2097151 & (Load4(wide[7..]) >> 7);
        long s4 = 2097151 & (Load4(wide[10..]) >> 4);
        long s5 = 2097151 & (Load3(wide[13..]) >> 1);
        long s6 = 2097151 & (Load4(wide[15..]) >> 6);
        long s7 = 2097151 & (Load3(wide[18..]) >> 3);
        long s8 = 2097151 & Load3(wide[21..]);
        long s9 = 2097151 & (Load4(wide[23..]) >> 5);
        long s10 = 2097151 & (Load3(wide[26..]) >> 2);
        long s11 = 2097151 & (Load4(wide[28..]) >> 7);
        long s12 = 2097151 & (Load4(wide[31..]) >> 4);
        long s13 = 2097151 & (Load3(wide[34..]) >> 1);
        long s14 = 2097151 & (Load4(wide[36..]) >> 6);
        long s15 = 2097151 & (Load3(wide[39..]) >> 3);
        long s16 = 2097151 & Load3(wide[42..]);
        long s17 = 2097151 & (Load4(wide[44..]) >> 5);
        long s18 = 2097151 & (Load3(wide[47..]) >> 2);
        long s19 = 2097151 & (Load4(wide[49..]) >> 7);
        long s20 = 2097151 & (Load4(wide[52..]) >> 4);
        long s21 = 2097151 & (Load3(wide[55..]) >> 1);
        long s22 = 2097151 & (Load4(wide[57..]) >> 6);
        long s23 = Load4(wide[60..]) >> 3;

        s11 += s23 * 666643;
        s12 += s23 * 470296;
        s13 += s23 * 654183;
        s14 -= s23 * 997805;
        s15 += s23 * 136657;
        s16 -= s23 * 683901;
        s10 += s22 * 666643;
        s11 += s22 * 470296;
        s12 += s22 * 654183;
        s13 -= s22 * 997805;
        s14 += s22 * 136657;
        s15 -= s22 * 683901;
        s9 += s21 * 666643;
        s10 += s21 * 470296;
        s11 += s21 * 654183;
        s12 -= s21 * 997805;
        s13 += s21 * 136657;
        s14 -= s21 * 683901;
        s8 += s20 * 666643;
        s9 += s20 * 470296;
        s10 += s20 * 654183;
        s11 -= s20 * 997805;
        s12 += s20 * 136657;
        s13 -= s20 * 683901;
        s7 += s19 * 666643;
        s8 += s19 * 470296;
        s9 += s19 * 654183;
        s10 -= s19 * 997805;
        s11 += s19 * 136657;
        s12 -= s19 * 683901;
        s6 += s18 * 666643;
        s7 += s18 * 470296;
        s8 += s18 * 654183;
        s9 -= s18 * 997805;
        s10 += s18 * 136657;
        s11 -= s18 * 683901;

        long c;
        c = (s6 + (1L << 20)) >> 21; s7 += c; s6 -= c << 21;
        c = (s8 + (1L << 20)) >> 21; s9 += c; s8 -= c << 21;
        c = (s10 + (1L << 20)) >> 21; s11 += c; s10 -= c << 21;
        c = (s12 + (1L << 20)) >> 21; s13 += c; s12 -= c << 21;
        c = (s14 + (1L << 20)) >> 21; s15 += c; s14 -= c << 21;
        c = (s16 + (1L << 20)) >> 21; s17 += c; s16 -= c << 21;

        c = (s7 + (1L << 20)) >> 21; s8 += c; s7 -= c << 21;
        c = (s9 + (1L << 20)) >> 21; s10 += c; s9 -= c << 21;
        c = (s11 + (1L << 20)) >> 21; s12 += c; s11 -= c << 21;
        c = (s13 + (1L << 20)) >> 21; s14 += c; s13 -= c << 21;
        c = (s15 + (1L << 20)) >> 21; s16 += c; s15 -= c << 21;

        s5 += s17 * 666643;
        s6 += s17 * 470296;
        s7 += s17 * 654183;
        s8 -= s17 * 997805;
        s9 += s17 * 136657;
        s10 -= s17 * 683901;
        s4 += s16 * 666643;
        s5 += s16 * 470296;
        s6 += s16 * 654183;
        s7 -= s16 * 997805;
        s8 += s16 * 136657;
        s9 -= s16 * 683901;
        s3 += s15 * 666643;
        s4 += s15 * 470296;
        s5 += s15 * 654183;
        s6 -= s15 * 997805;
        s7 += s15 * 136657;
        s8 -= s15 * 683901;
        s2 += s14 * 666643;
        s3 += s14 * 470296;
        s4 += s14 * 654183;
        s5 -= s14 * 997805;
        s6 += s14 * 136657;
        s7 -= s14 * 683901;
        s1 += s13 * 666643;
        s2 += s13 * 470296;
        s3 += s13 * 654183;
        s4 -= s13 * 997805;
        s5 += s13 * 136657;
        s6 -= s13 * 683901;
        s0 += s12 * 666643;
        s1 += s12 * 470296;
        s2 += s12 * 654183;
        s3 -= s12 * 997805;
        s4 += s12 * 136657;
        s5 -= s12 * 683901;
        s12 = 0;

        c = (s0 + (1L << 20)) >> 21; s1 += c; s0 -= c << 21;
        c = (s2 + (1L << 20)) >> 21; s3 += c; s2 -= c << 21;
        c = (s4 + (1L << 20)) >> 21; s5 += c; s4 -= c << 21;
        c = (s6 + (1L << 20)) >> 21; s7 += c; s6 -= c << 21;
        c = (s8 + (1L << 20)) >> 21; s9 += c; s8 -= c << 21;
        c = (s10 + (1L << 20)) >> 21; s11 += c; s10 -= c << 21;

        c = (s1 + (1L << 20)) >> 21; s2 += c; s1 -= c << 21;
        c = (s3 + (1L << 20)) >> 21; s4 += c; s3 -= c << 21;
        c = (s5 + (1L << 20)) >> 21; s6 += c; s5 -= c << 21;
        c = (s7 + (1L << 20)) >> 21; s8 += c; s7 -= c << 21;
        c = (s9 + (1L << 20)) >> 21; s10 += c; s9 -= c << 21;
        c = (s11 + (1L << 20)) >> 21; s12 += c; s11 -= c << 21;

        s0 += s12 * 666643;
        s1 += s12 * 470296;
        s2 += s12 * 654183;
        s3 -= s12 * 997805;
        s4 += s12 * 136657;
        s5 -= s12 * 683901;
        s12 = 0;

        c = s0 >> 21; s1 += c; s0 -= c << 21;
        c = s1 >> 21; s2 += c; s1 -= c << 21;
        c = s2 >> 21; s3 += c; s2 -= c << 21;
        c = s3 >> 21; s4 += c; s3 -= c << 21;
        c = s4 >> 21; s5 += c; s4 -= c << 21;
        c = s5 >> 21; s6 += c; s5 -= c << 21;
        c = s6 >> 21; s7 += c; s6 -= c << 21;
        c = s7 >> 21; s8 += c; s7 -= c << 21;
        c = s8 >> 21; s9 += c; s8 -= c << 21;
        c = s9 >> 21; s10 += c; s9 -= c << 21;
        c = s10 >> 21; s11 += c; s10 -= c << 21;
        c = s11 >> 21; s12 += c; s11 -= c << 21;

        s0 += s12 * 666643;
        s1 += s12 * 470296;
        s2 += s12 * 654183;
        s3 -= s12 * 997805;
        s4 += s12 * 136657;
        s5 -= s12 * 683901;

        c = s0 >> 21; s1 += c; s0 -= c << 21;
        c = s1 >> 21; s2 += c; s1 -= c << 21;
        c = s2 >> 21; s3 += c; s2 -= c << 21;
        c = s3 >> 21; s4 += c; s3 -= c << 21;
        c = s4 >> 21; s5 += c; s4 -= c << 21;
        c = s5 >> 21; s6 += c; s5 -= c << 21;
        c = s6 >> 21; s7 += c; s6 -= c << 21;
        c = s7 >> 21; s8 += c; s7 -= c << 21;
        c = s8 >> 21; s9 += c; s8 -= c << 21;
        c = s9 >> 21; s10 += c; s9 -= c << 21;
        c = s10 >> 21; s11 += c; s10 -= c << 21;

        StoreCanonical(output, s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
        // @formatter:on
    }
}
