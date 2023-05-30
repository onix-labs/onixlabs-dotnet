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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Provides generic mathematical functions.
/// </summary>
internal static class GenericMath
{
    /// <summary>
    /// Calculates the delta, or absolute difference between the specified left-hand and right-hand values.
    /// </summary>
    /// <param name="left">The left-hand value from which to calculate the delta.</param>
    /// <param name="right">The left-hand value from which to calculate the delta.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the delta, or absolute difference between the specified left-hand and right-hand values.</returns>
    public static T Delta<T>(T left, T right) where T : INumber<T>
    {
        return T.Abs(left - right);
    }

    /// <summary>
    /// Calculates the length of the integer part of the specified value.
    /// </summary>
    /// <param name="value">The value for which to calculate the length of the integer part.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the length of the integer part of the specified value.</returns>
    public static int IntegerLength<T>(T value) where T : INumber<T>
    {
        value = T.Abs(value);

        if (value < T.One) return default;

        int length = 1;
        T ten = T.CreateChecked(10);

        while (value >= ten)
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
        if (exponent == 0) return T.One;
        if (exponent == 1) return value;

        T result = value;
        int count = int.Abs(exponent);

        while (--count > 0) result *= value;

        return exponent > 1 ? result : T.One / result;
    }
}
