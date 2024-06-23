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

public sealed class Base16CodecTests
{
    [Theory(DisplayName = "Base16Codec.Encode should produce the expected result (Invariant)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16CodecEncodeShouldProduceExpectedResultInvariant(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Decode should produce the expected result (Invariant)")]
    [InlineData("", "")]
    [InlineData("4142434445464748494a4b4c4d4e4f505152535455565758595a", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("4142434445464748494A4B4C4D4E4F505152535455565758595A", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696a6b6c6d6e6f707172737475767778797a", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("6162636465666768696A6B6C6D6E6F707172737475767778797A", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839", "0123456789")]
    public void Base16CodecDecodeShouldProduceExpectedResultInvariant(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Encode should produce the expected result (Lowercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16CodecEncodeShouldProduceExpectedResultLowercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base16FormatProvider.Lowercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Decode should produce the expected result (Lowercase)")]
    [InlineData("", "")]
    [InlineData("4142434445464748494a4b4c4d4e4f505152535455565758595a", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696a6b6c6d6e6f707172737475767778797a", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839", "0123456789")]
    public void Base16CodecDecodeShouldProduceExpectedResultLowercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;

        // When
        byte[] bytes = codec.Decode(value, Base16FormatProvider.Lowercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Encode should produce the expected result (Uppercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494A4B4C4D4E4F505152535455565758595A")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696A6B6C6D6E6F707172737475767778797A")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16CodecEncodeShouldProduceExpectedResultUppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base16FormatProvider.Uppercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Decode should produce the expected result (Uppercase)")]
    [InlineData("", "")]
    [InlineData("4142434445464748494A4B4C4D4E4F505152535455565758595A", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696A6B6C6D6E6F707172737475767778797A", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839", "0123456789")]
    public void Base16CodecDecodeShouldProduceExpectedResultUppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;

        // When
        byte[] bytes = codec.Decode(value, Base16FormatProvider.Uppercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16Codec.Encode should throw FormatException when the format provider is invalid")]
    public void Base16CodecEncodeShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Encode(bytes, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base16Codec.Decode should throw FormatException when the format provider is invalid")]
    public void Base16CodecDecodeShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        const string value = "4142434445464748494a4b4c4d4e4f505152535455565758595a";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base16Codec.Decode should throw FormatException when the value is invalid")]
    public void Base16CodecDecodeShouldThrowFormatExceptionWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        const string value = "*INVALID_VALUE*";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base16Codec.TryEncode should return false when the value is invalid")]
    public void Base16CodecTryEncodeShouldReturnFalseWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = new byte[1073741824];

        // When
        bool result = codec.TryEncode(bytes, Base16FormatProvider.Invariant, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base16Codec.TryDecode should return false when the value contains invalid characters (Lowercase)")]
    public void Base16CodecTryDecodeShouldReturnFalseWhenValueContainsInvalidCharactersLowercase()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        const string value = "4142434445464748494A4B4C4D4E4F505152535455565758595A";

        // When
        bool result = codec.TryDecode(value, Base16FormatProvider.Lowercase, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base16Codec.TryDecode should return false when the value contains invalid characters (Uppercase)")]
    public void Base16CodecTryDecodeShouldReturnFalseWhenValueContainsInvalidCharactersUppercase()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        const string value = "4142434445464748494a4b4c4d4e4f505152535455565758595a";

        // When
        bool result = codec.TryDecode(value, Base16FormatProvider.Uppercase, out byte[] _);

        // Then
        Assert.False(result);
    }
}
