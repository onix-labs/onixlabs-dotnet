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
/// Represents a unit of electric potential difference.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Voltage<T> : IUnit<Voltage<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Voltage{T}"/> struct.
    /// </summary>
    /// <param name="value">The voltage unit in <see cref="QuectoVolts"/>.</param>
    private Voltage(T value) => QuectoVolts = value;

    /// <summary>
    /// Gets the voltage in Quectovolts (qV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qV.
    /// </remarks>
    public T QuectoVolts { get; }

    /// <summary>
    /// Gets the voltage in Rontovolts (rV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rV.
    /// </remarks>
    public T RontoVolts => QuectoVolts.ToRontoUnits();

    /// <summary>
    /// Gets the voltage in Yoctovolts (yV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yV.
    /// </remarks>
    public T YoctoVolts => QuectoVolts.ToYoctoUnits();

    /// <summary>
    /// Gets the voltage in Zeptovolts (zV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zV.
    /// </remarks>
    public T ZeptoVolts => QuectoVolts.ToZeptoUnits();

    /// <summary>
    /// Gets the voltage in Attovolts (aV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aV.
    /// </remarks>
    public T AttoVolts => QuectoVolts.ToAttoUnits();

    /// <summary>
    /// Gets the voltage in Femtovolts (fV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fV.
    /// </remarks>
    public T FemtoVolts => QuectoVolts.ToFemtoUnits();

    /// <summary>
    /// Gets the voltage in Picovolts (pV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pV.
    /// </remarks>
    public T PicoVolts => QuectoVolts.ToPicoUnits();

    /// <summary>
    /// Gets the voltage in Nanovolts (nV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nV.
    /// </remarks>
    public T NanoVolts => QuectoVolts.ToNanoUnits();

    /// <summary>
    /// Gets the voltage in Microvolts (µV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uV.
    /// </remarks>
    public T MicroVolts => QuectoVolts.ToMicroUnits();

    /// <summary>
    /// Gets the voltage in Millivolts (mV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mV.
    /// </remarks>
    public T MilliVolts => QuectoVolts.ToMilliUnits();

    /// <summary>
    /// Gets the voltage in Centivolts (cV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cV.
    /// </remarks>
    public T CentiVolts => QuectoVolts.ToCentiUnits();

    /// <summary>
    /// Gets the voltage in Decivolts (dV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dV.
    /// </remarks>
    public T DeciVolts => QuectoVolts.ToDeciUnits();

    /// <summary>
    /// Gets the voltage in Volts (V).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is V.
    /// </remarks>
    public T Volts => QuectoVolts.ToBaseUnits();

    /// <summary>
    /// Gets the voltage in Decavolts (daV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daV.
    /// </remarks>
    public T DecaVolts => QuectoVolts.ToDecaUnits();

    /// <summary>
    /// Gets the voltage in Hectovolts (hV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hV.
    /// </remarks>
    public T HectoVolts => QuectoVolts.ToHectoUnits();

    /// <summary>
    /// Gets the voltage in Kilovolts (kV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kV.
    /// </remarks>
    public T KiloVolts => QuectoVolts.ToKiloUnits();

    /// <summary>
    /// Gets the voltage in Megavolts (MV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MV.
    /// </remarks>
    public T MegaVolts => QuectoVolts.ToMegaUnits();

    /// <summary>
    /// Gets the voltage in Gigavolts (GV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GV.
    /// </remarks>
    public T GigaVolts => QuectoVolts.ToGigaUnits();

    /// <summary>
    /// Gets the voltage in Teravolts (TV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TV.
    /// </remarks>
    public T TeraVolts => QuectoVolts.ToTeraUnits();

    /// <summary>
    /// Gets the voltage in Petavolts (PV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PV.
    /// </remarks>
    public T PetaVolts => QuectoVolts.ToPetaUnits();

    /// <summary>
    /// Gets the voltage in Exavolts (EV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EV.
    /// </remarks>
    public T ExaVolts => QuectoVolts.ToExaUnits();

    /// <summary>
    /// Gets the voltage in Zettavolts (ZV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZV.
    /// </remarks>
    public T ZettaVolts => QuectoVolts.ToZettaUnits();

    /// <summary>
    /// Gets the voltage in Yottavolts (YV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YV.
    /// </remarks>
    public T YottaVolts => QuectoVolts.ToYottaUnits();

    /// <summary>
    /// Gets the voltage in Ronnavolts (RV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RV.
    /// </remarks>
    public T RonnaVolts => QuectoVolts.ToRonnaUnits();

    /// <summary>
    /// Gets the voltage in Quettavolts (QV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QV.
    /// </remarks>
    public T QuettaVolts => QuectoVolts.ToQuettaUnits();
}
