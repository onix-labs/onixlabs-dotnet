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

public sealed class BigDecimalConstructorInt64Tests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled Int64 value and scale (fractional scaling)")]
    [InlineData(long.MaxValue, 0, "9223372036854775807")]
    [InlineData(long.MaxValue, 1, "922337203685477580.7")]
    [InlineData(long.MaxValue, 2, "92233720368547758.07")]
    [InlineData(long.MaxValue, 3, "9223372036854775.807")]
    [InlineData(long.MaxValue, 4, "922337203685477.5807")]
    [InlineData(long.MaxValue, 5, "92233720368547.75807")]
    [InlineData(long.MaxValue, 6, "9223372036854.775807")]
    [InlineData(long.MaxValue, 7, "922337203685.4775807")]
    [InlineData(long.MaxValue, 8, "92233720368.54775807")]
    [InlineData(long.MaxValue, 9, "9223372036.854775807")]
    [InlineData(long.MaxValue, 10, "922337203.6854775807")]
    [InlineData(long.MaxValue, 20, "0.09223372036854775807")]
    [InlineData(long.MinValue, 0, "-9223372036854775808")]
    [InlineData(long.MinValue, 1, "-922337203685477580.8")]
    [InlineData(long.MinValue, 2, "-92233720368547758.08")]
    [InlineData(long.MinValue, 3, "-9223372036854775.808")]
    [InlineData(long.MinValue, 4, "-922337203685477.5808")]
    [InlineData(long.MinValue, 5, "-92233720368547.75808")]
    [InlineData(long.MinValue, 6, "-9223372036854.775808")]
    [InlineData(long.MinValue, 7, "-922337203685.4775808")]
    [InlineData(long.MinValue, 8, "-92233720368.54775808")]
    [InlineData(long.MinValue, 9, "-9223372036.854775808")]
    [InlineData(long.MinValue, 10, "-922337203.6854775808")]
    [InlineData(long.MinValue, 20, "-0.09223372036854775808")]
    public void BigDecimalShouldBeConstructableFromUnscaledInt64ValueAndScaleWithFractionalScaling(long value, int scale, string expected)
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
