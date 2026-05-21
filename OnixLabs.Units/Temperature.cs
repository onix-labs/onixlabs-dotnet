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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of temperature.
/// </summary>
/// <remarks>
/// Temperature is an affine quantity — a point on a scale, not a magnitude — so addition and
/// subtraction operators (<c>+</c>, <c>-</c>) are intentionally not defined. Adding two
/// temperatures has no physical meaning (e.g. <c>100°C + 50°C</c> is not <c>150°C</c>; the
/// offset between each scale and Kelvin gets double-counted). To increase or decrease a
/// temperature by a known delta, do the arithmetic on the underlying Kelvin field and
/// reconstruct:
/// <code>
/// var hotter = Temperature&lt;double&gt;.FromKelvin(temperature.Kelvin + deltaInKelvin);
/// </code>
/// To express the difference between two temperatures, subtract their Kelvin readings to obtain
/// a raw delta (which is identical in Kelvin and Celsius, and scaled differently in Fahrenheit
/// or Rankine): <c>var delta = a.Kelvin - b.Kelvin;</c>.
/// </remarks>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Temperature<T> : ICanonicalUnit<T>, IUnit<Temperature<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Temperature{T}"/> struct.
    /// </summary>
    /// <param name="value">The temperature unit in <see cref="Kelvin"/>.</param>
    private Temperature(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>
    /// Gets the temperature in Kelvin (K).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is K.
    /// </remarks>
    public T Kelvin => Canonical;

    /// <summary>
    /// Gets the temperature in Celsius (°C).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is C.
    /// </remarks>
    public T Celsius => Kelvin - CelsiusKelvinOffset;

    /// <summary>
    /// Gets the temperature in Delisle (°De).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is DE.
    /// </remarks>
    public T Delisle => (DelisleKelvinReference - Kelvin) * T.CreateChecked(1.5);

    /// <summary>
    /// Gets the temperature in Fahrenheit (°F).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is F.
    /// </remarks>
    public T Fahrenheit => Kelvin * NineFifths - FahrenheitKelvinOffset;

    /// <summary>
    /// Gets the temperature in Newton (°N).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is N.
    /// </remarks>
    public T Newton => (Kelvin - CelsiusKelvinOffset) * T.CreateChecked(33) / T.CreateChecked(100);

    /// <summary>
    /// Gets the temperature in Rankine (°R).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is R.
    /// </remarks>
    public T Rankine => Kelvin * NineFifths;

    /// <summary>
    /// Gets the temperature in Réaumur (°Ré).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RE.
    /// </remarks>
    public T Reaumur => (Kelvin - CelsiusKelvinOffset) * FourFifths;

    /// <summary>
    /// Gets the temperature in Rømer (°Rø).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RO.
    /// </remarks>
    public T Romer => (Kelvin - CelsiusKelvinOffset) * TwentyOneFortieths + T.CreateChecked(7.5);
}
