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

using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalArithmeticModulusTests
{
    [BigDecimalArithmeticModulusData]
    [Theory(DisplayName = "BigDecimal.Mod should produce the expected result")]
    public void BigDecimalModShouldProduceExpectedResult(decimal left, decimal right)
    {
        // Given
        decimal expected = left % right;

        // When
        BigDecimal actual = BigDecimal.Mod(left, right).Round(expected.Scale);

        // Then
        Assert.Equal(expected, actual);
    }
}
