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
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the abstract base class for the FIPS 202 SHA-3 family of hash algorithms.
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
    /// The number of 64-bit lanes in the sponge state.
    /// </summary>
    private const int StateLanes = 25;

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
    /// The permutable sponge state, as 25 64-bit lanes.
    /// </summary>
    /// <remarks>
    /// Why: allocated once per instance and cleared by <see cref="Initialize"/>, which <see cref="HashAlgorithm"/> calls after every hash.
    /// Absorbing and squeezing address the lanes as bytes in memory order, which matches the little-endian lane encoding FIPS 202 specifies on
    /// little-endian platforms, as the byte-wise implementation this replaced also assumed.
    /// </remarks>
    private readonly ulong[] state = new ulong[StateLanes];

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
    /// Gets the sponge state as bytes, in memory order.
    /// </summary>
    /// <value>The sponge state as bytes, in memory order.</value>
    private Span<byte> StateBytes => MemoryMarshal.AsBytes(state.AsSpan());

    /// <inheritdoc/>
    public sealed override void Initialize()
    {
        blockSize = 0;
        Array.Clear(state);
    }

    /// <inheritdoc/>
    protected override void HashCore(byte[] array, int ibStart, int cbSize) => HashCore(array.AsSpan(ibStart, cbSize));

    /// <inheritdoc/>
    protected override void HashCore(ReadOnlySpan<byte> source)
    {
        Span<byte> stateBytes = StateBytes;

        while (!source.IsEmpty)
        {
            int bytesToProcess = Math.Min(source.Length, rateBytes - blockSize);

            Xor(stateBytes.Slice(blockSize, bytesToProcess), source[..bytesToProcess]);

            blockSize += bytesToProcess;
            source = source[bytesToProcess..];

            if (blockSize != rateBytes) continue;

            Permute(state);
            blockSize = 0;
        }
    }

    /// <inheritdoc/>
    protected override byte[] HashFinal()
    {
        byte[] result = new byte[HashSize / 8];
        TryHashFinal(result, out int _);
        return result;
    }

    /// <inheritdoc/>
    protected override bool TryHashFinal(Span<byte> destination, out int bytesWritten)
    {
        int hashSizeBytes = HashSize / 8;

        if (destination.Length < hashSizeBytes)
        {
            bytesWritten = 0;
            return false;
        }

        Span<byte> stateBytes = StateBytes;

        // Apply padding to the current block
        stateBytes[blockSize] ^= (byte)delimiter;

        // If the delimiter has its highest bit set, and we're at the last byte of the block, permute the state
        if ((delimiter & 0x80) != 0 && blockSize == rateBytes - 1) Permute(state);

        // Apply final padding and permute the state
        stateBytes[rateBytes - 1] ^= 0x80;
        Permute(state);

        // Squeeze the output from the state, permuting between rate-sized blocks
        Span<byte> output = destination[..hashSizeBytes];

        while (true)
        {
            int bytesToOutput = Math.Min(output.Length, rateBytes);
            stateBytes[..bytesToOutput].CopyTo(output);
            output = output[bytesToOutput..];

            if (output.IsEmpty) break;

            Permute(state);
        }

        bytesWritten = hashSizeBytes;
        return true;
    }

    /// <summary>
    /// Combines each byte of the destination with the corresponding byte of the source, using exclusive-or.
    /// </summary>
    /// <param name="destination">The bytes to update.</param>
    /// <param name="source">The bytes to combine into the destination, which must be no longer than the destination.</param>
    /// <remarks>
    /// Why: the sponge absorbs up to 168 bytes per block, so the bytes are combined eight at a time. Unaligned reads and writes keep this correct
    /// at any block offset, and reading both sides in memory order keeps it byte-for-byte identical to a byte loop on any platform.
    /// </remarks>
    private static void Xor(Span<byte> destination, ReadOnlySpan<byte> source)
    {
        int index = 0;

        for (; index <= source.Length - sizeof(ulong); index += sizeof(ulong))
        {
            ulong value = MemoryMarshal.Read<ulong>(destination[index..]) ^ MemoryMarshal.Read<ulong>(source[index..]);
            MemoryMarshal.Write(destination[index..], in value);
        }

        for (; index < source.Length; index++)
            destination[index] ^= source[index];
    }
}
