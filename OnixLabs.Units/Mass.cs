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
/// Represents a unit of mass.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Mass<T> :
    IValueEquatable<Mass<T>>,
    IValueComparable<Mass<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mass{T}"/> struct.
    /// </summary>
    /// <param name="value">The mass unit in <see cref="YoctoGrams"/>.</param>
    private Mass(T value) => YoctoGrams = value;

    /// <summary>
    /// Gets the mass in yoctograms (yg).
    /// </summary>
    public T YoctoGrams { get; }

    /// <summary>
    /// Gets the mass in zeptograms (zg).
    /// </summary>
    public T ZeptoGrams => YoctoGrams / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the mass in attograms (ag).
    /// </summary>
    public T AttoGrams => YoctoGrams / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the mass in femtograms (fg).
    /// </summary>
    public T FemtoGrams => YoctoGrams / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the mass in picograms (pg).
    /// </summary>
    public T PicoGrams => YoctoGrams / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the mass in nanograms (ng).
    /// </summary>
    public T NanoGrams => YoctoGrams / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the mass in micrograms (ug).
    /// </summary>
    public T MicroGrams => YoctoGrams / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the mass in milligrams (mg).
    /// </summary>
    public T MilliGrams => YoctoGrams / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the mass in grams (g).
    /// </summary>
    public T Grams => YoctoGrams / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the mass in kilograms (kg).
    /// </summary>
    public T KiloGrams => YoctoGrams / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the mass in megagrams (Mg).
    /// </summary>
    public T MegaGrams => YoctoGrams / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the mass in tonnes (t).
    /// </summary>
    /// <remarks>
    /// This value is the same as <see cref="MegaGrams"/>.
    /// </remarks>
    public T Tonnes => MegaGrams;

    /// <summary>
    /// Gets the mass in gigagrams (Gg).
    /// </summary>
    public T GigaGrams => YoctoGrams / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the mass in teragrams (Tg).
    /// </summary>
    public T TeraGrams => YoctoGrams / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the mass in petagrams (Pg).
    /// </summary>
    public T PetaGrams => YoctoGrams / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the mass in exagrams (Eg).
    /// </summary>
    public T ExaGrams => YoctoGrams / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the mass in zettagrams (Zg).
    /// </summary>
    public T ZettaGrams => YoctoGrams / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the mass in yottagrams (Yg).
    /// </summary>
    public T YottaGrams => YoctoGrams / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the mass in avoirdupois pounds (lb).
    /// </summary>
    public T Pounds => YoctoGrams / T.CreateChecked(4.5359237e26);

    /// <summary>
    /// Gets the mass in avoirdupois ounces (oz).
    /// </summary>
    public T Ounces => YoctoGrams / T.CreateChecked(2.8349523125e25);

    /// <summary>
    /// Gets the mass in stones (st).
    /// </summary>
    public T Stones => YoctoGrams / T.CreateChecked(6.35029318e27);

    /// <summary>
    /// Gets the mass in grains (gr).
    /// </summary>
    public T Grains => YoctoGrams / T.CreateChecked(6.479891e22);

    /// <summary>
    /// Gets the mass in US short tons (ton).
    /// </summary>
    public T ShortTons => YoctoGrams / T.CreateChecked(9.0718474e29);

    /// <summary>
    /// Gets the mass in imperial long tons (lt).
    /// </summary>
    public T LongTons => YoctoGrams / T.CreateChecked(1.0160469088e30);

    /// <summary>
    /// Gets the mass in US hundredweight (cwtUS).
    /// </summary>
    public T HundredweightUs => YoctoGrams / T.CreateChecked(4.5359237e28);

    /// <summary>
    /// Gets the mass in UK (imperial) hundredweight (cwtUK).
    /// </summary>
    public T HundredweightUk => YoctoGrams / T.CreateChecked(5.080234544e28);

    /// <summary>
    /// Gets the mass in quarters (qr).
    /// </summary>
    public T Quarters => YoctoGrams / T.CreateChecked(1.270058636e28);

    /// <summary>
    /// Gets the mass in troy pounds (lbt).
    /// </summary>
    public T TroyPounds => YoctoGrams / T.CreateChecked(3.732417216e26);

    /// <summary>
    /// Gets the mass in troy ounces (ozt).
    /// </summary>
    public T TroyOunces => YoctoGrams / T.CreateChecked(3.11034768e25);

    /// <summary>
    /// Gets the mass in pennyweights (dwt).
    /// </summary>
    public T Pennyweights => YoctoGrams / T.CreateChecked(1.55517384e24);
}
