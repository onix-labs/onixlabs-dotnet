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
using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class ExtensionTests
{
    [Theory(DisplayName = "Byte[].ToBase16 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void ByteArrayToBase16ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] source = value.ToByteArray();

        // When
        Base16 result = source.ToBase16();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ReadOnlySpan<Byte>.ToBase16 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789", "30313233343536373839")]
    public void ReadOnlySpanOfByteToBase16ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        ReadOnlySpan<byte> source = value.ToByteArray().AsSpan();

        // When
        Base16 result = source.ToBase16();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Byte[].ToBase32 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void ByteArrayToBase32ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] source = value.ToByteArray();

        // When
        Base32 result = source.ToBase32();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ReadOnlySpan<Byte>.ToBase32 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void ReadOnlySpanOfByteToBase32ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        ReadOnlySpan<byte> source = value.ToByteArray().AsSpan();

        // When
        Base32 result = source.ToBase32();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Byte[].ToBase58 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void ByteArrayToBase58ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] source = value.ToByteArray();

        // When
        Base58 result = source.ToBase58();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ReadOnlySpan<Byte>.ToBase58 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    [InlineData("0123456789", "3i37NcgooY8f1S")]
    public void ReadOnlySpanOfByteToBase58ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        ReadOnlySpan<byte> source = value.ToByteArray().AsSpan();

        // When
        Base58 result = source.ToBase58();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Byte[].ToBase64 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void ByteArrayToBase64ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] source = value.ToByteArray();

        // When
        Base64 result = source.ToBase64();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ReadOnlySpan<Byte>.ToBase64 should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    [InlineData("0123456789", "MDEyMzQ1Njc4OQ==")]
    public void ReadOnlySpanOfByteToBase64ShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        ReadOnlySpan<byte> source = value.ToByteArray().AsSpan();

        // When
        Base64 result = source.ToBase64();
        string actual = result.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
