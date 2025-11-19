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
/// Represents a unit of speed.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Speed<T> :
    IValueEquatable<Speed<T>>,
    IValueComparable<Speed<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Speed{T}"/> struct.
    /// </summary>
    /// <param name="value">The speed unit in <see cref="QuectoMetersPerSecond"/>.</param>
    private Speed(T value) => QuectoMetersPerSecond = value;

    /// <summary>
    /// Gets the speed in quectometers per second (qmps).
    /// </summary>
    public T QuectoMetersPerSecond { get; }

    /// <summary>
    /// Gets the speed in rontometers per second (rmps).
    /// </summary>
    public T RontoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the speed in yoctometers per second (ymps).
    /// </summary>
    public T YoctoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the speed in zeptometers per second (zmps).
    /// </summary>
    public T ZeptoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the speed in attometers per second (amps).
    /// </summary>
    public T AttoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the speed in femtometers per second (fmps).
    /// </summary>
    public T FemtoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the speed in picometers per second (pmps).
    /// </summary>
    public T PicoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the speed in nanometers per second (nmps).
    /// </summary>
    public T NanoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the speed in micrometers per second (µmps).
    /// </summary>
    public T MicroMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the speed in millimeters per second (mmps).
    /// </summary>
    public T MilliMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the speed in centimeters per second (cmps).
    /// </summary>
    public T CentiMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e28);

    /// <summary>
    /// Gets the speed in decimeters per second (dmps).
    /// </summary>
    public T DeciMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e29);

    /// <summary>
    /// Gets the speed in meters per second (mps).
    /// </summary>
    public T MetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the speed in decameters per second (damps).
    /// </summary>
    public T DecaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e31);

    /// <summary>
    /// Gets the speed in hectometers per second (hmps).
    /// </summary>
    public T HectoMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e32);

    /// <summary>
    /// Gets the speed in kilometers per second (kmps).
    /// </summary>
    public T KiloMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the speed in megameters per second (Mmps).
    /// </summary>
    public T MegaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the speed in gigameters per second (Gmps).
    /// </summary>
    public T GigaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the speed in terameters per second (Tmps).
    /// </summary>
    public T TeraMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the speed in petameters per second (Pmps).
    /// </summary>
    public T PetaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the speed in exameters per second (Emps).
    /// </summary>
    public T ExaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the speed in zettameters per second (Zmps).
    /// </summary>
    public T ZettaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e51);

    /// <summary>
    /// Gets the speed in yottameters per second (Ymps).
    /// </summary>
    public T YottaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the speed in ronnameters per second (Rmps).
    /// </summary>
    public T RonnaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e57);

    /// <summary>
    /// Gets the speed in quettameters per second (Qmps).
    /// </summary>
    public T QuettaMetersPerSecond => QuectoMetersPerSecond / T.CreateChecked(1e60);

    /// <summary>
    /// Gets the speed in inches per second (inps).
    /// </summary>
    public T InchesPerSecond => QuectoMetersPerSecond / T.CreateChecked(0.0254e30);

    /// <summary>
    /// Gets the speed in feet per second (ftps).
    /// </summary>
    public T FeetPerSecond => QuectoMetersPerSecond / T.CreateChecked(0.3048e30);

    /// <summary>
    /// Gets the speed in kilometers per hour (kmph).
    /// </summary>
    public T KilometersPerHour => QuectoMetersPerSecond / T.CreateChecked(0.2777777777777778e30);

    /// <summary>
    /// Gets the speed in miles per hour (mph).
    /// </summary>
    public T MilesPerHour => QuectoMetersPerSecond / T.CreateChecked(0.44704e30);

    /// <summary>
    /// Gets the speed in knots (kt).
    /// </summary>
    public T Knots => QuectoMetersPerSecond / T.CreateChecked(0.5144444444444445e30);
}
