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
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Celsius value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromCelsius(T value) => new(value + CelsiusKelvinOffset);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Delisle value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromDelisle(T value) => new(DelisleKelvinReference - value * T.CreateChecked(2) / T.CreateChecked(3));

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Fahrenheit value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromFahrenheit(T value) => new((value + FahrenheitKelvinOffset) / NineFifths);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Kelvin value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromKelvin(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Newton value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromNewton(T value) => new(value * T.CreateChecked(100) / T.CreateChecked(33) + CelsiusKelvinOffset);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Rankine value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromRankine(T value) => new(value / NineFifths);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Réaumur value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromReaumur(T value) => new(value * T.CreateChecked(1.25) + CelsiusKelvinOffset);

    /// <summary>
    /// Creates a new <see cref="Temperature{T}"/> instance from the specified Rømer value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Temperature{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Temperature{T}"/> instance from the specified value.</returns>
    public static Temperature<T> FromRomer(T value) => new((value - T.CreateChecked(7.5)) * FortyTwentyFirsts + CelsiusKelvinOffset);
}
