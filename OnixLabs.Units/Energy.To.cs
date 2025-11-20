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
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

public readonly partial struct Energy<T>
{
/// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the default format.</returns>
    public override string ToString() => ToString(JoulesSpecifier);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        (string specifier, int scale) = format.GetSpecifierAndScale(defaultSpecifier: JoulesSpecifier);

        (T value, string symbol) = specifier.ToUpperInvariant() switch
        {
            JoulesSpecifier => (Joules, JoulesSymbol),
            KiloJoulesSpecifier => (KiloJoules, KiloJoulesSymbol),
            MegaJoulesSpecifier => (MegaJoules, MegaJoulesSymbol),
            GigaJoulesSpecifier => (GigaJoules, GigaJoulesSymbol),
            CaloriesSpecifier => (Calories, CaloriesSymbol),
            KiloCaloriesSpecifier => (KiloCalories, KiloCaloriesSymbol),
            WattHoursSpecifier => (WattHours, WattHoursSymbol),
            KiloWattHoursSpecifier => (KiloWattHours, KiloWattHoursSymbol),
            ErgsSpecifier => (Ergs, ErgsSymbol),
            BritishThermalUnitsSpecifier => (BritishThermalUnits, BritishThermalUnitsSymbol),
            FootPoundsSpecifier => (FootPounds, FootPoundsSymbol),
            ElectronVoltsSpecifier => (ElectronVolts, ElectronVoltsSymbol),
            _ => throw new ArgumentException(
                $"Format '{format.ToString()}' is invalid. Valid format specifiers are: " +
                "J, KJ, MJ, GJ, CAL, KCAL, WH, KWH, ERG, BTU, FTLB, EV. " +
                "Format specifiers may also be suffixed with a scale value.",
                nameof(format))
        };

        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} {symbol}";
    }
}
