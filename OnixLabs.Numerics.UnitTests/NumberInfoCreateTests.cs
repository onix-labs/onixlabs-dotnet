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
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumberInfoCreateTests
{
    [NumberInfoCreateIntegerData]
    [Theory(DisplayName = "NumberInfo.Create should produce the expected result (integer values)")]
    public void NumberInfoCreateShouldProduceExpectedResultIntegerValues
        (Int128 value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale)
    {
        // When
        NumberInfo candidate = NumberInfo.Create(value);

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumberInfoCreateFloatData]
    [Theory(DisplayName = "NumberInfo.Create should produce the expected result (float values)")]
    public void NumberInfoCreateShouldProduceExpectedResultFloatValues
        (float value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale)
    {
        // When
        NumberInfo candidate = NumberInfo.Create(value);

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumberInfoCreateDoubleData]
    [Theory(DisplayName = "NumberInfo.Create should produce the expected result (double values)")]
    public void NumberInfoCreateShouldProduceExpectedResultDoubleValues
        (double value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale)
    {
        // When
        NumberInfo candidate = NumberInfo.Create(value);

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }

    [NumberInfoCreateDecimalData]
    [Theory(DisplayName = "NumberInfo.Create should produce the expected result (decimal values)")]
    public void NumberInfoCreateShouldProduceExpectedResultDecimalValues
        (decimal value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale)
    {
        // When
        NumberInfo candidate = NumberInfo.Create(value);

        // Then
        Assert.Equal(significand, candidate.Significand);
        Assert.Equal(exponent, candidate.Exponent);
        Assert.Equal(precision, candidate.Precision);
        Assert.Equal(sign, candidate.Sign);
        Assert.Equal(unscaledValue, candidate.UnscaledValue);
        Assert.Equal(scale, candidate.Scale);
    }
}
