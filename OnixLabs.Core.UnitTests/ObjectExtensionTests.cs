// Copyright 2020 ONIXLabs
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
using OnixLabs.Core.UnitTests.Data;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class ObjectExtensionTests
{
    [Theory(DisplayName = "IsWithinRangeInclusive should produce the expected result")]
    [InlineData(2, 1, 3, true)]
    [InlineData(1, 1, 3, true)]
    [InlineData(3, 1, 3, true)]
    [InlineData(0, 1, 3, false)]
    [InlineData(4, 1, 3, false)]
    public void IsWithinRangeInclusiveShouldProduceExpectedResult(int value, int min, int max, bool expected)
    {
        // When
        bool actual = value.IsWithinRangeInclusive(min, max);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IsWithinRangeExclusive should produce the expected result")]
    [InlineData(2, 1, 3, true)]
    [InlineData(1, 1, 3, false)]
    [InlineData(3, 1, 3, false)]
    [InlineData(0, 1, 3, false)]
    [InlineData(4, 1, 3, false)]
    public void IsWithinRangeExclusiveShouldProduceExpectedResult(int value, int min, int max, bool expected)
    {
        // When
        bool actual = value.IsWithinRangeExclusive(min, max);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString should produce a record formatted string")]
    public void ToRecordStringShouldProduceExpectedResult()
    {
        // Given
        Record<Guid> record = new("abc", 123, Guid.NewGuid());
        string expected = record.ToString();

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }
}
