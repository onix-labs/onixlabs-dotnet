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

public readonly partial struct Frequency<T>
{
    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Quectohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromQuectohertz(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Rontohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromRontohertz(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Yoctohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromYoctohertz(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Zeptohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromZeptohertz(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Attohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromAttohertz(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Femtohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromFemtohertz(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Picohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromPicohertz(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Nanohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromNanohertz(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Microhertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMicrohertz(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Millihertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMillihertz(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Centihertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromCentihertz(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Decihertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromDecihertz(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Hertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromHertz(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Decahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromDecahertz(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Hectohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromHectohertz(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Kilohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromKilohertz(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Megahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMegahertz(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Gigahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromGigahertz(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Terahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromTerahertz(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Petahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromPetahertz(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Exahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromExahertz(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Zettahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromZettahertz(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Yottahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromYottahertz(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Ronnahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromRonnahertz(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Quettahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromQuettahertz(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Revolutions Per Minute value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromRevolutionsPerMinute(T value) => new((value / T.CreateChecked(60)).FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Beats Per Minute value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromBeatsPerMinute(T value) => new((value / T.CreateChecked(60)).FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified Radians Per Second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromRadiansPerSecond(T value) => new((value / (T.CreateChecked(2) * T.Pi)).FromBaseUnits());
}
