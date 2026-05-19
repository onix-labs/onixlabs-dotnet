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
using System.Globalization;
using System.Text;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float128FormatTests
{
    [Fact(DisplayName = "Float128.ToString of NaN should produce NaN literal")]
    public void Float128ToStringOfNaNShouldProduceNaNLiteral()
    {
        Assert.Equal("NaN", Float128.NaN.ToString());
    }

    [Fact(DisplayName = "Float128.ToString of infinity should produce Infinity literal")]
    public void Float128ToStringOfInfinityShouldProduceInfinityLiteral()
    {
        Assert.Equal("Infinity", Float128.PositiveInfinity.ToString());
        Assert.Equal("-Infinity", Float128.NegativeInfinity.ToString());
    }

    [Fact(DisplayName = "Float128.ToString of signed zero should produce 0 or -0")]
    public void Float128ToStringOfSignedZeroShouldProduceSignedZeroLiteral()
    {
        Assert.Equal("0", Float128.Zero.ToString());
        Assert.Equal("-0", Float128.NegativeZero.ToString());
    }

    [Theory(DisplayName = "Float128.ToString of small integers should produce integer text")]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(-1, "-1")]
    [InlineData(100, "100")]
    [InlineData(-100, "-100")]
    [InlineData(1000000, "1000000")]
    public void Float128ToStringOfSmallIntegersShouldProduceIntegerText(int value, string expected)
    {
        Float128 wide = value;
        Assert.Equal(expected, wide.ToString(null, CultureInfo.InvariantCulture));
    }

    [Theory(DisplayName = "Float128.ToString of representable binary fractions should produce the expected text")]
    [InlineData(0.5, "0.5")]
    [InlineData(0.25, "0.25")]
    [InlineData(0.125, "0.125")]
    [InlineData(1.5, "1.5")]
    [InlineData(-1.5, "-1.5")]
    [InlineData(2.5, "2.5")]
    public void Float128ToStringOfBinaryFractionsShouldProduceExpectedText(double value, string expected)
    {
        Float128 wide = value;
        Assert.Equal(expected, wide.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float128.ToString of E should match the constant to at least 33 significant digits")]
    public void Float128ToStringOfEShouldMatchExpectedDigits()
    {
        // Binary128 provides ~34 reliable decimal digits; the trailing 36-digit output reflects
        // the binary representation's residue past that point.
        string asString = Float128.E.ToString(null, CultureInfo.InvariantCulture);
        Assert.StartsWith("2.71828182845904523536028747135266", asString);
    }

    [Fact(DisplayName = "Float128.ToString of Pi should match the constant to at least 33 significant digits")]
    public void Float128ToStringOfPiShouldMatchExpectedDigits()
    {
        string asString = Float128.Pi.ToString(null, CultureInfo.InvariantCulture);
        Assert.StartsWith("3.14159265358979323846264338327950", asString);
    }

    [Fact(DisplayName = "Float128.TryFormat into a sufficient buffer should succeed")]
    public void Float128TryFormatShouldSucceedWithSufficientBuffer()
    {
        Span<char> buffer = stackalloc char[64];
        bool ok = ((ISpanFormattable)Float128.One).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(ok);
        Assert.Equal("1", buffer[..written].ToString());
    }

    [Fact(DisplayName = "Float128.TryFormat into a too-small buffer should fail with zero chars written")]
    public void Float128TryFormatShouldFailWithSmallBuffer()
    {
        Span<char> buffer = stackalloc char[1];
        bool ok = ((ISpanFormattable)(Float128)123).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.False(ok);
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Float128.IUtf8SpanFormattable.TryFormat into a sufficient buffer should succeed")]
    public void Float128Utf8TryFormatShouldSucceedWithSufficientBuffer()
    {
        Span<byte> buffer = stackalloc byte[64];
        bool ok = ((IUtf8SpanFormattable)Float128.One).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(ok);
        Assert.Equal("1", Encoding.UTF8.GetString(buffer[..written]));
    }
}
