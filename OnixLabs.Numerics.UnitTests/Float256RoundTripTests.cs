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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256RoundTripTests
{
    [Theory(DisplayName = "Float256 parse(ToString(value)) should equal value for representative finite values")]
    [InlineData(0.0)]
    [InlineData(1.0)]
    [InlineData(-1.0)]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(2.0)]
    [InlineData(10.0)]
    [InlineData(100.0)]
    [InlineData(0.25)]
    [InlineData(0.125)]
    [InlineData(1.5)]
    [InlineData(3.14)]
    [InlineData(2.718281828459045)]
    public void Float256RoundTripParseFormatShouldPreserveValue(double value)
    {
        Float256 original = value;
        string text = original.ToString(null, CultureInfo.InvariantCulture);
        Float256 parsed = Float256.Parse(text, CultureInfo.InvariantCulture);
        Assert.Equal(original.Bits.UpperBits, parsed.Bits.UpperBits);
        Assert.Equal(original.Bits.LowerBits, parsed.Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 parse(ToString(value)) should preserve constants E, Pi, and Tau")]
    public void Float256RoundTripShouldPreserveConstants()
    {
        Assert.Equal(Float256.E.Bits.UpperBits, Float256.Parse(Float256.E.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.UpperBits);
        Assert.Equal(Float256.E.Bits.LowerBits, Float256.Parse(Float256.E.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.LowerBits);
        Assert.Equal(Float256.Pi.Bits.UpperBits, Float256.Parse(Float256.Pi.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.UpperBits);
        Assert.Equal(Float256.Pi.Bits.LowerBits, Float256.Parse(Float256.Pi.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.LowerBits);
        Assert.Equal(Float256.Tau.Bits.UpperBits, Float256.Parse(Float256.Tau.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.UpperBits);
        Assert.Equal(Float256.Tau.Bits.LowerBits, Float256.Parse(Float256.Tau.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).Bits.LowerBits);
    }

    [Fact(DisplayName = "Float256 parse(ToString(NaN)) should produce NaN")]
    public void Float256RoundTripOfNaNShouldProduceNaN()
    {
        Float256 roundTripped = Float256.Parse(Float256.NaN.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float256.IsNaN(roundTripped));
    }

    [Fact(DisplayName = "Float256 parse(ToString(Infinity)) should produce infinity")]
    public void Float256RoundTripOfInfinityShouldProduceInfinity()
    {
        Float256 roundTrippedPositive = Float256.Parse(Float256.PositiveInfinity.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float256.IsPositiveInfinity(roundTrippedPositive));

        Float256 roundTrippedNegative = Float256.Parse(Float256.NegativeInfinity.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float256.IsNegativeInfinity(roundTrippedNegative));
    }
}
