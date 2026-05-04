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

public readonly partial struct Time<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(QuectoSecondsSpecifier);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: QuectoSecondsSpecifier);

        T value = specifier switch
        {
            QuectoSecondsSpecifier => QuectoSeconds,
            RontoSecondsSpecifier => RontoSeconds,
            YoctoSecondsSpecifier => YoctoSeconds,
            ZeptoSecondsSpecifier => ZeptoSeconds,
            AttoSecondsSpecifier => AttoSeconds,
            FemtoSecondsSpecifier => FemtoSeconds,
            PicoSecondsSpecifier => PicoSeconds,
            NanoSecondsSpecifier => NanoSeconds,
            MicroSecondsSpecifier => MicroSeconds,
            MilliSecondsSpecifier => MilliSeconds,
            CentiSecondsSpecifier => CentiSeconds,
            DeciSecondsSpecifier => DeciSeconds,
            SecondsSpecifier => Seconds,
            DecaSecondsSpecifier => DecaSeconds,
            HectoSecondsSpecifier => HectoSeconds,
            KiloSecondsSpecifier => KiloSeconds,
            MegaSecondsSpecifier => MegaSeconds,
            GigaSecondsSpecifier => GigaSeconds,
            TeraSecondsSpecifier => TeraSeconds,
            PetaSecondsSpecifier => PetaSeconds,
            ExaSecondsSpecifier => ExaSeconds,
            ZettaSecondsSpecifier => ZettaSeconds,
            YottaSecondsSpecifier => YottaSeconds,
            RonnaSecondsSpecifier => RonnaSeconds,
            QuettaSecondsSpecifier => QuettaSeconds,
            MinutesSpecifier => Minutes,
            HoursSpecifier => Hours,
            DaysSpecifier => Days,
            WeeksSpecifier => Weeks,
            JulianYearsSpecifier => JulianYears,
            _ => throw ArgumentException.InvalidFormat(format,
                "qs, rs, ys, zs, as, fs, ps, ns, us, ms, cs, ds, " +
                "s, das, hs, ks, Ms, Gs, Ts, Ps, Es, Zs, Ys, Rs, " +
                "Qs, min, h, d, wk, and yr")
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {specifier}";
    }
}
