// Copyright 2020 ONIXLabs
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

using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class StringBuilderExtensionTests
{
    [Fact(DisplayName = "StringBuilder.Append should produce the expected result")]
    public void StringBuilderAppendShouldProduceTheExpectedResult()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.Append("XYZ", 123, false);

        // Then
        Assert.Equal("ABCXYZ123False", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Prepend of a single item should produce the expected result")]
    public void StringBuilderPrependOfSingleItemShouldProduceTheExpectedResult()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.Prepend("XYZ");

        // Then
        Assert.Equal("XYZABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Prepend should produce the expected result")]
    public void StringBuilderPrependShouldProduceTheExpectedResult()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.Prepend("XYZ", 123, false);

        // Then
        Assert.Equal("XYZ123FalseABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.PrependJoin should produce the expected result")]
    public void StringBuilderPrependJoinShouldProduceTheExpectedResult()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.PrependJoin(", ", "XYZ", 123, false);

        // Then
        Assert.Equal("XYZ, 123, FalseABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Trim should produce the expected result (char)")]
    public void StringBuilderTrimShouldProduceTheExpectedResultChar()
    {
        // Given
        StringBuilder builder = new("###ABC###");

        // When
        builder.Trim('#');

        // Then
        Assert.Equal("ABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.TrimEnd should produce the expected result (char)")]
    public void StringBuilderTrimEndShouldProduceTheExpectedResultChar()
    {
        // Given
        StringBuilder builder = new("###ABC###");

        // When
        builder.TrimEnd('#');

        // Then
        Assert.Equal("###ABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.TrimStart should produce the expected result (char)")]
    public void StringBuilderTrimStartShouldProduceTheExpectedResultChar()
    {
        // Given
        StringBuilder builder = new("###ABC###");

        // When
        builder.TrimStart('#');

        // Then
        Assert.Equal("ABC###", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Trim should produce the expected result (string)")]
    public void StringBuilderTrimShouldProduceTheExpectedResultString()
    {
        // Given
        StringBuilder builder = new("#-#ABC#-#");

        // When
        builder.Trim("#-#");

        // Then
        Assert.Equal("ABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.TrimEnd should produce the expected result (string)")]
    public void StringBuilderTrimEndShouldProduceTheExpectedResultString()
    {
        // Given
        StringBuilder builder = new("#-#ABC#-#");

        // When
        builder.TrimEnd("#-#");

        // Then
        Assert.Equal("#-#ABC", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.TrimStart should produce the expected result (string)")]
    public void StringBuilderTrimStartShouldProduceTheExpectedResultString()
    {
        // Given
        StringBuilder builder = new("#-#ABC#-#");

        // When
        builder.TrimStart("#-#");

        // Then
        Assert.Equal("ABC#-#", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Wrap should produce the expected result (char)")]
    public void StringBuilderWrapShouldProduceTheExpectedResultChar()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.Wrap('(', ')');

        // Then
        Assert.Equal("(ABC)", builder.ToString());
    }

    [Fact(DisplayName = "StringBuilder.Wrap should produce the expected result (string)")]
    public void StringBuilderWrapShouldProduceTheExpectedResultString()
    {
        // Given
        StringBuilder builder = new("ABC");

        // When
        builder.Wrap("123", "XYZ");

        // Then
        Assert.Equal("123ABCXYZ", builder.ToString());
    }

    [Theory(DisplayName = "StringBuilder.TrimEnd and TrimStart should remove every repetition of a string value, and only whole repetitions")]
    [InlineData("ababab", "ab", "", "")]
    [InlineData("aababx", "ab", "aababx", "aababx")]
    [InlineData("abxab", "ab", "abx", "xab")]
    [InlineData("aaab", "ab", "aa", "aaab")]
    [InlineData("baaa", "aa", "ba", "baaa")]
    [InlineData("x", "xyz", "x", "x")]
    public void StringBuilderTrimShouldRemoveEveryWholeRepetitionOfAStringValue(string value, string trim, string expectedEnd, string expectedStart)
    {
        // When
        string actualEnd = new StringBuilder(value).TrimEnd(trim).ToString();
        string actualStart = new StringBuilder(value).TrimStart(trim).ToString();

        // Then
        Assert.Equal(expectedEnd, actualEnd);
        Assert.Equal(expectedStart, actualStart);
    }

    [Theory(DisplayName = "StringBuilder.TrimEnd and TrimStart should remove every repetition of a char value")]
    [InlineData("aaaa", 'a', "", "")]
    [InlineData("aabaa", 'a', "aab", "baa")]
    [InlineData("", 'a', "", "")]
    [InlineData("b", 'a', "b", "b")]
    public void StringBuilderTrimShouldRemoveEveryRepetitionOfACharValue(string value, char trim, string expectedEnd, string expectedStart)
    {
        // When
        string actualEnd = new StringBuilder(value).TrimEnd(trim).ToString();
        string actualStart = new StringBuilder(value).TrimStart(trim).ToString();

        // Then
        Assert.Equal(expectedEnd, actualEnd);
        Assert.Equal(expectedStart, actualStart);
    }
}
