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

public readonly partial struct ElectricResistance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;potential&gt;/&lt;current&gt;[:scale]</c> (e.g. <c>kg*m/s²*m/A*s/A:3</c> for ohms).
    /// </summary>
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
                throw new FormatException("ElectricResistance format scale must contain only decimal digits.");
        }

        // ElectricPotential-spec contains '/'; current-spec is a bare scale token. Split on last '/'.
        int lastSlash = unitPart.LastIndexOf('/');
        if (lastSlash < 0)
            throw new FormatException($"ElectricResistance format must contain a '/' separator between electric-potential and current (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> potentialSpecifier = unitPart[..lastSlash];
        ReadOnlySpan<char> currentSpecifier = unitPart[(lastSlash + 1)..];

        if (potentialSpecifier.IsEmpty)
            throw new FormatException("ElectricResistance format electric-potential specifier must not be empty.");

        if (currentSpecifier.IsEmpty)
            throw new FormatException("ElectricResistance format current specifier must not be empty.");

        T value = Left.ValueOf(potentialSpecifier) / Right.ValueOf(currentSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(potentialSpecifier)}/{new string(currentSpecifier)}";
    }
}
