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

public readonly partial struct AmountOfSubstance<T>
{
    /// <inheritdoc/>
    public static AmountOfSubstance<T> Zero => new(T.Zero);

    private const string QuectoMolesSpecifier = "qmol";
    private const string QuectoMolesSymbol = "qmol";

    private const string RontoMolesSpecifier = "rmol";
    private const string RontoMolesSymbol = "rmol";

    private const string YoctoMolesSpecifier = "ymol";
    private const string YoctoMolesSymbol = "ymol";

    private const string ZeptoMolesSpecifier = "zmol";
    private const string ZeptoMolesSymbol = "zmol";

    private const string AttoMolesSpecifier = "amol";
    private const string AttoMolesSymbol = "amol";

    private const string FemtoMolesSpecifier = "fmol";
    private const string FemtoMolesSymbol = "fmol";

    private const string PicoMolesSpecifier = "pmol";
    private const string PicoMolesSymbol = "pmol";

    private const string NanoMolesSpecifier = "nmol";
    private const string NanoMolesSymbol = "nmol";

    private const string MicroMolesSpecifier = "umol";
    private const string MicroMolesSymbol = "µmol";

    private const string MilliMolesSpecifier = "mmol";
    private const string MilliMolesSymbol = "mmol";

    private const string CentiMolesSpecifier = "cmol";
    private const string CentiMolesSymbol = "cmol";

    private const string DeciMolesSpecifier = "dmol";
    private const string DeciMolesSymbol = "dmol";

    private const string MolesSpecifier = "mol";
    private const string MolesSymbol = "mol";

    private const string DecaMolesSpecifier = "damol";
    private const string DecaMolesSymbol = "damol";

    private const string HectoMolesSpecifier = "hmol";
    private const string HectoMolesSymbol = "hmol";

    private const string KiloMolesSpecifier = "kmol";
    private const string KiloMolesSymbol = "kmol";

    private const string MegaMolesSpecifier = "Mmol";
    private const string MegaMolesSymbol = "Mmol";

    private const string GigaMolesSpecifier = "Gmol";
    private const string GigaMolesSymbol = "Gmol";

    private const string TeraMolesSpecifier = "Tmol";
    private const string TeraMolesSymbol = "Tmol";

    private const string PetaMolesSpecifier = "Pmol";
    private const string PetaMolesSymbol = "Pmol";

    private const string ExaMolesSpecifier = "Emol";
    private const string ExaMolesSymbol = "Emol";

    private const string ZettaMolesSpecifier = "Zmol";
    private const string ZettaMolesSymbol = "Zmol";

    private const string YottaMolesSpecifier = "Ymol";
    private const string YottaMolesSymbol = "Ymol";

    private const string RonnaMolesSpecifier = "Rmol";
    private const string RonnaMolesSymbol = "Rmol";

    private const string QuettaMolesSpecifier = "Qmol";
    private const string QuettaMolesSymbol = "Qmol";
}
