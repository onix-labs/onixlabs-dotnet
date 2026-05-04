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

public readonly partial struct Time<T>
{
    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Quectoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromQuectoseconds(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Rontoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromRontoseconds(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Yoctoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromYoctoseconds(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Zeptoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromZeptoseconds(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Attoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromAttoseconds(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Femtoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromFemtoseconds(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Picoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromPicoseconds(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Nanoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromNanoseconds(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Microseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromMicroseconds(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Milliseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromMilliseconds(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Centiseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromCentiseconds(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Deciseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromDeciseconds(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Seconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromSeconds(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Decaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromDecaseconds(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Hectoseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromHectoseconds(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Kiloseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromKiloseconds(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Megaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromMegaseconds(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Gigaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromGigaseconds(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Teraseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromTeraseconds(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Petaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromPetaseconds(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Exaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromExaseconds(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Zettaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromZettaseconds(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Yottaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromYottaseconds(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Ronnaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromRonnaseconds(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Quettaseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromQuettaseconds(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Minutes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromMinutes(T value) => new(value * T.CreateChecked(6e31));

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromHours(T value) => new(value * T.CreateChecked(3.6e33));

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Days value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromDays(T value) => new(value * T.CreateChecked(8.64e34));

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Weeks value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromWeeks(T value) => new(value * T.CreateChecked(6.048e35));

    /// <summary>
    /// Creates a new <see cref="Time{T}"/> instance from the specified Julian Years value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Time{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Time{T}"/> instance from the specified value.</returns>
    public static Time<T> FromJulianYears(T value) => new(value * T.CreateChecked(3.15576e37));
}
