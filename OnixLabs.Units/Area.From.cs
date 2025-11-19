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
    /// Creates a new <see cref="Area{T}"/> instance from the specified square yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYoctoMeters(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareZeptoMeters(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareAttoMeters(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareFemtoMeters(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquarePicoMeters(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareNanoMeters(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMicroMeters(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMilliMeters(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareCentiMeters(T value) => new(value * T.CreateChecked(1e44));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareDeciMeters(T value) => new(value * T.CreateChecked(1e46));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMeters(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareDecaMeters(T value) => new(value * T.CreateChecked(1e50));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareHectoMeters(T value) => new(value * T.CreateChecked(1e52));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareKiloMeters(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square megameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMegaMeters(T value) => new(value * T.CreateChecked(1e60));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareGigaMeters(T value) => new(value * T.CreateChecked(1e66));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareTeraMeters(T value) => new(value * T.CreateChecked(1e72));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquarePetaMeters(T value) => new(value * T.CreateChecked(1e78));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareExaMeters(T value) => new(value * T.CreateChecked(1e84));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareZettaMeters(T value) => new(value * T.CreateChecked(1e90));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYottaMeters(T value) => new(value * T.CreateChecked(1e96));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareInches(T value) => new(value * T.CreateChecked(6.4516e44));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareFeet(T value) => new(value * T.CreateChecked(9.290304e46));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareYards(T value) => new(value * T.CreateChecked(8.3612736e47));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified square miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromSquareMiles(T value) => new(value * T.CreateChecked(2.589988110336e54));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified acres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromAcres(T value) => new(value * T.CreateChecked(4.0468564224e51));

    /// <summary>
    /// Creates a new <see cref="Area{T}"/> instance from the specified hectares value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Area{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Area{T}"/> instance from the specified value.</returns>
    public static Area<T> FromHectares(T value) => new(value * T.CreateChecked(1e52));
}
