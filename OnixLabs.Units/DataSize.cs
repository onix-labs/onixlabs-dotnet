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
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of data size.
/// </summary>
/// <typeparam name="T">The underlying floating point value.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct DataSize<T> :
    IValueEquatable<DataSize<T>>,
    IValueComparable<DataSize<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    private DataSize(T value) => Bits = value;

    /// <summary>
    /// Gets the data size in bits.
    /// </summary>
    public T Bits { get; }

    /// <summary>
    /// Gets the data size in bytes.
    /// </summary>
    public T Bytes => Bits / T.CreateChecked(8);

    /// <summary>
    /// Gets the data size in kibibits.
    /// </summary>
    public T KibiBits => GetBinaryValue(1, false);

    /// <summary>
    /// Gets the data size in kibibytes.
    /// </summary>
    public T KibiBytes => GetBinaryValue(1, true);

    /// <summary>
    /// Gets the data size in kilobits.
    /// </summary>
    public T KiloBits => GetMetricValue(1, false);

    /// <summary>
    /// Gets the data size in kilobytes.
    /// </summary>
    public T KiloBytes => GetMetricValue(1, true);

    /// <summary>
    /// Gets the data size in mebibits.
    /// </summary>
    public T MebiBits => GetBinaryValue(2, false);

    /// <summary>
    /// Gets the data size in mebibytes.
    /// </summary>
    public T MebiBytes => GetBinaryValue(2, true);

    /// <summary>
    /// Gets the data size in megabits.
    /// </summary>
    public T MegaBits => GetMetricValue(2, false);

    /// <summary>
    /// Gets the data size in megabytes.
    /// </summary>
    public T MegaBytes => GetMetricValue(2, true);

    /// <summary>
    /// Gets the data size in gibibits.
    /// </summary>
    public T GibiBits => GetBinaryValue(3, false);

    /// <summary>
    /// Gets the data size in gibibytes.
    /// </summary>
    public T GibiBytes => GetBinaryValue(3, true);

    /// <summary>
    /// Gets the data size in gigabits.
    /// </summary>
    public T GigaBits => GetMetricValue(3, false);

    /// <summary>
    /// Gets the data size in gigabytes.
    /// </summary>
    public T GigaBytes => GetMetricValue(3, true);

    /// <summary>
    /// Gets the data size in tebibits.
    /// </summary>
    public T TebiBits => GetBinaryValue(4, false);

    /// <summary>
    /// Gets the data size in tebibytes.
    /// </summary>
    public T TebiBytes => GetBinaryValue(4, true);

    /// <summary>
    /// Gets the data size in terabits.
    /// </summary>
    public T TeraBits => GetMetricValue(4, false);

    /// <summary>
    /// Gets the data size in terabytes.
    /// </summary>
    public T TeraBytes => GetMetricValue(4, true);

    /// <summary>
    /// Gets the data size in pebibits.
    /// </summary>
    public T PebiBits => GetBinaryValue(5, false);

    /// <summary>
    /// Gets the data size in pebibytes.
    /// </summary>
    public T PebiBytes => GetBinaryValue(5, true);

    /// <summary>
    /// Gets the data size in petabits.
    /// </summary>
    public T PetaBits => GetMetricValue(5, false);

    /// <summary>
    /// Gets the data size in petabytes.
    /// </summary>
    public T PetaBytes => GetMetricValue(5, true);

    /// <summary>
    /// Gets the data size in exbibits.
    /// </summary>
    public T ExbiBits => GetBinaryValue(6, false);

    /// <summary>
    /// Gets the data size in exbibytes.
    /// </summary>
    public T ExbiBytes => GetBinaryValue(6, true);

    /// <summary>
    /// Gets the data size in exabits.
    /// </summary>
    public T ExaBits => GetMetricValue(6, false);

    /// <summary>
    /// Gets the data size in exabytes.
    /// </summary>
    public T ExaBytes => GetMetricValue(6, true);

    /// <summary>
    /// Gets the data size in zebibits.
    /// </summary>
    public T ZebiBits => GetBinaryValue(7, false);

    /// <summary>
    /// Gets the data size in zebibytes.
    /// </summary>
    public T ZebiBytes => GetBinaryValue(7, true);

    /// <summary>
    /// Gets the data size in zettabits.
    /// </summary>
    public T ZettaBits => GetMetricValue(7, false);

    /// <summary>
    /// Gets the data size in zettabytes.
    /// </summary>
    public T ZettaBytes => GetMetricValue(7, true);

    /// <summary>
    /// Gets the data size in yobibits.
    /// </summary>
    public T YobiBits => GetBinaryValue(8, false);

    /// <summary>
    /// Gets the data size in yobibytes.
    /// </summary>
    public T YobiBytes => GetBinaryValue(8, true);

    /// <summary>
    /// Gets the data size in yottabits.
    /// </summary>
    public T YottaBits => GetMetricValue(8, false);

    /// <summary>
    /// Gets the data size in yottabytes.
    /// </summary>
    public T YottaBytes => GetMetricValue(8, true);

    /// <summary>
    /// Obtains the size represented by <c>Bits</c> converted into a binary scaled unit, based
    /// on the specified power of 1024 and whether the value is expressed in bits or bytes.
    /// </summary>
    /// <param name="power">The exponent applied to 1024 when calculating the divisor.</param>
    /// <param name="isByteValue">Determines whether the calculation should be computed in bites or bytes.</param>
    /// <returns>Returns the size converted to the requested unit.</returns>
    private T GetBinaryValue(int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1024, power));
        return Bits / (isByteValue ? divisor * T.CreateChecked(8) : divisor);
    }

    /// <summary>
    /// Obtains the size represented by <c>Bits</c> converted into a metric scaled unit, based
    /// on the specified power of 1000 and whether the value is expressed in bits or bytes.
    /// </summary>
    /// <param name="power">The exponent applied to 1000 when calculating the divisor.</param>
    /// <param name="isByteValue">Determines whether the calculation should be computed in bites or bytes.</param>
    /// <returns>Returns the size converted to the requested unit.</returns>
    private T GetMetricValue(int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1000, power));
        return Bits / (isByteValue ? divisor * T.CreateChecked(8) : divisor);
    }
}
