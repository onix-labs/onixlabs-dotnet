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

public readonly partial struct Mass<T>
{
    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Quectograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromQuectograms(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Rontograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromRontograms(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Yoctograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromYoctograms(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Zeptograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromZeptograms(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Attograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromAttograms(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Femtograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromFemtograms(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Picograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPicograms(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Nanograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromNanograms(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Micrograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMicrograms(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Milligrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMilligrams(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Centigrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromCentigrams(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Decigrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromDecigrams(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Grams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGrams(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Decagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromDecagrams(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Hectograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromHectograms(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Kilograms value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromKilograms(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Megagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromMegagrams(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Gigagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGigagrams(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Teragrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTeragrams(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Petagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPetagrams(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Exagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromExagrams(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Zettagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromZettagrams(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Yottagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromYottagrams(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Ronnagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromRonnagrams(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Quettagrams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromQuettagrams(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Tonnes value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromTonnes(T value) => new(value * QuectogramsPerTonne);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Ounces value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromOunces(T value) => new(value * QuectogramsPerOunce);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Pounds value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromPounds(T value) => new(value * QuectogramsPerPound);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Stones value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromStones(T value) => new(value * QuectogramsPerStone);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Short Tons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromShortTons(T value) => new(value * QuectogramsPerShortTon);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Long Tons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromLongTons(T value) => new(value * QuectogramsPerLongTon);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Carats value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromCarats(T value) => new(value * QuectogramsPerCarat);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Grains value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromGrains(T value) => new(value * QuectogramsPerGrain);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Drams value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromDrams(T value) => new(value * QuectogramsPerDram);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Slugs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromSlugs(T value) => new(value * QuectogramsPerSlug);

    /// <summary>
    /// Creates a new <see cref="Mass{T}"/> instance from the specified Daltons value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Mass{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Mass{T}"/> instance from the specified value.</returns>
    public static Mass<T> FromDaltons(T value) => new(value * QuectogramsPerDalton);
}
