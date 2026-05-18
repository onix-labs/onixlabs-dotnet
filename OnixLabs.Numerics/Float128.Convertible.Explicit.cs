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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="sbyte"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="sbyte"/>.</returns>
    public static explicit operator sbyte(Float128 value) => (sbyte)ConvertToSignedSaturating(value, sbyte.MinValue, sbyte.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="sbyte"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="sbyte"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="sbyte"/>.</exception>
    public static explicit operator checked sbyte(Float128 value) => (sbyte)ConvertToSignedChecked(value, sbyte.MinValue, sbyte.MaxValue, nameof(SByte));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="byte"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="byte"/>.</returns>
    public static explicit operator byte(Float128 value) => (byte)ConvertToUnsignedSaturating(value, byte.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="byte"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="byte"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="byte"/>.</exception>
    public static explicit operator checked byte(Float128 value) => (byte)ConvertToUnsignedChecked(value, byte.MaxValue, nameof(Byte));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="short"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="short"/>.</returns>
    public static explicit operator short(Float128 value) => (short)ConvertToSignedSaturating(value, short.MinValue, short.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="short"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="short"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="short"/>.</exception>
    public static explicit operator checked short(Float128 value) => (short)ConvertToSignedChecked(value, short.MinValue, short.MaxValue, nameof(Int16));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="ushort"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="ushort"/>.</returns>
    public static explicit operator ushort(Float128 value) => (ushort)ConvertToUnsignedSaturating(value, ushort.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="ushort"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="ushort"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="ushort"/>.</exception>
    public static explicit operator checked ushort(Float128 value) => (ushort)ConvertToUnsignedChecked(value, ushort.MaxValue, nameof(UInt16));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="int"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="int"/>.</returns>
    public static explicit operator int(Float128 value) => (int)ConvertToSignedSaturating(value, int.MinValue, int.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="int"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="int"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="int"/>.</exception>
    public static explicit operator checked int(Float128 value) => (int)ConvertToSignedChecked(value, int.MinValue, int.MaxValue, nameof(Int32));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="uint"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="uint"/>.</returns>
    public static explicit operator uint(Float128 value) => (uint)ConvertToUnsignedSaturating(value, uint.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="uint"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="uint"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="uint"/>.</exception>
    public static explicit operator checked uint(Float128 value) => (uint)ConvertToUnsignedChecked(value, uint.MaxValue, nameof(UInt32));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="long"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="long"/>.</returns>
    public static explicit operator long(Float128 value) => ConvertToSignedSaturating(value, long.MinValue, long.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="long"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="long"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="long"/>.</exception>
    public static explicit operator checked long(Float128 value) => ConvertToSignedChecked(value, long.MinValue, long.MaxValue, nameof(Int64));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="ulong"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="ulong"/>.</returns>
    public static explicit operator ulong(Float128 value) => ConvertToUnsignedSaturating(value, ulong.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="ulong"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="ulong"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="ulong"/>.</exception>
    public static explicit operator checked ulong(Float128 value) => ConvertToUnsignedChecked(value, ulong.MaxValue, nameof(UInt64));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="Int128"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="Int128"/>.</returns>
    public static explicit operator Int128(Float128 value)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) return Int128.Zero;
        if (status == ExtractionStatus.Infinity || status == ExtractionStatus.Overflow)
        {
            return isNegative ? Int128.MinValue : Int128.MaxValue;
        }

        UInt128 maxPositive = (UInt128)Int128.MaxValue;
        UInt128 maxNegative = ((UInt128)Int128.MaxValue) + UInt128.One;

        if (isNegative)
        {
            if (magnitude > maxNegative) return Int128.MinValue;
            return magnitude == maxNegative ? Int128.MinValue : -(Int128)magnitude;
        }

        if (magnitude > maxPositive) return Int128.MaxValue;
        return (Int128)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an <see cref="Int128"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as an <see cref="Int128"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="Int128"/>.</exception>
    public static explicit operator checked Int128(Float128 value)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) throw new OverflowException($"Cannot convert NaN to {nameof(Int128)}.");
        if (status != ExtractionStatus.Ok) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int128)}.");

        UInt128 maxPositive = (UInt128)Int128.MaxValue;
        UInt128 maxNegative = ((UInt128)Int128.MaxValue) + UInt128.One;

        if (isNegative)
        {
            if (magnitude > maxNegative) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int128)}.");
            return magnitude == maxNegative ? Int128.MinValue : -(Int128)magnitude;
        }

        if (magnitude > maxPositive) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(Int128)}.");
        return (Int128)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="UInt128"/>, saturating on overflow and returning zero for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="UInt128"/>.</returns>
    public static explicit operator UInt128(Float128 value)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) return UInt128.Zero;
        if (status == ExtractionStatus.Infinity) return isNegative ? UInt128.Zero : UInt128.MaxValue;
        if (status == ExtractionStatus.Overflow) return isNegative ? UInt128.Zero : UInt128.MaxValue;
        if (isNegative && magnitude != UInt128.Zero) return UInt128.Zero;
        return magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="UInt128"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="UInt128"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="UInt128"/>.</exception>
    public static explicit operator checked UInt128(Float128 value)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) throw new OverflowException($"Cannot convert NaN to {nameof(UInt128)}.");
        if (status != ExtractionStatus.Ok) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt128)}.");
        if (isNegative && magnitude != UInt128.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt128)}.");
        return magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="char"/>, saturating on overflow and returning <c>(char)0</c> for NaN.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="char"/>.</returns>
    public static explicit operator char(Float128 value) => (char)ConvertToUnsignedSaturating(value, char.MaxValue);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="char"/>, throwing on overflow.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="char"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, negative, or outside the range of <see cref="char"/>.</exception>
    public static explicit operator checked char(Float128 value) => (char)ConvertToUnsignedChecked(value, char.MaxValue, nameof(Char));

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="BigInteger"/>, truncating any fractional part.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the integral part of <paramref name="value"/> as a <see cref="BigInteger"/>.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN or infinite.</exception>
    public static explicit operator BigInteger(Float128 value)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {nameof(BigInteger)}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {nameof(BigInteger)}.");

        Float128 truncated = Truncate(value);
        if (IsZero(truncated)) return BigInteger.Zero;

        DecomposeFinite(truncated.RawBits, out bool sign, out int unbiasedExponent, out UInt128 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        ulong significandHigh = (ulong)(significand >> 64);
        ulong significandLow = (ulong)significand;
        BigInteger significandBig = ((BigInteger)significandHigh << 64) | significandLow;

        BigInteger magnitude = unbiasedExponent >= TrailingSignificandBits
            ? significandBig << (unbiasedExponent - TrailingSignificandBits)
            : significandBig >> (TrailingSignificandBits - unbiasedExponent);

        return sign ? -magnitude : magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="Half"/> by reducing precision and rounding to nearest even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="Half"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator Half(Float128 value) => (Half)(double)value;

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="float"/> by reducing precision and rounding to nearest even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="float"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator float(Float128 value) => (float)(double)value;

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="double"/> by reducing precision and rounding to nearest even.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="double"/> closest to <paramref name="value"/>.</returns>
    public static explicit operator double(Float128 value) => ConvertToDouble(value);

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a <see cref="decimal"/> via <see cref="double"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the <see cref="decimal"/> closest to <paramref name="value"/>, computed through a <see cref="double"/> intermediate.</returns>
    /// <exception cref="OverflowException">Thrown when the value is NaN, infinite, or outside the range of <see cref="decimal"/>.</exception>
    /// <remarks>This conversion is doubly-rounded (binary128 -> binary64 -> decimal). A native conversion will replace this once parsing is in place.</remarks>
    public static explicit operator decimal(Float128 value)
    {
        if (IsNaN(value)) throw new OverflowException($"Cannot convert NaN to {nameof(Decimal)}.");
        if (IsInfinity(value)) throw new OverflowException($"Cannot convert infinity to {nameof(Decimal)}.");
        return (decimal)(double)value;
    }

    /// <summary>
    /// Specifies the outcome of attempting to extract an integer magnitude from a <see cref="Float128"/> value.
    /// </summary>
    private enum ExtractionStatus
    {
        /// <summary>
        /// The integer magnitude was extracted successfully.
        /// </summary>
        Ok,

        /// <summary>
        /// The value is <see cref="Float128.NaN"/> and no integer magnitude can be extracted.
        /// </summary>
        NaN,

        /// <summary>
        /// The value is infinite and no integer magnitude can be extracted.
        /// </summary>
        Infinity,

        /// <summary>
        /// The integer magnitude exceeds the 128-bit range used during extraction.
        /// </summary>
        Overflow
    }

    /// <summary>
    /// Truncates the specified <see cref="Float128"/> value toward zero and extracts the absolute integer magnitude as a <see cref="UInt128"/>.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <param name="magnitude">When this method returns, contains the absolute integer magnitude when the status is <see cref="ExtractionStatus.Ok"/>.</param>
    /// <param name="isNegative">When this method returns, indicates whether the original value was negative.</param>
    /// <returns>An <see cref="ExtractionStatus"/> describing the outcome of the extraction.</returns>
    private static ExtractionStatus ExtractIntegerMagnitude(Float128 value, out UInt128 magnitude, out bool isNegative)
    {
        magnitude = UInt128.Zero;
        isNegative = IsNegative(value);

        if (IsNaN(value)) return ExtractionStatus.NaN;
        if (IsInfinity(value)) return ExtractionStatus.Infinity;

        Float128 truncated = Truncate(value);
        if (IsZero(truncated)) return ExtractionStatus.Ok;

        DecomposeFinite(truncated.RawBits, out _, out int unbiasedExponent, out UInt128 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        if (unbiasedExponent >= 128) return ExtractionStatus.Overflow;

        magnitude = unbiasedExponent >= TrailingSignificandBits
            ? significand << (unbiasedExponent - TrailingSignificandBits)
            : significand >> (TrailingSignificandBits - unbiasedExponent);

        return ExtractionStatus.Ok;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a signed integer, clamping out-of-range values to the supplied bounds.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <param name="minValue">The minimum value of the destination signed integer type.</param>
    /// <param name="maxValue">The maximum value of the destination signed integer type.</param>
    /// <returns>The converted integer value, saturated to <paramref name="minValue"/> or <paramref name="maxValue"/> when out of range, or zero for NaN.</returns>
    private static long ConvertToSignedSaturating(Float128 value, long minValue, long maxValue)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) return 0L;
        if (status == ExtractionStatus.Infinity) return isNegative ? minValue : maxValue;
        if (status == ExtractionStatus.Overflow) return isNegative ? minValue : maxValue;

        UInt128 maxPositiveMagnitude = (UInt128)(ulong)maxValue;
        UInt128 maxNegativeMagnitude = (UInt128)(ulong)(-(minValue + 1L)) + UInt128.One;

        if (isNegative)
        {
            if (magnitude > maxNegativeMagnitude) return minValue;
            return magnitude == maxNegativeMagnitude ? minValue : -(long)(ulong)magnitude;
        }

        return magnitude > maxPositiveMagnitude ? maxValue : (long)(ulong)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to a signed integer, throwing <see cref="OverflowException"/> when the value falls outside the supplied bounds.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <param name="minValue">The minimum value of the destination signed integer type.</param>
    /// <param name="maxValue">The maximum value of the destination signed integer type.</param>
    /// <param name="typeName">The display name of the destination type, used in exception messages.</param>
    /// <returns>The converted integer value when it fits within the supplied range.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, or otherwise outside the representable range.</exception>
    private static long ConvertToSignedChecked(Float128 value, long minValue, long maxValue, string typeName)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) throw new OverflowException($"Cannot convert NaN to {typeName}.");
        if (status != ExtractionStatus.Ok) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");

        UInt128 maxPositiveMagnitude = (UInt128)(ulong)maxValue;
        UInt128 maxNegativeMagnitude = (UInt128)(ulong)(-(minValue + 1L)) + UInt128.One;

        if (isNegative)
        {
            if (magnitude > maxNegativeMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");
            return magnitude == maxNegativeMagnitude ? minValue : -(long)(ulong)magnitude;
        }

        if (magnitude > maxPositiveMagnitude) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");
        return (long)(ulong)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an unsigned integer, clamping out-of-range and negative values to the supplied bounds.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <param name="maxValue">The maximum value of the destination unsigned integer type.</param>
    /// <returns>The converted unsigned integer value, saturated to zero or <paramref name="maxValue"/> when out of range, or zero for NaN.</returns>
    private static ulong ConvertToUnsignedSaturating(Float128 value, ulong maxValue)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) return 0UL;
        if (status == ExtractionStatus.Infinity) return isNegative ? 0UL : maxValue;
        if (status == ExtractionStatus.Overflow) return isNegative ? 0UL : maxValue;
        if (isNegative && magnitude != UInt128.Zero) return 0UL;
        return magnitude > (UInt128)maxValue ? maxValue : (ulong)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to an unsigned integer, throwing <see cref="OverflowException"/> when the value is negative or out of range.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <param name="maxValue">The maximum value of the destination unsigned integer type.</param>
    /// <param name="typeName">The display name of the destination type, used in exception messages.</param>
    /// <returns>The converted unsigned integer value when it fits within the supplied range.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> is NaN, infinite, negative, or otherwise outside the representable range.</exception>
    private static ulong ConvertToUnsignedChecked(Float128 value, ulong maxValue, string typeName)
    {
        ExtractionStatus status = ExtractIntegerMagnitude(value, out UInt128 magnitude, out bool isNegative);

        if (status == ExtractionStatus.NaN) throw new OverflowException($"Cannot convert NaN to {typeName}.");
        if (status != ExtractionStatus.Ok) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");
        if (isNegative && magnitude != UInt128.Zero) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");
        if (magnitude > (UInt128)maxValue) throw new OverflowException($"Value was either too large or too small for the specified type: {typeName}.");
        return (ulong)magnitude;
    }

    /// <summary>
    /// Converts the specified <see cref="Float128"/> value to the nearest <see cref="double"/>, rounding to nearest ties-to-even and saturating to infinity on overflow.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> value to convert.</param>
    /// <returns>The closest representable <see cref="double"/> value.</returns>
    private static double ConvertToDouble(Float128 value)
    {
        if (IsNaN(value)) return double.NaN;
        if (IsPositiveInfinity(value)) return double.PositiveInfinity;
        if (IsNegativeInfinity(value)) return double.NegativeInfinity;
        if (IsZero(value)) return IsNegative(value) ? -0.0 : 0.0;

        DecomposeFinite(value.RawBits, out bool sign, out int unbiasedExponent, out UInt128 significand);
        NormalizeSubnormal(ref significand, ref unbiasedExponent);

        const int doubleBias = 1023;
        const int doubleSignificandBits = 52;
        const int doubleMaxUnbiased = 1023;
        const int doubleMinNormalUnbiased = -1022;
        const int doubleMinSubnormalUnbiased = doubleMinNormalUnbiased - doubleSignificandBits;
        const int rightShiftForNormal = TrailingSignificandBits - doubleSignificandBits;

        if (unbiasedExponent > doubleMaxUnbiased)
        {
            return sign ? double.NegativeInfinity : double.PositiveInfinity;
        }

        if (unbiasedExponent < doubleMinSubnormalUnbiased - 1)
        {
            return sign ? -0.0 : 0.0;
        }

        UInt128 sourceSignificand;
        int targetUnbiasedExponent;
        int totalRightShift;

        if (unbiasedExponent < doubleMinNormalUnbiased)
        {
            int additionalShift = doubleMinNormalUnbiased - unbiasedExponent;
            totalRightShift = rightShiftForNormal + additionalShift;
            targetUnbiasedExponent = doubleMinNormalUnbiased - 1;
            sourceSignificand = significand;
        }
        else
        {
            totalRightShift = rightShiftForNormal;
            targetUnbiasedExponent = unbiasedExponent;
            sourceSignificand = significand;
        }

        UInt128 newSignificand = sourceSignificand >> totalRightShift;
        UInt128 droppedMask = (UInt128.One << totalRightShift) - UInt128.One;
        UInt128 droppedBits = sourceSignificand & droppedMask;
        UInt128 roundBitMask = UInt128.One << (totalRightShift - 1);
        bool roundBit = (droppedBits & roundBitMask) != UInt128.Zero;
        bool stickyBit = (droppedBits & (roundBitMask - UInt128.One)) != UInt128.Zero;

        bool lsb = (newSignificand & UInt128.One) != UInt128.Zero;
        if (roundBit && (stickyBit || lsb))
        {
            newSignificand += UInt128.One;
        }

        ulong significandUlong = (ulong)newSignificand;

        if (targetUnbiasedExponent < doubleMinNormalUnbiased)
        {
            if (significandUlong >= (1UL << doubleSignificandBits))
            {
                targetUnbiasedExponent = doubleMinNormalUnbiased;
                significandUlong &= (1UL << doubleSignificandBits) - 1UL;
            }
        }
        else
        {
            if ((significandUlong & (1UL << (doubleSignificandBits + 1))) != 0UL)
            {
                significandUlong >>= 1;
                targetUnbiasedExponent++;
                if (targetUnbiasedExponent > doubleMaxUnbiased)
                {
                    return sign ? double.NegativeInfinity : double.PositiveInfinity;
                }
            }

            significandUlong &= (1UL << doubleSignificandBits) - 1UL;
        }

        ulong signBit = sign ? 1UL << 63 : 0UL;
        ulong biasedExponentBits;

        if (targetUnbiasedExponent < doubleMinNormalUnbiased)
        {
            biasedExponentBits = 0UL;
        }
        else
        {
            biasedExponentBits = (ulong)(targetUnbiasedExponent + doubleBias) << doubleSignificandBits;
        }

        ulong doubleBits = signBit | biasedExponentBits | significandUlong;
        return BitConverter.UInt64BitsToDouble(doubleBits);
    }
}
