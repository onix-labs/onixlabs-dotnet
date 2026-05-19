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

public readonly partial struct Acceleration<T>
{
    /// <summary>
    /// Gets the acceleration value expressed in the speed-and-time scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale. Two forms are accepted:
    /// <list type="bullet">
    /// <item><description>Squared form <c>&lt;distance&gt;/&lt;time&gt;²</c> (e.g. <c>m/s²</c>, <c>m/s2</c>).</description></item>
    /// <item><description>Compound form <c>&lt;speed&gt;/&lt;time&gt;</c> where the speed itself takes the
    /// <see cref="Speed{T}.ValueOf(ReadOnlySpan{char})"/> shape (e.g. <c>km/h/s</c>, <c>mi/h/min</c>).</description></item>
    /// </list>
    /// </param>
    /// <returns>Returns the acceleration value expressed as <c>speed / time</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        if (specifier.IsEmpty)
            throw new FormatException("Acceleration specifier must not be empty.");

        bool isSquaredForm = specifier[^1] == '²' || specifier[^1] == '2';
        ReadOnlySpan<char> unitPart = isSquaredForm ? specifier[..^1] : specifier;

        if (unitPart.IsEmpty)
            throw new FormatException("Acceleration specifier must contain a unit before the squared marker.");

        ReadOnlySpan<char> speedSpecifier;
        ReadOnlySpan<char> timeSpecifier;

        if (isSquaredForm)
        {
            // unitPart is "distance/time" — the speed-spec is the whole thing, the outer time-spec is
            // the part after the only '/'.
            int slashIndex = unitPart.IndexOf('/');
            if (slashIndex < 0)
                throw new FormatException($"Acceleration squared specifier must contain '/' between distance and time (e.g. 'm/s²'). Got: '{new string(specifier)}'.");

            speedSpecifier = unitPart;
            timeSpecifier = unitPart[(slashIndex + 1)..];
        }
        else
        {
            // unitPart is "<speed-spec>/<time-spec>" — the speed-spec itself contains '/', so split on the LAST '/'.
            int lastSlash = unitPart.LastIndexOf('/');
            if (lastSlash < 0)
                throw new FormatException($"Acceleration compound specifier must contain a '/' separator between speed and time (e.g. 'km/h/s'). Got: '{new string(specifier)}'.");

            speedSpecifier = unitPart[..lastSlash];
            timeSpecifier = unitPart[(lastSlash + 1)..];
        }

        if (speedSpecifier.IsEmpty)
            throw new FormatException("Acceleration specifier speed component must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("Acceleration specifier time component must not be empty.");

        return Left.ValueOf(speedSpecifier) / Right.ValueOf(timeSpecifier);
    }
}
