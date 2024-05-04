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

using System.Text;
using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base64Tests
{
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
}
