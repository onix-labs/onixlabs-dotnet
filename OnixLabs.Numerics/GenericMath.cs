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
    public static T Delta<T>(T left, T right) where T : INumberBase<T>
    {
        return T.Abs(left - right);
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
    public static (T Min, T Max) MinMax<T>(T left, T right) where T : INumber<T>
    {
        T min = T.Min(left, right);
        T max = T.Max(left, right);

        return (min, max);
    }
}
