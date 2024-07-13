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

using System.Numerics;
using OnixLabs.Core.Text;

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoFormatter
{
    /// <summary>
    /// Formats the current <see cref="NumberInfo"/> value using the currency format.
    /// </summary>
    private void FormatCurrency()
    {
        FormatInteger(numberFormat.CurrencyGroupSizes, numberFormat.CurrencyGroupSeparator);
        FormatFraction(numberFormat.CurrencyDecimalSeparator);
        FormatCurrencyPositivePattern();
        FormatCurrencyNegativePattern();
    }

    /// <summary>
    /// Applies currency positive pattern formatting to the current <see cref="NumberInfo"/> being formatted.
    /// </summary>
    private void FormatCurrencyPositivePattern()
    {
        if (BigInteger.IsNegative(value.UnscaledValue)) return;

        switch (numberFormat.CurrencyPositivePattern)
        {
            case 0: // $n
                builder.Prepend(numberFormat.CurrencySymbol);
                break;
            case 1: // n$
                builder.Append(numberFormat.CurrencySymbol);
                break;
            case 2: // $ n
                builder.Prepend(Whitespace).Prepend(numberFormat.CurrencySymbol);
                break;
            case 3: // n $
                builder.Append(Whitespace).Append(numberFormat.CurrencySymbol);
                break;
        }
    }

    /// <summary>
    /// Applies currency negative pattern formatting to the current <see cref="NumberInfo"/> being formatted.
    /// </summary>
    private void FormatCurrencyNegativePattern()
    {
        if (BigInteger.IsPositive(value.UnscaledValue)) return;

        switch (numberFormat.CurrencyNegativePattern)
        {
            case 0: // ($n)
                builder.Prepend(numberFormat.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 1: // -$n
                builder.Prepend(numberFormat.CurrencySymbol).Prepend(numberFormat.NegativeSign);
                break;
            case 2: // $-n
                builder.Prepend(numberFormat.NegativeSign).Prepend(numberFormat.CurrencySymbol);
                break;
            case 3: // $n-
                builder.Prepend(numberFormat.CurrencySymbol).Append(numberFormat.NegativeSign);
                break;
            case 4: // (n$)
                builder.Append(numberFormat.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 5: // -n$
                builder.Prepend(numberFormat.NegativeSign).Append(numberFormat.CurrencySymbol);
                break;
            case 6: // n-$
                builder.Append(numberFormat.NegativeSign).Append(numberFormat.CurrencySymbol);
                break;
            case 7: // n$-
                builder.Append(numberFormat.CurrencySymbol).Append(numberFormat.NegativeSign);
                break;
            case 8: // -n $
                builder.Prepend(numberFormat.NegativeSign).Append(Whitespace).Append(numberFormat.CurrencySymbol);
                break;
            case 9: // -$ n
                builder.Prepend(Whitespace).Prepend(numberFormat.CurrencySymbol).Prepend(numberFormat.NegativeSign);
                break;
            case 10: // n $-
                builder.Append(Whitespace).Append(numberFormat.CurrencySymbol).Append(numberFormat.NegativeSign);
                break;
            case 11: // $ n-
                builder.Prepend(Whitespace).Prepend(numberFormat.CurrencySymbol).Append(numberFormat.NegativeSign);
                break;
            case 12: // $ -n
                builder.Prepend(numberFormat.NegativeSign).Prepend(Whitespace).Prepend(numberFormat.CurrencySymbol);
                break;
            case 13: // n- $
                builder.Append(numberFormat.NegativeSign).Append(Whitespace).Append(numberFormat.CurrencySymbol);
                break;
            case 14: // ($ n)
                builder.Prepend(Whitespace).Prepend(numberFormat.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 15: // (n $)
                builder.Append(Whitespace).Append(numberFormat.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 16: // $- n
                builder.Prepend(Whitespace).Prepend(numberFormat.NegativeSign).Prepend(numberFormat.CurrencySymbol);
                break;
        }
    }
}
