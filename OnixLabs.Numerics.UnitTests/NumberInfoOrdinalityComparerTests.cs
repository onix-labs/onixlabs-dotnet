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
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumberInfoOrdinalityComparerTests
{
    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.Compare should produce the expected result")]
    public void NumberInfoOrdinalityComparerCompareShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        int expected = left.CompareTo(right);
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        int actualFromComparer = NumberInfoOrdinalityComparer.Default.Compare(leftCandidate, rightCandidate);
        int actualFromNumberInfo = leftCandidate.CompareTo(rightCandidate);

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.IsEqual should produce the expected result")]
    public void NumberInfoOrdinalityComparerIsEqualShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left.CompareTo(right) is 0;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoOrdinalityComparer.Default.IsEqual(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate.CompareTo(rightCandidate) is 0;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.IsGreaterThan should produce the expected result")]
    public void NumberInfoOrdinalityComparerIsGreaterThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left > right;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoOrdinalityComparer.Default.IsGreaterThan(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate > rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.IsGreaterThanOrEqual should produce the expected result")]
    public void NumberInfoOrdinalityComparerIsGreaterOrEqualThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left >= right;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoOrdinalityComparer.Default.IsGreaterThanOrEqual(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate >= rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.IsLessThan should produce the expected result")]
    public void NumberInfoOrdinalityComparerIsLessThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left < right;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoOrdinalityComparer.Default.IsLessThan(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate < rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoOrdinalityComparerData]
    [Theory(DisplayName = "NumberInfoOrdinalityComparer.IsLessThanOrEqual should produce the expected result")]
    public void NumberInfoOrdinalityComparerIsLessOrEqualThanShouldProduceExpectedResult(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left <= right;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoOrdinalityComparer.Default.IsLessThanOrEqual(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate <= rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }
}
