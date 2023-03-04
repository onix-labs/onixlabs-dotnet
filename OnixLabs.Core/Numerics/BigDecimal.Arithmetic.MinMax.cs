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
    /// Gets the lesser and the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the lesser and the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static (BigDecimal min, BigDecimal max) MinMax(BigDecimal left, BigDecimal right)
    {
        return left < right ? (left, right) : (right, left);
    }

    /// <summary>
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Min(BigDecimal left, BigDecimal right)
    {
        return MinMax(left, right).min;
    }

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Max(BigDecimal left, BigDecimal right)
    {
        return MinMax(left, right).max;
    }

    /// <summary>
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal MinMagnitude(BigDecimal x, BigDecimal y)
    {
        return Min(x, y);
    }

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal MaxMagnitude(BigDecimal x, BigDecimal y)
    {
        return Max(x, y);
    }

    /// <summary>
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    static BigDecimal INumberBase<BigDecimal>.MinMagnitudeNumber(BigDecimal x, BigDecimal y)
    {
        return Min(x, y);
    }

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    static BigDecimal INumberBase<BigDecimal>.MaxMagnitudeNumber(BigDecimal x, BigDecimal y)
    {
        return Max(x, y);
    }
}
