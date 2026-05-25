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

public sealed class Float256FormatTests
{
    [Fact(DisplayName = "Float256.ToString of NaN should produce NaN literal")]
    public void Float256ToStringOfNaNShouldProduceNaNLiteral()
    {
        Assert.Equal("NaN", Float256.NaN.ToString("G", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float256.ToString of infinity should produce Infinity literal")]
    public void Float256ToStringOfInfinityShouldProduceInfinityLiteral()
    {
        Assert.Equal("Infinity", Float256.PositiveInfinity.ToString("G", CultureInfo.InvariantCulture));
        Assert.Equal("-Infinity", Float256.NegativeInfinity.ToString("G", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float256.ToString of signed zero should produce 0 or -0")]
    public void Float256ToStringOfSignedZeroShouldProduceSignedZeroLiteral()
    {
        Assert.Equal("0", Float256.Zero.ToString("G", CultureInfo.InvariantCulture));
        Assert.Equal("-0", Float256.NegativeZero.ToString("G", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float256.ToString should use the provider's NaN, infinity and negative-sign symbols")]
    public void Float256ToStringShouldUseProviderSpecialSymbols()
    {
        NumberFormatInfo numberFormat = new()
        {
            NaNSymbol = "NICHTZAHL",
            PositiveInfinitySymbol = "UNENDLICH",
            NegativeInfinitySymbol = "~UNENDLICH",
            NegativeSign = "~",
        };

        Assert.Equal("NICHTZAHL", Float256.NaN.ToString("G", numberFormat));
        Assert.Equal("UNENDLICH", Float256.PositiveInfinity.ToString("G", numberFormat));
        Assert.Equal("~UNENDLICH", Float256.NegativeInfinity.ToString("G", numberFormat));
        Assert.Equal("~0", Float256.NegativeZero.ToString("G", numberFormat));

        // The special-value symbols must round-trip through Parse under the same provider.
        Assert.True(Float256.IsNaN(Float256.Parse(Float256.NaN.ToString("G", numberFormat), numberFormat)));
        Assert.Equal(Float256.PositiveInfinity, Float256.Parse(Float256.PositiveInfinity.ToString("G", numberFormat), numberFormat));
        Assert.Equal(Float256.NegativeInfinity, Float256.Parse(Float256.NegativeInfinity.ToString("G", numberFormat), numberFormat));
    }

    [Theory(DisplayName = "Float256.ToString of small integers should produce integer text")]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(-1, "-1")]
    [InlineData(100, "100")]
    [InlineData(-100, "-100")]
    [InlineData(1000000, "1000000")]
    public void Float256ToStringOfSmallIntegersShouldProduceIntegerText(int value, string expected)
    {
        Float256 wide = value;
        Assert.Equal(expected, wide.ToString(null, CultureInfo.InvariantCulture));
    }

    [Theory(DisplayName = "Float256.ToString of representable binary fractions should produce the expected text")]
    [InlineData(0.5, "0.5")]
    [InlineData(0.25, "0.25")]
    [InlineData(0.125, "0.125")]
    [InlineData(1.5, "1.5")]
    [InlineData(-1.5, "-1.5")]
    [InlineData(2.5, "2.5")]
    public void Float256ToStringOfBinaryFractionsShouldProduceExpectedText(double value, string expected)
    {
        Float256 wide = value;
        Assert.Equal(expected, wide.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Float256.ToString of E should match the constant to at least 33 significant digits")]
    public void Float256ToStringOfEShouldMatchExpectedDigits()
    {
        // Binary128 provides ~34 reliable decimal digits; the trailing 36-digit output reflects
        // the binary representation's residue past that point.
        string asString = Float256.E.ToString(null, CultureInfo.InvariantCulture);
        Assert.StartsWith("2.71828182845904523536028747135266", asString);
    }

    [Fact(DisplayName = "Float256.ToString of Pi should match the constant to at least 33 significant digits")]
    public void Float256ToStringOfPiShouldMatchExpectedDigits()
    {
        string asString = Float256.Pi.ToString(null, CultureInfo.InvariantCulture);
        Assert.StartsWith("3.14159265358979323846264338327950", asString);
    }

    [Fact(DisplayName = "Float256.TryFormat into a sufficient buffer should succeed")]
    public void Float256TryFormatShouldSucceedWithSufficientBuffer()
    {
        Span<char> buffer = stackalloc char[64];
        bool ok = ((ISpanFormattable)Float256.One).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(ok);
        Assert.Equal("1", buffer[..written].ToString());
    }

    [Fact(DisplayName = "Float256.TryFormat into a too-small buffer should fail with zero chars written")]
    public void Float256TryFormatShouldFailWithSmallBuffer()
    {
        Span<char> buffer = stackalloc char[1];
        bool ok = ((ISpanFormattable)(Float256)123).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.False(ok);
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Float256.IUtf8SpanFormattable.TryFormat into a sufficient buffer should succeed")]
    public void Float256Utf8TryFormatShouldSucceedWithSufficientBuffer()
    {
        Span<byte> buffer = stackalloc byte[64];
        bool ok = ((IUtf8SpanFormattable)Float256.One).TryFormat(buffer, out int written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture);
        Assert.True(ok);
        Assert.Equal("1", Encoding.UTF8.GetString(buffer[..written]));
    }
}
