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

namespace OnixLabs.Units;

public readonly partial struct Angle<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>m</c>, <c>km</c>, <c>mi</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoRadiansSpecifier => QuectoRadians,
        RontoRadiansSpecifier => RontoRadians,
        YoctoRadiansSpecifier => YoctoRadians,
        ZeptoRadiansSpecifier => ZeptoRadians,
        AttoRadiansSpecifier => AttoRadians,
        FemtoRadiansSpecifier => FemtoRadians,
        PicoRadiansSpecifier => PicoRadians,
        NanoRadiansSpecifier => NanoRadians,
        MicroRadiansSpecifier => MicroRadians,
        MilliRadiansSpecifier => MilliRadians,
        CentiRadiansSpecifier => CentiRadians,
        DeciRadiansSpecifier => DeciRadians,
        RadiansSpecifier => Radians,
        DecaRadiansSpecifier => DecaRadians,
        HectoRadiansSpecifier => HectoRadians,
        KiloRadiansSpecifier => KiloRadians,
        MegaRadiansSpecifier => MegaRadians,
        GigaRadiansSpecifier => GigaRadians,
        TeraRadiansSpecifier => TeraRadians,
        PetaRadiansSpecifier => PetaRadians,
        ExaRadiansSpecifier => ExaRadians,
        ZettaRadiansSpecifier => ZettaRadians,
        YottaRadiansSpecifier => YottaRadians,
        RonnaRadiansSpecifier => RonnaRadians,
        QuettaRadiansSpecifier => QuettaRadians,
        DegreesSpecifier => Degrees,
        ArcminutesSpecifier => Arcminutes,
        ArcsecondsSpecifier => Arcseconds,
        GradiansSpecifier => Gradians,
        TurnsSpecifier => Turns,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };

    /// <summary>
    /// Gets the display symbol corresponding to the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier whose display symbol should be returned.</param>
    /// <returns>Returns the display symbol corresponding to the specified format specifier, or the specifier itself when no mapping exists.</returns>
    public string SymbolOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoRadiansSpecifier => QuectoRadiansSymbol,
        RontoRadiansSpecifier => RontoRadiansSymbol,
        YoctoRadiansSpecifier => YoctoRadiansSymbol,
        ZeptoRadiansSpecifier => ZeptoRadiansSymbol,
        AttoRadiansSpecifier => AttoRadiansSymbol,
        FemtoRadiansSpecifier => FemtoRadiansSymbol,
        PicoRadiansSpecifier => PicoRadiansSymbol,
        NanoRadiansSpecifier => NanoRadiansSymbol,
        MicroRadiansSpecifier => MicroRadiansSymbol,
        MilliRadiansSpecifier => MilliRadiansSymbol,
        CentiRadiansSpecifier => CentiRadiansSymbol,
        DeciRadiansSpecifier => DeciRadiansSymbol,
        RadiansSpecifier => RadiansSymbol,
        DecaRadiansSpecifier => DecaRadiansSymbol,
        HectoRadiansSpecifier => HectoRadiansSymbol,
        KiloRadiansSpecifier => KiloRadiansSymbol,
        MegaRadiansSpecifier => MegaRadiansSymbol,
        GigaRadiansSpecifier => GigaRadiansSymbol,
        TeraRadiansSpecifier => TeraRadiansSymbol,
        PetaRadiansSpecifier => PetaRadiansSymbol,
        ExaRadiansSpecifier => ExaRadiansSymbol,
        ZettaRadiansSpecifier => ZettaRadiansSymbol,
        YottaRadiansSpecifier => YottaRadiansSymbol,
        RonnaRadiansSpecifier => RonnaRadiansSymbol,
        QuettaRadiansSpecifier => QuettaRadiansSymbol,
        DegreesSpecifier => DegreesSymbol,
        ArcminutesSpecifier => ArcminutesSymbol,
        ArcsecondsSpecifier => ArcsecondsSymbol,
        GradiansSpecifier => GradiansSymbol,
        TurnsSpecifier => TurnsSymbol,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
