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

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base58DefaultAlphabetTests
{
    [Fact(DisplayName = "Identical Base58 values produce identical hash codes.")]
    public void IdenticalBase58ValuesProduceIdenticalHashCodes()
    {
        // Arrange
        Base58 a = Base58.FromString("abcdefghijklmnopqrstuvwxyz", Base58Alphabet.Default);
        Base58 b = Base58.FromString("abcdefghijklmnopqrstuvwxyz", Base58Alphabet.Default);

        // Act
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Assert
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base58_FromString should produce the expected Base-58 value.")]
    [InlineData("3mJr7AoUCHxNqd", "1234567890")]
    [InlineData("2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f", "abcdefghijklmnopqrstuvwxyz")]
    public void Base58FromStringShouldProduceTheExpectedBase58Value(string expected, string value)
    {
        // Arrange
        Base58 candidate = Base58.FromString(value, Base58Alphabet.Default);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58_Parse should produce the expected plain text value.")]
    [InlineData("1234567890", "3mJr7AoUCHxNqd")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    public void Base58ParseShouldProduceTheExpectedPlainTextValue(string expected, string value)
    {
        // Arrange
        Base58 candidate = Base58.Parse(value, Base58Alphabet.Default);

        // Act
        string actual = candidate.ToPlainTextString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
