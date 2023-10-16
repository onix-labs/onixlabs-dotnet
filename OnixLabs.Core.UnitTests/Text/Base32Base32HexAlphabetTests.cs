// Copyright 2020-2023 ONIXLabs
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

public sealed class Base32Base32HexAlphabetTests
{
    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = Base32.Create("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Base32Hex);
        Base32 b = Base32.Create("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Base32Hex);

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base32.Create with padding should produce the expected Base-32 value")]
    [InlineData("64P36D1L6ORJGE9G", "1234567890")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value, Base32Alphabet.Base32Hex, true);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Create without padding should produce the expected Base-32 value")]
    [InlineData("64P36D1L6ORJGE9G", "1234567890")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithoutPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value, Base32Alphabet.Base32Hex, false);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "64P36D1L6ORJGE9G")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Parse(value, Base32Alphabet.Base32Hex);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
