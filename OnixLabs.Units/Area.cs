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
/// Represents a unit of area.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Area<T> :
    ICanonicalUnit<T>,
    IAdditiveUnit<Area<T>>,
    IMultiplicativeUnit<Area<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Area{T}"/> struct.
    /// </summary>
    /// <param name="value">The area unit in <see cref="SquareQuectoMeters"/>.</param>
    private Area(T value) => Canonical = value;

    /// <inheritdoc/>
    public T Canonical { get; }

    /// <summary>
    /// Gets the area in Square Quectometers (qm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqqm.
    /// </remarks>
    public T SquareQuectoMeters => Canonical;

    /// <summary>
    /// Gets the area in Square Rontometers (rm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqrm.
    /// </remarks>
    public T SquareRontoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(6);

    /// <summary>
    /// Gets the area in Square Yoctometers (ym²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqym.
    /// </remarks>
    public T SquareYoctoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(12);

    /// <summary>
    /// Gets the area in Square Zeptometers (zm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqzm.
    /// </remarks>
    public T SquareZeptoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(18);

    /// <summary>
    /// Gets the area in Square Attometers (am²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqam.
    /// </remarks>
    public T SquareAttoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(24);

    /// <summary>
    /// Gets the area in Square Femtometers (fm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqfm.
    /// </remarks>
    public T SquareFemtoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(30);

    /// <summary>
    /// Gets the area in Square Picometers (pm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqpm.
    /// </remarks>
    public T SquarePicoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(36);

    /// <summary>
    /// Gets the area in Square Nanometers (nm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqnm.
    /// </remarks>
    public T SquareNanoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(42);

    /// <summary>
    /// Gets the area in Square Micrometers (µm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is squm.
    /// </remarks>
    public T SquareMicroMeters => SquareQuectoMeters / UnitMath.Pow10<T>(48);

    /// <summary>
    /// Gets the area in Square Millimeters (mm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqmm.
    /// </remarks>
    public T SquareMilliMeters => SquareQuectoMeters / UnitMath.Pow10<T>(54);

    /// <summary>
    /// Gets the area in Square Centimeters (cm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqcm.
    /// </remarks>
    public T SquareCentiMeters => SquareQuectoMeters / UnitMath.Pow10<T>(56);

    /// <summary>
    /// Gets the area in Square Decimeters (dm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqdm.
    /// </remarks>
    public T SquareDeciMeters => SquareQuectoMeters / UnitMath.Pow10<T>(58);

    /// <summary>
    /// Gets the area in Square Meters (m²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqm.
    /// </remarks>
    public T SquareMeters => SquareQuectoMeters / UnitMath.Pow10<T>(60);

    /// <summary>
    /// Gets the area in Square Decameters (dam²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqdam.
    /// </remarks>
    public T SquareDecaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(62);

    /// <summary>
    /// Gets the area in Square Hectometers (hm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqhm.
    /// </remarks>
    public T SquareHectoMeters => SquareQuectoMeters / UnitMath.Pow10<T>(64);

    /// <summary>
    /// Gets the area in Square Kilometers (km²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqkm.
    /// </remarks>
    public T SquareKiloMeters => SquareQuectoMeters / UnitMath.Pow10<T>(66);

    /// <summary>
    /// Gets the area in Square Megameters (Mm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqMm.
    /// </remarks>
    public T SquareMegaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(72);

    /// <summary>
    /// Gets the area in Square Gigameters (Gm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqGm.
    /// </remarks>
    public T SquareGigaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(78);

    /// <summary>
    /// Gets the area in Square Terameters (Tm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqTm.
    /// </remarks>
    public T SquareTeraMeters => SquareQuectoMeters / UnitMath.Pow10<T>(84);

    /// <summary>
    /// Gets the area in Square Petameters (Pm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqPm.
    /// </remarks>
    public T SquarePetaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(90);

    /// <summary>
    /// Gets the area in Square Exameters (Em²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqEm.
    /// </remarks>
    public T SquareExaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(96);

    /// <summary>
    /// Gets the area in Square Zettameters (Zm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqZm.
    /// </remarks>
    public T SquareZettaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(102);

    /// <summary>
    /// Gets the area in Square Yottameters (Ym²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqYm.
    /// </remarks>
    public T SquareYottaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(108);

    /// <summary>
    /// Gets the area in Square Ronnameters (Rm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqRm.
    /// </remarks>
    public T SquareRonnaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(114);

    /// <summary>
    /// Gets the area in Square Quettameters (Qm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqQm.
    /// </remarks>
    public T SquareQuettaMeters => SquareQuectoMeters / UnitMath.Pow10<T>(120);

    /// <summary>
    /// Gets the area in Square Inches (in²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqin.
    /// </remarks>
    public T SquareInches => SquareQuectoMeters / SqQuectometersPerSquareInch;

    /// <summary>
    /// Gets the area in Square Feet (ft²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqft.
    /// </remarks>
    public T SquareFeet => SquareQuectoMeters / SqQuectometersPerSquareFoot;

    /// <summary>
    /// Gets the area in Square Yards (yd²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqyd.
    /// </remarks>
    public T SquareYards => SquareQuectoMeters / SqQuectometersPerSquareYard;

    /// <summary>
    /// Gets the area in Square Miles (mi²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqmi.
    /// </remarks>
    public T SquareMiles => SquareQuectoMeters / SqQuectometersPerSquareMile;

    /// <summary>
    /// Gets the area in Square Nautical Miles (nmi²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqnmi.
    /// </remarks>
    public T SquareNauticalMiles => SquareQuectoMeters / SqQuectometersPerSquareNauticalMile;

    /// <summary>
    /// Gets the area in Square Fermis (fm²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqfmi.
    /// </remarks>
    public T SquareFermis => SquareQuectoMeters / SqQuectometersPerSquareFermi;

    /// <summary>
    /// Gets the area in Square Angstroms (Å²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqa.
    /// </remarks>
    public T SquareAngstroms => SquareQuectoMeters / SqQuectometersPerSquareAngstrom;

    /// <summary>
    /// Gets the area in Square Astronomical Units (au²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqau.
    /// </remarks>
    public T SquareAstronomicalUnits => SquareQuectoMeters / SqQuectometersPerSquareAstronomicalUnit;

    /// <summary>
    /// Gets the area in Square Light Years (ly²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqly.
    /// </remarks>
    public T SquareLightYears => SquareQuectoMeters / SqQuectometersPerSquareLightYear;

    /// <summary>
    /// Gets the area in Square Parsecs (pc²).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is sqpc.
    /// </remarks>
    public T SquareParsecs => SquareQuectoMeters / SqQuectometersPerSquareParsec;
}
