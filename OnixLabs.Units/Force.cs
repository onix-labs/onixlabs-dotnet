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
/// Represents a unit of force.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Force<T> : IUnit<Force<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Force{T}"/> struct.
    /// </summary>
    /// <param name="value">The force unit in <see cref="QuectoNewtons"/>.</param>
    private Force(T value) => QuectoNewtons = value;

    /// <summary>
    /// Gets the force in Quectonewtons (qN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qN.
    /// </remarks>
    public T QuectoNewtons { get; }

    /// <summary>
    /// Gets the force in Rontonewtons (rN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rN.
    /// </remarks>
    public T RontoNewtons => QuectoNewtons.ToRontoUnits();

    /// <summary>
    /// Gets the force in Yoctonewtons (yN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yN.
    /// </remarks>
    public T YoctoNewtons => QuectoNewtons.ToYoctoUnits();

    /// <summary>
    /// Gets the force in Zeptonewtons (zN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zN.
    /// </remarks>
    public T ZeptoNewtons => QuectoNewtons.ToZeptoUnits();

    /// <summary>
    /// Gets the force in Attonewtons (aN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aN.
    /// </remarks>
    public T AttoNewtons => QuectoNewtons.ToAttoUnits();

    /// <summary>
    /// Gets the force in Femtonewtons (fN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fN.
    /// </remarks>
    public T FemtoNewtons => QuectoNewtons.ToFemtoUnits();

    /// <summary>
    /// Gets the force in Piconewtons (pN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pN.
    /// </remarks>
    public T PicoNewtons => QuectoNewtons.ToPicoUnits();

    /// <summary>
    /// Gets the force in Nanonewtons (nN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nN.
    /// </remarks>
    public T NanoNewtons => QuectoNewtons.ToNanoUnits();

    /// <summary>
    /// Gets the force in Micronewtons (µN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uN.
    /// </remarks>
    public T MicroNewtons => QuectoNewtons.ToMicroUnits();

    /// <summary>
    /// Gets the force in Millinewtons (mN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mN.
    /// </remarks>
    public T MilliNewtons => QuectoNewtons.ToMilliUnits();

    /// <summary>
    /// Gets the force in Centinewtons (cN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cN.
    /// </remarks>
    public T CentiNewtons => QuectoNewtons.ToCentiUnits();

    /// <summary>
    /// Gets the force in Decinewtons (dN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dN.
    /// </remarks>
    public T DeciNewtons => QuectoNewtons.ToDeciUnits();

    /// <summary>
    /// Gets the force in Newtons (N).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is N.
    /// </remarks>
    public T Newtons => QuectoNewtons.ToBaseUnits();

    /// <summary>
    /// Gets the force in Decanewtons (daN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daN.
    /// </remarks>
    public T DecaNewtons => QuectoNewtons.ToDecaUnits();

    /// <summary>
    /// Gets the force in Hectonewtons (hN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hN.
    /// </remarks>
    public T HectoNewtons => QuectoNewtons.ToHectoUnits();

    /// <summary>
    /// Gets the force in Kilonewtons (kN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kN.
    /// </remarks>
    public T KiloNewtons => QuectoNewtons.ToKiloUnits();

    /// <summary>
    /// Gets the force in Meganewtons (MN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MN.
    /// </remarks>
    public T MegaNewtons => QuectoNewtons.ToMegaUnits();

    /// <summary>
    /// Gets the force in Giganewtons (GN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GN.
    /// </remarks>
    public T GigaNewtons => QuectoNewtons.ToGigaUnits();

    /// <summary>
    /// Gets the force in Teranewtons (TN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TN.
    /// </remarks>
    public T TeraNewtons => QuectoNewtons.ToTeraUnits();

    /// <summary>
    /// Gets the force in Petanewtons (PN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PN.
    /// </remarks>
    public T PetaNewtons => QuectoNewtons.ToPetaUnits();

    /// <summary>
    /// Gets the force in Exanewtons (EN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EN.
    /// </remarks>
    public T ExaNewtons => QuectoNewtons.ToExaUnits();

    /// <summary>
    /// Gets the force in Zettanewtons (ZN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZN.
    /// </remarks>
    public T ZettaNewtons => QuectoNewtons.ToZettaUnits();

    /// <summary>
    /// Gets the force in Yottanewtons (YN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YN.
    /// </remarks>
    public T YottaNewtons => QuectoNewtons.ToYottaUnits();

    /// <summary>
    /// Gets the force in Ronnanewtons (RN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RN.
    /// </remarks>
    public T RonnaNewtons => QuectoNewtons.ToRonnaUnits();

    /// <summary>
    /// Gets the force in Quettanewtons (QN).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QN.
    /// </remarks>
    public T QuettaNewtons => QuectoNewtons.ToQuettaUnits();

    /// <summary>
    /// Gets the force in Dynes (dyn).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dyn.
    /// </remarks>
    public T Dynes => QuectoNewtons / T.CreateChecked(1e25);

    /// <summary>
    /// Gets the force in Pounds-force (lbf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is lbf.
    /// </remarks>
    public T PoundsForce => QuectoNewtons / T.CreateChecked(4.4482216152605e30);

    /// <summary>
    /// Gets the force in Ounces-force (ozf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ozf.
    /// </remarks>
    public T OuncesForce => QuectoNewtons / T.CreateChecked(2.7801385095378125e29);

    /// <summary>
    /// Gets the force in Poundals (pdl).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pdl.
    /// </remarks>
    public T Poundals => QuectoNewtons / T.CreateChecked(1.38254954376e29);

    /// <summary>
    /// Gets the force in Kilograms-force (kgf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kgf.
    /// </remarks>
    public T KilogramsForce => QuectoNewtons / T.CreateChecked(9.80665e30);

    /// <summary>
    /// Gets the force in Grams-force (gf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is gf.
    /// </remarks>
    public T GramsForce => QuectoNewtons / T.CreateChecked(9.80665e27);

    /// <summary>
    /// Gets the force in Metric Tons-force (tnf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is tnf.
    /// </remarks>
    public T MetricTonsForce => QuectoNewtons / T.CreateChecked(9.80665e33);
}
