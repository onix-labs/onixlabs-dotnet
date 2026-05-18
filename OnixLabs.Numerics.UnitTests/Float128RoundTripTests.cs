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

public sealed class Float128RoundTripTests
{
    [Theory(DisplayName = "Float128 parse(ToString(value)) should equal value for representative finite values")]
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
    public void Float128RoundTripParseFormatShouldPreserveValue(double value)
    {
        Float128 original = value;
        string text = original.ToString(null, CultureInfo.InvariantCulture);
        Float128 parsed = Float128.Parse(text, CultureInfo.InvariantCulture);
        Assert.Equal(original.RawBits, parsed.RawBits);
    }

    [Fact(DisplayName = "Float128 parse(ToString(value)) should preserve constants E, Pi, and Tau")]
    public void Float128RoundTripShouldPreserveConstants()
    {
        Assert.Equal(Float128.E.RawBits, Float128.Parse(Float128.E.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).RawBits);
        Assert.Equal(Float128.Pi.RawBits, Float128.Parse(Float128.Pi.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).RawBits);
        Assert.Equal(Float128.Tau.RawBits, Float128.Parse(Float128.Tau.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).RawBits);
    }

    [Fact(DisplayName = "Float128 parse(ToString(NaN)) should produce NaN")]
    public void Float128RoundTripOfNaNShouldProduceNaN()
    {
        Float128 roundTripped = Float128.Parse(Float128.NaN.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float128.IsNaN(roundTripped));
    }

    [Fact(DisplayName = "Float128 parse(ToString(Infinity)) should produce infinity")]
    public void Float128RoundTripOfInfinityShouldProduceInfinity()
    {
        Float128 roundTrippedPositive = Float128.Parse(Float128.PositiveInfinity.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float128.IsPositiveInfinity(roundTrippedPositive));

        Float128 roundTrippedNegative = Float128.Parse(Float128.NegativeInfinity.ToString(null, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        Assert.True(Float128.IsNegativeInfinity(roundTrippedNegative));
    }
}
