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

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Trims any trailing zeros from the fractional part of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value from which to trim trailing zeros.</param>
    /// <returns>Returns a new <see cref="BigDecimal"/> excluding any trailing zeros.</returns>
    public static BigDecimal TrimTrailingZeros(BigDecimal value)
    {
        int exponent = 0;
        while (value.NumberInfo.UnscaledValue % BigInteger.Pow(10, exponent) == 0) exponent++;
        return new BigDecimal(value.NumberInfo.UnscaledValue / BigInteger.Pow(10, --exponent), value.NumberInfo.Scale - exponent);
    }

    /// <summary>
    /// Trims any trailing zeros from the fractional part of the current <see cref="BigDecimal"/> value.
    /// </summary>
    /// <returns>Returns a new <see cref="BigDecimal"/> excluding any trailing zeros.</returns>
    public BigDecimal TrimTrailingZeros()
    {
        return TrimTrailingZeros(this);
    }
}
