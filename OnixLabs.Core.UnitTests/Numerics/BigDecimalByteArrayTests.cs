// Copyright 2020-2023 ONIXLabs
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

using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalByteArrayTests
{
    [Theory(DisplayName = "BigDecimal.ToByteArray should create a byte array in the same format expected by its constructor")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(0.01)]
    [InlineData(0.0000001)]
    [InlineData(1234567890123456)]
    [InlineData(123456789012345.6)]
    [InlineData(1.234567890123456)]
    public void BigDecimalToByteArrayShouldCreateAByteArrayInTheSameFormatExpectedByItsConstructor(double value)
    {
        // Given
        BigDecimal base2Expected = new(value, ConversionMode.Binary);
        BigDecimal base10Expected = new(value, ConversionMode.Decimal);

        // When
        byte[] base2Bytes = base2Expected.ToByteArray();
        byte[] base10Bytes = base10Expected.ToByteArray();
        
        BigDecimal base2Actual = new(base2Bytes);
        BigDecimal base10Actual = new(base10Bytes);

        // Then
        Assert.Equal(base2Expected, base2Actual);
        Assert.Equal(base10Expected, base10Actual);
    }
}
