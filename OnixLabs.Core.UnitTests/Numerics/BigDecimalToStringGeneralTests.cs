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

using System.Globalization;
using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalToStringGeneralTests
{
    [Theory(DisplayName = "BigDecimal.ToString should produce the expected result (general formatting with default scale)")]
    [InlineData(0, 0, "0")]
    [InlineData(0, 1, "0.0")]
    [InlineData(0, 2, "0.00")]
    [InlineData(0, 3, "0.000")]
    [InlineData(0, 4, "0.0000")]
    [InlineData(0, 5, "0.00000")]
    [InlineData(0, 6, "0.000000")]
    [InlineData(0, 7, "0.0000000")]
    [InlineData(0, 8, "0.00000000")]
    [InlineData(0, 9, "0.000000000")]
    [InlineData(0, 10, "0.0000000000")]
    [InlineData(100000000001, 1, "10000000000.1")]
    [InlineData(100000000001, 2, "1000000000.01")]
    [InlineData(100000000001, 3, "100000000.001")]
    [InlineData(100000000001, 4, "10000000.0001")]
    [InlineData(100000000001, 5, "1000000.00001")]
    [InlineData(100000000001, 6, "100000.000001")]
    [InlineData(100000000001, 7, "10000.0000001")]
    [InlineData(100000000001, 8, "1000.00000001")]
    [InlineData(100000000001, 9, "100.000000001")]
    [InlineData(100000000001, 10, "10.0000000001")]
    [InlineData(100000000001, 11, "1.00000000001")]
    [InlineData(100000000001, 12, "0.100000000001")]
    [InlineData(100000000001, 13, "0.0100000000001")]
    [InlineData(100000000001, 14, "0.00100000000001")]
    [InlineData(100000000001, 15, "0.000100000000001")]
    [InlineData(100000000001, 16, "0.0000100000000001")]
    [InlineData(100000000001, 17, "0.00000100000000001")]
    [InlineData(100000000001, 18, "0.000000100000000001")]
    [InlineData(100000000001, 19, "0.0000000100000000001")]
    [InlineData(100000000001, 20, "0.00000000100000000001")]
    [InlineData(-100000000001, 1, "-10000000000.1")]
    [InlineData(-100000000001, 2, "-1000000000.01")]
    [InlineData(-100000000001, 3, "-100000000.001")]
    [InlineData(-100000000001, 4, "-10000000.0001")]
    [InlineData(-100000000001, 5, "-1000000.00001")]
    [InlineData(-100000000001, 6, "-100000.000001")]
    [InlineData(-100000000001, 7, "-10000.0000001")]
    [InlineData(-100000000001, 8, "-1000.00000001")]
    [InlineData(-100000000001, 9, "-100.000000001")]
    [InlineData(-100000000001, 10, "-10.0000000001")]
    [InlineData(-100000000001, 11, "-1.00000000001")]
    [InlineData(-100000000001, 12, "-0.100000000001")]
    [InlineData(-100000000001, 13, "-0.0100000000001")]
    [InlineData(-100000000001, 14, "-0.00100000000001")]
    [InlineData(-100000000001, 15, "-0.000100000000001")]
    [InlineData(-100000000001, 16, "-0.0000100000000001")]
    [InlineData(-100000000001, 17, "-0.00000100000000001")]
    [InlineData(-100000000001, 18, "-0.000000100000000001")]
    [InlineData(-100000000001, 19, "-0.0000000100000000001")]
    [InlineData(-100000000001, 20, "-0.00000000100000000001")]
    public void BigDecimalToStringShouldProduceExpectedResult(long value, int scale, string expected)
    {
        // Given
        BigDecimal candidate = new(value, scale);
        CultureInfo culture = CultureInfo.GetCultureInfo("en-GB");

        // When
        string actual = candidate.ToString("G", culture);

        // Then
        Assert.Equal(expected, actual);
    }
}
