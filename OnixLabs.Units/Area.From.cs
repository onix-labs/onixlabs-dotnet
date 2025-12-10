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

public readonly partial struct Area<T>
{
    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Quectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareQuectometers(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Rontometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareRontometers(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYoctometers(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareZeptometers(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareAttometers(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareFemtometers(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquarePicometers(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareNanometers(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMicrometers(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMillimeters(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareCentimeters(T value) => new(value * T.CreateChecked(1e56));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareDecimeters(T value) => new(value * T.CreateChecked(1e58));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMeters(T value) => new(value * T.CreateChecked(1e60));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareDecameters(T value) => new(value * T.CreateChecked(1e62));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareHectometers(T value) => new(value * T.CreateChecked(1e64));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareKilometers(T value) => new(value * T.CreateChecked(1e66));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Megameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMegameters(T value) => new(value * T.CreateChecked(1e72));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareGigameters(T value) => new(value * T.CreateChecked(1e78));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareTerameters(T value) => new(value * T.CreateChecked(1e84));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquarePetameters(T value) => new(value * T.CreateChecked(1e90));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareExameters(T value) => new(value * T.CreateChecked(1e96));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareZettameters(T value) => new(value * T.CreateChecked(1e102));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYottameters(T value) => new(value * T.CreateChecked(1e108));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Ronnameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareRonnameters(T value) => new(value * T.CreateChecked(1e114));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Quettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareQuettameters(T value) => new(value * T.CreateChecked(1e120));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareInches(T value) => new(value * T.CreateChecked(6.4516e56));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareFeet(T value) => new(value * T.CreateChecked(9.290304e58));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYards(T value) => new(value * T.CreateChecked(8.3612736e59));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMiles(T value) => new(value * T.CreateChecked(2.589988110336e66));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Nautical Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareNauticalMiles(T value) => new(value * T.CreateChecked(3.4299040000e66));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Fermis value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareFermis(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Angstroms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareAngstroms(T value) => new(value * T.CreateChecked(1e40));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Astronomical Units value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareAstronomicalUnits(T value) => new(value * T.CreateChecked(2.2379522821e82));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Light Years value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareLightYears(T value) => new(value * T.CreateChecked(8.9505421074819e91));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified Square Parsecs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareParsecs(T value)
    {
        T metersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
        T sqMetersPerSqParsec = metersPerParsec * metersPerParsec;
        T sqQmPerSqParsec = sqMetersPerSqParsec * T.CreateChecked(1e60);
        return new Area<T>(value * sqQmPerSqParsec);
    }
}
