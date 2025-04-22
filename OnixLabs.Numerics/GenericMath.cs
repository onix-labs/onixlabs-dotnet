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
/// Provides generic mathematical functions.
/// </summary>
public static class GenericMath
{
    /// <summary>
    /// Computes the delta, or difference between the specified numbers.
    /// </summary>
    /// <param name="left">The left-hand number from which to compute the delta.</param>
    /// <param name="right">The right-hand number from which to compute the delta.</param>
    /// <typeparam name="T">The underlying <see cref="INumberBase{TSelf}"/> type.</typeparam>
    /// <returns>Returns the delta, or difference between the specified numbers.</returns>
    public static T Delta<T>(T left, T right) where T : INumberBase<T> => T.Abs(left - right);

    /// <summary>
    /// Gets the factorial of the specified <see cref="IBinaryInteger{TSelf}"/> value.
    /// </summary>
    /// <param name="value">The value for which the factorial will be computed.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IBinaryInteger{TSelf}"/> value.</typeparam>
    /// <returns>Returns the factorial of the specified <see cref="IBinaryInteger{TSelf}"/> value.</returns>
    public static BigInteger Factorial<T>(T value) where T : IBinaryInteger<T>
    {
        Require(value >= T.Zero, "Value must be greater than or equal to zero.");

        if (value <= T.One) return BigInteger.One;

        BigInteger result = BigInteger.One;

        for (T factor = T.One; factor <= value; factor++)
            result *= factor.ToBigInteger();

        return result;
    }

    /// <summary>
    /// Obtains the length of the integral component of the specified <see cref="INumberBase{TSelf}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="INumberBase{TSelf}"/> value from which to obtain the length of the integral component.</param>
    /// <typeparam name="T">The underlying <see cref="INumberBase{TSelf}"/> type.</typeparam>
    /// <returns>Returns the length of the integral component of the specified <see cref="INumberBase{TSelf}"/> value.</returns>
    public static int IntegerLength<T>(T value) where T : INumberBase<T>
    {
        BigInteger integer = BigInteger.Abs(BigInteger.CreateTruncating(value));
        return integer.ToString("G", CultureInfo.InvariantCulture).Length;
    }

    /// <summary>
    /// Obtains the minimum and maximum values from the specified left-hand and right-hand values.
    /// </summary>
    /// <param name="left">The left-hand <see cref="INumber{TSelf}"/> value to compare.</param>
    /// <param name="right">The right-hand <see cref="INumber{TSelf}"/> value to compare.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the minimum and maximum values from the specified left-hand and right-hand values.</returns>
    public static (T Min, T Max) MinMax<T>(T left, T right) where T : INumber<T> => (T.Min(left, right), T.Max(left, right));

    /// <summary>
    /// Computes 10 raised to the power of a non-negative integer exponent using exponentiation by squaring, for any numeric type implementing <see cref="INumber{T}"/>.
    /// </summary>
    /// <typeparam name="T">The numeric type. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="exponent">The exponent to raise 10 to. Must be greater than or equal to zero.</param>
    /// <returns>A value of type <typeparamref name="T"/> equal to 10 raised to the power of <paramref name="exponent"/>.</returns>
    /// <exception cref="ArgumentException"> if <paramref name="exponent"/> is less than zero.</exception>
    public static T Pow10<T>(int exponent) where T : INumber<T>
    {
        Require(exponent >= 0, "Exponent must be greater than, or equal to zero.", nameof(exponent));

        if (exponent == 0)
            return T.One;

        T result = T.One;
        T baseValue = T.CreateChecked(10);

        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                result *= baseValue;

            baseValue *= baseValue;
            exponent >>= 1;
        }

        return result;
    }
}
