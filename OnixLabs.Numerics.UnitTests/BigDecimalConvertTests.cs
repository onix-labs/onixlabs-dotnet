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

using System;
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalConvertTests
{
    // Invokes the static-virtual INumberBase.CreateChecked through a generic constraint, which exercises
    // BigDecimal.TryConvertToChecked (when TTo is BigDecimal) and TryConvertFromChecked (when TFrom is BigDecimal).
    private static TTo Create<TTo, TFrom>(TFrom value)
        where TTo : INumberBase<TTo>
        where TFrom : INumberBase<TFrom>
        => TTo.CreateChecked(value);

    [Fact(DisplayName = "INumberBase.CreateChecked should convert a BigDecimal to a target numeric type")]
    public void CreateCheckedShouldConvertBigDecimalToTargetType()
    {
        // Given
        BigDecimal value = new(42);

        // When / Then
        Assert.Equal((sbyte)42, Create<sbyte, BigDecimal>(value));
        Assert.Equal((byte)42, Create<byte, BigDecimal>(value));
        Assert.Equal(42, Create<int, BigDecimal>(value));
        Assert.Equal(42L, Create<long, BigDecimal>(value));
        Assert.Equal(42, Create<Int128, BigDecimal>(value));
        Assert.Equal(42, Create<BigInteger, BigDecimal>(value));
        Assert.Equal(42d, Create<double, BigDecimal>(value));
        Assert.Equal(42m, Create<decimal, BigDecimal>(value));
    }

    [Fact(DisplayName = "INumberBase.CreateChecked should convert a numeric value to a BigDecimal")]
    public void CreateCheckedShouldConvertNumericValueToBigDecimal()
    {
        // When / Then
        Assert.Equal(new BigDecimal(42), Create<BigDecimal, int>(42));
        Assert.Equal(new BigDecimal(42), Create<BigDecimal, long>(42L));
        Assert.Equal(new BigDecimal(42), Create<BigDecimal, BigInteger>(42));
    }
}
