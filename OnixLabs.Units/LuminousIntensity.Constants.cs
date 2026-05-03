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

public readonly partial struct LuminousIntensity<T>
{
    /// <inheritdoc/>
    public static LuminousIntensity<T> Zero => new(T.Zero);

    private const string QuectoCandelasSpecifier = "qcd";
    private const string QuectoCandelasSymbol = "qcd";

    private const string RontoCandelasSpecifier = "rcd";
    private const string RontoCandelasSymbol = "rcd";

    private const string YoctoCandelasSpecifier = "ycd";
    private const string YoctoCandelasSymbol = "ycd";

    private const string ZeptoCandelasSpecifier = "zcd";
    private const string ZeptoCandelasSymbol = "zcd";

    private const string AttoCandelasSpecifier = "acd";
    private const string AttoCandelasSymbol = "acd";

    private const string FemtoCandelasSpecifier = "fcd";
    private const string FemtoCandelasSymbol = "fcd";

    private const string PicoCandelasSpecifier = "pcd";
    private const string PicoCandelasSymbol = "pcd";

    private const string NanoCandelasSpecifier = "ncd";
    private const string NanoCandelasSymbol = "ncd";

    private const string MicroCandelasSpecifier = "ucd";
    private const string MicroCandelasSymbol = "µcd";

    private const string MilliCandelasSpecifier = "mcd";
    private const string MilliCandelasSymbol = "mcd";

    private const string CentiCandelasSpecifier = "ccd";
    private const string CentiCandelasSymbol = "ccd";

    private const string DeciCandelasSpecifier = "dcd";
    private const string DeciCandelasSymbol = "dcd";

    private const string CandelasSpecifier = "cd";
    private const string CandelasSymbol = "cd";

    private const string DecaCandelasSpecifier = "dacd";
    private const string DecaCandelasSymbol = "dacd";

    private const string HectoCandelasSpecifier = "hcd";
    private const string HectoCandelasSymbol = "hcd";

    private const string KiloCandelasSpecifier = "kcd";
    private const string KiloCandelasSymbol = "kcd";

    private const string MegaCandelasSpecifier = "Mcd";
    private const string MegaCandelasSymbol = "Mcd";

    private const string GigaCandelasSpecifier = "Gcd";
    private const string GigaCandelasSymbol = "Gcd";

    private const string TeraCandelasSpecifier = "Tcd";
    private const string TeraCandelasSymbol = "Tcd";

    private const string PetaCandelasSpecifier = "Pcd";
    private const string PetaCandelasSymbol = "Pcd";

    private const string ExaCandelasSpecifier = "Ecd";
    private const string ExaCandelasSymbol = "Ecd";

    private const string ZettaCandelasSpecifier = "Zcd";
    private const string ZettaCandelasSymbol = "Zcd";

    private const string YottaCandelasSpecifier = "Ycd";
    private const string YottaCandelasSymbol = "Ycd";

    private const string RonnaCandelasSpecifier = "Rcd";
    private const string RonnaCandelasSymbol = "Rcd";

    private const string QuettaCandelasSpecifier = "Qcd";
    private const string QuettaCandelasSymbol = "Qcd";
}
