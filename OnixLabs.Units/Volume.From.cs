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

public readonly partial struct Volume<T>
{
    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYoctoMeters(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZeptoMeters(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicAttoMeters(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFemtoMeters(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPicoMeters(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicNanoMeters(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMicroMeters(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMilliMeters(T value) => new(value * T.CreateChecked(1e63));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicCentiMeters(T value) => new(value * T.CreateChecked(1e66));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDeciMeters(T value) => new(value * T.CreateChecked(1e69));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMeters(T value) => new(value * T.CreateChecked(1e72));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDecaMeters(T value) => new(value * T.CreateChecked(1e75));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicHectoMeters(T value) => new(value * T.CreateChecked(1e78));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicKiloMeters(T value) => new(value * T.CreateChecked(1e81));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic megameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMegaMeters(T value) => new(value * T.CreateChecked(1e90));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicGigaMeters(T value) => new(value * T.CreateChecked(1e99));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicTeraMeters(T value) => new(value * T.CreateChecked(1e108));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPetaMeters(T value) => new(value * T.CreateChecked(1e117));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicExaMeters(T value) => new(value * T.CreateChecked(1e126));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZettaMeters(T value) => new(value * T.CreateChecked(1e135));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYottaMeters(T value) => new(value * T.CreateChecked(1e144));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified litres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromLiters(T value) => new(value * T.CreateChecked(1e69));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified millilitres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromMilliLiters(T value) => new(value * T.CreateChecked(1e66));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified centilitres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCentiLiters(T value) => new(value * T.CreateChecked(1e67));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified decilitres value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromDeciLiters(T value) => new(value * T.CreateChecked(1e68));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicInches(T value) => new(value * T.CreateChecked(1.6387064e67));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFeet(T value) => new(value * T.CreateChecked(2.8316846592e70));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified cubic yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYards(T value) => new(value * T.CreateChecked(7.64554857984e71));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US fluid ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromFluidOunces(T value) => new(value * T.CreateChecked(2.95735295625e67));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US cups value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCups(T value) => new(value * T.CreateChecked(2.365882365e68));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US pints value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromPints(T value) => new(value * T.CreateChecked(4.73176473e68));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US quarts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromQuarts(T value) => new(value * T.CreateChecked(9.46352946e68));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US gallons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromGallons(T value) => new(value * T.CreateChecked(3.785411784e69));
}
