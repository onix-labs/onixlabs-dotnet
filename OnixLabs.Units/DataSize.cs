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
    /// <remarks>
    /// The format specifier for this value is b.
    /// </remarks>
    public T Bits { get; }

    /// <summary>
    /// Gets the data size in bytes (B).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is B.
    /// </remarks>
    public T Bytes => Bits / T.CreateChecked(8);

    /// <summary>
    /// Gets the data size in kibibits (Kib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Kib.
    /// </remarks>
    public T KibiBits => GetBinaryValue(1, false);

    /// <summary>
    /// Gets the data size in kibibytes (KiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is KiB.
    /// </remarks>
    public T KibiBytes => GetBinaryValue(1, true);

    /// <summary>
    /// Gets the data size in kilobits (Kb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Kb.
    /// </remarks>
    public T KiloBits => GetMetricValue(1, false);

    /// <summary>
    /// Gets the data size in kilobytes (KB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is KB.
    /// </remarks>
    public T KiloBytes => GetMetricValue(1, true);

    /// <summary>
    /// Gets the data size in mebibits (Mib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mib.
    /// </remarks>
    public T MebiBits => GetBinaryValue(2, false);

    /// <summary>
    /// Gets the data size in mebibytes (MiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MiB.
    /// </remarks>
    public T MebiBytes => GetBinaryValue(2, true);

    /// <summary>
    /// Gets the data size in megabits (Mb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Mb.
    /// </remarks>
    public T MegaBits => GetMetricValue(2, false);

    /// <summary>
    /// Gets the data size in megabytes (MB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MB.
    /// </remarks>
    public T MegaBytes => GetMetricValue(2, true);

    /// <summary>
    /// Gets the data size in gibibits (Gib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gib.
    /// </remarks>
    public T GibiBits => GetBinaryValue(3, false);

    /// <summary>
    /// Gets the data size in gibibytes (GiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GiB.
    /// </remarks>
    public T GibiBytes => GetBinaryValue(3, true);

    /// <summary>
    /// Gets the data size in gigabits (Gb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gb.
    /// </remarks>
    public T GigaBits => GetMetricValue(3, false);

    /// <summary>
    /// Gets the data size in gigabytes (GB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GB.
    /// </remarks>
    public T GigaBytes => GetMetricValue(3, true);

    /// <summary>
    /// Gets the data size in tebibits (Tib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tib.
    /// </remarks>
    public T TebiBits => GetBinaryValue(4, false);

    /// <summary>
    /// Gets the data size in tebibytes (TiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TiB.
    /// </remarks>
    public T TebiBytes => GetBinaryValue(4, true);

    /// <summary>
    /// Gets the data size in terabits (Tb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Tb.
    /// </remarks>
    public T TeraBits => GetMetricValue(4, false);

    /// <summary>
    /// Gets the data size in terabytes (TB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TB.
    /// </remarks>
    public T TeraBytes => GetMetricValue(4, true);

    /// <summary>
    /// Gets the data size in pebibits (Pib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pib.
    /// </remarks>
    public T PebiBits => GetBinaryValue(5, false);

    /// <summary>
    /// Gets the data size in pebibytes (PiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PiB.
    /// </remarks>
    public T PebiBytes => GetBinaryValue(5, true);

    /// <summary>
    /// Gets the data size in petabits (Pb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Pb.
    /// </remarks>
    public T PetaBits => GetMetricValue(5, false);

    /// <summary>
    /// Gets the data size in petabytes (PB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PB.
    /// </remarks>
    public T PetaBytes => GetMetricValue(5, true);

    /// <summary>
    /// Gets the data size in exbibits (Eib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Eib.
    /// </remarks>
    public T ExbiBits => GetBinaryValue(6, false);

    /// <summary>
    /// Gets the data size in exbibytes (EiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EiB.
    /// </remarks>
    public T ExbiBytes => GetBinaryValue(6, true);

    /// <summary>
    /// Gets the data size in exabits (Eb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Eb.
    /// </remarks>
    public T ExaBits => GetMetricValue(6, false);

    /// <summary>
    /// Gets the data size in exabytes (EB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EB.
    /// </remarks>
    public T ExaBytes => GetMetricValue(6, true);

    /// <summary>
    /// Gets the data size in zebibits (Zib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zib.
    /// </remarks>
    public T ZebiBits => GetBinaryValue(7, false);

    /// <summary>
    /// Gets the data size in zebibytes (ZiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZiB.
    /// </remarks>
    public T ZebiBytes => GetBinaryValue(7, true);

    /// <summary>
    /// Gets the data size in zettabits (Zb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zb.
    /// </remarks>
    public T ZettaBits => GetMetricValue(7, false);

    /// <summary>
    /// Gets the data size in zettabytes (ZB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZB.
    /// </remarks>
    public T ZettaBytes => GetMetricValue(7, true);

    /// <summary>
    /// Gets the data size in yobibits (Yib).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Yib.
    /// </remarks>
    public T YobiBits => GetBinaryValue(8, false);

    /// <summary>
    /// Gets the data size in yobibytes (YiB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YiB.
    /// </remarks>
    public T YobiBytes => GetBinaryValue(8, true);

    /// <summary>
    /// Gets the data size in yottabits (Yb).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Yb.
    /// </remarks>
    public T YottaBits => GetMetricValue(8, false);

    /// <summary>
    /// Gets the data size in yottabytes (YB).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YB.
    /// </remarks>
    public T YottaBytes => GetMetricValue(8, true);

    /// <summary>
    /// Obtains the size represented by <see cref="Bits"/> converted into a binary scaled unit, based
    /// on the specified power of 1024 and whether the value is expressed in bits or bytes.
    /// </summary>
    /// <param name="power">The exponent applied to 1024 when calculating the divisor.</param>
    /// <param name="isByteValue">Determines whether the calculation should be computed in bits or bytes.</param>
    /// <returns>Returns the size converted to the requested unit.</returns>
    private T GetBinaryValue(int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1024, power));
        return Bits / (isByteValue ? divisor * T.CreateChecked(8) : divisor);
    }

    /// <summary>
    /// Obtains the size represented by <see cref="Bits"/> converted into a metric scaled unit, based
    /// on the specified power of 1000 and whether the value is expressed in bits or bytes.
    /// </summary>
    /// <param name="power">The exponent applied to 1000 when calculating the divisor.</param>
    /// <param name="isByteValue">Determines whether the calculation should be computed in bits or bytes.</param>
    /// <returns>Returns the size converted to the requested unit.</returns>
    private T GetMetricValue(int power, bool isByteValue)
    {
        T divisor = T.CreateChecked(Math.Pow(1000, power));
        return Bits / (isByteValue ? divisor * T.CreateChecked(8) : divisor);
    }
}
