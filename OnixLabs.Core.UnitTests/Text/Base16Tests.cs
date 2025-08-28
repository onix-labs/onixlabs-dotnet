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

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base16Tests
{
    [Fact(DisplayName = "Base16 should be implicitly constructable from byte[]")]
    public void Base16ShouldBeImplicitlyConstructableFromByteArray()
    {
        // Given
        const string expected = "414243616263313233";
        byte[] value = "ABCabc123".ToByteArray();

        // When
        Base16 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should be implicitly constructable from ReadOnlySpan<byte>")]
    public void Base16ShouldBeImplicitlyConstructableFromReadOnlySpanOfByte()
    {
        // Given
        const string expected = "414243616263313233";
        ReadOnlySpan<byte> value = "ABCabc123".ToByteArray();

        // When
        Base16 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should be implicitly constructable from string")]
    public void Base16ShouldBeImplicitlyConstructableFromString()
    {
        // Given
        const string expected = "414243616263313233";
        const string value = "ABCabc123";

        // When
        Base16 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should be implicitly constructable from char[]")]
    public void Base16ShouldBeImplicitlyConstructableFromCharArray()
    {
        // Given
        const string expected = "414243616263313233";
        char[] value = "ABCabc123".ToCharArray();

        // When
        Base16 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should be implicitly constructable from ReadOnlySequence<char>")]
    public void Base16ShouldBeImplicitlyConstructableFromReadOnlySequenceOfChar()
    {
        // Given
        const string expected = "414243616263313233";
        ReadOnlySequence<char> value = new("ABCabc123".ToCharArray());

        // When
        Base16 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should be implicitly constructable from ReadOnlySequence<byte>")]
    public void Base16ShouldBeImplicitlyConstructableFromReadOnlySequenceOfByte()
    {
        // Given
        byte[] expected = [1, 2, 3, 4];
        ReadOnlySequence<byte> value = new(expected);

        // When
        Base16 candidate = value;
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should not change when modifying the original byte array")]
    public void Base16ShouldNotChangeWhenModifyingOriginalByteArray()
    {
        // Given
        byte[] bytes = "ABCabc123".ToByteArray();
        Base16 candidate = new(bytes);
        const string expected = "414243616263313233";

        // When
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 should not change when modifying the obtained byte array")]
    public void Base16ShouldNotChangeWhenModifyingObtainedByteArray()
    {
        // Given
        Base16 candidate = new("ABCabc123".ToByteArray());
        const string expected = "414243616263313233";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base16 default values should be identical")]
    public void Base16DefaultValuesShouldBeIdentical()
    {
        // Given
        Base16 a = new();
        Base16 b = default;

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base16 values should be identical")]
    public void Base16ValuesShouldBeIdentical()
    {
        // Given
        Base16 a = new([0, 255]);
        Base16 b = new([0, 255]);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base16 values should not be identical")]
    public void Base16ValuesShouldNotBeIdentical()
    {
        // Given
        Base16 a = new([0, 255]);
        Base16 b = new([1, 127]);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Theory(DisplayName = "Base16.Parse should produce the expected result")]
    [InlineData("", "")]
    [InlineData("4142434445464748494a4b4c4d4e4f505152535455565758595a", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696a6b6c6d6e6f707172737475767778797a", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839", "0123456789")]
    public void Base16ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base16 candidate = Base16.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16ToStringShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Base16 candidate = new(bytes);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16.TryFormat should produce the expected result when the value was formatted correctly")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16TryFormatShouldProduceExpectedResultWhenValueWasFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base16(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[expected.Length];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Length, charsWritten);
    }

    [Theory(DisplayName = "Base16.TryFormat should produce the expected result when the value was not formatted correctly")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16TryFormatShouldProduceExpectedResultWhenValueWasNotFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base16(Encoding.UTF8.GetBytes(value));
        Span<char> destination = [];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.False(result);
        Assert.NotEqual(expected, actual);
        Assert.Equal(0, charsWritten);
    }

    [Theory(DisplayName = "Base16.TryParse should produce the expected result when the string is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16TryParseShouldProduceTheExpectedResultWhenStringIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base16.TryParse(value, null, out Base16 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16.TryParse should produce the expected result when the string is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base16TryParseShouldProduceTheExpectedResultWhenStringIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base16.TryParse(value, null, out Base16 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }

    [Theory(DisplayName = "Base16.TryParse should produce the expected result when the ReadOnlySpan<char> is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void Base16TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base16.TryParse(value.AsSpan(), null, out Base16 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16.TryParse should produce the expected result when the ReadOnlySpan<char> is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base16TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base16.TryParse(value.AsSpan(), null, out Base16 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }
}
