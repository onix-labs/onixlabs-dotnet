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

public sealed class BigDecimalConstructorUInt64Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled UInt64 value and scale (fractional scaling)")]
    [InlineData(ulong.MaxValue, 0, "18446744073709551615")]
    [InlineData(ulong.MaxValue, 1, "1844674407370955161.5")]
    [InlineData(ulong.MaxValue, 2, "184467440737095516.15")]
    [InlineData(ulong.MaxValue, 3, "18446744073709551.615")]
    [InlineData(ulong.MaxValue, 4, "1844674407370955.1615")]
    [InlineData(ulong.MaxValue, 5, "184467440737095.51615")]
    [InlineData(ulong.MaxValue, 6, "18446744073709.551615")]
    [InlineData(ulong.MaxValue, 7, "1844674407370.9551615")]
    [InlineData(ulong.MaxValue, 8, "184467440737.09551615")]
    [InlineData(ulong.MaxValue, 9, "18446744073.709551615")]
    [InlineData(ulong.MaxValue, 10, "1844674407.3709551615")]
    [InlineData(ulong.MaxValue, 20, "0.18446744073709551615")]
    [InlineData(ulong.MinValue, 0, "0")]
    [InlineData(ulong.MinValue, 1, "0.0")]
    [InlineData(ulong.MinValue, 2, "0.00")]
    [InlineData(ulong.MinValue, 3, "0.000")]
    [InlineData(ulong.MinValue, 4, "0.0000")]
    [InlineData(ulong.MinValue, 5, "0.00000")]
    [InlineData(ulong.MinValue, 6, "0.000000")]
    [InlineData(ulong.MinValue, 7, "0.0000000")]
    [InlineData(ulong.MinValue, 8, "0.00000000")]
    [InlineData(ulong.MinValue, 9, "0.000000000")]
    [InlineData(ulong.MinValue, 10, "0.0000000000")]
    [InlineData(ulong.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalShouldBeConstructableFromUnscaledUInt64ValueAndScaleWithFractionalScaling(ulong value, int scale, string expected)
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
