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

public sealed class BigDecimalConstructorSByteTests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled sbyte value and scale (fractional scaling)")]
    [InlineData(sbyte.MaxValue, 0, "127")]
    [InlineData(sbyte.MaxValue, 1, "12.7")]
    [InlineData(sbyte.MaxValue, 2, "1.27")]
    [InlineData(sbyte.MaxValue, 3, "0.127")]
    [InlineData(sbyte.MaxValue, 4, "0.0127")]
    [InlineData(sbyte.MaxValue, 5, "0.00127")]
    [InlineData(sbyte.MaxValue, 6, "0.000127")]
    [InlineData(sbyte.MaxValue, 7, "0.0000127")]
    [InlineData(sbyte.MaxValue, 8, "0.00000127")]
    [InlineData(sbyte.MaxValue, 9, "0.000000127")]
    [InlineData(sbyte.MaxValue, 10, "0.0000000127")]
    [InlineData(sbyte.MaxValue, 20, "0.00000000000000000127")]
    [InlineData(sbyte.MinValue, 0, "-128")]
    [InlineData(sbyte.MinValue, 1, "-12.8")]
    [InlineData(sbyte.MinValue, 2, "-1.28")]
    [InlineData(sbyte.MinValue, 3, "-0.128")]
    [InlineData(sbyte.MinValue, 4, "-0.0128")]
    [InlineData(sbyte.MinValue, 5, "-0.00128")]
    [InlineData(sbyte.MinValue, 6, "-0.000128")]
    [InlineData(sbyte.MinValue, 7, "-0.0000128")]
    [InlineData(sbyte.MinValue, 8, "-0.00000128")]
    [InlineData(sbyte.MinValue, 9, "-0.000000128")]
    [InlineData(sbyte.MinValue, 10, "-0.0000000128")]
    [InlineData(sbyte.MinValue, 20, "-0.00000000000000000128")]
    public void BigDecimalShouldBeConstructableFromUnscaledSByteValueAndScaleWithFractionalScaling(sbyte value, int scale, string expected)
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
