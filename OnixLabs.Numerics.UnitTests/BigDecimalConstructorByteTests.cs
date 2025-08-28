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

namespace OnixLabs.Numerics.UnitTests;

public class BigDecimalConstructorByteTests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled byte value and scale (fractional scaling)")]
    [InlineData(byte.MaxValue, 0, "255")]
    [InlineData(byte.MaxValue, 1, "25.5")]
    [InlineData(byte.MaxValue, 2, "2.55")]
    [InlineData(byte.MaxValue, 3, "0.255")]
    [InlineData(byte.MaxValue, 4, "0.0255")]
    [InlineData(byte.MaxValue, 5, "0.00255")]
    [InlineData(byte.MaxValue, 6, "0.000255")]
    [InlineData(byte.MaxValue, 7, "0.0000255")]
    [InlineData(byte.MaxValue, 8, "0.00000255")]
    [InlineData(byte.MaxValue, 9, "0.000000255")]
    [InlineData(byte.MaxValue, 10, "0.0000000255")]
    [InlineData(byte.MaxValue, 20, "0.00000000000000000255")]
    [InlineData(byte.MinValue, 0, "0")]
    [InlineData(byte.MinValue, 1, "0.0")]
    [InlineData(byte.MinValue, 2, "0.00")]
    [InlineData(byte.MinValue, 3, "0.000")]
    [InlineData(byte.MinValue, 4, "0.0000")]
    [InlineData(byte.MinValue, 5, "0.00000")]
    [InlineData(byte.MinValue, 6, "0.000000")]
    [InlineData(byte.MinValue, 7, "0.0000000")]
    [InlineData(byte.MinValue, 8, "0.00000000")]
    [InlineData(byte.MinValue, 9, "0.000000000")]
    [InlineData(byte.MinValue, 10, "0.0000000000")]
    [InlineData(byte.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalShouldBeConstructableFromUnscaledByteValueAndScaleWithFractionalScaling(byte value, int scale, string expected)
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
