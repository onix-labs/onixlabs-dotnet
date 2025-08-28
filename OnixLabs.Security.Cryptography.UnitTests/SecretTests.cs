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
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class SecretTests
{
    [Fact(DisplayName = "Secret should be constructable from bytes")]
    public void SecretShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        Secret candidate = new(value);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret value should not be modified when altering the original byte array")]
    public void SecretValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Secret candidate = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret value should not be modified when altering the obtained byte array")]
    public void SecretValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        Secret candidate = new byte[] { 1, 2, 3, 4 };
        const string expected = "01020304";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical secret values should be considered equal")]
    public void IdenticalSecretValuesShouldBeConsideredEqual()
    {
        // Given
        Secret left = new byte[] { 1, 2, 3, 4 };
        Secret right = new byte[] { 1, 2, 3, 4 };

        // Then
        Assert.Equal(left, right);
        Assert.True(left.Equals(right));
        Assert.True(left == right);
    }

    [Fact(DisplayName = "Different secret values should not be considered equal")]
    public void DifferentSecretValuesShouldNotBeConsideredEqual()
    {
        // Given
        Secret left = new byte[] { 1, 2, 3, 4 };
        Secret right = new byte[] { 5, 6, 7, 8 };

        // Then
        Assert.NotEqual(left, right);
        Assert.False(left.Equals(right));
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical secret values should produce identical hash codes")]
    public void IdenticalSecretValuesShouldProduceIdenticalSecretCodes()
    {
        // Given
        Secret left = new byte[] { 1, 2, 3, 4 };
        Secret right = new byte[] { 1, 2, 3, 4 };

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different secret values should produce different hash codes")]
    public void DifferentSecretValuesShouldProduceDifferentSecretCodes()
    {
        // Given
        Secret left = new byte[] { 1, 2, 3, 4 };
        Secret right = new byte[] { 5, 6, 7, 8 };

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Secret should be constructable from a string")]
    public void SecretShouldBeConstructableFromString()
    {
        // Given
        const string value = "test";
        Secret candidate = value;
        ReadOnlySpan<byte> expected = Encoding.UTF8.GetBytes(value);

        // When
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret should be constructable from a char array")]
    public void SecretShouldBeConstructableFromCharArray()
    {
        // Given
        char[] value = ['t', 'e', 's', 't'];
        Secret candidate = value;
        ReadOnlySpan<byte> expected = Encoding.UTF8.GetBytes(value);

        // When
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret should be constructable from a ReadOnlySequence<char>")]
    public void SecretShouldBeConstructableFromReadOnlySequence()
    {
        // Given
        ReadOnlySequence<char> value = new(['t', 'e', 's', 't']);
        Secret candidate = value;
        ReadOnlySpan<byte> expected = Encoding.UTF8.GetBytes(value);

        // When
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret should parse a valid string correctly")]
    public void SecretShouldParseValidString()
    {
        // Given
        string value = "01020304";
        Secret expected = new byte[] { 1, 2, 3, 4 };

        // When
        Secret actual = Secret.Parse(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Secret should throw on invalid string parse")]
    public void SecretShouldThrowOnInvalidStringParse()
    {
        // Given
        string value = "invalid";

        // Then
        Assert.Throws<FormatException>(() => Secret.Parse(value));
    }

    [Fact(DisplayName = "TryParse should return true for valid string and output correct Secret")]
    public void TryParseShouldReturnTrueForValidString()
    {
        // Given
        string value = "01020304";
        Secret expected = new byte[] { 1, 2, 3, 4 };

        // When
        bool success = Secret.TryParse(value, null, out Secret actual);

        // Then
        Assert.True(success);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "TryParse should return false for invalid string and output default Secret")]
    public void TryParseShouldReturnFalseForInvalidString()
    {
        // Given
        string value = "invalid";

        // When
        bool success = Secret.TryParse(value, null, out Secret actual);

        // Then
        Assert.False(success);
        Assert.Equal(default, actual);
    }

    [Fact(DisplayName = "ToString should return correct string representation with provided encoding")]
    public void ToStringShouldReturnCorrectStringWithEncoding()
    {
        // Given
        byte[] value = "ABCxyz123".ToByteArray();
        Secret candidate = new(value);
        const string expected = "ABCxyz123";

        // When
        string actual = candidate.ToString(Encoding.UTF8);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToString should return correct string representation with provided format provider")]
    public void ToStringShouldReturnCorrectStringWithFormatProvider()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Secret candidate = new(value);
        string expected = "01020304";

        // When
        string actual = candidate.ToString(Base16FormatProvider.Invariant);

        // Then
        Assert.Equal(expected, actual);
    }
}
