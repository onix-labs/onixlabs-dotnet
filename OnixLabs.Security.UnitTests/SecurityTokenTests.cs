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

namespace OnixLabs.Security.UnitTests;

public sealed class SecurityTokenTests
{
    [Fact(DisplayName = "SecurityToken should be constructable")]
    public void SecurityTokenShouldBeConstructableFromBytes()
    {
        // Given
        const string expected = "H3110,W0RLD!";

        // When
        SecurityToken candidate = new(expected);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Identical default security token values should be considered equal")]
    public void IdenticalDefaultSecurityTokenValuesShouldBeConsideredEqual()
    {
        // Given
        SecurityToken left = new();
        SecurityToken right = default;

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Identical security token values should be considered equal")]
    public void IdenticalSecurityTokenValuesShouldBeConsideredEqual()
    {
        // Given
        SecurityToken left = new("abc123");
        SecurityToken right = new("abc123");

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "Different security token values should not be considered equal")]
    public void DifferentSecurityTokenValuesShouldNotBeConsideredEqual()
    {
        // Given
        SecurityToken left = new("abc123");
        SecurityToken right = new("xyz789");

        // Then
        Assert.NotEqual(left, right);
        Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        Assert.False(left.Equals(right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "Identical security token values should produce identical hash codes")]
    public void IdenticalSecurityTokenValuesShouldProduceIdenticalSecurityTokenCodes()
    {
        // Given
        SecurityToken left = new("abc123");
        SecurityToken right = new("abc123");

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);
    }

    [Fact(DisplayName = "Different security token values should produce different hash codes")]
    public void DifferentSecurityTokenValuesShouldProduceDifferentSecurityTokenCodes()
    {
        // Given
        SecurityToken left = new("abc123");
        SecurityToken right = new("xyz789");

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);
    }

    [Theory(DisplayName = "SecurityToken.Length should produce the expected result")]
    [InlineData("a", 1)]
    [InlineData("ab", 2)]
    [InlineData("abcd", 4)]
    [InlineData("abcdefghijklmnopqrstuvwxyz", 26)]
    public void SecurityTokenLengthShouldProduceExpectedResult(string value, int expected)
    {
        // Given
        SecurityToken token = new(value);

        // When
        int actual = token.Length;

        // Then
        Assert.Equal(expected, actual);
    }
}
