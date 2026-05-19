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
/// Represents a unit of frequency.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Frequency<T> : IAdditiveUnit<Frequency<T>>, IMultiplicativeUnit<Frequency<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Frequency{T}"/> struct.
    /// </summary>
    /// <param name="value">The frequency unit in <see cref="QuectoHertz"/>.</param>
    private Frequency(T value) => QuectoHertz = value;

    /// <summary>
    /// Gets the frequency in Quectohertz (qHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qHz.
    /// </remarks>
    public T QuectoHertz { get; }

    /// <summary>
    /// Gets the frequency in Rontohertz (rHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rHz.
    /// </remarks>
    public T RontoHertz => QuectoHertz.ToRontoUnits();

    /// <summary>
    /// Gets the frequency in Yoctohertz (yHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yHz.
    /// </remarks>
    public T YoctoHertz => QuectoHertz.ToYoctoUnits();

    /// <summary>
    /// Gets the frequency in Zeptohertz (zHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zHz.
    /// </remarks>
    public T ZeptoHertz => QuectoHertz.ToZeptoUnits();

    /// <summary>
    /// Gets the frequency in Attohertz (aHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aHz.
    /// </remarks>
    public T AttoHertz => QuectoHertz.ToAttoUnits();

    /// <summary>
    /// Gets the frequency in Femtohertz (fHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fHz.
    /// </remarks>
    public T FemtoHertz => QuectoHertz.ToFemtoUnits();

    /// <summary>
    /// Gets the frequency in Picohertz (pHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pHz.
    /// </remarks>
    public T PicoHertz => QuectoHertz.ToPicoUnits();

    /// <summary>
    /// Gets the frequency in Nanohertz (nHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nHz.
    /// </remarks>
    public T NanoHertz => QuectoHertz.ToNanoUnits();

    /// <summary>
    /// Gets the frequency in Microhertz (µHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uHz.
    /// </remarks>
    public T MicroHertz => QuectoHertz.ToMicroUnits();

    /// <summary>
    /// Gets the frequency in Millihertz (mHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mHz.
    /// </remarks>
    public T MilliHertz => QuectoHertz.ToMilliUnits();

    /// <summary>
    /// Gets the frequency in Centihertz (cHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cHz.
    /// </remarks>
    public T CentiHertz => QuectoHertz.ToCentiUnits();

    /// <summary>
    /// Gets the frequency in Decihertz (dHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dHz.
    /// </remarks>
    public T DeciHertz => QuectoHertz.ToDeciUnits();

    /// <summary>
    /// Gets the frequency in Hertz (Hz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Hz.
    /// </remarks>
    public T Hertz => QuectoHertz.ToBaseUnits();

    /// <summary>
    /// Gets the frequency in Decahertz (daHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daHz.
    /// </remarks>
    public T DecaHertz => QuectoHertz.ToDecaUnits();

    /// <summary>
    /// Gets the frequency in Hectohertz (hHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hHz.
    /// </remarks>
    public T HectoHertz => QuectoHertz.ToHectoUnits();

    /// <summary>
    /// Gets the frequency in Kilohertz (kHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kHz.
    /// </remarks>
    public T KiloHertz => QuectoHertz.ToKiloUnits();

    /// <summary>
    /// Gets the frequency in Megahertz (MHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MHz.
    /// </remarks>
    public T MegaHertz => QuectoHertz.ToMegaUnits();

    /// <summary>
    /// Gets the frequency in Gigahertz (GHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GHz.
    /// </remarks>
    public T GigaHertz => QuectoHertz.ToGigaUnits();

    /// <summary>
    /// Gets the frequency in Terahertz (THz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is THz.
    /// </remarks>
    public T TeraHertz => QuectoHertz.ToTeraUnits();

    /// <summary>
    /// Gets the frequency in Petahertz (PHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PHz.
    /// </remarks>
    public T PetaHertz => QuectoHertz.ToPetaUnits();

    /// <summary>
    /// Gets the frequency in Exahertz (EHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EHz.
    /// </remarks>
    public T ExaHertz => QuectoHertz.ToExaUnits();

    /// <summary>
    /// Gets the frequency in Zettahertz (ZHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZHz.
    /// </remarks>
    public T ZettaHertz => QuectoHertz.ToZettaUnits();

    /// <summary>
    /// Gets the frequency in Yottahertz (YHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YHz.
    /// </remarks>
    public T YottaHertz => QuectoHertz.ToYottaUnits();

    /// <summary>
    /// Gets the frequency in Ronnahertz (RHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RHz.
    /// </remarks>
    public T RonnaHertz => QuectoHertz.ToRonnaUnits();

    /// <summary>
    /// Gets the frequency in Quettahertz (QHz).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QHz.
    /// </remarks>
    public T QuettaHertz => QuectoHertz.ToQuettaUnits();

    /// <summary>
    /// Gets the frequency in Revolutions Per Minute (rpm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rpm.
    /// </remarks>
    public T RevolutionsPerMinute => QuectoHertz / QuectoHertzPerRevolutionPerMinute;

    /// <summary>
    /// Gets the frequency in Beats Per Minute (bpm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is bpm.
    /// </remarks>
    public T BeatsPerMinute => QuectoHertz / QuectoHertzPerBeatPerMinute;

    /// <summary>
    /// Gets the frequency in Radians Per Second (rad/s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is radps.
    /// </remarks>
    public T RadiansPerSecond => QuectoHertz / QuectoHertzPerRadianPerSecond;
}
