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

using System.Collections.Generic;

namespace OnixLabs.Security.Cryptography;

public abstract partial class Sha3
{
    /// <summary>
    /// Performs the FIPS 202 SHA-3 permutation.
    /// </summary>
    /// <param name="state">The state upon which to perform the permutation.</param>
    private static void Permute(IList<ulong> state)
    {
        const int hashRounds = 24;

        ulong c0, c1, c2, c3, c4, d0, d1, d2, d3, d4;

        ulong[] roundConstants =
        [
            0x0000000000000001, 0x0000000000008082, 0x800000000000808A, 0x8000000080008000,
            0x000000000000808B, 0x0000000080000001, 0x8000000080008081, 0x8000000000008009,
            0x000000000000008A, 0x0000000000000088, 0x0000000080008009, 0x000000008000000A,
            0x000000008000808B, 0x800000000000008B, 0x8000000000008089, 0x8000000000008003,
            0x8000000000008002, 0x8000000000000080, 0x000000000000800A, 0x800000008000000A,
            0x8000000080008081, 0x8000000000008080, 0x0000000080000001, 0x8000000080008008
        ];

        for (int round = 0; round < hashRounds; round++)
        {
            Theta();
            RhoPi();
            Chi();
            Iota(round);
        }

        return;

        void Theta()
        {
            c0 = state[0] ^ state[5] ^ state[10] ^ state[15] ^ state[20];
            c1 = state[1] ^ state[6] ^ state[11] ^ state[16] ^ state[21];
            c2 = state[2] ^ state[7] ^ state[12] ^ state[17] ^ state[22];
            c3 = state[3] ^ state[8] ^ state[13] ^ state[18] ^ state[23];
            c4 = state[4] ^ state[9] ^ state[14] ^ state[19] ^ state[24];

            d0 = RotateLeft(c1, 1) ^ c4;
            d1 = RotateLeft(c2, 1) ^ c0;
            d2 = RotateLeft(c3, 1) ^ c1;
            d3 = RotateLeft(c4, 1) ^ c2;
            d4 = RotateLeft(c0, 1) ^ c3;

            state[00] ^= d0;
            state[05] ^= d0;
            state[10] ^= d0;
            state[15] ^= d0;
            state[20] ^= d0;
            state[01] ^= d1;
            state[06] ^= d1;
            state[11] ^= d1;
            state[16] ^= d1;
            state[21] ^= d1;
            state[02] ^= d2;
            state[07] ^= d2;
            state[12] ^= d2;
            state[17] ^= d2;
            state[22] ^= d2;
            state[03] ^= d3;
            state[08] ^= d3;
            state[13] ^= d3;
            state[18] ^= d3;
            state[23] ^= d3;
            state[04] ^= d4;
            state[09] ^= d4;
            state[14] ^= d4;
            state[19] ^= d4;
            state[24] ^= d4;
        }

        void RhoPi()
        {
            ulong final = RotateLeft(state[1], 1);

            state[01] = RotateLeft(state[06], 44);
            state[06] = RotateLeft(state[09], 20);
            state[09] = RotateLeft(state[22], 61);
            state[22] = RotateLeft(state[14], 39);
            state[14] = RotateLeft(state[20], 18);
            state[20] = RotateLeft(state[02], 62);
            state[02] = RotateLeft(state[12], 43);
            state[12] = RotateLeft(state[13], 25);
            state[13] = RotateLeft(state[19], 08);
            state[19] = RotateLeft(state[23], 56);
            state[23] = RotateLeft(state[15], 41);
            state[15] = RotateLeft(state[04], 27);
            state[04] = RotateLeft(state[24], 14);
            state[24] = RotateLeft(state[21], 02);
            state[21] = RotateLeft(state[08], 55);
            state[08] = RotateLeft(state[16], 45);
            state[16] = RotateLeft(state[05], 36);
            state[05] = RotateLeft(state[03], 28);
            state[03] = RotateLeft(state[18], 21);
            state[18] = RotateLeft(state[17], 15);
            state[17] = RotateLeft(state[11], 10);
            state[11] = RotateLeft(state[07], 06);
            state[07] = RotateLeft(state[10], 03);
            state[10] = final;
        }

        void Chi()
        {
            for (int i = 0; i < 25; i += 5)
            {
                c0 = state[0 + i] ^ (~state[1 + i] & state[2 + i]);
                c1 = state[1 + i] ^ (~state[2 + i] & state[3 + i]);
                c2 = state[2 + i] ^ (~state[3 + i] & state[4 + i]);
                c3 = state[3 + i] ^ (~state[4 + i] & state[0 + i]);
                c4 = state[4 + i] ^ (~state[0 + i] & state[1 + i]);

                state[0 + i] = c0;
                state[1 + i] = c1;
                state[2 + i] = c2;
                state[3 + i] = c3;
                state[4 + i] = c4;
            }
        }

        void Iota(int round) => state[0] ^= roundConstants[round];

        ulong RotateLeft(ulong x, byte y) => (x << y) | (x >> (64 - y));
    }
}
