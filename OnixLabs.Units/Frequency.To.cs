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

public readonly partial struct Frequency<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoHertzSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoHertzSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoHertzSpecifier => (QuectoHertz, QuectoHertzSymbol),
            RontoHertzSpecifier => (RontoHertz, RontoHertzSymbol),
            YoctoHertzSpecifier => (YoctoHertz, YoctoHertzSymbol),
            ZeptoHertzSpecifier => (ZeptoHertz, ZeptoHertzSymbol),
            AttoHertzSpecifier => (AttoHertz, AttoHertzSymbol),
            FemtoHertzSpecifier => (FemtoHertz, FemtoHertzSymbol),
            PicoHertzSpecifier => (PicoHertz, PicoHertzSymbol),
            NanoHertzSpecifier => (NanoHertz, NanoHertzSymbol),
            MicroHertzSpecifier => (MicroHertz, MicroHertzSymbol),
            MilliHertzSpecifier => (MilliHertz, MilliHertzSymbol),
            CentiHertzSpecifier => (CentiHertz, CentiHertzSymbol),
            DeciHertzSpecifier => (DeciHertz, DeciHertzSymbol),
            HertzSpecifier => (Hertz, HertzSymbol),
            DecaHertzSpecifier => (DecaHertz, DecaHertzSymbol),
            HectoHertzSpecifier => (HectoHertz, HectoHertzSymbol),
            KiloHertzSpecifier => (KiloHertz, KiloHertzSymbol),
            MegaHertzSpecifier => (MegaHertz, MegaHertzSymbol),
            GigaHertzSpecifier => (GigaHertz, GigaHertzSymbol),
            TeraHertzSpecifier => (TeraHertz, TeraHertzSymbol),
            PetaHertzSpecifier => (PetaHertz, PetaHertzSymbol),
            ExaHertzSpecifier => (ExaHertz, ExaHertzSymbol),
            ZettaHertzSpecifier => (ZettaHertz, ZettaHertzSymbol),
            YottaHertzSpecifier => (YottaHertz, YottaHertzSymbol),
            RonnaHertzSpecifier => (RonnaHertz, RonnaHertzSymbol),
            QuettaHertzSpecifier => (QuettaHertz, QuettaHertzSymbol),
            RevolutionsPerMinuteSpecifier => (RevolutionsPerMinute, RevolutionsPerMinuteSymbol),
            BeatsPerMinuteSpecifier => (BeatsPerMinute, BeatsPerMinuteSymbol),
            RadiansPerSecondSpecifier => (RadiansPerSecond, RadiansPerSecondSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qHz, rHz, yHz, zHz, aHz, fHz, pHz, nHz, uHz, mHz, cHz, dHz, " +
                "Hz, daHz, hHz, kHz, MHz, GHz, THz, PHz, EHz, ZHz, YHz, RHz, " +
                "QHz, rpm, bpm, and radps")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
