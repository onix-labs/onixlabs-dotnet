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

public readonly partial struct Distance<T>
{
    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Quectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromQuectometers(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Rontometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromRontometers(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromYoctometers(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromZeptometers(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromAttometers(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromFemtometers(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromPicometers(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromNanometers(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromMicrometers(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromMillimeters(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromCentimeters(T value) => new(value * T.CreateChecked(1e28));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromDecimeters(T value) => new(value * T.CreateChecked(1e29));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromMeters(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromDecameters(T value) => new(value * T.CreateChecked(1e31));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromHectometers(T value) => new(value * T.CreateChecked(1e32));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromKilometers(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Quectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromMegameters(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromGigameters(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromTerameters(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromPetameters(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromExameters(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromZettameters(T value) => new(value * T.CreateChecked(1e51));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromYottameters(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Ronnameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromRonnameters(T value) => new(value * T.CreateChecked(1e57));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Quettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromQuettameters(T value) => new(value * T.CreateChecked(1e60));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromInches(T value) => new(value * T.CreateChecked(2.54e28));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromFeet(T value) => new(value * T.CreateChecked(3.048e29));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromYards(T value) => new(value * T.CreateChecked(9.144e29));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromMiles(T value) => new(value * T.CreateChecked(1.609344e33));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Nautical Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromNauticalMiles(T value) => new(value * T.CreateChecked(1.852e33));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Fermis value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromFermis(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Angstroms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromAngstroms(T value) => new(value * T.CreateChecked(1e20));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Astronomical Units value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromAstronomicalUnits(T value) => new(value * T.CreateChecked(1.495978707e41));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Light Years value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromLightYears(T value) => new(value * T.CreateChecked(9.4607304725808e45));

    /// <summary>
    /// Creates a new <see cref="Distance{T}"/> instance from the specified Parsecs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Distance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Distance{T}"/> instance from the specified value.</returns>
    public static Distance<T> FromParsecs(T value)
    {
        T metersPerParsec = T.CreateChecked(1.495978707e11) * T.CreateChecked(648000) / T.Pi;
        T qmPerParsec = metersPerParsec * T.CreateChecked(1e30);
        return new Distance<T>(value * qmPerParsec);
    }
}
