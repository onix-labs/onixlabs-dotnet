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
    /// Computes the delta, or absolute difference, between the specified numbers.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> number from which to compute the delta.</param>
    /// <param name="right">The <paramref name="right"/> number from which to compute the delta.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the absolute difference between the specified numbers.</returns>
    /// <remarks>
    /// The difference is computed in order (subtracting the smaller operand from the larger), so the result is correct for
    /// unsigned integer types, which would otherwise wrap when <paramref name="left"/> is less than <paramref name="right"/>.
    /// </remarks>
    public static T Delta<T>(T left, T right) where T : INumber<T> => left < right ? right - left : left - right;

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

        // Iterate over a BigInteger counter rather than T: a T counter overflows when value is T.MaxValue
        // (e.g. byte 255 + 1 wraps to 0), which would loop forever.
        BigInteger maximum = value.ToBigInteger();
        BigInteger result = BigInteger.One;

        for (BigInteger factor = 2; factor <= maximum; factor++)
            result *= factor;

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
    /// Obtains the minimum and maximum values from the specified <paramref name="left"/> and <paramref name="right"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> <see cref="INumber{TSelf}"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> <see cref="INumber{TSelf}"/> value to compare.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the minimum and maximum values from the specified <paramref name="left"/> and <paramref name="right"/> values.</returns>
    public static (T Min, T Max) MinMax<T>(T left, T right) where T : INumber<T> => (T.Min(left, right), T.Max(left, right));

    /// <summary>
    /// Computes <paramref name="value"/> raised to the power of a non-negative integer <paramref name="exponent"/> using exponentiation by squaring, for any numeric type implementing <see cref="INumber{T}"/>.
    /// </summary>
    /// <typeparam name="T">The numeric type. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The base value to be raised.</param>
    /// <param name="exponent">The exponent to which <paramref name="value"/> is raised. Must be greater than or equal to zero.</param>
    /// <returns>Returns a value of type <typeparamref name="T"/> equal to <paramref name="value"/> raised to the power of <paramref name="exponent"/>, computed at <typeparamref name="T"/>'s precision.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="exponent"/> is less than zero.</exception>
    /// <remarks>
    /// Computing the power in <typeparamref name="T"/> preserves precision at the target type. Routing through
    /// a <see cref="double"/> intermediary (for example <c>T.CreateChecked(Math.Pow(10, exponent))</c>) caps
    /// accuracy at double's ~15–17 decimal digits regardless of how precise <typeparamref name="T"/> is.
    /// </remarks>
    public static T Pow<T>(T value, int exponent) where T : INumber<T>
    {
        Require(exponent >= 0, "Exponent must be greater than, or equal to zero.", nameof(exponent));

        if (exponent == 0)
            return T.One;

        T result = T.One;
        T baseValue = value;

        while (exponent > 0)
        {
            if ((exponent & 1) == 1)
                result *= baseValue;

            baseValue *= baseValue;
            exponent >>= 1;
        }

        return result;
    }

    /// <summary>
    /// Computes 10 raised to the power of a non-negative integer exponent using exponentiation by squaring, for any numeric type implementing <see cref="INumber{T}"/>.
    /// </summary>
    /// <typeparam name="T">The numeric type. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="exponent">The exponent to raise 10 to. Must be greater than or equal to zero.</param>
    /// <returns>Returns a value of type <typeparamref name="T"/> equal to 10 raised to the power of <paramref name="exponent"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="exponent"/> is less than zero.</exception>
    public static T Pow10<T>(int exponent) where T : INumber<T> => Pow(T.CreateChecked(10), exponent);
}
