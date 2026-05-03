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

public readonly partial struct Capacitance<T>
{
    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Quectofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromQuectofarads(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Rontofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromRontofarads(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Yoctofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromYoctofarads(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Zeptofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromZeptofarads(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Attofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromAttofarads(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Femtofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromFemtofarads(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Picofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromPicofarads(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Nanofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromNanofarads(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Microfarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromMicrofarads(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Millifarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromMillifarads(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Centifarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromCentifarads(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Decifarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromDecifarads(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Farads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromFarads(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Decafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromDecafarads(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Hectofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromHectofarads(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Kilofarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromKilofarads(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Megafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromMegafarads(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Gigafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromGigafarads(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Terafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromTerafarads(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Petafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromPetafarads(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Exafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromExafarads(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Zettafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromZettafarads(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Yottafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromYottafarads(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Ronnafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromRonnafarads(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Capacitance{T}"/> instance from the specified Quettafarads value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Capacitance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Capacitance{T}"/> instance from the specified value.</returns>
    public static Capacitance<T> FromQuettafarads(T value) => new(value.FromQuettaUnits());
}
