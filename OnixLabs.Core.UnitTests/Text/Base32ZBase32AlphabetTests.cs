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

public sealed class Base32ZBase32AlphabetTests
{
    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = Base32.Create("abcdefghijklmnopqrstuvwxyz");
        Base32 b = Base32.Create("abcdefghijklmnopqrstuvwxyz");

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base32.Create with padding should produce the expected Base-32 value")]
    [InlineData("gr3dgpbiga5uoqjo", "1234567890")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe======", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value);

        // When
        string actual = candidate.ToString("P", Base32Alphabet.ZBase32);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Create without padding should produce the expected Base-32 value")]
    [InlineData("gr3dgpbiga5uoqjo", "1234567890")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResultWithoutPadding(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Create(value);

        // When
        string actual = candidate.ToString(null, Base32Alphabet.ZBase32);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "gr3dgpbiga5uoqjo")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base32 candidate = Base32.Parse(value, Base32Alphabet.ZBase32);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
