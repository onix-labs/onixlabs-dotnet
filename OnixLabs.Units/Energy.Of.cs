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

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
    /// <summary>
    /// Gets the energy value expressed in the force-and-distance scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;force&gt;*&lt;distance&gt;</c> (for
    /// example <c>kg*m/s²*m</c> for joules, or <c>g*cm/s²*cm</c> for ergs). The <c>&lt;force&gt;</c> half itself takes
    /// the <see cref="Force{T}.ValueOf(ReadOnlySpan{char})"/> shape (which contains <c>*</c>), so the split is taken
    /// on the LAST <c>*</c>.
    /// </param>
    /// <returns>Returns the energy value expressed as <c>force * distance</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        // Named-unit alias (J, kJ, MJ, mJ, ...) — checked before compound parsing.
        if (NamedUnitAlias.TryMatch<T>(specifier, NamedSymbol, out T aliasMultiplier, out _))
            return Magnitude * aliasMultiplier;

        // The force-spec itself contains '*' (e.g. "kg*m/s²"), so split on the LAST '*' — everything before is the
        // force-spec, everything after is the outer distance-spec.
        int lastStar = specifier.LastIndexOf('*');
        if (lastStar < 0)
            throw new FormatException($"Energy specifier must contain a '*' separator between the force and distance specifiers (e.g. 'kg*m/s²*m'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> forceSpecifier = specifier[..lastStar];
        ReadOnlySpan<char> distanceSpecifier = specifier[(lastStar + 1)..];

        if (forceSpecifier.IsEmpty)
            throw new FormatException("Energy specifier force component must not be empty.");

        if (distanceSpecifier.IsEmpty)
            throw new FormatException("Energy specifier distance component must not be empty.");

        return Left.ValueOf(forceSpecifier) * Right.ValueOf(distanceSpecifier);
    }
}
