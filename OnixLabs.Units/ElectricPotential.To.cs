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

public readonly partial struct ElectricPotential<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;energy&gt;/&lt;charge&gt;[:scale]</c> (e.g. <c>kg*m/s²*m/A*s:3</c> for volts).
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
                throw new FormatException("ElectricPotential format scale must contain only decimal digits.");
        }

        // Named-unit alias (V, kV, mV, MV, ...).
        if (NamedUnitAlias.TryMatch<T>(unitPart, NamedSymbol, out T aliasMultiplier, out string renderedSymbol))
        {
            T aliasValue = SIBaseValue * aliasMultiplier;
            T aliasRounded = scale > 0 ? T.Round(aliasValue, scale) : aliasValue;
            return $"{aliasRounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {renderedSymbol}";
        }

        // Energy-spec contains '/' (via its inner acceleration), charge-spec uses '*' only. Split on last '/'.
        int lastSlash = unitPart.LastIndexOf('/');
        if (lastSlash < 0)
            throw new FormatException($"ElectricPotential format must contain a '/' separator between energy and charge (e.g. 'kg*m/s²*m/A*s').");

        ReadOnlySpan<char> energySpecifier = unitPart[..lastSlash];
        ReadOnlySpan<char> chargeSpecifier = unitPart[(lastSlash + 1)..];

        if (energySpecifier.IsEmpty)
            throw new FormatException("ElectricPotential format energy specifier must not be empty.");

        if (chargeSpecifier.IsEmpty)
            throw new FormatException("ElectricPotential format charge specifier must not be empty.");

        T value = Left.ValueOf(energySpecifier) / Right.ValueOf(chargeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(energySpecifier)}/{new string(chargeSpecifier)}";
    }
}
