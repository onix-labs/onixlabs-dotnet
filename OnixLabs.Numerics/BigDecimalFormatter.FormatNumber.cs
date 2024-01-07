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

namespace OnixLabs.Numerics;

internal sealed partial class BigDecimalFormatter
{
    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value as a number value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatNumber()
    {
        FormatInteger(numberFormat.NumberGroupSizes, numberFormat.NumberGroupSeparator);
        FormatFraction(numberFormat.NumberDecimalSeparator);
        FormatNumberNegativePattern();
    }

    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value using a negative number pattern.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatNumberNegativePattern()
    {
        if (BigDecimal.IsPositive(value)) return;

        switch (numberFormat.NumberNegativePattern)
        {
            case 0: // (n)
                builder.Wrap(LeadingParenthesis, TrailingParenthesis);
                break;
            case 1: // -n
                builder.Prepend(numberFormat.NegativeSign);
                break;
            case 2: // - n
                builder.Prepend(numberFormat.NegativeSign, Whitespace);
                break;
            case 3: // n-
                builder.Append(numberFormat.NegativeSign);
                break;
            case 4: // n -
                builder.Append(Whitespace, numberFormat.NegativeSign);
                break;
        }
    }
}