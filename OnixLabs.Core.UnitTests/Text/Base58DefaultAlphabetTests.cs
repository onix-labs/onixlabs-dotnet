// Copyright © 2020 ONIXLabs
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
    [Fact(DisplayName = "Base58 values should be identical")]
    public void Base58ValuesShouldBeIdentical()
    {
        // Given
        Base58 a = Base58.Create("abcdefghijklmnopqrstuvwxyz");
        Base58 b = Base58.Create("abcdefghijklmnopqrstuvwxyz");

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base58.Create should produce the expected Base-58 value")]
    [InlineData("3mJr7AoUCHxNqd", "1234567890")]
    [InlineData("2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = Base58.Create(value);

        // When
        string actual = candidate.ToString(null, Base58Alphabet.Default);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "3mJr7AoUCHxNqd")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2zuFXTJSTRK6ESktqhM2QDBkCnH1U46CnxaD")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3yxU3u1igY8WkgtjK92fbJQCd4BZiiT1v25f")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = Base58.Parse(value, Base58Alphabet.Default);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
