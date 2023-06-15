// Copyright 2020-2023 ONIXLabs
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
using System.Text.RegularExpressions;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Detects the <see cref="NumberStyles"/> of the specified value.
    /// </summary>
    /// <param name="value">The value from which to detect <see cref="NumberStyles"/>.</param>
    /// <returns>
    /// Returns one of the following values:
    /// <list type="bullet">
    /// <item><see cref="NumberStyles.Integer"/> if the value is detected as an integral number.</item>
    /// <item><see cref="NumberStyles.HexNumber"/> if the value is detected as a hexadecimal number.</item>
    /// <item><see cref="NumberStyles.Float"/> if the value is detected as a floating point number.</item>
    /// </list>
    /// </returns>
    private NumberStyles DetectNumberStyle(ReadOnlySpan<char> value)
    {
        string separatorPattern = decimalPoint.Length switch
        {
            0 => "\\.?",
            1 => $"\\{decimalPoint}?",
            _ => $"(?:{decimalPoint})?"
        };

        string negativeSignPattern = negativeSign.Length switch
        {
            0 => "[-]",
            1 => $"[{negativeSign}]",
            _ => $"{negativeSign}"
        };

        string positiveSignPattern = positiveSign.Length switch
        {
            0 => "[+]",
            1 => $"[{positiveSign}]",
            _ => $"{positiveSign}"
        };

        string signPattern = $"(?:{negativeSignPattern}|{positiveSignPattern})?";

        Regex hexPattern = new("^([0-9a-fA-F]{2})+$");
        Regex intPattern = new($"^{signPattern}[0-9]+{signPattern}$");
        Regex decPattern = new($"^{signPattern}[0-9]*{separatorPattern}[0-9]+([eE]{signPattern}[0-9]+)?{signPattern}$");

        if (intPattern.IsMatch(value)) return NumberStyles.Integer;
        if (hexPattern.IsMatch(value)) return NumberStyles.HexNumber;
        if (decPattern.IsMatch(value)) return NumberStyles.Float;
        return NumberStyles.None;
    }
}
