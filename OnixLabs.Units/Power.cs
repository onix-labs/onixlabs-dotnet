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
/// Represents a unit of power.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Power<T> :
    IValueEquatable<Power<T>>,
    IValueComparable<Power<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Power{T}"/> struct.
    /// </summary>
    /// <param name="value">The power unit in <see cref="YoctoWatts"/>.</param>
    private Power(T value) => YoctoWatts = value;

    /// <summary>
    /// Gets the power in yoctowatts (yW).
    /// </summary>
    public T YoctoWatts { get; }

    /// <summary>
    /// Gets the power in zeptowatts (zW).
    /// </summary>
    public T ZeptoWatts => YoctoWatts / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the power in attowatts (aW).
    /// </summary>
    public T AttoWatts => YoctoWatts / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the power in femtowatts (fW).
    /// </summary>
    public T FemtoWatts => YoctoWatts / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the power in picowatts (pW).
    /// </summary>
    public T PicoWatts => YoctoWatts / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the power in nanowatts (nW).
    /// </summary>
    public T NanoWatts => YoctoWatts / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the power in microwatts (µW).
    /// </summary>
    public T MicroWatts => YoctoWatts / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the power in milliwatts (mW).
    /// </summary>
    public T MilliWatts => YoctoWatts / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the power in watts (W).
    /// </summary>
    public T Watts => YoctoWatts / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the power in kilowatts (kW).
    /// </summary>
    public T KiloWatts => YoctoWatts / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the power in megawatts (MW).
    /// </summary>
    public T MegaWatts => YoctoWatts / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the power in gigawatts (GW).
    /// </summary>
    public T GigaWatts => YoctoWatts / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the power in terawatts (TW).
    /// </summary>
    public T TeraWatts => YoctoWatts / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the power in petawatts (PW).
    /// </summary>
    public T PetaWatts => YoctoWatts / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the power in exawatts (EW).
    /// </summary>
    public T ExaWatts => YoctoWatts / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the power in zettawatts (ZW).
    /// </summary>
    public T ZettaWatts => YoctoWatts / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the power in yottawatts (YW).
    /// </summary>
    public T YottaWatts => YoctoWatts / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the power in mechanical horsepower (hp).
    /// </summary>
    public T Horsepower => YoctoWatts / T.CreateChecked(7.456998715822702e26);

    /// <summary>
    /// Gets the power in metric horsepower (hpM).
    /// </summary>
    public T MetricHorsepower => YoctoWatts / T.CreateChecked(7.3549875e26);
}
