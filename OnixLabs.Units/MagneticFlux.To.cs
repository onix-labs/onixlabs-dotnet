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

public readonly partial struct MagneticFlux<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;potential&gt;*&lt;time&gt;[:scale]</c> (e.g. <c>kg*m/s²*m/A*s*s:3</c> for webers).
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
                throw new FormatException("MagneticFlux format scale must contain only decimal digits.");
        }

        // Named-unit alias (Wb, mWb, μWb, kWb, ...).
        if (NamedUnitAlias.TryMatch<T>(unitPart, NamedSymbol, out T aliasMultiplier, out string renderedSymbol))
        {
            T aliasValue = SIBaseValue * aliasMultiplier;
            T aliasRounded = scale > 0 ? T.Round(aliasValue, scale) : aliasValue;
            return $"{aliasRounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {renderedSymbol}";
        }

        // ElectricPotential-spec contains '*' (e.g. "kg*m/s²*m/A*s"); time-spec is a bare scale token. Split on the
        // LAST '*' — everything before is the potential-spec, everything after is the time-spec.
        int lastStar = unitPart.LastIndexOf('*');
        if (lastStar < 0)
            throw new FormatException($"MagneticFlux format must contain a '*' separator between electric-potential and time (e.g. 'kg*m/s²*m/A*s*s').");

        ReadOnlySpan<char> potentialSpecifier = unitPart[..lastStar];
        ReadOnlySpan<char> timeSpecifier = unitPart[(lastStar + 1)..];

        if (potentialSpecifier.IsEmpty)
            throw new FormatException("MagneticFlux format electric-potential specifier must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("MagneticFlux format time specifier must not be empty.");

        T value = Left.ValueOf(potentialSpecifier) * Right.ValueOf(timeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(potentialSpecifier)}*{new string(timeSpecifier)}";
    }
}
