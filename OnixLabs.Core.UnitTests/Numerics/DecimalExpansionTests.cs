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

public sealed class DecimalExpansionTests
{
    [Theory(DisplayName = "DecimalExpansion.FromDivision should produce terminating decimal expansions")]
    [InlineData(1, 2, 0, 1)]
    [InlineData(1, 4, 0, 2)]
    [InlineData(1, 8, 0, 3)]
    [InlineData(1, 16, 0, 4)]
    [InlineData(1, 32, 0, 5)]
    [InlineData(1, 64, 0, 6)]
    [InlineData(1, 128, 0, 7)]
    [InlineData(1, 256, 0, 8)]
    [InlineData(1, 512, 0, 9)]
    [InlineData(1, 1024, 0, 10)]
    [InlineData(1, 2048, 0, 11)]
    [InlineData(1, 4096, 0, 12)]
    [InlineData(1, 8192, 0, 13)]
    [InlineData(1, 16384, 0, 14)]
    [InlineData(1, 32768, 0, 15)]
    [InlineData(1, 65536, 0, 16)]
    public void FromDivisionShouldProduceExpectedResultTerminating(decimal dividend, decimal divisor, int integer, int fraction)
    {
        // When
        DecimalExpansion expansion = DecimalExpansion.FromDivision(dividend, divisor);

        // Then
        TerminatingDecimalExpansion terminatingDecimalExpansion = Assert.IsType<TerminatingDecimalExpansion>(expansion);
        Assert.Equal(integer, terminatingDecimalExpansion.IntegerLength);
        Assert.Equal(fraction, terminatingDecimalExpansion.FractionLength);
    }

    [Theory(DisplayName = "DecimalExpansion.FromDivision should produce repeating decimal expansions")]
    [InlineData(1, 3, 0, 1, 0)]
    [InlineData(1, 6, 0, 1, 1)]
    [InlineData(1, 7, 0, 6, 0)]
    [InlineData(1, 9, 0, 1, 0)]
    [InlineData(1, 11, 0, 2, 0)]
    [InlineData(1, 13, 0, 6, 0)]
    [InlineData(1, 14, 0, 6, 1)]
    [InlineData(1, 15, 0, 1, 1)]
    [InlineData(1, 17, 0, 16, 0)]
    [InlineData(1, 18, 0, 1, 1)]
    [InlineData(6758, 87, 2, 28, 0)]
    [InlineData(6758, 875, 1, 6, 3)]
    [InlineData(9949, 56, 3, 6, 3)]
    [InlineData(18.3579, 9.753197531, 1, 34_830, 0)]
    public void FromDivisionShouldProduceExpectedResultRepeating(decimal dividend, decimal divisor, int integer, int repetend, int offset)
    {
        // When
        DecimalExpansion expansion = DecimalExpansion.FromDivision(dividend, divisor);

        // Then
        RepeatingDecimalExpansion repeatingDecimalExpansion = Assert.IsType<RepeatingDecimalExpansion>(expansion);
        Assert.Equal(integer, repeatingDecimalExpansion.IntegerLength);
        Assert.Equal(repetend, repeatingDecimalExpansion.RepetendLength);
        Assert.Equal(offset, repeatingDecimalExpansion.RepetendOffset);
    }

    [Theory(DisplayName = "DecimalExpansion.FromDivision should produce unknown decimal expansions")]
    [InlineData(597.91317, 67.844525, 1)]
    [InlineData(0.19191919, 9.99919991, 0)]
    public void FromDivisionShouldProduceExpectedResultUnknown(decimal dividend, decimal divisor, int integer)
    {
        // When
        DecimalExpansion expansion = DecimalExpansion.FromDivision(dividend, divisor);

        // Then
        UnknownDecimalExpansion unknownDecimalExpansion = Assert.IsType<UnknownDecimalExpansion>(expansion);
        Assert.Equal(integer, unknownDecimalExpansion.IntegerLength);
    }
}
