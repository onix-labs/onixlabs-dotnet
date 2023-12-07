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
    /// Computes the sum of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, BigDecimal right)
    {
        if (IsZero(left)) return right;
        if (IsZero(right)) return left;

        int scale = MaxScale(left, right);

        (BigInteger leftAddend, BigInteger rightAddend) = NormalizeScaleMagnitude(left, right);
        BigInteger sum = leftAddend + rightAddend;

        return new BigDecimal(sum, scale);
    }

    /// <summary>
    /// Computes the sum of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator +(BigDecimal left, BigDecimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Computes the sum of the current <see cref="BigDecimal"/> value and the specified value.
    /// </summary>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the current <see cref="BigDecimal"/> value and the specified value.</returns>
    public BigDecimal Add(BigDecimal right)
    {
        return Add(this, right);
    }
}
