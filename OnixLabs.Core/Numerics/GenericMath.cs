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

/// <summary>
/// Provides generic mathematical functions.
/// </summary>
public static class GenericMath
{
    /// <summary>
    /// Calculates the greatest common denominator of the specified values. 
    /// </summary>
    /// <param name="left">The left-hand value for which to calculate the greatest common denominator.</param>
    /// <param name="right">The right-hand value for which to calculate the greatest common denominator.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the greatest common denominator of the specified values. </returns>
    public static T GreatestCommonDenominator<T>(T left, T right) where T : INumber<T>
    {
        while (!T.IsZero(right))
        {
            T copy = left;
            left = right;
            right = copy % right;
        }

        return left;
    }

    /// <summary>
    /// Calculates the division of the specified dividend by the specified divisor using integer division.
    /// </summary>
    /// <param name="dividend">The dividend to divide.</param>
    /// <param name="divisor">The divisor to divide by.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the division of the specified dividend by the specified divisor using integer division.</returns>
    /// <exception cref="DivideByZeroException">if the specified divisor is zero.</exception>
    public static T IntegerDivide<T>(T dividend, T divisor) where T : INumber<T>
    {
        if (divisor == T.Zero)
        {
            throw new DivideByZeroException("Divisor cannot be zero.");
        }

        if (divisor == T.One)
        {
            return dividend;
        }

        if (divisor == -T.One)
        {
            return -dividend;
        }

        T dividendSign = T.IsNegative(dividend) ? -T.One : T.One;
        T divisorSign = T.IsNegative(divisor) ? -T.One : T.One;
        T sign = dividendSign * divisorSign;

        dividend = T.Abs(dividend);
        divisor = T.Abs(divisor);

        dividend -= dividend % divisor;
        return dividend / divisor * sign;
    }

    /// <summary>
    /// Calculates the length of the integer part of the specified value.
    /// </summary>
    /// <param name="value">The value for which to calculate the length of the integer part.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the length of the integer part of the specified value.</returns>
    public static int IntegerLength<T>(T value) where T : INumber<T>
    {
        int length = 1;
        T ten = T.CreateChecked(10);

        value = T.Abs(value);

        while (value >= ten || value <= -ten)
        {
            value /= ten;
            length++;
        }

        return length;
    }

    /// <summary>
    /// Calculates the power of the specified value, raised by the specified exponent.
    /// </summary>
    /// <param name="value">The value to raise to the power of the specified exponent.</param>
    /// <param name="exponent">The specified exponent to raise the value by.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the power of the specified value, raised by the specified exponent.</returns>
    public static T Pow<T>(T value, int exponent) where T : INumber<T>
    {
        if (exponent == 0)
        {
            return T.One;
        }

        if (exponent == 1)
        {
            return value;
        }

        T result = value;
        int count = int.Abs(exponent);

        while (--count > 0)
        {
            result *= value;
        }

        if (exponent > 1)
        {
            return result;
        }

        return T.One / result;
    }
}
