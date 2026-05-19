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
/// Represents a unit of distance.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Distance<T> :
    ICanonicalUnit<T>,
    IAdditiveUnit<Distance<T>>,
    IMultiplicativeUnit<Distance<T>> where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Distance{T}"/> struct.
    /// </summary>
    /// <param name="value">The distance unit in <see cref="QuectoMeters"/>.</param>
    private Distance(T value) => Canonical = value;

    public T Canonical { get; }

    /// <summary>
    /// Gets the distance in Quectometers (qm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qm.
    /// </remarks>
    public T QuectoMeters => Canonical;

    /// <summary>
    /// Gets the distance in Rontometers (rm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rm.
    /// </remarks>
    public T RontoMeters => QuectoMeters.ToRontoUnits();

    /// <summary>
    /// Gets the distance in Yoctometers (ym).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ym.
    /// </remarks>
    public T YoctoMeters => QuectoMeters.ToYoctoUnits();

    /// <summary>
    /// Gets the distance in Zeptometers (zm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zm.
    /// </remarks>
    public T ZeptoMeters => QuectoMeters.ToZeptoUnits();

    /// <summary>
    /// Gets the distance in Attometers (am).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is am.
    /// </remarks>
    public T AttoMeters => QuectoMeters.ToAttoUnits();

    /// <summary>
    /// Gets the distance in Femtometers (fm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fm.
    /// </remarks>
    public T FemtoMeters => QuectoMeters.ToFemtoUnits();

    /// <summary>
    /// Gets the distance in Picometers (pm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pm.
    /// </remarks>
    public T PicoMeters => QuectoMeters.ToPicoUnits();

    /// <summary>
    /// Gets the distance in Nanometers (nm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nm.
    /// </remarks>
    public T NanoMeters => QuectoMeters.ToNanoUnits();

    /// <summary>
    /// Gets the distance in Micrometers (µm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is um.
    /// </remarks>
    public T MicroMeters => QuectoMeters.ToMicroUnits();

    /// <summary>
    /// Gets the distance in Millimeters (mm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mm.
    /// </remarks>
    public T MilliMeters => QuectoMeters.ToMilliUnits();

    /// <summary>
    /// Gets the distance in Centimeters (cm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cm.
    /// </remarks>
    public T CentiMeters => QuectoMeters.ToCentiUnits();

    /// <summary>
    /// Gets the distance in Decimeters (dm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dm.
    /// </remarks>
    public T DeciMeters => QuectoMeters.ToDeciUnits();

    /// <summary>
    /// Gets the distance in Meters (m).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is m.
    /// </remarks>
    public T Meters => QuectoMeters.ToBaseUnits();

    /// <summary>
    /// Gets the distance in Decameters (dam).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dam.
    /// </remarks>
    public T DecaMeters => QuectoMeters.ToDecaUnits();

    /// <summary>
    /// Gets the distance in Hectometers (hm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hm.
    /// </remarks>
    public T HectoMeters => QuectoMeters.ToHectoUnits();

    /// <summary>
    /// Gets the distance in Kilometers (km).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is km.
    /// </remarks>
    public T KiloMeters => QuectoMeters.ToKiloUnits();

    /// <summary>
    /// Gets the distance in Megameters (Mm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mm.
    /// </remarks>
    public T MegaMeters => QuectoMeters.ToMegaUnits();

    /// <summary>
    /// Gets the distance in Gigameters (Gm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gm.
    /// </remarks>
    public T GigaMeters => QuectoMeters.ToGigaUnits();

    /// <summary>
    /// Gets the distance in Terameters (Tm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tm.
    /// </remarks>
    public T TeraMeters => QuectoMeters.ToTeraUnits();

    /// <summary>
    /// Gets the distance in Petameters (Pm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pm.
    /// </remarks>
    public T PetaMeters => QuectoMeters.ToPetaUnits();

    /// <summary>
    /// Gets the distance in Exameters (Em).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Em.
    /// </remarks>
    public T ExaMeters => QuectoMeters.ToExaUnits();

    /// <summary>
    /// Gets the distance in Zettameters (Zm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zm.
    /// </remarks>
    public T ZettaMeters => QuectoMeters.ToZettaUnits();

    /// <summary>
    /// Gets the distance in Yottameters (Ym).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ym.
    /// </remarks>
    public T YottaMeters => QuectoMeters.ToYottaUnits();

    /// <summary>
    /// Gets the distance in Ronnameters (Rm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rm.
    /// </remarks>
    public T RonnaMeters => QuectoMeters.ToRonnaUnits();

    /// <summary>
    /// Gets the distance in Quettameters (Qm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qm.
    /// </remarks>
    public T QuettaMeters => QuectoMeters.ToQuettaUnits();

    /// <summary>
    /// Gets the distance in Inches (in).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is in.
    /// </remarks>
    public T Inches => QuectoMeters / QuectometersPerInch;

    /// <summary>
    /// Gets the distance in Feet (ft).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ft.
    /// </remarks>
    public T Feet => QuectoMeters / QuectometersPerFoot;

    /// <summary>
    /// Gets the distance in Yards (yd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yd.
    /// </remarks>
    public T Yards => QuectoMeters / QuectometersPerYard;

    // ReSharper disable once GrammarMistakeInComment
    /// <summary>
    /// Gets the distance in Miles (mi).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mi.
    /// </remarks>
    public T Miles => QuectoMeters / QuectometersPerMile;

    /// <summary>
    /// Gets the distance in Nautical Miles (nmi).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nmi.
    /// </remarks>
    public T NauticalMiles => QuectoMeters / QuectometersPerNauticalMile;

    /// <summary>
    /// Gets the distance in Fermis (fm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fmi.
    /// </remarks>
    public T Fermis => QuectoMeters / QuectometersPerFermi;

    /// <summary>
    /// Gets the distance in Angstroms (Å).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is a.
    /// </remarks>
    public T Angstroms => QuectoMeters / QuectometersPerAngstrom;

    /// <summary>
    /// Gets the distance in Astronomical Units (au).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is au.
    /// </remarks>
    public T AstronomicalUnits => QuectoMeters / QuectometersPerAstronomicalUnit;

    /// <summary>
    /// Gets the distance in Light Years (ly).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ly.
    /// </remarks>
    public T LightYears => QuectoMeters / QuectometersPerLightYear;

    /// <summary>
    /// Gets the distance in Parsecs (pc).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pc.
    /// </remarks>
    public T Parsecs => QuectoMeters / QuectometersPerParsec;
}
