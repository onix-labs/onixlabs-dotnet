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
    /// Valid formats for <see cref="Temperature{T}"/> include:
    /// <list type="table">
    /// <listheader><term>Format</term><description>Description</description></listheader>
    /// <item><term>K</term><description>Kelvin</description></item>
    /// <item><term>C</term><description>Celsius</description></item>
    /// <item><term>D</term><description>Delisle</description></item>
    /// <item><term>F</term><description>Fahrenheit</description></item>
    /// <item><term>N</term><description>Newton</description></item>
    /// <item><term>R</term><description>Reaumur</description></item>
    /// <item><term>Ra</term><description>Rankine</description></item>
    /// </list> 
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        string formatOrDefault = format ?? DefaultFormat;
        IFormatProvider formatProviderOrDefault = formatProvider ?? CultureInfo.CurrentCulture;

        T value = formatOrDefault switch
        {
            "K" => Kelvin,
            "C" => Celsius,
            "D" => Delisle,
            "F" => Fahrenheit,
            "N" => Newton,
            "R" => Reaumur,
            "Ra" => Rankine,
            _ => throw new ArgumentException($"Invalid format specifier: {format}.")
        };

        return $"{value.ToString("R", formatProviderOrDefault)}°{formatOrDefault}";
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// Valid formats for <see cref="Temperature{T}"/> include:
    /// <list type="table">
    /// <listheader><term>Format</term><description>Description</description></listheader>
    /// <item><term>K</term><description>Kelvin</description></item>
    /// <item><term>C</term><description>Celsius</description></item>
    /// <item><term>D</term><description>Delisle</description></item>
    /// <item><term>F</term><description>Fahrenheit</description></item>
    /// <item><term>N</term><description>Newton</description></item>
    /// <item><term>R</term><description>Reaumur</description></item>
    /// <item><term>Ra</term><description>Rankine</description></item>
    /// </list> 
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        return ToString(format.ToString(), formatProvider);
    }

    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    /// <exception cref="ArgumentException">If the specified format is invalid.</exception>
    public override string ToString()
    {
        return ToString(DefaultFormat);
    }
}
