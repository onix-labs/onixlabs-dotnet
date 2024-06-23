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

public sealed class Base58CodecTests
{
    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Bitcoin)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58CodecEncodeShouldProduceExpectedResultBitcoin(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Bitcoin)")]
    [InlineData("", "")]
    [InlineData("2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3i37NcgooY8f1S", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultBitcoin(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Flickr)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E")]
    [InlineData("0123456789", "3H37nBFNNx8E1r")]
    public void Base58CodecEncodeShouldProduceExpectedResultFlickr(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base58FormatProvider.Flickr);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Flickr)")]
    [InlineData("", "")]
    [InlineData("2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3H37nBFNNx8E1r", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultFlickr(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value, Base58FormatProvider.Flickr);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result (Ripple)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC")]
    [InlineData("0123456789", "s5sf4cgooY3CrS")]
    public void Base58CodecEncodeShouldProduceExpectedResultRipple(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base58FormatProvider.Ripple);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result (Ripple)")]
    [InlineData("", "")]
    [InlineData("pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("s5sf4cgooY3CrS", "0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResultRipple(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value, Base58FormatProvider.Ripple);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58Codec.Encode should throw FormatException when the format provider is invalid")]
    public void Base58CodecEncodeShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Encode(bytes, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.Decode should throw FormatException when the format provider is invalid")]
    public void Base58CodecDecodeShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        const string value = "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.Decode should throw FormatException when the value is invalid")]
    public void Base58CodecDecodeShouldThrowFormatExceptionWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        const string value = "*INVALID_VALUE*";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base58Codec.TryEncode should return false when the format provider is invalid")]
    public void Base58CodecTryEncodeShouldReturnFalseWhenValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        bool result = codec.TryEncode(bytes, InvalidFormatProvider.Instance, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base58Codec.TryEncode should pad zero values")]
    public void Base58CodecTryEncodeShouldPadZeroValues()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = [0];
        const string expected = "1";

        // When
        bool result = codec.TryEncode(bytes, Base58FormatProvider.Bitcoin, out string actual);

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }
}
