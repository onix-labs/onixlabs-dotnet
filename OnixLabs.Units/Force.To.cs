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

public readonly partial struct Force<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoNewtonsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoNewtonsSpecifier);

        (T value, string symbol) = specifier switch
        {
            QuectoNewtonsSpecifier => (QuectoNewtons, QuectoNewtonsSymbol),
            RontoNewtonsSpecifier => (RontoNewtons, RontoNewtonsSymbol),
            YoctoNewtonsSpecifier => (YoctoNewtons, YoctoNewtonsSymbol),
            ZeptoNewtonsSpecifier => (ZeptoNewtons, ZeptoNewtonsSymbol),
            AttoNewtonsSpecifier => (AttoNewtons, AttoNewtonsSymbol),
            FemtoNewtonsSpecifier => (FemtoNewtons, FemtoNewtonsSymbol),
            PicoNewtonsSpecifier => (PicoNewtons, PicoNewtonsSymbol),
            NanoNewtonsSpecifier => (NanoNewtons, NanoNewtonsSymbol),
            MicroNewtonsSpecifier => (MicroNewtons, MicroNewtonsSymbol),
            MilliNewtonsSpecifier => (MilliNewtons, MilliNewtonsSymbol),
            CentiNewtonsSpecifier => (CentiNewtons, CentiNewtonsSymbol),
            DeciNewtonsSpecifier => (DeciNewtons, DeciNewtonsSymbol),
            NewtonsSpecifier => (Newtons, NewtonsSymbol),
            DecaNewtonsSpecifier => (DecaNewtons, DecaNewtonsSymbol),
            HectoNewtonsSpecifier => (HectoNewtons, HectoNewtonsSymbol),
            KiloNewtonsSpecifier => (KiloNewtons, KiloNewtonsSymbol),
            MegaNewtonsSpecifier => (MegaNewtons, MegaNewtonsSymbol),
            GigaNewtonsSpecifier => (GigaNewtons, GigaNewtonsSymbol),
            TeraNewtonsSpecifier => (TeraNewtons, TeraNewtonsSymbol),
            PetaNewtonsSpecifier => (PetaNewtons, PetaNewtonsSymbol),
            ExaNewtonsSpecifier => (ExaNewtons, ExaNewtonsSymbol),
            ZettaNewtonsSpecifier => (ZettaNewtons, ZettaNewtonsSymbol),
            YottaNewtonsSpecifier => (YottaNewtons, YottaNewtonsSymbol),
            RonnaNewtonsSpecifier => (RonnaNewtons, RonnaNewtonsSymbol),
            QuettaNewtonsSpecifier => (QuettaNewtons, QuettaNewtonsSymbol),
            DynesSpecifier => (Dynes, DynesSymbol),
            PoundsForceSpecifier => (PoundsForce, PoundsForceSymbol),
            OuncesForceSpecifier => (OuncesForce, OuncesForceSymbol),
            PoundalsSpecifier => (Poundals, PoundalsSymbol),
            KilogramsForceSpecifier => (KilogramsForce, KilogramsForceSymbol),
            GramsForceSpecifier => (GramsForce, GramsForceSymbol),
            MetricTonsForceSpecifier => (MetricTonsForce, MetricTonsForceSymbol),
            _ => throw ArgumentException.InvalidFormat(format,
                "qN, rN, yN, zN, aN, fN, pN, nN, uN, mN, cN, dN, " +
                "N, daN, hN, kN, MN, GN, TN, PN, EN, ZN, YN, RN, " +
                "QN, dyn, lbf, ozf, pdl, kgf, gf, and tnf")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
