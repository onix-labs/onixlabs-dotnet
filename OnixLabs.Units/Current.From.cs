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

public readonly partial struct Current<T>
{
    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Quectoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromQuectoamperes(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Rontoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromRontoamperes(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Yoctoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromYoctoamperes(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Zeptoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromZeptoamperes(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Attoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromAttoamperes(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Femtoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromFemtoamperes(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Picoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromPicoamperes(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Nanoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromNanoamperes(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Microamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromMicroamperes(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Milliamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromMilliamperes(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Centiamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromCentiamperes(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Deciamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromDeciamperes(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Amperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromAmperes(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Decaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromDecaamperes(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Hectoamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromHectoamperes(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Kiloamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromKiloamperes(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Megaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromMegaamperes(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Gigaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromGigaamperes(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Teraamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromTeraamperes(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Petaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromPetaamperes(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Exaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromExaamperes(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Zettaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromZettaamperes(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Yottaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromYottaamperes(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Ronnaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromRonnaamperes(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Current{T}"/> instance from the specified Quettaamperes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Current{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Current{T}"/> instance from the specified value.</returns>
    public static Current<T> FromQuettaamperes(T value) => new(value.FromQuettaUnits());
}
