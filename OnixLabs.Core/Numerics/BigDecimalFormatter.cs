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
using System.Numerics;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Numerics;

internal static class BigDecimalFormatter
{
    public static string Format(BigDecimal value, string format, CultureInfo info)
    {
        return format.ToUpper() switch
        {
            "C" => FormatCurrency(value, info),
            "D" => FormatDecimal(value, info),
            "E" => FormatScientific(value, info),
            "F" => FormatFixedPoint(value, info),
            "G" => FormatGeneral(value, info),
            "N" => FormatNumberWithThousandSeparator(value, info),
            "P" => FormatPercentage(value, info),
            "X" => FormatHexadecimal(value),
            _ => throw new FormatException($"The specified format is invalid: {format}.")
        };
    }

    private static string FormatCurrency(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatDecimal(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatScientific(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatFixedPoint(BigDecimal value, CultureInfo info)
    {
        string sign = value.Sign < 0 ? info.NumberFormat.NegativeSign : string.Empty;
        string separator = info.NumberFormat.NumberDecimalSeparator;
        string integer = BigInteger.Abs(value.IntegralValue).ToString();
        string fraction = value.FractionalValue.ToString().PadLeft(value.Scale, '0');

        return value.Scale == 0 ? $"{sign}{integer}" : $"{sign}{integer}{separator}{fraction}";
    }

    private static string FormatGeneral(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatNumberWithThousandSeparator(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatPercentage(BigDecimal value, CultureInfo info)
    {
        throw new NotImplementedException();
    }

    private static string FormatHexadecimal(BigDecimal value)
    {
        byte[] bytes = value.ToByteArray();
        return Base16.FromByteArray(bytes).ToString();
    }
}
