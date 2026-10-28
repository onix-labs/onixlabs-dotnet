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
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
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

    [Fact(DisplayName = "Default Secret should not throw when disposed")]
    public void DefaultSecretShouldNotThrowWhenDisposed()
    {
        // Given
        Secret candidate = default;

        // When / Then
        candidate.Dispose();
    }

    [Fact(DisplayName = "Default Secret should return an empty span")]
    public void DefaultSecretShouldReturnAnEmptySpan()
    {
        // Given
        Secret candidate = default;

        // When
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.True(actual.IsEmpty);
    }

    [Fact(DisplayName = "Default Secret should return an empty string")]
    public void DefaultSecretShouldReturnAnEmptyString()
    {
        // Given
        Secret candidate = default;

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(string.Empty, actual);
    }

    [Fact(DisplayName = "Secret should throw ObjectDisposedException when read after being disposed")]
    public void SecretShouldThrowObjectDisposedExceptionWhenReadAfterBeingDisposed()
    {
        // Given
        Secret candidate = new([1, 2, 3, 4]);

        // When
        candidate.Dispose();

        // Then
        Assert.Throws<ObjectDisposedException>(() => candidate.AsReadOnlyMemory());
    }

    [Fact(DisplayName = "Secret should return the original value when read concurrently from many threads")]
    public void SecretShouldReturnTheOriginalValueWhenReadConcurrentlyFromManyThreads()
    {
        // Given
        byte[] expected = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17];
        Secret candidate = new(expected);
        ParallelOptions options = new() { MaxDegreeOfParallelism = Math.Max(8, Environment.ProcessorCount) };
        ConcurrentBag<byte[]> results = [];

        // When
        Parallel.For(fromInclusive: 0, toExclusive: 20_000, options, _ => results.Add(candidate.AsReadOnlySpan().ToArray()));

        // Then
        Assert.Equal(20_000, results.Count);
        Assert.All(results, actual => Assert.Equal(expected, actual));
    }

    [Theory(DisplayName = "Secret.CopyTo should write exactly the original value for lengths around the AES block size")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(32)]
    [InlineData(300)]
    public void SecretCopyToShouldWriteExactlyTheOriginalValue(int length)
    {
        // Given
        byte[] expected = CreateValue(length);
        Secret candidate = new(expected);
        byte[] larger = new byte[length + 40];
        larger.AsSpan().Fill(0xAA);

        // When
        candidate.CopyTo(larger);

        // Then
        Assert.Equal(length, candidate.Length);
        Assert.Equal(expected, larger[..length]);
        Assert.All(larger[length..], value => Assert.Equal(0xAA, value));
    }

    [Fact(DisplayName = "Default Secret should have zero length and copy nothing")]
    public void DefaultSecretShouldHaveZeroLengthAndCopyNothing()
    {
        // Given
        Secret candidate = default;
        byte[] destination = [0xAA];

        // When
        candidate.CopyTo(destination);

        // Then
        Assert.Equal(0, candidate.Length);
        Assert.Equal(0xAA, destination[0]);
    }

    [Fact(DisplayName = "Secret.CopyTo should throw ArgumentException when the destination is too short")]
    public void SecretCopyToShouldThrowArgumentExceptionWhenTheDestinationIsTooShort()
    {
        // Given
        Secret candidate = new(CreateValue(32));

        // When / Then
        Assert.Throws<ArgumentException>(() => candidate.CopyTo(new byte[31]));
    }

    [Fact(DisplayName = "Secret construction should not modify the caller's value")]
    public void SecretConstructionShouldNotModifyTheCallersValue()
    {
        // Given
        byte[] value = CreateValue(32);
        byte[] expected = [..value];

        // When
        Secret candidate = new(value);

        // Then
        Assert.Equal(expected, value);
        Assert.Equal(expected, candidate.AsReadOnlySpan().ToArray());
    }

    [Fact(DisplayName = "Secret.ToString should produce the original text and Base16 for values longer than the stack buffer")]
    public void SecretToStringShouldProduceTheOriginalTextAndBase16ForValuesLongerThanTheStackBuffer()
    {
        // Given
        string text = new('x', 300);
        Secret candidate = text.AsSpan();
        string expectedBase16 = Convert.ToHexString(Encoding.UTF8.GetBytes(text)).ToLowerInvariant();

        // When
        string actualText = candidate.ToString(Encoding.UTF8);
        string actualBase16 = candidate.ToString();

        // Then
        Assert.Equal(text, actualText);
        Assert.Equal(expectedBase16, actualBase16);
    }

    /// <summary>
    /// Creates a deterministic value of the specified length.
    /// </summary>
    /// <param name="length">The length of the value.</param>
    /// <returns>Returns a value of the specified length, where byte i is (i * 11 + 5) mod 256.</returns>
    private static byte[] CreateValue(int length)
    {
        byte[] result = new byte[length];

        for (int index = 0; index < length; index++)
            result[index] = (byte)((index * 11 + 5) & 0xFF);

        return result;
    }
}
