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
/// Represents a unit of energy.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Energy<T> :
    IValueEquatable<Energy<T>>,
    IValueComparable<Energy<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Energy{T}"/> struct.
    /// </summary>
    /// <param name="value">The energy value in <see cref="YoctoJoules"/>.</param>
    private Energy(T value) => YoctoJoules = value;

    /// <summary>
    /// Gets the energy in yoctojoules (yJ).
    /// </summary>
    public T YoctoJoules { get; }

    /// <summary>
    /// Gets the energy in joules (J).
    /// </summary>
    public T Joules => YoctoJoules / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the energy in kilojoules (kJ).
    /// </summary>
    public T KiloJoules => YoctoJoules / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the energy in megajoules (MJ).
    /// </summary>
    public T MegaJoules => YoctoJoules / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the energy in gigajoules (GJ).
    /// </summary>
    public T GigaJoules => YoctoJoules / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the energy in thermochemical calories (cal).
    /// </summary>
    public T Calories => YoctoJoules / T.CreateChecked(4.184e24);

    /// <summary>
    /// Gets the energy in thermochemical kilocalories (kcal).
    /// </summary>
    public T KiloCalories => YoctoJoules / T.CreateChecked(4.184e27);

    /// <summary>
    /// Gets the energy in watt-hours (Wh).
    /// </summary>
    public T WattHours => YoctoJoules / T.CreateChecked(3.6e27);

    /// <summary>
    /// Gets the energy in kilowatt-hours (kWh).
    /// </summary>
    public T KiloWattHours => YoctoJoules / T.CreateChecked(3.6e30);

    /// <summary>
    /// Gets the energy in ergs (erg).
    /// </summary>
    public T Ergs => YoctoJoules / T.CreateChecked(1e17);

    /// <summary>
    /// Gets the energy in British thermal units (BTU, I.T.).
    /// </summary>
    public T BritishThermalUnits => YoctoJoules / T.CreateChecked(1.05505585262e27);

    /// <summary>
    /// Gets the energy in foot-pounds force (ft⋅lbf).
    /// </summary>
    public T FootPounds => YoctoJoules / T.CreateChecked(1.3558179483314e24);

    /// <summary>
    /// Gets the energy in electronvolts (eV).
    /// </summary>
    public T ElectronVolts => YoctoJoules / T.CreateChecked(1.602176634e5);
}
