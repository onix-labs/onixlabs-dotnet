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

using System;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of temperature. The default value is absolute zero, or 0 K.
/// </summary>
public readonly partial struct Temperature<T> :
    IValueEquatable<Temperature<T>>,
    IValueComparable<Temperature<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Temperature{T}" /> struct.
    /// </summary>
    /// <param name="value">The value in Kelvin with which to initialize the current <see cref="Temperature{T}" /> instance.</param>
    private Temperature(T value) => Kelvin = value;

    /// <summary>
    /// Gets the temperature in Kelvin.
    /// </summary>
    public T Kelvin { get; }

    /// <summary>
    /// Gets the temperature in Celsius.
    /// </summary>
    public T Celsius => Kelvin - T.CreateChecked(273.15);

    /// <summary>
    /// Gets the temperature in Delisle.
    /// </summary>
    public T Delisle => (T.CreateChecked(373.15) - Kelvin) * T.CreateChecked(1.50);

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public T Fahrenheit => Kelvin * T.CreateChecked(1.80) - T.CreateChecked(459.67);

    /// <summary>
    /// Gets the temperature in Newton.
    /// </summary>
    public T Newton => (Kelvin - T.CreateChecked(273.15)) * T.CreateChecked(33.00) / T.CreateChecked(100.00);

    /// <summary>
    /// Gets the temperature in Rankine.
    /// </summary>
    public T Rankine => Kelvin * T.CreateChecked(1.8);

    /// <summary>
    /// Gets the temperature in Réaumur.
    /// </summary>
    public T Reaumur => (Kelvin - T.CreateChecked(273.15)) * T.CreateChecked(0.80);
}
