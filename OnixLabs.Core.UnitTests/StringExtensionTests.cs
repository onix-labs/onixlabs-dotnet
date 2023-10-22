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
