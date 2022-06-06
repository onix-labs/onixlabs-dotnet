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

using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class Base32ZBase32AlphabetTests
{
    [Fact(DisplayName = "Identical Base32 values produce identical hash codes.")]
    public void IdenticalBase32ValuesProduceIdenticalHashCodes()
    {
        // Arrange
        Base32 a = Base32.FromString("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.ZBase32);
        Base32 b = Base32.FromString("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.ZBase32);

        // Act
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Assert
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base32_FromString without padding should produce the expected Base-32 value.")]
    [InlineData("gr3dgpbiga5uoqjo", "1234567890")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe======", "abcdefghijklmnopqrstuvwxyz")]
    public void Base32FromStringWithPaddingShouldProduceTheExpectedBase32Value(string expected, string value)
    {
        // Arrange
        Base32 candidate = Base32.FromString(value, Base32Alphabet.ZBase32, true);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32_FromString without padding should produce the expected Base-32 value.")]
    [InlineData("gr3dgpbiga5uoqjo", "1234567890")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe", "abcdefghijklmnopqrstuvwxyz")]
    public void Base32FromStringWithoutPaddingShouldProduceTheExpectedBase32Value(string expected, string value)
    {
        // Arrange
        Base32 candidate = Base32.FromString(value, Base32Alphabet.ZBase32, false);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32_Parse should produce the expected plain text value.")]
    [InlineData("1234567890", "gr3dgpbiga5uoqjo")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe")]
    public void Base32ParseShouldProduceTheExpectedPlainTextValue(string expected, string value)
    {
        // Arrange
        Base32 candidate = Base32.Parse(value, Base32Alphabet.ZBase32);

        // Act
        string actual = candidate.ToPlainTextString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
