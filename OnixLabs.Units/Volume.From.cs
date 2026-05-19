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

using OnixLabs.Numerics;

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
    public static Volume<T> FromCubicRontometers(T value) => new(value * GenericMath.Pow10<T>(9));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yoctometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYoctometers(T value) => new(value * GenericMath.Pow10<T>(18));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Zeptometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZeptometers(T value) => new(value * GenericMath.Pow10<T>(27));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Attometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicAttometers(T value) => new(value * GenericMath.Pow10<T>(36));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Femtometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFemtometers(T value) => new(value * GenericMath.Pow10<T>(45));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Picometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPicometers(T value) => new(value * GenericMath.Pow10<T>(54));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Nanometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicNanometers(T value) => new(value * GenericMath.Pow10<T>(63));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Micrometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMicrometers(T value) => new(value * GenericMath.Pow10<T>(72));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Millimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMillimeters(T value) => new(value * GenericMath.Pow10<T>(81));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Centimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicCentimeters(T value) => new(value * GenericMath.Pow10<T>(84));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Decimeters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDecimeters(T value) => new(value * GenericMath.Pow10<T>(87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Meters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMeters(T value) => new(value * GenericMath.Pow10<T>(90));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Decameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicDecameters(T value) => new(value * GenericMath.Pow10<T>(93));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Hectometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicHectometers(T value) => new(value * GenericMath.Pow10<T>(96));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Kilometers value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicKilometers(T value) => new(value * GenericMath.Pow10<T>(99));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Megameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMegameters(T value) => new(value * GenericMath.Pow10<T>(108));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Gigameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicGigameters(T value) => new(value * GenericMath.Pow10<T>(117));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Terameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicTerameters(T value) => new(value * GenericMath.Pow10<T>(126));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Petameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicPetameters(T value) => new(value * GenericMath.Pow10<T>(135));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Exameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicExameters(T value) => new(value * GenericMath.Pow10<T>(144));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Zettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicZettameters(T value) => new(value * GenericMath.Pow10<T>(153));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yottameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYottameters(T value) => new(value * GenericMath.Pow10<T>(162));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Ronnameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicRonnameters(T value) => new(value * GenericMath.Pow10<T>(171));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Quettameters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicQuettameters(T value) => new(value * GenericMath.Pow10<T>(180));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Inches value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicInches(T value) => new(value * CuQuectometersPerCubicInch);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Feet value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicFeet(T value) => new(value * CuQuectometersPerCubicFoot);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Yards value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicYards(T value) => new(value * CuQuectometersPerCubicYard);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Miles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicMiles(T value) => new(value * CuQuectometersPerCubicMile);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Astronomical Units value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicAstronomicalUnits(T value) => new(value * CuQuectometersPerCubicAstronomicalUnit);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Light Years value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicLightYears(T value) => new(value * CuQuectometersPerCubicLightYear);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Cubic Parsecs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromCubicParsecs(T value) => new(value * CuQuectometersPerCubicParsec);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Liters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromLiters(T value) => new(value * GenericMath.Pow10<T>(87));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Milliliters value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromMilliliters(T value) => new(value * GenericMath.Pow10<T>(84));

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Gallons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSGallons(T value) => new(value * CuQuectometersPerUSGallon);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Quarts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSQuarts(T value) => new(value * CuQuectometersPerUSQuart);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Pints value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSPints(T value) => new(value * CuQuectometersPerUSPint);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Cups value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSCups(T value) => new(value * CuQuectometersPerUSCup);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Fluid Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSFluidOunces(T value) => new(value * CuQuectometersPerUSFluidOunce);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Tablespoons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSTablespoons(T value) => new(value * CuQuectometersPerUSTablespoon);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified US Teaspoons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromUSTeaspoons(T value) => new(value * CuQuectometersPerUSTeaspoon);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Gallons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialGallons(T value) => new(value * CuQuectometersPerImperialGallon);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Quarts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialQuarts(T value) => new(value * CuQuectometersPerImperialQuart);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Pints value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialPints(T value) => new(value * CuQuectometersPerImperialPint);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Imperial Fluid Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromImperialFluidOunces(T value) => new(value * CuQuectometersPerImperialFluidOunce);

    /// <summary>
    /// Creates a new <see cref="Volume{T}"/> instance from the specified Oil Barrels value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Volume{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Volume{T}"/> instance from the specified value.</returns>
    public static Volume<T> FromOilBarrels(T value) => new(value * CuQuectometersPerOilBarrel);
}
