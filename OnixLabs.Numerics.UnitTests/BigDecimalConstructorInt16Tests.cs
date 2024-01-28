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
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public class BigDecimalConstructorInt16Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled Int16 value and scale (fractional scaling)")]
    [InlineData(short.MaxValue, 0, "32767")]
    [InlineData(short.MaxValue, 1, "3276.7")]
    [InlineData(short.MaxValue, 2, "327.67")]
    [InlineData(short.MaxValue, 3, "32.767")]
    [InlineData(short.MaxValue, 4, "3.2767")]
    [InlineData(short.MaxValue, 5, "0.32767")]
    [InlineData(short.MaxValue, 6, "0.032767")]
    [InlineData(short.MaxValue, 7, "0.0032767")]
    [InlineData(short.MaxValue, 8, "0.00032767")]
    [InlineData(short.MaxValue, 9, "0.000032767")]
    [InlineData(short.MaxValue, 10, "0.0000032767")]
    [InlineData(short.MaxValue, 20, "0.00000000000000032767")]
    [InlineData(short.MinValue, 0, "-32768")]
    [InlineData(short.MinValue, 1, "-3276.8")]
    [InlineData(short.MinValue, 2, "-327.68")]
    [InlineData(short.MinValue, 3, "-32.768")]
    [InlineData(short.MinValue, 4, "-3.2768")]
    [InlineData(short.MinValue, 5, "-0.32768")]
    [InlineData(short.MinValue, 6, "-0.032768")]
    [InlineData(short.MinValue, 7, "-0.0032768")]
    [InlineData(short.MinValue, 8, "-0.00032768")]
    [InlineData(short.MinValue, 9, "-0.000032768")]
    [InlineData(short.MinValue, 10, "-0.0000032768")]
    [InlineData(short.MinValue, 20, "-0.00000000000000032768")]
    public void BigDecimalShouldBeConstructableFromUnscaledInt16ValueAndScaleWithFractionalScaling(short value, int scale, string expected)
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
