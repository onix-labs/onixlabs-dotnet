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

public sealed class BigDecimalConstructorUInt32Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled UInt32 value and scale (fractional scaling)")]
    [InlineData(uint.MaxValue, 0, "4294967295")]
    [InlineData(uint.MaxValue, 1, "429496729.5")]
    [InlineData(uint.MaxValue, 2, "42949672.95")]
    [InlineData(uint.MaxValue, 3, "4294967.295")]
    [InlineData(uint.MaxValue, 4, "429496.7295")]
    [InlineData(uint.MaxValue, 5, "42949.67295")]
    [InlineData(uint.MaxValue, 6, "4294.967295")]
    [InlineData(uint.MaxValue, 7, "429.4967295")]
    [InlineData(uint.MaxValue, 8, "42.94967295")]
    [InlineData(uint.MaxValue, 9, "4.294967295")]
    [InlineData(uint.MaxValue, 10, "0.4294967295")]
    [InlineData(uint.MaxValue, 20, "0.00000000004294967295")]
    [InlineData(uint.MinValue, 0, "0")]
    [InlineData(uint.MinValue, 1, "0.0")]
    [InlineData(uint.MinValue, 2, "0.00")]
    [InlineData(uint.MinValue, 3, "0.000")]
    [InlineData(uint.MinValue, 4, "0.0000")]
    [InlineData(uint.MinValue, 5, "0.00000")]
    [InlineData(uint.MinValue, 6, "0.000000")]
    [InlineData(uint.MinValue, 7, "0.0000000")]
    [InlineData(uint.MinValue, 8, "0.00000000")]
    [InlineData(uint.MinValue, 9, "0.000000000")]
    [InlineData(uint.MinValue, 10, "0.0000000000")]
    [InlineData(uint.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalShouldBeConstructableFromUnscaledUInt32ValueAndScaleWithFractionalScaling(uint value, int scale, string expected)
    {
        // Given
        BigDecimal candidate = value.ToBigDecimal(scale, ScaleMode.Fractional);
        CultureInfo culture = CultureInfo.GetCultureInfo("en-GB");

        // When
        string actual = candidate.ToString("G", culture);

        // Then
        Assert.Equal(expected, actual);
    }
}
