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
    /// Computes (a * b + c) mod L, where all inputs and the output are 32-byte little-endian scalars.
    /// </summary>
    /// <param name="a">The first 32-byte little-endian scalar factor.</param>
    /// <param name="b">The second 32-byte little-endian scalar factor.</param>
    /// <param name="c">The 32-byte little-endian scalar addend.</param>
    /// <param name="output">The 32-byte destination buffer that receives the little-endian canonical scalar.</param>
    public static void MulAdd(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b, ReadOnlySpan<byte> c, Span<byte> output)
    {
        // @formatter:off
        long a0 = 2097151 & Load3(a[0..]);
        long a1 = 2097151 & (Load4(a[2..]) >> 5);
        long a2 = 2097151 & (Load3(a[5..]) >> 2);
        long a3 = 2097151 & (Load4(a[7..]) >> 7);
        long a4 = 2097151 & (Load4(a[10..]) >> 4);
        long a5 = 2097151 & (Load3(a[13..]) >> 1);
        long a6 = 2097151 & (Load4(a[15..]) >> 6);
        long a7 = 2097151 & (Load3(a[18..]) >> 3);
        long a8 = 2097151 & Load3(a[21..]);
        long a9 = 2097151 & (Load4(a[23..]) >> 5);
        long a10 = 2097151 & (Load3(a[26..]) >> 2);
        long a11 = Load4(a[28..]) >> 7;

        long b0 = 2097151 & Load3(b[0..]);
        long b1 = 2097151 & (Load4(b[2..]) >> 5);
        long b2 = 2097151 & (Load3(b[5..]) >> 2);
        long b3 = 2097151 & (Load4(b[7..]) >> 7);
        long b4 = 2097151 & (Load4(b[10..]) >> 4);
        long b5 = 2097151 & (Load3(b[13..]) >> 1);
        long b6 = 2097151 & (Load4(b[15..]) >> 6);
        long b7 = 2097151 & (Load3(b[18..]) >> 3);
        long b8 = 2097151 & Load3(b[21..]);
        long b9 = 2097151 & (Load4(b[23..]) >> 5);
        long b10 = 2097151 & (Load3(b[26..]) >> 2);
        long b11 = Load4(b[28..]) >> 7;

        long c0 = 2097151 & Load3(c[0..]);
        long c1 = 2097151 & (Load4(c[2..]) >> 5);
        long c2 = 2097151 & (Load3(c[5..]) >> 2);
        long c3 = 2097151 & (Load4(c[7..]) >> 7);
        long c4 = 2097151 & (Load4(c[10..]) >> 4);
        long c5 = 2097151 & (Load3(c[13..]) >> 1);
        long c6 = 2097151 & (Load4(c[15..]) >> 6);
        long c7 = 2097151 & (Load3(c[18..]) >> 3);
        long c8 = 2097151 & Load3(c[21..]);
        long c9 = 2097151 & (Load4(c[23..]) >> 5);
        long c10 = 2097151 & (Load3(c[26..]) >> 2);
        long c11 = Load4(c[28..]) >> 7;

        long s0 = c0 + a0 * b0;
        long s1 = c1 + a0 * b1 + a1 * b0;
        long s2 = c2 + a0 * b2 + a1 * b1 + a2 * b0;
        long s3 = c3 + a0 * b3 + a1 * b2 + a2 * b1 + a3 * b0;
        long s4 = c4 + a0 * b4 + a1 * b3 + a2 * b2 + a3 * b1 + a4 * b0;
        long s5 = c5 + a0 * b5 + a1 * b4 + a2 * b3 + a3 * b2 + a4 * b1 + a5 * b0;
        long s6 = c6 + a0 * b6 + a1 * b5 + a2 * b4 + a3 * b3 + a4 * b2 + a5 * b1 + a6 * b0;
        long s7 = c7 + a0 * b7 + a1 * b6 + a2 * b5 + a3 * b4 + a4 * b3 + a5 * b2 + a6 * b1 + a7 * b0;
        long s8 = c8 + a0 * b8 + a1 * b7 + a2 * b6 + a3 * b5 + a4 * b4 + a5 * b3 + a6 * b2 + a7 * b1 + a8 * b0;
        long s9 = c9 + a0 * b9 + a1 * b8 + a2 * b7 + a3 * b6 + a4 * b5 + a5 * b4 + a6 * b3 + a7 * b2 + a8 * b1 + a9 * b0;
        long s10 = c10 + a0 * b10 + a1 * b9 + a2 * b8 + a3 * b7 + a4 * b6 + a5 * b5 + a6 * b4 + a7 * b3 + a8 * b2 + a9 * b1 + a10 * b0;
        long s11 = c11 + a0 * b11 + a1 * b10 + a2 * b9 + a3 * b8 + a4 * b7 + a5 * b6 + a6 * b5 + a7 * b4 + a8 * b3 + a9 * b2 + a10 * b1 + a11 * b0;
        long s12 = a1 * b11 + a2 * b10 + a3 * b9 + a4 * b8 + a5 * b7 + a6 * b6 + a7 * b5 + a8 * b4 + a9 * b3 + a10 * b2 + a11 * b1;
        long s13 = a2 * b11 + a3 * b10 + a4 * b9 + a5 * b8 + a6 * b7 + a7 * b6 + a8 * b5 + a9 * b4 + a10 * b3 + a11 * b2;
        long s14 = a3 * b11 + a4 * b10 + a5 * b9 + a6 * b8 + a7 * b7 + a8 * b6 + a9 * b5 + a10 * b4 + a11 * b3;
        long s15 = a4 * b11 + a5 * b10 + a6 * b9 + a7 * b8 + a8 * b7 + a9 * b6 + a10 * b5 + a11 * b4;
        long s16 = a5 * b11 + a6 * b10 + a7 * b9 + a8 * b8 + a9 * b7 + a10 * b6 + a11 * b5;
        long s17 = a6 * b11 + a7 * b10 + a8 * b9 + a9 * b8 + a10 * b7 + a11 * b6;
        long s18 = a7 * b11 + a8 * b10 + a9 * b9 + a10 * b8 + a11 * b7;
        long s19 = a8 * b11 + a9 * b10 + a10 * b9 + a11 * b8;
        long s20 = a9 * b11 + a10 * b10 + a11 * b9;
        long s21 = a10 * b11 + a11 * b10;
        long s22 = a11 * b11;
        long s23 = 0;

        long cy;
        cy = (s0 + (1L << 20)) >> 21; s1 += cy; s0 -= cy << 21;
        cy = (s2 + (1L << 20)) >> 21; s3 += cy; s2 -= cy << 21;
        cy = (s4 + (1L << 20)) >> 21; s5 += cy; s4 -= cy << 21;
        cy = (s6 + (1L << 20)) >> 21; s7 += cy; s6 -= cy << 21;
        cy = (s8 + (1L << 20)) >> 21; s9 += cy; s8 -= cy << 21;
        cy = (s10 + (1L << 20)) >> 21; s11 += cy; s10 -= cy << 21;
        cy = (s12 + (1L << 20)) >> 21; s13 += cy; s12 -= cy << 21;
        cy = (s14 + (1L << 20)) >> 21; s15 += cy; s14 -= cy << 21;
        cy = (s16 + (1L << 20)) >> 21; s17 += cy; s16 -= cy << 21;
        cy = (s18 + (1L << 20)) >> 21; s19 += cy; s18 -= cy << 21;
        cy = (s20 + (1L << 20)) >> 21; s21 += cy; s20 -= cy << 21;
        cy = (s22 + (1L << 20)) >> 21; s23 += cy; s22 -= cy << 21;

        cy = (s1 + (1L << 20)) >> 21; s2 += cy; s1 -= cy << 21;
        cy = (s3 + (1L << 20)) >> 21; s4 += cy; s3 -= cy << 21;
        cy = (s5 + (1L << 20)) >> 21; s6 += cy; s5 -= cy << 21;
        cy = (s7 + (1L << 20)) >> 21; s8 += cy; s7 -= cy << 21;
        cy = (s9 + (1L << 20)) >> 21; s10 += cy; s9 -= cy << 21;
        cy = (s11 + (1L << 20)) >> 21; s12 += cy; s11 -= cy << 21;
        cy = (s13 + (1L << 20)) >> 21; s14 += cy; s13 -= cy << 21;
        cy = (s15 + (1L << 20)) >> 21; s16 += cy; s15 -= cy << 21;
        cy = (s17 + (1L << 20)) >> 21; s18 += cy; s17 -= cy << 21;
        cy = (s19 + (1L << 20)) >> 21; s20 += cy; s19 -= cy << 21;
        cy = (s21 + (1L << 20)) >> 21; s22 += cy; s21 -= cy << 21;

        s11 += s23 * 666643; s12 += s23 * 470296; s13 += s23 * 654183;
        s14 -= s23 * 997805; s15 += s23 * 136657; s16 -= s23 * 683901;
        s10 += s22 * 666643; s11 += s22 * 470296; s12 += s22 * 654183;
        s13 -= s22 * 997805; s14 += s22 * 136657; s15 -= s22 * 683901;
        s9 += s21 * 666643; s10 += s21 * 470296; s11 += s21 * 654183;
        s12 -= s21 * 997805; s13 += s21 * 136657; s14 -= s21 * 683901;
        s8 += s20 * 666643; s9 += s20 * 470296; s10 += s20 * 654183;
        s11 -= s20 * 997805; s12 += s20 * 136657; s13 -= s20 * 683901;
        s7 += s19 * 666643; s8 += s19 * 470296; s9 += s19 * 654183;
        s10 -= s19 * 997805; s11 += s19 * 136657; s12 -= s19 * 683901;
        s6 += s18 * 666643; s7 += s18 * 470296; s8 += s18 * 654183;
        s9 -= s18 * 997805; s10 += s18 * 136657; s11 -= s18 * 683901;

        cy = (s6 + (1L << 20)) >> 21; s7 += cy; s6 -= cy << 21;
        cy = (s8 + (1L << 20)) >> 21; s9 += cy; s8 -= cy << 21;
        cy = (s10 + (1L << 20)) >> 21; s11 += cy; s10 -= cy << 21;
        cy = (s12 + (1L << 20)) >> 21; s13 += cy; s12 -= cy << 21;
        cy = (s14 + (1L << 20)) >> 21; s15 += cy; s14 -= cy << 21;
        cy = (s16 + (1L << 20)) >> 21; s17 += cy; s16 -= cy << 21;

        cy = (s7 + (1L << 20)) >> 21; s8 += cy; s7 -= cy << 21;
        cy = (s9 + (1L << 20)) >> 21; s10 += cy; s9 -= cy << 21;
        cy = (s11 + (1L << 20)) >> 21; s12 += cy; s11 -= cy << 21;
        cy = (s13 + (1L << 20)) >> 21; s14 += cy; s13 -= cy << 21;
        cy = (s15 + (1L << 20)) >> 21; s16 += cy; s15 -= cy << 21;

        s5 += s17 * 666643; s6 += s17 * 470296; s7 += s17 * 654183;
        s8 -= s17 * 997805; s9 += s17 * 136657; s10 -= s17 * 683901;
        s4 += s16 * 666643; s5 += s16 * 470296; s6 += s16 * 654183;
        s7 -= s16 * 997805; s8 += s16 * 136657; s9 -= s16 * 683901;
        s3 += s15 * 666643; s4 += s15 * 470296; s5 += s15 * 654183;
        s6 -= s15 * 997805; s7 += s15 * 136657; s8 -= s15 * 683901;
        s2 += s14 * 666643; s3 += s14 * 470296; s4 += s14 * 654183;
        s5 -= s14 * 997805; s6 += s14 * 136657; s7 -= s14 * 683901;
        s1 += s13 * 666643; s2 += s13 * 470296; s3 += s13 * 654183;
        s4 -= s13 * 997805; s5 += s13 * 136657; s6 -= s13 * 683901;
        s0 += s12 * 666643; s1 += s12 * 470296; s2 += s12 * 654183;
        s3 -= s12 * 997805; s4 += s12 * 136657; s5 -= s12 * 683901;
        s12 = 0;

        cy = (s0 + (1L << 20)) >> 21; s1 += cy; s0 -= cy << 21;
        cy = (s2 + (1L << 20)) >> 21; s3 += cy; s2 -= cy << 21;
        cy = (s4 + (1L << 20)) >> 21; s5 += cy; s4 -= cy << 21;
        cy = (s6 + (1L << 20)) >> 21; s7 += cy; s6 -= cy << 21;
        cy = (s8 + (1L << 20)) >> 21; s9 += cy; s8 -= cy << 21;
        cy = (s10 + (1L << 20)) >> 21; s11 += cy; s10 -= cy << 21;

        cy = (s1 + (1L << 20)) >> 21; s2 += cy; s1 -= cy << 21;
        cy = (s3 + (1L << 20)) >> 21; s4 += cy; s3 -= cy << 21;
        cy = (s5 + (1L << 20)) >> 21; s6 += cy; s5 -= cy << 21;
        cy = (s7 + (1L << 20)) >> 21; s8 += cy; s7 -= cy << 21;
        cy = (s9 + (1L << 20)) >> 21; s10 += cy; s9 -= cy << 21;
        cy = (s11 + (1L << 20)) >> 21; s12 += cy; s11 -= cy << 21;

        s0 += s12 * 666643; s1 += s12 * 470296; s2 += s12 * 654183;
        s3 -= s12 * 997805; s4 += s12 * 136657; s5 -= s12 * 683901;
        s12 = 0;

        cy = s0 >> 21; s1 += cy; s0 -= cy << 21;
        cy = s1 >> 21; s2 += cy; s1 -= cy << 21;
        cy = s2 >> 21; s3 += cy; s2 -= cy << 21;
        cy = s3 >> 21; s4 += cy; s3 -= cy << 21;
        cy = s4 >> 21; s5 += cy; s4 -= cy << 21;
        cy = s5 >> 21; s6 += cy; s5 -= cy << 21;
        cy = s6 >> 21; s7 += cy; s6 -= cy << 21;
        cy = s7 >> 21; s8 += cy; s7 -= cy << 21;
        cy = s8 >> 21; s9 += cy; s8 -= cy << 21;
        cy = s9 >> 21; s10 += cy; s9 -= cy << 21;
        cy = s10 >> 21; s11 += cy; s10 -= cy << 21;
        cy = s11 >> 21; s12 += cy; s11 -= cy << 21;

        s0 += s12 * 666643; s1 += s12 * 470296; s2 += s12 * 654183;
        s3 -= s12 * 997805; s4 += s12 * 136657; s5 -= s12 * 683901;

        cy = s0 >> 21; s1 += cy; s0 -= cy << 21;
        cy = s1 >> 21; s2 += cy; s1 -= cy << 21;
        cy = s2 >> 21; s3 += cy; s2 -= cy << 21;
        cy = s3 >> 21; s4 += cy; s3 -= cy << 21;
        cy = s4 >> 21; s5 += cy; s4 -= cy << 21;
        cy = s5 >> 21; s6 += cy; s5 -= cy << 21;
        cy = s6 >> 21; s7 += cy; s6 -= cy << 21;
        cy = s7 >> 21; s8 += cy; s7 -= cy << 21;
        cy = s8 >> 21; s9 += cy; s8 -= cy << 21;
        cy = s9 >> 21; s10 += cy; s9 -= cy << 21;
        cy = s10 >> 21; s11 += cy; s10 -= cy << 21;
        // @formatter:on

        StoreCanonical(output, s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
    }
}
