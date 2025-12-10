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

using System;

namespace OnixLabs.Units;

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified Bits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromBits(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified Bytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromBytes(T value) => new(value * T.CreateChecked(8));

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified KibiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromKibiBits(T value) => CreateFromBinaryValue(value, 1, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified KibiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromKibiBytes(T value) => CreateFromBinaryValue(value, 1, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified KiloBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromKiloBits(T value) => CreateFromMetricValue(value, 1, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified KiloBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromKiloBytes(T value) => CreateFromMetricValue(value, 1, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified MebiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromMebiBits(T value) => CreateFromBinaryValue(value, 2, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified MebiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromMebiBytes(T value) => CreateFromBinaryValue(value, 2, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified MegaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromMegaBits(T value) => CreateFromMetricValue(value, 2, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified MegaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromMegaBytes(T value) => CreateFromMetricValue(value, 2, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified GibiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromGibiBits(T value) => CreateFromBinaryValue(value, 3, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified GibiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromGibiBytes(T value) => CreateFromBinaryValue(value, 3, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified GigaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromGigaBits(T value) => CreateFromMetricValue(value, 3, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified GigaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromGigaBytes(T value) => CreateFromMetricValue(value, 3, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified TebiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromTebiBits(T value) => CreateFromBinaryValue(value, 4, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified TebiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromTebiBytes(T value) => CreateFromBinaryValue(value, 4, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified TeraBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromTeraBits(T value) => CreateFromMetricValue(value, 4, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified TeraBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromTeraBytes(T value) => CreateFromMetricValue(value, 4, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified PebiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromPebiBits(T value) => CreateFromBinaryValue(value, 5, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified PebiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromPebiBytes(T value) => CreateFromBinaryValue(value, 5, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified PetaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromPetaBits(T value) => CreateFromMetricValue(value, 5, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified PetaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromPetaBytes(T value) => CreateFromMetricValue(value, 5, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ExbiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromExbiBits(T value) => CreateFromBinaryValue(value, 6, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ExbiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromExbiBytes(T value) => CreateFromBinaryValue(value, 6, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ExaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromExaBits(T value) => CreateFromMetricValue(value, 6, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ExaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromExaBytes(T value) => CreateFromMetricValue(value, 6, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ZebiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromZebiBits(T value) => CreateFromBinaryValue(value, 7, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ZebiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromZebiBytes(T value) => CreateFromBinaryValue(value, 7, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ZettaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromZettaBits(T value) => CreateFromMetricValue(value, 7, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified ZettaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromZettaBytes(T value) => CreateFromMetricValue(value, 7, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified YobiBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromYobiBits(T value) => CreateFromBinaryValue(value, 8, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified YobiBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromYobiBytes(T value) => CreateFromBinaryValue(value, 8, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified YottaBits value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromYottaBits(T value) => CreateFromMetricValue(value, 8, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified YottaBytes value.
    /// </summary>
    /// <param name="value">The value from which to construct a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified value.</returns>
    public static DataSize<T> FromYottaBytes(T value) => CreateFromMetricValue(value, 8, true);

    /// <summary>
    /// Obtains the size in bits of the specified IEC (binary) value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="power">The exponent applied to 1024 when calculating the divisor (Ki=1, Mi=2, …).</param>
    /// <param name="isByteValue">If <c>true</c>, <paramref name="value"/> is in bytes; otherwise it is in bits.</param>
    /// <returns>Returns a <see cref="DataSize{T}"/> representing the size in bits.</returns>
    private static DataSize<T> CreateFromBinaryValue(T value, int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1024, power));
        return new DataSize<T>(value * (isByteValue ? divisor * T.CreateChecked(8) : divisor));
    }

    /// <summary>
    /// Obtains the size in bits of the specified SI (metric) value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="power">The exponent applied to 1000 when calculating the divisor (k=1, M=2, …).</param>
    /// <param name="isByteValue">If <c>true</c>, <paramref name="value"/> is in bytes; otherwise it is in bits.</param>
    /// <returns>Returns a <see cref="DataSize{T}"/> representing the size in bits.</returns>
    private static DataSize<T> CreateFromMetricValue(T value, int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1000, power));
        return new DataSize<T>(value * (isByteValue ? divisor * T.CreateChecked(8) : divisor));
    }
}
