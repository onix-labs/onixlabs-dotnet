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
    /// Creates a new <see cref="DataSize{T}"/> instance from a Bits value.
    /// </summary>
    /// <param name="value">The value to convert from bits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a Bits value.</returns>
    public static DataSize<T> FromBits(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a Bytes value.
    /// </summary>
    /// <param name="value">The value to convert from bytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a Bytes value.</returns>
    public static DataSize<T> FromBytes(T value) => new(value * T.CreateChecked(8));

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a KibiBits value (IEC, <c>2^10</c>).
    /// </summary>
    /// <param name="value">The value to convert from kibibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a KibiBits value.</returns>
    public static DataSize<T> FromKibiBits(T value) => CreateFromBinaryValue(value, 1, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a KibiBytes value (IEC, <c>2^10</c>).
    /// </summary>
    /// <param name="value">The value to convert from kibibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a KibiBytes value.</returns>
    public static DataSize<T> FromKibiBytes(T value) => CreateFromBinaryValue(value, 1, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a KiloBits value (SI, <c>10^3</c>).
    /// </summary>
    /// <param name="value">The value to convert from kilobits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a KiloBits value.</returns>
    public static DataSize<T> FromKiloBits(T value) => CreateFromMetricValue(value, 1, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a KiloBytes value (SI, <c>10^3</c>).
    /// </summary>
    /// <param name="value">The value to convert from kilobytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a KiloBytes value.</returns>
    public static DataSize<T> FromKiloBytes(T value) => CreateFromMetricValue(value, 1, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a MebiBits value (IEC, <c>2^20</c>).
    /// </summary>
    /// <param name="value">The value to convert from mebibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a MebiBits value.</returns>
    public static DataSize<T> FromMebiBits(T value) => CreateFromBinaryValue(value, 2, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a MebiBytes value (IEC, <c>2^20</c>).
    /// </summary>
    /// <param name="value">The value to convert from mebibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a MebiBytes value.</returns>
    public static DataSize<T> FromMebiBytes(T value) => CreateFromBinaryValue(value, 2, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a MegaBits value (SI, <c>10^6</c>).
    /// </summary>
    /// <param name="value">The value to convert from megabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a MegaBits value.</returns>
    public static DataSize<T> FromMegaBits(T value) => CreateFromMetricValue(value, 2, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a MegaBytes value (SI, <c>10^6</c>).
    /// </summary>
    /// <param name="value">The value to convert from megabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a MegaBytes value.</returns>
    public static DataSize<T> FromMegaBytes(T value) => CreateFromMetricValue(value, 2, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a GibiBits value (IEC, <c>2^30</c>).
    /// </summary>
    /// <param name="value">The value to convert from gibibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a GibiBits value.</returns>
    public static DataSize<T> FromGibiBits(T value) => CreateFromBinaryValue(value, 3, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a GibiBytes value (IEC, <c>2^30</c>).
    /// </summary>
    /// <param name="value">The value to convert from gibibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a GibiBytes value.</returns>
    public static DataSize<T> FromGibiBytes(T value) => CreateFromBinaryValue(value, 3, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a GigaBits value (SI, <c>10^9</c>).
    /// </summary>
    /// <param name="value">The value to convert from gigabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a GigaBits value.</returns>
    public static DataSize<T> FromGigaBits(T value) => CreateFromMetricValue(value, 3, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a GigaBytes value (SI, <c>10^9</c>).
    /// </summary>
    /// <param name="value">The value to convert from gigabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a GigaBytes value.</returns>
    public static DataSize<T> FromGigaBytes(T value) => CreateFromMetricValue(value, 3, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a TebiBits value (IEC, <c>2^40</c>).
    /// </summary>
    /// <param name="value">The value to convert from tebibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a TebiBits value.</returns>
    public static DataSize<T> FromTebiBits(T value) => CreateFromBinaryValue(value, 4, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a TebiBytes value (IEC, <c>2^40</c>).
    /// </summary>
    /// <param name="value">The value to convert from tebibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a TebiBytes value.</returns>
    public static DataSize<T> FromTebiBytes(T value) => CreateFromBinaryValue(value, 4, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a TeraBits value (SI, <c>10^12</c>).
    /// </summary>
    /// <param name="value">The value to convert from terabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a TeraBits value.</returns>
    public static DataSize<T> FromTeraBits(T value) => CreateFromMetricValue(value, 4, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a TeraBytes value (SI, <c>10^12</c>).
    /// </summary>
    /// <param name="value">The value to convert from terabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a TeraBytes value.</returns>
    public static DataSize<T> FromTeraBytes(T value) => CreateFromMetricValue(value, 4, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a PebiBits value (IEC, <c>2^50</c>).
    /// </summary>
    /// <param name="value">The value to convert from pebibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a PebiBits value.</returns>
    public static DataSize<T> FromPebiBits(T value) => CreateFromBinaryValue(value, 5, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a PebiBytes value (IEC, <c>2^50</c>).
    /// </summary>
    /// <param name="value">The value to convert from pebibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a PebiBytes value.</returns>
    public static DataSize<T> FromPebiBytes(T value) => CreateFromBinaryValue(value, 5, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a PetaBits value (SI, <c>10^15</c>).
    /// </summary>
    /// <param name="value">The value to convert from petabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a PetaBits value.</returns>
    public static DataSize<T> FromPetaBits(T value) => CreateFromMetricValue(value, 5, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a PetaBytes value (SI, <c>10^15</c>).
    /// </summary>
    /// <param name="value">The value to convert from petabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a PetaBytes value.</returns>
    public static DataSize<T> FromPetaBytes(T value) => CreateFromMetricValue(value, 5, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from an ExbiBits value (IEC, <c>2^60</c>).
    /// </summary>
    /// <param name="value">The value to convert from exbibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from an ExbiBits value.</returns>
    public static DataSize<T> FromExbiBits(T value) => CreateFromBinaryValue(value, 6, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from an ExbiBytes value (IEC, <c>2^60</c>).
    /// </summary>
    /// <param name="value">The value to convert from exbibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from an ExbiBytes value.</returns>
    public static DataSize<T> FromExbiBytes(T value) => CreateFromBinaryValue(value, 6, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from an ExaBits value (SI, <c>10^18</c>).
    /// </summary>
    /// <param name="value">The value to convert from exabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from an ExaBits value.</returns>
    public static DataSize<T> FromExaBits(T value) => CreateFromMetricValue(value, 6, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from an ExaBytes value (SI, <c>10^18</c>).
    /// </summary>
    /// <param name="value">The value to convert from exabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from an ExaBytes value.</returns>
    public static DataSize<T> FromExaBytes(T value) => CreateFromMetricValue(value, 6, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a ZebiBits value (IEC, <c>2^70</c>).
    /// </summary>
    /// <param name="value">The value to convert from zebibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a ZebiBits value.</returns>
    public static DataSize<T> FromZebiBits(T value) => CreateFromBinaryValue(value, 7, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a ZebiBytes value (IEC, <c>2^70</c>).
    /// </summary>
    /// <param name="value">The value to convert from zebibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a ZebiBytes value.</returns>
    public static DataSize<T> FromZebiBytes(T value) => CreateFromBinaryValue(value, 7, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a ZettaBits value (SI, <c>10^21</c>).
    /// </summary>
    /// <param name="value">The value to convert from zettabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a ZettaBits value.</returns>
    public static DataSize<T> FromZettaBits(T value) => CreateFromMetricValue(value, 7, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a ZettaBytes value (SI, <c>10^21</c>).
    /// </summary>
    /// <param name="value">The value to convert from zettabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a ZettaBytes value.</returns>
    public static DataSize<T> FromZettaBytes(T value) => CreateFromMetricValue(value, 7, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a YobiBits value (IEC, <c>2^80</c>).
    /// </summary>
    /// <param name="value">The value to convert from yobibits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a YobiBits value.</returns>
    public static DataSize<T> FromYobiBits(T value) => CreateFromBinaryValue(value, 8, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a YobiBytes value (IEC, <c>2^80</c>).
    /// </summary>
    /// <param name="value">The value to convert from yobibytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a YobiBytes value.</returns>
    public static DataSize<T> FromYobiBytes(T value) => CreateFromBinaryValue(value, 8, true);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a YottaBits value (SI, <c>10^24</c>).
    /// </summary>
    /// <param name="value">The value to convert from yottabits.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a YottaBits value.</returns>
    public static DataSize<T> FromYottaBits(T value) => CreateFromMetricValue(value, 8, false);

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from a YottaBytes value (SI, <c>10^24</c>).
    /// </summary>
    /// <param name="value">The value to convert from yottabytes.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from a YottaBytes value.</returns>
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
