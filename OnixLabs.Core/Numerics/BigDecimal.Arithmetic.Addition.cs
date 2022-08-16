// Copyright 2020-2022 ONIXLabs
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
    /// Gets the sum of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator +(BigDecimal left, BigDecimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal operator +(BigDecimal left, float right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal operator +(BigDecimal left, double right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal operator +(BigDecimal left, decimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator +(float left, BigDecimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator +(double left, BigDecimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator +(decimal left, BigDecimal right)
    {
        return Add(left, right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, BigDecimal right)
    {
        (BigDecimal leftBalanced, BigDecimal rightBalanced) = Balance(left, right);
        BigInteger unscaledValue = leftBalanced.UnscaledValue + rightBalanced.UnscaledValue;

        return new BigDecimal(unscaledValue, leftBalanced.Scale);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, float right)
    {
        return Add(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, double right)
    {
        return Add(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal Add(BigDecimal left, decimal right)
    {
        return Add(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(float left, BigDecimal right)
    {
        return Add(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(double left, BigDecimal right)
    {
        return Add(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the sum of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Add(decimal left, BigDecimal right)
    {
        return Add(left.ToBigDecimal(), right);
    }
}
