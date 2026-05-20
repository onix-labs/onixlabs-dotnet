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

public readonly partial struct ElectricPotential<T>
{
    /// <summary>
    /// Gets the electric-potential value expressed in the energy-and-charge scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;energy&gt;/&lt;charge&gt;</c> (for
    /// example <c>kg*m/s²*m/A*s</c> for volts). The <c>&lt;energy&gt;</c> half itself takes the
    /// <see cref="Energy{T}.ValueOf(ReadOnlySpan{char})"/> shape (which contains <c>/</c> via its inner acceleration),
    /// so the split is taken on the LAST <c>/</c>.
    /// </param>
    /// <returns>Returns the electric-potential value expressed as <c>energy / charge</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        // Named-unit alias (V, kV, mV, MV, ...).
        if (NamedUnitAlias.TryMatch<T>(specifier, NamedSymbol, out T aliasMultiplier, out _))
            return Magnitude * aliasMultiplier;

        // The energy-spec itself contains '/' (via its inner acceleration spec, e.g. "kg*m/s²*m"), while the
        // charge-spec contains no '/' (only '*'). Split on the LAST '/' — everything before is the energy-spec,
        // everything after is the charge-spec.
        int lastSlash = specifier.LastIndexOf('/');
        if (lastSlash < 0)
            throw new FormatException($"ElectricPotential specifier must contain a '/' separator between the energy and charge specifiers (e.g. 'kg*m/s²*m/A*s'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> energySpecifier = specifier[..lastSlash];
        ReadOnlySpan<char> chargeSpecifier = specifier[(lastSlash + 1)..];

        if (energySpecifier.IsEmpty)
            throw new FormatException("ElectricPotential specifier energy component must not be empty.");

        if (chargeSpecifier.IsEmpty)
            throw new FormatException("ElectricPotential specifier charge component must not be empty.");

        return Left.ValueOf(energySpecifier) / Right.ValueOf(chargeSpecifier);
    }
}
