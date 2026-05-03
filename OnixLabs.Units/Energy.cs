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
/// Represents a unit of energy.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Energy<T> : IUnit<Energy<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Energy{T}"/> struct.
    /// </summary>
    /// <param name="value">The energy unit in <see cref="QuectoJoules"/>.</param>
    private Energy(T value) => QuectoJoules = value;

    /// <summary>
    /// Gets the energy in Quectojoules (qJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qJ.
    /// </remarks>
    public T QuectoJoules { get; }

    /// <summary>
    /// Gets the energy in Rontojoules (rJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rJ.
    /// </remarks>
    public T RontoJoules => QuectoJoules.ToRontoUnits();

    /// <summary>
    /// Gets the energy in Yoctojoules (yJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yJ.
    /// </remarks>
    public T YoctoJoules => QuectoJoules.ToYoctoUnits();

    /// <summary>
    /// Gets the energy in Zeptojoules (zJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zJ.
    /// </remarks>
    public T ZeptoJoules => QuectoJoules.ToZeptoUnits();

    /// <summary>
    /// Gets the energy in Attojoules (aJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aJ.
    /// </remarks>
    public T AttoJoules => QuectoJoules.ToAttoUnits();

    /// <summary>
    /// Gets the energy in Femtojoules (fJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fJ.
    /// </remarks>
    public T FemtoJoules => QuectoJoules.ToFemtoUnits();

    /// <summary>
    /// Gets the energy in Picojoules (pJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pJ.
    /// </remarks>
    public T PicoJoules => QuectoJoules.ToPicoUnits();

    /// <summary>
    /// Gets the energy in Nanojoules (nJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nJ.
    /// </remarks>
    public T NanoJoules => QuectoJoules.ToNanoUnits();

    /// <summary>
    /// Gets the energy in Microjoules (µJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uJ.
    /// </remarks>
    public T MicroJoules => QuectoJoules.ToMicroUnits();

    /// <summary>
    /// Gets the energy in Millijoules (mJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mJ.
    /// </remarks>
    public T MilliJoules => QuectoJoules.ToMilliUnits();

    /// <summary>
    /// Gets the energy in Centijoules (cJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cJ.
    /// </remarks>
    public T CentiJoules => QuectoJoules.ToCentiUnits();

    /// <summary>
    /// Gets the energy in Decijoules (dJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dJ.
    /// </remarks>
    public T DeciJoules => QuectoJoules.ToDeciUnits();

    /// <summary>
    /// Gets the energy in Joules (J).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is J.
    /// </remarks>
    public T Joules => QuectoJoules.ToBaseUnits();

    /// <summary>
    /// Gets the energy in Decajoules (daJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daJ.
    /// </remarks>
    public T DecaJoules => QuectoJoules.ToDecaUnits();

    /// <summary>
    /// Gets the energy in Hectojoules (hJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hJ.
    /// </remarks>
    public T HectoJoules => QuectoJoules.ToHectoUnits();

    /// <summary>
    /// Gets the energy in Kilojoules (kJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kJ.
    /// </remarks>
    public T KiloJoules => QuectoJoules.ToKiloUnits();

    /// <summary>
    /// Gets the energy in Megajoules (MJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MJ.
    /// </remarks>
    public T MegaJoules => QuectoJoules.ToMegaUnits();

    /// <summary>
    /// Gets the energy in Gigajoules (GJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GJ.
    /// </remarks>
    public T GigaJoules => QuectoJoules.ToGigaUnits();

    /// <summary>
    /// Gets the energy in Terajoules (TJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TJ.
    /// </remarks>
    public T TeraJoules => QuectoJoules.ToTeraUnits();

    /// <summary>
    /// Gets the energy in Petajoules (PJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PJ.
    /// </remarks>
    public T PetaJoules => QuectoJoules.ToPetaUnits();

    /// <summary>
    /// Gets the energy in Exajoules (EJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EJ.
    /// </remarks>
    public T ExaJoules => QuectoJoules.ToExaUnits();

    /// <summary>
    /// Gets the energy in Zettajoules (ZJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZJ.
    /// </remarks>
    public T ZettaJoules => QuectoJoules.ToZettaUnits();

    /// <summary>
    /// Gets the energy in Yottajoules (YJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YJ.
    /// </remarks>
    public T YottaJoules => QuectoJoules.ToYottaUnits();

    /// <summary>
    /// Gets the energy in Ronnajoules (RJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RJ.
    /// </remarks>
    public T RonnaJoules => QuectoJoules.ToRonnaUnits();

    /// <summary>
    /// Gets the energy in Quettajoules (QJ).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QJ.
    /// </remarks>
    public T QuettaJoules => QuectoJoules.ToQuettaUnits();

    /// <summary>
    /// Gets the energy in Calories (cal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cal.
    /// </remarks>
    public T Calories => QuectoJoules / T.CreateChecked(4.184e30);

    /// <summary>
    /// Gets the energy in Kilocalories (kcal).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kcal.
    /// </remarks>
    public T Kilocalories => QuectoJoules / T.CreateChecked(4.184e33);

    /// <summary>
    /// Gets the energy in Watt Hours (Wh).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Wh.
    /// </remarks>
    public T WattHours => QuectoJoules / T.CreateChecked(3.6e33);

    /// <summary>
    /// Gets the energy in Kilowatt Hours (kWh).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kWh.
    /// </remarks>
    public T KilowattHours => QuectoJoules / T.CreateChecked(3.6e36);

    /// <summary>
    /// Gets the energy in Megawatt Hours (MWh).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MWh.
    /// </remarks>
    public T MegawattHours => QuectoJoules / T.CreateChecked(3.6e39);

    /// <summary>
    /// Gets the energy in Gigawatt Hours (GWh).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GWh.
    /// </remarks>
    public T GigawattHours => QuectoJoules / T.CreateChecked(3.6e42);

    /// <summary>
    /// Gets the energy in Terawatt Hours (TWh).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TWh.
    /// </remarks>
    public T TerawattHours => QuectoJoules / T.CreateChecked(3.6e45);

    /// <summary>
    /// Gets the energy in British Thermal Units (BTU).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is BTU.
    /// </remarks>
    public T BritishThermalUnits => QuectoJoules / T.CreateChecked(1.05505585262e33);

    /// <summary>
    /// Gets the energy in Therms (therm).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is therm.
    /// </remarks>
    public T Therms => QuectoJoules / T.CreateChecked(1.05505585262e38);

    /// <summary>
    /// Gets the energy in Ergs (erg).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is erg.
    /// </remarks>
    public T Ergs => QuectoJoules / T.CreateChecked(1e23);

    /// <summary>
    /// Gets the energy in Electron Volts (eV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is eV.
    /// </remarks>
    public T ElectronVolts => QuectoJoules / T.CreateChecked(1.602176634e11);

    /// <summary>
    /// Gets the energy in Kilo Electron Volts (keV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is keV.
    /// </remarks>
    public T KiloElectronVolts => QuectoJoules / T.CreateChecked(1.602176634e14);

    /// <summary>
    /// Gets the energy in Mega Electron Volts (MeV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MeV.
    /// </remarks>
    public T MegaElectronVolts => QuectoJoules / T.CreateChecked(1.602176634e17);

    /// <summary>
    /// Gets the energy in Giga Electron Volts (GeV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GeV.
    /// </remarks>
    public T GigaElectronVolts => QuectoJoules / T.CreateChecked(1.602176634e20);

    /// <summary>
    /// Gets the energy in Tera Electron Volts (TeV).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TeV.
    /// </remarks>
    public T TeraElectronVolts => QuectoJoules / T.CreateChecked(1.602176634e23);

    /// <summary>
    /// Gets the energy in Foot Pounds (ft·lbf).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ftlbf.
    /// </remarks>
    public T FootPounds => QuectoJoules / T.CreateChecked(1.3558179483314004e30);

    /// <summary>
    /// Gets the energy in Tons Of TNT (tTNT).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is tTNT.
    /// </remarks>
    public T TonsOfTnt => QuectoJoules / T.CreateChecked(4.184e39);

    /// <summary>
    /// Gets the energy in Kilotons Of TNT (ktTNT).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ktTNT.
    /// </remarks>
    public T KilotonsOfTnt => QuectoJoules / T.CreateChecked(4.184e42);

    /// <summary>
    /// Gets the energy in Megatons Of TNT (MtTNT).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MtTNT.
    /// </remarks>
    public T MegatonsOfTnt => QuectoJoules / T.CreateChecked(4.184e45);
}
