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
/// Represents a unit of solid angle. The SI unit is the steradian (sr), the solid-angle analogue of the radian.
/// Distinct from <see cref="Angle{T}"/> (planar angle) — although both are dimensionless ratios, they describe
/// physically different geometric quantities, and SI defines the lumen as <c>cd · sr</c>, not <c>cd · rad</c>.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct SolidAngle<T> : ICanonicalUnit<T>, IAdditiveUnit<SolidAngle<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SolidAngle{T}"/> struct.
    /// </summary>
    /// <param name="value">The solid-angle value in <see cref="QuectoSteradians"/>.</param>
    private SolidAngle(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>Gets the solid angle in Quectosteradians (qsr).</summary>
    public T QuectoSteradians => Canonical;

    /// <summary>Gets the solid angle in Rontosteradians (rsr).</summary>
    public T RontoSteradians => QuectoSteradians.ToRontoUnits();

    /// <summary>Gets the solid angle in Yoctosteradians (ysr).</summary>
    public T YoctoSteradians => QuectoSteradians.ToYoctoUnits();

    /// <summary>Gets the solid angle in Zeptosteradians (zsr).</summary>
    public T ZeptoSteradians => QuectoSteradians.ToZeptoUnits();

    /// <summary>Gets the solid angle in Attosteradians (asr).</summary>
    public T AttoSteradians => QuectoSteradians.ToAttoUnits();

    /// <summary>Gets the solid angle in Femtosteradians (fsr).</summary>
    public T FemtoSteradians => QuectoSteradians.ToFemtoUnits();

    /// <summary>Gets the solid angle in Picosteradians (psr).</summary>
    public T PicoSteradians => QuectoSteradians.ToPicoUnits();

    /// <summary>Gets the solid angle in Nanosteradians (nsr).</summary>
    public T NanoSteradians => QuectoSteradians.ToNanoUnits();

    /// <summary>Gets the solid angle in Microsteradians (µsr).</summary>
    public T MicroSteradians => QuectoSteradians.ToMicroUnits();

    /// <summary>Gets the solid angle in Millisteradians (msr).</summary>
    public T MilliSteradians => QuectoSteradians.ToMilliUnits();

    /// <summary>Gets the solid angle in Centisteradians (csr).</summary>
    public T CentiSteradians => QuectoSteradians.ToCentiUnits();

    /// <summary>Gets the solid angle in Decisteradians (dsr).</summary>
    public T DeciSteradians => QuectoSteradians.ToDeciUnits();

    /// <summary>Gets the solid angle in Steradians (sr).</summary>
    public T Steradians => QuectoSteradians.ToBaseUnits();

    /// <summary>Gets the solid angle in Decasteradians (dasr).</summary>
    public T DecaSteradians => QuectoSteradians.ToDecaUnits();

    /// <summary>Gets the solid angle in Hectosteradians (hsr).</summary>
    public T HectoSteradians => QuectoSteradians.ToHectoUnits();

    /// <summary>Gets the solid angle in Kilosteradians (ksr).</summary>
    public T KiloSteradians => QuectoSteradians.ToKiloUnits();

    /// <summary>Gets the solid angle in Megasteradians (Msr).</summary>
    public T MegaSteradians => QuectoSteradians.ToMegaUnits();

    /// <summary>Gets the solid angle in Gigasteradians (Gsr).</summary>
    public T GigaSteradians => QuectoSteradians.ToGigaUnits();

    /// <summary>Gets the solid angle in Terasteradians (Tsr).</summary>
    public T TeraSteradians => QuectoSteradians.ToTeraUnits();

    /// <summary>Gets the solid angle in Petasteradians (Psr).</summary>
    public T PetaSteradians => QuectoSteradians.ToPetaUnits();

    /// <summary>Gets the solid angle in Exasteradians (Esr).</summary>
    public T ExaSteradians => QuectoSteradians.ToExaUnits();

    /// <summary>Gets the solid angle in Zettasteradians (Zsr).</summary>
    public T ZettaSteradians => QuectoSteradians.ToZettaUnits();

    /// <summary>Gets the solid angle in Yottasteradians (Ysr).</summary>
    public T YottaSteradians => QuectoSteradians.ToYottaUnits();

    /// <summary>Gets the solid angle in Ronnasteradians (Rsr).</summary>
    public T RonnaSteradians => QuectoSteradians.ToRonnaUnits();

    /// <summary>Gets the solid angle in Quettasteradians (Qsr).</summary>
    public T QuettaSteradians => QuectoSteradians.ToQuettaUnits();

    /// <summary>Gets the solid angle in Square Degrees (deg²).</summary>
    public T SquareDegrees => QuectoSteradians / QuectoSteradiansPerSquareDegree;

    /// <summary>Gets the solid angle in Square Arcminutes (arcmin²).</summary>
    public T SquareArcminutes => QuectoSteradians / QuectoSteradiansPerSquareArcminute;

    /// <summary>Gets the solid angle in Square Arcseconds (arcsec²).</summary>
    public T SquareArcseconds => QuectoSteradians / QuectoSteradiansPerSquareArcsecond;

    /// <summary>Gets the solid angle in Spats (sp). One spat is the full sphere (<c>4π sr</c>).</summary>
    public T Spats => QuectoSteradians / QuectoSteradiansPerSpat;
}
