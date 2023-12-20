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
using OnixLabs.Core.UnitTests.Data.Generators;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalParseTests
{
    [BigDecimalParseData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (General)")]
    public void BigDecimalParseShouldProduceExpectedResultGeneral(decimal expected)
    {
        // Given
        string value = expected.ToString("G");

        // When
        BigDecimal actual = BigDecimal.Parse(value);

        // Then
        Assert.Equal(expected, actual);
    }
    
    [BigDecimalParseData]
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result (Engineering)")]
    public void BigDecimalParseShouldProduceExpectedResultEngineering(decimal expected)
    {
        // Given
        string value = expected.ToString("E");

        // When
        BigDecimal actual = BigDecimal.Parse(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
