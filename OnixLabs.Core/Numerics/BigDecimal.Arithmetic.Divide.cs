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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Divide(BigDecimal left, BigDecimal right)
    {
        (BigInteger leftBalanced, BigInteger rightBalanced) = BalanceUnscaled(left, right);
        BigInteger quotient = BigInteger.DivRem(leftBalanced, rightBalanced, out BigInteger remainder);

        for (int index = 0; index < left.Scale; index++)
        {
            remainder *= 10;
            quotient = quotient * 10 + BigInteger.DivRem(remainder, rightBalanced, out remainder);
        }

        return new BigDecimal(quotient, left.Scale);
    }

    /// <summary>
    /// Gets the quotient of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator /(BigDecimal left, BigDecimal right)
    {
        return Divide(left, right);
    }
}
