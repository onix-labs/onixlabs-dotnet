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

public sealed class Int512FormatTests
{
    [Fact(DisplayName = "Int512.ToString with no format for positive value should produce decimal")]
    public void Int512ToStringNoFormatPositiveShouldProduceDecimal()
    {
        Int512 value = (Int512)12345;
        Assert.Equal("12345", value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.ToString with no format for negative value should produce negative decimal")]
    public void Int512ToStringNoFormatNegativeShouldProduceNegativeDecimal()
    {
        Int512 value = (Int512)(-12345);
        Assert.Equal("-12345", value.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.ToString with 'D' format should produce decimal representation")]
    public void Int512ToStringDShouldProduceDecimal()
    {
        Int512 value = (Int512)(-9876);
        Assert.Equal("-9876", value.ToString("D", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.ToString of MinValue should produce the canonical -2^511 decimal string")]
    public void Int512ToStringMinValueShouldProduceCanonicalDecimal()
    {
        string expected = "-6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042048";
        Assert.Equal(expected, Int512.MinValue.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.ToString of MaxValue should produce the canonical 2^511 - 1 decimal string")]
    public void Int512ToStringMaxValueShouldProduceCanonicalDecimal()
    {
        string expected = "6703903964971298549787012499102923063739682910296196688861780721860882015036773488400937149083451713845015929093243025426876941405973284973216824503042047";
        Assert.Equal(expected, Int512.MaxValue.ToString(null, CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.ToString with N0 format should match BigInteger thousands separator output")]
    public void Int512ToStringN0FormatShouldMatchBigInteger()
    {
        Int512 value = (Int512)(-1234567);
        Assert.Equal("-1,234,567", value.ToString("N0", CultureInfo.InvariantCulture));
    }

    [Fact(DisplayName = "Int512.TryFormat into a span should succeed when the destination is large enough")]
    public void Int512TryFormatShouldSucceedWithLargeDestination()
    {
        Int512 value = (Int512)(-987654);
        Span<char> buffer = stackalloc char[16];
        Assert.True(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal("-987654", buffer[..written].ToString());
    }

    [Fact(DisplayName = "Int512.TryFormat should fail when destination is too small")]
    public void Int512TryFormatShouldFailWithSmallDestination()
    {
        Int512 value = (Int512)(-987654);
        Span<char> buffer = stackalloc char[3];
        Assert.False(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }

    [Fact(DisplayName = "Int512.TryFormat with 'D10' format should pad negative numbers")]
    public void Int512TryFormatWithD10FormatShouldPadNegative()
    {
        Int512 value = (Int512)(-42);
        Span<char> buffer = stackalloc char[16];
        Assert.True(value.TryFormat(buffer, out int written, "D10", CultureInfo.InvariantCulture));
        Assert.Equal("-0000000042", buffer[..written].ToString());
    }

    [Fact(DisplayName = "Int512.TryFormat into UTF-8 should produce expected bytes for negative")]
    public void Int512TryFormatUtf8ShouldProduceExpectedBytesForNegative()
    {
        Int512 value = (Int512)(-12345);
        Span<byte> buffer = stackalloc byte[32];
        Assert.True(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal("-12345", Encoding.UTF8.GetString(buffer[..written]));
    }

    [Fact(DisplayName = "Int512.TryFormat UTF-8 should fail when destination is too small")]
    public void Int512TryFormatUtf8ShouldFailWithSmallDestination()
    {
        Int512 value = (Int512)(-12345);
        Span<byte> buffer = stackalloc byte[3];
        Assert.False(value.TryFormat(buffer, out int written, default, CultureInfo.InvariantCulture));
        Assert.Equal(0, written);
    }
}
