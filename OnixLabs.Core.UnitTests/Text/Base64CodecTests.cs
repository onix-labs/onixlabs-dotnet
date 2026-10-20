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
using System.Text;
using OnixLabs.Core.Text;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base64CodecTests
{
    [Fact(DisplayName = "Base64Codec.Encode should throw FormatException when the format provider is invalid")]
    public void Base64CodecEncodeShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Encode(bytes, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base64Codec.Decode should throw FormatException when the format provider is invalid")]
    public void Base64CodecDecodeShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        const string value = "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base64Codec.Decode should throw FormatException when the value is invalid")]
    public void Base64CodecDecodeShouldThrowFormatExceptionWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        const string value = "*INVALID_VALUE*";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Theory(DisplayName = "Base64Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("MDEyMzQ1Njc4OQ==", "0123456789")]
    public void Base64CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64Codec.Encode should produce the expected result for each format provider")]
    [InlineData("PaddedRfc4648", "", "")]
    [InlineData("PaddedRfc4648", "66", "Zg==")]
    [InlineData("PaddedRfc4648", "666F", "Zm8=")]
    [InlineData("PaddedRfc4648", "666F6F", "Zm9v")]
    [InlineData("PaddedRfc4648", "666F6F62", "Zm9vYg==")]
    [InlineData("PaddedRfc4648", "666F6F6261", "Zm9vYmE=")]
    [InlineData("PaddedRfc4648", "666F6F626172", "Zm9vYmFy")]
    [InlineData("PaddedRfc4648", "FBFF", "+/8=")]
    [InlineData("PaddedRfc4648", "FBEFBE", "++++")]
    [InlineData("PaddedRfc4648", "FFFFFF", "////")]
    [InlineData("UnpaddedRfc4648", "", "")]
    [InlineData("UnpaddedRfc4648", "66", "Zg")]
    [InlineData("UnpaddedRfc4648", "666F", "Zm8")]
    [InlineData("UnpaddedRfc4648", "666F6F", "Zm9v")]
    [InlineData("UnpaddedRfc4648", "666F6F62", "Zm9vYg")]
    [InlineData("UnpaddedRfc4648", "666F6F6261", "Zm9vYmE")]
    [InlineData("UnpaddedRfc4648", "666F6F626172", "Zm9vYmFy")]
    [InlineData("UnpaddedRfc4648", "FBFF", "+/8")]
    [InlineData("UnpaddedRfc4648", "FBEFBE", "++++")]
    [InlineData("UnpaddedRfc4648", "FFFFFF", "////")]
    [InlineData("PaddedRfc4648Url", "", "")]
    [InlineData("PaddedRfc4648Url", "66", "Zg==")]
    [InlineData("PaddedRfc4648Url", "666F", "Zm8=")]
    [InlineData("PaddedRfc4648Url", "666F6F", "Zm9v")]
    [InlineData("PaddedRfc4648Url", "666F6F62", "Zm9vYg==")]
    [InlineData("PaddedRfc4648Url", "666F6F6261", "Zm9vYmE=")]
    [InlineData("PaddedRfc4648Url", "666F6F626172", "Zm9vYmFy")]
    [InlineData("PaddedRfc4648Url", "FBFF", "-_8=")]
    [InlineData("PaddedRfc4648Url", "FBEFBE", "----")]
    [InlineData("PaddedRfc4648Url", "FFFFFF", "____")]
    [InlineData("UnpaddedRfc4648Url", "", "")]
    [InlineData("UnpaddedRfc4648Url", "66", "Zg")]
    [InlineData("UnpaddedRfc4648Url", "666F", "Zm8")]
    [InlineData("UnpaddedRfc4648Url", "666F6F", "Zm9v")]
    [InlineData("UnpaddedRfc4648Url", "666F6F62", "Zm9vYg")]
    [InlineData("UnpaddedRfc4648Url", "666F6F6261", "Zm9vYmE")]
    [InlineData("UnpaddedRfc4648Url", "666F6F626172", "Zm9vYmFy")]
    [InlineData("UnpaddedRfc4648Url", "FBFF", "-_8")]
    [InlineData("UnpaddedRfc4648Url", "FBEFBE", "----")]
    [InlineData("UnpaddedRfc4648Url", "FFFFFF", "____")]
    public void Base64CodecEncodeShouldProduceExpectedResultForEachFormatProvider(string providerName, string hex, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        Base64FormatProvider provider = Base64FormatProvider.FromName(providerName);
        byte[] bytes = Convert.FromHexString(hex);

        // When
        string actual = codec.Encode(bytes, provider);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64Codec.Decode should produce the expected result for each format provider")]
    [InlineData("PaddedRfc4648", "", "")]
    [InlineData("PaddedRfc4648", "Zg==", "66")]
    [InlineData("PaddedRfc4648", "Zm8=", "666F")]
    [InlineData("PaddedRfc4648", "Zm9v", "666F6F")]
    [InlineData("PaddedRfc4648", "Zm9vYg==", "666F6F62")]
    [InlineData("PaddedRfc4648", "Zm9vYmE=", "666F6F6261")]
    [InlineData("PaddedRfc4648", "Zm9vYmFy", "666F6F626172")]
    [InlineData("PaddedRfc4648", "+/8=", "FBFF")]
    [InlineData("PaddedRfc4648", "++++", "FBEFBE")]
    [InlineData("PaddedRfc4648", "////", "FFFFFF")]
    [InlineData("UnpaddedRfc4648", "", "")]
    [InlineData("UnpaddedRfc4648", "Zg", "66")]
    [InlineData("UnpaddedRfc4648", "Zm8", "666F")]
    [InlineData("UnpaddedRfc4648", "Zm9v", "666F6F")]
    [InlineData("UnpaddedRfc4648", "Zm9vYg", "666F6F62")]
    [InlineData("UnpaddedRfc4648", "Zm9vYmE", "666F6F6261")]
    [InlineData("UnpaddedRfc4648", "Zm9vYmFy", "666F6F626172")]
    [InlineData("UnpaddedRfc4648", "+/8", "FBFF")]
    [InlineData("UnpaddedRfc4648", "++++", "FBEFBE")]
    [InlineData("UnpaddedRfc4648", "////", "FFFFFF")]
    [InlineData("PaddedRfc4648Url", "", "")]
    [InlineData("PaddedRfc4648Url", "Zg==", "66")]
    [InlineData("PaddedRfc4648Url", "Zm8=", "666F")]
    [InlineData("PaddedRfc4648Url", "Zm9v", "666F6F")]
    [InlineData("PaddedRfc4648Url", "Zm9vYg==", "666F6F62")]
    [InlineData("PaddedRfc4648Url", "Zm9vYmE=", "666F6F6261")]
    [InlineData("PaddedRfc4648Url", "Zm9vYmFy", "666F6F626172")]
    [InlineData("PaddedRfc4648Url", "-_8=", "FBFF")]
    [InlineData("PaddedRfc4648Url", "----", "FBEFBE")]
    [InlineData("PaddedRfc4648Url", "____", "FFFFFF")]
    [InlineData("UnpaddedRfc4648Url", "", "")]
    [InlineData("UnpaddedRfc4648Url", "Zg", "66")]
    [InlineData("UnpaddedRfc4648Url", "Zm8", "666F")]
    [InlineData("UnpaddedRfc4648Url", "Zm9v", "666F6F")]
    [InlineData("UnpaddedRfc4648Url", "Zm9vYg", "666F6F62")]
    [InlineData("UnpaddedRfc4648Url", "Zm9vYmE", "666F6F6261")]
    [InlineData("UnpaddedRfc4648Url", "Zm9vYmFy", "666F6F626172")]
    [InlineData("UnpaddedRfc4648Url", "-_8", "FBFF")]
    [InlineData("UnpaddedRfc4648Url", "----", "FBEFBE")]
    [InlineData("UnpaddedRfc4648Url", "____", "FFFFFF")]
    public void Base64CodecDecodeShouldProduceExpectedResultForEachFormatProvider(string providerName, string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        Base64FormatProvider provider = Base64FormatProvider.FromName(providerName);

        // When
        byte[] bytes = codec.Decode(value, provider);
        string actual = Convert.ToHexString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64Codec.TryDecode should reject non-canonical values for each format provider")]
    [InlineData("PaddedRfc4648", "Zg")]
    [InlineData("PaddedRfc4648", "Zg=")]
    [InlineData("PaddedRfc4648", "Zm8")]
    [InlineData("PaddedRfc4648", "Z===")]
    [InlineData("PaddedRfc4648", "====")]
    [InlineData("PaddedRfc4648", "Zm9vYg===")]
    [InlineData("PaddedRfc4648", "Zg==Zm9v")]
    [InlineData("PaddedRfc4648", "-_8=")]
    [InlineData("PaddedRfc4648", "Zm9v YmFy")]
    [InlineData("PaddedRfc4648", "Zm9 ")]
    [InlineData("PaddedRfc4648", "QR==")]
    [InlineData("PaddedRfc4648", "Zm9=")]
    [InlineData("UnpaddedRfc4648", "Zg==")]
    [InlineData("UnpaddedRfc4648", "Zm8=")]
    [InlineData("UnpaddedRfc4648", "Zg=A")]
    [InlineData("UnpaddedRfc4648", "-_8")]
    [InlineData("UnpaddedRfc4648", "Zm9v YmFy")]
    [InlineData("UnpaddedRfc4648", "Zm9v\n")]
    [InlineData("UnpaddedRfc4648", "Z")]
    [InlineData("UnpaddedRfc4648", "Zm9vY")]
    [InlineData("UnpaddedRfc4648", "QR")]
    [InlineData("UnpaddedRfc4648", "Zm9")]
    [InlineData("PaddedRfc4648Url", "Zg")]
    [InlineData("PaddedRfc4648Url", "Zg=")]
    [InlineData("PaddedRfc4648Url", "Zm8")]
    [InlineData("PaddedRfc4648Url", "Z===")]
    [InlineData("PaddedRfc4648Url", "====")]
    [InlineData("PaddedRfc4648Url", "Zm9vYg===")]
    [InlineData("PaddedRfc4648Url", "Zg==Zm9v")]
    [InlineData("PaddedRfc4648Url", "+/8=")]
    [InlineData("PaddedRfc4648Url", "Zm9 ")]
    [InlineData("PaddedRfc4648Url", "QR==")]
    [InlineData("PaddedRfc4648Url", "Zm9=")]
    [InlineData("UnpaddedRfc4648Url", "Zg==")]
    [InlineData("UnpaddedRfc4648Url", "Zm8=")]
    [InlineData("UnpaddedRfc4648Url", "Zg=A")]
    [InlineData("UnpaddedRfc4648Url", "+/8")]
    [InlineData("UnpaddedRfc4648Url", "Zm9v YmFy")]
    [InlineData("UnpaddedRfc4648Url", "Zm9v\n")]
    [InlineData("UnpaddedRfc4648Url", "Z")]
    [InlineData("UnpaddedRfc4648Url", "Zm9vY")]
    [InlineData("UnpaddedRfc4648Url", "QR")]
    [InlineData("UnpaddedRfc4648Url", "Zm9")]
    [InlineData("UnpaddedRfc4648Url", "*INVALID*")]
    public void Base64CodecTryDecodeShouldRejectNonCanonicalValuesForEachFormatProvider(string providerName, string value)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        Base64FormatProvider provider = Base64FormatProvider.FromName(providerName);

        // When
        bool success = codec.TryDecode(value, provider, out byte[] _);

        // Then
        Assert.False(success);
    }

    [Theory(DisplayName = "Base64Codec should agree with Convert.ToBase64String for random values")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Base64CodecShouldAgreeWithConvertForRandomValues(int seed)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        Random random = new(seed);

        for (int length = 0; length <= 256; length++)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            string expectedPadded = Convert.ToBase64String(bytes);
            string expectedUrlPadded = expectedPadded.Replace('+', '-').Replace('/', '_');

            // When
            (Base64FormatProvider Provider, string Expected)[] cases =
            [
                (Base64FormatProvider.PaddedRfc4648, expectedPadded),
                (Base64FormatProvider.UnpaddedRfc4648, expectedPadded.TrimEnd('=')),
                (Base64FormatProvider.PaddedRfc4648Url, expectedUrlPadded),
                (Base64FormatProvider.UnpaddedRfc4648Url, expectedUrlPadded.TrimEnd('='))
            ];

            // Then
            foreach ((Base64FormatProvider provider, string expected) in cases)
            {
                Assert.Equal(expected, codec.Encode(bytes, provider));
                Assert.Equal(bytes, codec.Decode(expected, provider));
            }
        }
    }

#if NET9_0_OR_GREATER
    [Theory(DisplayName = "Base64Codec should agree with System.Buffers.Text.Base64Url for random values")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Base64CodecShouldAgreeWithBase64UrlForRandomValues(int seed)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base64;
        Random random = new(seed);

        for (int length = 0; length <= 256; length++)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            string expected = System.Buffers.Text.Base64Url.EncodeToString(bytes);

            // When
            string actual = codec.Encode(bytes, Base64FormatProvider.UnpaddedRfc4648Url);
            byte[] decoded = codec.Decode(expected, Base64FormatProvider.UnpaddedRfc4648Url);

            // Then
            Assert.Equal(expected, actual);
            Assert.Equal(bytes, decoded);
        }
    }
#endif
}
