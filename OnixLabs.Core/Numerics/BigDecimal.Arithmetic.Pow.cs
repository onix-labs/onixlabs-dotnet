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
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Raises the specified <see cref="BigDecimal"/> value by the specified exponent power.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise to the exponent power.</param>
    /// <param name="exponent">The exponent to raise by.</param>
    /// <param name="scale">
    /// Gets the preferred scale for representing powers raised by negative exponents. If the scale is non-negative, then the scale is
    /// fixed to the specified value. If the scale is negative, then the scale will either be the number of digits required to represent
    /// a terminating decimal expansion, or in the event that the decimal expansion is infinite, then the number of digits to represent
    /// the first significant digit to the right of the decimal point.
    /// </param>
    /// <returns>Returns a <see cref="BigDecimal"/> result of raising the value by the specified exponent power.</returns>
    public static BigDecimal Pow(BigDecimal value, int exponent, int scale = -1)
    {
        return exponent switch
        {
            0 => One,
            1 => value,
            > 1 => RaisePositive(value, exponent),
            < 0 => RaiseNegative(value, exponent, scale)
        };
    }

    /// <summary>
    /// Raises the specified <see cref="BigDecimal"/> value by the absolute value of the specified exponent power.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise to the exponent power.</param>
    /// <param name="exponent">The exponent to raise by.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> result of raising the value by the specified exponent power.</returns>
    private static BigDecimal RaisePositive(BigDecimal value, int exponent)
    {
        BigDecimal result = value;
        int count = int.Abs(exponent);

        while (--count > 0)
        {
            result *= value;
        }

        return result;
    }

    /// <summary>
    /// Raises the specified <see cref="BigDecimal"/> value by the specified negative exponent power.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise to the negative exponent power.</param>
    /// <param name="exponent">The negative exponent to raise by.</param>
    /// <param name="maxScale">Determines the maximum scale for values that are raised to the power of a negative exponent.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> result of raising the value by the specified negative exponent power.</returns>
    private static BigDecimal RaiseNegative(BigDecimal value, int exponent, int maxScale)
    {
        BigDecimal result = RaisePositive(value, exponent);
        int scale = GetScale(value, result, exponent, maxScale);
        BigDecimal n = BigInteger.Pow(10, scale).ToBigDecimal(scale);

        result = n / result;

        return value > 0 && value < 1 ? result.IntegralValue : result;
    }

    /// <summary>
    /// Calculates the number of digits required to represent a terminating decimal expansion.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise to the exponent power.</param>
    /// <param name="exponent">The exponent to raise by.</param>
    /// <returns>
    /// Returns the number of digits required to represent a terminating decimal expansion,
    /// or negative 1 (-1) in the event that the decimal expansion is infinite.
    /// </returns>
    private static int GetTerminatingDecimalExpansionDigits(BigDecimal value, int exponent)
    {
        int twos = 0;
        int fives = 0;
        BigDecimal remainder = value;

        while (remainder % 2 == 0)
        {
            twos++;
            remainder /= 2;
        }

        while (remainder % 5 == 0)
        {
            fives++;
            remainder /= 5;
        }

        if (remainder != 1)
        {
            return -1;
        }

        return remainder != 1 ? -1 : int.Abs(Math.Max(twos, fives) * exponent);
    }

    /// <summary>
    /// Gets the preferred scale for representing powers raised by negative exponents. If the scale is non-negative, then the scale is
    /// fixed to the specified value. If the scale is negative, then the scale will either be the number of digits required to represent
    /// a terminating decimal expansion, or in the event that the decimal expansion is infinite, then the number of digits to represent
    /// the first significant digit to the right of the decimal point.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="raisedValue"></param>
    /// <param name="exponent"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    private static int GetScale(BigDecimal value, BigDecimal raisedValue, int exponent, int scale)
    {
        if (scale >= 0)
        {
            return scale;
        }

        int digitsForExpansion = GetTerminatingDecimalExpansionDigits(value, exponent);

        return digitsForExpansion >= 0 ? digitsForExpansion : raisedValue.UnscaledValue.GetDigitLength();
    }
}
