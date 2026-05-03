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

public readonly partial struct Resistance<T>
{
    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Quectoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromQuectoohms(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Rontoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromRontoohms(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Yoctoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromYoctoohms(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Zeptoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromZeptoohms(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Attoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromAttoohms(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Femtoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromFemtoohms(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Picoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromPicoohms(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Nanoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromNanoohms(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Microohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromMicroohms(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Milliohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromMilliohms(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Centiohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromCentiohms(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Deciohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromDeciohms(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Ohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromOhms(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Decaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromDecaohms(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Hectoohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromHectoohms(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Kiloohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromKiloohms(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Megaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromMegaohms(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Gigaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromGigaohms(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Teraohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromTeraohms(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Petaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromPetaohms(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Exaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromExaohms(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Zettaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromZettaohms(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Yottaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromYottaohms(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Ronnaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromRonnaohms(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Resistance{T}"/> instance from the specified Quettaohms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Resistance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Resistance{T}"/> instance from the specified value.</returns>
    public static Resistance<T> FromQuettaohms(T value) => new(value.FromQuettaUnits());
}
