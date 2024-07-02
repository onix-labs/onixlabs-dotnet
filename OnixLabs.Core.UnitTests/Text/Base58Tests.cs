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
using System.Buffers;
using System.Text;
using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base58Tests
{
    [Fact(DisplayName = "Base58 should be implicitly constructable from byte[]")]
    public void Base58ShouldBeImplicitlyConstructableFromByteArray()
    {
        // Given
        const string expected = "qBLgTCSW82Hg";
        byte[] value = "ABCabc123".ToByteArray();

        // When
        Base58 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should be implicitly constructable from ReadOnlySpan<byte>")]
    public void Base58ShouldBeImplicitlyConstructableFromReadOnlySpanOfByte()
    {
        // Given
        const string expected = "qBLgTCSW82Hg";
        ReadOnlySpan<byte> value = "ABCabc123".ToByteArray().AsSpan();

        // When
        Base58 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should be implicitly constructable from ReadOnlySequence<byte>")]
    public void Base58ShouldBeImplicitlyConstructableFromReadOnlySequenceOfByte()
    {
        // Given
        byte[] expected = [1, 2, 3, 4];
        ReadOnlySequence<byte> value = new(expected);

        // When
        Base58 candidate = value;
        byte[] actual = candidate.ToByteArray();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should be implicitly constructable from string")]
    public void Base58ShouldBeImplicitlyConstructableFromString()
    {
        // Given
        const string expected = "qBLgTCSW82Hg";
        const string value = "ABCabc123";

        // When
        Base58 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should be implicitly constructable from char[]")]
    public void Base58ShouldBeImplicitlyConstructableFromCharArray()
    {
        // Given
        const string expected = "qBLgTCSW82Hg";
        char[] value = "ABCabc123".ToCharArray();

        // When
        Base58 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should be implicitly constructable from ReadOnlySequence<char>")]
    public void Base58ShouldBeImplicitlyConstructableFromReadOnlySequenceOfChar()
    {
        // Given
        const string expected = "qBLgTCSW82Hg";
        ReadOnlySequence<char> value = new("ABCabc123".ToCharArray());

        // When
        Base58 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should not change when modifying the original byte array")]
    public void Base58ShouldNotChangeWhenModifyingOriginalByteArray()
    {
        // Given
        byte[] bytes = "ABCabc123".ToByteArray();
        Base58 candidate = new(bytes);
        const string expected = "qBLgTCSW82Hg";

        // When
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 should not change when modifying the obtained byte array")]
    public void Base58ShouldNotChangeWhenModifyingObtainedByteArray()
    {
        // Given
        Base58 candidate = new("ABCabc123".ToByteArray());
        const string expected = "qBLgTCSW82Hg";

        // When
        byte[] bytes = candidate.ToByteArray();
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base58 default values should be identical")]
    public void Base58DefaultValuesShouldBeIdentical()
    {
        // Given
        Base58 a = new();
        Base58 b = default;

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base58 values should be identical")]
    public void Base58ValuesShouldBeIdentical()
    {
        // Given
        Base58 a = new([0, 255]);
        Base58 b = new([0, 255]);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base58 values should not be identical")]
    public void Base58ValuesShouldNotBeIdentical()
    {
        // Given
        Base58 a = new([0, 255]);
        Base58 b = new([1, 127]);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Theory(DisplayName = "Base58.Parse should produce the expected result")]
    [InlineData("", "")]
    [InlineData("2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3i37NcgooY8f1S", "0123456789")]
    public void Base58ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base58 candidate = Base58.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58ToStringShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Base58 candidate = new(bytes);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.TryFormat should produce the expected result when the value was formatted correctly")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58TryFormatShouldProduceExpectedResultWhenValueWasFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base58(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[expected.Length];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Length, charsWritten);
    }

    [Theory(DisplayName = "Base58.TryFormat should produce the expected result when the value was not formatted correctly")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58TryFormatShouldProduceExpectedResultWhenValueWasNotFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base58(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[0];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.False(result);
        Assert.NotEqual(expected, actual);
        Assert.Equal(0, charsWritten);
    }

    [Theory(DisplayName = "Base58.TryParse should produce the expected result when the string is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58TryParseShouldProduceTheExpectedResultWhenStringIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base58.TryParse(value, null, out Base58 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.TryParse should produce the expected result when the string is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base58TryParseShouldProduceTheExpectedResultWhenStringIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base58.TryParse(value, null, out Base58 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }

    [Theory(DisplayName = "Base58.TryParse should produce the expected result when the ReadOnlySpan<char> is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void Base58TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base58.TryParse(value.AsSpan(), null, out Base58 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.TryParse should produce the expected result when the ReadOnlySpan<char> is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base58TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base58.TryParse(value.AsSpan(), null, out Base58 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }
}
