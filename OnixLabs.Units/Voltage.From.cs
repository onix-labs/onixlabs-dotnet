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

public readonly partial struct Voltage<T>
{
    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Quectovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromQuectovolts(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Rontovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromRontovolts(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Yoctovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromYoctovolts(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Zeptovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromZeptovolts(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Attovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromAttovolts(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Femtovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromFemtovolts(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Picovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromPicovolts(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Nanovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromNanovolts(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Microvolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromMicrovolts(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Millivolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromMillivolts(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Centivolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromCentivolts(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Decivolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromDecivolts(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Volts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromVolts(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Decavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromDecavolts(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Hectovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromHectovolts(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Kilovolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromKilovolts(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Megavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromMegavolts(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Gigavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromGigavolts(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Teravolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromTeravolts(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Petavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromPetavolts(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Exavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromExavolts(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Zettavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromZettavolts(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Yottavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromYottavolts(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Ronnavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromRonnavolts(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Voltage{T}"/> instance from the specified Quettavolts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Voltage{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Voltage{T}"/> instance from the specified value.</returns>
    public static Voltage<T> FromQuettavolts(T value) => new(value.FromQuettaUnits());
}
