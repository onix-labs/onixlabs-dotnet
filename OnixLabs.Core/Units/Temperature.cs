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

namespace OnixLabs.Core.Units;

/// <summary>
/// Represents a unit of temperature.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the current <see cref="Temperature{T}"/> unit.</typeparam>
public readonly partial struct Temperature<T> where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Temperature" /> struct.
    /// </summary>
    /// <param name="kelvin">The initial temperature value in Kelvin.</param>
    private Temperature(T kelvin)
    {
        Kelvin = kelvin;
    }

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
    public T Delisle => (T.CreateChecked(373.15) - Kelvin) * T.CreateChecked(1.5);

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public T Fahrenheit => Kelvin * T.CreateChecked(1.8) - T.CreateChecked(459.67);

    /// <summary>
    /// Gets the temperature in Newton.
    /// </summary>
    public T Newton => (Kelvin - T.CreateChecked(273.15)) * T.CreateChecked(0.33);

    /// <summary>
    /// Gets the temperature in Reaumur.
    /// </summary>
    public T Reaumur => (Kelvin - T.CreateChecked(273.15)) * T.CreateChecked(0.8);

    /// <summary>
    /// Gets the temperature in Rankine.
    /// </summary>
    public T Rankine => Kelvin * T.CreateChecked(1.8);
}
