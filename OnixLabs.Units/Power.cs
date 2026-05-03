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
/// Represents a unit of power.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Power<T> : IUnit<Power<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Power{T}"/> struct.
    /// </summary>
    /// <param name="value">The power unit in <see cref="QuectoWatts"/>.</param>
    private Power(T value) => QuectoWatts = value;

    /// <summary>
    /// Gets the power in Quectowatts (qW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qW.
    /// </remarks>
    public T QuectoWatts { get; }

    /// <summary>
    /// Gets the power in Rontowatts (rW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rW.
    /// </remarks>
    public T RontoWatts => QuectoWatts.ToRontoUnits();

    /// <summary>
    /// Gets the power in Yoctowatts (yW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yW.
    /// </remarks>
    public T YoctoWatts => QuectoWatts.ToYoctoUnits();

    /// <summary>
    /// Gets the power in Zeptowatts (zW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zW.
    /// </remarks>
    public T ZeptoWatts => QuectoWatts.ToZeptoUnits();

    /// <summary>
    /// Gets the power in Attowatts (aW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aW.
    /// </remarks>
    public T AttoWatts => QuectoWatts.ToAttoUnits();

    /// <summary>
    /// Gets the power in Femtowatts (fW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fW.
    /// </remarks>
    public T FemtoWatts => QuectoWatts.ToFemtoUnits();

    /// <summary>
    /// Gets the power in Picowatts (pW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pW.
    /// </remarks>
    public T PicoWatts => QuectoWatts.ToPicoUnits();

    /// <summary>
    /// Gets the power in Nanowatts (nW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nW.
    /// </remarks>
    public T NanoWatts => QuectoWatts.ToNanoUnits();

    /// <summary>
    /// Gets the power in Microwatts (µW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uW.
    /// </remarks>
    public T MicroWatts => QuectoWatts.ToMicroUnits();

    /// <summary>
    /// Gets the power in Milliwatts (mW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mW.
    /// </remarks>
    public T MilliWatts => QuectoWatts.ToMilliUnits();

    /// <summary>
    /// Gets the power in Centiwatts (cW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cW.
    /// </remarks>
    public T CentiWatts => QuectoWatts.ToCentiUnits();

    /// <summary>
    /// Gets the power in Deciwatts (dW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dW.
    /// </remarks>
    public T DeciWatts => QuectoWatts.ToDeciUnits();

    /// <summary>
    /// Gets the power in Watts (W).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is W.
    /// </remarks>
    public T Watts => QuectoWatts.ToBaseUnits();

    /// <summary>
    /// Gets the power in Decawatts (daW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daW.
    /// </remarks>
    public T DecaWatts => QuectoWatts.ToDecaUnits();

    /// <summary>
    /// Gets the power in Hectowatts (hW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hW.
    /// </remarks>
    public T HectoWatts => QuectoWatts.ToHectoUnits();

    /// <summary>
    /// Gets the power in Kilowatts (kW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kW.
    /// </remarks>
    public T KiloWatts => QuectoWatts.ToKiloUnits();

    /// <summary>
    /// Gets the power in Megawatts (MW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MW.
    /// </remarks>
    public T MegaWatts => QuectoWatts.ToMegaUnits();

    /// <summary>
    /// Gets the power in Gigawatts (GW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GW.
    /// </remarks>
    public T GigaWatts => QuectoWatts.ToGigaUnits();

    /// <summary>
    /// Gets the power in Terawatts (TW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TW.
    /// </remarks>
    public T TeraWatts => QuectoWatts.ToTeraUnits();

    /// <summary>
    /// Gets the power in Petawatts (PW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PW.
    /// </remarks>
    public T PetaWatts => QuectoWatts.ToPetaUnits();

    /// <summary>
    /// Gets the power in Exawatts (EW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EW.
    /// </remarks>
    public T ExaWatts => QuectoWatts.ToExaUnits();

    /// <summary>
    /// Gets the power in Zettawatts (ZW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZW.
    /// </remarks>
    public T ZettaWatts => QuectoWatts.ToZettaUnits();

    /// <summary>
    /// Gets the power in Yottawatts (YW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YW.
    /// </remarks>
    public T YottaWatts => QuectoWatts.ToYottaUnits();

    /// <summary>
    /// Gets the power in Ronnawatts (RW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RW.
    /// </remarks>
    public T RonnaWatts => QuectoWatts.ToRonnaUnits();

    /// <summary>
    /// Gets the power in Quettawatts (QW).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QW.
    /// </remarks>
    public T QuettaWatts => QuectoWatts.ToQuettaUnits();

    /// <summary>
    /// Gets the power in Mechanical Horsepower (hp).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hp.
    /// </remarks>
    public T MechanicalHorsepower => QuectoWatts / T.CreateChecked(7.456998715822702e32);

    /// <summary>
    /// Gets the power in Metric Horsepower (PS).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PS.
    /// </remarks>
    public T MetricHorsepower => QuectoWatts / T.CreateChecked(7.3549875e32);

    /// <summary>
    /// Gets the power in BTUs Per Hour (BTU/h).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is BTUph.
    /// </remarks>
    public T BtusPerHour => QuectoWatts.ToBaseUnits() * T.CreateChecked(3600) / T.CreateChecked(1055.05585262);

    /// <summary>
    /// Gets the power in Calories Per Second (cal/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is calps.
    /// </remarks>
    public T CaloriesPerSecond => QuectoWatts / T.CreateChecked(4.184e30);

    /// <summary>
    /// Gets the power in Ergs Per Second (erg/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ergps.
    /// </remarks>
    public T ErgsPerSecond => QuectoWatts / T.CreateChecked(1e23);

    /// <summary>
    /// Gets the power in Foot Pounds Per Second (ft·lbf/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ftlbfps.
    /// </remarks>
    public T FootPoundsPerSecond => QuectoWatts / T.CreateChecked(1.3558179483314004e30);

    /// <summary>
    /// Gets the power in Tons Of Refrigeration (TR).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is tref.
    /// </remarks>
    public T TonsOfRefrigeration => QuectoWatts / T.CreateChecked(3.5168528420666664e33);
}
