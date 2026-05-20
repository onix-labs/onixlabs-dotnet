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
/// Represents a unit of an amount of substance.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct AmountOfSubstance<T> : IAdditiveUnit<AmountOfSubstance<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AmountOfSubstance{T}"/> struct.
    /// </summary>
    /// <param name="value">The amount of substance unit in <see cref="QuectoMoles"/>.</param>
    private AmountOfSubstance(T value) => QuectoMoles = value;

    /// <summary>
    /// Gets the amount of substance in Quectomoles (qmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qmol.
    /// </remarks>
    public T QuectoMoles { get; }

    /// <summary>
    /// Gets the amount of substance in Rontomoles (rmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rmol.
    /// </remarks>
    public T RontoMoles => QuectoMoles.ToRontoUnits();

    /// <summary>
    /// Gets the amount of substance in Yoctomoles (ymol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ymol.
    /// </remarks>
    public T YoctoMoles => QuectoMoles.ToYoctoUnits();

    /// <summary>
    /// Gets the amount of substance in Zeptomoles (zmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zmol.
    /// </remarks>
    public T ZeptoMoles => QuectoMoles.ToZeptoUnits();

    /// <summary>
    /// Gets the amount of substance in Attomoles (amol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is amol.
    /// </remarks>
    public T AttoMoles => QuectoMoles.ToAttoUnits();

    /// <summary>
    /// Gets the amount of substance in Femtomoles (fmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fmol.
    /// </remarks>
    public T FemtoMoles => QuectoMoles.ToFemtoUnits();

    /// <summary>
    /// Gets the amount of substance in Picomoles (pmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pmol.
    /// </remarks>
    public T PicoMoles => QuectoMoles.ToPicoUnits();

    /// <summary>
    /// Gets the amount of substance in Nanomoles (nmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nmol.
    /// </remarks>
    public T NanoMoles => QuectoMoles.ToNanoUnits();

    /// <summary>
    /// Gets the amount of substance in Micromoles (µmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is umol.
    /// </remarks>
    public T MicroMoles => QuectoMoles.ToMicroUnits();

    /// <summary>
    /// Gets the amount of substance in Millimoles (mmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mmol.
    /// </remarks>
    public T MilliMoles => QuectoMoles.ToMilliUnits();

    /// <summary>
    /// Gets the amount of substance in Centimoles (cmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cmol.
    /// </remarks>
    public T CentiMoles => QuectoMoles.ToCentiUnits();

    /// <summary>
    /// Gets the amount of substance in Decimoles (dmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dmol.
    /// </remarks>
    public T DeciMoles => QuectoMoles.ToDeciUnits();

    /// <summary>
    /// Gets the amount of substance in Moles (mol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mol.
    /// </remarks>
    public T Moles => QuectoMoles.ToBaseUnits();

    /// <summary>
    /// Gets the amount of substance in Decamoles (damol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is damol.
    /// </remarks>
    public T DecaMoles => QuectoMoles.ToDecaUnits();

    /// <summary>
    /// Gets the amount of substance in Hectomoles (hmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hmol.
    /// </remarks>
    public T HectoMoles => QuectoMoles.ToHectoUnits();

    /// <summary>
    /// Gets the amount of substance in Kilomoles (kmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kmol.
    /// </remarks>
    public T KiloMoles => QuectoMoles.ToKiloUnits();

    /// <summary>
    /// Gets the amount of substance in Megamoles (Mmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mmol.
    /// </remarks>
    public T MegaMoles => QuectoMoles.ToMegaUnits();

    /// <summary>
    /// Gets the amount of substance in Gigamoles (Gmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gmol.
    /// </remarks>
    public T GigaMoles => QuectoMoles.ToGigaUnits();

    /// <summary>
    /// Gets the amount of substance in Teramoles (Tmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tmol.
    /// </remarks>
    public T TeraMoles => QuectoMoles.ToTeraUnits();

    /// <summary>
    /// Gets the amount of substance in Petamoles (Pmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pmol.
    /// </remarks>
    public T PetaMoles => QuectoMoles.ToPetaUnits();

    /// <summary>
    /// Gets the amount of substance in Examoles (Emol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Emol.
    /// </remarks>
    public T ExaMoles => QuectoMoles.ToExaUnits();

    /// <summary>
    /// Gets the amount of substance in Zettamoles (Zmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zmol.
    /// </remarks>
    public T ZettaMoles => QuectoMoles.ToZettaUnits();

    /// <summary>
    /// Gets the amount of substance in Yottamoles (Ymol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ymol.
    /// </remarks>
    public T YottaMoles => QuectoMoles.ToYottaUnits();

    /// <summary>
    /// Gets the amount of substance in Ronnamoles (Rmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rmol.
    /// </remarks>
    public T RonnaMoles => QuectoMoles.ToRonnaUnits();

    /// <summary>
    /// Gets the amount of substance in Quettamoles (Qmol).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qmol.
    /// </remarks>
    public T QuettaMoles => QuectoMoles.ToQuettaUnits();
}
