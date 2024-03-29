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

public sealed class Base32CrockfordAlphabetTests
{
    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = Base32.Create("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Crockford);
        Base32 b = Base32.Create("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Crockford);

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base32.Create with padding should produce the expected Base-32 value")]
    [InlineData("64S36D1N6RVKGE9G", "1234567890")]
    [InlineData("85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8======", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value, Base32Alphabet.Crockford, true);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Create without padding should produce the expected Base-32 value")]
    [InlineData("64S36D1N6RVKGE9G", "1234567890")]
    [InlineData("85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithoutPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value, Base32Alphabet.Crockford, false);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "64S36D1N6RVKGE9G")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Parse(value, Base32Alphabet.Crockford);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
