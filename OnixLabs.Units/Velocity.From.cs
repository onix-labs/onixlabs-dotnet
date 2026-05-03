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

public readonly partial struct Velocity<T>
{
    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Quectometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromQuectometersPerSecond(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Rontometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromRontometersPerSecond(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Yoctometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromYoctometersPerSecond(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Zeptometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromZeptometersPerSecond(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Attometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromAttometersPerSecond(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Femtometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromFemtometersPerSecond(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Picometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromPicometersPerSecond(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Nanometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromNanometersPerSecond(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Micrometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMicrometersPerSecond(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Millimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMillimetersPerSecond(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Centimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromCentimetersPerSecond(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Decimeters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromDecimetersPerSecond(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Meters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMetersPerSecond(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Decameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromDecametersPerSecond(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Hectometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromHectometersPerSecond(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Kilometers per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromKilometersPerSecond(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Megameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMegametersPerSecond(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Gigameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromGigametersPerSecond(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Terameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromTerametersPerSecond(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Petameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromPetametersPerSecond(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Exameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromExametersPerSecond(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Zettameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromZettametersPerSecond(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Yottameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromYottametersPerSecond(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Ronnameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromRonnametersPerSecond(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Quettameters per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromQuettametersPerSecond(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Kilometers per hour value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromKilometersPerHour(T value) => new(value * T.CreateChecked(1e30) / T.CreateChecked(3.6));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Miles per hour value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMilesPerHour(T value) => new(value * T.CreateChecked(4.4704e29));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Feet per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromFeetPerSecond(T value) => new(value * T.CreateChecked(3.048e29));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Knots value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromKnots(T value) => new(value * T.CreateChecked(1852e30) / T.CreateChecked(3600));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Inches per second value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromInchesPerSecond(T value) => new(value * T.CreateChecked(2.54e28));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified Mach value, based on the standard atmosphere speed of sound at sea level (340.29 m/s).
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromMach(T value) => new(value * T.CreateChecked(3.4029e32));

    /// <summary>
    /// Creates a new <see cref="Velocity{T}"/> instance from the specified value as a multiple of the speed of light in vacuum (c = 299,792,458 m/s).
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Velocity{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Velocity{T}"/> instance from the specified value.</returns>
    public static Velocity<T> FromSpeedOfLight(T value) => new(value * T.CreateChecked(2.99792458e38));
}
