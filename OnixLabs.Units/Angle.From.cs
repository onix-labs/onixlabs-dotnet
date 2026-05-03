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

public readonly partial struct Angle<T>
{
    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Quectoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromQuectoradians(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Rontoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromRontoradians(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Yoctoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromYoctoradians(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Zeptoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromZeptoradians(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Attoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromAttoradians(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Femtoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromFemtoradians(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Picoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromPicoradians(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Nanoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromNanoradians(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Microradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromMicroradians(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Milliradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromMilliradians(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Centiradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromCentiradians(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Deciradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromDeciradians(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Radians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromRadians(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Decaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromDecaradians(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Hectoradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromHectoradians(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Kiloradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromKiloradians(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Megaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromMegaradians(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Gigaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromGigaradians(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Teraradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromTeraradians(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Petaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromPetaradians(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Exaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromExaradians(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Zettaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromZettaradians(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Yottaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromYottaradians(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Ronnaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromRonnaradians(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Quettaradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromQuettaradians(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Degrees value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromDegrees(T value) => new(value * T.CreateChecked(1.7453292519943295e28));

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Arcminutes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromArcminutes(T value) => new(value * T.CreateChecked(2.908882086657216e26));

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Arcseconds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromArcseconds(T value) => new(value * T.CreateChecked(4.84813681109536e24));

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Gradians value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromGradians(T value) => new(value * T.CreateChecked(1.5707963267948966e28));

    /// <summary>
    /// Creates a new <see cref="Angle{T}"/> instance from the specified Turns value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Angle{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Angle{T}"/> instance from the specified value.</returns>
    public static Angle<T> FromTurns(T value) => new(value * T.CreateChecked(6.283185307179586e30));
}
