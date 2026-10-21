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

// ReSharper disable InconsistentNaming
public sealed class IBaseCodecTests
{
    [Fact(DisplayName = "IBaseCodec.GetString should return the expected result")]
    public void IBaseCodecGetStringShouldReturnExpectedResult()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();
        const string expected = "48656c6c6f2c20576f726c6421";

        // When
        string actual = IBaseCodec.GetString(value, Base16FormatProvider.Invariant);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IBaseCodec.GetString should throw FormatException when the format provider is invalid")]
    public void IBaseCodecGetStringShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => IBaseCodec.GetString(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "IBaseCodec.GetBytes should return the expected result")]
    public void IBaseCodecGetBytesShouldReturnExpectedResult()
    {
        // Given
        const string value = "48656c6c6f2c20576f726c6421";
        const string expected = "Hello, World!";

        // When
        byte[] bytes = IBaseCodec.GetBytes(value, Base16FormatProvider.Invariant);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IBaseCodec.GetBytes should throw FormatException when the format provider is invalid")]
    public void IBaseCodecGetBytesShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        const string value = "48656c6c6f2c20576f726c6421";

        // When
        Exception exception = Assert.Throws<FormatException>(() => IBaseCodec.GetBytes(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetString should return true (Base16)")]
    public void IBaseCodecTryGetStringShouldReturnTrueBase16()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        bool result = IBaseCodec.TryGetString(value, Base16FormatProvider.Invariant, out string _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetString should return true (Base32)")]
    public void IBaseCodecTryGetStringShouldReturnTrueBase32()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        bool result = IBaseCodec.TryGetString(value, Base32FormatProvider.Rfc4648, out string _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetString should return true (Base58)")]
    public void IBaseCodecTryGetStringShouldReturnTrueBase58()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        bool result = IBaseCodec.TryGetString(value, Base58FormatProvider.Bitcoin, out string _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetString should return true (Base64)")]
    public void IBaseCodecTryGetStringShouldReturnTrueBase64()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        bool result = IBaseCodec.TryGetString(value, Base64FormatProvider.Rfc4648, out string _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetString should return false when the format provider is invalid")]
    public void IBaseCodecTryGetStringShouldReturnFalseWhenFormatProviderIsInvalid()
    {
        // Given
        byte[] value = "Hello, World!".ToByteArray();

        // When
        bool result = IBaseCodec.TryGetString(value, InvalidFormatProvider.Instance, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetBytes should return true (Base16)")]
    public void IBaseCodecTryGetBytesShouldReturnTrueBase16()
    {
        // Given
        const string value = "48656c6c6f2c20576f726c6421";

        // When
        bool result = IBaseCodec.TryGetBytes(value, Base16FormatProvider.Invariant, out byte[] _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetBytes should return true (Base32)")]
    public void IBaseCodecTryGetBytesShouldReturnTrueBase32()
    {
        // Given
        const string value = "JBSWY3DPFQQFO33SNRSCC";

        // When
        bool result = IBaseCodec.TryGetBytes(value, Base32FormatProvider.Rfc4648, out byte[] _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetBytes should return true (Base58)")]
    public void IBaseCodecTryGetBytesShouldReturnTrueBase58()
    {
        // Given
        const string value = "72k1xXWG59fYdzSNoA";

        // When
        bool result = IBaseCodec.TryGetBytes(value, Base58FormatProvider.Bitcoin, out byte[] _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetBytes should return true (Base64)")]
    public void IBaseCodecTryGetBytesShouldReturnTrueBase64()
    {
        // Given
        const string value = "SGVsbG8sIFdvcmxkIQ==";

        // When
        bool result = IBaseCodec.TryGetBytes(value, Base64FormatProvider.Rfc4648, out byte[] _);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "IBaseCodec.TryGetBytes should return false when the format provider is invalid")]
    public void IBaseCodecTryGetBytesShouldReturnFalseWhenFormatProviderIsInvalid()
    {
        // Given
        const string value = "48656c6c6f2c20576f726c6421";

        // When
        bool result = IBaseCodec.TryGetBytes(value, InvalidFormatProvider.Instance, out byte[] _);

        // Then
        Assert.False(result);
    }
}
