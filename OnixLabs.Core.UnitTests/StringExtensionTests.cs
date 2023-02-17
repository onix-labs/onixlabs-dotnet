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

using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class StringExtensionTests
{
    [Theory(DisplayName = "SubstringBefore should return the expected result (char)")]
    [InlineData("First:Second", "First", ':')]
    [InlineData("12345+678910", "12345", '+')]
    public void SubstringBeforeShouldReturnTheExpectedResultChar(string value, string expected, char delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringBefore(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringBefore should return the expected result (string)")]
    [InlineData("First:Second", "First", ":")]
    [InlineData("12345+678910", "12345", "+")]
    public void SubstringBeforeShouldReturnTheExpectedResultString(string value, string expected, string delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringBefore(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringBeforeLast should return the expected result (char)")]
    [InlineData("First:Second:Third", "First:Second", ':')]
    [InlineData("12345+678910+12345", "12345+678910", '+')]
    public void SubstringBeforeLastShouldReturnTheExpectedResultChar(string value, string expected, char delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringBeforeLast(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringBeforeLast should return the expected result (string)")]
    [InlineData("First:Second:Third", "First:Second", ":")]
    [InlineData("12345+678910+12345", "12345+678910", "+")]
    public void SubstringBeforeLastShouldReturnTheExpectedResultString(string value, string expected, string delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringBeforeLast(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringAfter should return the expected result (char)")]
    [InlineData("First:Second", "Second", ':')]
    [InlineData("12345+678910", "678910", '+')]
    public void SubstringAfterShouldReturnTheExpectedResultChar(string value, string expected, char delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringAfter(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringAfter should return the expected result (string)")]
    [InlineData("First:Second", "Second", ":")]
    [InlineData("12345+678910", "678910", "+")]
    public void SubstringAfterShouldReturnTheExpectedResultString(string value, string expected, string delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringAfter(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringAfterLast should return the expected result (char)")]
    [InlineData("First:Second:Third", "Third", ':')]
    [InlineData("12345+678910+12345", "12345", '+')]
    public void SubstringAfterLastShouldReturnTheExpectedResultChar(string value, string expected, char delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringAfterLast(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SubstringAfterLast should return the expected result (string)")]
    [InlineData("First:Second:Third", "Third", ":")]
    [InlineData("12345+678910+12345", "12345", "+")]
    public void SubstringAfterLastShouldReturnTheExpectedResultString(string value, string expected, string delimiter)
    {
        // Arrange / Act
        string actual = value.SubstringAfterLast(delimiter);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ToByteArray should return the expected result")]
    [InlineData("Hello, World!", new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x21})]
    public void ToByteArrayShouldReturnTheExpectedResult(string value, byte[] expected)
    {
        // Arrange / Act
        byte[] actual = value.ToByteArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Wrap should return the expected result")]
    [InlineData("<", "value", ">", "<value>")]
    [InlineData("BEFORE:", "value", ":AFTER", "BEFORE:value:AFTER")]
    public void WrapShouldReturnTheExpectedResult(string before, string value, string after, string expected)
    {
        // Arrange / Act
        string actual = value.Wrap(before, after);

        // Assert
        Assert.Equal(expected, actual);
    }
}
