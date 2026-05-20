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

public readonly partial struct ElectricCapacitance<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;charge&gt;/&lt;potential&gt;[:scale]</c>
    /// (e.g. <c>A*s/kg*m/s²*m/A*s:3</c> for farads).
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
                throw new FormatException("ElectricCapacitance format scale must contain only decimal digits.");
        }

        // Named-unit alias (F, mF, μF, nF, pF, kF, ...).
        if (NamedUnitAlias.TryMatch<T>(unitPart, NamedSymbol, out T aliasMultiplier, out string renderedSymbol))
        {
            T aliasValue = Magnitude * aliasMultiplier;
            T aliasRounded = scale > 0 ? T.Round(aliasValue, scale) : aliasValue;
            return $"{aliasRounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {renderedSymbol}";
        }

        // Charge-spec uses '*' only (e.g. "A*s"); potential-spec contains '/' internally. The FIRST '/' cleanly
        // separates the charge half from the potential half.
        int firstSlash = unitPart.IndexOf('/');
        if (firstSlash < 0)
            throw new FormatException($"ElectricCapacitance format must contain a '/' separator between charge and electric-potential (e.g. 'A*s/kg*m/s²*m/A*s').");

        ReadOnlySpan<char> chargeSpecifier = unitPart[..firstSlash];
        ReadOnlySpan<char> potentialSpecifier = unitPart[(firstSlash + 1)..];

        if (chargeSpecifier.IsEmpty)
            throw new FormatException("ElectricCapacitance format charge specifier must not be empty.");

        if (potentialSpecifier.IsEmpty)
            throw new FormatException("ElectricCapacitance format electric-potential specifier must not be empty.");

        T value = Left.ValueOf(chargeSpecifier) / Right.ValueOf(potentialSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(chargeSpecifier)}/{new string(potentialSpecifier)}";
    }
}
