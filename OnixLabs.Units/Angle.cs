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
/// Represents a unit of plane angle.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Angle<T> : ICanonicalUnit<T>, IAdditiveUnit<Angle<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Angle{T}"/> struct.
    /// </summary>
    /// <param name="value">The angle unit in <see cref="QuectoRadians"/>.</param>
    private Angle(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>
    /// Gets the angle in Quectoradians (qrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qrad.
    /// </remarks>
    public T QuectoRadians => Canonical;

    /// <summary>
    /// Gets the angle in Rontoradians (rrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rrad.
    /// </remarks>
    public T RontoRadians => QuectoRadians.ToRontoUnits();

    /// <summary>
    /// Gets the angle in Yoctoradians (yrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yrad.
    /// </remarks>
    public T YoctoRadians => QuectoRadians.ToYoctoUnits();

    /// <summary>
    /// Gets the angle in Zeptoradians (zrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zrad.
    /// </remarks>
    public T ZeptoRadians => QuectoRadians.ToZeptoUnits();

    /// <summary>
    /// Gets the angle in Attoradians (arad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is arad.
    /// </remarks>
    public T AttoRadians => QuectoRadians.ToAttoUnits();

    /// <summary>
    /// Gets the angle in Femtoradians (frad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is frad.
    /// </remarks>
    public T FemtoRadians => QuectoRadians.ToFemtoUnits();

    /// <summary>
    /// Gets the angle in Picoradians (prad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is prad.
    /// </remarks>
    public T PicoRadians => QuectoRadians.ToPicoUnits();

    /// <summary>
    /// Gets the angle in Nanoradians (nrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nrad.
    /// </remarks>
    public T NanoRadians => QuectoRadians.ToNanoUnits();

    /// <summary>
    /// Gets the angle in Microradians (µrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is urad.
    /// </remarks>
    public T MicroRadians => QuectoRadians.ToMicroUnits();

    /// <summary>
    /// Gets the angle in Milliradians (mrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mrad.
    /// </remarks>
    public T MilliRadians => QuectoRadians.ToMilliUnits();

    /// <summary>
    /// Gets the angle in Centiradians (crad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is crad.
    /// </remarks>
    public T CentiRadians => QuectoRadians.ToCentiUnits();

    /// <summary>
    /// Gets the angle in Deciradians (drad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is drad.
    /// </remarks>
    public T DeciRadians => QuectoRadians.ToDeciUnits();

    /// <summary>
    /// Gets the angle in Radians (rad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rad.
    /// </remarks>
    public T Radians => QuectoRadians.ToBaseUnits();

    /// <summary>
    /// Gets the angle in Decaradians (darad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is darad.
    /// </remarks>
    public T DecaRadians => QuectoRadians.ToDecaUnits();

    /// <summary>
    /// Gets the angle in Hectoradians (hrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hrad.
    /// </remarks>
    public T HectoRadians => QuectoRadians.ToHectoUnits();

    /// <summary>
    /// Gets the angle in Kiloradians (krad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is krad.
    /// </remarks>
    public T KiloRadians => QuectoRadians.ToKiloUnits();

    /// <summary>
    /// Gets the angle in Megaradians (Mrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mrad.
    /// </remarks>
    public T MegaRadians => QuectoRadians.ToMegaUnits();

    /// <summary>
    /// Gets the angle in Gigaradians (Grad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Grad.
    /// </remarks>
    public T GigaRadians => QuectoRadians.ToGigaUnits();

    /// <summary>
    /// Gets the angle in Teraradians (Trad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Trad.
    /// </remarks>
    public T TeraRadians => QuectoRadians.ToTeraUnits();

    /// <summary>
    /// Gets the angle in Petaradians (Prad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Prad.
    /// </remarks>
    public T PetaRadians => QuectoRadians.ToPetaUnits();

    /// <summary>
    /// Gets the angle in Exaradians (Erad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Erad.
    /// </remarks>
    public T ExaRadians => QuectoRadians.ToExaUnits();

    /// <summary>
    /// Gets the angle in Zettaradians (Zrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zrad.
    /// </remarks>
    public T ZettaRadians => QuectoRadians.ToZettaUnits();

    /// <summary>
    /// Gets the angle in Yottaradians (Yrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Yrad.
    /// </remarks>
    public T YottaRadians => QuectoRadians.ToYottaUnits();

    /// <summary>
    /// Gets the angle in Ronnaradians (Rrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rrad.
    /// </remarks>
    public T RonnaRadians => QuectoRadians.ToRonnaUnits();

    /// <summary>
    /// Gets the angle in Quettaradians (Qrad).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qrad.
    /// </remarks>
    public T QuettaRadians => QuectoRadians.ToQuettaUnits();

    /// <summary>
    /// Gets the angle in Degrees (°).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is deg.
    /// </remarks>
    public T Degrees => QuectoRadians / QuectoRadiansPerDegree;

    /// <summary>
    /// Gets the angle in Arcminutes (′).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is arcmin.
    /// </remarks>
    public T Arcminutes => QuectoRadians / QuectoRadiansPerArcminute;

    /// <summary>
    /// Gets the angle in Arcseconds (″).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is arcsec.
    /// </remarks>
    public T Arcseconds => QuectoRadians / QuectoRadiansPerArcsecond;

    /// <summary>
    /// Gets the angle in Gradians (gon).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is gon.
    /// </remarks>
    public T Gradians => QuectoRadians / QuectoRadiansPerGradian;

    /// <summary>
    /// Gets the angle in Turns (tr).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is tr.
    /// </remarks>
    public T Turns => QuectoRadians / QuectoRadiansPerTurn;
}
