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
using System.Globalization;

namespace OnixLabs.Units;

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>The value of the current instance in the default format.</returns>
    public override string ToString() => ToString(BitsSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: BitsSpecifier);

        T value = specifier switch
        {
            BitsSpecifier => Bits,
            BytesSpecifier => Bytes,
            KibiBitsSpecifier => KibiBits,
            KibiBytesSpecifier => KibiBytes,
            KiloBitsSpecifier => KiloBits,
            KiloBytesSpecifier => KiloBytes,
            MebiBitsSpecifier => MebiBits,
            MebiBytesSpecifier => MebiBytes,
            MegaBitsSpecifier => MegaBits,
            MegaBytesSpecifier => MegaBytes,
            GibiBitsSpecifier => GibiBits,
            GibiBytesSpecifier => GibiBytes,
            GigaBitsSpecifier => GigaBits,
            GigaBytesSpecifier => GigaBytes,
            TebiBitsSpecifier => TebiBits,
            TebiBytesSpecifier => TebiBytes,
            TeraBitsSpecifier => TeraBits,
            TeraBytesSpecifier => TeraBytes,
            PebiBitsSpecifier => PebiBits,
            PebiBytesSpecifier => PebiBytes,
            PetaBitsSpecifier => PetaBits,
            PetaBytesSpecifier => PetaBytes,
            ExbiBitsSpecifier => ExbiBits,
            ExbiBytesSpecifier => ExbiBytes,
            ExaBitsSpecifier => ExaBits,
            ExaBytesSpecifier => ExaBytes,
            ZebiBitsSpecifier => ZebiBits,
            ZebiBytesSpecifier => ZebiBytes,
            ZettaBitsSpecifier => ZettaBits,
            ZettaBytesSpecifier => ZettaBytes,
            YobiBitsSpecifier => YobiBits,
            YobiBytesSpecifier => YobiBytes,
            YottaBitsSpecifier => YottaBits,
            YottaBytesSpecifier => YottaBytes,
            _ => throw new ArgumentException(
                $"Format '{format}' is invalid. Valid format specifiers are: " +
                "b, B, Kib, KiB, Kb, KB, Mib, MiB, Mb, MB, Gib, GiB, Gb, GB, " +
                "Tib, TiB, Tb, TB, Pib, PiB, Pb, PB, Eib, EiB, Eb, EB, " +
                "Zib, ZiB, Zb, ZB, Yib, YiB, Yb, YB. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
