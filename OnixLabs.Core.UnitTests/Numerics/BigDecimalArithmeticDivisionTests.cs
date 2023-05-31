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

using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticDivisionTests
{
    [Theory(DisplayName = "BigDecimal.Divide should produce the expected result")]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(1, 3, 0.3)]
    [InlineData(1, 5, 0.2)]
    [InlineData(1, 7, 0.142857)]
    [InlineData(1, 256, 0.00390625)]
    [InlineData(1, 65536, 0.0000152587890625)]
    public void BigDecimalDivideShouldProduceExpectedResult(double left, double right, double expected)
    {
        // Given
        BigDecimal leftBigDecimal = left;
        BigDecimal rightBigDecimal = right;

        // When
        BigDecimal actual = leftBigDecimal / rightBigDecimal;

        // Then
        Assert.Equal(expected, actual);
    }
}
