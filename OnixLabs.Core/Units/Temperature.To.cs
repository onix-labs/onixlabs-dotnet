// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Core.Units;

public readonly partial struct Temperature<T> : IFormattable
{
    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        (string formatOrDefault, T value) = (format ?? DefaultFormat).ToUpper() switch
        {
            "K" => ("K", Kelvin),
            "C" => ("C", Celsius),
            "D" => ("D", Delisle),
            "F" => ("F", Fahrenheit),
            "N" => ("N", Newton),
            "R" => ("R", Reaumur),
            "RA" => ("Ra", Rankine),
            _ => throw new ArgumentException($"Invalid format specifier: {format}")
        };

        string number = value.ToString("R", formatProvider as CultureInfo ?? CultureInfo.CurrentCulture);
        return $"{number}°{formatOrDefault}";
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        return ToString(format.ToString(), formatProvider);
    }

    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>The value of the current instance in the default format.</returns>
    public override string ToString()
    {
        return ToString(DefaultFormat, CultureInfo.CurrentCulture);
    }
}
