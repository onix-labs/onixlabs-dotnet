// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

public readonly partial struct Temperature<T>
{
    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Celsius value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromCelsius(T value) => new(value + T.CreateChecked(273.15));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Delisle value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromDelisle(T value) => new(T.CreateChecked(373.15) - value * (T.CreateChecked(2.00) / T.CreateChecked(3.00)));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Fahrenheit value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromFahrenheit(T value) => new((value + T.CreateChecked(459.67)) / T.CreateChecked(1.80));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Kelvin value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromKelvin(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Newton value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromNewton(T value) => new(value * (T.CreateChecked(100.00) / T.CreateChecked(33.00)) + T.CreateChecked(273.15));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Rankine value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromRankine(T value) => new(value / T.CreateChecked(1.8));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Réaumur value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromReaumur(T value) => new(value * T.CreateChecked(1.25) + T.CreateChecked(273.15));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from a Rømer value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>A newly created <see cref="Temperature{T}"/> instance.</returns>
    public static Temperature<T> FromRomer(T value) => new((value - T.CreateChecked(7.5)) * T.CreateChecked(40.0 / 21.0) + T.CreateChecked(273.15));
}
