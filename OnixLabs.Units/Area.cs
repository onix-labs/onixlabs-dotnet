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
/// Represents a unit of area.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Area<T> :
    IValueEquatable<Area<T>>,
    IValueComparable<Area<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Area{T}"/> struct.
    /// </summary>
    /// <param name="value">The area unit in <see cref="SquareYoctoMeters"/>.</param>
    private Area(T value) => SquareYoctoMeters = value;

    /// <summary>
    /// Gets the area in square yoctometers (sqym).
    /// </summary>
    public T SquareYoctoMeters { get; }

    /// <summary>
    /// Gets the area in square zeptometers (sqzm).
    /// </summary>
    public T SquareZeptoMeters => SquareYoctoMeters / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the area in square attometers (sqam).
    /// </summary>
    public T SquareAttoMeters => SquareYoctoMeters / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the area in square femtometers (sqfm).
    /// </summary>
    public T SquareFemtoMeters => SquareYoctoMeters / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the area in square picometers (sqpm).
    /// </summary>
    public T SquarePicoMeters => SquareYoctoMeters / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the area in square nanometers (sqnm).
    /// </summary>
    public T SquareNanoMeters => SquareYoctoMeters / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the area in square micrometers (squm).
    /// </summary>
    public T SquareMicroMeters => SquareYoctoMeters / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the area in square millimeters (sqmm).
    /// </summary>
    public T SquareMilliMeters => SquareYoctoMeters / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the area in square centimeters (sqcm).
    /// </summary>
    public T SquareCentiMeters => SquareYoctoMeters / T.CreateChecked(1e44);

    /// <summary>
    /// Gets the area in square decimeters (sqdm).
    /// </summary>
    public T SquareDeciMeters => SquareYoctoMeters / T.CreateChecked(1e46);

    /// <summary>
    /// Gets the area in square meters (sqm).
    /// </summary>
    public T SquareMeters => SquareYoctoMeters / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the area in square decameters (sqdam).
    /// </summary>
    public T SquareDecaMeters => SquareYoctoMeters / T.CreateChecked(1e50);

    /// <summary>
    /// Gets the area in square hectometers (sqhm).
    /// </summary>
    public T SquareHectoMeters => SquareYoctoMeters / T.CreateChecked(1e52);

    /// <summary>
    /// Gets the area in square kilometers (sqkm).
    /// </summary>
    public T SquareKiloMeters => SquareYoctoMeters / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the area in square megameters (sqMm).
    /// </summary>
    public T SquareMegaMeters => SquareYoctoMeters / T.CreateChecked(1e60);

    /// <summary>
    /// Gets the area in square gigameters (sqGm).
    /// </summary>
    public T SquareGigaMeters => SquareYoctoMeters / T.CreateChecked(1e66);

    /// <summary>
    /// Gets the area in square terameters (sqTm).
    /// </summary>
    public T SquareTeraMeters => SquareYoctoMeters / T.CreateChecked(1e72);

    /// <summary>
    /// Gets the area in square petameters (sqPm).
    /// </summary>
    public T SquarePetaMeters => SquareYoctoMeters / T.CreateChecked(1e78);

    /// <summary>
    /// Gets the area in square exameters (sqEm).
    /// </summary>
    public T SquareExaMeters => SquareYoctoMeters / T.CreateChecked(1e84);

    /// <summary>
    /// Gets the area in square zettameters (sqZm).
    /// </summary>
    public T SquareZettaMeters => SquareYoctoMeters / T.CreateChecked(1e90);

    /// <summary>
    /// Gets the area in square yottameters (sqYm).
    /// </summary>
    public T SquareYottaMeters => SquareYoctoMeters / T.CreateChecked(1e96);

    /// <summary>
    /// Gets the area in square inches (sqin).
    /// </summary>
    public T SquareInches => SquareYoctoMeters / T.CreateChecked(6.4516e44);

    /// <summary>
    /// Gets the area in square feet (sqft).
    /// </summary>
    public T SquareFeet => SquareYoctoMeters / T.CreateChecked(9.290304e46);

    /// <summary>
    /// Gets the area in square yards (sqyd).
    /// </summary>
    public T SquareYards => SquareYoctoMeters / T.CreateChecked(8.3612736e47);

    /// <summary>
    /// Gets the area in square miles (sqmi).
    /// </summary>
    public T SquareMiles => SquareYoctoMeters / T.CreateChecked(2.589988110336e54);

    /// <summary>
    /// Gets the area in acres (ac).
    /// </summary>
    public T Acres => SquareYoctoMeters / T.CreateChecked(4.0468564224e51);

    /// <summary>
    /// Gets the area in hectares (ha).
    /// </summary>
    public T Hectares => SquareYoctoMeters / T.CreateChecked(1e52);
}
