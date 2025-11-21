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
    /// Creates a new <see cref="Power{T}"/> instance from the specified yoctowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromYoctoWatts(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified zeptowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromZeptoWatts(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified attowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromAttoWatts(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified femtowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromFemtoWatts(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified picowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromPicoWatts(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified nanowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromNanoWatts(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified microwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMicroWatts(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified milliwatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMilliWatts(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified watts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromWatts(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified kilowatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromKiloWatts(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified megawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMegaWatts(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified gigawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromGigaWatts(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified terawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromTeraWatts(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified petawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromPetaWatts(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified exawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromExaWatts(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified zettawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromZettaWatts(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified yottawatts value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromYottaWatts(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified mechanical horsepower value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromHorsepower(T value) => new(value * T.CreateChecked(7.456998715822702e26));

    /// <summary>
    /// Creates a new <see cref="Power{T}"/> instance from the specified metric horsepower value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Power{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Power{T}"/> instance from the specified value.</returns>
    public static Power<T> FromMetricHorsepower(T value) => new(value * T.CreateChecked(7.3549875e26));
}
