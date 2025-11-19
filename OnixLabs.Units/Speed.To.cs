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
public readonly partial struct Speed<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(QuectoMetersPerSecondSpecifier, CultureInfo.CurrentCulture);

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
    /// <param name="format">The format to use, or an empty span to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoMetersPerSecondSpecifier);

        T value = specifier switch
        {
            QuectoMetersPerSecondSpecifier => QuectoMetersPerSecond,
            RontoMetersPerSecondSpecifier => RontoMetersPerSecond,
            YoctoMetersPerSecondSpecifier => YoctoMetersPerSecond,
            ZeptoMetersPerSecondSpecifier => ZeptoMetersPerSecond,
            AttoMetersPerSecondSpecifier => AttoMetersPerSecond,
            FemtoMetersPerSecondSpecifier => FemtoMetersPerSecond,
            PicoMetersPerSecondSpecifier => PicoMetersPerSecond,
            NanoMetersPerSecondSpecifier => NanoMetersPerSecond,
            MicroMetersPerSecondSpecifier => MicroMetersPerSecond,
            MilliMetersPerSecondSpecifier => MilliMetersPerSecond,
            CentiMetersPerSecondSpecifier => CentiMetersPerSecond,
            DeciMetersPerSecondSpecifier => DeciMetersPerSecond,
            MetersPerSecondSpecifier => MetersPerSecond,
            DecaMetersPerSecondSpecifier => DecaMetersPerSecond,
            HectoMetersPerSecondSpecifier => HectoMetersPerSecond,
            KiloMetersPerSecondSpecifier => KiloMetersPerSecond,
            MegaMetersPerSecondSpecifier => MegaMetersPerSecond,
            GigaMetersPerSecondSpecifier => GigaMetersPerSecond,
            TeraMetersPerSecondSpecifier => TeraMetersPerSecond,
            PetaMetersPerSecondSpecifier => PetaMetersPerSecond,
            ExaMetersPerSecondSpecifier => ExaMetersPerSecond,
            ZettaMetersPerSecondSpecifier => ZettaMetersPerSecond,
            YottaMetersPerSecondSpecifier => YottaMetersPerSecond,
            RonnaMetersPerSecondSpecifier => RonnaMetersPerSecond,
            QuettaMetersPerSecondSpecifier => QuettaMetersPerSecond,
            InchesPerSecondSpecifier => InchesPerSecond,
            FeetPerSecondSpecifier => FeetPerSecond,
            KilometersPerHourSpecifier => KilometersPerHour,
            MilesPerHourSpecifier => MilesPerHour,
            KnotsSpecifier => Knots,
            _ => throw new ArgumentException(
                $"Format '{format.ToString()}' is invalid. Valid format specifiers are: " +
                "qmps, rmps, ymps, zmps, amps, fmps, pmps, nmps, umps, mmps, cmps, dmps, mps, damps, " +
                "hmps, kmps, Mmps, Gmps, Tmps, Pmps, Emps, Zmps, Ymps, Rmps, Qmps, ips, fps, kmph, mph, kt. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
