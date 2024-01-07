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

using System.Numerics;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumberInfoConstantTests
{
    [Fact(DisplayName = "NumberInfo.Zero should produce the expected result")]
    public void NumberInfoZeroShouldProduceExpectedResult()
    {
        // Given
        const int exponent = 0;
        const int precision = 1;
        const int sign = 0;
        const int scale = 0;
        BigInteger significand = 0;
        BigInteger unscaledValue = 0;

        // When
        NumberInfo candidate = NumberInfo.Zero;

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [Fact(DisplayName = "NumberInfo.One should produce the expected result")]
    public void NumberInfoOneShouldProduceExpectedResult()
    {
        // Given
        const int exponent = 0;
        const int precision = 1;
        const int sign = 1;
        const int scale = 0;
        BigInteger significand = 1;
        BigInteger unscaledValue = 1;

        // When
        NumberInfo candidate = NumberInfo.One;

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [Fact(DisplayName = "NumberInfo.NegativeOne should produce the expected result")]
    public void NumberInfoNegativeOneShouldProduceExpectedResult()
    {
        // Given
        const int exponent = 0;
        const int precision = 1;
        const int sign = -1;
        const int scale = 0;
        BigInteger significand = 1;
        BigInteger unscaledValue = -1;

        // When
        NumberInfo candidate = NumberInfo.NegativeOne;

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }
}
