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

public sealed class Base58RippleAlphabetTests
{
    [Fact(DisplayName = "Base58 values should be identical")]
    public void Base58ValuesShouldBeIdentical()
    {
        // Given
        Base58 a = new("abcdefghijklmnopqrstuvwxyz");
        Base58 b = new("abcdefghijklmnopqrstuvwxyz");

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "new should produce the expected Base-58 value")]
    [InlineData("smJifwo7UHx4qd", "1234567890")]
    [InlineData("pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = new(value);

        // When
        string actual = candidate.ToString(null, Base58FormatInfo.Ripple);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "smJifwo7UHx4qd")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "pzuEXTJSTRKaNSktq6MpQDBkU8Hr7haU8x2D")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "syx7sur5gY3WkgtjK9pCbJQUdhBZ55TrvpnC")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = Base58.Parse(value, Base58FormatInfo.Ripple);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
