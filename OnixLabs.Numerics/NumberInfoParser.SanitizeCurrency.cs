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

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoParser
{
    private bool TrySanitizeCurrency(ref ReadOnlySpan<char> value, out int sign)
    {
        sign = 1;

        bool hasParentheses = false;
        bool hasLeadingNegativeSign = false;
        bool hasTrailingNegativeSign = false;

        if (!TryTrimLeadingWhitespace(ref value)) return false;
        if (!TryTrimTrailingWhitespace(ref value)) return false;

        switch (numberFormat.CurrencyPositivePattern)
        {
            case 0: // $n
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                break;
            case 1: // n$
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                break;
            case 2: // $ n
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                break;
            case 3: // n $
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                break;
        }

        switch (numberFormat.CurrencyNegativePattern)
        {
            case 0: // ($n)
                if (!TryTrimParentheses(ref value, out hasParentheses)) return false;
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                break;
            case 1: // -$n
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                break;
            case 2: // $-n
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                break;
            case 3: // $n-
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                break;
            case 4: // (n$)
                if (!TryTrimParentheses(ref value, out hasParentheses)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                break;
            case 5: // -n$
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                break;
            case 6: // n-$
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                break;
            case 7: // n$-
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                break;
            case 8: // -n $
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                break;
            case 9: // -$ n
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                break;
            case 10: // n $-
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                break;
            case 11: // $ n-
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                break;
            case 12: // $ -n
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                break;
            case 13: // n- $
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                break;
            case 14: // ($ n)
                if (!TryTrimParentheses(ref value, out hasParentheses)) return false;
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                break;
            case 15: // (n $)
                if (!TryTrimParentheses(ref value, out hasParentheses)) return false;
                if (!TryTrimTrailingCurrencySymbol(ref value)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                break;
            case 16: // $- n
                if (!TryTrimLeadingCurrencySymbol(ref value)) return false;
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                break;
        }

        int count = 0;
        if (hasParentheses) count++;
        if (hasLeadingNegativeSign) count++;
        if (hasTrailingNegativeSign) count++;
        if (count > 1) return false;

        if (hasParentheses || hasLeadingNegativeSign || hasTrailingNegativeSign) sign = -1;
        return true;
    }
}
