// Copyright © 2020 ONIXLabs
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
using System.Globalization;

namespace OnixLabs.Core.Units;

public readonly partial struct DataSize<T> : IFormattable
{
    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// Valid formats for <see cref="DataSize{T}"/> include:
    /// <list type="table">
    /// <listheader><term>Format</term><description>Description</description></listheader>
    /// <item><term>b</term><description>Bits</description></item>
    /// <item><term>n</term><description>Nibbles</description></item>
    /// <item><term>B</term><description>Bytes</description></item>
    /// <item><term>W</term><description>Words</description></item>
    /// <item><term>DW</term><description>DoubleWords</description></item>
    /// <item><term>QW</term><description>QuadWords</description></item>
    /// <item><term>Kb</term><description>KiloBits</description></item>
    /// <item><term>Kib</term><description>KibiBits</description></item>
    /// <item><term>KB</term><description>KiloBytes</description></item>
    /// <item><term>KiB</term><description>KibiBytes</description></item>
    /// <item><term>Mb</term><description>MegaBits</description></item>
    /// <item><term>Mib</term><description>MebiBits</description></item>
    /// <item><term>MB</term><description>MegaBytes</description></item>
    /// <item><term>MiB</term><description>MebiBytes</description></item>
    /// <item><term>Gb</term><description>GigaBits</description></item>
    /// <item><term>Gib</term><description>GibiBits</description></item>
    /// <item><term>GB</term><description>GigaBytes</description></item>
    /// <item><term>GiB</term><description>GibiBytes</description></item>
    /// <item><term>Tb</term><description>TeraBits</description></item>
    /// <item><term>Tib</term><description>TebiBits</description></item>
    /// <item><term>TB</term><description>TeraBytes</description></item>
    /// <item><term>TiB</term><description>TebiBytes</description></item>
    /// <item><term>Pb</term><description>PetaBits</description></item>
    /// <item><term>Pib</term><description>PebiBits</description></item>
    /// <item><term>PB</term><description>PetaBytes</description></item>
    /// <item><term>PiB</term><description>PebiBytes</description></item>
    /// <item><term>Eb</term><description>ExaBits</description></item>
    /// <item><term>Eib</term><description>ExbiBits</description></item>
    /// <item><term>EB</term><description>ExaBytes</description></item>
    /// <item><term>EiB</term><description>ExbiBytes</description></item>
    /// <item><term>Zb</term><description>ZettaBits</description></item>
    /// <item><term>Zib</term><description>ZebiBits</description></item>
    /// <item><term>ZB</term><description>ZettaBytes</description></item>
    /// <item><term>ZiB</term><description>ZebiBytes</description></item>
    /// <item><term>Yb</term><description>YottaBits</description></item>
    /// <item><term>Yib</term><description>YobiBits</description></item>
    /// <item><term>YB</term><description>YottaBytes</description></item>
    /// <item><term>YiB</term><description>YobiBytes</description></item>
    /// </list>
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        string formatOrDefault = format ?? DefaultFormat;
        IFormatProvider formatProviderOrDefault = formatProvider ?? CultureInfo.CurrentCulture;

        T value = formatOrDefault switch
        {
            "b" => Bits,
            "n" => Nibbles,
            "B" => Bytes,
            "W" => Words,
            "DW" => DoubleWords,
            "QW" => QuadWords,
            "Kb" => KiloBits,
            "Kib" => KibiBits,
            "KB" => KiloBytes,
            "KiB" => KibiBytes,
            "Mb" => MegaBits,
            "Mib" => MebiBits,
            "MB" => MegaBytes,
            "MiB" => MebiBytes,
            "Gb" => GigaBits,
            "Gib" => GibiBits,
            "GB" => GigaBytes,
            "GiB" => GibiBytes,
            "Tb" => TeraBits,
            "Tib" => TebiBits,
            "TB" => TeraBytes,
            "TiB" => TebiBytes,
            "Pb" => PetaBits,
            "Pib" => PebiBits,
            "PB" => PetaBytes,
            "PiB" => PebiBytes,
            "Eb" => ExaBits,
            "Eib" => ExbiBits,
            "EB" => ExaBytes,
            "EiB" => ExbiBytes,
            "Zb" => ZettaBits,
            "Zib" => ZebiBits,
            "ZB" => ZettaBytes,
            "ZiB" => ZebiBytes,
            "Yb" => YottaBits,
            "Yib" => YobiBits,
            "YB" => YottaBytes,
            "YiB" => YobiBytes,
            _ => throw new ArgumentException($"Invalid format specifier: {format}.")
        };

        return $"{value.ToString("R", formatProviderOrDefault)} {formatOrDefault}";
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// Valid formats for <see cref="DataSize{T}"/> include:
    /// <list type="table">
    /// <listheader><term>Format</term><description>Description</description></listheader>
    /// <item><term>b</term><description>Bits</description></item>
    /// <item><term>n</term><description>Nibbles</description></item>
    /// <item><term>B</term><description>Bytes</description></item>
    /// <item><term>W</term><description>Words</description></item>
    /// <item><term>DW</term><description>DoubleWords</description></item>
    /// <item><term>QW</term><description>QuadWords</description></item>
    /// <item><term>Kb</term><description>KiloBits</description></item>
    /// <item><term>Kib</term><description>KibiBits</description></item>
    /// <item><term>KB</term><description>KiloBytes</description></item>
    /// <item><term>KiB</term><description>KibiBytes</description></item>
    /// <item><term>Mb</term><description>MegaBits</description></item>
    /// <item><term>Mib</term><description>MebiBits</description></item>
    /// <item><term>MB</term><description>MegaBytes</description></item>
    /// <item><term>MiB</term><description>MebiBytes</description></item>
    /// <item><term>Gb</term><description>GigaBits</description></item>
    /// <item><term>Gib</term><description>GibiBits</description></item>
    /// <item><term>GB</term><description>GigaBytes</description></item>
    /// <item><term>GiB</term><description>GibiBytes</description></item>
    /// <item><term>Tb</term><description>TeraBits</description></item>
    /// <item><term>Tib</term><description>TebiBits</description></item>
    /// <item><term>TB</term><description>TeraBytes</description></item>
    /// <item><term>TiB</term><description>TebiBytes</description></item>
    /// <item><term>Pb</term><description>PetaBits</description></item>
    /// <item><term>Pib</term><description>PebiBits</description></item>
    /// <item><term>PB</term><description>PetaBytes</description></item>
    /// <item><term>PiB</term><description>PebiBytes</description></item>
    /// <item><term>Eb</term><description>ExaBits</description></item>
    /// <item><term>Eib</term><description>ExbiBits</description></item>
    /// <item><term>EB</term><description>ExaBytes</description></item>
    /// <item><term>EiB</term><description>ExbiBytes</description></item>
    /// <item><term>Zb</term><description>ZettaBits</description></item>
    /// <item><term>Zib</term><description>ZebiBits</description></item>
    /// <item><term>ZB</term><description>ZettaBytes</description></item>
    /// <item><term>ZiB</term><description>ZebiBytes</description></item>
    /// <item><term>Yb</term><description>YottaBits</description></item>
    /// <item><term>Yib</term><description>YobiBits</description></item>
    /// <item><term>YB</term><description>YottaBytes</description></item>
    /// <item><term>YiB</term><description>YobiBytes</description></item>
    /// </list>
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        return ToString(format.ToString(), formatProvider);
    }

    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public override string ToString()
    {
        return ToString(DefaultFormat);
    }
}
