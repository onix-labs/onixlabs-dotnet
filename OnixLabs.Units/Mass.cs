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
/// Represents a unit of mass.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Mass<T> : IUnit<Mass<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mass{T}"/> struct.
    /// </summary>
    /// <param name="value">The mass unit in <see cref="QuectoGrams"/>.</param>
    private Mass(T value) => QuectoGrams = value;

    /// <summary>
    /// Gets the mass in Quectograms (qg).
    /// </summary>
    public T QuectoGrams { get; }

    /// <summary>
    /// Gets the mass in Rontograms (rg).
    /// </summary>
    public T RontoGrams => QuectoGrams.ToRontoUnits();

    /// <summary>
    /// Gets the mass in Yoctograms (yg).
    /// </summary>
    public T YoctoGrams => QuectoGrams.ToYoctoUnits();

    /// <summary>
    /// Gets the mass in Zeptograms (zg).
    /// </summary>
    public T ZeptoGrams => QuectoGrams.ToZeptoUnits();

    /// <summary>
    /// Gets the mass in Attograms (ag).
    /// </summary>
    public T AttoGrams => QuectoGrams.ToAttoUnits();

    /// <summary>
    /// Gets the mass in Femtograms (fg).
    /// </summary>
    public T FemtoGrams => QuectoGrams.ToFemtoUnits();

    /// <summary>
    /// Gets the mass in Picograms (pg).
    /// </summary>
    public T PicoGrams => QuectoGrams.ToPicoUnits();

    /// <summary>
    /// Gets the mass in Nanograms (ng).
    /// </summary>
    public T NanoGrams => QuectoGrams.ToNanoUnits();

    /// <summary>
    /// Gets the mass in Micrograms (ug).
    /// </summary>
    public T MicroGrams => QuectoGrams.ToMicroUnits();

    /// <summary>
    /// Gets the mass in Milligrams (mg).
    /// </summary>
    public T MilliGrams => QuectoGrams.ToMilliUnits();

    /// <summary>
    /// Gets the mass in Centigrams (cg).
    /// </summary>
    public T CentiGrams => QuectoGrams.ToCentiUnits();

    /// <summary>
    /// Gets the mass in Decigrams (dg).
    /// </summary>
    public T DeciGrams => QuectoGrams.ToDeciUnits();

    /// <summary>
    /// Gets the mass in Grams (g).
    /// </summary>
    public T Grams => QuectoGrams.ToBaseUnits();

    /// <summary>
    /// Gets the mass in Decagrams (dag).
    /// </summary>
    public T DecaGrams => QuectoGrams.ToDecaUnits();

    /// <summary>
    /// Gets the mass in Hectograms (hg).
    /// </summary>
    public T HectoGrams => QuectoGrams.ToHectoUnits();

    /// <summary>
    /// Gets the mass in Kilograms (kg).
    /// </summary>
    public T KiloGrams => QuectoGrams.ToKiloUnits();

    /// <summary>
    /// Gets the mass in Megagrams (Mg).
    /// </summary>
    public T MegaGrams => QuectoGrams.ToMegaUnits();

    /// <summary>
    /// Gets the mass in Gigagrams (Gg).
    /// </summary>
    public T GigaGrams => QuectoGrams.ToGigaUnits();

    /// <summary>
    /// Gets the mass in Teragrams (Tg).
    /// </summary>
    public T TeraGrams => QuectoGrams.ToTeraUnits();

    /// <summary>
    /// Gets the mass in Petagrams (Pg).
    /// </summary>
    public T PetaGrams => QuectoGrams.ToPetaUnits();

    /// <summary>
    /// Gets the mass in Exagrams (Eg).
    /// </summary>
    public T ExaGrams => QuectoGrams.ToExaUnits();

    /// <summary>
    /// Gets the mass in Zettagrams (Zg).
    /// </summary>
    public T ZettaGrams => QuectoGrams.ToZettaUnits();

    /// <summary>
    /// Gets the mass in Yottagrams (Yg).
    /// </summary>
    public T YottaGrams => QuectoGrams.ToYottaUnits();

    /// <summary>
    /// Gets the mass in Ronnagrams (Rg).
    /// </summary>
    public T RonnaGrams => QuectoGrams.ToRonnaUnits();

    /// <summary>
    /// Gets the mass in Quettagrams (Qg).
    /// </summary>
    public T QuettaGrams => QuectoGrams.ToQuettaUnits();

    /// <summary>
    /// Gets the mass in Tonnes (t).
    /// </summary>
    public T Tonnes => QuectoGrams / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the mass in Ounces (oz).
    /// </summary>
    public T Ounces => QuectoGrams / T.CreateChecked(28.349523125e30);

    /// <summary>
    /// Gets the mass in Pounds (lb).
    /// </summary>
    public T Pounds => QuectoGrams / T.CreateChecked(453.59237e30);

    /// <summary>
    /// Gets the mass in Stones (st).
    /// </summary>
    public T Stones => QuectoGrams / T.CreateChecked(6350.29318e30);

    /// <summary>
    /// Gets the mass in Short Tons (sht).
    /// </summary>
    public T ShortTons => QuectoGrams / T.CreateChecked(907184.74e30);

    /// <summary>
    /// Gets the mass in Long Tons (lt).
    /// </summary>
    public T LongTons => QuectoGrams / T.CreateChecked(1016046.9088e30);

    /// <summary>
    /// Gets the mass in Carats (ct).
    /// </summary>
    public T Carats => QuectoGrams / T.CreateChecked(0.2e30);

    /// <summary>
    /// Gets the mass in Grains (gr).
    /// </summary>
    public T Grains => QuectoGrams / T.CreateChecked(0.06479891e30);

    /// <summary>
    /// Gets the mass in Drams (dr).
    /// </summary>
    public T Drams => QuectoGrams / T.CreateChecked(1.7718451953125e30);

    /// <summary>
    /// Gets the mass in Slugs (slug).
    /// </summary>
    public T Slugs => QuectoGrams / T.CreateChecked(14593.90293720636e30);

    /// <summary>
    /// Gets the mass in Daltons (Da).
    /// </summary>
    public T Daltons => QuectoGrams / T.CreateChecked(1.66053906660e6);
}
