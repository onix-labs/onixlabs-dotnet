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
using System.Runtime.CompilerServices;
using OnixLabs.Core.Text;

namespace OnixLabs.Numerics;

internal sealed partial class BigDecimalFormatter
{
    /// <summary>
    /// Formats the <see cref="BigDecimal"/> value in exponential, or scientific notation.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatExponential(char specifier)
    {
        builder.Append(BigInteger.Abs(value.NumberInfo.UnscaledValue));

        if(BigDecimal.IsZero(value)) return;

        int exponent = builder.Length - value.NumberInfo.Scale - 1;
        builder.Trim('0').Insert(1, numberFormat.NumberDecimalSeparator).TrimEnd(numberFormat.NumberDecimalSeparator);

        if (exponent == 0) return;

        string sign = exponent > 0 ? numberFormat.PositiveSign : numberFormat.NegativeSign;
        builder.Append(specifier, sign, int.Abs(exponent));

        if (value < 0) builder.Prepend(numberFormat.NegativeSign);
    }
}
