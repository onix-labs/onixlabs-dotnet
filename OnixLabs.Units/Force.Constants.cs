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

public readonly partial struct Force<T>
{
    /// <inheritdoc/>
    public static Force<T> Zero => new(T.Zero);

    private const string QuectoNewtonsSpecifier = "qN";
    private const string QuectoNewtonsSymbol = "qN";

    private const string RontoNewtonsSpecifier = "rN";
    private const string RontoNewtonsSymbol = "rN";

    private const string YoctoNewtonsSpecifier = "yN";
    private const string YoctoNewtonsSymbol = "yN";

    private const string ZeptoNewtonsSpecifier = "zN";
    private const string ZeptoNewtonsSymbol = "zN";

    private const string AttoNewtonsSpecifier = "aN";
    private const string AttoNewtonsSymbol = "aN";

    private const string FemtoNewtonsSpecifier = "fN";
    private const string FemtoNewtonsSymbol = "fN";

    private const string PicoNewtonsSpecifier = "pN";
    private const string PicoNewtonsSymbol = "pN";

    private const string NanoNewtonsSpecifier = "nN";
    private const string NanoNewtonsSymbol = "nN";

    private const string MicroNewtonsSpecifier = "uN";
    private const string MicroNewtonsSymbol = "µN";

    private const string MilliNewtonsSpecifier = "mN";
    private const string MilliNewtonsSymbol = "mN";

    private const string CentiNewtonsSpecifier = "cN";
    private const string CentiNewtonsSymbol = "cN";

    private const string DeciNewtonsSpecifier = "dN";
    private const string DeciNewtonsSymbol = "dN";

    private const string NewtonsSpecifier = "N";
    private const string NewtonsSymbol = "N";

    private const string DecaNewtonsSpecifier = "daN";
    private const string DecaNewtonsSymbol = "daN";

    private const string HectoNewtonsSpecifier = "hN";
    private const string HectoNewtonsSymbol = "hN";

    private const string KiloNewtonsSpecifier = "kN";
    private const string KiloNewtonsSymbol = "kN";

    private const string MegaNewtonsSpecifier = "MN";
    private const string MegaNewtonsSymbol = "MN";

    private const string GigaNewtonsSpecifier = "GN";
    private const string GigaNewtonsSymbol = "GN";

    private const string TeraNewtonsSpecifier = "TN";
    private const string TeraNewtonsSymbol = "TN";

    private const string PetaNewtonsSpecifier = "PN";
    private const string PetaNewtonsSymbol = "PN";

    private const string ExaNewtonsSpecifier = "EN";
    private const string ExaNewtonsSymbol = "EN";

    private const string ZettaNewtonsSpecifier = "ZN";
    private const string ZettaNewtonsSymbol = "ZN";

    private const string YottaNewtonsSpecifier = "YN";
    private const string YottaNewtonsSymbol = "YN";

    private const string RonnaNewtonsSpecifier = "RN";
    private const string RonnaNewtonsSymbol = "RN";

    private const string QuettaNewtonsSpecifier = "QN";
    private const string QuettaNewtonsSymbol = "QN";

    private const string DynesSpecifier = "dyn";
    private const string DynesSymbol = "dyn";

    private const string PoundsForceSpecifier = "lbf";
    private const string PoundsForceSymbol = "lbf";

    private const string OuncesForceSpecifier = "ozf";
    private const string OuncesForceSymbol = "ozf";

    private const string PoundalsSpecifier = "pdl";
    private const string PoundalsSymbol = "pdl";

    private const string KilogramsForceSpecifier = "kgf";
    private const string KilogramsForceSymbol = "kgf";

    private const string GramsForceSpecifier = "gf";
    private const string GramsForceSymbol = "gf";

    private const string MetricTonsForceSpecifier = "tnf";
    private const string MetricTonsForceSymbol = "tnf";
}
