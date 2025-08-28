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

namespace OnixLabs.Core.UnitTests;

public sealed class DateTimeExtensionTests
{
    [Theory(DisplayName = "ToDateOnly should produce the expected result")]
    [InlineData(1, 1, 1, 0, 0, 0)]
    [InlineData(1999, 12, 12, 12, 30, 31)]
    public void ToDateOnlyShouldProduceExpectedResult(int year, int month, int day, int hour, int minute, int second)
    {
        // When
        DateTime value = new(year, month, day, hour, minute, second);
        DateOnly expected = DateOnly.FromDateTime(value);
        DateOnly actual = value.ToDateOnly();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ToTimeOnly should produce the expected result")]
    [InlineData(1, 1, 1, 0, 0, 0)]
    [InlineData(1999, 12, 12, 12, 30, 31)]
    public void ToTimeOnlyShouldProduceExpectedResult(int year, int month, int day, int hour, int minute, int second)
    {
        // When
        DateTime value = new(year, month, day, hour, minute, second);
        TimeOnly expected = TimeOnly.FromDateTime(value);
        TimeOnly actual = value.ToTimeOnly();

        // Then
        Assert.Equal(expected, actual);
    }
}
