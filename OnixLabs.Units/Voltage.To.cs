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

public readonly partial struct Voltage<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoVoltsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoVoltsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoVoltsSpecifier => (QuectoVolts, QuectoVoltsSymbol),
            RontoVoltsSpecifier => (RontoVolts, RontoVoltsSymbol),
            YoctoVoltsSpecifier => (YoctoVolts, YoctoVoltsSymbol),
            ZeptoVoltsSpecifier => (ZeptoVolts, ZeptoVoltsSymbol),
            AttoVoltsSpecifier => (AttoVolts, AttoVoltsSymbol),
            FemtoVoltsSpecifier => (FemtoVolts, FemtoVoltsSymbol),
            PicoVoltsSpecifier => (PicoVolts, PicoVoltsSymbol),
            NanoVoltsSpecifier => (NanoVolts, NanoVoltsSymbol),
            MicroVoltsSpecifier => (MicroVolts, MicroVoltsSymbol),
            MilliVoltsSpecifier => (MilliVolts, MilliVoltsSymbol),
            CentiVoltsSpecifier => (CentiVolts, CentiVoltsSymbol),
            DeciVoltsSpecifier => (DeciVolts, DeciVoltsSymbol),
            VoltsSpecifier => (Volts, VoltsSymbol),
            DecaVoltsSpecifier => (DecaVolts, DecaVoltsSymbol),
            HectoVoltsSpecifier => (HectoVolts, HectoVoltsSymbol),
            KiloVoltsSpecifier => (KiloVolts, KiloVoltsSymbol),
            MegaVoltsSpecifier => (MegaVolts, MegaVoltsSymbol),
            GigaVoltsSpecifier => (GigaVolts, GigaVoltsSymbol),
            TeraVoltsSpecifier => (TeraVolts, TeraVoltsSymbol),
            PetaVoltsSpecifier => (PetaVolts, PetaVoltsSymbol),
            ExaVoltsSpecifier => (ExaVolts, ExaVoltsSymbol),
            ZettaVoltsSpecifier => (ZettaVolts, ZettaVoltsSymbol),
            YottaVoltsSpecifier => (YottaVolts, YottaVoltsSymbol),
            RonnaVoltsSpecifier => (RonnaVolts, RonnaVoltsSymbol),
            QuettaVoltsSpecifier => (QuettaVolts, QuettaVoltsSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qV, rV, yV, zV, aV, fV, pV, nV, uV, mV, cV, dV, " +
                "V, daV, hV, kV, MV, GV, TV, PV, EV, ZV, YV, RV, and QV")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
