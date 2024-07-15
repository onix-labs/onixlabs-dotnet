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

    // /// <summary>
    // /// The length of the hash in bits.
    // /// </summary>
    // private readonly int bitLength;

    /// <summary>
    /// The state block size.
    /// </summary>
    private int blockSize;

    /// <summary>
    /// The state input pointer.
    /// </summary>
    private int inputPointer;

    /// <summary>
    /// The state output pointer.
    /// </summary>
    private int outputPointer;

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
    }

    /// <summary>
    /// Initializes an implementation of the <see cref="Sha3"/> class.
    /// </summary>
    public override void Initialize()
    {
        // ReSharper disable HeapView.ObjectAllocation.Evident
        blockSize = default;
        inputPointer = default;
        outputPointer = default;
        state = new ulong[25];
        result = new byte[HashSize / 8];
    }

    /// <summary>
    /// Routes data written to the object into the hash algorithm for computing the hash.
    /// </summary>
    /// <param name="array">The input to compute the hash code for.</param>
    /// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
    /// <param name="cbSize">The number of bytes in the byte array to use as data.</param>
    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
        Initialize();

        while (cbSize > 0)
        {
            blockSize = Math.Min(cbSize, rateBytes);

            for (int index = ibStart; index < blockSize; index++)
            {
                byte value = Convert.ToByte(Buffer.GetByte(state, index) ^ array[index + inputPointer]);
                Buffer.SetByte(state, index, value);
            }

            inputPointer += blockSize;
            cbSize -= blockSize;

            if (blockSize != rateBytes) continue;
            Permute(state);
            blockSize = 0;
        }
    }

    /// <summary>
    /// Finalizes the hash computation after the last data is processed by the cryptographic stream object.
    /// </summary>
    /// <returns>The computed hash code.</returns>
    protected override byte[] HashFinal()
    {
        byte pad = Convert.ToByte(Buffer.GetByte(state, blockSize) ^ delimiter);
        Buffer.SetByte(state, blockSize, pad);

        if ((delimiter & 0x80) != 0 && blockSize == rateBytes - 1)
        {
            Permute(state);
        }

        pad = Convert.ToByte(Buffer.GetByte(state, rateBytes - 1) ^ 0x80);
        Buffer.SetByte(state, rateBytes - 1, pad);
        Permute(state);

        int outputBytesLeft = HashSize / 8;

        while (outputBytesLeft > 0)
        {
            blockSize = Math.Min(outputBytesLeft, rateBytes);
            Buffer.BlockCopy(state, 0, result, outputPointer, blockSize);
            outputPointer += blockSize;
            outputBytesLeft -= blockSize;

            if (outputBytesLeft > 0)
            {
                Permute(state);
            }
        }

        return result;
    }
}
