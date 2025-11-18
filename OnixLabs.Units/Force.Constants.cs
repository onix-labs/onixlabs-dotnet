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
    /// <summary>
    /// Gets a zero <see cref="Force{T}"/> value, equal to zero yoctonewtons.
    /// </summary>
    public static readonly Force<T> Zero = new(T.Zero);

    private const string YoctoNewtonsSpecifier = "yN";
    private const string ZeptoNewtonsSpecifier = "zN";
    private const string AttoNewtonsSpecifier = "aN";
    private const string FemtoNewtonsSpecifier = "fN";
    private const string PicoNewtonsSpecifier = "pN";
    private const string NanoNewtonsSpecifier = "nN";
    private const string MicroNewtonsSpecifier = "uN";
    private const string MilliNewtonsSpecifier = "mN";
    private const string NewtonsSpecifier = "N";
    private const string KiloNewtonsSpecifier = "kN";
    private const string MegaNewtonsSpecifier = "MN";
    private const string GigaNewtonsSpecifier = "GN";
    private const string TeraNewtonsSpecifier = "TN";
    private const string PetaNewtonsSpecifier = "PN";
    private const string ExaNewtonsSpecifier = "EN";
    private const string ZettaNewtonsSpecifier = "ZN";
    private const string YottaNewtonsSpecifier = "YN";
    private const string DynesSpecifier = "dyn";
    private const string KilogramForceSpecifier = "kgf";
    private const string GramForceSpecifier = "gf";
    private const string TonneForceSpecifier = "tf";
    private const string PoundForceSpecifier = "lbf";
    private const string OunceForceSpecifier = "ozf";
    private const string PoundalsSpecifier = "pdl";
    private const string ShortTonForceSpecifier = "tonf";
    private const string LongTonForceSpecifier = "ltf";
}
