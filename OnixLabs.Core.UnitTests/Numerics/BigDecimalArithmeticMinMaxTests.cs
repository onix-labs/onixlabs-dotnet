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

public sealed class BigDecimalArithmeticMinMaxTests
{
    [Theory(DisplayName = "BigDecimal.MinMax should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMinMaxShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        (BigDecimal minCandidate, BigDecimal maxCandidate) = BigDecimal.MinMax(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.True(minCandidate <= maxCandidate);
    }

    [Theory(DisplayName = "BigDecimal.Min should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMinShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        BigDecimal candidate = BigDecimal.Min(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.Equal(candidate, min);
    }

    [Theory(DisplayName = "BigDecimal.Max should return the expected result")]
    [InlineData(sbyte.MinValue, sbyte.MaxValue)]
    [InlineData(byte.MinValue, byte.MaxValue)]
    [InlineData(short.MinValue, short.MaxValue)]
    [InlineData(ushort.MinValue, ushort.MaxValue)]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(uint.MinValue, uint.MaxValue)]
    [InlineData(long.MinValue, long.MaxValue)]
    [InlineData(ulong.MinValue, ulong.MaxValue)]
    public void BigDecimalMaxShouldReturnTheExpectedResult(decimal min, decimal max)
    {
        // Arrange / Act
        BigDecimal candidate = BigDecimal.Max(min.ToBigDecimal(), max.ToBigDecimal());

        // Assert
        Assert.Equal(candidate, max);
    }
}
