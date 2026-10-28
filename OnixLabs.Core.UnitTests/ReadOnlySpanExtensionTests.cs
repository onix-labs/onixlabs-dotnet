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

namespace OnixLabs.Core.UnitTests;

public sealed class ReadOnlySpanExtensionTests
{
    [Fact(DisplayName = "ReadOnlySpan<byte>.GetContentHashCode should produce equal hash codes for equal content")]
    public void ByteSpanGetContentHashCodeShouldProduceEqualHashCodesForEqualContent()
    {
        // Given
        byte[] left = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        byte[] right = [1, 2, 3, 4, 5, 6, 7, 8, 9];

        // When
        int leftHashCode = new ReadOnlySpan<byte>(left).GetContentHashCode();
        int rightHashCode = new ReadOnlySpan<byte>(right).GetContentHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte>.GetContentHashCode should produce different hash codes for different content")]
    public void ByteSpanGetContentHashCodeShouldProduceDifferentHashCodesForDifferentContent()
    {
        // Given
        byte[] left = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        byte[] right = [9, 8, 7, 6, 5, 4, 3, 2, 1];

        // When
        int leftHashCode = new ReadOnlySpan<byte>(left).GetContentHashCode();
        int rightHashCode = new ReadOnlySpan<byte>(right).GetContentHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "ReadOnlySpan<byte>.GetContentHashCode should depend on every byte, not only a prefix")]
    public void ByteSpanGetContentHashCodeShouldDependOnEveryByte()
    {
        // Given
        byte[] left = new byte[37];
        byte[] right = new byte[37];
        right[^1] = 1;

        // When
        int leftHashCode = new ReadOnlySpan<byte>(left).GetContentHashCode();
        int rightHashCode = new ReadOnlySpan<byte>(right).GetContentHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "ReadOnlySpan<char>.GetContentHashCode should produce equal hash codes for ordinally equal content")]
    public void CharSpanGetContentHashCodeShouldProduceEqualHashCodesForOrdinallyEqualContent()
    {
        // Given
        string left = "The quick brown fox";
        string right = new(['T', 'h', 'e', ' ', 'q', 'u', 'i', 'c', 'k', ' ', 'b', 'r', 'o', 'w', 'n', ' ', 'f', 'o', 'x']);

        // When
        int leftHashCode = left.AsSpan().GetContentHashCode();
        int rightHashCode = right.AsSpan().GetContentHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "ReadOnlySpan<char>.GetContentHashCode should be case-sensitive")]
    public void CharSpanGetContentHashCodeShouldBeCaseSensitive()
    {
        // Given
        string left = "The quick brown fox";
        string right = "THE QUICK BROWN FOX";

        // When
        int leftHashCode = left.AsSpan().GetContentHashCode();
        int rightHashCode = right.AsSpan().GetContentHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }
}
