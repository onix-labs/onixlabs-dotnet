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

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Mass<T>
{
    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Yoctograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromYoctograms(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Zeptograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromZeptograms(T value) => new(value * T.CreateChecked(1e3));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Attograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromAttograms(T value) => new(value * T.CreateChecked(1e6));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Femtograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromFemtograms(T value) => new(value * T.CreateChecked(1e9));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Picograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPicograms(T value) => new(value * T.CreateChecked(1e12));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Nanograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromNanograms(T value) => new(value * T.CreateChecked(1e15));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Micrograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMicrograms(T value) => new(value * T.CreateChecked(1e18));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Milligrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMilligrams(T value) => new(value * T.CreateChecked(1e21));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Grams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGrams(T value) => new(value * T.CreateChecked(1e24));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Kilograms value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromKilograms(T value) => new(value * T.CreateChecked(1e27));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Megagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMegagrams(T value) => new(value * T.CreateChecked(1e30));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Tonnes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTonnes(T value) => FromMegagrams(value);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Gigagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGigagrams(T value) => new(value * T.CreateChecked(1e33));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Teragrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTeragrams(T value) => new(value * T.CreateChecked(1e36));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Petagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPetagrams(T value) => new(value * T.CreateChecked(1e39));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Exagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromExagrams(T value) => new(value * T.CreateChecked(1e42));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Zettagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromZettagrams(T value) => new(value * T.CreateChecked(1e45));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Yottagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromYottagrams(T value) => new(value * T.CreateChecked(1e48));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Pounds value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPounds(T value) => new(value * T.CreateChecked(4.5359237e26));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromOunces(T value) => new(value * T.CreateChecked(2.8349523125e25));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Stones value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromStones(T value) => new(value * T.CreateChecked(6.35029318e27));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Grains value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGrains(T value) => new(value * T.CreateChecked(6.479891e22));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified ShortTons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromShortTons(T value) => new(value * T.CreateChecked(9.0718474e29));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified LongTons value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromLongTons(T value) => new(value * T.CreateChecked(1.0160469088e30));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified HundredweightUs value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromHundredweightUs(T value) => new(value * T.CreateChecked(4.5359237e28));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified HundredweightUk value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromHundredweightUk(T value) => new(value * T.CreateChecked(5.080234544e28));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Quarters value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromQuarters(T value) => new(value * T.CreateChecked(1.270058636e28));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified TroyPounds value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTroyPounds(T value) => new(value * T.CreateChecked(3.732417216e26));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified TroyOunces value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTroyOunces(T value) => new(value * T.CreateChecked(3.11034768e25));

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Pennyweights value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a newly created <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPennyweights(T value) => new(value * T.CreateChecked(1.55517384e24));
}
