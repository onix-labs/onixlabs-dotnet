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

public sealed class Base32DefaultAlphabetTests
{
    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = new("abcdefghijklmnopqrstuvwxyz");
        Base32 b = new("abcdefghijklmnopqrstuvwxyz");

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "new with padding should produce the expected Base-32 value")]
    [InlineData("GEZDGNBVGY3TQOJQ", "1234567890")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithPadding(string expected, string value)
    {
        // Given
        Base32 candidate = new(value);

        // When
        string actual = candidate.ToString("P", Base32FormatInfo.Default);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "new without padding should produce the expected Base-32 value")]
    [InlineData("GEZDGNBVGY3TQOJQ", "1234567890")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithoutPadding(string expected, string value)
    {
        // Given
        Base32 candidate = new(value);

        // When
        string actual = candidate.ToString(null, Base32FormatInfo.Default);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "GEZDGNBVGY3TQOJQ")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Parse(value, Base32FormatInfo.Default);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
