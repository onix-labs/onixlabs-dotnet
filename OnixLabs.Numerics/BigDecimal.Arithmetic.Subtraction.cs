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
    /// Computes the difference between the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Subtract(BigDecimal left, BigDecimal right)
    {
        int scale = MaxScale(left, right);

        if (IsZero(left)) return -right.SetScale(scale);
        if (IsZero(right)) return left.SetScale(scale);

        (BigInteger minuend, BigInteger subtrahend) = NormalizeUnscaledValues(left, right);
        BigInteger difference = minuend - subtrahend;

        return new BigDecimal(difference, scale);
    }

    /// <summary>
    /// Computes the difference between the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator -(BigDecimal left, BigDecimal right) => Subtract(left, right);
}
