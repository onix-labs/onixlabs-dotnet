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

public readonly partial struct LuminousFlux<T>
{
    /// <summary>
    /// Gets the luminous-flux value expressed in the intensity-and-solid-angle scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;intensity&gt;*&lt;solidAngle&gt;</c> (for
    /// example <c>cd*sr</c> for lumens). Both halves use the underlying <see cref="LuminousIntensity{T}"/> and
    /// <see cref="SolidAngle{T}"/> specifiers.
    /// </param>
    /// <returns>Returns the luminous-flux value expressed as <c>intensity * solidAngle</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        // Both intensity-spec and solid-angle-spec have no '*', so the single '*' cleanly separates the two.
        int starIndex = specifier.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"LuminousFlux specifier must contain a '*' separator between the luminous-intensity and solid-angle specifiers (e.g. 'cd*sr'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> intensitySpecifier = specifier[..starIndex];
        ReadOnlySpan<char> solidAngleSpecifier = specifier[(starIndex + 1)..];

        if (intensitySpecifier.IsEmpty)
            throw new FormatException("LuminousFlux specifier luminous-intensity component must not be empty.");

        if (solidAngleSpecifier.IsEmpty)
            throw new FormatException("LuminousFlux specifier solid-angle component must not be empty.");

        return Left.ValueOf(intensitySpecifier) * Right.ValueOf(solidAngleSpecifier);
    }
}
