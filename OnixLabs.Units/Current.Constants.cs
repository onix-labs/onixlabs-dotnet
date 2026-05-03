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

public readonly partial struct Current<T>
{
    /// <inheritdoc/>
    public static Current<T> Zero => new(T.Zero);

    private const string QuectoAmperesSpecifier = "qA";
    private const string QuectoAmperesSymbol = "qA";

    private const string RontoAmperesSpecifier = "rA";
    private const string RontoAmperesSymbol = "rA";

    private const string YoctoAmperesSpecifier = "yA";
    private const string YoctoAmperesSymbol = "yA";

    private const string ZeptoAmperesSpecifier = "zA";
    private const string ZeptoAmperesSymbol = "zA";

    private const string AttoAmperesSpecifier = "aA";
    private const string AttoAmperesSymbol = "aA";

    private const string FemtoAmperesSpecifier = "fA";
    private const string FemtoAmperesSymbol = "fA";

    private const string PicoAmperesSpecifier = "pA";
    private const string PicoAmperesSymbol = "pA";

    private const string NanoAmperesSpecifier = "nA";
    private const string NanoAmperesSymbol = "nA";

    private const string MicroAmperesSpecifier = "uA";
    private const string MicroAmperesSymbol = "µA";

    private const string MilliAmperesSpecifier = "mA";
    private const string MilliAmperesSymbol = "mA";

    private const string CentiAmperesSpecifier = "cA";
    private const string CentiAmperesSymbol = "cA";

    private const string DeciAmperesSpecifier = "dA";
    private const string DeciAmperesSymbol = "dA";

    private const string AmperesSpecifier = "A";
    private const string AmperesSymbol = "A";

    private const string DecaAmperesSpecifier = "daA";
    private const string DecaAmperesSymbol = "daA";

    private const string HectoAmperesSpecifier = "hA";
    private const string HectoAmperesSymbol = "hA";

    private const string KiloAmperesSpecifier = "kA";
    private const string KiloAmperesSymbol = "kA";

    private const string MegaAmperesSpecifier = "MA";
    private const string MegaAmperesSymbol = "MA";

    private const string GigaAmperesSpecifier = "GA";
    private const string GigaAmperesSymbol = "GA";

    private const string TeraAmperesSpecifier = "TA";
    private const string TeraAmperesSymbol = "TA";

    private const string PetaAmperesSpecifier = "PA";
    private const string PetaAmperesSymbol = "PA";

    private const string ExaAmperesSpecifier = "EA";
    private const string ExaAmperesSymbol = "EA";

    private const string ZettaAmperesSpecifier = "ZA";
    private const string ZettaAmperesSymbol = "ZA";

    private const string YottaAmperesSpecifier = "YA";
    private const string YottaAmperesSymbol = "YA";

    private const string RonnaAmperesSpecifier = "RA";
    private const string RonnaAmperesSymbol = "RA";

    private const string QuettaAmperesSpecifier = "QA";
    private const string QuettaAmperesSymbol = "QA";
}
