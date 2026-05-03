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
/// Represents a unit of electrical resistance.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Resistance<T> : IUnit<Resistance<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Resistance{T}"/> struct.
    /// </summary>
    /// <param name="value">The resistance unit in <see cref="QuectoOhms"/>.</param>
    private Resistance(T value) => QuectoOhms = value;

    /// <summary>
    /// Gets the resistance in Quectoohms (qΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qohm.
    /// </remarks>
    public T QuectoOhms { get; }

    /// <summary>
    /// Gets the resistance in Rontoohms (rΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rohm.
    /// </remarks>
    public T RontoOhms => QuectoOhms.ToRontoUnits();

    /// <summary>
    /// Gets the resistance in Yoctoohms (yΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yohm.
    /// </remarks>
    public T YoctoOhms => QuectoOhms.ToYoctoUnits();

    /// <summary>
    /// Gets the resistance in Zeptoohms (zΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zohm.
    /// </remarks>
    public T ZeptoOhms => QuectoOhms.ToZeptoUnits();

    /// <summary>
    /// Gets the resistance in Attoohms (aΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aohm.
    /// </remarks>
    public T AttoOhms => QuectoOhms.ToAttoUnits();

    /// <summary>
    /// Gets the resistance in Femtoohms (fΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fohm.
    /// </remarks>
    public T FemtoOhms => QuectoOhms.ToFemtoUnits();

    /// <summary>
    /// Gets the resistance in Picoohms (pΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pohm.
    /// </remarks>
    public T PicoOhms => QuectoOhms.ToPicoUnits();

    /// <summary>
    /// Gets the resistance in Nanoohms (nΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nohm.
    /// </remarks>
    public T NanoOhms => QuectoOhms.ToNanoUnits();

    /// <summary>
    /// Gets the resistance in Microohms (µΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uohm.
    /// </remarks>
    public T MicroOhms => QuectoOhms.ToMicroUnits();

    /// <summary>
    /// Gets the resistance in Milliohms (mΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mohm.
    /// </remarks>
    public T MilliOhms => QuectoOhms.ToMilliUnits();

    /// <summary>
    /// Gets the resistance in Centiohms (cΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cohm.
    /// </remarks>
    public T CentiOhms => QuectoOhms.ToCentiUnits();

    /// <summary>
    /// Gets the resistance in Deciohms (dΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dohm.
    /// </remarks>
    public T DeciOhms => QuectoOhms.ToDeciUnits();

    /// <summary>
    /// Gets the resistance in Ohms (Ω).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ohm.
    /// </remarks>
    public T Ohms => QuectoOhms.ToBaseUnits();

    /// <summary>
    /// Gets the resistance in Decaohms (daΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daohm.
    /// </remarks>
    public T DecaOhms => QuectoOhms.ToDecaUnits();

    /// <summary>
    /// Gets the resistance in Hectoohms (hΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hohm.
    /// </remarks>
    public T HectoOhms => QuectoOhms.ToHectoUnits();

    /// <summary>
    /// Gets the resistance in Kiloohms (kΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kohm.
    /// </remarks>
    public T KiloOhms => QuectoOhms.ToKiloUnits();

    /// <summary>
    /// Gets the resistance in Megaohms (MΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mohm.
    /// </remarks>
    public T MegaOhms => QuectoOhms.ToMegaUnits();

    /// <summary>
    /// Gets the resistance in Gigaohms (GΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gohm.
    /// </remarks>
    public T GigaOhms => QuectoOhms.ToGigaUnits();

    /// <summary>
    /// Gets the resistance in Teraohms (TΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tohm.
    /// </remarks>
    public T TeraOhms => QuectoOhms.ToTeraUnits();

    /// <summary>
    /// Gets the resistance in Petaohms (PΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pohm.
    /// </remarks>
    public T PetaOhms => QuectoOhms.ToPetaUnits();

    /// <summary>
    /// Gets the resistance in Exaohms (EΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Eohm.
    /// </remarks>
    public T ExaOhms => QuectoOhms.ToExaUnits();

    /// <summary>
    /// Gets the resistance in Zettaohms (ZΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zohm.
    /// </remarks>
    public T ZettaOhms => QuectoOhms.ToZettaUnits();

    /// <summary>
    /// Gets the resistance in Yottaohms (YΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Yohm.
    /// </remarks>
    public T YottaOhms => QuectoOhms.ToYottaUnits();

    /// <summary>
    /// Gets the resistance in Ronnaohms (RΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rohm.
    /// </remarks>
    public T RonnaOhms => QuectoOhms.ToRonnaUnits();

    /// <summary>
    /// Gets the resistance in Quettaohms (QΩ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qohm.
    /// </remarks>
    public T QuettaOhms => QuectoOhms.ToQuettaUnits();
}
