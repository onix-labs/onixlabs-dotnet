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

public readonly partial struct Charge<T>
{
    /// <inheritdoc/>
    public static Charge<T> Zero => new(T.Zero);

    private const string QuectoCoulombsSpecifier = "qC";
    private const string QuectoCoulombsSymbol = "qC";

    private const string RontoCoulombsSpecifier = "rC";
    private const string RontoCoulombsSymbol = "rC";

    private const string YoctoCoulombsSpecifier = "yC";
    private const string YoctoCoulombsSymbol = "yC";

    private const string ZeptoCoulombsSpecifier = "zC";
    private const string ZeptoCoulombsSymbol = "zC";

    private const string AttoCoulombsSpecifier = "aC";
    private const string AttoCoulombsSymbol = "aC";

    private const string FemtoCoulombsSpecifier = "fC";
    private const string FemtoCoulombsSymbol = "fC";

    private const string PicoCoulombsSpecifier = "pC";
    private const string PicoCoulombsSymbol = "pC";

    private const string NanoCoulombsSpecifier = "nC";
    private const string NanoCoulombsSymbol = "nC";

    private const string MicroCoulombsSpecifier = "uC";
    private const string MicroCoulombsSymbol = "µC";

    private const string MilliCoulombsSpecifier = "mC";
    private const string MilliCoulombsSymbol = "mC";

    private const string CentiCoulombsSpecifier = "cC";
    private const string CentiCoulombsSymbol = "cC";

    private const string DeciCoulombsSpecifier = "dC";
    private const string DeciCoulombsSymbol = "dC";

    private const string CoulombsSpecifier = "C";
    private const string CoulombsSymbol = "C";

    private const string DecaCoulombsSpecifier = "daC";
    private const string DecaCoulombsSymbol = "daC";

    private const string HectoCoulombsSpecifier = "hC";
    private const string HectoCoulombsSymbol = "hC";

    private const string KiloCoulombsSpecifier = "kC";
    private const string KiloCoulombsSymbol = "kC";

    private const string MegaCoulombsSpecifier = "MC";
    private const string MegaCoulombsSymbol = "MC";

    private const string GigaCoulombsSpecifier = "GC";
    private const string GigaCoulombsSymbol = "GC";

    private const string TeraCoulombsSpecifier = "TC";
    private const string TeraCoulombsSymbol = "TC";

    private const string PetaCoulombsSpecifier = "PC";
    private const string PetaCoulombsSymbol = "PC";

    private const string ExaCoulombsSpecifier = "EC";
    private const string ExaCoulombsSymbol = "EC";

    private const string ZettaCoulombsSpecifier = "ZC";
    private const string ZettaCoulombsSymbol = "ZC";

    private const string YottaCoulombsSpecifier = "YC";
    private const string YottaCoulombsSymbol = "YC";

    private const string RonnaCoulombsSpecifier = "RC";
    private const string RonnaCoulombsSymbol = "RC";

    private const string QuettaCoulombsSpecifier = "QC";
    private const string QuettaCoulombsSymbol = "QC";

    private const string AmpereHoursSpecifier = "Ah";
    private const string AmpereHoursSymbol = "A·h";

    private const string MilliampereHoursSpecifier = "mAh";
    private const string MilliampereHoursSymbol = "mA·h";

    private const string ElementaryChargesSpecifier = "e";
    private const string ElementaryChargesSymbol = "e";
}
