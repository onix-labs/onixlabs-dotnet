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

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Parses the specified value based on the specified number style.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">The number style of the value to parse.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">if the input value is not in a valid format.</exception>
    public BigDecimal Parse(ReadOnlySpan<char> value, NumberStyles style)
    {
        value = Sanitize(value);

        if (style == NumberStyles.None)
        {
            style = DetectNumberStyle(value);
        }

        return style switch
        {
            NumberStyles.Float => ParseFloat(value),
            NumberStyles.Integer => ParseInteger(value),
            NumberStyles.HexNumber => ParseHexadecimal(value),
            _ => throw new FormatException("Input value is not in a valid format.")
        };
    }
}
