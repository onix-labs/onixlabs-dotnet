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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256FusedMultiplyAddTests
{
    [Fact(DisplayName = "FusedMultiplyAdd(2, 3, 4) should equal 10")]
    public void FusedMultiplyAddSimpleShouldEqual10()
    {
        Float256 result = Float256.FusedMultiplyAdd((Float256)2, (Float256)3, (Float256)4);
        Assert.Equal((Float256)10, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of small integers should match exact computation")]
    public void FusedMultiplyAddSmallIntegersShouldMatchExact()
    {
        for (int a = -10; a <= 10; a++)
        {
            for (int b = -10; b <= 10; b++)
            {
                for (int c = -10; c <= 10; c++)
                {
                    Float256 result = Float256.FusedMultiplyAdd((Float256)a, (Float256)b, (Float256)c);
                    Float256 expected = (Float256)(a * b + c);
                    Assert.Equal(expected, result);
                }
            }
        }
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c=0 should equal product")]
    public void FusedMultiplyAddWithZeroAddendShouldEqualProduct()
    {
        Float256 a = Float256.Parse("3.14159265358979323846264338327950288");
        Float256 b = Float256.Parse("2.71828182845904523536028747135266250");
        Float256 expected = a * b;
        Float256 result = Float256.FusedMultiplyAdd(a, b, Float256.Zero);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with a=0 should equal c")]
    public void FusedMultiplyAddWithZeroMultiplicandShouldEqualAddend()
    {
        Float256 c = Float256.Parse("123.456789012345678901234567890123456");
        Float256 result = Float256.FusedMultiplyAdd(Float256.Zero, Float256.Parse("99999.9"), c);
        Assert.Equal(c, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with cancellation should produce small magnitude")]
    public void FusedMultiplyAddWithCancellationShouldProduceSmallMagnitude()
    {
        Float256 a = (Float256)1000;
        Float256 b = (Float256)1000;
        Float256 c = (Float256)(-1000000);
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float256.Zero, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with near-cancellation preserves small contribution")]
    public void FusedMultiplyAddWithNearCancellationPreservesSmallContribution()
    {
        Float256 a = (Float256)1000;
        Float256 b = (Float256)1000;
        Float256 c = (Float256)(-1000000) + Float256.Parse("0.5");
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float256.Parse("0.5"), result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with same signs should add magnitudes")]
    public void FusedMultiplyAddWithSameSignsShouldAddMagnitudes()
    {
        Float256 a = Float256.Parse("2.5");
        Float256 b = Float256.Parse("4");
        Float256 c = Float256.Parse("3.0");
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal((Float256)13, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with opposite signs should subtract magnitudes")]
    public void FusedMultiplyAddWithOppositeSignsShouldSubtractMagnitudes()
    {
        Float256 a = Float256.Parse("2.5");
        Float256 b = Float256.Parse("4");
        Float256 c = Float256.Parse("-3");
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal((Float256)7, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of -2 * -3 + -4 should equal 2")]
    public void FusedMultiplyAddNegativeMultiplicandsShouldGivePositiveProduct()
    {
        Float256 result = Float256.FusedMultiplyAdd((Float256)(-2), (Float256)(-3), (Float256)(-4));
        Assert.Equal((Float256)2, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd preserves precision better than separate multiply+add")]
    public void FusedMultiplyAddPreservesPrecisionBetterThanSeparateOps()
    {
        Float256 a = Float256.Parse("1.0000000000000000000000000000000001");
        Float256 b = (Float256)2;
        Float256 c = Float256.Parse("-2.0000000000000000000000000000000002");
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(Float256.Zero, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c much larger than product should equal c")]
    public void FusedMultiplyAddWithCMuchLargerThanProductShouldRoundToC()
    {
        Float256 a = Float256.Epsilon;
        Float256 b = Float256.Epsilon;
        Float256 c = (Float256)1000;
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(c, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd with c much smaller than product should equal product")]
    public void FusedMultiplyAddWithCMuchSmallerThanProductShouldRoundToProduct()
    {
        Float256 a = (Float256)1000;
        Float256 b = (Float256)1000;
        Float256 c = Float256.Epsilon;
        Float256 expected = a * b;
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd of fractional values should round once")]
    public void FusedMultiplyAddOfFractionalValuesShouldRoundOnce()
    {
        Float256 a = Float256.Parse("0.1");
        Float256 b = Float256.Parse("0.1");
        Float256 c = Float256.Parse("0.01");
        Float256 expected = Float256.Parse("0.02");
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Float256 difference = Float256.Abs(result - expected);
        Float256 tolerance = Float256.Parse("1E-67");
        Assert.True(difference < tolerance);
    }

    [Fact(DisplayName = "FusedMultiplyAdd produces same sign zero rules")]
    public void FusedMultiplyAddZeroSignRules()
    {
        Assert.Equal(Float256.Zero, Float256.FusedMultiplyAdd(Float256.Zero, Float256.Zero, Float256.Zero));
        Assert.Equal(Float256.NegativeZero, Float256.FusedMultiplyAdd(Float256.NegativeZero, Float256.Zero, Float256.NegativeZero));
        Assert.Equal(Float256.Zero, Float256.FusedMultiplyAdd(Float256.Zero, Float256.NegativeZero, Float256.Zero));
    }

    [Fact(DisplayName = "FusedMultiplyAdd respects infinity propagation")]
    public void FusedMultiplyAddRespectsInfinityPropagation()
    {
        Assert.Equal(Float256.PositiveInfinity, Float256.FusedMultiplyAdd(Float256.PositiveInfinity, Float256.One, Float256.Zero));
        Assert.Equal(Float256.NegativeInfinity, Float256.FusedMultiplyAdd(Float256.NegativeInfinity, Float256.One, Float256.Zero));
        Assert.Equal(Float256.PositiveInfinity, Float256.FusedMultiplyAdd(Float256.One, Float256.One, Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "FusedMultiplyAdd of (Inf, 1, -Inf) returns NaN")]
    public void FusedMultiplyAddOfInfMinusInfReturnsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.FusedMultiplyAdd(Float256.PositiveInfinity, Float256.One, Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "FusedMultiplyAdd with subnormal addend handles correctly")]
    public void FusedMultiplyAddWithSubnormalAddendHandlesCorrectly()
    {
        Float256 a = (Float256)2;
        Float256 b = (Float256)3;
        Float256 c = Float256.Epsilon;
        Float256 expected = (Float256)6 + c;
        Float256 result = Float256.FusedMultiplyAdd(a, b, c);
        Assert.Equal(expected, result);
    }

    [Fact(DisplayName = "FusedMultiplyAdd matches BigDecimal oracle for assorted finite values")]
    public void FusedMultiplyAddMatchesBigDecimalOracleForAssortedValues()
    {
        (string a, string b, string c)[] cases =
        {
            ("3.14159265358979323846264338327950288", "2.71828182845904523536028747135266250", "1.41421356237309504880168872420969807"),
            ("1.5", "2.5", "3.5"),
            ("1E10", "1E10", "1E20"),
            ("1E-30", "1E10", "1"),
            ("-1.5", "1.5", "2.25"),
            ("1.000000000000000000000000000000001", "1.000000000000000000000000000000001", "-1"),
            ("0.5", "0.5", "0.25"),
            ("7", "11", "13"),
            ("1.7976931348623157E308", "1E-308", "1"),
            ("1.0001", "1.0001", "-1.0002"),
        };

        foreach ((string aStr, string bStr, string cStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            Float256 c = Float256.Parse(cStr);

            BigDecimal exact = (BigDecimal)a * (BigDecimal)b + (BigDecimal)c;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.FusedMultiplyAdd(a, b, c);

            Assert.Equal(expected, actual);
        }
    }
}
