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

public readonly partial struct BigDecimal
{
    public static BigDecimal Parse(string value)
    {
        return Parse(value.AsSpan());
    }

    public static BigDecimal Parse(string value, IFormatProvider? provider)
    {
        return Parse(value.AsSpan(), provider);
    }

    public static BigDecimal Parse(string value, NumberStyles style, IFormatProvider? provider)
    {
        return Parse(value.AsSpan(), style, provider);
    }

    public static BigDecimal Parse(ReadOnlySpan<char> value)
    {
        return Parse(value, CurrentCultureNumberFormat);
    }

    public static BigDecimal Parse(ReadOnlySpan<char> value, IFormatProvider? provider)
    {
        return Parse(value, DefaultNumberStyles, provider);
    }

    public static BigDecimal Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider)
    {
        NumberFormatInfo info = NumberFormatInfo.GetInstance(provider);
        BigDecimalParser parser = new(info);

        return parser.Parse(value, style);
    }

    public static bool TryParse(string? value, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), out result);
    }

    public static bool TryParse(string? value, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), provider, out result);
    }

    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), style, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> value, out BigDecimal result)
    {
        return TryParse(value, CurrentCultureNumberFormat, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out BigDecimal result)
    {
        return TryParse(value, DefaultNumberStyles, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out BigDecimal result)
    {
        try
        {
            result = Parse(value, style, provider);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
