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

public readonly partial struct Float128
{
    /// <summary>
    /// The number of bytes required to serialize the 128-bit significand in either endianness.
    /// </summary>
    private const int SignificandByteCount = 16;

    /// <summary>
    /// Tries to write the current exponent, in big-endian format, to the specified span.
    /// </summary>
    /// <param name="destination">The span into which the exponent should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <returns>Returns <see langword="true"/> if the exponent was successfully written; otherwise, <see langword="false"/>.</returns>
    public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < sizeof(short))
        {
            bytesWritten = 0;
            return false;
        }

        short exponent = ExtractUnbiasedExponentForSerialization();
        BinaryPrimitives.WriteInt16BigEndian(destination, exponent);
        bytesWritten = sizeof(short);
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
        if (destination.Length < sizeof(short))
        {
            bytesWritten = 0;
            return false;
        }

        short exponent = ExtractUnbiasedExponentForSerialization();
        BinaryPrimitives.WriteInt16LittleEndian(destination, exponent);
        bytesWritten = sizeof(short);
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

        UInt128 significand = ExtractSignificandWithImplicitBit();
        ulong high = (ulong)(significand >> 64);
        ulong low = (ulong)significand;
        BinaryPrimitives.WriteUInt64BigEndian(destination, high);
        BinaryPrimitives.WriteUInt64BigEndian(destination[sizeof(ulong)..], low);
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

        UInt128 significand = ExtractSignificandWithImplicitBit();
        ulong high = (ulong)(significand >> 64);
        ulong low = (ulong)significand;
        BinaryPrimitives.WriteUInt64LittleEndian(destination, low);
        BinaryPrimitives.WriteUInt64LittleEndian(destination[sizeof(ulong)..], high);
        bytesWritten = SignificandByteCount;
        return true;
    }
}
