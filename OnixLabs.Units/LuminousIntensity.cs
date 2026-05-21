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
/// Represents a unit of luminous intensity.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct LuminousIntensity<T> : ICanonicalUnit<T>, IAdditiveUnit<LuminousIntensity<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LuminousIntensity{T}"/> struct.
    /// </summary>
    /// <param name="value">The luminous intensity unit in <see cref="QuectoCandelas"/>.</param>
    private LuminousIntensity(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>
    /// Gets the luminous intensity in Quectocandelas (qcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qcd.
    /// </remarks>
    public T QuectoCandelas => Canonical;

    /// <summary>
    /// Gets the luminous intensity in Rontocandelas (rcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rcd.
    /// </remarks>
    public T RontoCandelas => QuectoCandelas.ToRontoUnits();

    /// <summary>
    /// Gets the luminous intensity in Yoctocandelas (ycd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ycd.
    /// </remarks>
    public T YoctoCandelas => QuectoCandelas.ToYoctoUnits();

    /// <summary>
    /// Gets the luminous intensity in Zeptocandelas (zcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zcd.
    /// </remarks>
    public T ZeptoCandelas => QuectoCandelas.ToZeptoUnits();

    /// <summary>
    /// Gets the luminous intensity in Attocandelas (acd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is acd.
    /// </remarks>
    public T AttoCandelas => QuectoCandelas.ToAttoUnits();

    /// <summary>
    /// Gets the luminous intensity in Femtocandelas (fcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fcd.
    /// </remarks>
    public T FemtoCandelas => QuectoCandelas.ToFemtoUnits();

    /// <summary>
    /// Gets the luminous intensity in Picocandelas (pcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pcd.
    /// </remarks>
    public T PicoCandelas => QuectoCandelas.ToPicoUnits();

    /// <summary>
    /// Gets the luminous intensity in Nanocandelas (ncd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ncd.
    /// </remarks>
    public T NanoCandelas => QuectoCandelas.ToNanoUnits();

    /// <summary>
    /// Gets the luminous intensity in Microcandelas (µcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ucd.
    /// </remarks>
    public T MicroCandelas => QuectoCandelas.ToMicroUnits();

    /// <summary>
    /// Gets the luminous intensity in Millicandelas (mcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mcd.
    /// </remarks>
    public T MilliCandelas => QuectoCandelas.ToMilliUnits();

    /// <summary>
    /// Gets the luminous intensity in Centicandelas (ccd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ccd.
    /// </remarks>
    public T CentiCandelas => QuectoCandelas.ToCentiUnits();

    /// <summary>
    /// Gets the luminous intensity in Decicandelas (dcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dcd.
    /// </remarks>
    public T DeciCandelas => QuectoCandelas.ToDeciUnits();

    /// <summary>
    /// Gets the luminous intensity in Candelas (cd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cd.
    /// </remarks>
    public T Candelas => QuectoCandelas.ToBaseUnits();

    /// <summary>
    /// Gets the luminous intensity in Decacandelas (dacd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dacd.
    /// </remarks>
    public T DecaCandelas => QuectoCandelas.ToDecaUnits();

    /// <summary>
    /// Gets the luminous intensity in Hectocandelas (hcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hcd.
    /// </remarks>
    public T HectoCandelas => QuectoCandelas.ToHectoUnits();

    /// <summary>
    /// Gets the luminous intensity in Kilocandelas (kcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kcd.
    /// </remarks>
    public T KiloCandelas => QuectoCandelas.ToKiloUnits();

    /// <summary>
    /// Gets the luminous intensity in Megacandelas (Mcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mcd.
    /// </remarks>
    public T MegaCandelas => QuectoCandelas.ToMegaUnits();

    /// <summary>
    /// Gets the luminous intensity in Gigacandelas (Gcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gcd.
    /// </remarks>
    public T GigaCandelas => QuectoCandelas.ToGigaUnits();

    /// <summary>
    /// Gets the luminous intensity in Teracandelas (Tcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tcd.
    /// </remarks>
    public T TeraCandelas => QuectoCandelas.ToTeraUnits();

    /// <summary>
    /// Gets the luminous intensity in Petacandelas (Pcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pcd.
    /// </remarks>
    public T PetaCandelas => QuectoCandelas.ToPetaUnits();

    /// <summary>
    /// Gets the luminous intensity in Exacandelas (Ecd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ecd.
    /// </remarks>
    public T ExaCandelas => QuectoCandelas.ToExaUnits();

    /// <summary>
    /// Gets the luminous intensity in Zettacandelas (Zcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zcd.
    /// </remarks>
    public T ZettaCandelas => QuectoCandelas.ToZettaUnits();

    /// <summary>
    /// Gets the luminous intensity in Yottacandelas (Ycd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ycd.
    /// </remarks>
    public T YottaCandelas => QuectoCandelas.ToYottaUnits();

    /// <summary>
    /// Gets the luminous intensity in Ronnacandelas (Rcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rcd.
    /// </remarks>
    public T RonnaCandelas => QuectoCandelas.ToRonnaUnits();

    /// <summary>
    /// Gets the luminous intensity in Quettacandelas (Qcd).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qcd.
    /// </remarks>
    public T QuettaCandelas => QuectoCandelas.ToQuettaUnits();
}
