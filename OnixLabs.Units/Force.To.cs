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

public readonly partial struct Force<T>
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
    /// The format to use. Product specifiers take the form <c>&lt;mass&gt;*&lt;acceleration&gt;[:scale]</c> where
    /// <c>&lt;mass&gt;</c> is a <see cref="Mass{T}"/> specifier and <c>&lt;acceleration&gt;</c> is an
    /// <see cref="Acceleration{T}"/> specifier in either squared or compound form. Examples:
    /// <c>kg*m/s²:3</c> (Newtons), <c>g*cm/s²:3</c> (dynes), <c>lb*ft/s²:3</c> (poundals).
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
                throw new FormatException("Force format scale must contain only decimal digits.");
        }

        // Named-unit alias (N, kN, mN, MN, ...).
        if (NamedUnitAlias.TryMatch<T>(unitPart, NamedSymbol, out T aliasMultiplier, out string renderedSymbol))
        {
            T aliasValue = SIBaseValue * aliasMultiplier;
            T aliasRounded = scale > 0 ? T.Round(aliasValue, scale) : aliasValue;
            return $"{aliasRounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {renderedSymbol}";
        }

        int starIndex = unitPart.IndexOf('*');
        if (starIndex < 0)
            throw new FormatException($"Force format must contain a '*' separator between the mass and acceleration specifiers (e.g. 'kg*m/s²').");

        ReadOnlySpan<char> massSpecifier = unitPart[..starIndex];
        ReadOnlySpan<char> accelerationSpecifier = unitPart[(starIndex + 1)..];

        if (massSpecifier.IsEmpty)
            throw new FormatException("Force format mass specifier must not be empty.");

        if (accelerationSpecifier.IsEmpty)
            throw new FormatException("Force format acceleration specifier must not be empty.");

        T value = Left.ValueOf(massSpecifier) * Right.ValueOf(accelerationSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(massSpecifier)}*{new string(accelerationSpecifier)}";
    }
}
