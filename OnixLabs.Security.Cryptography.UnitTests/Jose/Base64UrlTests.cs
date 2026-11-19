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
using OnixLabs.Security.Cryptography.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests.Jose;

public sealed class Base64UrlTests
{
    /// <summary>
    /// Verifies the example of RFC 7515 Appendix C: the octets 3, 236, 255, 224, 193 encode as "A-z_4ME".
    /// </summary>
    [Fact(DisplayName = "Base64Url must satisfy the RFC 7515 Appendix C example in both directions")]
    public void Base64UrlMustSatisfyRfc7515AppendixC()
    {
        // Given
        byte[] octets = [3, 236, 255, 224, 193];

        // When
        string encoded = Base64Url.Encode(octets);
        byte[] decoded = Base64Url.Decode("A-z_4ME");

        // Then
        Assert.Equal("A-z_4ME", encoded);
        Assert.Equal(octets, decoded);
    }

    [Theory(DisplayName = "Base64Url should round-trip every length up to three blocks")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(31)]
    [InlineData(32)]
    [InlineData(33)]
    public void Base64UrlShouldRoundTrip(int length)
    {
        // Given
        byte[] data = new byte[length];
        new Random(length).NextBytes(data);

        // When
        string encoded = Base64Url.Encode(data);

        // Then
        Assert.DoesNotContain('=', encoded);
        Assert.DoesNotContain('+', encoded);
        Assert.DoesNotContain('/', encoded);
        Assert.Equal(data, Base64Url.Decode(encoded));
    }

    [Theory(DisplayName = "Base64Url.TryDecode should reject padding, whitespace, '+', '/' and impossible lengths")]
    [InlineData("A-z_4ME=")]
    [InlineData("A-z_ 4ME")]
    [InlineData("A-z_4ME\n")]
    [InlineData("A+z/4ME")]
    [InlineData("A")]
    [InlineData("AAAAA")]
    public void Base64UrlTryDecodeShouldRejectInvalidInput(string value)
    {
        // When
        bool result = Base64Url.TryDecode(value, out byte[] decoded);

        // Then
        Assert.False(result);
        Assert.Empty(decoded);
        Assert.Throws<FormatException>(() => Base64Url.Decode(value));
    }
}
