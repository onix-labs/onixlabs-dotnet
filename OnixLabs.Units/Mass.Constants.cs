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
    /// <inheritdoc/>
    public static Mass<T> Zero => new(T.Zero);

    private const string QuectoGramsSpecifier = "qg";
    private const string RontoGramsSpecifier = "rg";
    private const string YoctoGramsSpecifier = "yg";
    private const string ZeptoGramsSpecifier = "zg";
    private const string AttoGramsSpecifier = "ag";
    private const string FemtoGramsSpecifier = "fg";
    private const string PicoGramsSpecifier = "pg";
    private const string NanoGramsSpecifier = "ng";
    private const string MicroGramsSpecifier = "ug";
    private const string MilliGramsSpecifier = "mg";
    private const string CentiGramsSpecifier = "cg";
    private const string DeciGramsSpecifier = "dg";
    private const string GramsSpecifier = "g";
    private const string DecaGramsSpecifier = "dag";
    private const string HectoGramsSpecifier = "hg";
    private const string KiloGramsSpecifier = "kg";
    private const string MegaGramsSpecifier = "Mg";
    private const string GigaGramsSpecifier = "Gg";
    private const string TeraGramsSpecifier = "Tg";
    private const string PetaGramsSpecifier = "Pg";
    private const string ExaGramsSpecifier = "Eg";
    private const string ZettaGramsSpecifier = "Zg";
    private const string YottaGramsSpecifier = "Yg";
    private const string RonnaGramsSpecifier = "Rg";
    private const string QuettaGramsSpecifier = "Qg";
    private const string TonnesSpecifier = "t";
    private const string OuncesSpecifier = "oz";
    private const string PoundsSpecifier = "lb";
    private const string StonesSpecifier = "st";
    private const string ShortTonsSpecifier = "sht";
    private const string LongTonsSpecifier = "lt";
    private const string CaratsSpecifier = "ct";
    private const string GrainsSpecifier = "gr";
    private const string DramsSpecifier = "dr";
    private const string SlugsSpecifier = "slug";
    private const string DaltonsSpecifier = "Da";

    private const string ValidSpecifiers =
        "qg, rg, yg, zg, ag, fg, pg, ng, ug, mg, cg, dg, " +
        "g, dag, hg, kg, Mg, Gg, Tg, Pg, Eg, Zg, Yg, Rg, " +
        "Qg, t, oz, lb, st, sht, lt, ct, gr, dr, slug, and Da";
}
