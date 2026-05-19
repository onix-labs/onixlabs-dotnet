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
using OnixLabs.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of volume.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Volume<T> :
    ICanonicalUnit<T>,
    IAdditiveUnit<Volume<T>>,
    IMultiplicativeUnit<Volume<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Volume{T}"/> struct.
    /// </summary>
    /// <param name="value">The volume unit in <see cref="CubicQuectoMeters"/>.</param>
    private Volume(T value) => Canonical = value;

    public T Canonical { get; }

    /// <summary>
    /// Gets the volume in Cubic Quectometers (qm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuqm.
    /// </remarks>
    public T CubicQuectoMeters => Canonical;

    /// <summary>
    /// Gets the volume in Cubic Rontometers (rm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is curm.
    /// </remarks>
    public T CubicRontoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(9);

    /// <summary>
    /// Gets the volume in Cubic Yoctometers (ym³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuym.
    /// </remarks>
    public T CubicYoctoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(18);

    /// <summary>
    /// Gets the volume in Cubic Zeptometers (zm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuzm.
    /// </remarks>
    public T CubicZeptoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(27);

    /// <summary>
    /// Gets the volume in Cubic Attometers (am³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuam.
    /// </remarks>
    public T CubicAttoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(36);

    /// <summary>
    /// Gets the volume in Cubic Femtometers (fm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cufm.
    /// </remarks>
    public T CubicFemtoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(45);

    /// <summary>
    /// Gets the volume in Cubic Picometers (pm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cupm.
    /// </remarks>
    public T CubicPicoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(54);

    /// <summary>
    /// Gets the volume in Cubic Nanometers (nm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cunm.
    /// </remarks>
    public T CubicNanoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(63);

    /// <summary>
    /// Gets the volume in Cubic Micrometers (µm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuum.
    /// </remarks>
    public T CubicMicroMeters => CubicQuectoMeters / GenericMath.Pow10<T>(72);

    /// <summary>
    /// Gets the volume in Cubic Millimeters (mm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cumm.
    /// </remarks>
    public T CubicMilliMeters => CubicQuectoMeters / GenericMath.Pow10<T>(81);

    /// <summary>
    /// Gets the volume in Cubic Centimeters (cm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cucm.
    /// </remarks>
    public T CubicCentiMeters => CubicQuectoMeters / GenericMath.Pow10<T>(84);

    /// <summary>
    /// Gets the volume in Cubic Decimeters (dm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cudm.
    /// </remarks>
    public T CubicDeciMeters => CubicQuectoMeters / GenericMath.Pow10<T>(87);

    /// <summary>
    /// Gets the volume in Cubic Meters (m³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cum.
    /// </remarks>
    public T CubicMeters => CubicQuectoMeters / GenericMath.Pow10<T>(90);

    /// <summary>
    /// Gets the volume in Cubic Decameters (dam³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cudam.
    /// </remarks>
    public T CubicDecaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(93);

    /// <summary>
    /// Gets the volume in Cubic Hectometers (hm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuhm.
    /// </remarks>
    public T CubicHectoMeters => CubicQuectoMeters / GenericMath.Pow10<T>(96);

    /// <summary>
    /// Gets the volume in Cubic Kilometers (km³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cukm.
    /// </remarks>
    public T CubicKiloMeters => CubicQuectoMeters / GenericMath.Pow10<T>(99);

    /// <summary>
    /// Gets the volume in Cubic Megameters (Mm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuMm.
    /// </remarks>
    public T CubicMegaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(108);

    /// <summary>
    /// Gets the volume in Cubic Gigameters (Gm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuGm.
    /// </remarks>
    public T CubicGigaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(117);

    /// <summary>
    /// Gets the volume in Cubic Terameters (Tm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuTm.
    /// </remarks>
    public T CubicTeraMeters => CubicQuectoMeters / GenericMath.Pow10<T>(126);

    /// <summary>
    /// Gets the volume in Cubic Petameters (Pm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuPm.
    /// </remarks>
    public T CubicPetaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(135);

    /// <summary>
    /// Gets the volume in Cubic Exameters (Em³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuEm.
    /// </remarks>
    public T CubicExaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(144);

    /// <summary>
    /// Gets the volume in Cubic Zettameters (Zm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuZm.
    /// </remarks>
    public T CubicZettaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(153);

    /// <summary>
    /// Gets the volume in Cubic Yottameters (Ym³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuYm.
    /// </remarks>
    public T CubicYottaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(162);

    /// <summary>
    /// Gets the volume in Cubic Ronnameters (Rm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuRm.
    /// </remarks>
    public T CubicRonnaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(171);

    /// <summary>
    /// Gets the volume in Cubic Quettameters (Qm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuQm.
    /// </remarks>
    public T CubicQuettaMeters => CubicQuectoMeters / GenericMath.Pow10<T>(180);

    /// <summary>
    /// Gets the volume in Cubic Inches (in³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuin.
    /// </remarks>
    public T CubicInches => CubicQuectoMeters / CuQuectometersPerCubicInch;

    /// <summary>
    /// Gets the volume in Cubic Feet (ft³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuft.
    /// </remarks>
    public T CubicFeet => CubicQuectoMeters / CuQuectometersPerCubicFoot;

    /// <summary>
    /// Gets the volume in Cubic Yards (yd³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuyd.
    /// </remarks>
    public T CubicYards => CubicQuectoMeters / CuQuectometersPerCubicYard;

    /// <summary>
    /// Gets the volume in Cubic Miles (mi³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cumi.
    /// </remarks>
    public T CubicMiles => CubicQuectoMeters / CuQuectometersPerCubicMile;

    /// <summary>
    /// Gets the volume in Cubic Astronomical Units (au³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuau.
    /// </remarks>
    public T CubicAstronomicalUnits => CubicQuectoMeters / CuQuectometersPerCubicAstronomicalUnit;

    /// <summary>
    /// Gets the volume in Cubic Light Years (ly³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is culy.
    /// </remarks>
    public T CubicLightYears => CubicQuectoMeters / CuQuectometersPerCubicLightYear;

    /// <summary>
    /// Gets the volume in Cubic Parsecs (pc³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cupc.
    /// </remarks>
    public T CubicParsecs => CubicQuectoMeters / CuQuectometersPerCubicParsec;

    /// <summary>
    /// Gets the volume in Liters (L).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is L.
    /// </remarks>
    public T Liters => CubicQuectoMeters / GenericMath.Pow10<T>(87);

    /// <summary>
    /// Gets the volume in Milliliters (mL).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mL.
    /// </remarks>
    public T Milliliters => CubicQuectoMeters / GenericMath.Pow10<T>(84);

    /// <summary>
    /// Gets the volume in US Gallons (US gal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USgal.
    /// </remarks>
    public T USGallons => CubicQuectoMeters / CuQuectometersPerUSGallon;

    /// <summary>
    /// Gets the volume in US Quarts (US qt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USqt.
    /// </remarks>
    public T USQuarts => CubicQuectoMeters / CuQuectometersPerUSQuart;

    /// <summary>
    /// Gets the volume in US Pints (US pt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USpt.
    /// </remarks>
    public T USPints => CubicQuectoMeters / CuQuectometersPerUSPint;

    /// <summary>
    /// Gets the volume in US Cups (US cup).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UScup.
    /// </remarks>
    public T USCups => CubicQuectoMeters / CuQuectometersPerUSCup;

    /// <summary>
    /// Gets the volume in US Fluid Ounces (US fl oz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USfloz.
    /// </remarks>
    public T USFluidOunces => CubicQuectoMeters / CuQuectometersPerUSFluidOunce;

    /// <summary>
    /// Gets the volume in US Tablespoons (US tbsp).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UStbsp.
    /// </remarks>
    public T USTablespoons => CubicQuectoMeters / CuQuectometersPerUSTablespoon;

    /// <summary>
    /// Gets the volume in US Teaspoons (US tsp).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UStsp.
    /// </remarks>
    public T USTeaspoons => CubicQuectoMeters / CuQuectometersPerUSTeaspoon;

    /// <summary>
    /// Gets the volume in Imperial Gallons (imp gal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impgal.
    /// </remarks>
    public T ImperialGallons => CubicQuectoMeters / CuQuectometersPerImperialGallon;

    /// <summary>
    /// Gets the volume in Imperial Quarts (imp qt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impqt.
    /// </remarks>
    public T ImperialQuarts => CubicQuectoMeters / CuQuectometersPerImperialQuart;

    /// <summary>
    /// Gets the volume in Imperial Pints (imp pt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is imppt.
    /// </remarks>
    public T ImperialPints => CubicQuectoMeters / CuQuectometersPerImperialPint;

    /// <summary>
    /// Gets the volume in Imperial Fluid Ounces (imp fl oz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impfloz.
    /// </remarks>
    public T ImperialFluidOunces => CubicQuectoMeters / CuQuectometersPerImperialFluidOunce;

    /// <summary>
    /// Gets the volume in Oil Barrels (bbl).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is bbl.
    /// </remarks>
    public T OilBarrels => CubicQuectoMeters / CuQuectometersPerOilBarrel;
}
