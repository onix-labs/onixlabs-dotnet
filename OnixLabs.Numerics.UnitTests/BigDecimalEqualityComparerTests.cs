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

public sealed class BigDecimalEqualityComparerTests
{
    [NumberInfoEqualityComparerData]
    [Theory(DisplayName = "BigDecimalEqualityComparer.Equals should produce the expected result (Strict)")]
    public void BigDecimalEqualityComparerEqualsShouldProduceExpectedResultStrict(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left == right && left.Scale == right.Scale;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalEqualityComparer.Strict.Equals(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate.Equals(rightCandidate);

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }

    [NumberInfoEqualityComparerData]
    [Theory(DisplayName = "BigDecimalEqualityComparer.Equals should produce the expected result (Semantic)")]
    public void BigDecimalEqualityComparerEqualsShouldProduceExpectedResultSemantic(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left == right;
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // When
        bool actualFromComparer = BigDecimalEqualityComparer.Semantic.Equals(leftCandidate, rightCandidate);
        bool actualFromBigDecimal = leftCandidate == rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromBigDecimal);
    }
}
