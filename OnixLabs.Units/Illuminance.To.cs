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

public readonly partial struct Illuminance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;luminous-flux&gt;/&lt;area&gt;[:scale]</c> (e.g. <c>cd*rad/sqm:3</c> for lux).
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
                throw new FormatException("Illuminance format scale must contain only decimal digits.");
        }

        // Luminous-flux-spec contains '*' but no '/'; area-spec is a bare scale token. Single '/' separates the two.
        int slashIndex = unitPart.IndexOf('/');
        if (slashIndex < 0)
            throw new FormatException($"Illuminance format must contain a '/' separator between luminous-flux and area (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> luminousFluxSpecifier = unitPart[..slashIndex];
        ReadOnlySpan<char> areaSpecifier = unitPart[(slashIndex + 1)..];

        if (luminousFluxSpecifier.IsEmpty)
            throw new FormatException("Illuminance format luminous-flux specifier must not be empty.");

        if (areaSpecifier.IsEmpty)
            throw new FormatException("Illuminance format area specifier must not be empty.");

        T value = Left.ValueOf(luminousFluxSpecifier) / Right.ValueOf(areaSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(luminousFluxSpecifier)}/{new string(areaSpecifier)}";
    }
}
