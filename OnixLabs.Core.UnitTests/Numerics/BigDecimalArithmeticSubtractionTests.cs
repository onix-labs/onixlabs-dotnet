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
using OnixLabs.Core.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticSubtractionTests
{
    [Theory(DisplayName = "BigDecimal.Subtract should return the expected result")]
    [BigDecimalArithmeticData(ArithmeticOperator.Subtraction, 1, 1, 1, 1000)]
    [BigDecimalArithmeticData(ArithmeticOperator.Subtraction, 100, 100, 10, 1000)]
    [BigDecimalArithmeticData(ArithmeticOperator.Subtraction, 0.123, 0.456, 0.12, 1000)]
    [BigDecimalArithmeticData(ArithmeticOperator.Subtraction, 1.234567, 9.876543, 0.123, 1000)]
    [BigDecimalArithmeticData(ArithmeticOperator.Subtraction, 123.45678, 987.65432, 0.1234, 1000)]
    public void BigDecimalSubtractShouldReturnTheExpectedResult(decimal left, decimal right, decimal expected)
    {
        // Arrange / Act
        BigDecimal actual = BigDecimal.Subtract(left, right);

        // Assert
        Assert.Equal(decimal.Round(expected, actual.Scale, MidpointRounding.ToZero), actual);
    }
}
