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
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class ExtensionsTests
{
    [Fact(DisplayName = "Byte array ToHash should produce expected Hash instance")]
    public void ByteArrayToHashShouldProduceExpectedHash()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Hash expected = new(value);

        // When
        Hash actual = value.ToHash();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte> ToHash should produce expected Hash instance")]
    public void ReadOnlySpanToHashShouldProduceExpectedHash()
    {
        // Given
        byte[] valueArray = [1, 2, 3, 4];
        ReadOnlySpan<byte> value = valueArray;
        Hash expected = new(valueArray);

        // When
        Hash actual = value.ToHash();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Byte array ToSecret should produce expected Secret instance")]
    public void ByteArrayToSecretShouldProduceExpectedSecret()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Secret expected = new(value);

        // When
        Secret actual = value.ToSecret();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte> ToSecret should produce expected Secret instance")]
    public void ReadOnlySpanToSecretShouldProduceExpectedSecret()
    {
        // Given
        byte[] valueArray = [1, 2, 3, 4];
        ReadOnlySpan<byte> value = valueArray;
        Secret expected = new(valueArray);

        // When
        Secret actual = value.ToSecret();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Byte array ToHash should handle empty array")]
    public void ByteArrayToHashShouldHandleEmptyArray()
    {
        // Given
        byte[] value = [];
        Hash expected = new(value);

        // When
        Hash actual = value.ToHash();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte> ToHash should handle empty span")]
    public void ReadOnlySpanToHashShouldHandleEmptySpan()
    {
        // Given
        byte[] valueArray = [];
        ReadOnlySpan<byte> value = valueArray;
        Hash expected = new(valueArray);

        // When
        Hash actual = value.ToHash();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Byte array ToSecret should handle empty array")]
    public void ByteArrayToSecretShouldHandleEmptyArray()
    {
        // Given
        byte[] value = [];
        Secret expected = new(value);

        // When
        Secret actual = value.ToSecret();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte> ToSecret should handle empty span")]
    public void ReadOnlySpanToSecretShouldHandleEmptySpan()
    {
        // Given
        byte[] valueArray = [];
        ReadOnlySpan<byte> value = valueArray;
        Secret expected = new(valueArray);

        // When
        Secret actual = value.ToSecret();

        // Then
        Assert.Equal(expected, actual);
    }
}
