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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the modulus of the specified <see cref="BigDecimal"/> values by dividing the left-hand value by the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the modulus of the specified <see cref="BigDecimal"/> values by dividing the left-hand value by the right-hand value.</returns>
    public static BigDecimal Mod(BigDecimal left, BigDecimal right)
    {
        int scale = MaxScale(left, right);

        (BigInteger dividend, BigInteger divisor) = NormalizeUnscaledValues(left, right);
        BigInteger remainder = BigInteger.Remainder(dividend, divisor);

        return new BigDecimal(remainder, scale);
    }

    /// <summary>
    /// Gets the modulus of the specified <see cref="BigDecimal"/> values by dividing the left-hand value by the right-hand value.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the modulus of the specified <see cref="BigDecimal"/> values by dividing the left-hand value by the right-hand value.</returns>
    public static BigDecimal operator %(BigDecimal left, BigDecimal right)
    {
        return Mod(left, right);
    }
}
