// Copyright 2020-2023 ONIXLabs
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
using System.Runtime.CompilerServices;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Represents conversion from IEEE-754 binary floating point numbers into unscaled values and scale.
/// </summary>
internal static class Ieee754Converter
{
    private const string ExValueMustNotBeNaN = "Value must not be NaN.";
    private const string ExValueMustNotBeInfinite = "Value must not be infinite.";
    private const string ExConversionModeBinaryOrDecimal = "Conversion mode must be Binary or Decimal.";

    /// <summary>
    /// Converts a single-precision, binary floating point number into an unscaled value and scale.
    /// </summary>
    /// <param name="mode">The conversion mode for converting the single-precision, binary floating point number.</param>
    /// <param name="value">The single-precision, binary floating point number to convert.</param>
    /// <returns>Returns the unscaled value and scale representation of the specified single-precision, binary floating point number.</returns>
    public static (BigInteger UnscaledValue, int Scale) Convert(float value, ConversionMode mode)
    {
        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, ExConversionModeBinaryOrDecimal)
        };
    }

    /// <summary>
    /// Converts a double-precision, binary floating point number into an unscaled value and scale.
    /// </summary>
    /// <param name="mode">The conversion mode for converting the double-precision, binary floating point number.</param>
    /// <param name="value">The double-precision, binary floating point number to convert.</param>
    /// <returns>Returns the unscaled value and scale representation of the specified double-precision, binary floating point number.</returns>
    public static (BigInteger UnscaledValue, int Scale) Convert(double value, ConversionMode mode)
    {
        return mode switch
        {
            ConversionMode.Binary => ConvertFromBinary(value),
            ConversionMode.Decimal => ConvertFromDecimal(value),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, ExConversionModeBinaryOrDecimal)
        };
    }

    /// <summary>
    /// Converts a single-precision, binary floating point number into an unscaled value and scale.
    /// </summary>
    /// <param name="value">The single-precision, binary floating point number to convert.</param>
    /// <returns>Returns the unscaled value and scale representation of the specified single-precision, binary floating point number.</returns>
    private static (BigInteger UnscaledValue, int Scale) ConvertFromBinary(float value)
    {
        if (IsZeroOrOne(value, out (BigInteger UnscaledValue, int Scale) result)) return result;

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
        int scale = default;

        if (exponent < 0)
        {
            scale = int.Abs(exponent);
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= exponent;
        }

        return (unscaledValue, scale);
    }

    /// <summary>
    /// Converts a double-precision, binary floating point number into an unscaled value and scale.
    /// </summary>
    /// <param name="value">The double-precision, binary floating point number to convert.</param>
    /// <returns>Returns the unscaled value and scale representation of the specified double-precision, binary floating point number.</returns>
    private static (BigInteger UnscaledValue, int Scale) ConvertFromBinary(double value)
    {
        if (IsZeroOrOne(value, out (BigInteger UnscaledValue, int Scale) result)) return result;

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
        int scale = default;

        if (exponent < 0)
        {
            scale = (int)long.Abs(exponent);
            unscaledValue *= BigInteger.Pow(5, scale);
        }
        else
        {
            unscaledValue <<= (int)exponent;
        }

        return (unscaledValue, scale);
    }

    /// <summary>
    /// Converts a <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number into an unscaled value and scale.
    /// </summary>
    /// <param name="value">The <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number to convert.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number.</typeparam>
    /// <returns>Returns the unscaled value and scale representation of the specified <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number.</returns>
    private static (BigInteger UnscaledValue, int Scale) ConvertFromDecimal<T>(T value) where T : IBinaryFloatingPointIeee754<T>
    {
        if (IsZeroOrOne(value, out (BigInteger UnscaledValue, int Scale) result)) return result;

        const char zero = '0';
        const string format = "R";
        const string delimiter = "E";
        const StringComparison comparison = StringComparison.CurrentCultureIgnoreCase;

        int sign = T.Sign(value);
        int exponent = int.Parse(value.ToString("E", NumberFormatInfo.CurrentInfo).SubstringAfterLast(delimiter, comparison: comparison));

        string digits = value
            .ToString(format, NumberFormatInfo.CurrentInfo)
            .SubstringBeforeFirst(delimiter, comparison: comparison)
            .Where(char.IsDigit)
            .JoinToString(string.Empty)
            .PadRight(int.Max(0, exponent + 1), zero);

        BigInteger unscaledValue = BigInteger.Parse(digits) * sign;
        int length = GenericMath.IntegerLength(unscaledValue);
        int scale = length - (exponent + 1);

        return (unscaledValue, scale);
    }

    /// <summary>
    /// Performs sanity checks for all conversion mechanisms.
    /// NaN and Infinity cannot be converted.
    /// Zero and One don't require any conversion.
    /// </summary>
    /// <param name="value">The <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number to check.</param>
    /// <param name="result">The result if the specified <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number was zero or one.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number.</typeparam>
    /// <returns>Returns true if the <see cref="IBinaryFloatingPointIeee754{TSelf}"/> number is zero or one; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool IsZeroOrOne<T>(T value, out (BigInteger UnscaledValue, int Scale) result) where T : IBinaryFloatingPointIeee754<T>
    {
        Require(!T.IsNaN(value), ExValueMustNotBeNaN, nameof(value));
        Require(!T.IsInfinity(value), ExValueMustNotBeInfinite, nameof(value));

        if (value == T.Zero || value == T.NegativeZero)
        {
            result = (BigInteger.Zero, default);
            return true;
        }

        if (value == T.One)
        {
            result = (BigInteger.One, default);
            return true;
        }

        if (value == T.NegativeOne)
        {
            result = (BigInteger.MinusOne, default);
            return true;
        }

        result = default;
        return false;
    }
}
