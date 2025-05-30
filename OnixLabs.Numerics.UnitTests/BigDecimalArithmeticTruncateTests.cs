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

public sealed class BigDecimalArithmeticTruncateTests
{
    [BigDecimalArithmeticTruncateData]
    [Theory(DisplayName = "BigDecimal.Truncate should produce the expected result")]
    public void BigDecimalTruncateShouldProduceExpectedResult(decimal value, Guid _)
    {
        // Given
        decimal expected = decimal.Truncate(value);

        // When
        BigDecimal actual = BigDecimal.Truncate(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
