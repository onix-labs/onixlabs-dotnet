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
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class NamedUnitAliasTests
{
    [Theory(DisplayName = "NamedUnitAlias.TryMatch bare base symbol returns multiplier=1 and original symbol")]
    [InlineData("N")]
    [InlineData("J")]
    [InlineData("Pa")]
    [InlineData("Wb")]
    [InlineData("lm")]
    [InlineData("lx")]
    [InlineData("Ω")]
    public void TryMatchBareBaseSymbolReturnsUnityMultiplier(string baseSymbol)
    {
        bool matched = NamedUnitAlias.TryMatch<Float128>(baseSymbol.AsSpan(), baseSymbol, out Float128 multiplier, out string renderedSymbol);
        Assert.True(matched);
        Assert.Equal(Float128.One, multiplier);
        Assert.Equal(baseSymbol, renderedSymbol);
    }

    [Theory(DisplayName = "NamedUnitAlias.TryMatch SI sub-base prefixes give >1 multipliers")]
    [InlineData("mN", "N", "1e3")]   // milli → ×1000
    [InlineData("uN", "N", "1e6")]   // micro → ×1,000,000 (rendered with µ symbol)
    [InlineData("nN", "N", "1e9")]
    [InlineData("pN", "N", "1e12")]
    [InlineData("fN", "N", "1e15")]
    [InlineData("aN", "N", "1e18")]
    [InlineData("cN", "N", "100")]   // centi → ×100
    [InlineData("dN", "N", "10")]    // deci → ×10
    public void TryMatchSubBasePrefixGivesLargerMultiplier(string specifier, string baseSymbol, string expectedMultiplier)
    {
        bool matched = NamedUnitAlias.TryMatch<Float128>(specifier.AsSpan(), baseSymbol, out Float128 multiplier, out _);
        Assert.True(matched);
        Assert.Equal(Float128.Parse(expectedMultiplier), multiplier);
    }

    [Theory(DisplayName = "NamedUnitAlias.TryMatch SI super-base prefixes give <1 multipliers")]
    [InlineData("daN", "N", "0.1")]    // deca → ×0.1
    [InlineData("hN", "N", "0.01")]    // hecto → ×0.01
    [InlineData("kN", "N", "1e-3")]
    [InlineData("MN", "N", "1e-6")]
    [InlineData("GN", "N", "1e-9")]
    [InlineData("TN", "N", "1e-12")]
    public void TryMatchSuperBasePrefixGivesSmallerMultiplier(string specifier, string baseSymbol, string expectedMultiplier)
    {
        bool matched = NamedUnitAlias.TryMatch<Float128>(specifier.AsSpan(), baseSymbol, out Float128 multiplier, out _);
        Assert.True(matched);
        Assert.Equal(Float128.Parse(expectedMultiplier), multiplier);
    }

    [Fact(DisplayName = "NamedUnitAlias.TryMatch 'u' prefix rewrites symbol to 'µ' on render")]
    public void TryMatchMicroPrefixRendersMu()
    {
        bool matched = NamedUnitAlias.TryMatch<Float128>("uF".AsSpan(), "F", out _, out string renderedSymbol);
        Assert.True(matched);
        Assert.Equal("µF", renderedSymbol);
    }

    [Fact(DisplayName = "NamedUnitAlias.TryMatch 'da' is parsed as deca, not d+a")]
    public void TryMatchDaPrefixedSymbolIsDeca()
    {
        // For base "N", "daN" should parse as deca-N (×0.1), not as some other combination.
        bool matched = NamedUnitAlias.TryMatch<Float128>("daN".AsSpan(), "N", out Float128 multiplier, out string renderedSymbol);
        Assert.True(matched);
        Assert.Equal(Float128.Parse("0.1"), multiplier);
        Assert.Equal("daN", renderedSymbol);
    }

    [Fact(DisplayName = "NamedUnitAlias.TryMatch multi-char base symbols (Pa, Wb, lm, lx)")]
    public void TryMatchMultiCharBaseSymbols()
    {
        Assert.True(NamedUnitAlias.TryMatch<Float128>("hPa".AsSpan(), "Pa", out Float128 hpaMult, out string hpaSym));
        Assert.Equal(Float128.Parse("0.01"), hpaMult);
        Assert.Equal("hPa", hpaSym);

        Assert.True(NamedUnitAlias.TryMatch<Float128>("mWb".AsSpan(), "Wb", out _, out string mwbSym));
        Assert.Equal("mWb", mwbSym);

        Assert.True(NamedUnitAlias.TryMatch<Float128>("klm".AsSpan(), "lm", out _, out _));
        Assert.True(NamedUnitAlias.TryMatch<Float128>("Mlx".AsSpan(), "lx", out _, out _));
    }

    [Fact(DisplayName = "NamedUnitAlias.TryMatch non-ASCII base symbol Ω")]
    public void TryMatchOhm()
    {
        Assert.True(NamedUnitAlias.TryMatch<Float128>("Ω".AsSpan(), "Ω", out Float128 mult, out string sym));
        Assert.Equal(Float128.One, mult);
        Assert.Equal("Ω", sym);

        Assert.True(NamedUnitAlias.TryMatch<Float128>("kΩ".AsSpan(), "Ω", out Float128 kMult, out _));
        Assert.Equal(Float128.Parse("1e-3"), kMult);

        Assert.True(NamedUnitAlias.TryMatch<Float128>("mΩ".AsSpan(), "Ω", out Float128 mMult, out _));
        Assert.Equal(Float128.Parse("1e3"), mMult);
    }

    [Theory(DisplayName = "NamedUnitAlias.TryMatch returns false for non-matching specifiers")]
    [InlineData("", "N")]              // empty
    [InlineData("Newton", "N")]        // not a prefix; just longer
    [InlineData("xN", "N")]            // unknown prefix 'x'
    [InlineData("N*m", "N")]           // contains separator — not an alias
    [InlineData("kg", "N")]            // wrong base
    [InlineData("MN", "Pa")]           // wrong base (Mega-N for Pa)
    public void TryMatchReturnsFalseForNonMatching(string specifier, string baseSymbol)
    {
        bool matched = NamedUnitAlias.TryMatch<Float128>(specifier.AsSpan(), baseSymbol, out Float128 multiplier, out string renderedSymbol);
        Assert.False(matched);
        Assert.Equal(Float128.Zero, multiplier);
        Assert.Equal(string.Empty, renderedSymbol);
    }

    [Fact(DisplayName = "NamedUnitAlias case-sensitivity: M (mega) ≠ m (milli)")]
    public void TryMatchIsCaseSensitive()
    {
        // For base "N": "MN" → mega-newton (×1e-6); "mN" → milli-newton (×1e3).
        Assert.True(NamedUnitAlias.TryMatch<Float128>("MN".AsSpan(), "N", out Float128 megaMult, out _));
        Assert.True(NamedUnitAlias.TryMatch<Float128>("mN".AsSpan(), "N", out Float128 milliMult, out _));
        Assert.Equal(Float128.Parse("1e-6"), megaMult);
        Assert.Equal(Float128.Parse("1e3"), milliMult);
        Assert.NotEqual(megaMult, milliMult);
    }

    // -- inputAlias (keyboard-friendly ASCII alternative for non-ASCII symbols like Ω) --

    [Fact(DisplayName = "NamedUnitAlias inputAlias accepts ASCII form, renders canonical symbol")]
    public void TryMatchInputAliasRendersCanonical()
    {
        // "Ohm" → canonical "Ω" with multiplier 1.
        Assert.True(NamedUnitAlias.TryMatch<Float128>("Ohm".AsSpan(), "Ω", out Float128 mult, out string sym, inputAlias: "Ohm"));
        Assert.Equal(Float128.One, mult);
        Assert.Equal("Ω", sym);

        // "kOhm" → "kΩ" with multiplier 1e-3.
        Assert.True(NamedUnitAlias.TryMatch<Float128>("kOhm".AsSpan(), "Ω", out Float128 kMult, out string kSym, inputAlias: "Ohm"));
        Assert.Equal(Float128.Parse("1e-3"), kMult);
        Assert.Equal("kΩ", kSym);

        // "mOhm" → "mΩ" with multiplier 1e3.
        Assert.True(NamedUnitAlias.TryMatch<Float128>("mOhm".AsSpan(), "Ω", out Float128 mMult, out string mSym, inputAlias: "Ohm"));
        Assert.Equal(Float128.Parse("1e3"), mMult);
        Assert.Equal("mΩ", mSym);

        // "MOhm" → "MΩ" with multiplier 1e-6.
        Assert.True(NamedUnitAlias.TryMatch<Float128>("MOhm".AsSpan(), "Ω", out Float128 megMult, out string megSym, inputAlias: "Ohm"));
        Assert.Equal(Float128.Parse("1e-6"), megMult);
        Assert.Equal("MΩ", megSym);

        // "uOhm" → "µΩ" with multiplier 1e6 (u→µ rewrite preserved through alias).
        Assert.True(NamedUnitAlias.TryMatch<Float128>("uOhm".AsSpan(), "Ω", out _, out string uSym, inputAlias: "Ohm"));
        Assert.Equal("µΩ", uSym);
    }

    [Fact(DisplayName = "NamedUnitAlias inputAlias does NOT override canonical match — both still work")]
    public void TryMatchInputAliasCoexistsWithCanonical()
    {
        // Both forms accepted; both render canonical "Ω".
        Assert.True(NamedUnitAlias.TryMatch<Float128>("kΩ".AsSpan(), "Ω", out _, out string canonicalSym, inputAlias: "Ohm"));
        Assert.Equal("kΩ", canonicalSym);

        Assert.True(NamedUnitAlias.TryMatch<Float128>("kOhm".AsSpan(), "Ω", out _, out string aliasSym, inputAlias: "Ohm"));
        Assert.Equal("kΩ", aliasSym);
    }

    [Fact(DisplayName = "NamedUnitAlias inputAlias=null still works as before (no alias)")]
    public void TryMatchInputAliasNullRetainsOriginalBehavior()
    {
        // Without inputAlias, "Ohm" doesn't match anything for base "Ω".
        Assert.False(NamedUnitAlias.TryMatch<Float128>("Ohm".AsSpan(), "Ω", out _, out _));
    }
}
