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

public readonly partial struct Energy<T>
{
/// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified yoctojoule value.
    /// </summary>
    /// <param name="value">The value, in yoctojoules (yJ), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromYoctoJoules(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified joule value.
    /// </summary>
    /// <param name="value">The value, in joules (J), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromJoules(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified kilojoule value.
    /// </summary>
    /// <param name="value">The value, in kilojoules (kJ), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKiloJoules(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified megajoule value.
    /// </summary>
    /// <param name="value">The value, in megajoules (MJ), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMegaJoules(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified gigajoule value.
    /// </summary>
    /// <param name="value">The value, in gigajoules (GJ), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromGigaJoules(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified thermochemical calorie value.
    /// </summary>
    /// <param name="value">The value, in thermochemical calories (cal), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromCalories(T value) => new(value * T.CreateChecked(4.184e24));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified thermochemical kilocalorie value.
    /// </summary>
    /// <param name="value">The value, in thermochemical kilocalories (kcal), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKiloCalories(T value) => new(value * T.CreateChecked(4.184e27));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified watt-hour value.
    /// </summary>
    /// <param name="value">The value, in watt-hours (Wh), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromWattHours(T value) => new(value * T.CreateChecked(3.6e27));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified kilowatt-hour value.
    /// </summary>
    /// <param name="value">The value, in kilowatt-hours (kWh), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKiloWattHours(T value) => new(value * T.CreateChecked(3.6e30));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified erg value.
    /// </summary>
    /// <param name="value">The value, in ergs (erg), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromErgs(T value) => new(value * T.CreateChecked(1e17));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified British thermal unit value.
    /// </summary>
    /// <param name="value">The value, in British thermal units (BTU, I.T.), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromBritishThermalUnits(T value) => new(value * T.CreateChecked(1.05505585262e27));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified foot-pound value.
    /// </summary>
    /// <param name="value">The value, in foot-pounds force (ft⋅lbf), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromFootPounds(T value) => new(value * T.CreateChecked(1.3558179483314e24));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified electronvolt value.
    /// </summary>
    /// <param name="value">The value, in electronvolts (eV), from which to construct a new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e5));
}
