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
    private BigDecimal ParseFloat(ReadOnlySpan<char> value)
    {
        try
        {
            StringBuilder integerDigits = new();
            StringBuilder exponentDigits = new();
            int integerSign = GetSign(value);
            int exponentSign = 1;
            bool hasDecimalPoint = false;
            bool hasExponent = false;
            int scale = 0;

            value = value.Trim(negativeSign).Trim(positiveSign);

            while (value.Length > 0)
            {
                if (value.StartsWith(decimalPoint))
                {
                    hasDecimalPoint = true;
                    value = value[1..];
                    continue;
                }

                if (value.StartsWith("e") || value.StartsWith("E"))
                {
                    hasExponent = true;
                    value = value[1..];
                    continue;
                }

                if (value.StartsWith(negativeSign))
                {
                    exponentSign = -1;
                    value = value[1..];
                    continue;
                }

                if (char.IsDigit(value[0]))
                {
                    if (hasDecimalPoint && !hasExponent)
                    {
                        scale++;
                    }

                    StringBuilder builder = hasExponent ? exponentDigits : integerDigits;
                    builder.Append(value[0]);
                }

                value = value[1..];
            }

            BigInteger integer = BigInteger.Parse(integerDigits.ToString());
            BigDecimal result = integer.ToBigDecimal(scale, ScaleMode.Fractional) * integerSign;

            if (!hasExponent) return result;

            int exponent = int.Parse(exponentDigits.ToString()) * exponentSign;
            BigDecimal power = BigDecimal.Pow(BigDecimal.Ten, exponent);

            result *= power;

            if (result.FractionalValue == BigInteger.Zero) result = result.FractionalValue;

            return result;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }
}
