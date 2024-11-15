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

public sealed class Base32Tests
{
    [Fact(DisplayName = "Base32 should be implicitly constructable from byte[]")]
    public void Base32ShouldBeImplicitlyConstructableFromByteArray()
    {
        // Given
        const string expected = "IFBEGYLCMMYTEMY";
        byte[] value = "ABCabc123".ToByteArray();

        // When
        Base32 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should be implicitly constructable from ReadOnlySpan<byte>")]
    public void Base32ShouldBeImplicitlyConstructableFromReadOnlySpanOfByte()
    {
        // Given
        const string expected = "IFBEGYLCMMYTEMY";
        ReadOnlySpan<byte> value = "ABCabc123".ToByteArray();

        // When
        Base32 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should be implicitly constructable from string")]
    public void Base32ShouldBeImplicitlyConstructableFromString()
    {
        // Given
        const string expected = "IFBEGYLCMMYTEMY";
        const string value = "ABCabc123";

        // When
        Base32 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should be implicitly constructable from char[]")]
    public void Base32ShouldBeImplicitlyConstructableFromCharArray()
    {
        // Given
        const string expected = "IFBEGYLCMMYTEMY";
        char[] value = "ABCabc123".ToCharArray();

        // When
        Base32 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should be implicitly constructable from ReadOnlySequence<char>")]
    public void Base32ShouldBeImplicitlyConstructableFromReadOnlySequenceOfChar()
    {
        // Given
        const string expected = "IFBEGYLCMMYTEMY";
        ReadOnlySequence<char> value = new("ABCabc123".ToCharArray());

        // When
        Base32 candidate = value;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should be implicitly constructable from ReadOnlySequence<byte>")]
    public void Base32ShouldBeImplicitlyConstructableFromReadOnlySequenceOfByte()
    {
        // Given
        byte[] expected = [1, 2, 3, 4];
        ReadOnlySequence<byte> value = new(expected);

        // When
        Base32 candidate = value;
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should not change when modifying the original byte array")]
    public void Base32ShouldNotChangeWhenModifyingOriginalByteArray()
    {
        // Given
        byte[] bytes = "ABCabc123".ToByteArray();
        Base32 candidate = new(bytes);
        const string expected = "IFBEGYLCMMYTEMY";

        // When
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should not change when modifying the obtained byte array")]
    public void Base32ShouldNotChangeWhenModifyingObtainedByteArray()
    {
        // Given
        Base32 candidate = new("ABCabc123".ToByteArray());
        const string expected = "IFBEGYLCMMYTEMY";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 default values should be identical")]
    public void Base32DefaultValuesShouldBeIdentical()
    {
        // Given
        Base32 a = new();
        Base32 b = default;

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = new([0, 255]);
        Base32 b = new([0, 255]);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base32 values should not be identical")]
    public void Base32ValuesShouldNotBeIdentical()
    {
        // Given
        Base32 a = new([0, 255]);
        Base32 b = new([1, 127]);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected result")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base32 candidate = Base32.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32ToStringShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Base32 candidate = new(bytes);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.TryFormat should produce the expected result when the value was formatted correctly")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32TryFormatShouldProduceExpectedResultWhenValueWasFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base32(Encoding.UTF8.GetBytes(value));
        Span<char> destination = stackalloc char[expected.Length];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Length, charsWritten);
    }

    [Theory(DisplayName = "Base32.TryFormat should produce the expected result when the value was not formatted correctly")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32TryFormatShouldProduceExpectedResultWhenValueWasNotFormattedCorrectly(string value, string expected)
    {
        // Given
        ISpanFormattable candidate = new Base32(Encoding.UTF8.GetBytes(value));
        Span<char> destination = [];

        // When
        bool result = candidate.TryFormat(destination, out int charsWritten, [], null);
        string actual = destination.ToString();

        // Then
        Assert.False(result);
        Assert.NotEqual(expected, actual);
        Assert.Equal(0, charsWritten);
    }

    [Theory(DisplayName = "Base32.TryParse should produce the expected result when the string is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32TryParseShouldProduceTheExpectedResultWhenStringIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base32.TryParse(value, null, out Base32 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.TryParse should produce the expected result when the string is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base32TryParseShouldProduceTheExpectedResultWhenStringIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base32.TryParse(value, null, out Base32 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }

    [Theory(DisplayName = "Base32.TryParse should produce the expected result when the ReadOnlySpan<char> is parsed successfully")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base32.TryParse(value.AsSpan(), null, out Base32 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.TryParse should produce the expected result when the ReadOnlySpan<char> is not parsed successfully")]
    [InlineData("", "*INVALID_VALUE*")]
    public void Base32TryParseShouldProduceTheExpectedResultWhenReadOnlySpanOfCharIsNotParsedSuccessfully(string expected, string value)
    {
        // When
        bool result = Base32.TryParse(value.AsSpan(), null, out Base32 candidate);
        string actual = Encoding.UTF8.GetString(candidate.AsReadOnlySpan());

        // Then
        Assert.False(result);
        Assert.Equal(expected, actual);
        Assert.Equal(default, candidate);
    }
}
