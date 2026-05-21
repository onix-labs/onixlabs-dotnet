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

public readonly partial struct AmountOfSubstance<T>
{
    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Quectomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromQuectomoles(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Rontomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromRontomoles(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Yoctomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromYoctomoles(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Zeptomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromZeptomoles(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Attomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromAttomoles(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Femtomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromFemtomoles(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Picomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromPicomoles(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Nanomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromNanomoles(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Micromoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromMicromoles(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Millimoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromMillimoles(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Centimoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromCentimoles(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Decimoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromDecimoles(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Moles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromMoles(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Decamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromDecamoles(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Hectomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromHectomoles(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Kilomoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromKilomoles(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Megamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromMegamoles(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Gigamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromGigamoles(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Teramoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromTeramoles(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Petamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromPetamoles(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Examoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromExamoles(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Zettamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromZettamoles(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Yottamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromYottamoles(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Ronnamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromRonnamoles(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="AmountOfSubstance{T}"/> instance from the specified Quettamoles value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="AmountOfSubstance{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="AmountOfSubstance{T}"/> instance from the specified value.</returns>
    public static AmountOfSubstance<T> FromQuettamoles(T value) => new(value.FromQuettaUnits());
}
