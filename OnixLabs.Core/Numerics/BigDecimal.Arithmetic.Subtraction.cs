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
    /// Computes the difference between the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Subtract(BigDecimal left, BigDecimal right)
    {
        if (IsZero(left)) return -right;
        if (IsZero(right)) return left;

        int scale = MaxScale(left, right);

        (BigInteger leftNormalized, BigInteger rightNormalized) = NormalizeUnscaledOrderOfMagnitude(left, right);
        BigInteger difference = leftNormalized - rightNormalized;

        return new BigDecimal(difference, scale);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator -(BigDecimal left, BigDecimal right)
    {
        return Subtract(left, right);
    }

    /// <summary>
    /// Computes the difference between the current <see cref="BigDecimal"/> value and the specified value.
    /// </summary>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the current <see cref="BigDecimal"/> value and the specified value.</returns>
    public BigDecimal Subtract(BigDecimal right)
    {
        return Subtract(this, right);
    }
}
