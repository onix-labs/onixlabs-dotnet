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
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Min(BigDecimal left, BigDecimal right) => left < right ? left : right;

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal Max(BigDecimal left, BigDecimal right) => left > right ? left : right;

    /// <summary>
    /// Gets the lesser and the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the lesser and the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static (BigDecimal Min, BigDecimal Max) MinMax(BigDecimal left, BigDecimal right)
    {
        BigDecimal min = Min(left, right);
        BigDecimal max = Max(left, right);

        return (min, max);
    }

    /// <summary>
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal MinMagnitude(BigDecimal x, BigDecimal y)
    {
        BigDecimal left = Abs(x);
        BigDecimal right = Abs(y);

        return left < right || left == right && IsNegative(x) ? x : y;
    }

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    public static BigDecimal MaxMagnitude(BigDecimal x, BigDecimal y)
    {
        BigDecimal left = Abs(x);
        BigDecimal right = Abs(y);

        return left > right || left == right && !IsNegative(x) ? x : y;
    }

    /// <summary>
    /// Gets the lesser and the greater of the specified <see cref="BigDecimal"/> values by magnitude.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <returns>Returns the lesser and the greater of the specified <see cref="BigDecimal"/> values by magnitude.</returns>
    public static (BigDecimal MinMagnitude, BigDecimal MaxMagnitude) MinMaxMagnitude(BigDecimal left, BigDecimal right)
    {
        BigDecimal minMagnitude = MinMagnitude(left, right);
        BigDecimal maxMagnitude = MaxMagnitude(left, right);

        return (minMagnitude, maxMagnitude);
    }

    /// <summary>
    /// Obtains the minimum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the minimum scale value.</param>
    /// <param name="right">The left-hand value from which to obtain the minimum scale value.</param>
    /// <returns>Returns the minimum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static int MinScale(BigDecimal left, BigDecimal right) => int.Min(left.Scale, right.Scale);

    /// <summary>
    /// Obtains the maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the maximum scale value.</param>
    /// <param name="right">The left-hand value from which to obtain the maximum scale value.</param>
    /// <returns>Returns the maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static int MaxScale(BigDecimal left, BigDecimal right) => int.Max(left.Scale, right.Scale);

    /// <summary>
    /// Obtains the minimum and maximum scale of the specified left-hand and right-hand <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="left">The left-hand value from which to obtain the minimum and maximum values.</param>
    /// <param name="right">The left-hand value from which to obtain the minimum and maximum values.</param>
    /// <returns>Returns the minimum and maximum of the specified left-hand and right-hand <see cref="BigDecimal"/> values.</returns>
    public static (int Min, int Max) MinMaxScale(BigDecimal left, BigDecimal right)
    {
        int min = MinScale(left, right);
        int max = MaxScale(left, right);

        return (min, max);
    }

    /// <summary>
    /// Gets the lesser of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the lesser of the specified <see cref="BigDecimal"/> values.</returns>
    static BigDecimal INumberBase<BigDecimal>.MinMagnitudeNumber(BigDecimal x, BigDecimal y) => MinMagnitude(x, y);

    /// <summary>
    /// Gets the greater of the specified <see cref="BigDecimal"/> values.
    /// </summary>
    /// <param name="x">The left value to compare.</param>
    /// <param name="y">The right value to compare.</param>
    /// <returns>Returns the greater of the specified <see cref="BigDecimal"/> values.</returns>
    static BigDecimal INumberBase<BigDecimal>.MaxMagnitudeNumber(BigDecimal x, BigDecimal y) => MaxMagnitude(x, y);
}
