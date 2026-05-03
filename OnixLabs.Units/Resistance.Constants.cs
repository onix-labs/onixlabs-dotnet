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

namespace OnixLabs.Units;

public readonly partial struct Resistance<T>
{
    /// <inheritdoc/>
    public static Resistance<T> Zero => new(T.Zero);

    private const string QuectoOhmsSpecifier = "qohm";
    private const string QuectoOhmsSymbol = "qΩ";

    private const string RontoOhmsSpecifier = "rohm";
    private const string RontoOhmsSymbol = "rΩ";

    private const string YoctoOhmsSpecifier = "yohm";
    private const string YoctoOhmsSymbol = "yΩ";

    private const string ZeptoOhmsSpecifier = "zohm";
    private const string ZeptoOhmsSymbol = "zΩ";

    private const string AttoOhmsSpecifier = "aohm";
    private const string AttoOhmsSymbol = "aΩ";

    private const string FemtoOhmsSpecifier = "fohm";
    private const string FemtoOhmsSymbol = "fΩ";

    private const string PicoOhmsSpecifier = "pohm";
    private const string PicoOhmsSymbol = "pΩ";

    private const string NanoOhmsSpecifier = "nohm";
    private const string NanoOhmsSymbol = "nΩ";

    private const string MicroOhmsSpecifier = "uohm";
    private const string MicroOhmsSymbol = "µΩ";

    private const string MilliOhmsSpecifier = "mohm";
    private const string MilliOhmsSymbol = "mΩ";

    private const string CentiOhmsSpecifier = "cohm";
    private const string CentiOhmsSymbol = "cΩ";

    private const string DeciOhmsSpecifier = "dohm";
    private const string DeciOhmsSymbol = "dΩ";

    private const string OhmsSpecifier = "ohm";
    private const string OhmsSymbol = "Ω";

    private const string DecaOhmsSpecifier = "daohm";
    private const string DecaOhmsSymbol = "daΩ";

    private const string HectoOhmsSpecifier = "hohm";
    private const string HectoOhmsSymbol = "hΩ";

    private const string KiloOhmsSpecifier = "kohm";
    private const string KiloOhmsSymbol = "kΩ";

    private const string MegaOhmsSpecifier = "Mohm";
    private const string MegaOhmsSymbol = "MΩ";

    private const string GigaOhmsSpecifier = "Gohm";
    private const string GigaOhmsSymbol = "GΩ";

    private const string TeraOhmsSpecifier = "Tohm";
    private const string TeraOhmsSymbol = "TΩ";

    private const string PetaOhmsSpecifier = "Pohm";
    private const string PetaOhmsSymbol = "PΩ";

    private const string ExaOhmsSpecifier = "Eohm";
    private const string ExaOhmsSymbol = "EΩ";

    private const string ZettaOhmsSpecifier = "Zohm";
    private const string ZettaOhmsSymbol = "ZΩ";

    private const string YottaOhmsSpecifier = "Yohm";
    private const string YottaOhmsSymbol = "YΩ";

    private const string RonnaOhmsSpecifier = "Rohm";
    private const string RonnaOhmsSymbol = "RΩ";

    private const string QuettaOhmsSpecifier = "Qohm";
    private const string QuettaOhmsSymbol = "QΩ";
}
