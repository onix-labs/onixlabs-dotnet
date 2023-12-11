// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Gets a <see cref="BigDecimal"/> value representing the sign of the specified value.
    /// </summary>
    /// <param name="value">The value from which to obtain a sign.</param>
    /// <returns>
    /// Returns <see cref="BigDecimal.NegativeOne"/> if the specified value contains a leading or trailing negative sign;
    /// otherwise <see cref="BigDecimal.One"/> if the specified value contains a leading or trailing positive sign, or no sign at all.
    /// </returns>
    /// <exception cref="FormatException">if the specified value contains a leading and trailing sign.</exception>
    private int GetSign(ReadOnlySpan<char> value)
    {
        bool hasLeadingSign = value.StartsWith(negativeSign) || value.StartsWith(positiveSign);
        bool hasTrailingSign = value.EndsWith(negativeSign) || value.EndsWith(positiveSign);

        if (hasLeadingSign && hasTrailingSign)
        {
            throw new FormatException("Input value is not in a valid format as it contains a leading and trailing sign specifier.");
        }

        return value.StartsWith(negativeSign) || value.EndsWith(negativeSign) ? -1 : 1;
    }
}
