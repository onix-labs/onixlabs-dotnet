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

public readonly partial struct Speed<T>
{
    /// <summary>
    /// Gets the speed value expressed in the distance-and-time scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;distance&gt;/&lt;time&gt;</c> (for example
    /// <c>m/s</c>, <c>mi/h</c>, or <c>km/h</c>). Both halves use the underlying <see cref="Distance{T}"/> and
    /// <see cref="Time{T}"/> specifiers.
    /// </param>
    /// <returns>Returns the speed value expressed as <c>distance / time</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        int slashIndex = specifier.IndexOf('/');
        if (slashIndex < 0)
            throw new FormatException($"Speed specifier must contain a '/' separator between the distance and time specifiers (e.g. 'm/s'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> distanceSpecifier = specifier[..slashIndex];
        ReadOnlySpan<char> timeSpecifier = specifier[(slashIndex + 1)..];

        if (distanceSpecifier.IsEmpty)
            throw new FormatException("Speed specifier distance component must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("Speed specifier time component must not be empty.");

        return Left.ValueOf(distanceSpecifier) / Right.ValueOf(timeSpecifier);
    }
}
