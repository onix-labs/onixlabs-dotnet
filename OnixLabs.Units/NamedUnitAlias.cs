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
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Parses SI-prefixed named-unit aliases (e.g. <c>N</c>, <c>kN</c>, <c>mPa</c>, <c>µF</c>, <c>MΩ</c>) for composite
/// units whose magnitude already lands at the SI base scale (newton, joule, pascal, …).
/// </summary>
/// <remarks>
/// The prefix table uses the library's existing <c>u</c>-for-micro convention on input; rendered output emits the
/// Unicode <c>µ</c>. Prefix matching is longest-first to disambiguate <c>da</c> (deca) from <c>d</c> (deci) and <c>a</c>
/// (atto). Comparisons are ordinal/case-sensitive (matching the rest of the library), so <c>M</c> = mega and
/// <c>m</c> = milli are distinct.
///
/// Some unit symbols are non-ASCII (notably <c>Ω</c>). For those, callers may pass <c>inputAlias</c> with a
/// keyboard-friendly spelling (e.g. <c>"Ohm"</c>) — the matcher then accepts either form on input but always renders
/// the canonical <c>baseSymbol</c> on output (e.g. typing <c>"kOhm"</c> renders <c>"kΩ"</c>).
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class NamedUnitAlias
{
    // SI prefix exponent: for prefix p with exponent n, "1 base = 10^(-n) prefixed", e.g. k → 3 → 1 N = 10^-3 kN.
    private static readonly Dictionary<string, int> PrefixExponents = new(StringComparer.Ordinal)
    {
        { "q", -30 }, { "r", -27 }, { "y", -24 }, { "z", -21 }, { "a", -18 },
        { "f", -15 }, { "p", -12 }, { "n", -9 }, { "u", -6 }, { "m", -3 },
        { "c", -2 }, { "d", -1 }, { "", 0 },
        { "da", 1 }, { "h", 2 }, { "k", 3 }, { "M", 6 }, { "G", 9 },
        { "T", 12 }, { "P", 15 }, { "E", 18 }, { "Z", 21 }, { "Y", 24 },
        { "R", 27 }, { "Q", 30 }
    };

    /// <summary>
    /// Attempts to interpret <paramref name="specifier"/> as an SI-prefixed alias of <paramref name="baseSymbol"/>
    /// (or, optionally, of <paramref name="inputAlias"/>).
    /// </summary>
    /// <typeparam name="T">The numeric type used by the surrounding unit.</typeparam>
    /// <param name="specifier">The specifier under test (e.g. <c>"kN"</c>, <c>"mPa"</c>, <c>"µF"</c>, <c>"kOhm"</c>).</param>
    /// <param name="baseSymbol">The canonical named-unit base symbol used for rendering (e.g. <c>"N"</c>, <c>"Ω"</c>).</param>
    /// <param name="multiplier">
    /// On success, the factor by which the unit's SI-base magnitude must be multiplied to obtain the prefixed value
    /// (e.g. for <c>kN</c>: 1e-3 so that <c>1 N × 1e-3 = 0.001 kN</c>).
    /// </param>
    /// <param name="renderedSymbol">
    /// On success, the symbol that should appear in formatted output, built as <c>prefix + baseSymbol</c>. Input
    /// prefix <c>u</c> is rewritten to the Unicode <c>µ</c>. When matched via <paramref name="inputAlias"/>, the
    /// alias portion is replaced by the canonical <paramref name="baseSymbol"/> (e.g. input <c>"kOhm"</c> → <c>"kΩ"</c>).
    /// </param>
    /// <param name="inputAlias">
    /// Optional keyboard-friendly alternative form accepted on input only. Pass <c>"Ohm"</c> when
    /// <paramref name="baseSymbol"/> is <c>"Ω"</c>, for example. Has no effect on rendering.
    /// </param>
    /// <returns>Returns <see langword="true"/> when matched; otherwise <see langword="false"/>.</returns>
    public static bool TryMatch<T>(
        ReadOnlySpan<char> specifier,
        string baseSymbol,
        out T multiplier,
        out string renderedSymbol,
        string? inputAlias = null)
        where T : IFloatingPoint<T>
    {
        if (TryMatchAgainst(specifier, baseSymbol, baseSymbol, out multiplier, out renderedSymbol))
            return true;

        if (inputAlias is not null && TryMatchAgainst(specifier, inputAlias, baseSymbol, out multiplier, out renderedSymbol))
            return true;

        multiplier = T.Zero;
        renderedSymbol = string.Empty;
        return false;
    }

    private static bool TryMatchAgainst<T>(
        ReadOnlySpan<char> specifier,
        string matchSymbol,
        string renderSymbol,
        out T multiplier,
        out string renderedSymbol)
        where T : IFloatingPoint<T>
    {
        // Longest-first: try 2-char prefix ("da"), then 1-char, then bare match symbol.
        for (int prefixLength = 2; prefixLength >= 0; prefixLength--)
        {
            if (specifier.Length != prefixLength + matchSymbol.Length) continue;
            if (!specifier[prefixLength..].SequenceEqual(matchSymbol.AsSpan())) continue;

            string prefix = new(specifier[..prefixLength]);
            if (!PrefixExponents.TryGetValue(prefix, out int exponent)) continue;

            multiplier = exponent switch
            {
                0 => T.One,
                > 0 => T.One / UnitMath.Pow10<T>(exponent),
                _ => UnitMath.Pow10<T>(-exponent)
            };
            renderedSymbol = prefix == "u" ? "µ" + renderSymbol : prefix + renderSymbol;
            return true;
        }

        multiplier = T.Zero;
        renderedSymbol = string.Empty;
        return false;
    }
}
