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
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Quectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicQuectometers(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Rontometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicRontometers(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYoctometers(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZeptometers(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicAttometers(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFemtometers(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPicometers(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicNanometers(T value) => new(value * T.CreateChecked(1e63));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMicrometers(T value) => new(value * T.CreateChecked(1e72));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMillimeters(T value) => new(value * T.CreateChecked(1e81));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicCentimeters(T value) => new(value * T.CreateChecked(1e84));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDecimeters(T value) => new(value * T.CreateChecked(1e87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMeters(T value) => new(value * T.CreateChecked(1e90));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDecameters(T value) => new(value * T.CreateChecked(1e93));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicHectometers(T value) => new(value * T.CreateChecked(1e96));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicKilometers(T value) => new(value * T.CreateChecked(1e99));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Megameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMegameters(T value) => new(value * T.CreateChecked(1e108));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicGigameters(T value) => new(value * T.CreateChecked(1e117));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicTerameters(T value) => new(value * T.CreateChecked(1e126));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPetameters(T value) => new(value * T.CreateChecked(1e135));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicExameters(T value) => new(value * T.CreateChecked(1e144));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZettameters(T value) => new(value * T.CreateChecked(1e153));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYottameters(T value) => new(value * T.CreateChecked(1e162));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Ronnameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicRonnameters(T value) => new(value * T.CreateChecked(1e171));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Quettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicQuettameters(T value) => new(value * T.CreateChecked(1e180));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicInches(T value) => new(value * T.CreateChecked(1.6387064e85));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFeet(T value) => new(value * T.CreateChecked(2.8316846592e88));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYards(T value) => new(value * T.CreateChecked(7.64554857984e89));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMiles(T value) => new(value * T.CreateChecked(4.168181825440579584e99));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Astronomical Units value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicAstronomicalUnits(T value) => new(value * T.CreateChecked(3.347928976e123));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Light Years value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicLightYears(T value) => new(value * T.CreateChecked(8.46808e137));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Parsecs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicParsecs(T value)
    {
        T metersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
        T cuMetersPerCuParsec = metersPerParsec * metersPerParsec * metersPerParsec;
        T cuQmPerCuParsec = cuMetersPerCuParsec * T.CreateChecked(1e90);
        return new Volume<T>(value * cuQmPerCuParsec);
    }

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Liters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromLiters(T value) => new(value * T.CreateChecked(1e87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Milliliters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromMilliliters(T value) => new(value * T.CreateChecked(1e84));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Gallons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSGallons(T value) => new(value * T.CreateChecked(3.785411784e87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Quarts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSQuarts(T value) => new(value * T.CreateChecked(9.46352946e86));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Pints value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSPints(T value) => new(value * T.CreateChecked(4.73176473e86));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Cups value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSCups(T value) => new(value * T.CreateChecked(2.365882365e86));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Fluid Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSFluidOunces(T value) => new(value * T.CreateChecked(2.95735295625e85));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Tablespoons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSTablespoons(T value) => new(value * T.CreateChecked(1.478676478125e85));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Teaspoons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSTeaspoons(T value) => new(value * T.CreateChecked(4.92892159375e84));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Gallons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialGallons(T value) => new(value * T.CreateChecked(4.54609e87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Quarts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialQuarts(T value) => new(value * T.CreateChecked(1.1365225e87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Pints value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialPints(T value) => new(value * T.CreateChecked(5.6826125e86));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Fluid Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialFluidOunces(T value) => new(value * T.CreateChecked(2.84130625e85));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Oil Barrels value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromOilBarrels(T value) => new(value * T.CreateChecked(1.58987294928e89));
}
