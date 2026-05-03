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

public readonly partial struct Power<T>
{
    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Quectowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromQuectowatts(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Rontowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromRontowatts(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Yoctowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromYoctowatts(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Zeptowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromZeptowatts(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Attowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromAttowatts(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Femtowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromFemtowatts(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Picowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromPicowatts(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Nanowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromNanowatts(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Microwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMicrowatts(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Milliwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMilliwatts(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Centiwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromCentiwatts(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Deciwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromDeciwatts(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Watts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromWatts(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Decawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromDecawatts(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Hectowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromHectowatts(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Kilowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromKilowatts(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Megawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMegawatts(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Gigawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromGigawatts(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Terawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromTerawatts(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Petawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromPetawatts(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Exawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromExawatts(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Zettawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromZettawatts(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Yottawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromYottawatts(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Ronnawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromRonnawatts(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Quettawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromQuettawatts(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Mechanical Horsepower value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMechanicalHorsepower(T value) => new(value * T.CreateChecked(7.456998715822702e32));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Metric Horsepower value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMetricHorsepower(T value) => new(value * T.CreateChecked(7.3549875e32));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified BTUs Per Hour value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromBtusPerHour(T value) => new((value * T.CreateChecked(1055.05585262) / T.CreateChecked(3600)).FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Calories Per Second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromCaloriesPerSecond(T value) => new(value * T.CreateChecked(4.184e30));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Ergs Per Second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromErgsPerSecond(T value) => new(value * T.CreateChecked(1e23));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Foot Pounds Per Second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromFootPoundsPerSecond(T value) => new(value * T.CreateChecked(1.3558179483314004e30));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified Tons Of Refrigeration value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromTonsOfRefrigeration(T value) => new(value * T.CreateChecked(3.5168528420666664e33));
}
