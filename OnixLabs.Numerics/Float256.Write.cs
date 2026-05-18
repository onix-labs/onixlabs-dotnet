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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>
    /// The number of bytes required to serialize a <see cref="Float256"/> significand in either endianness.
    /// </summary>
    private const int SignificandByteCount = 32;

    /// <summary>
    /// Tries to write the current exponent, in big-endian format, to the specified span.
    /// </summary>
    /// <param name="destination">The span into which the exponent should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <returns>Returns <see langword="true"/> if the exponent was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < sizeof(int))
        {
            bytesWritten = 0;
            return false;
        }

        int exponent = ExtractUnbiasedExponentForSerialization();
        BinaryPrimitives.WriteInt32BigEndian(destination, exponent);
        bytesWritten = sizeof(int);
        return true;
    }

    /// <summary>
    /// Tries to write the current exponent, in little-endian format, to the specified span.
    /// </summary>
    /// <param name="destination">The span into which the exponent should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <returns>Returns <see langword="true"/> if the exponent was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < sizeof(int))
        {
            bytesWritten = 0;
            return false;
        }

        int exponent = ExtractUnbiasedExponentForSerialization();
        BinaryPrimitives.WriteInt32LittleEndian(destination, exponent);
        bytesWritten = sizeof(int);
        return true;
    }

    /// <summary>
    /// Tries to write the current significand, in big-endian format, to the specified span.
    /// </summary>
    /// <param name="destination">The span into which the significand should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <returns>Returns <see langword="true"/> if the significand was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < SignificandByteCount)
        {
            bytesWritten = 0;
            return false;
        }

        UInt256 significand = ExtractSignificandWithImplicitBit();
        WriteUInt128BigEndian(destination, significand.UpperBits);
        WriteUInt128BigEndian(destination[16..], significand.LowerBits);
        bytesWritten = SignificandByteCount;
        return true;
    }

    /// <summary>
    /// Tries to write the current significand, in little-endian format, to the specified span.
    /// </summary>
    /// <param name="destination">The span into which the significand should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <returns>Returns <see langword="true"/> if the significand was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < SignificandByteCount)
        {
            bytesWritten = 0;
            return false;
        }

        UInt256 significand = ExtractSignificandWithImplicitBit();
        WriteUInt128LittleEndian(destination, significand.LowerBits);
        WriteUInt128LittleEndian(destination[16..], significand.UpperBits);
        bytesWritten = SignificandByteCount;
        return true;
    }

    /// <summary>
    /// Writes the specified <see cref="UInt128"/> value to the destination span in big-endian byte order.
    /// </summary>
    /// <param name="destination">The span into which the value should be written.</param>
    /// <param name="value">The <see cref="UInt128"/> value to write.</param>
    private static void WriteUInt128BigEndian(Span<byte> destination, UInt128 value)
    {
        ulong high = (ulong)(value >> 64);
        ulong low = (ulong)value;
        BinaryPrimitives.WriteUInt64BigEndian(destination, high);
        BinaryPrimitives.WriteUInt64BigEndian(destination[sizeof(ulong)..], low);
    }

    /// <summary>
    /// Writes the specified <see cref="UInt128"/> value to the destination span in little-endian byte order.
    /// </summary>
    /// <param name="destination">The span into which the value should be written.</param>
    /// <param name="value">The <see cref="UInt128"/> value to write.</param>
    private static void WriteUInt128LittleEndian(Span<byte> destination, UInt128 value)
    {
        ulong high = (ulong)(value >> 64);
        ulong low = (ulong)value;
        BinaryPrimitives.WriteUInt64LittleEndian(destination, low);
        BinaryPrimitives.WriteUInt64LittleEndian(destination[sizeof(ulong)..], high);
    }
}
