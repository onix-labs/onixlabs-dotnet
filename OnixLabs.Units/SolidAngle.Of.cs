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

public readonly partial struct SolidAngle<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>sr</c>, <c>msr</c>, <c>sqdeg</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoSteradiansSpecifier => QuectoSteradians,
        RontoSteradiansSpecifier => RontoSteradians,
        YoctoSteradiansSpecifier => YoctoSteradians,
        ZeptoSteradiansSpecifier => ZeptoSteradians,
        AttoSteradiansSpecifier => AttoSteradians,
        FemtoSteradiansSpecifier => FemtoSteradians,
        PicoSteradiansSpecifier => PicoSteradians,
        NanoSteradiansSpecifier => NanoSteradians,
        MicroSteradiansSpecifier => MicroSteradians,
        MilliSteradiansSpecifier => MilliSteradians,
        CentiSteradiansSpecifier => CentiSteradians,
        DeciSteradiansSpecifier => DeciSteradians,
        SteradiansSpecifier => Steradians,
        DecaSteradiansSpecifier => DecaSteradians,
        HectoSteradiansSpecifier => HectoSteradians,
        KiloSteradiansSpecifier => KiloSteradians,
        MegaSteradiansSpecifier => MegaSteradians,
        GigaSteradiansSpecifier => GigaSteradians,
        TeraSteradiansSpecifier => TeraSteradians,
        PetaSteradiansSpecifier => PetaSteradians,
        ExaSteradiansSpecifier => ExaSteradians,
        ZettaSteradiansSpecifier => ZettaSteradians,
        YottaSteradiansSpecifier => YottaSteradians,
        RonnaSteradiansSpecifier => RonnaSteradians,
        QuettaSteradiansSpecifier => QuettaSteradians,
        SquareDegreesSpecifier => SquareDegrees,
        SquareArcminutesSpecifier => SquareArcminutes,
        SquareArcsecondsSpecifier => SquareArcseconds,
        SpatsSpecifier => Spats,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };

    /// <summary>
    /// Gets the display symbol corresponding to the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier whose display symbol should be returned.</param>
    /// <returns>Returns the display symbol corresponding to the specified format specifier, or the specifier itself when no mapping exists.</returns>
    public string SymbolOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoSteradiansSpecifier => QuectoSteradiansSymbol,
        RontoSteradiansSpecifier => RontoSteradiansSymbol,
        YoctoSteradiansSpecifier => YoctoSteradiansSymbol,
        ZeptoSteradiansSpecifier => ZeptoSteradiansSymbol,
        AttoSteradiansSpecifier => AttoSteradiansSymbol,
        FemtoSteradiansSpecifier => FemtoSteradiansSymbol,
        PicoSteradiansSpecifier => PicoSteradiansSymbol,
        NanoSteradiansSpecifier => NanoSteradiansSymbol,
        MicroSteradiansSpecifier => MicroSteradiansSymbol,
        MilliSteradiansSpecifier => MilliSteradiansSymbol,
        CentiSteradiansSpecifier => CentiSteradiansSymbol,
        DeciSteradiansSpecifier => DeciSteradiansSymbol,
        SteradiansSpecifier => SteradiansSymbol,
        DecaSteradiansSpecifier => DecaSteradiansSymbol,
        HectoSteradiansSpecifier => HectoSteradiansSymbol,
        KiloSteradiansSpecifier => KiloSteradiansSymbol,
        MegaSteradiansSpecifier => MegaSteradiansSymbol,
        GigaSteradiansSpecifier => GigaSteradiansSymbol,
        TeraSteradiansSpecifier => TeraSteradiansSymbol,
        PetaSteradiansSpecifier => PetaSteradiansSymbol,
        ExaSteradiansSpecifier => ExaSteradiansSymbol,
        ZettaSteradiansSpecifier => ZettaSteradiansSymbol,
        YottaSteradiansSpecifier => YottaSteradiansSymbol,
        RonnaSteradiansSpecifier => RonnaSteradiansSymbol,
        QuettaSteradiansSpecifier => QuettaSteradiansSymbol,
        SquareDegreesSpecifier => SquareDegreesSymbol,
        SquareArcminutesSpecifier => SquareArcminutesSymbol,
        SquareArcsecondsSpecifier => SquareArcsecondsSymbol,
        SpatsSpecifier => SpatsSymbol,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
