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
    /// Formats the current <see cref="NumberInfo"/> value using the exponential (otherwise known as scientific notation) format.
    /// </summary>
    /// <param name="specifier">The exponentiation specifier, which is either an uppercase E, or lowercase e.</param>
    private void FormatExponential(char specifier)
    {
        builder.Append(BigInteger.Abs(value.UnscaledValue));

        if (value == NumberInfo.Zero) return;

        int exponent = builder.Length - value.Scale - 1;
        builder.Trim('0').Insert(1, numberFormat.NumberDecimalSeparator).TrimEnd(numberFormat.NumberDecimalSeparator);

        if (exponent == 0) return;

        string sign = exponent > 0 ? numberFormat.PositiveSign : numberFormat.NegativeSign;
        builder.Append(specifier, sign, int.Abs(exponent));

        if (value.UnscaledValue < BigInteger.Zero) builder.Prepend(numberFormat.NegativeSign);
    }
}
