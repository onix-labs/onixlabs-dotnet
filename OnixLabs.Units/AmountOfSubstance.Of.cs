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

public readonly partial struct AmountOfSubstance<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>m</c>, <c>km</c>, <c>mi</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoMolesSpecifier => QuectoMoles,
        RontoMolesSpecifier => RontoMoles,
        YoctoMolesSpecifier => YoctoMoles,
        ZeptoMolesSpecifier => ZeptoMoles,
        AttoMolesSpecifier => AttoMoles,
        FemtoMolesSpecifier => FemtoMoles,
        PicoMolesSpecifier => PicoMoles,
        NanoMolesSpecifier => NanoMoles,
        MicroMolesSpecifier => MicroMoles,
        MilliMolesSpecifier => MilliMoles,
        CentiMolesSpecifier => CentiMoles,
        DeciMolesSpecifier => DeciMoles,
        MolesSpecifier => Moles,
        DecaMolesSpecifier => DecaMoles,
        HectoMolesSpecifier => HectoMoles,
        KiloMolesSpecifier => KiloMoles,
        MegaMolesSpecifier => MegaMoles,
        GigaMolesSpecifier => GigaMoles,
        TeraMolesSpecifier => TeraMoles,
        PetaMolesSpecifier => PetaMoles,
        ExaMolesSpecifier => ExaMoles,
        ZettaMolesSpecifier => ZettaMoles,
        YottaMolesSpecifier => YottaMoles,
        RonnaMolesSpecifier => RonnaMoles,
        QuettaMolesSpecifier => QuettaMoles,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };

    /// <summary>
    /// Gets the display symbol corresponding to the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier whose display symbol should be returned.</param>
    /// <returns>Returns the display symbol corresponding to the specified format specifier, or the specifier itself when no mapping exists.</returns>
    public string SymbolOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        QuectoMolesSpecifier => QuectoMolesSymbol,
        RontoMolesSpecifier => RontoMolesSymbol,
        YoctoMolesSpecifier => YoctoMolesSymbol,
        ZeptoMolesSpecifier => ZeptoMolesSymbol,
        AttoMolesSpecifier => AttoMolesSymbol,
        FemtoMolesSpecifier => FemtoMolesSymbol,
        PicoMolesSpecifier => PicoMolesSymbol,
        NanoMolesSpecifier => NanoMolesSymbol,
        MicroMolesSpecifier => MicroMolesSymbol,
        MilliMolesSpecifier => MilliMolesSymbol,
        CentiMolesSpecifier => CentiMolesSymbol,
        DeciMolesSpecifier => DeciMolesSymbol,
        MolesSpecifier => MolesSymbol,
        DecaMolesSpecifier => DecaMolesSymbol,
        HectoMolesSpecifier => HectoMolesSymbol,
        KiloMolesSpecifier => KiloMolesSymbol,
        MegaMolesSpecifier => MegaMolesSymbol,
        GigaMolesSpecifier => GigaMolesSymbol,
        TeraMolesSpecifier => TeraMolesSymbol,
        PetaMolesSpecifier => PetaMolesSymbol,
        ExaMolesSpecifier => ExaMolesSymbol,
        ZettaMolesSpecifier => ZettaMolesSymbol,
        YottaMolesSpecifier => YottaMolesSymbol,
        RonnaMolesSpecifier => RonnaMolesSymbol,
        QuettaMolesSpecifier => QuettaMolesSymbol,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
