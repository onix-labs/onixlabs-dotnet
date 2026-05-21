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

public readonly partial struct LuminousIntensity<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>m</c>, <c>km</c>, <c>mi</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoCandelasSpecifier => QuectoCandelas,
        RontoCandelasSpecifier => RontoCandelas,
        YoctoCandelasSpecifier => YoctoCandelas,
        ZeptoCandelasSpecifier => ZeptoCandelas,
        AttoCandelasSpecifier => AttoCandelas,
        FemtoCandelasSpecifier => FemtoCandelas,
        PicoCandelasSpecifier => PicoCandelas,
        NanoCandelasSpecifier => NanoCandelas,
        MicroCandelasSpecifier => MicroCandelas,
        MilliCandelasSpecifier => MilliCandelas,
        CentiCandelasSpecifier => CentiCandelas,
        DeciCandelasSpecifier => DeciCandelas,
        CandelasSpecifier => Candelas,
        DecaCandelasSpecifier => DecaCandelas,
        HectoCandelasSpecifier => HectoCandelas,
        KiloCandelasSpecifier => KiloCandelas,
        MegaCandelasSpecifier => MegaCandelas,
        GigaCandelasSpecifier => GigaCandelas,
        TeraCandelasSpecifier => TeraCandelas,
        PetaCandelasSpecifier => PetaCandelas,
        ExaCandelasSpecifier => ExaCandelas,
        ZettaCandelasSpecifier => ZettaCandelas,
        YottaCandelasSpecifier => YottaCandelas,
        RonnaCandelasSpecifier => RonnaCandelas,
        QuettaCandelasSpecifier => QuettaCandelas,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };

    /// <summary>
    /// Gets the display symbol corresponding to the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier whose display symbol should be returned.</param>
    /// <returns>Returns the display symbol corresponding to the specified format specifier, or the specifier itself when no mapping exists.</returns>
    public string SymbolOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoCandelasSpecifier => QuectoCandelasSymbol,
        RontoCandelasSpecifier => RontoCandelasSymbol,
        YoctoCandelasSpecifier => YoctoCandelasSymbol,
        ZeptoCandelasSpecifier => ZeptoCandelasSymbol,
        AttoCandelasSpecifier => AttoCandelasSymbol,
        FemtoCandelasSpecifier => FemtoCandelasSymbol,
        PicoCandelasSpecifier => PicoCandelasSymbol,
        NanoCandelasSpecifier => NanoCandelasSymbol,
        MicroCandelasSpecifier => MicroCandelasSymbol,
        MilliCandelasSpecifier => MilliCandelasSymbol,
        CentiCandelasSpecifier => CentiCandelasSymbol,
        DeciCandelasSpecifier => DeciCandelasSymbol,
        CandelasSpecifier => CandelasSymbol,
        DecaCandelasSpecifier => DecaCandelasSymbol,
        HectoCandelasSpecifier => HectoCandelasSymbol,
        KiloCandelasSpecifier => KiloCandelasSymbol,
        MegaCandelasSpecifier => MegaCandelasSymbol,
        GigaCandelasSpecifier => GigaCandelasSymbol,
        TeraCandelasSpecifier => TeraCandelasSymbol,
        PetaCandelasSpecifier => PetaCandelasSymbol,
        ExaCandelasSpecifier => ExaCandelasSymbol,
        ZettaCandelasSpecifier => ZettaCandelasSymbol,
        YottaCandelasSpecifier => YottaCandelasSymbol,
        RonnaCandelasSpecifier => RonnaCandelasSymbol,
        QuettaCandelasSpecifier => QuettaCandelasSymbol,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
