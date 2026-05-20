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

public readonly partial struct Force<T>
{
    /// <summary>
    /// Gets the force value expressed in the mass-and-acceleration scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;mass&gt;*&lt;acceleration&gt;</c> (for
    /// example <c>kg*m/s²</c> for Newtons, <c>g*cm/s²</c> for dynes, or <c>lb*ft/s²</c> for poundals). The
    /// <c>&lt;acceleration&gt;</c> half itself takes the <see cref="Acceleration{T}.ValueOf(ReadOnlySpan{char})"/>
    /// shape (squared or compound form).
    /// </param>
    /// <returns>Returns the force value expressed as <c>mass * acceleration</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        // Named-unit alias (N, kN, mN, ...) — checked before compound parsing because aliased forms don't contain '*'.
        if (NamedUnitAlias.TryMatch<T>(specifier, NamedSymbol, out T aliasMultiplier, out _))
            return Magnitude * aliasMultiplier;

        // Mass specifiers in this library don't contain '*', so splitting on the FIRST '*' separates the mass-spec
        // (no '*') from the acceleration-spec (may contain '/' for compound or '²' for squared forms — both fine).
        int starIndex = specifier.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"Force specifier must contain a '*' separator between the mass and acceleration specifiers (e.g. 'kg*m/s²'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> massSpecifier = specifier[..starIndex];
        ReadOnlySpan<char> accelerationSpecifier = specifier[(starIndex + 1)..];

        if (massSpecifier.IsEmpty)
            throw new FormatException("Force specifier mass component must not be empty.");

        if (accelerationSpecifier.IsEmpty)
            throw new FormatException("Force specifier acceleration component must not be empty.");

        return Left.ValueOf(massSpecifier) * Right.ValueOf(accelerationSpecifier);
    }
}
