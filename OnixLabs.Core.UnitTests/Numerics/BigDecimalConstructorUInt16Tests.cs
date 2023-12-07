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

public sealed class BigDecimalConstructorUInt16Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled UInt16 value and scale (fractional scaling)")]
    [InlineData(ushort.MaxValue, 0, "65535")]
    [InlineData(ushort.MaxValue, 1, "6553.5")]
    [InlineData(ushort.MaxValue, 2, "655.35")]
    [InlineData(ushort.MaxValue, 3, "65.535")]
    [InlineData(ushort.MaxValue, 4, "6.5535")]
    [InlineData(ushort.MaxValue, 5, "0.65535")]
    [InlineData(ushort.MaxValue, 6, "0.065535")]
    [InlineData(ushort.MaxValue, 7, "0.0065535")]
    [InlineData(ushort.MaxValue, 8, "0.00065535")]
    [InlineData(ushort.MaxValue, 9, "0.000065535")]
    [InlineData(ushort.MaxValue, 10, "0.0000065535")]
    [InlineData(ushort.MaxValue, 20, "0.00000000000000065535")]
    [InlineData(ushort.MinValue, 0, "0")]
    [InlineData(ushort.MinValue, 1, "0.0")]
    [InlineData(ushort.MinValue, 2, "0.00")]
    [InlineData(ushort.MinValue, 3, "0.000")]
    [InlineData(ushort.MinValue, 4, "0.0000")]
    [InlineData(ushort.MinValue, 5, "0.00000")]
    [InlineData(ushort.MinValue, 6, "0.000000")]
    [InlineData(ushort.MinValue, 7, "0.0000000")]
    [InlineData(ushort.MinValue, 8, "0.00000000")]
    [InlineData(ushort.MinValue, 9, "0.000000000")]
    [InlineData(ushort.MinValue, 10, "0.0000000000")]
    [InlineData(ushort.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalShouldBeConstructableFromUnscaledUInt16ValueAndScaleWithFractionalScaling(ushort value, int scale, string expected)
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
