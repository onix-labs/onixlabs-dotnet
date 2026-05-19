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

public readonly partial struct MolarConcentration<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use. Composite specifiers take the form <c>&lt;amount&gt;/&lt;volume&gt;[:scale]</c> (for example <c>mol/cum</c> or <c>mol/L:0</c>).</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int slashIndex = format.IndexOf('/');

        if (slashIndex < 0)
            throw new FormatException($"MolarConcentration format must contain a '/' separator between the amount and volume specifiers (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> amountSpecifier = format[..slashIndex];
        ReadOnlySpan<char> rest = format[(slashIndex + 1)..];
        int colonIndex = rest.IndexOf(':');
        ReadOnlySpan<char> volumeSpecifier;
        int scale;

        if (colonIndex < 0)
        {
            volumeSpecifier = rest;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture)
                .NumberFormat.NumberDecimalDigits;
        }
        else
        {
            volumeSpecifier = rest[..colonIndex];
            if (!int.TryParse(rest[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("MolarConcentration format scale must contain only decimal digits.");
        }

        if (amountSpecifier.IsEmpty)
            throw new FormatException("MolarConcentration format amount specifier must not be empty.");

        if (volumeSpecifier.IsEmpty)
            throw new FormatException("MolarConcentration format volume specifier must not be empty.");

        T value = Left.ValueOf(amountSpecifier) / Right.ValueOf(volumeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(amountSpecifier)}/{new string(volumeSpecifier)}";
    }
}
