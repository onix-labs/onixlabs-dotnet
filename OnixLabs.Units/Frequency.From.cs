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
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified yoctohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromYoctoHertz(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified zeptohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromZeptoHertz(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified attohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromAttoHertz(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified femtohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromFemtoHertz(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified picohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromPicoHertz(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified nanohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromNanoHertz(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified microhertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMicroHertz(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified millihertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMilliHertz(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified hertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromHertz(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified kilohertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromKiloHertz(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified megahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromMegaHertz(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified gigahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromGigaHertz(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified terahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromTeraHertz(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified petahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromPetaHertz(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified exahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromExaHertz(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified zettahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromZettaHertz(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified yottahertz value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified value.</returns>
    public static Frequency<T> FromYottaHertz(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Frequency{T}"/> instance from the specified revolutions per minute value.
    /// </summary>
    /// <param name="value">The value, in revolutions per minute, from which to construct a new <see cref="Frequency{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Frequency{T}"/> instance from the specified revolutions per minute value.</returns>
    public static Frequency<T> FromRevolutionsPerMinute(T value) => FromHertz(value / T.CreateChecked(60));
}
