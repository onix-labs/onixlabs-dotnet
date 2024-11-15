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

public sealed class NumberInfoEqualityComparerTests
{
    [NumberInfoEqualityComparerData]
    [Theory(DisplayName = "NumberInfoEqualityComparer.Equals should produce the expected result (Strict)")]
    public void NumberInfoEqualityComparerEqualsShouldProduceExpectedResultStrict(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left == right && left.Scale == right.Scale;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoEqualityComparer.Strict.Equals(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate.Equals(rightCandidate);

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }

    [NumberInfoEqualityComparerData]
    [Theory(DisplayName = "NumberInfoEqualityComparer.Equals should produce the expected result (Semantic)")]
    public void NumberInfoEqualityComparerEqualsShouldProduceExpectedResultSemantic(decimal left, decimal right, Guid _)
    {
        // Given
        bool expected = left == right;
        NumberInfo leftCandidate = left.ToNumberInfo();
        NumberInfo rightCandidate = right.ToNumberInfo();

        // When
        bool actualFromComparer = NumberInfoEqualityComparer.Semantic.Equals(leftCandidate, rightCandidate);
        bool actualFromNumberInfo = leftCandidate == rightCandidate;

        // Then
        Assert.Equal(expected, actualFromComparer);
        Assert.Equal(expected, actualFromNumberInfo);
    }
}
