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
/// Represents a unit of pressure.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Pressure<T> :
    IValueEquatable<Pressure<T>>,
    IValueComparable<Pressure<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pressure{T}"/> struct.
    /// </summary>
    /// <param name="value">The pressure unit in <see cref="QuectoPascals"/>.</param>
    private Pressure(T value) => QuectoPascals = value;

    /// <summary>
    /// Gets the pressure in quectopascals (qPa).
    /// </summary>
    public T QuectoPascals { get; }

    /// <summary>
    /// Gets the pressure in rontopascals (rPa).
    /// </summary>
    public T RontoPascals => QuectoPascals / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the pressure in yoctopascals (yPa).
    /// </summary>
    public T YoctoPascals => QuectoPascals / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the pressure in zeptopascals (zPa).
    /// </summary>
    public T ZeptoPascals => QuectoPascals / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the pressure in attopascals (aPa).
    /// </summary>
    public T AttoPascals => QuectoPascals / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the pressure in femtopascals (fPa).
    /// </summary>
    public T FemtoPascals => QuectoPascals / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the pressure in picopascals (pPa).
    /// </summary>
    public T PicoPascals => QuectoPascals / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the pressure in nanopascals (nPa).
    /// </summary>
    public T NanoPascals => QuectoPascals / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the pressure in micropascals (µPa).
    /// </summary>
    public T MicroPascals => QuectoPascals / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the pressure in millipascals (mPa).
    /// </summary>
    public T MilliPascals => QuectoPascals / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the pressure in centipascals (cPa).
    /// </summary>
    public T CentiPascals => QuectoPascals / T.CreateChecked(1e28);

    /// <summary>
    /// Gets the pressure in decipascals (dPa).
    /// </summary>
    public T DeciPascals => QuectoPascals / T.CreateChecked(1e29);

    /// <summary>
    /// Gets the pressure in pascals (Pa).
    /// </summary>
    public T Pascals => QuectoPascals / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the pressure in decapascals (daPa).
    /// </summary>
    public T DecaPascals => QuectoPascals / T.CreateChecked(1e31);

    /// <summary>
    /// Gets the pressure in hectopascals (hPa).
    /// </summary>
    public T HectoPascals => QuectoPascals / T.CreateChecked(1e32);

    /// <summary>
    /// Gets the pressure in kilopascals (kPa).
    /// </summary>
    public T KiloPascals => QuectoPascals / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the pressure in megapascals (MPa).
    /// </summary>
    public T MegaPascals => QuectoPascals / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the pressure in gigapascals (GPa).
    /// </summary>
    public T GigaPascals => QuectoPascals / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the pressure in terapascals (TPa).
    /// </summary>
    public T TeraPascals => QuectoPascals / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the pressure in petapascals (PPa).
    /// </summary>
    public T PetaPascals => QuectoPascals / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the pressure in exapascals (EPa).
    /// </summary>
    public T ExaPascals => QuectoPascals / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the pressure in zettapascals (ZPa).
    /// </summary>
    public T ZettaPascals => QuectoPascals / T.CreateChecked(1e51);

    /// <summary>
    /// Gets the pressure in yottapascals (YPa).
    /// </summary>
    public T YottaPascals => QuectoPascals / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the pressure in ronnapascals (RPa).
    /// </summary>
    public T RonnaPascals => QuectoPascals / T.CreateChecked(1e57);

    /// <summary>
    /// Gets the pressure in quettapascals (QPa).
    /// </summary>
    public T QuettaPascals => QuectoPascals / T.CreateChecked(1e60);

    /// <summary>
    /// Gets the pressure in bars (bar).
    /// </summary>
    public T Bars => QuectoPascals / T.CreateChecked(1e35);

    /// <summary>
    /// Gets the pressure in millibars (mbar).
    /// </summary>
    public T Millibars => QuectoPascals / T.CreateChecked(1e32);

    /// <summary>
    /// Gets the pressure in standard atmospheres (atm).
    /// </summary>
    public T Atmospheres => QuectoPascals / T.CreateChecked(101325e30);

    /// <summary>
    /// Gets the pressure in technical atmospheres (at).
    /// </summary>
    public T TechnicalAtmospheres => QuectoPascals / T.CreateChecked(98066.5e30);

    /// <summary>
    /// Gets the pressure in torr (Torr).
    /// </summary>
    public T Torr => QuectoPascals / T.CreateChecked(133.32236842105263e30);

    /// <summary>
    /// Gets the pressure in millimetres of mercury (mmHg).
    /// </summary>
    public T MillimetersOfMercury => QuectoPascals / T.CreateChecked(133.322387415e30);

    /// <summary>
    /// Gets the pressure in inches of mercury (inHg).
    /// </summary>
    public T InchesOfMercury => QuectoPascals / T.CreateChecked(3386.389e30);

    /// <summary>
    /// Gets the pressure in pounds per square inch (psi).
    /// </summary>
    public T PoundsPerSquareInch => QuectoPascals / T.CreateChecked(6894.757293168e30);

    /// <summary>
    /// Gets the pressure in pounds per square foot (psf).
    /// </summary>
    public T PoundsPerSquareFoot => QuectoPascals / T.CreateChecked(47.88025898e30);

    /// <summary>
    /// Gets the pressure in barye (Ba).
    /// </summary>
    public T Barye => QuectoPascals / T.CreateChecked(0.1e30);

    /// <summary>
    /// Gets the pressure in millimetres of water column (mmH₂O).
    /// </summary>
    public T MillimetersOfWaterColumn => QuectoPascals / T.CreateChecked(9.80665e30);

    /// <summary>
    /// Gets the pressure in inches of water column (inH₂O).
    /// </summary>
    public T InchesOfWaterColumn => QuectoPascals / T.CreateChecked(249.08891e30);
}
