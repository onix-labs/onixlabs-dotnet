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
/// Represents a unit of electric current.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Current<T> : IAdditiveUnit<Current<T>>, IMultiplicativeUnit<Current<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Current{T}"/> struct.
    /// </summary>
    /// <param name="value">The current unit in <see cref="QuectoAmperes"/>.</param>
    private Current(T value) => QuectoAmperes = value;

    /// <summary>
    /// Gets the current in Quectoamperes (qA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qA.
    /// </remarks>
    public T QuectoAmperes { get; }

    /// <summary>
    /// Gets the current in Rontoamperes (rA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rA.
    /// </remarks>
    public T RontoAmperes => QuectoAmperes.ToRontoUnits();

    /// <summary>
    /// Gets the current in Yoctoamperes (yA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yA.
    /// </remarks>
    public T YoctoAmperes => QuectoAmperes.ToYoctoUnits();

    /// <summary>
    /// Gets the current in Zeptoamperes (zA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zA.
    /// </remarks>
    public T ZeptoAmperes => QuectoAmperes.ToZeptoUnits();

    /// <summary>
    /// Gets the current in Attoamperes (aA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aA.
    /// </remarks>
    public T AttoAmperes => QuectoAmperes.ToAttoUnits();

    /// <summary>
    /// Gets the current in Femtoamperes (fA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fA.
    /// </remarks>
    public T FemtoAmperes => QuectoAmperes.ToFemtoUnits();

    /// <summary>
    /// Gets the current in Picoamperes (pA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pA.
    /// </remarks>
    public T PicoAmperes => QuectoAmperes.ToPicoUnits();

    /// <summary>
    /// Gets the current in Nanoamperes (nA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nA.
    /// </remarks>
    public T NanoAmperes => QuectoAmperes.ToNanoUnits();

    /// <summary>
    /// Gets the current in Microamperes (µA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uA.
    /// </remarks>
    public T MicroAmperes => QuectoAmperes.ToMicroUnits();

    /// <summary>
    /// Gets the current in Milliamperes (mA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mA.
    /// </remarks>
    public T MilliAmperes => QuectoAmperes.ToMilliUnits();

    /// <summary>
    /// Gets the current in Centiamperes (cA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cA.
    /// </remarks>
    public T CentiAmperes => QuectoAmperes.ToCentiUnits();

    /// <summary>
    /// Gets the current in Deciamperes (dA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dA.
    /// </remarks>
    public T DeciAmperes => QuectoAmperes.ToDeciUnits();

    /// <summary>
    /// Gets the current in Amperes (A).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is A.
    /// </remarks>
    public T Amperes => QuectoAmperes.ToBaseUnits();

    /// <summary>
    /// Gets the current in Decaamperes (daA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daA.
    /// </remarks>
    public T DecaAmperes => QuectoAmperes.ToDecaUnits();

    /// <summary>
    /// Gets the current in Hectoamperes (hA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hA.
    /// </remarks>
    public T HectoAmperes => QuectoAmperes.ToHectoUnits();

    /// <summary>
    /// Gets the current in Kiloamperes (kA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kA.
    /// </remarks>
    public T KiloAmperes => QuectoAmperes.ToKiloUnits();

    /// <summary>
    /// Gets the current in Megaamperes (MA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MA.
    /// </remarks>
    public T MegaAmperes => QuectoAmperes.ToMegaUnits();

    /// <summary>
    /// Gets the current in Gigaamperes (GA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GA.
    /// </remarks>
    public T GigaAmperes => QuectoAmperes.ToGigaUnits();

    /// <summary>
    /// Gets the current in Teraamperes (TA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TA.
    /// </remarks>
    public T TeraAmperes => QuectoAmperes.ToTeraUnits();

    /// <summary>
    /// Gets the current in Petaamperes (PA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PA.
    /// </remarks>
    public T PetaAmperes => QuectoAmperes.ToPetaUnits();

    /// <summary>
    /// Gets the current in Exaamperes (EA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EA.
    /// </remarks>
    public T ExaAmperes => QuectoAmperes.ToExaUnits();

    /// <summary>
    /// Gets the current in Zettaamperes (ZA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZA.
    /// </remarks>
    public T ZettaAmperes => QuectoAmperes.ToZettaUnits();

    /// <summary>
    /// Gets the current in Yottaamperes (YA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YA.
    /// </remarks>
    public T YottaAmperes => QuectoAmperes.ToYottaUnits();

    /// <summary>
    /// Gets the current in Ronnaamperes (RA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RA.
    /// </remarks>
    public T RonnaAmperes => QuectoAmperes.ToRonnaUnits();

    /// <summary>
    /// Gets the current in Quettaamperes (QA).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QA.
    /// </remarks>
    public T QuettaAmperes => QuectoAmperes.ToQuettaUnits();
}
