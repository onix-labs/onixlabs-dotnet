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

public readonly partial struct MassFlowRate<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use. Composite specifiers take the form <c>&lt;mass&gt;/&lt;time&gt;[:scale]</c> (for example <c>kg/s</c> or <c>g/s:0</c>).</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int slashIndex = format.IndexOf('/');

        if (slashIndex < 0)
            throw new FormatException($"MassFlowRate format must contain a '/' separator between the mass and time specifiers (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> massSpecifier = format[..slashIndex];
        ReadOnlySpan<char> rest = format[(slashIndex + 1)..];
        int colonIndex = rest.IndexOf(':');
        ReadOnlySpan<char> timeSpecifier;
        int scale;

        if (colonIndex < 0)
        {
            timeSpecifier = rest;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture)
                .NumberFormat.NumberDecimalDigits;
        }
        else
        {
            timeSpecifier = rest[..colonIndex];
            if (!int.TryParse(rest[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("MassFlowRate format scale must contain only decimal digits.");
        }

        if (massSpecifier.IsEmpty)
            throw new FormatException("MassFlowRate format mass specifier must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("MassFlowRate format time specifier must not be empty.");

        T value = Left.ValueOf(massSpecifier) / Right.ValueOf(timeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(massSpecifier)}/{new string(timeSpecifier)}";
    }
}
