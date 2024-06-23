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
using Xunit;

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
}
