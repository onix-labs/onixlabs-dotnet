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

using System;
using System.Globalization;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class StringExtensionTests
{
    [Theory(DisplayName = "String.Repeat should return the expected result")]
    [InlineData("0", 10, "0000000000")]
    [InlineData("Abc1", 3, "Abc1Abc1Abc1")]
    public void RepeatShouldProduceExpectedResult(string value, int count, string expected)
    {
        // When
        string actual = value.Repeat(count);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringBeforeFirst should return the string before the first delimiter")]
    [InlineData("First:Second", "First", ':')]
    [InlineData("12345+678910", "12345", '+')]
    public void SubstringBeforeFirstShouldProduceExpectedResultChar(string value, string expected, char delimiter)
    {
        // When
        string actual = value.SubstringBeforeFirst(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringBeforeFirst should return the string before the first delimiter")]
    [InlineData("First:Second", "First", ":")]
    [InlineData("12345+678910", "12345", "+")]
    public void SubstringBeforeFirstShouldProduceExpectedResultString(string value, string expected, string delimiter)
    {
        // When
        string actual = value.SubstringBeforeFirst(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringBeforeLast should return the string before the last char delimiter")]
    [InlineData("First:Second:Third", "First:Second", ':')]
    [InlineData("12345+678910+12345", "12345+678910", '+')]
    public void SubstringBeforeLastShouldProduceExpectedResultChar(string value, string expected, char delimiter)
    {
        // When
        string actual = value.SubstringBeforeLast(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringBeforeLast should return the string before the last string delimiter")]
    [InlineData("First:Second:Third", "First:Second", ":")]
    [InlineData("12345+678910+12345", "12345+678910", "+")]
    public void SubstringBeforeLastShouldProduceExpectedResultString(string value, string expected, string delimiter)
    {
        // When
        string actual = value.SubstringBeforeLast(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringAfterFirst should return the string after the first char delimiter")]
    [InlineData("First:Second", "Second", ':')]
    [InlineData("12345+678910", "678910", '+')]
    public void SubstringAfterFirstShouldProduceExpectedResultChar(string value, string expected, char delimiter)
    {
        // When
        string actual = value.SubstringAfterFirst(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringAfterFirst should return the string after the first string delimiter")]
    [InlineData("First:Second", "Second", ":")]
    [InlineData("12345+678910", "678910", "+")]
    public void SubstringAfterFirstShouldProduceExpectedResultString(string value, string expected, string delimiter)
    {
        // When
        string actual = value.SubstringAfterFirst(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringAfterLast should return the string after the last char delimiter")]
    [InlineData("First:Second:Third", "Third", ':')]
    [InlineData("12345+678910+12345", "12345", '+')]
    public void SubstringAfterLastShouldProduceExpectedResultChar(string value, string expected, char delimiter)
    {
        // When
        string actual = value.SubstringAfterLast(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.SubstringAfterLast should return the the string after the last string delimiter")]
    [InlineData("First:Second:Third", "Third", ":")]
    [InlineData("12345+678910+12345", "12345", "+")]
    public void SubstringAfterLastShouldProduceExpectedResultString(string value, string expected, string delimiter)
    {
        // When
        string actual = value.SubstringAfterLast(delimiter);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.ToByteArray should produce the byte array equivalent of the current string")]
    [InlineData("Hello, World!", new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x21 })]
    public void ToByteArrayShouldProduceExpectedResult(string value, byte[] expected)
    {
        // When
        byte[] actual = value.ToByteArray();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "String.ToDateTime should return the DateTime equivalent of the current string")]
    [InlineData("0001-01-01T00:00:00Z", 1, 1, 1, 0, 0, 0)]
    [InlineData("1970-01-01T00:00:00Z", 1970, 1, 1, 0, 0, 0)]
    [InlineData("9999-12-31T23:59:59Z", 9999, 12, 31, 23, 59, 59)]
    public void ToDateTimeShouldProduceExpectedResult(string value, int year, int month, int day, int hour, int minute, int second)
    {
        // When
        // AdjustToUniversal is essential, otherwise the tests may pass locally but fail on a build server in another time-zone.
        DateTime actual = value.ToDateTime(styles: DateTimeStyles.AdjustToUniversal);

        // Then
        Assert.Equal(year, actual.Year);
        Assert.Equal(month, actual.Month);
        Assert.Equal(day, actual.Day);
        Assert.Equal(hour, actual.Hour);
        Assert.Equal(minute, actual.Minute);
        Assert.Equal(second, actual.Second);
    }

    [Theory(DisplayName = "String.ToDateOnly should return the DateOnly equivalent of the current string")]
    [InlineData("0001-01-01", 1, 1, 1)]
    [InlineData("1970-01-01", 1970, 1, 1)]
    [InlineData("9999-12-31", 9999, 12, 31)]
    public void ToDateOnlyShouldProduceExpectedResult(string value, int year, int month, int day)
    {
        // When
        DateOnly actual = value.ToDateOnly();

        // Then
        Assert.Equal(year, actual.Year);
        Assert.Equal(month, actual.Month);
        Assert.Equal(day, actual.Day);
    }

    [Theory(DisplayName = "String.ToTimeOnly should return the TimeOnly equivalent of the current string")]
    [InlineData("00:00:00", 0, 0, 0)]
    [InlineData("01:01:01", 1, 1, 1)]
    [InlineData("23:59:59", 23, 59, 59)]
    public void ToTimeOnlyShouldProduceExpectedResult(string value, int hour, int minute, int second)
    {
        // When
        TimeOnly actual = value.ToTimeOnly();

        // Then
        Assert.Equal(hour, actual.Hour);
        Assert.Equal(minute, actual.Minute);
        Assert.Equal(second, actual.Second);
    }

    [Theory(DisplayName = "String.ToEscapedString should produce the expected result")]
    [InlineData("\n", @"\n")]
    [InlineData("\r", @"\r")]
    [InlineData("\t", @"\t")]
    [InlineData("\"", @"\""")]
    [InlineData("\'", @"\'")]
    [InlineData("\\", @"\\")]
    [InlineData("\u0000", @"\u0000")]
    [InlineData("\u0001", @"\u0001")]
    [InlineData("\u0002", @"\u0002")]
    [InlineData("\u0003", @"\u0003")]
    [InlineData("\u0004", @"\u0004")]
    [InlineData("\u0005", @"\u0005")]
    [InlineData("\u0006", @"\u0006")]
    [InlineData("\u0007", @"\u0007")]
    [InlineData("\u0008", @"\u0008")]
    [InlineData("\u000B", @"\u000B")]
    [InlineData("\u000C", @"\u000C")]
    [InlineData("\u000E", @"\u000E")]
    [InlineData("\u000F", @"\u000F")]
    [InlineData("\u0010", @"\u0010")]
    [InlineData("\u0011", @"\u0011")]
    [InlineData("\u0012", @"\u0012")]
    [InlineData("\u0013", @"\u0013")]
    [InlineData("\u0014", @"\u0014")]
    [InlineData("\u0015", @"\u0015")]
    [InlineData("\u0016", @"\u0016")]
    [InlineData("\u0017", @"\u0017")]
    [InlineData("\u0018", @"\u0018")]
    [InlineData("\u0019", @"\u0019")]
    [InlineData("\u001A", @"\u001A")]
    [InlineData("\u001B", @"\u001B")]
    [InlineData("\u001C", @"\u001C")]
    [InlineData("\u001D", @"\u001D")]
    [InlineData("\u001E", @"\u001E")]
    [InlineData("\u001F", @"\u001F")]
    [InlineData("\u007F", @"\u007F")]
    [InlineData("\u0085", @"\u0085")]
    [InlineData("\u0098", @"\u0098")]
    [InlineData("\u009C", @"\u009C")]
    [InlineData("\u009D", @"\u009D")]
    [InlineData("\u009E", @"\u009E")]
    [InlineData("\u009F", @"\u009F")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")]
    public void ToEscapedStringShouldProduceExpectedResult(string value, string expected)
    {
        // When
        string actual = value.ToEscapedString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "String.TryCopyTo should return true if the string is successfully copied to the target span")]
    public void StringTryCopyToShouldReturnTrueIfStringIsSuccessfullyCopiedToTargetSpan()
    {
        // Given
        const string expected = "Hello, World!";
        int expectedCharsWritten = 13;
        Span<char> destination = stackalloc char[expectedCharsWritten];

        // When
        bool result = expected.TryCopyTo(destination, out expectedCharsWritten);
        string actual = destination.ToString();

        // Then
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "String.TryCopyTo should return false if the string not is successfully copied to the target span")]
    public void StringTryCopyToShouldReturnFalseIfStringIsNotSuccessfullyCopiedToTargetSpan()
    {
        // Given
        const string expected = "Hello, World!";
        int expectedCharsWritten = 0;
        Span<char> destination = stackalloc char[expectedCharsWritten];

        // When
        bool result = expected.TryCopyTo(destination, out expectedCharsWritten);

        // Then
        Assert.False(result);
    }

    [Theory(DisplayName = "String.Wrap should wrap the current string value between the before and after string values")]
    [InlineData("<", "value", ">", "<value>")]
    [InlineData("BEFORE:", "value", ":AFTER", "BEFORE:value:AFTER")]
    public void WrapShouldProduceExpectedResult(string before, string value, string after, string expected)
    {
        // When
        string actual = value.Wrap(before, after);

        // Then
        Assert.Equal(expected, actual);
    }
}
