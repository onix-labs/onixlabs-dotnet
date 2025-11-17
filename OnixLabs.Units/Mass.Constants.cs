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

public readonly partial struct Mass<T>
{
    /// <summary>
    /// Gets a zero <see cref="Mass{T}"/> value, equal to zero yoctograms.
    /// </summary>
    public static readonly Mass<T> Zero = new(T.Zero);

    private const string YoctoGramsSpecifier = "yg";
    private const string ZeptoGramsSpecifier = "zg";
    private const string AttoGramsSpecifier = "ag";
    private const string FemtoGramsSpecifier = "fg";
    private const string PicoGramsSpecifier = "pg";
    private const string NanoGramsSpecifier = "ng";
    private const string MicroGramsSpecifier = "ug";
    private const string MilliGramsSpecifier = "mg";
    private const string GramsSpecifier = "g";
    private const string KiloGramsSpecifier = "kg";
    private const string MegaGramsSpecifier = "Mg";
    private const string TonneSpecifier = "t";
    private const string GigaGramsSpecifier = "Gg";
    private const string TeraGramsSpecifier = "Tg";
    private const string PetaGramsSpecifier = "Pg";
    private const string ExaGramsSpecifier = "Eg";
    private const string ZettaGramsSpecifier = "Zg";
    private const string YottaGramsSpecifier = "Yg";
    private const string PoundsSpecifier = "lb";
    private const string OuncesSpecifier = "oz";
    private const string StonesSpecifier = "st";
    private const string GrainsSpecifier = "gr";
    private const string ShortTonsSpecifier = "ton";
    private const string LongTonsSpecifier = "lt";
    private const string HundredweightUsSpecifier = "cwtUS";
    private const string HundredweightUkSpecifier = "cwtUK";
    private const string QuartersSpecifier = "qr";
    private const string TroyPoundsSpecifier = "lbt";
    private const string TroyOuncesSpecifier = "ozt";
    private const string PennyweightsSpecifier = "dwt";
}
