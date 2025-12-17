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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Computes the FIPS 202 SHA-3 hash for the input data.
/// </summary>
public abstract partial class Sha3 : HashAlgorithm
{
    /// <summary>
    /// The delimiter used for hash implementations.
    /// </summary>
    protected const int HashDelimiter = 0x06;

    /// <summary>
    /// The delimiter used for Shake implementations.
    /// </summary>
    protected const int ShakeDelimiter = 0x1f;

    /// <summary>
    /// The rate in bytes of the sponge state.
    /// </summary>
    private readonly int rateBytes;

    /// <summary>
    /// The state delimiter.
    /// </summary>
    private readonly int delimiter;

    /// <summary>
    /// The state block size.
    /// </summary>
    private int blockSize;

    /// <summary>
    /// The permutable sponge state.
    /// </summary>
    private ulong[] state = [];

    /// <summary>
    /// The hash result.
    /// </summary>
    private byte[] result = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Sha3"/> class.
    /// </summary>
    /// <param name="rateBytes">The rate in bytes of the sponge state.</param>
    /// <param name="delimiter">The state delimiter.</param>
    /// <param name="bitLength">The output length of the hash in bits.</param>
    protected Sha3(int rateBytes, int delimiter, int bitLength)
    {
        this.rateBytes = rateBytes;
        this.delimiter = delimiter;
        HashSizeValue = bitLength;

        Initialize();
    }

    /// <inheritdoc/>
    public sealed override void Initialize()
    {
        // ReSharper disable HeapView.ObjectAllocation.Evident
        blockSize = 0;
        state = new ulong[25];
        result = new byte[HashSize / 8];
    }

    /// <inheritdoc/>
    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
        int offset = ibStart;

        while (cbSize > 0)
        {
            // Calculate the number of bytes we can process in this iteration
            int bytesToProcess = Math.Min(cbSize, rateBytes - blockSize);

            // Absorb the input into the state
            for (int i = 0; i < bytesToProcess; i++)
            {
                int stateIndex = blockSize + i;
                byte value = Convert.ToByte(Buffer.GetByte(state, stateIndex) ^ array[offset + i]);
                Buffer.SetByte(state, stateIndex, value);
            }

            // Update the block size and offsets
            blockSize += bytesToProcess;
            offset += bytesToProcess;
            cbSize -= bytesToProcess;

            // If the block isn't full, continue...
            if (blockSize != rateBytes) continue;

            // ...otherwise, permute the state
            Permute(state);
            blockSize = 0;
        }
    }

    /// <inheritdoc/>
    protected override byte[] HashFinal()
    {
        // Apply padding to the current block
        byte pad = Convert.ToByte(Buffer.GetByte(state, blockSize) ^ delimiter);
        Buffer.SetByte(state, blockSize, pad);

        // If the delimiter has its highest bit set, and we're at the last byte of the block, permute the state
        if ((delimiter & 0x80) != 0 && blockSize == rateBytes - 1) Permute(state);

        // Apply final padding and permute the state
        pad = Convert.ToByte(Buffer.GetByte(state, rateBytes - 1) ^ 0x80);
        Buffer.SetByte(state, rateBytes - 1, pad);
        Permute(state);

        int outputBytesLeft = HashSize / 8;
        int outputOffset = 0; // Local variable to track the offset in the result array

        // Extract the hash output from the state
        while (outputBytesLeft > 0)
        {
            int bytesToOutput = Math.Min(outputBytesLeft, rateBytes);
            Buffer.BlockCopy(state, 0, result, outputOffset, bytesToOutput);
            outputOffset += bytesToOutput;
            outputBytesLeft -= bytesToOutput;

            if (outputBytesLeft > 0) Permute(state);
        }

        return result;
    }
}
