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

public readonly partial struct ElectricCharge<T>
{
    /// <summary>
    /// Gets the electric-charge value expressed in the current-and-time scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;current&gt;*&lt;time&gt;</c> (for
    /// example <c>A*s</c> for coulombs). Both halves use the underlying <see cref="Current{T}"/> and
    /// <see cref="Time{T}"/> specifiers.
    /// </param>
    /// <returns>Returns the electric-charge value expressed as <c>current * time</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        // Named-unit alias (C, mC, μC, kC, ...).
        if (NamedUnitAlias.TryMatch<T>(specifier, NamedSymbol, out T aliasMultiplier, out _))
            return Magnitude * aliasMultiplier;

        // Both current-spec and time-spec have no '*', so the single '*' cleanly separates the two.
        int starIndex = specifier.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"ElectricCharge specifier must contain a '*' separator between the current and time specifiers (e.g. 'A*s'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> currentSpecifier = specifier[..starIndex];
        ReadOnlySpan<char> timeSpecifier = specifier[(starIndex + 1)..];

        if (currentSpecifier.IsEmpty)
            throw new FormatException("ElectricCharge specifier current component must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("ElectricCharge specifier time component must not be empty.");

        return Left.ValueOf(currentSpecifier) * Right.ValueOf(timeSpecifier);
    }
}
