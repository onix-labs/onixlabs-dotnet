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

public sealed class BigDecimalConstructorInt32Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled Int32 value and scale (fractional scaling)")]
    [InlineData(1, 0, "1")]
    [InlineData(1, 1, "0.1")]
    [InlineData(1, 2, "0.01")]
    [InlineData(1, 3, "0.001")]
    [InlineData(1, 4, "0.0001")]
    [InlineData(1, 5, "0.00001")]
    [InlineData(1, 6, "0.000001")]
    [InlineData(1, 7, "0.0000001")]
    [InlineData(1, 8, "0.00000001")]
    [InlineData(1, 9, "0.000000001")]
    [InlineData(1, 10, "0.0000000001")]
    [InlineData(1, 20, "0.00000000000000000001")]
    [InlineData(-1, 0, "-1")]
    [InlineData(-1, 1, "-0.1")]
    [InlineData(-1, 2, "-0.01")]
    [InlineData(-1, 3, "-0.001")]
    [InlineData(-1, 4, "-0.0001")]
    [InlineData(-1, 5, "-0.00001")]
    [InlineData(-1, 6, "-0.000001")]
    [InlineData(-1, 7, "-0.0000001")]
    [InlineData(-1, 8, "-0.00000001")]
    [InlineData(-1, 9, "-0.000000001")]
    [InlineData(-1, 10, "-0.0000000001")]
    [InlineData(-1, 20, "-0.00000000000000000001")]
    [InlineData(int.MaxValue, 0, "2147483647")]
    [InlineData(int.MaxValue, 1, "214748364.7")]
    [InlineData(int.MaxValue, 2, "21474836.47")]
    [InlineData(int.MaxValue, 3, "2147483.647")]
    [InlineData(int.MaxValue, 4, "214748.3647")]
    [InlineData(int.MaxValue, 5, "21474.83647")]
    [InlineData(int.MaxValue, 6, "2147.483647")]
    [InlineData(int.MaxValue, 7, "214.7483647")]
    [InlineData(int.MaxValue, 8, "21.47483647")]
    [InlineData(int.MaxValue, 9, "2.147483647")]
    [InlineData(int.MaxValue, 10, "0.2147483647")]
    [InlineData(int.MaxValue, 20, "0.00000000002147483647")]
    [InlineData(int.MinValue, 0, "-2147483648")]
    [InlineData(int.MinValue, 1, "-214748364.8")]
    [InlineData(int.MinValue, 2, "-21474836.48")]
    [InlineData(int.MinValue, 3, "-2147483.648")]
    [InlineData(int.MinValue, 4, "-214748.3648")]
    [InlineData(int.MinValue, 5, "-21474.83648")]
    [InlineData(int.MinValue, 6, "-2147.483648")]
    [InlineData(int.MinValue, 7, "-214.7483648")]
    [InlineData(int.MinValue, 8, "-21.47483648")]
    [InlineData(int.MinValue, 9, "-2.147483648")]
    [InlineData(int.MinValue, 10, "-0.2147483648")]
    [InlineData(int.MinValue, 20, "-0.00000000002147483648")]
    public void BigDecimalShouldBeConstructableFromUnscaledInt32ValueAndScaleWithFractionalScaling(int value, int scale, string expected)
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
