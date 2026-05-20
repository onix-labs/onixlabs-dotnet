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

public readonly partial struct LuminousFlux<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>Formats using <c>&lt;luminous-intensity&gt;*&lt;solidAngle&gt;[:scale]</c> (e.g. <c>cd*sr:3</c> for lumens).</summary>
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
                throw new FormatException("LuminousFlux format scale must contain only decimal digits.");
        }

        // LuminousIntensity-spec has no '*'; solid-angle-spec has no '*'. Single '*' separates the two.
        int starIndex = unitPart.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"LuminousFlux format must contain '*' (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> luminousIntensitySpecifier = unitPart[..starIndex];
        ReadOnlySpan<char> solidAngleSpecifier = unitPart[(starIndex + 1)..];

        if (luminousIntensitySpecifier.IsEmpty || solidAngleSpecifier.IsEmpty)
            throw new FormatException("LuminousFlux format luminous-intensity and solid-angle specifiers must be non-empty.");

        T value = Left.ValueOf(luminousIntensitySpecifier) * Right.ValueOf(solidAngleSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(luminousIntensitySpecifier)}*{new string(solidAngleSpecifier)}";
    }
}
