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

using OnixLabs.Numerics;

namespace OnixLabs.Units;

public readonly partial struct Mass<T>
{
    /// <inheritdoc/>
    public static Mass<T> Zero => new(T.Zero);

    // T-precision conversion factors for non-power-of-10 scales (Tonnes, imperial, atomic).
    // Stored as static readonly per closed T so they're computed once and reused.
    //
    // Each constant represents "QuectoGrams in one X". Written as
    // `T.CreateChecked(<integer>) * GenericMath.Pow10<T>(<exponent>)` so the entire factor
    // stays at T's precision; `T.CreateChecked(<lossy-double-literal>)` would pre-round
    // through double's ~15-17 digit precision before reaching T.
    private static readonly T QuectogramsPerTonne     = GenericMath.Pow10<T>(36);                                     // 1e36
    private static readonly T QuectogramsPerOunce     = T.CreateChecked(28349523125L) * GenericMath.Pow10<T>(21);    // 28.349523125 g
    private static readonly T QuectogramsPerPound     = T.CreateChecked(45359237L) * GenericMath.Pow10<T>(25);       // 453.59237 g
    private static readonly T QuectogramsPerStone     = T.CreateChecked(635029318L) * GenericMath.Pow10<T>(25);      // 6350.29318 g
    private static readonly T QuectogramsPerShortTon  = T.CreateChecked(90718474L) * GenericMath.Pow10<T>(28);       // 907184.74 g
    private static readonly T QuectogramsPerLongTon   = T.CreateChecked(10160469088L) * GenericMath.Pow10<T>(26);    // 1016046.9088 g
    private static readonly T QuectogramsPerCarat     = T.CreateChecked(2) * GenericMath.Pow10<T>(29);                // 0.2 g
    private static readonly T QuectogramsPerGrain     = T.CreateChecked(6479891L) * GenericMath.Pow10<T>(22);        // 0.06479891 g
    private static readonly T QuectogramsPerDram      = T.CreateChecked(17718451953125L) * GenericMath.Pow10<T>(17); // 1.7718451953125 g
    private static readonly T QuectogramsPerSlug      = T.CreateChecked(1459390293720636L) * GenericMath.Pow10<T>(19); // 14593.90293720636 g
    private static readonly T QuectogramsPerDalton    = T.CreateChecked(16605390666L) / T.CreateChecked(10000);       // 1660539.0666 qg = 1.66053906660e-24 g

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
