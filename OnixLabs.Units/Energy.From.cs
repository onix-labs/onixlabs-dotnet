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

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Quectojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromQuectojoules(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Rontojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromRontojoules(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Yoctojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromYoctojoules(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Zeptojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromZeptojoules(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Attojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromAttojoules(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Femtojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromFemtojoules(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Picojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromPicojoules(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Nanojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromNanojoules(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Microjoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMicrojoules(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Millijoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMillijoules(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Centijoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromCentijoules(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Decijoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromDecijoules(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Joules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromJoules(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Decajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromDecajoules(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Hectojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromHectojoules(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Kilojoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKilojoules(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Megajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMegajoules(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Gigajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromGigajoules(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Terajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromTerajoules(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Petajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromPetajoules(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Exajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromExajoules(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Zettajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromZettajoules(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Yottajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromYottajoules(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Ronnajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromRonnajoules(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Quettajoules value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromQuettajoules(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Calories value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromCalories(T value) => new(value * T.CreateChecked(4.184e30));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Kilocalories value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKilocalories(T value) => new(value * T.CreateChecked(4.184e33));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Watt Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromWattHours(T value) => new(value * T.CreateChecked(3.6e33));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Kilowatt Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKilowattHours(T value) => new(value * T.CreateChecked(3.6e36));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Megawatt Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMegawattHours(T value) => new(value * T.CreateChecked(3.6e39));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Gigawatt Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromGigawattHours(T value) => new(value * T.CreateChecked(3.6e42));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Terawatt Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromTerawattHours(T value) => new(value * T.CreateChecked(3.6e45));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified British Thermal Units value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromBritishThermalUnits(T value) => new(value * T.CreateChecked(1.05505585262e33));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Therms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromTherms(T value) => new(value * T.CreateChecked(1.05505585262e38));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Ergs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromErgs(T value) => new(value * T.CreateChecked(1e23));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Electron Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e11));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Kilo Electron Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKiloElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e14));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Mega Electron Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMegaElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e17));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Giga Electron Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromGigaElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e20));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Tera Electron Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromTeraElectronVolts(T value) => new(value * T.CreateChecked(1.602176634e23));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Foot Pounds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromFootPounds(T value) => new(value * T.CreateChecked(1.3558179483314004e30));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Tons Of TNT value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromTonsOfTnt(T value) => new(value * T.CreateChecked(4.184e39));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Kilotons Of TNT value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromKilotonsOfTnt(T value) => new(value * T.CreateChecked(4.184e42));

    /// <summary>
    /// Creates a new <see cref="Energy{T}"/> instance from the specified Megatons Of TNT value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Energy{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Energy{T}"/> instance from the specified value.</returns>
    public static Energy<T> FromMegatonsOfTnt(T value) => new(value * T.CreateChecked(4.184e45));
}
