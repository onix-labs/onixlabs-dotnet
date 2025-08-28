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

using System;
using OnixLabs.Numerics.UnitTests.Data;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalOrdinalityComparerTests
{
    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.Compare should produce the expected result")]
    public void BigDecimalOrdinalityComparerCompareShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        int expected = left.CompareTo(right);
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        int actualFromComparer = BigDecimalOrdinalityComparer.Default.Compare(leftCandidate, rightCandidate);
        int actualFromBigDecimal = leftCandidate.CompareTo(rightCandidate);

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.IsEqual should produce the expected result")]
    public void BigDecimalOrdinalityComparerIsEqualShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left.CompareTo(right) is 0;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalOrdinalityComparer.Default.IsEqual(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate.CompareTo(rightCandidate) is 0;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.IsGreaterThan should produce the expected result")]
    public void BigDecimalOrdinalityComparerIsGreaterThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left > right;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalOrdinalityComparer.Default.IsGreaterThan(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate > rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.IsGreaterThanOrEqual should produce the expected result")]
    public void BigDecimalOrdinalityComparerIsGreaterOrEqualThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left >= right;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalOrdinalityComparer.Default.IsGreaterThanOrEqual(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate >= rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.IsLessThan should produce the expected result")]
    public void BigDecimalOrdinalityComparerIsLessThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left < right;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalOrdinalityComparer.Default.IsLessThan(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate < rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "BigDecimalOrdinalityComparer.IsLessThanOrEqual should produce the expected result")]
    public void BigDecimalOrdinalityComparerIsLessOrEqualThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left <= right;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalOrdinalityComparer.Default.IsLessThanOrEqual(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate <= rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }
}
