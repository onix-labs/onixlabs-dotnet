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

public sealed class BigDecimalArithmeticModTests
{
    [Theory(DisplayName = "BigDecimal.Mod should produce the expected result")]
    [InlineData(0, 1, 0)]
    [InlineData(1, 1, 0)]
    [InlineData(2, 1, 0)]
    [InlineData(3, 2, 1)]
    [InlineData(4, 2, 0)]
    [InlineData(4, 3, 1)]
    [InlineData(100.01, 0.3, 0.11)]
    [InlineData(100.01, -0.3, 0.11)]
    [InlineData(123.456, 12.34, 0.056)]
    [InlineData(123.456, -12.34, 0.056)]
    public void BigDecimalModShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        BigDecimal actual = BigDecimal.Mod(left, right);

        // Then
        Assert.Equal(expected, actual);
    }
}
