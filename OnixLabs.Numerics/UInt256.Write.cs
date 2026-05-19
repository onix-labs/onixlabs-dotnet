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

public readonly partial struct UInt256
{
    /// <summary>
    /// The fixed serialised byte count of a <see cref="UInt256"/> value in either endianness.
    /// </summary>
    private const int ByteCount = 32;

    /// <summary>
    /// Tries to write the current <see cref="UInt256"/> value, in big-endian format, to the specified span.
    /// </summary>
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

    /// <summary>
    /// Tries to write the current <see cref="UInt256"/> value, in little-endian format, to the specified span.
    /// </summary>
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

    /// <summary>
    /// Tries to read a <see cref="UInt256"/> value from the specified span in big-endian format.
    /// </summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt256 value)
    {
        if (source.Length == 0)
        {
            value = Zero;
            return true;
        }

        if (source.Length > ByteCount)
        {
            ReadOnlySpan<byte> overflow = source[..^ByteCount];
            byte signCheck = isUnsigned ? (byte)0 : (byte)0;
            foreach (byte b in overflow)
            {
                if (b != signCheck)
                {
                    value = default;
                    return false;
                }
            }

            source = source[^ByteCount..];
        }

        Span<byte> buffer = stackalloc byte[ByteCount];
        source.CopyTo(buffer[(ByteCount - source.Length)..]);

        UInt128 newUpper = ReadUInt128BigEndian(buffer);
        UInt128 newLower = ReadUInt128BigEndian(buffer[16..]);
        value = new UInt256(newUpper, newLower);
        return true;
    }

    /// <summary>
    /// Tries to read a <see cref="UInt256"/> value from the specified span in little-endian format.
    /// </summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <param name="value">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was successfully read; otherwise, <see langword="false"/>.</returns>
    public static bool TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt256 value)
    {
        if (source.Length == 0)
        {
            value = Zero;
            return true;
        }

        if (source.Length > ByteCount)
        {
            ReadOnlySpan<byte> overflow = source[ByteCount..];
            byte signCheck = isUnsigned ? (byte)0 : (byte)0;
            foreach (byte b in overflow)
            {
                if (b != signCheck)
                {
                    value = default;
                    return false;
                }
            }

            source = source[..ByteCount];
        }

        Span<byte> buffer = stackalloc byte[ByteCount];
        source.CopyTo(buffer);

        UInt128 newLower = ReadUInt128LittleEndian(buffer);
        UInt128 newUpper = ReadUInt128LittleEndian(buffer[16..]);
        value = new UInt256(newUpper, newLower);
        return true;
    }

    /// <summary>
    /// Reads a <see cref="UInt256"/> value from the specified span in big-endian format.
    /// </summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <returns>Returns the read value.</returns>
    /// <exception cref="OverflowException">Thrown when the source has more significant bytes than fit in a <see cref="UInt256"/>.</exception>
    public static UInt256 ReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned)
    {
        if (!TryReadBigEndian(source, isUnsigned, out UInt256 value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        }
        return value;
    }

    /// <summary>
    /// Reads a <see cref="UInt256"/> value from the specified span in little-endian format.
    /// </summary>
    /// <param name="source">The span from which to read.</param>
    /// <param name="isUnsigned">Indicates whether the source is treated as an unsigned value.</param>
    /// <returns>Returns the read value.</returns>
    /// <exception cref="OverflowException">Thrown when the source has more significant bytes than fit in a <see cref="UInt256"/>.</exception>
    public static UInt256 ReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned)
    {
        if (!TryReadLittleEndian(source, isUnsigned, out UInt256 value))
        {
            throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt256)}.");
        }
        return value;
    }

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteBigEndian"/>
    bool IBinaryInteger<UInt256>.TryWriteBigEndian(Span<byte> destination, out int bytesWritten) => TryWriteBigEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryWriteLittleEndian"/>
    bool IBinaryInteger<UInt256>.TryWriteLittleEndian(Span<byte> destination, out int bytesWritten) => TryWriteLittleEndian(destination, out bytesWritten);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadBigEndian"/>
    static bool IBinaryInteger<UInt256>.TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt256 value) => TryReadBigEndian(source, isUnsigned, out value);

    /// <inheritdoc cref="IBinaryInteger{TSelf}.TryReadLittleEndian"/>
    static bool IBinaryInteger<UInt256>.TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt256 value) => TryReadLittleEndian(source, isUnsigned, out value);

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
