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

using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalConstructorBigIntegerTests
{
    [Theory(DisplayName = "BigDecimal should be constructable from unscaled BigInteger value and scale (fractional scaling)")]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(1, 10)]
    [InlineData(-1, 0)]
    [InlineData(-1, 1)]
    [InlineData(-1, 2)]
    [InlineData(-1, 10)]
    [InlineData(10, 0)]
    [InlineData(10, 1)]
    [InlineData(10, 2)]
    [InlineData(10, 10)]
    [InlineData(-10, 0)]
    [InlineData(-10, 1)]
    [InlineData(-10, 2)]
    [InlineData(-10, 10)]
    public void BigDecimalShouldBeConstructableFromUnscaledValueAndScaleWithFractionalScaling(long unscaledValue, int scale)
    {
        // When
        BigDecimal value = unscaledValue.ToBigInteger().ToBigDecimal(scale, ScaleMode.Fractional);

        // Then
        Assert.Equal(unscaledValue, value.ToNumberInfo().UnscaledValue);
        Assert.Equal(scale, value.ToNumberInfo().Scale);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled BigInteger value and scale (Integral Scaling)")]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(1, 1, 10)]
    [InlineData(1, 2, 100)]
    [InlineData(1, 10, 10000000000)]
    [InlineData(-1, 0, -1)]
    [InlineData(-1, 1, -10)]
    [InlineData(-1, 2, -100)]
    [InlineData(-1, 10, -10000000000)]
    [InlineData(10, 0, 10)]
    [InlineData(10, 1, 100)]
    [InlineData(10, 2, 1000)]
    [InlineData(10, 10, 100000000000)]
    [InlineData(-10, 0, -10)]
    [InlineData(-10, 1, -100)]
    [InlineData(-10, 2, -1000)]
    [InlineData(-10, 10, -100000000000)]
    public void BigDecimalShouldBeConstructableFromUnscaledValueAndScaleUsingIntegralScaling(long unscaledValue, int scale, long expected)
    {
        // When
        BigDecimal value = unscaledValue.ToBigInteger().ToBigDecimal(scale);

        // Then
        Assert.Equal(expected, value.ToNumberInfo().UnscaledValue);
        Assert.Equal(scale, value.ToNumberInfo().Scale);
    }
}
