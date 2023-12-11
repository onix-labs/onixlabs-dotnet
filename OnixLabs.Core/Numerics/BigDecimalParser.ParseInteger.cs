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
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Formats the specified value as an integer.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">if the input value is not in a valid format.</exception>
    private BigDecimal ParseInteger(ReadOnlySpan<char> value)
    {
        try
        {
            StringBuilder digits = new();
            BigDecimal sign = GetSign(value);

            while (value.Length > 0)
            {
                if (char.IsDigit(value[0]))
                {
                    digits.Append(value[0]);
                }

                value = value[1..];
            }

            BigInteger integer = BigInteger.Parse(digits.ToString());

            return integer.ToBigDecimal(0) * sign;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }
}
