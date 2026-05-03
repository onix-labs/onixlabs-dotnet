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

public readonly partial struct Angle<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoRadiansSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoRadiansSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoRadiansSpecifier => (QuectoRadians, QuectoRadiansSymbol),
            RontoRadiansSpecifier => (RontoRadians, RontoRadiansSymbol),
            YoctoRadiansSpecifier => (YoctoRadians, YoctoRadiansSymbol),
            ZeptoRadiansSpecifier => (ZeptoRadians, ZeptoRadiansSymbol),
            AttoRadiansSpecifier => (AttoRadians, AttoRadiansSymbol),
            FemtoRadiansSpecifier => (FemtoRadians, FemtoRadiansSymbol),
            PicoRadiansSpecifier => (PicoRadians, PicoRadiansSymbol),
            NanoRadiansSpecifier => (NanoRadians, NanoRadiansSymbol),
            MicroRadiansSpecifier => (MicroRadians, MicroRadiansSymbol),
            MilliRadiansSpecifier => (MilliRadians, MilliRadiansSymbol),
            CentiRadiansSpecifier => (CentiRadians, CentiRadiansSymbol),
            DeciRadiansSpecifier => (DeciRadians, DeciRadiansSymbol),
            RadiansSpecifier => (Radians, RadiansSymbol),
            DecaRadiansSpecifier => (DecaRadians, DecaRadiansSymbol),
            HectoRadiansSpecifier => (HectoRadians, HectoRadiansSymbol),
            KiloRadiansSpecifier => (KiloRadians, KiloRadiansSymbol),
            MegaRadiansSpecifier => (MegaRadians, MegaRadiansSymbol),
            GigaRadiansSpecifier => (GigaRadians, GigaRadiansSymbol),
            TeraRadiansSpecifier => (TeraRadians, TeraRadiansSymbol),
            PetaRadiansSpecifier => (PetaRadians, PetaRadiansSymbol),
            ExaRadiansSpecifier => (ExaRadians, ExaRadiansSymbol),
            ZettaRadiansSpecifier => (ZettaRadians, ZettaRadiansSymbol),
            YottaRadiansSpecifier => (YottaRadians, YottaRadiansSymbol),
            RonnaRadiansSpecifier => (RonnaRadians, RonnaRadiansSymbol),
            QuettaRadiansSpecifier => (QuettaRadians, QuettaRadiansSymbol),
            DegreesSpecifier => (Degrees, DegreesSymbol),
            ArcminutesSpecifier => (Arcminutes, ArcminutesSymbol),
            ArcsecondsSpecifier => (Arcseconds, ArcsecondsSymbol),
            GradiansSpecifier => (Gradians, GradiansSymbol),
            TurnsSpecifier => (Turns, TurnsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qrad, rrad, yrad, zrad, arad, frad, prad, nrad, urad, mrad, crad, drad, " +
                "rad, darad, hrad, krad, Mrad, Grad, Trad, Prad, Erad, Zrad, Yrad, Rrad, " +
                "Qrad, deg, arcmin, arcsec, gon, and tr")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
