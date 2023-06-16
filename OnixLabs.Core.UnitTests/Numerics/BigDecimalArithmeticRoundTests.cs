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

using System;
using OnixLabs.Core.Numerics;
using OnixLabs.Core.UnitTests.Data.Generators;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticRoundTests
{
    [Theory(DisplayName = "BigDecimal.Round should produce the correct result")]
    [BigDecimalRoundDataGenerator(-1000, 1000, 7, 5)]
    [BigDecimalRoundDataGenerator(-100000, 100000, 997531, 7)]
    public void BigDecimalRoundShouldProduceExpectedResult(decimal value, int scale, MidpointRounding rounding, decimal expected)
    {
        // Arrange
        BigDecimal expectedBigDecimal = new(expected);

        // Act
        BigDecimal actualBigDecimal = BigDecimal.Round(new BigDecimal(value), scale, rounding);

        // Assert
        Assert.Equal(expectedBigDecimal, actualBigDecimal);
    }
}