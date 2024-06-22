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

public sealed class Base64Tests
{
    [Fact(DisplayName = "Base64 should be implicitly constructable from byte[]")]
    public void Base64ShouldBeImplicitlyConstructableFromByteArray()
    {
        // Given
        const string expected = "QUJDYWJjMTIz";
        byte[] value = "ABCabc123".ToByteArray();

        // When
        Base64 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should be implicitly constructable from ReadOnlySpan<byte>")]
    public void Base64ShouldBeImplicitlyConstructableFromReadOnlySpanOfByte()
    {
        // Given
        const string expected = "QUJDYWJjMTIz";
        ReadOnlySpan<byte> value = "ABCabc123".ToByteArray().AsSpan();

        // When
        Base64 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should be implicitly constructable from string")]
    public void Base64ShouldBeImplicitlyConstructableFromString()
    {
        // Given
        const string expected = "QUJDYWJjMTIz";
        const string value = "ABCabc123";

        // When
        Base64 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should be implicitly constructable from char[]")]
    public void Base64ShouldBeImplicitlyConstructableFromCharArray()
    {
        // Given
        const string expected = "QUJDYWJjMTIz";
        char[] value = "ABCabc123".ToCharArray();

        // When
        Base64 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should be implicitly constructable from ReadOnlySequence<char>")]
    public void Base64ShouldBeImplicitlyConstructableFromReadOnlySequenceOfChar()
    {
        // Given
        const string expected = "QUJDYWJjMTIz";
        ReadOnlySequence<char> value = new("ABCabc123".ToCharArray());

        // When
        Base64 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should not change when modifying the original byte array")]
    public void Base64ShouldNotChangeWhenModifyingOriginalByteArray()
    {
        // Given
        byte[] bytes = "ABCabc123".ToByteArray();
        Base64 candidate = new(bytes);
        const string expected = "QUJDYWJjMTIz";

        // When
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 should not change when modifying the obtained byte array")]
    public void Base64ShouldNotChangeWhenModifyingObtainedByteArray()
    {
        // Given
        Base64 candidate = new("ABCabc123".ToByteArray());
        const string expected = "QUJDYWJjMTIz";

        // When
        byte[] bytes = candidate.ToByteArray();
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base64 default values should be identical")]
    public void Base64DefaultValuesShouldBeIdentical()
    {
        // Given
        Base64 a = new();
        Base64 b = default;

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base64 values should be identical")]
    public void Base64ValuesShouldBeIdentical()
    {
        // Given
        Base64 a = new([0, 255]);
        Base64 b = new([0, 255]);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base64 values should not be identical")]
    public void Base64ValuesShouldNotBeIdentical()
    {
        // Given
        Base64 a = new([0, 255]);
        Base64 b = new([1, 127]);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Theory(DisplayName = "Base64.Parse should produce the expected result")]
    [InlineData("", "")]
    [InlineData("QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("MDEyMzQ1Njc4OQ==", "0123456789")]
    public void Base64ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base64 candidate = Base64.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64ToStringShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Base64 candidate = new(bytes);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64.TryFormat should produce the expected result when the value was formatted correctly")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64TryFormatShouldProduceExpectedResultWhenValueWasFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base64(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[expected.Length];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Length, charsWritten);
    }

    [Theory(DisplayName = "Base64.TryFormat should produce the expected result when the value was not formatted correctly")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64TryFormatShouldProduceExpectedResultWhenValueWasNotFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base64(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[0];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.False(result);
        Assert.NotEqual(expected, actual);
        Assert.Equal(0, charsWritten);
    }

    [Theory(DisplayName = "Base64.TryParse should produce the expected result when the string is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64TryParseShouldProduceTheExpectedResultWhenStringIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base64.TryParse(value, null, out Base64 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64.TryParse should produce the expected result when the string is not parsed successfully")]
    [InlineData("", "*INVALID VALUE*")]
    public void Base64TryParseShouldProduceTheExpectedResultWhenStringIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base64.TryParse(value, null, out Base64 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }

    [Theory(DisplayName = "Base64.TryParse should produce the expected result when the ReadOnlySpan<char> is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void Base64TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base64.TryParse(value.AsSpan(), null, out Base64 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64.TryParse should produce the expected result when the ReadOnlySpan<char> is not parsed successfully")]
    [InlineData("", "*INVALID VALUE*")]
    public void Base64TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base64.TryParse(value.AsSpan(), null, out Base64 candidate);
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }
}
