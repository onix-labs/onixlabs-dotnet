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

using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticMultiplicationTests
{
    [Theory(DisplayName = "BigDecimal.Multiply should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(10, 10, 100)]
    [InlineData(1.23, 4.56, 5.6088)]
    [InlineData(123.456, 789.123, 97421.969088)]
    [InlineData(-1, 0, -0)]
    [InlineData(-1, 1, -1)]
    [InlineData(-1, 2, -2)]
    [InlineData(-2, 2, -4)]
    [InlineData(-10, 10, -100)]
    [InlineData(-1.23, 4.56, -5.6088)]
    [InlineData(-123.456, 789.123, -97421.969088)]
    public void BigDecimalMultiplyShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Multiply(left, right);

        // Then
        Assert.Equal(expected, actual);
    }
}
