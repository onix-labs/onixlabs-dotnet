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
/// Represents a unit of distance.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Distance<T> :
    IValueEquatable<Distance<T>>,
    IValueComparable<Distance<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Distance{T}"/> struct.
    /// </summary>
    /// <param name="value">The distance unit in <see cref="QuectoMeters"/>.</param>
    private Distance(T value) => QuectoMeters = value;

    /// <summary>
    /// Gets the distance in Quectometers (qm).
    /// </summary>
    public T QuectoMeters { get; }

    /// <summary>
    /// Gets the distance in Rontometers (rm).
    /// </summary>
    public T RontoMeters => QuectoMeters / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the distance in Yoctometers (ym).
    /// </summary>
    public T YoctoMeters => QuectoMeters / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the distance in Zeptometers (zm).
    /// </summary>
    public T ZeptoMeters => QuectoMeters / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the distance in Attometers (am).
    /// </summary>
    public T AttoMeters => QuectoMeters / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the distance in Femtometers (fm).
    /// </summary>
    public T FemtoMeters => QuectoMeters / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the distance in Picometers (pm).
    /// </summary>
    public T PicoMeters => QuectoMeters / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the distance in Nanometers (nm).
    /// </summary>
    public T NanoMeters => QuectoMeters / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the distance in Micrometers (um).
    /// </summary>
    public T MicroMeters => QuectoMeters / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the distance in Millimeters (mm).
    /// </summary>
    public T MilliMeters => QuectoMeters / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the distance in Centimeters (cm).
    /// </summary>
    public T CentiMeters => QuectoMeters / T.CreateChecked(1e28);

    /// <summary>
    /// Gets the distance in Decimeters (dm).
    /// </summary>
    public T DeciMeters => QuectoMeters / T.CreateChecked(1e29);

    /// <summary>
    /// Gets the distance in Meters (m).
    /// </summary>
    public T Meters => QuectoMeters / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the distance in Decameters (dam).
    /// </summary>
    public T DecaMeters => QuectoMeters / T.CreateChecked(1e31);

    /// <summary>
    /// Gets the distance in Hectometers (hm).
    /// </summary>
    public T HectoMeters => QuectoMeters / T.CreateChecked(1e32);

    /// <summary>
    /// Gets the distance in Kilometers (km).
    /// </summary>
    public T KiloMeters => QuectoMeters / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the distance in Megameters (Mm).
    /// </summary>
    public T MegaMeters => QuectoMeters / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the distance in Gigameters (Gm).
    /// </summary>
    public T GigaMeters => QuectoMeters / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the distance in Terameters (Tm).
    /// </summary>
    public T TeraMeters => QuectoMeters / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the distance in Petameters (Pm).
    /// </summary>
    public T PetaMeters => QuectoMeters / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the distance in Exameters (Em).
    /// </summary>
    public T ExaMeters => QuectoMeters / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the distance in Zettameters (Zm).
    /// </summary>
    public T ZettaMeters => QuectoMeters / T.CreateChecked(1e51);

    /// <summary>
    /// Gets the distance in Yottameters (Ym).
    /// </summary>
    public T YottaMeters => QuectoMeters / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the distance in Ronnameters (Rm).
    /// </summary>
    public T RonnaMeters => QuectoMeters / T.CreateChecked(1e57);

    /// <summary>
    /// Gets the distance in Quettameters (Qm).
    /// </summary>
    public T QuettaMeters => QuectoMeters / T.CreateChecked(1e60);

    /// <summary>
    /// Gets the distance in Inches (in).
    /// </summary>
    public T Inches => QuectoMeters / T.CreateChecked(0.0254e30);

    /// <summary>
    /// Gets the distance in Feet (ft).
    /// </summary>
    public T Feet => QuectoMeters / T.CreateChecked(0.3048e30);

    /// <summary>
    /// Gets the distance in Yards (yd).
    /// </summary>
    public T Yards => QuectoMeters / T.CreateChecked(0.9144e30);

    /// <summary>
    /// Gets the distance in Miles (mi).
    /// </summary>
    public T Miles => QuectoMeters / T.CreateChecked(1609.344e30);

    /// <summary>
    /// Gets the distance in Nautical Miles (nmi).
    /// </summary>
    public T NauticalMiles => QuectoMeters / T.CreateChecked(1852e30);

    /// <summary>
    /// Gets the distance in Nautical Fermis (fmi).
    /// </summary>
    public T Fermis => QuectoMeters / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the distance in Nautical Angstroms (a).
    /// </summary>
    public T Angstroms => QuectoMeters / T.CreateChecked(1e20);

    /// <summary>
    /// Gets the distance in Astronomical Units (au).
    /// </summary>
    public T AstronomicalUnits => QuectoMeters / T.CreateChecked(149_597_870_700L * 1e30);

    /// <summary>
    /// Gets the distance in Light Years (ly).
    /// </summary>
    public T LightYears => QuectoMeters / T.CreateChecked(9_460_730_472_580_800L * 1e30);

    /// <summary>
    /// Gets the distance in Parsecs (pc).
    /// </summary>
    public T Parsecs
    {
        get
        {
            T metersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
            T qmPerParsec = metersPerParsec * T.CreateChecked(1e30);
            return QuectoMeters / qmPerParsec;
        }
    }
}
