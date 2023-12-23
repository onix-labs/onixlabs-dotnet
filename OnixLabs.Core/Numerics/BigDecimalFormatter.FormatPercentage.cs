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
    /// Formats the <see cref="BigDecimal"/> value as a percentage.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatPercent()
    {
        FormatIntegerComponent(info.PercentGroupSizes[0], info.PercentGroupSeparator);
        FormatFractionComponent(info.PercentDecimalSeparator);
        FormatPercentPositivePattern();
        FormatPercentNegativePattern();
    }

    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value using a positive percentage pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatPercentPositivePattern()
    {
        if (BigDecimal.IsNegative(value)) return;

        switch (info.PercentPositivePattern)
        {
            case 0: // n %
                builder.Append(Whitespace, info.PercentSymbol);
                break;
            case 1: //  n%
                builder.Append(info.PercentSymbol);
                break;
            case 2: // %n
                builder.Prepend(info.PercentSymbol);
                break;
            case 4: // % n
                builder.Prepend(info.PercentSymbol, Whitespace);
                break;
        }
    }

    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value using a negative percentage pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatPercentNegativePattern()
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (info.PercentNegativePattern)
        {
            case 0: // -n %
                builder.Prepend(info.NegativeSign).Append(Whitespace, info.PercentSymbol);
                break;
            case 1: // -n%
                builder.Prepend(info.NegativeSign).Append(info.PercentSymbol);
                break;
            case 2: // -%n
                builder.Prepend(info.NegativeSign, info.PercentSymbol);
                break;
            case 3: // %-n
                builder.Prepend(info.PercentSymbol, info.NegativeSign);
                break;
            case 4: // %n-
                builder.Prepend(info.PercentSymbol).Append(info.NegativeSign);
                break;
            case 5: // n-%
                builder.Append(info.NegativeSign, info.PercentSymbol);
                break;
            case 6: // n%-
                builder.Append(info.PercentSymbol, info.NegativeSign);
                break;
            case 7: // -% n
                builder.Prepend(info.NegativeSign, info.PercentSymbol);
                break;
            case 8: // n %-
                builder.Append(Whitespace, info.PercentSymbol, info.NegativeSign);
                break;
            case 9: // % n-
                builder.Prepend(info.PercentSymbol, Whitespace).Append(info.NegativeSign);
                break;
            case 10: // % -n
                builder.Prepend(info.PercentSymbol, Whitespace, info.NegativeSign);
                break;
            case 11: // n- %
                builder.Append(info.NegativeSign, Whitespace, info.PercentSymbol);
                break;
        }
    }
}
