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
public readonly partial struct Force<T>
{
    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(YoctoNewtonsSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: YoctoNewtonsSpecifier);

        T value = specifier switch
        {
            YoctoNewtonsSpecifier => YoctoNewtons,
            ZeptoNewtonsSpecifier => ZeptoNewtons,
            AttoNewtonsSpecifier => AttoNewtons,
            FemtoNewtonsSpecifier => FemtoNewtons,
            PicoNewtonsSpecifier => PicoNewtons,
            NanoNewtonsSpecifier => NanoNewtons,
            MicroNewtonsSpecifier => MicroNewtons,
            MilliNewtonsSpecifier => MilliNewtons,
            NewtonsSpecifier => Newtons,
            KiloNewtonsSpecifier => KiloNewtons,
            MegaNewtonsSpecifier => MegaNewtons,
            GigaNewtonsSpecifier => GigaNewtons,
            TeraNewtonsSpecifier => TeraNewtons,
            PetaNewtonsSpecifier => PetaNewtons,
            ExaNewtonsSpecifier => ExaNewtons,
            ZettaNewtonsSpecifier => ZettaNewtons,
            YottaNewtonsSpecifier => YottaNewtons,
            DynesSpecifier => Dynes,
            KilogramForceSpecifier => KilogramForce,
            GramForceSpecifier => GramForce,
            TonneForceSpecifier => TonneForce,
            PoundForceSpecifier => PoundForce,
            OunceForceSpecifier => OunceForce,
            PoundalsSpecifier => Poundals,
            ShortTonForceSpecifier => ShortTonForce,
            LongTonForceSpecifier => LongTonForce,
            _ => throw ArgumentException.InvalidFormat(format,
                "yN, zN, aN, fN, pN, nN, uN, mN, N, " +
                "kN, MN, GN, TN, PN, EN, ZN, YN, dyn, " +
                "kgf, gf, tf, lbf, ozf, pdl, tonf, and ltf")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
