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
/// Represents a unit of pressure.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Pressure<T> : IUnit<Pressure<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pressure{T}"/> struct.
    /// </summary>
    /// <param name="value">The pressure unit in <see cref="QuectoPascals"/>.</param>
    private Pressure(T value) => QuectoPascals = value;

    /// <summary>
    /// Gets the pressure in Quectopascals (qPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qPa.
    /// </remarks>
    public T QuectoPascals { get; }

    /// <summary>
    /// Gets the pressure in Rontopascals (rPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rPa.
    /// </remarks>
    public T RontoPascals => QuectoPascals.ToRontoUnits();

    /// <summary>
    /// Gets the pressure in Yoctopascals (yPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yPa.
    /// </remarks>
    public T YoctoPascals => QuectoPascals.ToYoctoUnits();

    /// <summary>
    /// Gets the pressure in Zeptopascals (zPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zPa.
    /// </remarks>
    public T ZeptoPascals => QuectoPascals.ToZeptoUnits();

    /// <summary>
    /// Gets the pressure in Attopascals (aPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aPa.
    /// </remarks>
    public T AttoPascals => QuectoPascals.ToAttoUnits();

    /// <summary>
    /// Gets the pressure in Femtopascals (fPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fPa.
    /// </remarks>
    public T FemtoPascals => QuectoPascals.ToFemtoUnits();

    /// <summary>
    /// Gets the pressure in Picopascals (pPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pPa.
    /// </remarks>
    public T PicoPascals => QuectoPascals.ToPicoUnits();

    /// <summary>
    /// Gets the pressure in Nanopascals (nPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nPa.
    /// </remarks>
    public T NanoPascals => QuectoPascals.ToNanoUnits();

    /// <summary>
    /// Gets the pressure in Micropascals (µPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uPa.
    /// </remarks>
    public T MicroPascals => QuectoPascals.ToMicroUnits();

    /// <summary>
    /// Gets the pressure in Millipascals (mPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mPa.
    /// </remarks>
    public T MilliPascals => QuectoPascals.ToMilliUnits();

    /// <summary>
    /// Gets the pressure in Centipascals (cPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cPa.
    /// </remarks>
    public T CentiPascals => QuectoPascals.ToCentiUnits();

    /// <summary>
    /// Gets the pressure in Decipascals (dPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dPa.
    /// </remarks>
    public T DeciPascals => QuectoPascals.ToDeciUnits();

    /// <summary>
    /// Gets the pressure in Pascals (Pa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pa.
    /// </remarks>
    public T Pascals => QuectoPascals.ToBaseUnits();

    /// <summary>
    /// Gets the pressure in Decapascals (daPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daPa.
    /// </remarks>
    public T DecaPascals => QuectoPascals.ToDecaUnits();

    /// <summary>
    /// Gets the pressure in Hectopascals (hPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hPa.
    /// </remarks>
    public T HectoPascals => QuectoPascals.ToHectoUnits();

    /// <summary>
    /// Gets the pressure in Kilopascals (kPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kPa.
    /// </remarks>
    public T KiloPascals => QuectoPascals.ToKiloUnits();

    /// <summary>
    /// Gets the pressure in Megapascals (MPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MPa.
    /// </remarks>
    public T MegaPascals => QuectoPascals.ToMegaUnits();

    /// <summary>
    /// Gets the pressure in Gigapascals (GPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GPa.
    /// </remarks>
    public T GigaPascals => QuectoPascals.ToGigaUnits();

    /// <summary>
    /// Gets the pressure in Terapascals (TPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TPa.
    /// </remarks>
    public T TeraPascals => QuectoPascals.ToTeraUnits();

    /// <summary>
    /// Gets the pressure in Petapascals (PPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PPa.
    /// </remarks>
    public T PetaPascals => QuectoPascals.ToPetaUnits();

    /// <summary>
    /// Gets the pressure in Exapascals (EPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EPa.
    /// </remarks>
    public T ExaPascals => QuectoPascals.ToExaUnits();

    /// <summary>
    /// Gets the pressure in Zettapascals (ZPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZPa.
    /// </remarks>
    public T ZettaPascals => QuectoPascals.ToZettaUnits();

    /// <summary>
    /// Gets the pressure in Yottapascals (YPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YPa.
    /// </remarks>
    public T YottaPascals => QuectoPascals.ToYottaUnits();

    /// <summary>
    /// Gets the pressure in Ronnapascals (RPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RPa.
    /// </remarks>
    public T RonnaPascals => QuectoPascals.ToRonnaUnits();

    /// <summary>
    /// Gets the pressure in Quettapascals (QPa).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QPa.
    /// </remarks>
    public T QuettaPascals => QuectoPascals.ToQuettaUnits();

    /// <summary>
    /// Gets the pressure in Bar (bar).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is bar.
    /// </remarks>
    public T Bar => QuectoPascals / T.CreateChecked(1e35);

    /// <summary>
    /// Gets the pressure in Millibar (mbar).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mbar.
    /// </remarks>
    public T Millibar => QuectoPascals / T.CreateChecked(1e32);

    /// <summary>
    /// Gets the pressure in Atmospheres (atm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is atm.
    /// </remarks>
    public T Atmospheres => QuectoPascals / T.CreateChecked(1.01325e35);

    /// <summary>
    /// Gets the pressure in Torr (Torr).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Torr.
    /// </remarks>
    public T Torr => QuectoPascals.ToBaseUnits() * T.CreateChecked(760) / T.CreateChecked(101325);

    /// <summary>
    /// Gets the pressure in Millimeters Of Mercury (mmHg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mmHg.
    /// </remarks>
    public T MillimetersOfMercury => QuectoPascals / T.CreateChecked(1.33322387415e32);

    /// <summary>
    /// Gets the pressure in Inches Of Mercury (inHg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is inHg.
    /// </remarks>
    public T InchesOfMercury => QuectoPascals / T.CreateChecked(3.386389e33);

    /// <summary>
    /// Gets the pressure in Pounds Per Square Inch (psi).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is psi.
    /// </remarks>
    public T PoundsPerSquareInch => QuectoPascals / T.CreateChecked(6.894757293168361e33);

    /// <summary>
    /// Gets the pressure in Kilopounds Per Square Inch (ksi).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ksi.
    /// </remarks>
    public T KilopoundsPerSquareInch => QuectoPascals / T.CreateChecked(6.894757293168361e36);

    /// <summary>
    /// Gets the pressure in Technical Atmospheres (at).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is at.
    /// </remarks>
    public T TechnicalAtmospheres => QuectoPascals / T.CreateChecked(9.80665e34);

    /// <summary>
    /// Gets the pressure in Baryes (Ba).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ba.
    /// </remarks>
    public T Baryes => QuectoPascals / T.CreateChecked(1e29);
}
