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
public readonly partial struct Mass<T> :
    ICanonicalUnit<T>,
    IAdditiveUnit<Mass<T>>,
    IMultiplicativeUnit<Mass<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mass{T}"/> struct.
    /// </summary>
    /// <param name="value">The mass unit in <see cref="QuectoGrams"/>.</param>
    private Mass(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>
    /// Gets the mass in Quectograms (qg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qg.
    /// </remarks>
    public T QuectoGrams => Canonical;

    /// <summary>
    /// Gets the mass in Rontograms (rg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rg.
    /// </remarks>
    public T RontoGrams => QuectoGrams.ToRontoUnits();

    /// <summary>
    /// Gets the mass in Yoctograms (yg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yg.
    /// </remarks>
    public T YoctoGrams => QuectoGrams.ToYoctoUnits();

    /// <summary>
    /// Gets the mass in Zeptograms (zg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zg.
    /// </remarks>
    public T ZeptoGrams => QuectoGrams.ToZeptoUnits();

    /// <summary>
    /// Gets the mass in Attograms (ag).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ag.
    /// </remarks>
    public T AttoGrams => QuectoGrams.ToAttoUnits();

    /// <summary>
    /// Gets the mass in Femtograms (fg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fg.
    /// </remarks>
    public T FemtoGrams => QuectoGrams.ToFemtoUnits();

    /// <summary>
    /// Gets the mass in Picograms (pg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pg.
    /// </remarks>
    public T PicoGrams => QuectoGrams.ToPicoUnits();

    /// <summary>
    /// Gets the mass in Nanograms (ng).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ng.
    /// </remarks>
    public T NanoGrams => QuectoGrams.ToNanoUnits();

    /// <summary>
    /// Gets the mass in Micrograms (µg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ug.
    /// </remarks>
    public T MicroGrams => QuectoGrams.ToMicroUnits();

    /// <summary>
    /// Gets the mass in Milligrams (mg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mg.
    /// </remarks>
    public T MilliGrams => QuectoGrams.ToMilliUnits();

    /// <summary>
    /// Gets the mass in Centigrams (cg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cg.
    /// </remarks>
    public T CentiGrams => QuectoGrams.ToCentiUnits();

    /// <summary>
    /// Gets the mass in Decigrams (dg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dg.
    /// </remarks>
    public T DeciGrams => QuectoGrams.ToDeciUnits();

    /// <summary>
    /// Gets the mass in Grams (g).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is g.
    /// </remarks>
    public T Grams => QuectoGrams.ToBaseUnits();

    /// <summary>
    /// Gets the mass in Decagrams (dag).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dag.
    /// </remarks>
    public T DecaGrams => QuectoGrams.ToDecaUnits();

    /// <summary>
    /// Gets the mass in Hectograms (hg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hg.
    /// </remarks>
    public T HectoGrams => QuectoGrams.ToHectoUnits();

    /// <summary>
    /// Gets the mass in Kilograms (kg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kg.
    /// </remarks>
    public T KiloGrams => QuectoGrams.ToKiloUnits();

    /// <summary>
    /// Gets the mass in Megagrams (Mg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mg.
    /// </remarks>
    public T MegaGrams => QuectoGrams.ToMegaUnits();

    /// <summary>
    /// Gets the mass in Gigagrams (Gg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gg.
    /// </remarks>
    public T GigaGrams => QuectoGrams.ToGigaUnits();

    /// <summary>
    /// Gets the mass in Teragrams (Tg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tg.
    /// </remarks>
    public T TeraGrams => QuectoGrams.ToTeraUnits();

    /// <summary>
    /// Gets the mass in Petagrams (Pg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pg.
    /// </remarks>
    public T PetaGrams => QuectoGrams.ToPetaUnits();

    /// <summary>
    /// Gets the mass in Exagrams (Eg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Eg.
    /// </remarks>
    public T ExaGrams => QuectoGrams.ToExaUnits();

    /// <summary>
    /// Gets the mass in Zettagrams (Zg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zg.
    /// </remarks>
    public T ZettaGrams => QuectoGrams.ToZettaUnits();

    /// <summary>
    /// Gets the mass in Yottagrams (Yg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Yg.
    /// </remarks>
    public T YottaGrams => QuectoGrams.ToYottaUnits();

    /// <summary>
    /// Gets the mass in Ronnagrams (Rg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rg.
    /// </remarks>
    public T RonnaGrams => QuectoGrams.ToRonnaUnits();

    /// <summary>
    /// Gets the mass in Quettagrams (Qg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qg.
    /// </remarks>
    public T QuettaGrams => QuectoGrams.ToQuettaUnits();

    /// <summary>
    /// Gets the mass in Tonnes (t).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is t.
    /// </remarks>
    public T Tonnes => QuectoGrams / QuectogramsPerTonne;

    /// <summary>
    /// Gets the mass in Ounces (oz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is oz.
    /// </remarks>
    public T Ounces => QuectoGrams / QuectogramsPerOunce;

    /// <summary>
    /// Gets the mass in Pounds (lb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is lb.
    /// </remarks>
    public T Pounds => QuectoGrams / QuectogramsPerPound;

    /// <summary>
    /// Gets the mass in Stones (st).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is st.
    /// </remarks>
    public T Stones => QuectoGrams / QuectogramsPerStone;

    /// <summary>
    /// Gets the mass in Short Tons (sht).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sht.
    /// </remarks>
    public T ShortTons => QuectoGrams / QuectogramsPerShortTon;

    /// <summary>
    /// Gets the mass in Long Tons (lt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is lt.
    /// </remarks>
    public T LongTons => QuectoGrams / QuectogramsPerLongTon;

    /// <summary>
    /// Gets the mass in Carats (ct).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ct.
    /// </remarks>
    public T Carats => QuectoGrams / QuectogramsPerCarat;

    /// <summary>
    /// Gets the mass in Grains (gr).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is gr.
    /// </remarks>
    public T Grains => QuectoGrams / QuectogramsPerGrain;

    /// <summary>
    /// Gets the mass in Drams (dr).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dr.
    /// </remarks>
    public T Drams => QuectoGrams / QuectogramsPerDram;

    /// <summary>
    /// Gets the mass in Slugs (slug).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is slug.
    /// </remarks>
    public T Slugs => QuectoGrams / QuectogramsPerSlug;

    /// <summary>
    /// Gets the mass in Daltons (Da).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Da.
    /// </remarks>
    public T Daltons => QuectoGrams / QuectogramsPerDalton;
}
