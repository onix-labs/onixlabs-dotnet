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
/// Represents a unit of electrical capacitance.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Capacitance<T> : IUnit<Capacitance<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Capacitance{T}"/> struct.
    /// </summary>
    /// <param name="value">The capacitance unit in <see cref="QuectoFarads"/>.</param>
    private Capacitance(T value) => QuectoFarads = value;

    /// <summary>
    /// Gets the capacitance in Quectofarads (qF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qF.
    /// </remarks>
    public T QuectoFarads { get; }

    /// <summary>
    /// Gets the capacitance in Rontofarads (rF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rF.
    /// </remarks>
    public T RontoFarads => QuectoFarads.ToRontoUnits();

    /// <summary>
    /// Gets the capacitance in Yoctofarads (yF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yF.
    /// </remarks>
    public T YoctoFarads => QuectoFarads.ToYoctoUnits();

    /// <summary>
    /// Gets the capacitance in Zeptofarads (zF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zF.
    /// </remarks>
    public T ZeptoFarads => QuectoFarads.ToZeptoUnits();

    /// <summary>
    /// Gets the capacitance in Attofarads (aF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aF.
    /// </remarks>
    public T AttoFarads => QuectoFarads.ToAttoUnits();

    /// <summary>
    /// Gets the capacitance in Femtofarads (fF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fF.
    /// </remarks>
    public T FemtoFarads => QuectoFarads.ToFemtoUnits();

    /// <summary>
    /// Gets the capacitance in Picofarads (pF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pF.
    /// </remarks>
    public T PicoFarads => QuectoFarads.ToPicoUnits();

    /// <summary>
    /// Gets the capacitance in Nanofarads (nF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nF.
    /// </remarks>
    public T NanoFarads => QuectoFarads.ToNanoUnits();

    /// <summary>
    /// Gets the capacitance in Microfarads (µF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uF.
    /// </remarks>
    public T MicroFarads => QuectoFarads.ToMicroUnits();

    /// <summary>
    /// Gets the capacitance in Millifarads (mF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mF.
    /// </remarks>
    public T MilliFarads => QuectoFarads.ToMilliUnits();

    /// <summary>
    /// Gets the capacitance in Centifarads (cF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cF.
    /// </remarks>
    public T CentiFarads => QuectoFarads.ToCentiUnits();

    /// <summary>
    /// Gets the capacitance in Decifarads (dF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dF.
    /// </remarks>
    public T DeciFarads => QuectoFarads.ToDeciUnits();

    /// <summary>
    /// Gets the capacitance in Farads (F).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is F.
    /// </remarks>
    public T Farads => QuectoFarads.ToBaseUnits();

    /// <summary>
    /// Gets the capacitance in Decafarads (daF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daF.
    /// </remarks>
    public T DecaFarads => QuectoFarads.ToDecaUnits();

    /// <summary>
    /// Gets the capacitance in Hectofarads (hF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hF.
    /// </remarks>
    public T HectoFarads => QuectoFarads.ToHectoUnits();

    /// <summary>
    /// Gets the capacitance in Kilofarads (kF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kF.
    /// </remarks>
    public T KiloFarads => QuectoFarads.ToKiloUnits();

    /// <summary>
    /// Gets the capacitance in Megafarads (MF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MF.
    /// </remarks>
    public T MegaFarads => QuectoFarads.ToMegaUnits();

    /// <summary>
    /// Gets the capacitance in Gigafarads (GF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GF.
    /// </remarks>
    public T GigaFarads => QuectoFarads.ToGigaUnits();

    /// <summary>
    /// Gets the capacitance in Terafarads (TF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TF.
    /// </remarks>
    public T TeraFarads => QuectoFarads.ToTeraUnits();

    /// <summary>
    /// Gets the capacitance in Petafarads (PF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PF.
    /// </remarks>
    public T PetaFarads => QuectoFarads.ToPetaUnits();

    /// <summary>
    /// Gets the capacitance in Exafarads (EF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EF.
    /// </remarks>
    public T ExaFarads => QuectoFarads.ToExaUnits();

    /// <summary>
    /// Gets the capacitance in Zettafarads (ZF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZF.
    /// </remarks>
    public T ZettaFarads => QuectoFarads.ToZettaUnits();

    /// <summary>
    /// Gets the capacitance in Yottafarads (YF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YF.
    /// </remarks>
    public T YottaFarads => QuectoFarads.ToYottaUnits();

    /// <summary>
    /// Gets the capacitance in Ronnafarads (RF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RF.
    /// </remarks>
    public T RonnaFarads => QuectoFarads.ToRonnaUnits();

    /// <summary>
    /// Gets the capacitance in Quettafarads (QF).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QF.
    /// </remarks>
    public T QuettaFarads => QuectoFarads.ToQuettaUnits();
}
