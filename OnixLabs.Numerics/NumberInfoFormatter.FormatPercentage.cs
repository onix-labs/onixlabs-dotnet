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
    /// Formats the current <see cref="NumberInfo"/> value using the percentage format.
    /// </summary>
    private void FormatPercent()
    {
        FormatInteger(numberFormat.PercentGroupSizes, numberFormat.PercentGroupSeparator);
        FormatFraction(numberFormat.PercentDecimalSeparator);
        FormatPercentPositivePattern();
        FormatPercentNegativePattern();
    }

    /// <summary>
    /// Applies percent positive pattern formatting to the current <see cref="NumberInfo"/> being formatted.
    /// </summary>
    private void FormatPercentPositivePattern()
    {
        if (BigInteger.IsNegative(value.UnscaledValue)) return;

        switch (numberFormat.PercentPositivePattern)
        {
            case 0: // n %
                builder.Append(Whitespace, numberFormat.PercentSymbol);
                break;
            case 1: //  n%
                builder.Append(numberFormat.PercentSymbol);
                break;
            case 2: // %n
                builder.Prepend(numberFormat.PercentSymbol);
                break;
            case 3: // % n
                builder.Prepend(numberFormat.PercentSymbol, Whitespace);
                break;
        }
    }

    /// <summary>
    /// Applies percent negative pattern formatting to the current <see cref="NumberInfo"/> being formatted.
    /// </summary>
    private void FormatPercentNegativePattern()
    {
        if (BigInteger.IsPositive(value.UnscaledValue)) return;

        switch (numberFormat.PercentNegativePattern)
        {
            case 0: // -n %
                builder.Prepend(numberFormat.NegativeSign).Append(Whitespace, numberFormat.PercentSymbol);
                break;
            case 1: // -n%
                builder.Prepend(numberFormat.NegativeSign).Append(numberFormat.PercentSymbol);
                break;
            case 2: // -%n
                builder.Prepend(numberFormat.NegativeSign, numberFormat.PercentSymbol);
                break;
            case 3: // %-n
                builder.Prepend(numberFormat.PercentSymbol, numberFormat.NegativeSign);
                break;
            case 4: // %n-
                builder.Prepend(numberFormat.PercentSymbol).Append(numberFormat.NegativeSign);
                break;
            case 5: // n-%
                builder.Append(numberFormat.NegativeSign, numberFormat.PercentSymbol);
                break;
            case 6: // n%-
                builder.Append(numberFormat.PercentSymbol, numberFormat.NegativeSign);
                break;
            case 7: // -% n
                builder.Prepend(numberFormat.NegativeSign, numberFormat.PercentSymbol, Whitespace);
                break;
            case 8: // n %-
                builder.Append(Whitespace, numberFormat.PercentSymbol, numberFormat.NegativeSign);
                break;
            case 9: // % n-
                builder.Prepend(numberFormat.PercentSymbol, Whitespace).Append(numberFormat.NegativeSign);
                break;
            case 10: // % -n
                builder.Prepend(numberFormat.PercentSymbol, Whitespace, numberFormat.NegativeSign);
                break;
            case 11: // n- %
                builder.Append(numberFormat.NegativeSign, Whitespace, numberFormat.PercentSymbol);
                break;
        }
    }
}
