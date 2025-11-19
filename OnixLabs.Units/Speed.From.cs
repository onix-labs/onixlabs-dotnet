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

public readonly partial struct Speed<T>
{
    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified quectometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromQuectometersPerSecond(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified rontometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromRontometersPerSecond(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified yoctometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromYoctometersPerSecond(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified zeptometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromZeptometersPerSecond(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified attometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromAttometersPerSecond(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified femtometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromFemtometersPerSecond(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified picometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromPicometersPerSecond(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified nanometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromNanometersPerSecond(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified micrometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromMicrometersPerSecond(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified millimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromMillimetersPerSecond(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified centimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromCentimetersPerSecond(T value) => new(value * T.CreateChecked(1e28));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified decimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromDecimetersPerSecond(T value) => new(value * T.CreateChecked(1e29));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified meters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromMetersPerSecond(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified decameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromDecametersPerSecond(T value) => new(value * T.CreateChecked(1e31));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified hectometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromHectometersPerSecond(T value) => new(value * T.CreateChecked(1e32));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified kilometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromKilometersPerSecond(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified megameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromMegametersPerSecond(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified gigameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromGigametersPerSecond(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified terameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromTerametersPerSecond(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified petameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromPetametersPerSecond(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified exameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromExametersPerSecond(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified zettameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromZettametersPerSecond(T value) => new(value * T.CreateChecked(1e51));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified yottameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromYottametersPerSecond(T value) => new(value * T.CreateChecked(1e54));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified ronnameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromRonnametersPerSecond(T value) => new(value * T.CreateChecked(1e57));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified quettameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromQuettametersPerSecond(T value) => new(value * T.CreateChecked(1e60));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified inches per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromInchesPerSecond(T value) => new(value * T.CreateChecked(2.54e28));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified feet per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromFeetPerSecond(T value) => new(value * T.CreateChecked(3.048e29));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified kilometers per hour value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromKilometersPerHour(T value) => new(value * T.CreateChecked(2.777777777777778e29));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified miles per hour value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromMilesPerHour(T value) => new(value * T.CreateChecked(4.4704e29));

    /// <summary>
    /// Creates a new <see cref="Speed{T}"/> instance from the specified knots value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Speed{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Speed{T}"/> instance from the specified value.</returns>
    public static Speed<T> FromKnots(T value) => new(value * T.CreateChecked(5.144444444444445e29));
}
