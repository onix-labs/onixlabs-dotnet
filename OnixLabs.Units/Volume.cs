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
/// Represents a unit of volume.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Volume<T> :
    IValueEquatable<Volume<T>>,
    IValueComparable<Volume<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Volume{T}"/> struct.
    /// </summary>
    /// <param name="value">The volume unit in <see cref="CubicYoctoMeters"/>.</param>
    private Volume(T value) => CubicYoctoMeters = value;

    /// <summary>
    /// Gets the volume in cubic yoctometers (cuym).
    /// </summary>
    public T CubicYoctoMeters { get; }

    /// <summary>
    /// Gets the volume in cubic zeptometers (cuzm).
    /// </summary>
    public T CubicZeptoMeters => CubicYoctoMeters / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the volume in cubic attometers (cuam).
    /// </summary>
    public T CubicAttoMeters => CubicYoctoMeters / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the volume in cubic femtometers (cufm).
    /// </summary>
    public T CubicFemtoMeters => CubicYoctoMeters / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the volume in cubic picometers (cupm).
    /// </summary>
    public T CubicPicoMeters => CubicYoctoMeters / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the volume in cubic nanometers (cunm).
    /// </summary>
    public T CubicNanoMeters => CubicYoctoMeters / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the volume in cubic micrometers (cuum).
    /// </summary>
    public T CubicMicroMeters => CubicYoctoMeters / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the volume in cubic millimeters (cumm).
    /// </summary>
    public T CubicMilliMeters => CubicYoctoMeters / T.CreateChecked(1e63);

    /// <summary>
    /// Gets the volume in cubic centimeters (cucm).
    /// </summary>
    public T CubicCentiMeters => CubicYoctoMeters / T.CreateChecked(1e66);

    /// <summary>
    /// Gets the volume in cubic decimeters (cudm).
    /// </summary>
    public T CubicDeciMeters => CubicYoctoMeters / T.CreateChecked(1e69);

    /// <summary>
    /// Gets the volume in cubic meters (cum).
    /// </summary>
    public T CubicMeters => CubicYoctoMeters / T.CreateChecked(1e72);

    /// <summary>
    /// Gets the volume in cubic decameters (cudam).
    /// </summary>
    public T CubicDecaMeters => CubicYoctoMeters / T.CreateChecked(1e75);

    /// <summary>
    /// Gets the volume in cubic hectometers (cuhm).
    /// </summary>
    public T CubicHectoMeters => CubicYoctoMeters / T.CreateChecked(1e78);

    /// <summary>
    /// Gets the volume in cubic kilometers (cukm).
    /// </summary>
    public T CubicKiloMeters => CubicYoctoMeters / T.CreateChecked(1e81);

    /// <summary>
    /// Gets the volume in cubic megameters (cuMm).
    /// </summary>
    public T CubicMegaMeters => CubicYoctoMeters / T.CreateChecked(1e90);

    /// <summary>
    /// Gets the volume in cubic gigameters (cuGm).
    /// </summary>
    public T CubicGigaMeters => CubicYoctoMeters / T.CreateChecked(1e99);

    /// <summary>
    /// Gets the volume in cubic terameters (cuTm).
    /// </summary>
    public T CubicTeraMeters => CubicYoctoMeters / T.CreateChecked(1e108);

    /// <summary>
    /// Gets the volume in cubic petameters (cuPm).
    /// </summary>
    public T CubicPetaMeters => CubicYoctoMeters / T.CreateChecked(1e117);

    /// <summary>
    /// Gets the volume in cubic exameters (cuEm).
    /// </summary>
    public T CubicExaMeters => CubicYoctoMeters / T.CreateChecked(1e126);

    /// <summary>
    /// Gets the volume in cubic zettameters (cuZm).
    /// </summary>
    public T CubicZettaMeters => CubicYoctoMeters / T.CreateChecked(1e135);

    /// <summary>
    /// Gets the volume in cubic yottameters (cuYm).
    /// </summary>
    public T CubicYottaMeters => CubicYoctoMeters / T.CreateChecked(1e144);

    /// <summary>
    /// Gets the volume in liters (L).
    /// </summary>
    public T Liters => CubicYoctoMeters / T.CreateChecked(1e69);

    /// <summary>
    /// Gets the volume in millilitres (mL).
    /// </summary>
    public T MilliLiters => CubicYoctoMeters / T.CreateChecked(1e66);

    /// <summary>
    /// Gets the volume in centilitres (cL).
    /// </summary>
    public T CentiLiters => CubicYoctoMeters / T.CreateChecked(1e67);

    /// <summary>
    /// Gets the volume in decilitres (dL).
    /// </summary>
    public T DeciLiters => CubicYoctoMeters / T.CreateChecked(1e68);

    /// <summary>
    /// Gets the volume in cubic inches (cuin).
    /// </summary>
    public T CubicInches => CubicYoctoMeters / T.CreateChecked(1.6387064e67);

    /// <summary>
    /// Gets the volume in cubic feet (cuft).
    /// </summary>
    public T CubicFeet => CubicYoctoMeters / T.CreateChecked(2.8316846592e70);

    /// <summary>
    /// Gets the volume in cubic yards (cuyd).
    /// </summary>
    public T CubicYards => CubicYoctoMeters / T.CreateChecked(7.64554857984e71);

    /// <summary>
    /// Gets the volume in US fluid ounces (floz).
    /// </summary>
    public T FluidOunces => CubicYoctoMeters / T.CreateChecked(2.95735295625e67);

    /// <summary>
    /// Gets the volume in US cups (cup).
    /// </summary>
    public T Cups => CubicYoctoMeters / T.CreateChecked(2.365882365e68);

    /// <summary>
    /// Gets the volume in US pints (pt).
    /// </summary>
    public T Pints => CubicYoctoMeters / T.CreateChecked(4.73176473e68);

    /// <summary>
    /// Gets the volume in US quarts (qt).
    /// </summary>
    public T Quarts => CubicYoctoMeters / T.CreateChecked(9.46352946e68);

    /// <summary>
    /// Gets the volume in US gallons (gal).
    /// </summary>
    public T Gallons => CubicYoctoMeters / T.CreateChecked(3.785411784e69);
}
