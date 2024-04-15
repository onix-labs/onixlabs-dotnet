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

public sealed class Base58FlickrAlphabetTests
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
    [InlineData("3LiR7aNtchXnQC", "1234567890")]
    [InlineData("2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = new(value);

        // When
        string actual = candidate.ToString(null, Base58FormatInfo.Flickr);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "3LiR7aNtchXnQC")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base58 candidate = Base58.Parse(value, Base58FormatInfo.Flickr);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
