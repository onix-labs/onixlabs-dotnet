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

public sealed class Base16Tests
{
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
        byte[] bytes = candidate.ToByteArray();
        bytes[0] = 0;
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
    [InlineData("4142434445464748494a4b4c4d4e4f505152535455565758595a","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696a6b6c6d6e6f707172737475767778797a","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839","0123456789")]
    public void Base16ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base16 candidate = Base16.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789","30313233343536373839")]
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
}
