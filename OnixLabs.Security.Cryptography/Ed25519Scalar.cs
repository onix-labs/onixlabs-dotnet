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
/// Represents scalar arithmetic modulo L, the order of the Ed25519 prime-order base-point subgroup
/// (L = 2^252 + 27,742,317,777,372,353,535,851,937,790,883,648,493, RFC 8032 §5.1).
/// </summary>
/// <remarks>
/// Ported from SUPERCOP ref10 (public domain): scalars are represented as twelve 21-bit limbs
/// during reduction so that the relation L = 2^252 + δ (δ = 27,742,317,777,372,353,535,851,937,790,883,648,493)
/// lets us fold high limbs back into low ones with a fixed sequence of multiply-and-add operations.
/// Every operation is data-oblivious — no branches depend on scalar values, so secret scalars
/// cannot leak through control flow.
/// </remarks>
internal static partial class Ed25519Scalar
{
    /// <summary>
    /// Packs twelve 21-bit limbs into a 32-byte little-endian buffer. Each limb is in [0, 2^21)
    /// after a complete reduction, so this is a fixed byte-shuffle pattern with no data-dependent
    /// branches.
    /// </summary>
    /// <param name="output">The 32-byte destination buffer that receives the packed little-endian scalar.</param>
    /// <param name="s0">The first 21-bit limb.</param>
    /// <param name="s1">The second 21-bit limb.</param>
    /// <param name="s2">The third 21-bit limb.</param>
    /// <param name="s3">The fourth 21-bit limb.</param>
    /// <param name="s4">The fifth 21-bit limb.</param>
    /// <param name="s5">The sixth 21-bit limb.</param>
    /// <param name="s6">The seventh 21-bit limb.</param>
    /// <param name="s7">The eighth 21-bit limb.</param>
    /// <param name="s8">The ninth 21-bit limb.</param>
    /// <param name="s9">The tenth 21-bit limb.</param>
    /// <param name="s10">The eleventh 21-bit limb.</param>
    /// <param name="s11">The twelfth 21-bit limb.</param>
    private static void StoreCanonical(Span<byte> output,
        long s0, long s1, long s2, long s3, long s4, long s5,
        long s6, long s7, long s8, long s9, long s10, long s11)
    {
        output[0] = (byte)(s0 >> 0);
        output[1] = (byte)(s0 >> 8);
        output[2] = (byte)((s0 >> 16) | (s1 << 5));
        output[3] = (byte)(s1 >> 3);
        output[4] = (byte)(s1 >> 11);
        output[5] = (byte)((s1 >> 19) | (s2 << 2));
        output[6] = (byte)(s2 >> 6);
        output[7] = (byte)((s2 >> 14) | (s3 << 7));
        output[8] = (byte)(s3 >> 1);
        output[9] = (byte)(s3 >> 9);
        output[10] = (byte)((s3 >> 17) | (s4 << 4));
        output[11] = (byte)(s4 >> 4);
        output[12] = (byte)(s4 >> 12);
        output[13] = (byte)((s4 >> 20) | (s5 << 1));
        output[14] = (byte)(s5 >> 7);
        output[15] = (byte)((s5 >> 15) | (s6 << 6));
        output[16] = (byte)(s6 >> 2);
        output[17] = (byte)(s6 >> 10);
        output[18] = (byte)((s6 >> 18) | (s7 << 3));
        output[19] = (byte)(s7 >> 5);
        output[20] = (byte)(s7 >> 13);
        output[21] = (byte)(s8 >> 0);
        output[22] = (byte)(s8 >> 8);
        output[23] = (byte)((s8 >> 16) | (s9 << 5));
        output[24] = (byte)(s9 >> 3);
        output[25] = (byte)(s9 >> 11);
        output[26] = (byte)((s9 >> 19) | (s10 << 2));
        output[27] = (byte)(s10 >> 6);
        output[28] = (byte)((s10 >> 14) | (s11 << 7));
        output[29] = (byte)(s11 >> 1);
        output[30] = (byte)(s11 >> 9);
        output[31] = (byte)(s11 >> 17);
    }

    /// <summary>
    /// Reads three little-endian bytes from <paramref name="bytes"/> as a 24-bit unsigned integer.
    /// </summary>
    /// <param name="bytes">The buffer to read from; must contain at least three bytes.</param>
    /// <returns>Returns the 24-bit little-endian unsigned integer value.</returns>
    private static long Load3(ReadOnlySpan<byte> bytes) =>
        bytes[0] | ((long)bytes[1] << 8) | ((long)bytes[2] << 16);

    /// <summary>
    /// Reads four little-endian bytes from <paramref name="bytes"/> as a 32-bit unsigned integer.
    /// </summary>
    /// <param name="bytes">The buffer to read from; must contain at least four bytes.</param>
    /// <returns>Returns the 32-bit little-endian unsigned integer value.</returns>
    private static long Load4(ReadOnlySpan<byte> bytes) =>
        bytes[0] | ((long)bytes[1] << 8) | ((long)bytes[2] << 16) | ((long)bytes[3] << 24);
}
