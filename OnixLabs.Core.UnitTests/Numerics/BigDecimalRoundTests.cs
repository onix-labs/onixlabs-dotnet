// Copyright 2020-2022 ONIXLabs
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
using OnixLabs.Core.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public class BigDecimalRoundTests
{
    [Theory(DisplayName = "BigDecimal.Round should produce the correct result")]
    [BigDecimalRoundData(-1000, 1000, 1, 5)]
    [BigDecimalRoundData(-100000, 100000, 7531, 7)]
    public void BigDecimalRound(decimal value, int scale, MidpointRounding rounding, decimal expected)
    {
        // Arrange
        BigDecimal expectedBigDecimal = new(expected);

        // Act
        BigDecimal actualBigDecimal = BigDecimal.Round(new BigDecimal(value), scale, rounding);

        // Assert
        Assert.Equal(expectedBigDecimal, actualBigDecimal);
    }
}
