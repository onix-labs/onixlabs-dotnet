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
using OnixLabs.Core.Numerics;
using OnixLabs.Core.UnitTests.Data.Generators;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalArithmeticDivisionTests
{
    [BigDecimalDivisionData]
    [Theory(DisplayName = "BigDecimal.Divide should produce the expected result")]
    public void BigDecimalDivideShouldProduceExpectedResult(decimal left, decimal right, MidpointRounding mode)
    {
        // Given
        decimal expected = decimal.Round(left / right, left.Scale, mode);

        // When
        BigDecimal actual = BigDecimal.Divide(left, right, mode);

        // Then
        Assert.Equal(expected, actual);
    }
}
