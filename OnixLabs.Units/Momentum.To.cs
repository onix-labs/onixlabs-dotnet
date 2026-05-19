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

public readonly partial struct Momentum<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>Formats using <c>&lt;mass&gt;*&lt;speed&gt;[:scale]</c> (e.g. <c>kg*m/s:3</c>).</summary>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int colonIndex = format.IndexOf(':');
        ReadOnlySpan<char> unitPart;
        int scale;

        if (colonIndex < 0)
        {
            unitPart = format;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture).NumberFormat.NumberDecimalDigits;
        }
        else
        {
            unitPart = format[..colonIndex];
            if (!int.TryParse(format[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("Momentum format scale must contain only decimal digits.");
        }

        // Mass-spec has no '*'; speed-spec has '/' but no '*'. Single '*' separates the two.
        int starIndex = unitPart.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"Momentum format must contain '*' (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> massSpecifier = unitPart[..starIndex];
        ReadOnlySpan<char> speedSpecifier = unitPart[(starIndex + 1)..];

        if (massSpecifier.IsEmpty || speedSpecifier.IsEmpty)
            throw new FormatException("Momentum format mass and speed specifiers must be non-empty.");

        T value = Left.ValueOf(massSpecifier) * Right.ValueOf(speedSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(massSpecifier)}*{new string(speedSpecifier)}";
    }
}
