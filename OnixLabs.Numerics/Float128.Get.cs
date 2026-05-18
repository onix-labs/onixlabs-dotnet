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

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Gets the number of bytes that will be written by <see cref="TryWriteExponentBigEndian"/> or <see cref="TryWriteExponentLittleEndian"/>.
    /// </summary>
    /// <returns>Returns the number of bytes needed to write the exponent in either endianness.</returns>
    public int GetExponentByteCount() => sizeof(short);

    /// <summary>
    /// Gets the length, in bits, of the shortest two's-complement representation of the current exponent.
    /// </summary>
    /// <returns>Returns the shortest bit length of the current exponent.</returns>
    public int GetExponentShortestBitLength()
    {
        short exponent = ExtractUnbiasedExponentForSerialization();
        return exponent >= 0
            ? (sizeof(short) * 8) - short.LeadingZeroCount(exponent)
            : (sizeof(short) * 8) + 1 - short.LeadingZeroCount((short)(~exponent));
    }

    /// <summary>
    /// Gets the length, in bits, of the significand of the current <see cref="Float128"/> value.
    /// </summary>
    /// <returns>Returns the bit length of the significand, including the implicit leading bit for normal values.</returns>
    public int GetSignificandBitLength() => SignificandPrecision;

    /// <summary>
    /// Gets the number of bytes that will be written by <see cref="TryWriteSignificandBigEndian"/> or <see cref="TryWriteSignificandLittleEndian"/>.
    /// </summary>
    /// <returns>Returns the number of bytes needed to write the significand in either endianness.</returns>
    public int GetSignificandByteCount() => 16;

    /// <summary>
    /// Extracts the unbiased exponent of this <see cref="Float128"/> in the form expected by the serialization helpers, treating subnormals as the minimum normal exponent.
    /// </summary>
    /// <returns>The unbiased exponent value used when serializing the significand and exponent pair.</returns>
    private short ExtractUnbiasedExponentForSerialization()
    {
        uint biasedExponent = ExtractBiasedExponent(Bits);
        return biasedExponent == 0u
            ? (short)MinNormalUnbiasedExponent
            : (short)((int)biasedExponent - ExponentBias);
    }

    private UInt128 ExtractSignificandWithImplicitBit()
    {
        UInt128 trailing = ExtractTrailingSignificand(Bits);
        uint biasedExponent = ExtractBiasedExponent(Bits);
        return biasedExponent == 0u ? trailing : (ImplicitSignificandBit | trailing);
    }
}
