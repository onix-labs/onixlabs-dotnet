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

using System;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;


public readonly partial struct Volume<T>
{
    /// <summary>
    /// Gets a zero <c>0</c> <see cref="Volume{T}"/> value.
    /// </summary>
    public static readonly Volume<T> Zero = new(T.Zero);

    private const string CubicYoctoMetersSpecifier = "cuym";
    private const string CubicZeptoMetersSpecifier = "cuzm";
    private const string CubicAttoMetersSpecifier = "cuam";
    private const string CubicFemtoMetersSpecifier = "cufm";
    private const string CubicPicoMetersSpecifier = "cupm";
    private const string CubicNanoMetersSpecifier = "cunm";
    private const string CubicMicroMetersSpecifier = "cuum";
    private const string CubicMilliMetersSpecifier = "cumm";
    private const string CubicCentiMetersSpecifier = "cucm";
    private const string CubicDeciMetersSpecifier = "cudm";
    private const string CubicMetersSpecifier = "cum";
    private const string CubicDecaMetersSpecifier = "cudam";
    private const string CubicHectoMetersSpecifier = "cuhm";
    private const string CubicKiloMetersSpecifier = "cukm";
    private const string CubicMegaMetersSpecifier = "cuMm";
    private const string CubicGigaMetersSpecifier = "cuGm";
    private const string CubicTeraMetersSpecifier = "cuTm";
    private const string CubicPetaMetersSpecifier = "cuPm";
    private const string CubicExaMetersSpecifier = "cuEm";
    private const string CubicZettaMetersSpecifier = "cuZm";
    private const string CubicYottaMetersSpecifier = "cuYm";
    private const string LitersSpecifier = "l";
    private const string MilliLitersSpecifier = "ml";
    private const string CentiLitersSpecifier = "cl";
    private const string DeciLitersSpecifier = "dl";
    private const string CubicInchesSpecifier = "cuin";
    private const string CubicFeetSpecifier = "cuft";
    private const string CubicYardsSpecifier = "cuyd";
    private const string FluidOuncesSpecifier = "floz";
    private const string CupsSpecifier = "cup";
    private const string PintsSpecifier = "pt";
    private const string QuartsSpecifier = "qt";
    private const string GallonsSpecifier = "gal";
}
