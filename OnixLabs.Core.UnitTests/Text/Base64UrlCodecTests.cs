// Copyright 2020 ONIXLabs
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
using OnixLabs.Core.Text;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base64UrlCodecTests
{
    /// <summary>
    /// Verifies the example of RFC 7515 Appendix C: the octets 3, 236, 255, 224, 193 encode as "A-z_4ME".
    /// </summary>
    [Fact(DisplayName = "Base64Codec with Base64FormatProvider.Url must satisfy the RFC 7515 Appendix C example in both directions")]
    public void Base64UrlMustSatisfyRfc7515AppendixC()
    {
        // Given
        byte[] octets = [3, 236, 255, 224, 193];

        // When
        string encoded = IBaseCodec.Base64.Encode(octets, Base64FormatProvider.Url);
        byte[] decoded = IBaseCodec.Base64.Decode("A-z_4ME", Base64FormatProvider.Url);

        // Then
        Assert.Equal("A-z_4ME", encoded);
        Assert.Equal(octets, decoded);
    }

    [Theory(DisplayName = "Base64Codec with Base64FormatProvider.Url should round-trip every length up to three blocks")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(31)]
    [InlineData(32)]
    [InlineData(33)]
    [InlineData(1000)]
    public void Base64UrlShouldRoundTrip(int length)
    {
        // Given
        byte[] data = new byte[length];
        new Random(length).NextBytes(data);

        // When
        string encoded = IBaseCodec.Base64.Encode(data, Base64FormatProvider.Url);
        byte[] decoded = IBaseCodec.Base64.Decode(encoded, Base64FormatProvider.Url);

        // Then
        Assert.DoesNotContain('=', encoded);
        Assert.DoesNotContain('+', encoded);
        Assert.DoesNotContain('/', encoded);
        Assert.Equal(Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_'), encoded);
        Assert.Equal(data, decoded);
    }

    [Theory(DisplayName = "Base64Codec with Base64FormatProvider.Url should reject non-canonical and malformed input")]
    [InlineData("A-z_4ME=")]
    [InlineData("A-z_ 4ME")]
    [InlineData("A-z_4ME\n")]
    [InlineData("A+z/4ME")]
    [InlineData("A")]
    [InlineData("AAAAA")]
    [InlineData("AB")]
    [InlineData("AAB")]
    public void Base64UrlShouldRejectInvalidInput(string value)
    {
        // When
        bool result = IBaseCodec.Base64.TryDecode(value, Base64FormatProvider.Url, out byte[] decoded);

        // Then
        Assert.False(result);
        Assert.Empty(decoded);
        Assert.Throws<FormatException>(() => IBaseCodec.Base64.Decode(value, Base64FormatProvider.Url));
    }

    [Fact(DisplayName = "Base64 values should format and parse with Base64FormatProvider.Url")]
    public void Base64ValueShouldFormatAndParseWithBase64Url()
    {
        // Given
        byte[] octets = [3, 236, 255, 224, 193];
        Base64 value = new(octets);
        Span<char> destination = stackalloc char[16];

        // When
        string text = value.ToString(null, Base64FormatProvider.Url);
        bool formatted = ((ISpanFormattable)value).TryFormat(destination, out int charsWritten, default, Base64FormatProvider.Url);
        Base64 parsed = Base64.Parse("A-z_4ME", Base64FormatProvider.Url);

        // Then
        Assert.Equal("A-z_4ME", text);
        Assert.True(formatted);
        Assert.Equal("A-z_4ME", destination[..charsWritten].ToString());
        Assert.Equal(value, parsed);
    }

    [Fact(DisplayName = "Base64 values should not format with Base64FormatProvider.Url into a destination that is too short")]
    public void Base64ValueShouldNotFormatIntoShortDestination()
    {
        // Given
        Base64 value = new([3, 236, 255, 224, 193]);
        Span<char> destination = stackalloc char[6];

        // When
        bool formatted = ((ISpanFormattable)value).TryFormat(destination, out int charsWritten, default, Base64FormatProvider.Url);

        // Then
        Assert.False(formatted);
        Assert.Equal(0, charsWritten);
    }
}
