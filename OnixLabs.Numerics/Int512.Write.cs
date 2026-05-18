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

namespace OnixLabs.Numerics;

public readonly partial struct Int512
{
    /// <summary>
    /// The fixed serialised byte count of an <see cref="Int512"/> value in either endianness.
    /// </summary>
    private const int ByteCount = 64;

    /// <summary>
    /// The serialised byte count of each <see cref="UInt256"/> half of an <see cref="Int512"/> value.
    /// </summary>
    private const int HalfByteCount = 32;

    /// <summary>Tries to write the current <see cref="Int512"/> value, in big-endian format, to the specified span.</summary>
    /// <param name="destination">The span into which this instance's value should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes that were written.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteBigEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < ByteCount)
        {
            bytesWritten = 0;
            return false;
        }

        Upper.TryWriteBigEndian(destination, out _);
        Lower.TryWriteBigEndian(destination[HalfByteCount..], out _);
        bytesWritten = ByteCount;
        return true;
    }

    /// <summary>Tries to write the current <see cref="Int512"/> value, in little-endian format, to the specified span.</summary>
    /// <param name="destination">The span into which this instance's value should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes that were written.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < ByteCount)
        {
            bytesWritten = 0;
            return false;
        }

        Lower.TryWriteLittleEndian(destination, out _);
        Upper.TryWriteLittleEndian(destination[HalfByteCount..], out _);
        bytesWritten = ByteCount;
        return true;
    }

    /// <summary>Tries to read an <see cref="Int512"/> value from the specified span in big-endian format.</summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int512 value)
    {
        if (source.Length == 0)
        {
            value = Zero;
            return true;
        }

        bool isNegative = !isUnsigned && (source[0] & 0x80) != 0;

        if (source.Length > ByteCount)
        {
            ReadOnlySpan<byte> overflow = source[..^ByteCount];
            byte signExtension = isNegative ? (byte)0xFF : (byte)0;
            foreach (byte b in overflow)
            {
                if (b != signExtension)
                {
                    value = default;
                    return false;
                }
            }

            source = source[^ByteCount..];
        }

        Span<byte> buffer = stackalloc byte[ByteCount];
        if (isNegative) buffer.Fill(0xFF);
        source.CopyTo(buffer[(ByteCount - source.Length)..]);

        UInt256.TryReadBigEndian(buffer[..HalfByteCount], isUnsigned: true, out UInt256 newUpper);
        UInt256.TryReadBigEndian(buffer[HalfByteCount..], isUnsigned: true, out UInt256 newLower);
        value = new Int512(newUpper, newLower);
        return true;
    }

    /// <summary>Tries to read an <see cref="Int512"/> value from the specified span in little-endian format.</summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int512 value)
    {
        if (source.Length == 0)
        {
            value = Zero;
            return true;
        }

        bool isNegative = !isUnsigned && (source[^1] & 0x80) != 0;

        if (source.Length > ByteCount)
        {
            ReadOnlySpan<byte> overflow = source[ByteCount..];
            byte signExtension = isNegative ? (byte)0xFF : (byte)0;
            foreach (byte b in overflow)
            {
                if (b != signExtension)
                {
                    value = default;
                    return false;
                }
            }

            source = source[..ByteCount];
        }

        Span<byte> buffer = stackalloc byte[ByteCount];
        if (isNegative) buffer.Fill(0xFF);
        source.CopyTo(buffer);

        UInt256.TryReadLittleEndian(buffer[..HalfByteCount], isUnsigned: true, out UInt256 newLower);
        UInt256.TryReadLittleEndian(buffer[HalfByteCount..], isUnsigned: true, out UInt256 newUpper);
        value = new Int512(newUpper, newLower);
        return true;
    }

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteBigEndian"/>
    bool IBinaryInteger<Int512>.TryWriteBigEndian(Span<byte> destination, out int bytesWritten) => TryWriteBigEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian"/>
    bool IBinaryInteger<Int512>.TryWriteLittleEndian(Span<byte> destination, out int bytesWritten) => TryWriteLittleEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian"/>
    static bool IBinaryInteger<Int512>.TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int512 value) => TryReadBigEndian(source, isUnsigned, out value);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian"/>
    static bool IBinaryInteger<Int512>.TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int512 value) => TryReadLittleEndian(source, isUnsigned, out value);
}
