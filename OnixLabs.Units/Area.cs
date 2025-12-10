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
public readonly partial struct Area<T> : IUnit<Area<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Area{T}"/> struct.
    /// </summary>
    /// <param name="value">The area unit in <see cref="SquareQuectoMeters"/>.</param>
    private Area(T value) => SquareQuectoMeters = value;

    /// <summary>
    /// Gets the area in Square Quectometers (qm²).
    /// </summary>
    public T SquareQuectoMeters { get; }

    /// <summary>
    /// Gets the area in Square Rontometers (rm²).
    /// </summary>
    public T SquareRontoMeters => SquareQuectoMeters / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the area in Square Yoctometers (ym²).
    /// </summary>
    public T SquareYoctoMeters => SquareQuectoMeters / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the area in Square Zeptometers (zm²).
    /// </summary>
    public T SquareZeptoMeters => SquareQuectoMeters / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the area in Square Attometers (am²).
    /// </summary>
    public T SquareAttoMeters => SquareQuectoMeters / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the area in Square Femtometers (fm²).
    /// </summary>
    public T SquareFemtoMeters => SquareQuectoMeters / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the area in Square Picometers (pm²).
    /// </summary>
    public T SquarePicoMeters => SquareQuectoMeters / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the area in Square Nanometers (nm²).
    /// </summary>
    public T SquareNanoMeters => SquareQuectoMeters / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the area in Square Micrometers (µm²).
    /// </summary>
    public T SquareMicroMeters => SquareQuectoMeters / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the area in Square Millimeters (mm²).
    /// </summary>
    public T SquareMilliMeters => SquareQuectoMeters / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the area in Square Centimeters (cm²).
    /// </summary>
    public T SquareCentiMeters => SquareQuectoMeters / T.CreateChecked(1e56);

    /// <summary>
    /// Gets the area in Square Decimeters (dm²).
    /// </summary>
    public T SquareDeciMeters => SquareQuectoMeters / T.CreateChecked(1e58);

    /// <summary>
    /// Gets the area in Square Meters (m²).
    /// </summary>
    public T SquareMeters => SquareQuectoMeters / T.CreateChecked(1e60);

    /// <summary>
    /// Gets the area in Square Decameters (dam²).
    /// </summary>
    public T SquareDecaMeters => SquareQuectoMeters / T.CreateChecked(1e62);

    /// <summary>
    /// Gets the area in Square Hectometers (hm²).
    /// </summary>
    public T SquareHectoMeters => SquareQuectoMeters / T.CreateChecked(1e64);

    /// <summary>
    /// Gets the area in Square Kilometers (km²).
    /// </summary>
    public T SquareKiloMeters => SquareQuectoMeters / T.CreateChecked(1e66);

    /// <summary>
    /// Gets the area in Square Megameters (Mm²).
    /// </summary>
    public T SquareMegaMeters => SquareQuectoMeters / T.CreateChecked(1e72);

    /// <summary>
    /// Gets the area in Square Gigameters (Gm²).
    /// </summary>
    public T SquareGigaMeters => SquareQuectoMeters / T.CreateChecked(1e78);

    /// <summary>
    /// Gets the area in Square Terameters (Tm²).
    /// </summary>
    public T SquareTeraMeters => SquareQuectoMeters / T.CreateChecked(1e84);

    /// <summary>
    /// Gets the area in Square Petameters (Pm²).
    /// </summary>
    public T SquarePetaMeters => SquareQuectoMeters / T.CreateChecked(1e90);

    /// <summary>
    /// Gets the area in Square Exameters (Em²).
    /// </summary>
    public T SquareExaMeters => SquareQuectoMeters / T.CreateChecked(1e96);

    /// <summary>
    /// Gets the area in Square Zettameters (Zm²).
    /// </summary>
    public T SquareZettaMeters => SquareQuectoMeters / T.CreateChecked(1e102);

    /// <summary>
    /// Gets the area in Square Yottameters (Ym²).
    /// </summary>
    public T SquareYottaMeters => SquareQuectoMeters / T.CreateChecked(1e108);

    /// <summary>
    /// Gets the area in Square Ronnameters (Rm²).
    /// </summary>
    public T SquareRonnaMeters => SquareQuectoMeters / T.CreateChecked(1e114);

    /// <summary>
    /// Gets the area in Square Quettameters (Qm²).
    /// </summary>
    public T SquareQuettaMeters => SquareQuectoMeters / T.CreateChecked(1e120);

    /// <summary>
    /// Gets the area in Square Inches (in²).
    /// </summary>
    public T SquareInches => SquareQuectoMeters / T.CreateChecked(6.4516e56);

    /// <summary>
    /// Gets the area in Square Feet (ft²).
    /// </summary>
    public T SquareFeet => SquareQuectoMeters / T.CreateChecked(9.290304e58);

    /// <summary>
    /// Gets the area in Square Yards (yd²).
    /// </summary>
    public T SquareYards => SquareQuectoMeters / T.CreateChecked(8.3612736e59);

    /// <summary>
    /// Gets the area in Square Miles (mi²).
    /// </summary>
    public T SquareMiles => SquareQuectoMeters / T.CreateChecked(2.589988110336e66);

    /// <summary>
    /// Gets the area in Square Nautical Miles (nmi²).
    /// </summary>
    public T SquareNauticalMiles => SquareQuectoMeters / T.CreateChecked(3.4299040000e66);

    /// <summary>
    /// Gets the area in Square Fermis (fmi²).
    /// </summary>
    public T SquareFermis => SquareQuectoMeters / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the area in Square Angstroms (Å²).
    /// </summary>
    public T SquareAngstroms => SquareQuectoMeters / T.CreateChecked(1e40);

    /// <summary>
    /// Gets the area in Square Astronomical Units (au²).
    /// </summary>
    public T SquareAstronomicalUnits => SquareQuectoMeters / T.CreateChecked(2.2379522821e82);

    /// <summary>
    /// Gets the area in Square Light Years (ly²).
    /// </summary>
    public T SquareLightYears => SquareQuectoMeters / T.CreateChecked(8.9505421074819e91);

    /// <summary>
    /// Gets the area in Square Parsecs (pc²).
    /// </summary>
    public T SquareParsecs
    {
        get
        {
            T metersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
            T sqMetersPerSqParsec = metersPerParsec * metersPerParsec;
            T sqQmPerSqParsec = sqMetersPerSqParsec * T.CreateChecked(1e60);
            return SquareQuectoMeters / sqQmPerSqParsec;
        }
    }
}
