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

public readonly partial struct BigDecimal
{
    /// <summary>Tries to write the current exponent, in big-endian format, to a given span.</summary>
    /// <param name="destination">The span to which the current exponent should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination" />.</param>
    /// <returns> Returns <see langword="true" /> if the exponent was successfully written to <paramref name="destination" />; otherwise, <see langword="false" />.</returns>
    bool IFloatingPoint<BigDecimal>.TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
    {
        const int size = sizeof(int);

        if (destination.Length >= size && BinaryPrimitives.TryWriteInt32BigEndian(destination, number.Exponent))
        {
            bytesWritten = size;
            return true;
        }

        bytesWritten = 0;
        return false;
    }

    /// <summary>Tries to write the current exponent, in little-endian format, to a given span.</summary>
    /// <param name="destination">The span to which the current exponent should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination" />.</param>
    /// <returns> Returns <see langword="true" /> if the exponent was successfully written to <paramref name="destination" />; otherwise, <see langword="false" />.</returns>
    bool IFloatingPoint<BigDecimal>.TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        const int size = sizeof(int);

        if (destination.Length >= size && BinaryPrimitives.TryWriteInt32LittleEndian(destination, number.Exponent))
        {
            bytesWritten = size;
            return true;
        }

        bytesWritten = 0;
        return false;
    }

    /// <summary>Tries to write the current significand, in big-endian format, to a given span.</summary>
    /// <param name="destination">The span to which the current significand should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination" />.</param>
    /// <returns> Returns <see langword="true" /> if the significand was successfully written to <paramref name="destination" />; otherwise, <see langword="false" />.</returns>
    bool IFloatingPoint<BigDecimal>.TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten) =>
        number.Significand.TryWriteBytes(destination, out bytesWritten, isBigEndian: true);

    /// <summary>Tries to write the current significand, in little-endian format, to a given span.</summary>
    /// <param name="destination">The span to which the current significand should be written.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written to <paramref name="destination" />.</param>
    /// <returns> Returns <see langword="true" /> if the significand was successfully written to <paramref name="destination" />; otherwise, <see langword="false" />.</returns>
    bool IFloatingPoint<BigDecimal>.TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten) =>
        number.Significand.TryWriteBytes(destination, out bytesWritten);
}
