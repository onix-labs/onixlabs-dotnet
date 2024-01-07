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

using OnixLabs.Core;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalConstructorByteArrayTests
{
    [Theory(DisplayName = "BigDecimal should be constructable from a byte array")]
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
    [InlineData(long.MinValue, 0)]
    [InlineData(long.MinValue, 1)]
    [InlineData(long.MinValue, 2)]
    [InlineData(long.MinValue, 10)]
    [InlineData(long.MaxValue, 0)]
    [InlineData(long.MaxValue, 1)]
    [InlineData(long.MaxValue, 2)]
    [InlineData(long.MaxValue, 10)]
    [InlineData(long.MinValue, int.MaxValue)]
    [InlineData(long.MaxValue, int.MaxValue)]
    public void BigDecimalShouldBeConstructableFromByteArray(long unscaledValue, int scale)
    {
        // Given
        byte[] unscaledValueBytes = BitConverter.GetBytes(unscaledValue);
        byte[] scaleBytes = BitConverter.GetBytes(scale);
        byte[] bytes = unscaledValueBytes.ConcatenateWith(scaleBytes);

        // When
        BigDecimal value = new(bytes);

        // Then
        Assert.Equal(unscaledValue, value.ToNumberInfo().UnscaledValue);
        Assert.Equal(scale, value.ToNumberInfo().Scale);
    }

    [Theory(DisplayName = "BigDecimal should throw an ArgumentException when constructed with a negative scale")]
    [InlineData(0, -1)]
    [InlineData(0, int.MinValue)]
    public void BigDecimalShouldThrowExceptionWhenConstructedWithNegativeScale(long unscaledValue, int scale)
    {
        // Given
        byte[] unscaledValueBytes = BitConverter.GetBytes(unscaledValue);
        byte[] scaleBytes = BitConverter.GetBytes(scale);
        byte[] bytes = unscaledValueBytes.ConcatenateWith(scaleBytes);

        // When
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new BigDecimal(bytes));

        // Then
        Assert.Equal("Scale must be greater than or equal to zero. (Parameter 'scale')", exception.Message);
    }
}
