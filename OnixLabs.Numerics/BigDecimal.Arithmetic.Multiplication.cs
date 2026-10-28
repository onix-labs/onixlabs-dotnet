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
    /// Computes the product of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to multiply by.</param>
    /// <param name="right">The <paramref name="right"/> value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, BigDecimal right)
    {
        int scale = left.Scale + right.Scale;

        // Why: results are built with the unvalidated constructor, so the one way a scale could go negative, int overflow, is still
        // reported the way the validating constructor reported it.
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));

        if (IsZero(left) || IsZero(right)) return new BigDecimal(new NumberInfo(0, scale));
        BigInteger product = left.UnscaledValue * right.UnscaledValue;

        return new BigDecimal(new NumberInfo(product, scale));
    }

    /// <summary>
    /// Computes the product of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to multiply by.</param>
    /// <param name="right">The <paramref name="right"/> value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator *(BigDecimal left, BigDecimal right) => Multiply(left, right);
}
