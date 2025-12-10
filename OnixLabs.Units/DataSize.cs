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

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of data size.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct DataSize<T> : IUnit<DataSize<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataSize{T}"/> struct.
    /// </summary>
    /// <param name="value">The data size unit in <see cref="Bits"/>.</param>
    private DataSize(T value) => Bits = value;

    /// <summary>
    /// Gets the data size in bits (b).
    /// </summary>
    public T Bits { get; }

    /// <summary>
    /// Gets the data size in bytes (B).
    /// </summary>
    public T Bytes => Bits / T.CreateChecked(8);

    /// <summary>
    /// Gets the data size in kibibits (Kib).
    /// </summary>
    public T KibiBits => GetBinaryValue(1, false);

    /// <summary>
    /// Gets the data size in kibibytes (KiB).
    /// </summary>
    public T KibiBytes => GetBinaryValue(1, true);

    /// <summary>
    /// Gets the data size in kilobits (Kb).
    /// </summary>
    public T KiloBits => GetMetricValue(1, false);

    /// <summary>
    /// Gets the data size in kilobytes (KB).
    /// </summary>
    public T KiloBytes => GetMetricValue(1, true);

    /// <summary>
    /// Gets the data size in mebibits (Mib).
    /// </summary>
    public T MebiBits => GetBinaryValue(2, false);

    /// <summary>
    /// Gets the data size in mebibytes (MiB).
    /// </summary>
    public T MebiBytes => GetBinaryValue(2, true);

    /// <summary>
    /// Gets the data size in megabits (Mb).
    /// </summary>
    public T MegaBits => GetMetricValue(2, false);

    /// <summary>
    /// Gets the data size in megabytes (MB).
    /// </summary>
    public T MegaBytes => GetMetricValue(2, true);

    /// <summary>
    /// Gets the data size in gibibits (Gib).
    /// </summary>
    public T GibiBits => GetBinaryValue(3, false);

    /// <summary>
    /// Gets the data size in gibibytes (GiB).
    /// </summary>
    public T GibiBytes => GetBinaryValue(3, true);

    /// <summary>
    /// Gets the data size in gigabits (Gb).
    /// </summary>
    public T GigaBits => GetMetricValue(3, false);

    /// <summary>
    /// Gets the data size in gigabytes (GB).
    /// </summary>
    public T GigaBytes => GetMetricValue(3, true);

    /// <summary>
    /// Gets the data size in tebibits (Tib).
    /// </summary>
    public T TebiBits => GetBinaryValue(4, false);

    /// <summary>
    /// Gets the data size in tebibytes (TiB).
    /// </summary>
    public T TebiBytes => GetBinaryValue(4, true);

    /// <summary>
    /// Gets the data size in terabits (Tb).
    /// </summary>
    public T TeraBits => GetMetricValue(4, false);

    /// <summary>
    /// Gets the data size in terabytes (TB).
    /// </summary>
    public T TeraBytes => GetMetricValue(4, true);

    /// <summary>
    /// Gets the data size in pebibits (Pib).
    /// </summary>
    public T PebiBits => GetBinaryValue(5, false);

    /// <summary>
    /// Gets the data size in pebibytes (PiB).
    /// </summary>
    public T PebiBytes => GetBinaryValue(5, true);

    /// <summary>
    /// Gets the data size in petabits (Pb).
    /// </summary>
    public T PetaBits => GetMetricValue(5, false);

    /// <summary>
    /// Gets the data size in petabytes (PB).
    /// </summary>
    public T PetaBytes => GetMetricValue(5, true);

    /// <summary>
    /// Gets the data size in exbibits (Eib).
    /// </summary>
    public T ExbiBits => GetBinaryValue(6, false);

    /// <summary>
    /// Gets the data size in exbibytes (EiB).
    /// </summary>
    public T ExbiBytes => GetBinaryValue(6, true);

    /// <summary>
    /// Gets the data size in exabits (Eb).
    /// </summary>
    public T ExaBits => GetMetricValue(6, false);

    /// <summary>
    /// Gets the data size in exabytes (EB).
    /// </summary>
    public T ExaBytes => GetMetricValue(6, true);

    /// <summary>
    /// Gets the data size in zebibits (Zib).
    /// </summary>
    public T ZebiBits => GetBinaryValue(7, false);

    /// <summary>
    /// Gets the data size in zebibytes (ZiB).
    /// </summary>
    public T ZebiBytes => GetBinaryValue(7, true);

    /// <summary>
    /// Gets the data size in zettabits (Zb).
    /// </summary>
    public T ZettaBits => GetMetricValue(7, false);

    /// <summary>
    /// Gets the data size in zettabytes (ZB).
    /// </summary>
    public T ZettaBytes => GetMetricValue(7, true);

    /// <summary>
    /// Gets the data size in yobibits (Yib).
    /// </summary>
    public T YobiBits => GetBinaryValue(8, false);

    /// <summary>
    /// Gets the data size in yobibytes (YiB).
    /// </summary>
    public T YobiBytes => GetBinaryValue(8, true);

    /// <summary>
    /// Gets the data size in yottabits. (Yb)
    /// </summary>
    public T YottaBits => GetMetricValue(8, false);

    /// <summary>
    /// Gets the data size in yottabytes (YB).
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
