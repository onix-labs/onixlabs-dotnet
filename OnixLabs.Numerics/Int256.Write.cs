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
using System.Buffers.Binary;
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Int256
{
    /// <summary>
    /// The fixed serialised byte count of an <see cref="Int256"/> value in either endianness.
    /// </summary>
    private const int ByteCount = 32;

    /// <summary>Tries to write the current <see cref="Int256"/> value, in big-endian format, to the specified span.</summary>
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

        WriteUInt128BigEndian(destination, UpperBits);
        WriteUInt128BigEndian(destination[16..], LowerBits);
        bytesWritten = ByteCount;
        return true;
    }

    /// <summary>Tries to write the current <see cref="Int256"/> value, in little-endian format, to the specified span.</summary>
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

        WriteUInt128LittleEndian(destination, LowerBits);
        WriteUInt128LittleEndian(destination[16..], UpperBits);
        bytesWritten = ByteCount;
        return true;
    }

    /// <summary>Tries to read an <see cref="Int256"/> value from the specified span in big-endian format.</summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int256 value)
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

        UInt128 newUpper = ReadUInt128BigEndian(buffer);
        UInt128 newLower = ReadUInt128BigEndian(buffer[16..]);
        value = new Int256(newUpper, newLower);
        return true;
    }

    /// <summary>Tries to read an <see cref="Int256"/> value from the specified span in little-endian format.</summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int256 value)
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

        UInt128 newLower = ReadUInt128LittleEndian(buffer);
        UInt128 newUpper = ReadUInt128LittleEndian(buffer[16..]);
        value = new Int256(newUpper, newLower);
        return true;
    }

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteBigEndian"/>
    bool IBinaryInteger<Int256>.TryWriteBigEndian(Span<byte> destination, out int bytesWritten) => TryWriteBigEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian"/>
    bool IBinaryInteger<Int256>.TryWriteLittleEndian(Span<byte> destination, out int bytesWritten) => TryWriteLittleEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian"/>
    static bool IBinaryInteger<Int256>.TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int256 value) => TryReadBigEndian(source, isUnsigned, out value);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian"/>
    static bool IBinaryInteger<Int256>.TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Int256 value) => TryReadLittleEndian(source, isUnsigned, out value);

    /// <summary>
    /// Writes a <see cref="UInt128"/> value to the specified span in big-endian byte order.
    /// </summary>
    /// <param name="destination">The span into which the value is written; must have at least 16 bytes of capacity.</param>
    /// <param name="value">The <see cref="UInt128"/> value to write.</param>
    private static void WriteUInt128BigEndian(Span<byte> destination, UInt128 value)
    {
        ulong high = (ulong)(value >> 64);
        ulong low = (ulong)value;
        BinaryPrimitives.WriteUInt64BigEndian(destination, high);
        BinaryPrimitives.WriteUInt64BigEndian(destination[sizeof(ulong)..], low);
    }

    /// <summary>
    /// Writes a <see cref="UInt128"/> value to the specified span in little-endian byte order.
    /// </summary>
    /// <param name="destination">The span into which the value is written; must have at least 16 bytes of capacity.</param>
    /// <param name="value">The <see cref="UInt128"/> value to write.</param>
    private static void WriteUInt128LittleEndian(Span<byte> destination, UInt128 value)
    {
        ulong high = (ulong)(value >> 64);
        ulong low = (ulong)value;
        BinaryPrimitives.WriteUInt64LittleEndian(destination, low);
        BinaryPrimitives.WriteUInt64LittleEndian(destination[sizeof(ulong)..], high);
    }

    /// <summary>
    /// Reads a <see cref="UInt128"/> value from the specified span in big-endian byte order.
    /// </summary>
    /// <param name="source">The span from which the value is read; must have at least 16 bytes of content.</param>
    /// <returns>Returns the decoded <see cref="UInt128"/> value.</returns>
    private static UInt128 ReadUInt128BigEndian(ReadOnlySpan<byte> source)
    {
        ulong high = BinaryPrimitives.ReadUInt64BigEndian(source);
        ulong low = BinaryPrimitives.ReadUInt64BigEndian(source[sizeof(ulong)..]);
        return ((UInt128)high << 64) | low;
    }

    /// <summary>
    /// Reads a <see cref="UInt128"/> value from the specified span in little-endian byte order.
    /// </summary>
    /// <param name="source">The span from which the value is read; must have at least 16 bytes of content.</param>
    /// <returns>Returns the decoded <see cref="UInt128"/> value.</returns>
    private static UInt128 ReadUInt128LittleEndian(ReadOnlySpan<byte> source)
    {
        ulong low = BinaryPrimitives.ReadUInt64LittleEndian(source);
        ulong high = BinaryPrimitives.ReadUInt64LittleEndian(source[sizeof(ulong)..]);
        return ((UInt128)high << 64) | low;
    }
}
