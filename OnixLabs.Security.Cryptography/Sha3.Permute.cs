// Copyright © 2020 ONIXLabs
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
using System.Numerics;

namespace OnixLabs.Security.Cryptography;

public abstract partial class Sha3
{
    /// <summary>
    /// Gets the FIPS 202 SHA-3 round constants applied during the iota step of each permutation round.
    /// </summary>
    /// <value>The 24 round constants, in round order.</value>
    private static ReadOnlySpan<ulong> RoundConstants =>
    [
        0x0000000000000001, 0x0000000000008082, 0x800000000000808A, 0x8000000080008000,
        0x000000000000808B, 0x0000000080000001, 0x8000000080008081, 0x8000000000008009,
        0x000000000000008A, 0x0000000000000088, 0x0000000080008009, 0x000000008000000A,
        0x000000008000808B, 0x800000000000008B, 0x8000000000008089, 0x8000000000008003,
        0x8000000000008002, 0x8000000000000080, 0x000000000000800A, 0x800000008000000A,
        0x8000000080008081, 0x8000000000008080, 0x0000000080000001, 0x8000000080008008
    ];

    /// <summary>
    /// Performs the FIPS 202 SHA-3 permutation, Keccak-f[1600].
    /// </summary>
    /// <param name="state">The 25-lane state upon which to perform the permutation.</param>
    /// <remarks>
    /// Why: the 25 lanes are loaded into locals once, permuted for all 24 rounds, then stored once. Working on locals rather than the array
    /// avoids a bounds-checked load and store for every lane access in every step of every round, and lets the JIT keep lanes in registers.
    /// </remarks>
    private static void Permute(ulong[] state)
    {
        Span<ulong> lanes = state.AsSpan(0, StateLanes);

        ulong a00 = lanes[00];
        ulong a01 = lanes[01];
        ulong a02 = lanes[02];
        ulong a03 = lanes[03];
        ulong a04 = lanes[04];
        ulong a05 = lanes[05];
        ulong a06 = lanes[06];
        ulong a07 = lanes[07];
        ulong a08 = lanes[08];
        ulong a09 = lanes[09];
        ulong a10 = lanes[10];
        ulong a11 = lanes[11];
        ulong a12 = lanes[12];
        ulong a13 = lanes[13];
        ulong a14 = lanes[14];
        ulong a15 = lanes[15];
        ulong a16 = lanes[16];
        ulong a17 = lanes[17];
        ulong a18 = lanes[18];
        ulong a19 = lanes[19];
        ulong a20 = lanes[20];
        ulong a21 = lanes[21];
        ulong a22 = lanes[22];
        ulong a23 = lanes[23];
        ulong a24 = lanes[24];

        foreach (ulong roundConstant in RoundConstants)
        {
            // Theta
            ulong c0 = a00 ^ a05 ^ a10 ^ a15 ^ a20;
            ulong c1 = a01 ^ a06 ^ a11 ^ a16 ^ a21;
            ulong c2 = a02 ^ a07 ^ a12 ^ a17 ^ a22;
            ulong c3 = a03 ^ a08 ^ a13 ^ a18 ^ a23;
            ulong c4 = a04 ^ a09 ^ a14 ^ a19 ^ a24;

            ulong d0 = BitOperations.RotateLeft(c1, 1) ^ c4;
            ulong d1 = BitOperations.RotateLeft(c2, 1) ^ c0;
            ulong d2 = BitOperations.RotateLeft(c3, 1) ^ c1;
            ulong d3 = BitOperations.RotateLeft(c4, 1) ^ c2;
            ulong d4 = BitOperations.RotateLeft(c0, 1) ^ c3;

            a00 ^= d0;
            a05 ^= d0;
            a10 ^= d0;
            a15 ^= d0;
            a20 ^= d0;
            a01 ^= d1;
            a06 ^= d1;
            a11 ^= d1;
            a16 ^= d1;
            a21 ^= d1;
            a02 ^= d2;
            a07 ^= d2;
            a12 ^= d2;
            a17 ^= d2;
            a22 ^= d2;
            a03 ^= d3;
            a08 ^= d3;
            a13 ^= d3;
            a18 ^= d3;
            a23 ^= d3;
            a04 ^= d4;
            a09 ^= d4;
            a14 ^= d4;
            a19 ^= d4;
            a24 ^= d4;

            // Rho and pi
            ulong final = BitOperations.RotateLeft(a01, 1);
            a01 = BitOperations.RotateLeft(a06, 44);
            a06 = BitOperations.RotateLeft(a09, 20);
            a09 = BitOperations.RotateLeft(a22, 61);
            a22 = BitOperations.RotateLeft(a14, 39);
            a14 = BitOperations.RotateLeft(a20, 18);
            a20 = BitOperations.RotateLeft(a02, 62);
            a02 = BitOperations.RotateLeft(a12, 43);
            a12 = BitOperations.RotateLeft(a13, 25);
            a13 = BitOperations.RotateLeft(a19, 8);
            a19 = BitOperations.RotateLeft(a23, 56);
            a23 = BitOperations.RotateLeft(a15, 41);
            a15 = BitOperations.RotateLeft(a04, 27);
            a04 = BitOperations.RotateLeft(a24, 14);
            a24 = BitOperations.RotateLeft(a21, 2);
            a21 = BitOperations.RotateLeft(a08, 55);
            a08 = BitOperations.RotateLeft(a16, 45);
            a16 = BitOperations.RotateLeft(a05, 36);
            a05 = BitOperations.RotateLeft(a03, 28);
            a03 = BitOperations.RotateLeft(a18, 21);
            a18 = BitOperations.RotateLeft(a17, 15);
            a17 = BitOperations.RotateLeft(a11, 10);
            a11 = BitOperations.RotateLeft(a07, 6);
            a07 = BitOperations.RotateLeft(a10, 3);
            a10 = final;

            // Chi
            c0 = a00 ^ (~a01 & a02);
            c1 = a01 ^ (~a02 & a03);
            c2 = a02 ^ (~a03 & a04);
            c3 = a03 ^ (~a04 & a00);
            c4 = a04 ^ (~a00 & a01);
            a00 = c0;
            a01 = c1;
            a02 = c2;
            a03 = c3;
            a04 = c4;

            c0 = a05 ^ (~a06 & a07);
            c1 = a06 ^ (~a07 & a08);
            c2 = a07 ^ (~a08 & a09);
            c3 = a08 ^ (~a09 & a05);
            c4 = a09 ^ (~a05 & a06);
            a05 = c0;
            a06 = c1;
            a07 = c2;
            a08 = c3;
            a09 = c4;

            c0 = a10 ^ (~a11 & a12);
            c1 = a11 ^ (~a12 & a13);
            c2 = a12 ^ (~a13 & a14);
            c3 = a13 ^ (~a14 & a10);
            c4 = a14 ^ (~a10 & a11);
            a10 = c0;
            a11 = c1;
            a12 = c2;
            a13 = c3;
            a14 = c4;

            c0 = a15 ^ (~a16 & a17);
            c1 = a16 ^ (~a17 & a18);
            c2 = a17 ^ (~a18 & a19);
            c3 = a18 ^ (~a19 & a15);
            c4 = a19 ^ (~a15 & a16);
            a15 = c0;
            a16 = c1;
            a17 = c2;
            a18 = c3;
            a19 = c4;

            c0 = a20 ^ (~a21 & a22);
            c1 = a21 ^ (~a22 & a23);
            c2 = a22 ^ (~a23 & a24);
            c3 = a23 ^ (~a24 & a20);
            c4 = a24 ^ (~a20 & a21);
            a20 = c0;
            a21 = c1;
            a22 = c2;
            a23 = c3;
            a24 = c4;

            // Iota
            a00 ^= roundConstant;
        }

        lanes[00] = a00;
        lanes[01] = a01;
        lanes[02] = a02;
        lanes[03] = a03;
        lanes[04] = a04;
        lanes[05] = a05;
        lanes[06] = a06;
        lanes[07] = a07;
        lanes[08] = a08;
        lanes[09] = a09;
        lanes[10] = a10;
        lanes[11] = a11;
        lanes[12] = a12;
        lanes[13] = a13;
        lanes[14] = a14;
        lanes[15] = a15;
        lanes[16] = a16;
        lanes[17] = a17;
        lanes[18] = a18;
        lanes[19] = a19;
        lanes[20] = a20;
        lanes[21] = a21;
        lanes[22] = a22;
        lanes[23] = a23;
        lanes[24] = a24;
    }
}
