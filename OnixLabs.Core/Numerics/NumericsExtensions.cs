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
using System.ComponentModel;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Provides extension methods for numeric types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class NumericsExtensions
{
    /// <summary>
    /// Gets the unscaled value of the current <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> from which to obtain an unscaled value.</param>
    /// <returns>Returns the unscaled value of the current <see cref="decimal"/> as a <see cref="BigInteger"/>.</returns>
    public static BigInteger GetUnscaledValue(this decimal value)
    {
        const int mantissaBits = 3;
        int[] bits = decimal.GetBits(value);
        byte[] bytes = new byte[mantissaBits * sizeof(int) + 1];
        Buffer.BlockCopy(bits, 0, bytes, 0, bytes.Length);
        BigInteger result = new(bytes);

        return decimal.IsPositive(value) ? result : -result;
    }

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a <see cref="BigInteger"/> representing the current value.</returns>
    public static BigInteger ToBigInteger<T>(this T value) where T : IBinaryInteger<T>
    {
        return BigInteger.CreateChecked(value);
    }

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the current value should be scaled.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the current value.</returns>
    public static BigDecimal ToBigDecimal<T>(this T value, int scale = default, ScaleMode mode = ScaleMode.Integral)
        where T : IBinaryInteger<T>
    {
        return new BigDecimal(value.ToBigInteger(), scale, mode);
    }

    /// <summary>
    /// Converts the current <see cref="float"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to convert.</param>
    /// <param name="mode">The mode that specifies whether the <see cref="BigDecimal"/> value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="float"/> value.</returns>
    public static BigDecimal ToBigDecimal(this float value, ConversionMode mode = default)
    {
        return new BigDecimal(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="double"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <param name="mode">The mode that specifies whether the <see cref="BigDecimal"/> value should be converted using its binary or decimal representation.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="double"/> value.</returns>
    public static BigDecimal ToBigDecimal(this double value, ConversionMode mode = default)
    {
        return new BigDecimal(value, mode);
    }

    /// <summary>
    /// Converts the current <see cref="decimal"/> value to a <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value to convert.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> representing the current <see cref="decimal"/> value.</returns>
    public static BigDecimal ToBigDecimal(this decimal value)
    {
        return new BigDecimal(value);
    }
}
