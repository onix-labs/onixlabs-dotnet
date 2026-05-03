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

public readonly partial struct LuminousIntensity<T>
{
    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Quectocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromQuectocandelas(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Rontocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromRontocandelas(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Yoctocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromYoctocandelas(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Zeptocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromZeptocandelas(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Attocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromAttocandelas(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Femtocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromFemtocandelas(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Picocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromPicocandelas(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Nanocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromNanocandelas(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Microcandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromMicrocandelas(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Millicandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromMillicandelas(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Centicandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromCenticandelas(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Decicandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromDecicandelas(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Candelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromCandelas(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Decacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromDecacandelas(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Hectocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromHectocandelas(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Kilocandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromKilocandelas(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Megacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromMegacandelas(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Gigacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromGigacandelas(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Teracandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromTeracandelas(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Petacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromPetacandelas(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Exacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromExacandelas(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Zettacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromZettacandelas(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Yottacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromYottacandelas(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Ronnacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromRonnacandelas(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="LuminousIntensity{T}"/> instance from the specified Quettacandelas value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="LuminousIntensity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="LuminousIntensity{T}"/> instance from the specified value.</returns>
    public static LuminousIntensity<T> FromQuettacandelas(T value) => new(value.FromQuettaUnits());
}
