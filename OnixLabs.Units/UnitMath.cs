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

namespace OnixLabs.Units;

/// <summary>
/// Provides extension methods for <see cref="IFloatingPoint{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class UnitMath
{
    /// <summary>
    /// Computes <paramref name="value"/> raised to the power of a non-negative integer <paramref name="exponent"/> using exponentiation by squaring, for any numeric type implementing <see cref="INumber{T}"/>.
    /// </summary>
    /// <typeparam name="T">The numeric type. Must implement <see cref="INumber{T}"/>.</typeparam>
    /// <param name="value">The base value to be raised.</param>
    /// <param name="exponent">The exponent to which <paramref name="value"/> is raised. Must be greater than or equal to zero.</param>
    /// <returns>Returns a value of type <typeparamref name="T"/> equal to <paramref name="value"/> raised to the power of <paramref name="exponent"/>, computed at <typeparamref name="T"/>'s precision.</returns>
    /// <exception cref="ArgumentException">If <paramref name="exponent"/> is less than zero.</exception>
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
    /// <exception cref="ArgumentException">If <paramref name="exponent"/> is less than zero.</exception>
    public static T Pow10<T>(int exponent) where T : INumber<T> => Pow(T.CreateChecked(10), exponent);
}
