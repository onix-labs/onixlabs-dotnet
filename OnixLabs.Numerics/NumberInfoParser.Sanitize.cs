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
using static System.Globalization.NumberStyles;

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoParser
{
    private bool TryTrimLeadingWhitespace(ref ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> result = value.TrimStart();

        if (result.SequenceEqual(value)) return true;
        if ((style & AllowLeadingWhite) is 0) return false;

        value = result;
        return true;
    }

    private bool TryTrimTrailingWhitespace(ref ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> result = value.TrimEnd();

        if (result.SequenceEqual(value)) return true;
        if ((style & AllowTrailingWhite) is 0) return false;

        value = result;
        return true;
    }

    private bool TryTrimLeadingCurrencySymbol(ref ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> result = value.TrimStart(numberFormat.CurrencySymbol);

        if (result.SequenceEqual(value)) return true;
        if ((style & AllowCurrencySymbol) is 0) return false;

        value = result;
        return true;
    }

    private bool TryTrimTrailingCurrencySymbol(ref ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> result = value.TrimEnd(numberFormat.CurrencySymbol);

        if (result.SequenceEqual(value)) return true;
        if ((style & AllowCurrencySymbol) is 0) return false;

        value = result;
        return true;
    }

    private bool TryTrimLeadingPositiveSign(ref ReadOnlySpan<char> value, out bool hasLeadingPositiveSign)
    {
        hasLeadingPositiveSign = value.StartsWith(numberFormat.PositiveSign, Comparison);

        if (!hasLeadingPositiveSign) return true;
        if ((style & AllowLeadingSign) is 0) return false;

        value = value.TrimStart(numberFormat.PositiveSign);
        return true;
    }

    private bool TryTrimTrailingPositiveSign(ref ReadOnlySpan<char> value, out bool hasTrailingPositiveSign)
    {
        hasTrailingPositiveSign = value.EndsWith(numberFormat.PositiveSign, Comparison);

        if (!hasTrailingPositiveSign) return true;
        if ((style & AllowTrailingSign) is 0) return false;

        value = value.TrimStart(numberFormat.PositiveSign);
        return true;
    }

    private bool TryTrimLeadingNegativeSign(ref ReadOnlySpan<char> value, out bool hasLeadingNegativeSign)
    {
        hasLeadingNegativeSign = value.StartsWith(numberFormat.NegativeSign, Comparison);

        if (!hasLeadingNegativeSign) return true;
        if ((style & AllowLeadingSign) is 0) return false;

        value = value.TrimStart(numberFormat.NegativeSign);
        return true;
    }

    private bool TryTrimTrailingNegativeSign(ref ReadOnlySpan<char> value, out bool hasTrailingNegativeSign)
    {
        hasTrailingNegativeSign = value.EndsWith(numberFormat.NegativeSign, Comparison);

        if (!hasTrailingNegativeSign) return true;
        if ((style & AllowTrailingSign) is 0) return false;

        value = value.TrimStart(numberFormat.NegativeSign);
        return true;
    }

    private bool TryTrimParentheses(ref ReadOnlySpan<char> value, out bool hasParentheses)
    {
        hasParentheses = false;
        bool hasLeadingParenthesis = value.StartsWith(LeadingParenthesis, Comparison);
        bool hasTrailingParenthesis = value.EndsWith(TrailingParenthesis, Comparison);
        bool areParenthesesAllowed = (style & AllowParentheses) is not 0;

        // ReSharper disable once InvertIf
        if (hasLeadingParenthesis && hasTrailingParenthesis && areParenthesesAllowed)
        {
            hasParentheses = true;
            value = value.TrimStart(LeadingParenthesis).TrimEnd(TrailingParenthesis);
            return true;
        }

        return !hasLeadingParenthesis && !hasTrailingParenthesis;
    }

    private bool TryTrimExponent(ref ReadOnlySpan<char> value, out int exponent)
    {
        exponent = 0;

        int index = value.IndexOf(ExponentSymbol, Comparison);
        int lastIndex = value.LastIndexOf(ExponentSymbol, Comparison);

        if (index == -1) return true;
        if (index != lastIndex) return false;

        ReadOnlySpan<char> chars = value[(index + 1)..];
        value = value[..index];

        return int.TryParse(chars, culture, out exponent) && (style & AllowExponent) is not 0;
    }
}
