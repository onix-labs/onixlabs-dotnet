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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator *(BigDecimal left, BigDecimal right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal operator *(BigDecimal left, float right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal operator *(BigDecimal left, double right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal operator *(BigDecimal left, decimal right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator *(float left, BigDecimal right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator *(double left, BigDecimal right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal operator *(decimal left, BigDecimal right)
    {
        return Multiply(left, right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, BigDecimal right)
    {
        return new BigDecimal(left.UnscaledValue * right.UnscaledValue, left.Scale + right.Scale);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="float"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, float right)
    {
        return Multiply(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="double"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, double right)
    {
        return Multiply(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the product of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="BigDecimal"/> and <see cref="decimal"/> values.</returns>
    public static BigDecimal Multiply(BigDecimal left, decimal right)
    {
        return Multiply(left, right.ToBigDecimal());
    }

    /// <summary>
    /// Gets the product of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="float"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(float left, BigDecimal right)
    {
        return Multiply(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="double"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(double left, BigDecimal right)
    {
        return Multiply(left.ToBigDecimal(), right);
    }

    /// <summary>
    /// Gets the product of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply by.</param>
    /// <param name="right">The right-hand value to multiply.</param>
    /// <returns>Returns the product of the specified <see cref="decimal"/> and <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Multiply(decimal left, BigDecimal right)
    {
        return Multiply(left.ToBigDecimal(), right);
    }
}
