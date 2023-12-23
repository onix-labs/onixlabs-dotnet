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
using System.Linq;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    private bool TrySanitizeNumber(ref ReadOnlySpan<char> value, out int sign, out int exponent)
    {
        sign = 1;
        exponent = 0;

        bool hasParentheses = false;
        bool hasLeadingPositiveSign = false;
        bool hasLeadingNegativeSign = false;
        bool hasTrailingPositiveSign = false;
        bool hasTrailingNegativeSign = false;

        // Trim overall leading and trailing whitespace
        if (!TryTrimLeadingWhitespace(ref value)) return false;
        if (!TryTrimTrailingWhitespace(ref value)) return false;
        
        if (!TryTrimLeadingPositiveSign(ref value, out hasLeadingPositiveSign)) return false;
        if (!TryTrimTrailingPositiveSign(ref value, out hasTrailingPositiveSign)) return false;
        
        switch (numberFormatInfo.NumberNegativePattern)
        {
            case 0: // (n)
                if (!TryTrimParentheses(ref value, out hasParentheses)) return false;
                break;
            case 1: // -n
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                break;
            case 2: // - n
                if (!TryTrimLeadingNegativeSign(ref value, out hasLeadingNegativeSign)) return false;
                if (!TryTrimLeadingWhitespace(ref value)) return false;
                break;
            case 3: // n-
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                break;
            case 4:
                if (!TryTrimTrailingNegativeSign(ref value, out hasTrailingNegativeSign)) return false;
                if (!TryTrimTrailingWhitespace(ref value)) return false;
                break;
        }
        
        // Trim whitespace that appears after a leading positive or negative sign
        if (!TryTrimLeadingWhitespace(ref value)) return false;
        if (!TryTrimTrailingWhitespace(ref value)) return false;

        if (!TryTrimExponent(ref value, out exponent)) return false;
        
        // Trim whitespace that appears between the number and an exponent
        if (!TryTrimTrailingWhitespace(ref value)) return false;

        bool[] values = [hasParentheses, hasLeadingPositiveSign, hasLeadingNegativeSign, hasTrailingPositiveSign, hasTrailingNegativeSign];
        if (values.Count(value => value) > 1) return false;

        if (hasParentheses || hasLeadingNegativeSign || hasTrailingNegativeSign) sign = -1;
        return true;
    }
}
