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

namespace OnixLabs.Units;

public readonly partial struct Pressure<T>
{
    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Quectopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromQuectopascals(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Rontopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromRontopascals(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Yoctopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromYoctopascals(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Zeptopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromZeptopascals(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Attopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromAttopascals(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Femtopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromFemtopascals(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Picopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPicopascals(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Nanopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromNanopascals(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Micropascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMicropascals(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Millipascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillipascals(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Centipascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromCentipascals(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Decipascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromDecipascals(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Pascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPascals(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Decapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromDecapascals(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Hectopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromHectopascals(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Kilopascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromKilopascals(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Megapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMegapascals(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Gigapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromGigapascals(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Terapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTerapascals(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Petapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPetapascals(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Exapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromExapascals(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Zettapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromZettapascals(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Yottapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromYottapascals(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Ronnapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromRonnapascals(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Quettapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromQuettapascals(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Bar value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromBar(T value) => new(value * T.CreateChecked(1e35));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Millibar value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillibar(T value) => new(value * T.CreateChecked(1e32));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Atmospheres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromAtmospheres(T value) => new(value * T.CreateChecked(1.01325e35));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Torr value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTorr(T value) => new((value * T.CreateChecked(101325) / T.CreateChecked(760)).FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Millimeters Of Mercury value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillimetersOfMercury(T value) => new(value * T.CreateChecked(1.33322387415e32));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Inches Of Mercury value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromInchesOfMercury(T value) => new(value * T.CreateChecked(3.386389e33));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Pounds Per Square Inch value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPoundsPerSquareInch(T value) => new(value * T.CreateChecked(6.894757293168361e33));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Kilopounds Per Square Inch value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromKilopoundsPerSquareInch(T value) => new(value * T.CreateChecked(6.894757293168361e36));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Technical Atmospheres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTechnicalAtmospheres(T value) => new(value * T.CreateChecked(9.80665e34));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified Baryes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromBaryes(T value) => new(value * T.CreateChecked(1e29));
}
