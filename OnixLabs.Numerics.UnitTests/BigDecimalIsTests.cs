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

public sealed class BigDecimalIsTests
{
    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsInteger should produce the expected result")]
    public void BigDecimalIsIntegerShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = decimal.IsInteger(value);

        // When
        bool actual = BigDecimal.IsInteger(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsEvenInteger should produce the expected result")]
    public void BigDecimalIsEvenIntegerShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = decimal.IsEvenInteger(value);

        // When
        bool actual = BigDecimal.IsEvenInteger(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsOddInteger should produce the expected result")]
    public void BigDecimalIsOddIntegerShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = decimal.IsOddInteger(value);

        // When
        bool actual = BigDecimal.IsOddInteger(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsNegative should produce the expected result")]
    public void BigDecimalIsNegativeShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = decimal.IsNegative(value);

        // When
        bool actual = BigDecimal.IsNegative(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsPositive should produce the expected result")]
    public void BigDecimalIsPositiveShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = decimal.IsPositive(value);

        // When
        bool actual = BigDecimal.IsPositive(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [BigDecimalIsData]
    [Theory(DisplayName = "BigDecimal.IsZero should produce the expected result")]
    public void BigDecimalIsZeroShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        bool expected = value is 0;

        // When
        bool actual = BigDecimal.IsZero(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
