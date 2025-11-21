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

// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Power<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(YoctoWattsSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: YoctoWattsSpecifier);

        T value = specifier switch
        {
            YoctoWattsSpecifier => YoctoWatts,
            ZeptoWattsSpecifier => ZeptoWatts,
            AttoWattsSpecifier => AttoWatts,
            FemtoWattsSpecifier => FemtoWatts,
            PicoWattsSpecifier => PicoWatts,
            NanoWattsSpecifier => NanoWatts,
            MicroWattsSpecifier => MicroWatts,
            MilliWattsSpecifier => MilliWatts,
            WattsSpecifier => Watts,
            KiloWattsSpecifier => KiloWatts,
            MegaWattsSpecifier => MegaWatts,
            GigaWattsSpecifier => GigaWatts,
            TeraWattsSpecifier => TeraWatts,
            PetaWattsSpecifier => PetaWatts,
            ExaWattsSpecifier => ExaWatts,
            ZettaWattsSpecifier => ZettaWatts,
            YottaWattsSpecifier => YottaWatts,
            HorsepowerSpecifier => Horsepower,
            MetricHorsepowerSpecifier => MetricHorsepower,
            _ => throw ArgumentException.InvalidFormat(format,
                "yW, zW, aW, fW, pW, nW, uW, mW, W, kW, " +
                "MW, GW, TW, PW, EW, ZW, YW, hp, and hpM")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
