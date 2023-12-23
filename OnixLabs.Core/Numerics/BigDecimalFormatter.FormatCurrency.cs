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

using System.Runtime.CompilerServices;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalFormatter
{
    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value as currency.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatCurrency()
    {
        FormatIntegerComponent(info.CurrencyGroupSizes[0], info.CurrencyGroupSeparator);
        FormatFractionComponent(info.NumberDecimalSeparator);
        FormatCurrencyPositivePattern();
        FormatCurrencyNegativePattern();
    }

    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value using a positive currency pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatCurrencyPositivePattern()
    {
        if (BigDecimal.IsNegative(value)) return;

        switch (info.CurrencyPositivePattern)
        {
            case 0: // $n
                builder.Prepend(info.CurrencySymbol);
                break;
            case 1: // n$
                builder.Append(info.CurrencySymbol);
                break;
            case 2: // $ n
                builder.Prepend(info.CurrencySymbol, Whitespace);
                break;
            case 3: // n $
                builder.Append(Whitespace, info.CurrencySymbol);
                break;
        }
    }

    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value using a negative currency pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatCurrencyNegativePattern()
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (info.CurrencyNegativePattern)
        {
            case 0: // ($n)
                builder.Prepend(info.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 1: // -$n
                builder.Prepend(info.NegativeSign, info.CurrencySymbol);
                break;
            case 2: // $-n
                builder.Prepend(info.CurrencySymbol, info.NegativeSign);
                break;
            case 3: // $n-
                builder.Prepend(info.CurrencySymbol).Append(info.NegativeSign);
                break;
            case 4: // (n$)
                builder.Append(info.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 5: // -n$
                builder.Prepend(info.NegativeSign).Append(info.CurrencySymbol);
                break;
            case 6: // n-$
                builder.Append(info.NegativeSign, info.CurrencySymbol);
                break;
            case 7: // n$-
                builder.Append(info.CurrencySymbol, info.NegativeSign);
                break;
            case 8: // -n $
                builder.Prepend(info.NegativeSign).Append(Whitespace, info.CurrencySymbol);
                break;
            case 9: // -$ n
                builder.Prepend(info.NegativeSign, info.CurrencySymbol, Whitespace);
                break;
            case 10: // n $-
                builder.Append(Whitespace, info.CurrencySymbol, info.NegativeSign);
                break;
            case 11: // $ n-
                builder.Prepend(info.CurrencySymbol, Whitespace).Append(info.NegativeSign);
                break;
            case 12: // $ -n
                builder.Prepend(info.CurrencySymbol, Whitespace, info.NegativeSign);
                break;
            case 13: // n- $
                builder.Append(info.NegativeSign, Whitespace, info.CurrencySymbol);
                break;
            case 14: // ($ n)
                builder.Prepend(info.CurrencySymbol, Whitespace).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 15: // (n $)
                builder.Append(Whitespace, info.CurrencySymbol).Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 16: // $- n
                builder.Prepend(info.CurrencySymbol, info.NegativeSign, Whitespace);
                break;
        }
    }
}
