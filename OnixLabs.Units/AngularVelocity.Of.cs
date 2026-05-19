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

public readonly partial struct AngularVelocity<T>
{
    /// <summary>
    /// Gets the angular velocity value expressed in the angle-and-time scale identified by the specified composite specifier.
    /// </summary>
    /// <param name="specifier">
    /// The composite format specifier identifying the scale, of the form <c>&lt;angle&gt;/&lt;time&gt;</c> (for example
    /// <c>rad/s</c> or <c>deg/min</c>). Both halves use the underlying <see cref="Angle{T}"/> and
    /// <see cref="Time{T}"/> specifiers.
    /// </param>
    /// <returns>Returns the angular velocity value expressed as <c>angle / time</c> in the requested units.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier)
    {
        int slashIndex = specifier.IndexOf('/');
        if (slashIndex < 0)
            throw new FormatException($"AngularVelocity specifier must contain a '/' separator between the angle and time specifiers (e.g. 'rad/s'). Got: '{new string(specifier)}'.");

        ReadOnlySpan<char> angleSpecifier = specifier[..slashIndex];
        ReadOnlySpan<char> timeSpecifier = specifier[(slashIndex + 1)..];

        if (angleSpecifier.IsEmpty)
            throw new FormatException("AngularVelocity specifier angle component must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("AngularVelocity specifier time component must not be empty.");

        return Left.ValueOf(angleSpecifier) / Right.ValueOf(timeSpecifier);
    }
}
