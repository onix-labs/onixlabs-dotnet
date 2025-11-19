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
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified quectopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromQuectoPascals(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified rontopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromRontoPascals(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified yoctopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromYoctoPascals(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified zeptopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromZeptoPascals(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified attopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromAttoPascals(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified femtopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromFemtoPascals(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified picopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPicoPascals(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified nanopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromNanoPascals(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified micropascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMicroPascals(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified millipascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMilliPascals(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified centipascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromCentiPascals(T value) => new(value * T.CreateChecked(1e28));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified decipascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromDeciPascals(T value) => new(value * T.CreateChecked(1e29));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified pascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPascals(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified decapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromDecaPascals(T value) => new(value * T.CreateChecked(1e31));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified hectopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromHectoPascals(T value) => new(value * T.CreateChecked(1e32));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified kilopascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromKiloPascals(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified megapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMegaPascals(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified gigapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromGigaPascals(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified terapascals value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTeraPascals(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified petapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPetaPascals(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified exapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromExaPascals(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified zettapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromZettaPascals(T value) => new(value * T.CreateChecked(1e51));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified yottapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromYottaPascals(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified ronnapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromRonnaPascals(T value) => new(value * T.CreateChecked(1e57));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified quettapascal value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromQuettaPascals(T value) => new(value * T.CreateChecked(1e60));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified bar value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromBars(T value) => new(value * T.CreateChecked(1e35));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified millibar value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillibars(T value) => new(value * T.CreateChecked(1e32));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified atmosphere value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromAtmospheres(T value) => new(value * T.CreateChecked(101325e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified technical atmosphere value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTechnicalAtmospheres(T value) => new(value * T.CreateChecked(98066.5e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified torr value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromTorr(T value) => new(value * T.CreateChecked(133.32236842105263e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified millimetres of mercury value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillimetersOfMercury(T value) => new(value * T.CreateChecked(133.322387415e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified inches of mercury value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromInchesOfMercury(T value) => new(value * T.CreateChecked(3386.389e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified pounds per square inch value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPoundsPerSquareInch(T value) => new(value * T.CreateChecked(6894.757293168e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified pounds per square foot value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromPoundsPerSquareFoot(T value) => new(value * T.CreateChecked(47.88025898e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified barye value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromBarye(T value) => new(value * T.CreateChecked(0.1e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified millimetres of water column value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromMillimetersOfWaterColumn(T value) => new(value * T.CreateChecked(9.80665e30));

    /// <summary>
    /// Creates a new <see cref="Pressure{T}"/> instance from the specified inches of water column value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Pressure{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Pressure{T}"/> instance from the specified value.</returns>
    public static Pressure<T> FromInchesOfWaterColumn(T value) => new(value * T.CreateChecked(249.08891e30));
}
