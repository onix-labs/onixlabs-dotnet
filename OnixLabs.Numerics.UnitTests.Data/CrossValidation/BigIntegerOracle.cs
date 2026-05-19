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

using System.Numerics;

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Provides helpers for reducing <see cref="BigInteger"/> results to fixed-width
/// two's-complement or unsigned representations, and for determining whether a
/// <see cref="BigInteger"/> value fits within a fixed-width target type.
/// Used by cross-validation tests that compare extended-width integer operations
/// against <see cref="BigInteger"/> as an oracle.
/// </summary>
public static class BigIntegerOracle
{
    public const int Int256Width = 256;
    public const int UInt256Width = 256;
    public const int Int512Width = 512;
    public const int UInt512Width = 512;

    /// <summary>Reduces <paramref name="value"/> modulo 2^<paramref name="bits"/>, returning a non-negative result.</summary>
    public static BigInteger ReduceUnsigned(BigInteger value, int bits)
    {
        BigInteger modulus = BigInteger.One << bits;
        BigInteger reduced = value % modulus;
        return reduced < 0 ? reduced + modulus : reduced;
    }

    /// <summary>Reduces <paramref name="value"/> to a signed two's-complement representation of <paramref name="bits"/> bits.</summary>
    public static BigInteger ReduceSigned(BigInteger value, int bits)
    {
        BigInteger unsigned = ReduceUnsigned(value, bits);
        BigInteger signBit = BigInteger.One << (bits - 1);
        return unsigned >= signBit ? unsigned - (BigInteger.One << bits) : unsigned;
    }

    /// <summary>Returns true if <paramref name="value"/> fits in a signed two's-complement integer of <paramref name="bits"/> bits.</summary>
    public static bool FitsSigned(BigInteger value, int bits)
    {
        BigInteger min = -(BigInteger.One << (bits - 1));
        BigInteger max = (BigInteger.One << (bits - 1)) - BigInteger.One;
        return value >= min && value <= max;
    }

    /// <summary>Returns true if <paramref name="value"/> fits in an unsigned integer of <paramref name="bits"/> bits.</summary>
    public static bool FitsUnsigned(BigInteger value, int bits)
    {
        BigInteger max = (BigInteger.One << bits) - BigInteger.One;
        return value >= 0 && value <= max;
    }
}
