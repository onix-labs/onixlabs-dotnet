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

namespace OnixLabs.Core.Units;

public readonly partial struct Temperature<T>
{
    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Celsius value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Celsius value.</returns>
    public static Temperature<T> FromCelsius(T value)
    {
        return new Temperature<T>(value + T.CreateChecked(273.15));
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Delisle value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Delisle value.</returns>
    public static Temperature<T> FromDelisle(T value)
    {
        return new Temperature<T>(T.CreateChecked(373.15) - value * T.CreateChecked(2) / T.CreateChecked(3));
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Fahrenheit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Fahrenheit value.</returns>
    public static Temperature<T> FromFahrenheit(T value)
    {
        return new Temperature<T>((value + T.CreateChecked(459.67)) * T.CreateChecked(5) / T.CreateChecked(9));
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Kelvin value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Kelvin value.</returns>
    public static Temperature<T> FromKelvin(T value)
    {
        return new Temperature<T>(value);
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Newton value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Newton value.</returns>
    public static Temperature<T> FromNewton(T value)
    {
        return new Temperature<T>(value * (T.CreateChecked(100) / T.CreateChecked(33)) + T.CreateChecked(273.15));
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Reaumur value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Reaumur value.</returns>
    public static Temperature<T> FromReaumur(T value)
    {
        return new Temperature<T>(value * T.CreateChecked(1.25) + T.CreateChecked(273.15));
    }

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Rankine value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified Rankine value.</returns>
    public static Temperature<T> FromRankine(T value)
    {
        return new Temperature<T>(value / T.CreateChecked(1.8));
    }
}
