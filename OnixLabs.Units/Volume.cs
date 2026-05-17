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

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of volume.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Volume<T> : IAdditiveUnit<Volume<T>>, IMultiplicativeUnit<Volume<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Volume{T}"/> struct.
    /// </summary>
    /// <param name="value">The volume unit in <see cref="CubicQuectoMeters"/>.</param>
    private Volume(T value) => CubicQuectoMeters = value;

    /// <summary>
    /// Gets the volume in Cubic Quectometers (qm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuqm.
    /// </remarks>
    public T CubicQuectoMeters { get; }

    /// <summary>
    /// Gets the volume in Cubic Rontometers (rm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is curm.
    /// </remarks>
    public T CubicRontoMeters => CubicQuectoMeters / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the volume in Cubic Yoctometers (ym³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuym.
    /// </remarks>
    public T CubicYoctoMeters => CubicQuectoMeters / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the volume in Cubic Zeptometers (zm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuzm.
    /// </remarks>
    public T CubicZeptoMeters => CubicQuectoMeters / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the volume in Cubic Attometers (am³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuam.
    /// </remarks>
    public T CubicAttoMeters => CubicQuectoMeters / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the volume in Cubic Femtometers (fm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cufm.
    /// </remarks>
    public T CubicFemtoMeters => CubicQuectoMeters / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the volume in Cubic Picometers (pm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cupm.
    /// </remarks>
    public T CubicPicoMeters => CubicQuectoMeters / T.CreateChecked(1e54);

    /// <summary>
    /// Gets the volume in Cubic Nanometers (nm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cunm.
    /// </remarks>
    public T CubicNanoMeters => CubicQuectoMeters / T.CreateChecked(1e63);

    /// <summary>
    /// Gets the volume in Cubic Micrometers (µm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuum.
    /// </remarks>
    public T CubicMicroMeters => CubicQuectoMeters / T.CreateChecked(1e72);

    /// <summary>
    /// Gets the volume in Cubic Millimeters (mm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cumm.
    /// </remarks>
    public T CubicMilliMeters => CubicQuectoMeters / T.CreateChecked(1e81);

    /// <summary>
    /// Gets the volume in Cubic Centimeters (cm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cucm.
    /// </remarks>
    public T CubicCentiMeters => CubicQuectoMeters / T.CreateChecked(1e84);

    /// <summary>
    /// Gets the volume in Cubic Decimeters (dm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cudm.
    /// </remarks>
    public T CubicDeciMeters => CubicQuectoMeters / T.CreateChecked(1e87);

    /// <summary>
    /// Gets the volume in Cubic Meters (m³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cum.
    /// </remarks>
    public T CubicMeters => CubicQuectoMeters / T.CreateChecked(1e90);

    /// <summary>
    /// Gets the volume in Cubic Decameters (dam³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cudam.
    /// </remarks>
    public T CubicDecaMeters => CubicQuectoMeters / T.CreateChecked(1e93);

    /// <summary>
    /// Gets the volume in Cubic Hectometers (hm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuhm.
    /// </remarks>
    public T CubicHectoMeters => CubicQuectoMeters / T.CreateChecked(1e96);

    /// <summary>
    /// Gets the volume in Cubic Kilometers (km³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cukm.
    /// </remarks>
    public T CubicKiloMeters => CubicQuectoMeters / T.CreateChecked(1e99);

    /// <summary>
    /// Gets the volume in Cubic Megameters (Mm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuMm.
    /// </remarks>
    public T CubicMegaMeters => CubicQuectoMeters / T.CreateChecked(1e108);

    /// <summary>
    /// Gets the volume in Cubic Gigameters (Gm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuGm.
    /// </remarks>
    public T CubicGigaMeters => CubicQuectoMeters / T.CreateChecked(1e117);

    /// <summary>
    /// Gets the volume in Cubic Terameters (Tm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuTm.
    /// </remarks>
    public T CubicTeraMeters => CubicQuectoMeters / T.CreateChecked(1e126);

    /// <summary>
    /// Gets the volume in Cubic Petameters (Pm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuPm.
    /// </remarks>
    public T CubicPetaMeters => CubicQuectoMeters / T.CreateChecked(1e135);

    /// <summary>
    /// Gets the volume in Cubic Exameters (Em³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuEm.
    /// </remarks>
    public T CubicExaMeters => CubicQuectoMeters / T.CreateChecked(1e144);

    /// <summary>
    /// Gets the volume in Cubic Zettameters (Zm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuZm.
    /// </remarks>
    public T CubicZettaMeters => CubicQuectoMeters / T.CreateChecked(1e153);

    /// <summary>
    /// Gets the volume in Cubic Yottameters (Ym³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuYm.
    /// </remarks>
    public T CubicYottaMeters => CubicQuectoMeters / T.CreateChecked(1e162);

    /// <summary>
    /// Gets the volume in Cubic Ronnameters (Rm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuRm.
    /// </remarks>
    public T CubicRonnaMeters => CubicQuectoMeters / T.CreateChecked(1e171);

    /// <summary>
    /// Gets the volume in Cubic Quettameters (Qm³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuQm.
    /// </remarks>
    public T CubicQuettaMeters => CubicQuectoMeters / T.CreateChecked(1e180);

    /// <summary>
    /// Gets the volume in Cubic Inches (in³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuin.
    /// </remarks>
    public T CubicInches => CubicQuectoMeters / T.CreateChecked(1.6387064e85);

    /// <summary>
    /// Gets the volume in Cubic Feet (ft³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuft.
    /// </remarks>
    public T CubicFeet => CubicQuectoMeters / T.CreateChecked(2.8316846592e88);

    /// <summary>
    /// Gets the volume in Cubic Yards (yd³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuyd.
    /// </remarks>
    public T CubicYards => CubicQuectoMeters / T.CreateChecked(7.64554857984e89);

    /// <summary>
    /// Gets the volume in Cubic Miles (mi³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cumi.
    /// </remarks>
    public T CubicMiles => CubicQuectoMeters / T.CreateChecked(4.168181825440579584e99);

    /// <summary>
    /// Gets the volume in Cubic Astronomical Units (au³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cuau.
    /// </remarks>
    public T CubicAstronomicalUnits => CubicQuectoMeters / T.CreateChecked(3.347928976e123);

    /// <summary>
    /// Gets the volume in Cubic Light Years (ly³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is culy.
    /// </remarks>
    public T CubicLightYears => CubicQuectoMeters / T.CreateChecked(8.46808e137);

    /// <summary>
    /// Gets the volume in Cubic Parsecs (pc³).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cupc.
    /// </remarks>
    public T CubicParsecs
    {
        get
        {
            T metersPerParsec = T.CreateChecked(149_597_870_700L) * T.CreateChecked(648000) / T.Pi;
            T cuMetersPerCuParsec = metersPerParsec * metersPerParsec * metersPerParsec;
            T cuQmPerCuParsec = cuMetersPerCuParsec * T.CreateChecked(1e90);
            return CubicQuectoMeters / cuQmPerCuParsec;
        }
    }

    /// <summary>
    /// Gets the volume in Liters (L).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is L.
    /// </remarks>
    public T Liters => CubicQuectoMeters / T.CreateChecked(1e87);

    /// <summary>
    /// Gets the volume in Milliliters (mL).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mL.
    /// </remarks>
    public T Milliliters => CubicQuectoMeters / T.CreateChecked(1e84);

    /// <summary>
    /// Gets the volume in US Gallons (US gal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USgal.
    /// </remarks>
    public T USGallons => CubicQuectoMeters / T.CreateChecked(3.785411784e87);

    /// <summary>
    /// Gets the volume in US Quarts (US qt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USqt.
    /// </remarks>
    public T USQuarts => CubicQuectoMeters / T.CreateChecked(9.46352946e86);

    /// <summary>
    /// Gets the volume in US Pints (US pt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USpt.
    /// </remarks>
    public T USPints => CubicQuectoMeters / T.CreateChecked(4.73176473e86);

    /// <summary>
    /// Gets the volume in US Cups (US cup).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UScup.
    /// </remarks>
    public T USCups => CubicQuectoMeters / T.CreateChecked(2.365882365e86);

    /// <summary>
    /// Gets the volume in US Fluid Ounces (US fl oz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is USfloz.
    /// </remarks>
    public T USFluidOunces => CubicQuectoMeters / T.CreateChecked(2.95735295625e85);

    /// <summary>
    /// Gets the volume in US Tablespoons (US tbsp).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UStbsp.
    /// </remarks>
    public T USTablespoons => CubicQuectoMeters / T.CreateChecked(1.478676478125e85);

    /// <summary>
    /// Gets the volume in US Teaspoons (US tsp).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is UStsp.
    /// </remarks>
    public T USTeaspoons => CubicQuectoMeters / T.CreateChecked(4.92892159375e84);

    /// <summary>
    /// Gets the volume in Imperial Gallons (imp gal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impgal.
    /// </remarks>
    public T ImperialGallons => CubicQuectoMeters / T.CreateChecked(4.54609e87);

    /// <summary>
    /// Gets the volume in Imperial Quarts (imp qt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impqt.
    /// </remarks>
    public T ImperialQuarts => CubicQuectoMeters / T.CreateChecked(1.1365225e87);

    /// <summary>
    /// Gets the volume in Imperial Pints (imp pt).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is imppt.
    /// </remarks>
    public T ImperialPints => CubicQuectoMeters / T.CreateChecked(5.6826125e86);

    /// <summary>
    /// Gets the volume in Imperial Fluid Ounces (imp fl oz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is impfloz.
    /// </remarks>
    public T ImperialFluidOunces => CubicQuectoMeters / T.CreateChecked(2.84130625e85);

    /// <summary>
    /// Gets the volume in Oil Barrels (bbl).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is bbl.
    /// </remarks>
    public T OilBarrels => CubicQuectoMeters / T.CreateChecked(1.58987294928e89);
}
