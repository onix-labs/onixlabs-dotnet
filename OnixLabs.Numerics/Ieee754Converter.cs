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
using System.Globalization;
using System.Numerics;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents conversion of IEEE 754 binary floating-point numbers.
/// </summary>
internal static class Ieee754Converter
{
    /// <summary>
    /// Converts an IEEE 754 single-precision binary floating-point number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The mode that specifies whether the value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    public static NumberInfo Convert(float value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode);

        if (IsZeroOrOne(value, out NumberInfo result)) return result;

        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
    }

    /// <summary>
    /// Converts an IEEE 754 double-precision binary floating-point number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The mode that specifies whether the value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    public static NumberInfo Convert(double value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode);

        if (IsZeroOrOne(value, out NumberInfo result)) return result;

        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
    }

    /// <summary>
    /// Converts an IEEE 754 quadruple-precision binary floating-point number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The mode that specifies whether the value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    public static NumberInfo Convert(Float128 value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode);

        if (IsZeroOrOne(value, out NumberInfo result)) return result;

        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
    }

    /// <summary>
    /// Converts an IEEE 754 octuple-precision binary floating-point number.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="mode">The mode that specifies whether the value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    public static NumberInfo Convert(Float256 value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode);

        if (IsZeroOrOne(value, out NumberInfo result)) return result;

        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
    }

    /// <summary>
    /// Converts an IEEE 754 quadruple-precision binary floating-point number to the nearest <see cref="decimal"/>, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="decimal"/> closest to <paramref name="value"/>, carrying the largest scale whose mantissa fits in 96 bits, with trailing fractional zeros removed.</returns>
    /// <exception cref="OverflowException">Thrown when the magnitude of <paramref name="value"/> rounds to 2^96 or above.</exception>
    public static decimal ToDecimal(Float128 value)
    {
        RequireRealNumber(value);

        if (Float128.IsZero(value)) return decimal.Zero;

        Float128.DecomposeFinite(value.Bits, out bool sign, out int unbiasedExponent, out UInt128 significand);
        Float128.NormalizeSubnormal(ref significand, ref unbiasedExponent);

        return ToDecimal(sign, significand, unbiasedExponent - Float128.TrailingSignificandBits);
    }

    /// <summary>
    /// Converts an IEEE 754 octuple-precision binary floating-point number to the nearest <see cref="decimal"/>, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="decimal"/> closest to <paramref name="value"/>, carrying the largest scale whose mantissa fits in 96 bits, with trailing fractional zeros removed.</returns>
    /// <exception cref="OverflowException">Thrown when the magnitude of <paramref name="value"/> rounds to 2^96 or above.</exception>
    public static decimal ToDecimal(Float256 value)
    {
        RequireRealNumber(value);

        if (Float256.IsZero(value)) return decimal.Zero;

        Float256.DecomposeFinite(value.Bits, out bool sign, out int unbiasedExponent, out UInt256 significand);
        Float256.NormalizeSubnormal(ref significand, ref unbiasedExponent);

        return ToDecimal(sign, significand, unbiasedExponent - Float256.TrailingSignificandBits);
    }

    /// <summary>
    /// The exclusive upper bound of the 96-bit <see cref="decimal"/> mantissa.
    /// </summary>
    private static readonly BigInteger DecimalMantissaLimit = BigInteger.One << 96;

    /// <summary>
    /// Converts the binary value <c>significand × 2^exponent</c> to the nearest <see cref="decimal"/>, rounding to nearest, ties-to-even.
    /// </summary>
    /// <param name="sign"><see langword="true"/> if the value is negative; otherwise, <see langword="false"/>.</param>
    /// <param name="significand">The absolute, non-zero binary significand.</param>
    /// <param name="exponent">The power of two that scales <paramref name="significand"/>.</param>
    /// <returns>Returns the <see cref="decimal"/> closest to the value, using the largest scale whose rounded mantissa fits in 96 bits.</returns>
    /// <exception cref="OverflowException">Thrown when the magnitude of the value rounds to 2^96 or above.</exception>
    private static decimal ToDecimal(bool sign, BigInteger significand, int exponent)
    {
        const int maxScale = 28;
        const double log10Of2 = 0.30102999566398120;

        if (exponent >= 0)
        {
            BigInteger integer = significand << exponent;
            if (integer >= DecimalMantissaLimit) throw DecimalOverflow();
            return CreateDecimal(sign, integer, scale: 0);
        }

        int fractionBits = -exponent;
        BigInteger remainderMask = (BigInteger.One << fractionBits) - BigInteger.One;
        BigInteger half = BigInteger.One << (fractionBits - 1);

        int magnitudeExponent = (int)significand.GetBitLength() - 1 - fractionBits;
        if (magnitudeExponent >= 96) throw DecimalOverflow();

        // Why: the value lies in [2^m, 2^(m+1)), so the largest scale whose mantissa fits below 2^96 is never above
        // floor((95 - m) × log10(2)) + 1; starting there and stepping down costs at most two extra multiplications instead of up to 28.
        int scale = Math.Min(maxScale, (int)Math.Floor((95 - magnitudeExponent) * log10Of2) + 1);

        while (true)
        {
            BigInteger numerator = significand * BigIntegerPowersOfTen.Get(scale);
            BigInteger quotient = numerator >> fractionBits;
            BigInteger remainder = numerator & remainderMask;

            if (remainder > half || (remainder == half && !quotient.IsEven)) quotient += BigInteger.One;

            if (quotient < DecimalMantissaLimit) return quotient.IsZero ? decimal.Zero : CreateDecimal(sign, quotient, scale);
            if (scale == 0) throw DecimalOverflow();

            scale--;
        }
    }

    /// <summary>
    /// Creates a <see cref="decimal"/> from a 96-bit mantissa and scale, removing trailing fractional zeros so that the result carries its shortest scale.
    /// </summary>
    /// <param name="sign"><see langword="true"/> if the value is negative; otherwise, <see langword="false"/>.</param>
    /// <param name="mantissa">The absolute mantissa, which must be below 2^96.</param>
    /// <param name="scale">The number of fractional decimal digits, between 0 and 28.</param>
    /// <returns>Returns the <see cref="decimal"/> value <c>(-1)^sign × mantissa × 10^-scale</c>.</returns>
    private static decimal CreateDecimal(bool sign, BigInteger mantissa, int scale)
    {
        while (scale > 0)
        {
            BigInteger reduced = BigInteger.DivRem(mantissa, 10, out BigInteger remainder);
            if (!remainder.IsZero) break;

            mantissa = reduced;
            scale--;
        }

        int low = (int)(uint)(mantissa & uint.MaxValue);
        int mid = (int)(uint)((mantissa >> 32) & uint.MaxValue);
        int high = (int)(uint)(mantissa >> 64);

        return new decimal(low, mid, high, sign, (byte)scale);
    }

    /// <summary>
    /// Creates the <see cref="OverflowException"/> thrown when a value does not fit in a <see cref="decimal"/>.
    /// </summary>
    /// <returns>Returns a new <see cref="OverflowException"/> describing the <see cref="decimal"/> overflow.</returns>
    private static OverflowException DecimalOverflow() => new($"Value was either too large or too small for the specified type: {nameof(Decimal)}.");

    /// <summary>
    /// Converts an IEEE 754 single-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    private static NumberInfo ConvertFromBinary(float value)
    {
        const int significandBits = 23;
        const int exponentBits = 8;
        const int significandMask = (1 << significandBits) - 1;
        const int exponentMask = (1 << exponentBits) - 1;

        int bits = BitConverter.SingleToInt32Bits(value);
        int significand = bits & significandMask;
        int exponent = (bits >> significandBits) & exponentMask;
        bool denormalizedExponent = exponent is 0;

        exponent -= denormalizedExponent ? 126 : 127;
        exponent -= significandBits;

        if (!denormalizedExponent) significand |= 1 << significandBits;

        while (exponent < 0 && (significand & 1) is 0)
        {
            exponent++;
            significand >>= 1;
        }

        BigInteger unscaledValue = bits < 0 ? -significand : significand;
        int scale = 0;

        if (exponent < 0)
        {
            scale = int.Abs(exponent);
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= exponent;
        }

        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 double-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    private static NumberInfo ConvertFromBinary(double value)
    {
        const int significandBits = 52;
        const int exponentBits = 11;
        const long significandMask = (1L << significandBits) - 1;
        const long exponentMask = (1L << exponentBits) - 1;

        long bits = BitConverter.DoubleToInt64Bits(value);
        long significand = bits & significandMask;
        long exponent = (bits >> significandBits) & exponentMask;
        bool denormalizedExponent = exponent is 0;

        exponent -= denormalizedExponent ? 1022 : 1023;
        exponent -= significandBits;

        if (!denormalizedExponent) significand |= 1L << significandBits;

        while (exponent < 0 && (significand & 1) is 0)
        {
            exponent++;
            significand >>= 1;
        }

        BigInteger unscaledValue = bits < 0 ? -significand : significand;
        int scale = 0;

        if (exponent < 0)
        {
            scale = (int)long.Abs(exponent);
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= (int)exponent;
        }

        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 quadruple-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    private static NumberInfo ConvertFromBinary(in Float128 value)
    {
        Float128.DecomposeFinite(value.Bits, out bool sign, out int unbiasedExponent, out UInt128 significand);
        Float128.NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int exponent = unbiasedExponent - Float128.TrailingSignificandBits;
        BigInteger unscaledValue = significand;

        while (exponent < 0 && unscaledValue.IsEven)
        {
            exponent++;
            unscaledValue >>= 1;
        }

        if (sign) unscaledValue = -unscaledValue;
        int scale = 0;

        if (exponent < 0)
        {
            scale = -exponent;
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= exponent;
        }

        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 octuple-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    private static NumberInfo ConvertFromBinary(in Float256 value)
    {
        Float256.DecomposeFinite(value.Bits, out bool sign, out int unbiasedExponent, out UInt256 significand);
        Float256.NormalizeSubnormal(ref significand, ref unbiasedExponent);

        int exponent = unbiasedExponent - Float256.TrailingSignificandBits;
        BigInteger unscaledValue = significand;

        while (exponent < 0 && unscaledValue.IsEven)
        {
            exponent++;
            unscaledValue >>= 1;
        }

        if (sign) unscaledValue = -unscaledValue;
        int scale = 0;

        if (exponent < 0)
        {
            scale = -exponent;
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= exponent;
        }

        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 binary floating-point number using its decimal representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> value.</typeparam>
    /// <returns>Returns a <see cref="NumberInfo"/> containing the unscaled value and scale.</returns>
    private static NumberInfo ConvertFromDecimal<T>(T value) where T : IBinaryFloatingPointIeee754<T>
    {
        // The "R" (round-trip) format yields the shortest decimal that maps back to the same binary value, which
        // is exactly the Dragon4 representation we want. Parsing it with the invariant culture reuses the tested
        // NumberInfo parser, which correctly applies the round-trip exponent to both the magnitude and the scale.
        return NumberInfo.Parse(value.ToString("R", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Determines whether the specified IEEE 754 binary floating-point number is zero or one.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="result">The <see cref="NumberInfo"/> result if the number is zero or one.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> value.</typeparam>
    /// <returns>Returns <see langword="true"/> if the specified IEEE 754 binary floating-point number is zero or one; otherwise, <see langword="false"/>.</returns>
    private static bool IsZeroOrOne<T>(T value, out NumberInfo result) where T : IBinaryFloatingPointIeee754<T>
    {
        if (value == T.Zero || value == T.NegativeZero)
        {
            result = NumberInfo.Zero;
            return true;
        }

        if (value == T.One)
        {
            result = NumberInfo.One;
            return true;
        }

        if (value == T.NegativeOne)
        {
            result = NumberInfo.NegativeOne;
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Requires that the specified value is a real number.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryFloatingPointIeee754{TSelf}"/> type.</typeparam>
    private static void RequireRealNumber<T>(T value) where T : IBinaryFloatingPointIeee754<T>
    {
        Require(!T.IsNaN(value), "Value must not be NaN.", nameof(value));
        Require(!T.IsInfinity(value), "Value must not be infinite.", nameof(value));
    }
}
