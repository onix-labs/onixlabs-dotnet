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

public readonly partial struct Area<T>
{
    /// <summary>
    /// Gets an empty <see cref="Area{T}"/> value.
    /// </summary>
    public static Area<T> Empty => new(T.Zero);

    private const string SquareYoctoMetersSpecifier = "sqym";
    private const string SquareZeptoMetersSpecifier = "sqzm";
    private const string SquareAttoMetersSpecifier = "sqam";
    private const string SquareFemtoMetersSpecifier = "sqfm";
    private const string SquarePicoMetersSpecifier = "sqpm";
    private const string SquareNanoMetersSpecifier = "sqnm";
    private const string SquareMicroMetersSpecifier = "squm";
    private const string SquareMilliMetersSpecifier = "sqmm";
    private const string SquareCentiMetersSpecifier = "sqcm";
    private const string SquareDeciMetersSpecifier = "sqdm";
    private const string SquareMetersSpecifier = "sqm";
    private const string SquareDecaMetersSpecifier = "sqdam";
    private const string SquareHectoMetersSpecifier = "sqhm";
    private const string SquareKiloMetersSpecifier = "sqkm";
    private const string SquareMegaMetersSpecifier = "sqMm";
    private const string SquareGigaMetersSpecifier = "sqGm";
    private const string SquareTeraMetersSpecifier = "sqTm";
    private const string SquarePetaMetersSpecifier = "sqPm";
    private const string SquareExaMetersSpecifier = "sqEm";
    private const string SquareZettaMetersSpecifier = "sqZm";
    private const string SquareYottaMetersSpecifier = "sqYm";
    private const string SquareInchesSpecifier = "sqin";
    private const string SquareFeetSpecifier = "sqft";
    private const string SquareYardsSpecifier = "sqyd";
    private const string SquareMilesSpecifier = "sqmi";
    private const string AcresSpecifier = "ac";
    private const string HectaresSpecifier = "ha";
}
