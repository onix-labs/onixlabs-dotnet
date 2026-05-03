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
/// Represents a unit of velocity.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Velocity<T> : IUnit<Velocity<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Velocity{T}"/> struct.
    /// </summary>
    /// <param name="value">The velocity unit in <see cref="QuectoMetersPerSecond"/>.</param>
    private Velocity(T value) => QuectoMetersPerSecond = value;

    /// <summary>
    /// Gets the velocity in Quectometers per second (qm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qmps.
    /// </remarks>
    public T QuectoMetersPerSecond { get; }

    /// <summary>
    /// Gets the velocity in Rontometers per second (rm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rmps.
    /// </remarks>
    public T RontoMetersPerSecond => QuectoMetersPerSecond.ToRontoUnits();

    /// <summary>
    /// Gets the velocity in Yoctometers per second (ym/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ymps.
    /// </remarks>
    public T YoctoMetersPerSecond => QuectoMetersPerSecond.ToYoctoUnits();

    /// <summary>
    /// Gets the velocity in Zeptometers per second (zm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zmps.
    /// </remarks>
    public T ZeptoMetersPerSecond => QuectoMetersPerSecond.ToZeptoUnits();

    /// <summary>
    /// Gets the velocity in Attometers per second (am/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is amps.
    /// </remarks>
    public T AttoMetersPerSecond => QuectoMetersPerSecond.ToAttoUnits();

    /// <summary>
    /// Gets the velocity in Femtometers per second (fm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fmps.
    /// </remarks>
    public T FemtoMetersPerSecond => QuectoMetersPerSecond.ToFemtoUnits();

    /// <summary>
    /// Gets the velocity in Picometers per second (pm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pmps.
    /// </remarks>
    public T PicoMetersPerSecond => QuectoMetersPerSecond.ToPicoUnits();

    /// <summary>
    /// Gets the velocity in Nanometers per second (nm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nmps.
    /// </remarks>
    public T NanoMetersPerSecond => QuectoMetersPerSecond.ToNanoUnits();

    /// <summary>
    /// Gets the velocity in Micrometers per second (µm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is umps.
    /// </remarks>
    public T MicroMetersPerSecond => QuectoMetersPerSecond.ToMicroUnits();

    /// <summary>
    /// Gets the velocity in Millimeters per second (mm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mmps.
    /// </remarks>
    public T MilliMetersPerSecond => QuectoMetersPerSecond.ToMilliUnits();

    /// <summary>
    /// Gets the velocity in Centimeters per second (cm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cmps.
    /// </remarks>
    public T CentiMetersPerSecond => QuectoMetersPerSecond.ToCentiUnits();

    /// <summary>
    /// Gets the velocity in Decimeters per second (dm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dmps.
    /// </remarks>
    public T DeciMetersPerSecond => QuectoMetersPerSecond.ToDeciUnits();

    /// <summary>
    /// Gets the velocity in Meters per second (m/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mps.
    /// </remarks>
    public T MetersPerSecond => QuectoMetersPerSecond.ToBaseUnits();

    /// <summary>
    /// Gets the velocity in Decameters per second (dam/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is damps.
    /// </remarks>
    public T DecaMetersPerSecond => QuectoMetersPerSecond.ToDecaUnits();

    /// <summary>
    /// Gets the velocity in Hectometers per second (hm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hmps.
    /// </remarks>
    public T HectoMetersPerSecond => QuectoMetersPerSecond.ToHectoUnits();

    /// <summary>
    /// Gets the velocity in Kilometers per second (km/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kmps.
    /// </remarks>
    public T KiloMetersPerSecond => QuectoMetersPerSecond.ToKiloUnits();

    /// <summary>
    /// Gets the velocity in Megameters per second (Mm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mmps.
    /// </remarks>
    public T MegaMetersPerSecond => QuectoMetersPerSecond.ToMegaUnits();

    /// <summary>
    /// Gets the velocity in Gigameters per second (Gm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gmps.
    /// </remarks>
    public T GigaMetersPerSecond => QuectoMetersPerSecond.ToGigaUnits();

    /// <summary>
    /// Gets the velocity in Terameters per second (Tm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tmps.
    /// </remarks>
    public T TeraMetersPerSecond => QuectoMetersPerSecond.ToTeraUnits();

    /// <summary>
    /// Gets the velocity in Petameters per second (Pm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pmps.
    /// </remarks>
    public T PetaMetersPerSecond => QuectoMetersPerSecond.ToPetaUnits();

    /// <summary>
    /// Gets the velocity in Exameters per second (Em/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Emps.
    /// </remarks>
    public T ExaMetersPerSecond => QuectoMetersPerSecond.ToExaUnits();

    /// <summary>
    /// Gets the velocity in Zettameters per second (Zm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zmps.
    /// </remarks>
    public T ZettaMetersPerSecond => QuectoMetersPerSecond.ToZettaUnits();

    /// <summary>
    /// Gets the velocity in Yottameters per second (Ym/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ymps.
    /// </remarks>
    public T YottaMetersPerSecond => QuectoMetersPerSecond.ToYottaUnits();

    /// <summary>
    /// Gets the velocity in Ronnameters per second (Rm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rmps.
    /// </remarks>
    public T RonnaMetersPerSecond => QuectoMetersPerSecond.ToRonnaUnits();

    /// <summary>
    /// Gets the velocity in Quettameters per second (Qm/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qmps.
    /// </remarks>
    public T QuettaMetersPerSecond => QuectoMetersPerSecond.ToQuettaUnits();

    /// <summary>
    /// Gets the velocity in Kilometers per hour (km/h).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kmph.
    /// </remarks>
    public T KilometersPerHour => QuectoMetersPerSecond * T.CreateChecked(3.6) / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the velocity in Miles per hour (mph).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mph.
    /// </remarks>
    public T MilesPerHour => QuectoMetersPerSecond / T.CreateChecked(4.4704e29);

    /// <summary>
    /// Gets the velocity in Feet per second (ft/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ftps.
    /// </remarks>
    public T FeetPerSecond => QuectoMetersPerSecond / T.CreateChecked(3.048e29);

    /// <summary>
    /// Gets the velocity in Knots (kn).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kn.
    /// </remarks>
    public T Knots => QuectoMetersPerSecond * T.CreateChecked(3600) / T.CreateChecked(1852e30);

    /// <summary>
    /// Gets the velocity in Inches per second (in/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is inps.
    /// </remarks>
    public T InchesPerSecond => QuectoMetersPerSecond / T.CreateChecked(2.54e28);

    /// <summary>
    /// Gets the velocity in Mach (Ma), based on the standard atmosphere speed of sound at sea level (340.29 m/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ma.
    /// </remarks>
    public T Mach => QuectoMetersPerSecond / T.CreateChecked(3.4029e32);

    /// <summary>
    /// Gets the velocity in multiples of the speed of light in vacuum (c = 299,792,458 m/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is c.
    /// </remarks>
    public T SpeedOfLight => QuectoMetersPerSecond / T.CreateChecked(2.99792458e38);
}
