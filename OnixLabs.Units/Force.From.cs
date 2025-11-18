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
    /// Creates a new <see cref="Force{T}"/> instance from the specified yoctonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromYoctoNewtons(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified zeptonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromZeptoNewtons(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified attonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromAttoNewtons(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified femtonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromFemtoNewtons(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified piconewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPicoNewtons(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified nanonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromNanoNewtons(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified micronewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMicroNewtons(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified millinewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMilliNewtons(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified newtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromNewtons(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified kilonewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromKiloNewtons(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified meganewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromMegaNewtons(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified giganewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromGigaNewtons(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified teranewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromTeraNewtons(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified petanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPetaNewtons(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified exanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromExaNewtons(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified zettanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromZettaNewtons(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified yottanewtons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromYottaNewtons(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified dynes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromDynes(T value) => new(value * T.CreateChecked(1e19));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified kilogram-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromKilogramForce(T value) => new(value * T.CreateChecked(9.80665e24));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified gram-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromGramForce(T value) => new(value * T.CreateChecked(9.80665e21));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified tonne-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromTonneForce(T value) => new(value * T.CreateChecked(9.80665e27));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified pound-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPoundForce(T value) => new(value * T.CreateChecked(4.4482216152605e24));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified ounce-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromOunceForce(T value) => new(value * T.CreateChecked(2.780138850953781e23));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified poundals value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromPoundals(T value) => new(value * T.CreateChecked(1.38255e23));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified US short ton-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromShortTonForce(T value) => new(value * T.CreateChecked(8.896443230521e27));

    /// <summary>
    /// Creates a new <see cref="Force{T}"/> instance from the specified imperial long ton-force value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Force{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Force{T}"/> instance from the specified value.</returns>
    public static Force<T> FromLongTonForce(T value) => new(value * T.CreateChecked(9.964016418183519e27));
}
