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
using System.Linq;
using System.Numerics;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core.Numerics;

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
    /// <returns>Returns a <see cref="RawBigDecimal"/> containing the unscaled value and scale.</returns>
    public static RawBigDecimal Convert(float value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode, nameof(mode));

        if (IsZeroOrOne(value, out RawBigDecimal result)) return result;

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
    /// <returns>Returns a <see cref="RawBigDecimal"/> containing the unscaled value and scale.</returns>
    public static RawBigDecimal Convert(double value, ConversionMode mode)
    {
        RequireRealNumber(value);
        RequireIsDefined(mode, nameof(mode));

        if (IsZeroOrOne(value, out RawBigDecimal result)) return result;

        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
    }

    /// <summary>
    /// Converts an IEEE 754 single-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="RawBigDecimal"/> containing the unscaled value and scale.</returns>
    private static RawBigDecimal ConvertFromBinary(float value)
    {
        const int mantissaBits = 23;
        const int exponentBits = 8;
        const int mantissaMask = (1 << mantissaBits) - 1;
        const int exponentMask = (1 << exponentBits) - 1;

        int bits = BitConverter.SingleToInt32Bits(value);
        int mantissa = bits & mantissaMask;
        int exponent = (bits >> mantissaBits) & exponentMask;
        bool denormalizedExponent = exponent is 0;

        exponent -= denormalizedExponent ? 126 : 127;
        exponent -= mantissaBits;

        if (!denormalizedExponent) mantissa |= 1 << mantissaBits;

        while (exponent < 0 && (mantissa & 1) is 0)
        {
            exponent++;
            mantissa >>= 1;
        }

        BigInteger unscaledValue = bits < 0 ? -mantissa : mantissa;
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

        return new RawBigDecimal(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 double-precision binary floating-point number using its binary representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a <see cref="RawBigDecimal"/> containing the unscaled value and scale.</returns>
    private static RawBigDecimal ConvertFromBinary(double value)
    {
        const int mantissaBits = 52;
        const int exponentBits = 11;
        const long mantissaMask = (1L << mantissaBits) - 1;
        const long exponentMask = (1L << exponentBits) - 1;

        long bits = BitConverter.DoubleToInt64Bits(value);
        long mantissa = bits & mantissaMask;
        long exponent = (bits >> mantissaBits) & exponentMask;
        bool denormalizedExponent = exponent is 0;

        exponent -= denormalizedExponent ? 1022 : 1023;
        exponent -= mantissaBits;

        if (!denormalizedExponent) mantissa |= 1L << mantissaBits;

        while (exponent < 0 && (mantissa & 1) is 0)
        {
            exponent++;
            mantissa >>= 1;
        }

        BigInteger unscaledValue = bits < 0 ? -mantissa : mantissa;
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

        return new RawBigDecimal(unscaledValue, scale);
    }

    /// <summary>
    /// Converts an IEEE 754 binary floating-point number using its decimal representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> value.</typeparam>
    /// <returns>Returns a <see cref="RawBigDecimal"/> containing the unscaled value and scale.</returns>
    private static RawBigDecimal ConvertFromDecimal<T>(T value) where T : IBinaryFloatingPointIeee754<T>
    {
        const char zero = '0';
        const string format = "R";
        const string delimiter = "E";
        const StringComparison comparison = StringComparison.CurrentCultureIgnoreCase;

        int exponent = int.Parse(value.ToString("E", NumberFormatInfo.CurrentInfo).SubstringAfterLast(delimiter, comparison: comparison));

        string digits = value
            .ToString(format, NumberFormatInfo.CurrentInfo)
            .SubstringBeforeFirst(delimiter, comparison: comparison)
            .Where(char.IsDigit)
            .JoinToString(string.Empty)
            .PadRight(int.Max(0, exponent + 1), zero);

        BigInteger unscaledValue = BigInteger.Parse(digits) * T.Sign(value);
        int scale = BigInteger.Abs(unscaledValue).ToString().Length - (exponent + 1);

        return new RawBigDecimal(unscaledValue, scale);
    }

    /// <summary>
    /// Determines whether the specified IEEE 754 binary floating-point number is zero or one.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="result">The <see cref="RawBigDecimal"/> result if the number is zero or one.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> value.</typeparam>
    /// <returns>Returns true if the specified IEEE 754 binary floating-point number is zero or one; otherwise, false.</returns>
    private static bool IsZeroOrOne<T>(T value, out RawBigDecimal result) where T : IBinaryFloatingPointIeee754<T>
    {
        if (value == T.Zero || value == T.NegativeZero)
        {
            result = new RawBigDecimal(BigInteger.Zero, default);
            return true;
        }

        if (value == T.One)
        {
            result = new RawBigDecimal(BigInteger.One, default);
            return true;
        }

        if (value == T.NegativeOne)
        {
            result = new RawBigDecimal(BigInteger.MinusOne, default);
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Requires that the specified value is a real number.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <typeparam name="T">The underlying type of the &lt;see cref="IBinaryFloatingPointIeee754{TSelf}"/&gt; value.</typeparam>
    private static void RequireRealNumber<T>(T value) where T : IBinaryFloatingPointIeee754<T>
    {
        Require(!T.IsNaN(value), "Value must not be NaN.", nameof(value));
        Require(!T.IsInfinity(value), "Value must not be infinite.", nameof(value));
    }
}

