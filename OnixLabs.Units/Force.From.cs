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

public readonly partial struct Force<T>
{
    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Quectonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromQuectonewtons(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Rontonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromRontonewtons(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Yoctonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromYoctonewtons(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Zeptonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromZeptonewtons(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Attonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromAttonewtons(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Femtonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromFemtonewtons(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Piconewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPiconewtons(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Nanonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromNanonewtons(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Micronewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMicronewtons(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Millinewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMillinewtons(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Centinewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromCentinewtons(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Decinewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromDecinewtons(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Newtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromNewtons(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Decanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromDecanewtons(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Hectonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromHectonewtons(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Kilonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromKilonewtons(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Meganewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMeganewtons(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Giganewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromGiganewtons(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Teranewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromTeranewtons(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Petanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPetanewtons(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Exanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromExanewtons(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Zettanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromZettanewtons(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Yottanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromYottanewtons(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Ronnanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromRonnanewtons(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Quettanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromQuettanewtons(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Dynes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromDynes(T value) => new(value * T.CreateChecked(1e25));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Pounds-force value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPoundsForce(T value) => new(value * T.CreateChecked(4.4482216152605e30));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Ounces-force value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromOuncesForce(T value) => new(value * T.CreateChecked(2.7801385095378125e29));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Poundals value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPoundals(T value) => new(value * T.CreateChecked(1.38254954376e29));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Kilograms-force value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromKilogramsForce(T value) => new(value * T.CreateChecked(9.80665e30));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Grams-force value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromGramsForce(T value) => new(value * T.CreateChecked(9.80665e27));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified Metric Tons-force value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMetricTonsForce(T value) => new(value * T.CreateChecked(9.80665e33));
}
