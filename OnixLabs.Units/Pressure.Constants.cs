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

public readonly partial struct Pressure<T>
{
    /// <summary>
    /// Gets a zero <see cref="Pressure{T}"/> value, equal to zero quectopascals.
    /// </summary>
    public static readonly Pressure<T> Zero = new(T.Zero);

    private const string QuectoPascalsSpecifier = "qPa";
    private const string RontoPascalsSpecifier = "rPa";
    private const string YoctoPascalsSpecifier = "yPa";
    private const string ZeptoPascalsSpecifier = "zPa";
    private const string AttoPascalsSpecifier = "aPa";
    private const string FemtoPascalsSpecifier = "fPa";
    private const string PicoPascalsSpecifier = "pPa";
    private const string NanoPascalsSpecifier = "nPa";
    private const string MicroPascalsSpecifier = "uPa";
    private const string MilliPascalsSpecifier = "mPa";
    private const string CentiPascalsSpecifier = "cPa";
    private const string DeciPascalsSpecifier = "dPa";
    private const string PascalsSpecifier = "Pa";
    private const string DecaPascalsSpecifier = "daPa";
    private const string HectoPascalsSpecifier = "hPa";
    private const string KiloPascalsSpecifier = "kPa";
    private const string MegaPascalsSpecifier = "MPa";
    private const string GigaPascalsSpecifier = "GPa";
    private const string TeraPascalsSpecifier = "TPa";
    private const string PetaPascalsSpecifier = "PPa";
    private const string ExaPascalsSpecifier = "EPa";
    private const string ZettaPascalsSpecifier = "ZPa";
    private const string YottaPascalsSpecifier = "YPa";
    private const string RonnaPascalsSpecifier = "RPa";
    private const string QuettaPascalsSpecifier = "QPa";
    private const string BarsSpecifier = "bar";
    private const string MillibarsSpecifier = "mbar";
    private const string AtmospheresSpecifier = "atm";
    private const string TechnicalAtmospheresSpecifier = "at";
    private const string TorrSpecifier = "Torr";
    private const string MillimetersOfMercurySpecifier = "mmHg";
    private const string InchesOfMercurySpecifier = "inHg";
    private const string PoundsPerSquareInchSpecifier = "psi";
    private const string PoundsPerSquareFootSpecifier = "psf";
    private const string BaryeSpecifier = "Ba";
    private const string MillimetersOfWaterColumnSpecifier = "mmH2O";
    private const string InchesOfWaterColumnSpecifier = "inH2O";
}
