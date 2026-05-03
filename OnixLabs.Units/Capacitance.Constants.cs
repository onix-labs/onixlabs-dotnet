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

public readonly partial struct Capacitance<T>
{
    /// <inheritdoc/>
    public static Capacitance<T> Zero => new(T.Zero);

    private const string QuectoFaradsSpecifier = "qF";
    private const string QuectoFaradsSymbol = "qF";

    private const string RontoFaradsSpecifier = "rF";
    private const string RontoFaradsSymbol = "rF";

    private const string YoctoFaradsSpecifier = "yF";
    private const string YoctoFaradsSymbol = "yF";

    private const string ZeptoFaradsSpecifier = "zF";
    private const string ZeptoFaradsSymbol = "zF";

    private const string AttoFaradsSpecifier = "aF";
    private const string AttoFaradsSymbol = "aF";

    private const string FemtoFaradsSpecifier = "fF";
    private const string FemtoFaradsSymbol = "fF";

    private const string PicoFaradsSpecifier = "pF";
    private const string PicoFaradsSymbol = "pF";

    private const string NanoFaradsSpecifier = "nF";
    private const string NanoFaradsSymbol = "nF";

    private const string MicroFaradsSpecifier = "uF";
    private const string MicroFaradsSymbol = "µF";

    private const string MilliFaradsSpecifier = "mF";
    private const string MilliFaradsSymbol = "mF";

    private const string CentiFaradsSpecifier = "cF";
    private const string CentiFaradsSymbol = "cF";

    private const string DeciFaradsSpecifier = "dF";
    private const string DeciFaradsSymbol = "dF";

    private const string FaradsSpecifier = "F";
    private const string FaradsSymbol = "F";

    private const string DecaFaradsSpecifier = "daF";
    private const string DecaFaradsSymbol = "daF";

    private const string HectoFaradsSpecifier = "hF";
    private const string HectoFaradsSymbol = "hF";

    private const string KiloFaradsSpecifier = "kF";
    private const string KiloFaradsSymbol = "kF";

    private const string MegaFaradsSpecifier = "MF";
    private const string MegaFaradsSymbol = "MF";

    private const string GigaFaradsSpecifier = "GF";
    private const string GigaFaradsSymbol = "GF";

    private const string TeraFaradsSpecifier = "TF";
    private const string TeraFaradsSymbol = "TF";

    private const string PetaFaradsSpecifier = "PF";
    private const string PetaFaradsSymbol = "PF";

    private const string ExaFaradsSpecifier = "EF";
    private const string ExaFaradsSymbol = "EF";

    private const string ZettaFaradsSpecifier = "ZF";
    private const string ZettaFaradsSymbol = "ZF";

    private const string YottaFaradsSpecifier = "YF";
    private const string YottaFaradsSymbol = "YF";

    private const string RonnaFaradsSpecifier = "RF";
    private const string RonnaFaradsSymbol = "RF";

    private const string QuettaFaradsSpecifier = "QF";
    private const string QuettaFaradsSymbol = "QF";
}
