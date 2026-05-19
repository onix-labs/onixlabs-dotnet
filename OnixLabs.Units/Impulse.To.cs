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
using System.Globalization;

namespace OnixLabs.Units;

public readonly partial struct Impulse<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">
    /// The format to use. Composite specifiers take the form <c>&lt;force&gt;*&lt;time&gt;[:scale]</c> where
    /// <c>&lt;force&gt;</c> is itself the composite shape <c>&lt;mass&gt;*&lt;acceleration&gt;</c>. Example:
    /// <c>kg*m/s²*s:3</c> (Newton-seconds).
    /// </param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int colonIndex = format.IndexOf(':');
        ReadOnlySpan<char> unitPart;
        int scale;

        if (colonIndex < 0)
        {
            unitPart = format;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture)
                .NumberFormat.NumberDecimalDigits;
        }
        else
        {
            unitPart = format[..colonIndex];
            if (!int.TryParse(format[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("Impulse format scale must contain only decimal digits.");
        }

        // The force-spec itself contains '*' (e.g. "kg*m/s²"), so we split on the LAST '*' — everything before is the
        // force-spec, everything after is the outer time-spec.
        int lastStar = unitPart.LastIndexOf('*');
        if (lastStar < 0)
            throw new FormatException($"Impulse format must contain a '*' separator between the force and time specifiers (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> forceSpecifier = unitPart[..lastStar];
        ReadOnlySpan<char> timeSpecifier = unitPart[(lastStar + 1)..];

        if (forceSpecifier.IsEmpty)
            throw new FormatException("Impulse format force specifier must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("Impulse format time specifier must not be empty.");

        T value = Left.ValueOf(forceSpecifier) * Right.ValueOf(timeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(forceSpecifier)}*{new string(timeSpecifier)}";
    }
}
