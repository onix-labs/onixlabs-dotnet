// Copyright 2020-2022 ONIXLabs
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

using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class HmacTests
{
    [Fact(DisplayName = "Identical Hmac values produce identical hash codes.")]
    public void IdenticalHmacValuesProduceIdenticalHashCodes()
    {
        // Arrange
        Hmac a = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");

        // Act
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Assert
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Fact(DisplayName = "Identical HMACs should be considered equal")]
    public void IdenticalHashesShouldBeConsideredEqual()
    {
        // Arrange
        Hmac a = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");

        // Assert
        Assert.Equal(a, b);
    }

    [Fact(DisplayName = "Different HMACs should not be considered equal (different data)")]
    public void DifferentHashesShouldNotBeConsideredEqualWithDifferentData()
    {
        // Arrange
        Hmac a = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = Hmac.ComputeSha2Hmac256("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "key");

        // Assert
        Assert.NotEqual(a, b);
    }

    [Fact(DisplayName = "Different HMACs should not be considered equal (different keys)")]
    public void DifferentHashesShouldNotBeConsideredEqualWithDifferentKeys()
    {
        // Arrange
        Hmac a = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = Hmac.ComputeSha2Hmac256("abcdefghijklmnopqrstuvwxyz", "123");

        // Assert
        Assert.NotEqual(a, b);
    }

    [Fact(DisplayName = "Parse should be able to parse a known hash")]
    public void ParseShouldBeAbleToParseAKnownHash()
    {
        // Arrange
        const string expected =
            "Sha2Hmac256:73ac6fa8599f4bde8dfee594c7f5f6ff03023b2d99ca71a7eccf729a8fc5c324:48656c6c6f2c20576f726c6421";

        // Act
        Hmac hash = Hmac.Parse(expected);
        string actual = hash.ToStringWithAlgorithmType();

        // Assert
        Assert.Equal(expected, actual);
    }
}
