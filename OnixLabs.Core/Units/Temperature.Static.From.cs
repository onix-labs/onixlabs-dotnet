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

using System.Numerics;

namespace OnixLabs.Core.Units;

public static partial class Temperature
{
    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Celsius value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Celsius value.</returns>
    public static Temperature<T> FromCelsius<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromCelsius(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Delisle value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Delisle value.</returns>
    public static Temperature<T> FromDelisle<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromDelisle(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Fahrenheit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Fahrenheit value.</returns>
    public static Temperature<T> FromFahrenheit<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromFahrenheit(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Kelvin value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Kelvin value.</returns>
    public static Temperature<T> FromKelvin<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromKelvin(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Newton value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Newton value.</returns>
    public static Temperature<T> FromNewton<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromNewton(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Reaumur value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Reaumur value.</returns>
    public static Temperature<T> FromReaumur<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromReaumur(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Rankine value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="Temperature{T}"/> unit.</typeparam>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Rankine value.</returns>
    public static Temperature<T> FromRankine<T>(T value) where T : IFloatingPoint<T>
    {
        return Temperature<T>.FromRankine(value);
    }
}
