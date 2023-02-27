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

using System;
using System.Globalization;
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
    [InlineData("Hello, World!", new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x57, 0x6f, 0x72, 0x6c, 0x64, 0x21 })]
    public void ToByteArrayShouldReturnTheExpectedResult(string value, byte[] expected)
    {
        // Arrange / Act
        byte[] actual = value.ToByteArray();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "ToDateTime should return the expected result")]
    [InlineData("1950-06-11T00:28:47Z", 1950, 6, 11, 0, 28, 47)]
    [InlineData("1966-10-22T05:52:09Z", 1966, 10, 22, 5, 52, 9)]
    [InlineData("1938-02-02T17:32:40Z", 1938, 2, 2, 17, 32, 40)]
    [InlineData("2009-01-31T20:58:12Z", 2009, 1, 31, 20, 58, 12)]
    [InlineData("1931-01-17T22:48:37Z", 1931, 1, 17, 22, 48, 37)]
    [InlineData("1955-12-02T15:39:22Z", 1955, 12, 2, 15, 39, 22)]
    [InlineData("2009-08-31T08:02:08Z", 2009, 8, 31, 8, 2, 8)]
    [InlineData("1921-05-25T13:54:55Z", 1921, 5, 25, 13, 54, 55)]
    [InlineData("2019-06-20T22:43:19Z", 2019, 6, 20, 22, 43, 19)]
    [InlineData("1904-12-23T19:32:54Z", 1904, 12, 23, 19, 32, 54)]
    [InlineData("1918-03-31T01:39:50Z", 1918, 3, 31, 1, 39, 50)]
    [InlineData("1948-02-08T13:43:22Z", 1948, 2, 8, 13, 43, 22)]
    [InlineData("1950-01-02T02:56:04Z", 1950, 1, 2, 2, 56, 4)]
    [InlineData("1945-12-02T19:52:50Z", 1945, 12, 2, 19, 52, 50)]
    [InlineData("2015-05-26T09:37:38Z", 2015, 5, 26, 9, 37, 38)]
    [InlineData("2013-07-04T12:40:57Z", 2013, 7, 4, 12, 40, 57)]
    [InlineData("1906-03-12T14:16:14Z", 1906, 3, 12, 14, 16, 14)]
    [InlineData("1982-05-21T17:09:26Z", 1982, 5, 21, 17, 9, 26)]
    [InlineData("1984-09-17T12:51:42Z", 1984, 9, 17, 12, 51, 42)]
    [InlineData("1906-08-07T17:18:09Z", 1906, 8, 7, 17, 18, 9)]
    [InlineData("1964-08-25T04:47:37Z", 1964, 8, 25, 4, 47, 37)]
    [InlineData("1995-10-07T06:36:27Z", 1995, 10, 7, 6, 36, 27)]
    [InlineData("1907-01-21T05:04:11Z", 1907, 1, 21, 5, 4, 11)]
    [InlineData("1912-03-12T08:40:37Z", 1912, 3, 12, 8, 40, 37)]
    [InlineData("1944-09-19T03:25:00Z", 1944, 9, 19, 3, 25, 0)]
    [InlineData("1906-07-06T10:14:24Z", 1906, 7, 6, 10, 14, 24)]
    [InlineData("1918-11-29T17:08:38Z", 1918, 11, 29, 17, 8, 38)]
    [InlineData("2018-07-23T02:26:20Z", 2018, 7, 23, 2, 26, 20)]
    [InlineData("1925-11-21T12:48:22Z", 1925, 11, 21, 12, 48, 22)]
    [InlineData("2005-07-18T22:29:14Z", 2005, 7, 18, 22, 29, 14)]
    [InlineData("1946-10-07T23:54:01Z", 1946, 10, 7, 23, 54, 1)]
    [InlineData("1993-02-14T15:44:15Z", 1993, 2, 14, 15, 44, 15)]
    [InlineData("1933-10-04T03:34:57Z", 1933, 10, 4, 3, 34, 57)]
    [InlineData("2017-07-31T06:21:39Z", 2017, 7, 31, 6, 21, 39)]
    [InlineData("1928-04-17T21:58:54Z", 1928, 4, 17, 21, 58, 54)]
    [InlineData("1916-11-27T02:59:35Z", 1916, 11, 27, 2, 59, 35)]
    [InlineData("1907-02-18T23:08:26Z", 1907, 2, 18, 23, 8, 26)]
    [InlineData("1929-04-06T12:12:43Z", 1929, 4, 6, 12, 12, 43)]
    [InlineData("1918-06-29T09:26:51Z", 1918, 6, 29, 9, 26, 51)]
    [InlineData("1918-02-19T11:45:00Z", 1918, 2, 19, 11, 45, 0)]
    [InlineData("1982-04-23T06:41:53Z", 1982, 4, 23, 6, 41, 53)]
    [InlineData("2014-07-08T21:31:43Z", 2014, 7, 8, 21, 31, 43)]
    [InlineData("1973-05-25T04:36:31Z", 1973, 5, 25, 4, 36, 31)]
    [InlineData("1963-09-04T02:22:24Z", 1963, 9, 4, 2, 22, 24)]
    [InlineData("1950-01-16T02:17:48Z", 1950, 1, 16, 2, 17, 48)]
    [InlineData("1934-12-09T10:49:35Z", 1934, 12, 9, 10, 49, 35)]
    [InlineData("1981-06-26T06:39:59Z", 1981, 6, 26, 6, 39, 59)]
    [InlineData("1975-10-01T04:44:19Z", 1975, 10, 1, 4, 44, 19)]
    [InlineData("1935-02-03T14:45:13Z", 1935, 2, 3, 14, 45, 13)]
    [InlineData("1967-12-02T11:54:24Z", 1967, 12, 2, 11, 54, 24)]
    [InlineData("1991-11-06T11:17:18Z", 1991, 11, 6, 11, 17, 18)]
    [InlineData("2019-10-17T04:03:05Z", 2019, 10, 17, 4, 3, 5)]
    [InlineData("1933-12-16T20:42:55Z", 1933, 12, 16, 20, 42, 55)]
    [InlineData("1953-06-17T06:18:09Z", 1953, 6, 17, 6, 18, 9)]
    [InlineData("1921-04-08T23:00:41Z", 1921, 4, 8, 23, 0, 41)]
    [InlineData("1911-06-20T00:43:10Z", 1911, 6, 20, 0, 43, 10)]
    [InlineData("1909-07-14T07:51:19Z", 1909, 7, 14, 7, 51, 19)]
    [InlineData("2013-10-17T10:10:12Z", 2013, 10, 17, 10, 10, 12)]
    [InlineData("1992-09-12T04:43:33Z", 1992, 9, 12, 4, 43, 33)]
    [InlineData("1908-01-23T05:11:03Z", 1908, 1, 23, 5, 11, 3)]
    [InlineData("1904-04-27T21:04:39Z", 1904, 4, 27, 21, 4, 39)]
    [InlineData("1914-01-04T09:07:19Z", 1914, 1, 4, 9, 7, 19)]
    [InlineData("1964-03-05T18:48:22Z", 1964, 3, 5, 18, 48, 22)]
    [InlineData("1912-09-22T21:20:04Z", 1912, 9, 22, 21, 20, 4)]
    [InlineData("1938-12-04T00:12:08Z", 1938, 12, 4, 0, 12, 8)]
    [InlineData("1991-10-22T23:12:40Z", 1991, 10, 22, 23, 12, 40)]
    [InlineData("1997-07-15T14:05:40Z", 1997, 7, 15, 14, 5, 40)]
    [InlineData("1997-10-12T17:37:44Z", 1997, 10, 12, 17, 37, 44)]
    [InlineData("1970-01-24T11:56:17Z", 1970, 1, 24, 11, 56, 17)]
    [InlineData("1962-02-21T22:29:57Z", 1962, 2, 21, 22, 29, 57)]
    [InlineData("2006-09-23T00:11:28Z", 2006, 9, 23, 0, 11, 28)]
    [InlineData("1930-05-25T05:23:23Z", 1930, 5, 25, 5, 23, 23)]
    [InlineData("1909-01-18T14:02:34Z", 1909, 1, 18, 14, 2, 34)]
    [InlineData("1946-03-21T12:09:45Z", 1946, 3, 21, 12, 9, 45)]
    [InlineData("2013-08-05T12:36:44Z", 2013, 8, 5, 12, 36, 44)]
    [InlineData("1986-05-04T07:45:19Z", 1986, 5, 4, 7, 45, 19)]
    [InlineData("1912-08-26T17:19:22Z", 1912, 8, 26, 17, 19, 22)]
    [InlineData("1991-02-06T12:22:25Z", 1991, 2, 6, 12, 22, 25)]
    [InlineData("1928-06-16T00:18:40Z", 1928, 6, 16, 0, 18, 40)]
    [InlineData("1966-09-23T19:56:49Z", 1966, 9, 23, 19, 56, 49)]
    [InlineData("1969-06-07T07:21:41Z", 1969, 6, 7, 7, 21, 41)]
    [InlineData("1952-09-19T01:51:41Z", 1952, 9, 19, 1, 51, 41)]
    [InlineData("2003-01-24T04:32:59Z", 2003, 1, 24, 4, 32, 59)]
    [InlineData("1979-03-30T12:08:00Z", 1979, 3, 30, 12, 8, 0)]
    [InlineData("1994-06-16T12:15:52Z", 1994, 6, 16, 12, 15, 52)]
    [InlineData("1909-02-12T21:44:31Z", 1909, 2, 12, 21, 44, 31)]
    [InlineData("1972-12-17T13:01:46Z", 1972, 12, 17, 13, 1, 46)]
    [InlineData("1985-01-21T20:01:57Z", 1985, 1, 21, 20, 1, 57)]
    [InlineData("1924-07-27T10:17:22Z", 1924, 7, 27, 10, 17, 22)]
    [InlineData("1904-05-25T01:52:18Z", 1904, 5, 25, 1, 52, 18)]
    [InlineData("1918-05-21T19:17:31Z", 1918, 5, 21, 19, 17, 31)]
    [InlineData("1902-09-11T03:48:17Z", 1902, 9, 11, 3, 48, 17)]
    [InlineData("1953-07-19T17:09:38Z", 1953, 7, 19, 17, 9, 38)]
    [InlineData("1933-06-28T19:23:08Z", 1933, 6, 28, 19, 23, 8)]
    [InlineData("1932-06-20T17:44:24Z", 1932, 6, 20, 17, 44, 24)]
    [InlineData("1910-05-29T19:27:44Z", 1910, 5, 29, 19, 27, 44)]
    [InlineData("1962-08-17T20:51:53Z", 1962, 8, 17, 20, 51, 53)]
    [InlineData("1921-01-06T18:00:35Z", 1921, 1, 6, 18, 0, 35)]
    [InlineData("1995-01-31T22:10:57Z", 1995, 1, 31, 22, 10, 57)]
    [InlineData("1964-08-18T01:56:03Z", 1964, 8, 18, 1, 56, 3)]
    public void ToDateTimeShouldReturnTheExpectedResult(string value, int year, int month, int day, int hour, int minute, int second)
    {
        // Arrange / Act
        // Must be tested against UTC or tests might pass locally, but fail on a build server in another time zone.
        DateTime actual = value.ToDateTime().ToUniversalTime();

        // Assert
        Assert.Equal(year, actual.Year);
        Assert.Equal(month, actual.Month);
        Assert.Equal(day, actual.Day);
        Assert.Equal(hour, actual.Hour);
        Assert.Equal(minute, actual.Minute);
        Assert.Equal(second, actual.Second);
    }

    [Theory(DisplayName = "ToDateOnly should return the expected result")]
    [InlineData("1950-06-11", 1950, 6, 11)]
    [InlineData("1966-10-22", 1966, 10, 22)]
    [InlineData("1938-02-02", 1938, 2, 2)]
    [InlineData("2009-01-31", 2009, 1, 31)]
    [InlineData("1931-01-17", 1931, 1, 17)]
    [InlineData("1955-12-02", 1955, 12, 2)]
    [InlineData("2009-08-31", 2009, 8, 31)]
    [InlineData("1921-05-25", 1921, 5, 25)]
    [InlineData("2019-06-20", 2019, 6, 20)]
    [InlineData("1904-12-23", 1904, 12, 23)]
    [InlineData("1918-03-31", 1918, 3, 31)]
    [InlineData("1948-02-08", 1948, 2, 8)]
    [InlineData("1950-01-02", 1950, 1, 2)]
    [InlineData("1945-12-02", 1945, 12, 2)]
    [InlineData("2015-05-26", 2015, 5, 26)]
    [InlineData("2013-07-04", 2013, 7, 4)]
    [InlineData("1906-03-12", 1906, 3, 12)]
    [InlineData("1982-05-21", 1982, 5, 21)]
    [InlineData("1984-09-17", 1984, 9, 17)]
    [InlineData("1906-08-07", 1906, 8, 7)]
    [InlineData("1964-08-25", 1964, 8, 25)]
    [InlineData("1995-10-07", 1995, 10, 7)]
    [InlineData("1907-01-21", 1907, 1, 21)]
    [InlineData("1912-03-12", 1912, 3, 12)]
    [InlineData("1944-09-19", 1944, 9, 19)]
    [InlineData("1906-07-06", 1906, 7, 6)]
    [InlineData("1918-11-29", 1918, 11, 29)]
    [InlineData("2018-07-23", 2018, 7, 23)]
    [InlineData("1925-11-21", 1925, 11, 21)]
    [InlineData("2005-07-18", 2005, 7, 18)]
    [InlineData("1946-10-07", 1946, 10, 7)]
    [InlineData("1993-02-14", 1993, 2, 14)]
    [InlineData("1933-10-04", 1933, 10, 4)]
    [InlineData("2017-07-31", 2017, 7, 31)]
    [InlineData("1928-04-17", 1928, 4, 17)]
    [InlineData("1916-11-27", 1916, 11, 27)]
    [InlineData("1907-02-18", 1907, 2, 18)]
    [InlineData("1929-04-06", 1929, 4, 6)]
    [InlineData("1918-06-29", 1918, 6, 29)]
    [InlineData("1918-02-19", 1918, 2, 19)]
    [InlineData("1982-04-23", 1982, 4, 23)]
    [InlineData("2014-07-08", 2014, 7, 8)]
    [InlineData("1973-05-25", 1973, 5, 25)]
    [InlineData("1963-09-04", 1963, 9, 4)]
    [InlineData("1950-01-16", 1950, 1, 16)]
    [InlineData("1934-12-09", 1934, 12, 9)]
    [InlineData("1981-06-26", 1981, 6, 26)]
    [InlineData("1975-10-01", 1975, 10, 1)]
    [InlineData("1935-02-03", 1935, 2, 3)]
    [InlineData("1967-12-02", 1967, 12, 2)]
    [InlineData("1991-11-06", 1991, 11, 6)]
    [InlineData("2019-10-17", 2019, 10, 17)]
    [InlineData("1933-12-16", 1933, 12, 16)]
    [InlineData("1953-06-17", 1953, 6, 17)]
    [InlineData("1921-04-08", 1921, 4, 8)]
    [InlineData("1911-06-20", 1911, 6, 20)]
    [InlineData("1909-07-14", 1909, 7, 14)]
    [InlineData("2013-10-17", 2013, 10, 17)]
    [InlineData("1992-09-12", 1992, 9, 12)]
    [InlineData("1908-01-23", 1908, 1, 23)]
    [InlineData("1904-04-27", 1904, 4, 27)]
    [InlineData("1914-01-04", 1914, 1, 4)]
    [InlineData("1964-03-05", 1964, 3, 5)]
    [InlineData("1912-09-22", 1912, 9, 22)]
    [InlineData("1938-12-04", 1938, 12, 4)]
    [InlineData("1991-10-22", 1991, 10, 22)]
    [InlineData("1997-07-15", 1997, 7, 15)]
    [InlineData("1997-10-12", 1997, 10, 12)]
    [InlineData("1970-01-24", 1970, 1, 24)]
    [InlineData("1962-02-21", 1962, 2, 21)]
    [InlineData("2006-09-23", 2006, 9, 23)]
    [InlineData("1930-05-25", 1930, 5, 25)]
    [InlineData("1909-01-18", 1909, 1, 18)]
    [InlineData("1946-03-21", 1946, 3, 21)]
    [InlineData("2013-08-05", 2013, 8, 5)]
    [InlineData("1986-05-04", 1986, 5, 4)]
    [InlineData("1912-08-26", 1912, 8, 26)]
    [InlineData("1991-02-06", 1991, 2, 6)]
    [InlineData("1928-06-16", 1928, 6, 16)]
    [InlineData("1966-09-23", 1966, 9, 23)]
    [InlineData("1969-06-07", 1969, 6, 7)]
    [InlineData("1952-09-19", 1952, 9, 19)]
    [InlineData("2003-01-24", 2003, 1, 24)]
    [InlineData("1979-03-30", 1979, 3, 30)]
    [InlineData("1994-06-16", 1994, 6, 16)]
    [InlineData("1909-02-12", 1909, 2, 12)]
    [InlineData("1972-12-17", 1972, 12, 17)]
    [InlineData("1985-01-21", 1985, 1, 21)]
    [InlineData("1924-07-27", 1924, 7, 27)]
    [InlineData("1904-05-25", 1904, 5, 25)]
    [InlineData("1918-05-21", 1918, 5, 21)]
    [InlineData("1902-09-11", 1902, 9, 11)]
    [InlineData("1953-07-19", 1953, 7, 19)]
    [InlineData("1933-06-28", 1933, 6, 28)]
    [InlineData("1932-06-20", 1932, 6, 20)]
    [InlineData("1910-05-29", 1910, 5, 29)]
    [InlineData("1962-08-17", 1962, 8, 17)]
    [InlineData("1921-01-06", 1921, 1, 6)]
    [InlineData("1995-01-31", 1995, 1, 31)]
    [InlineData("1964-08-18", 1964, 8, 18)]
    public void ToDateOnlyShouldReturnTheExpectedResult(string value, int year, int month, int day)
    {
        // Arrange / Act
        DateOnly actual = value.ToDateOnly();

        // Assert
        Assert.Equal(year, actual.Year);
        Assert.Equal(month, actual.Month);
        Assert.Equal(day, actual.Day);
    }

    [Theory(DisplayName = "ToTimeOnly should return the expected result")]
    [InlineData("00:28:47", 0, 28, 47)]
    [InlineData("05:52:09", 5, 52, 9)]
    [InlineData("17:32:40", 17, 32, 40)]
    [InlineData("20:58:12", 20, 58, 12)]
    [InlineData("22:48:37", 22, 48, 37)]
    [InlineData("15:39:22", 15, 39, 22)]
    [InlineData("08:02:08", 8, 2, 8)]
    [InlineData("13:54:55", 13, 54, 55)]
    [InlineData("22:43:19", 22, 43, 19)]
    [InlineData("19:32:54", 19, 32, 54)]
    [InlineData("01:39:50", 1, 39, 50)]
    [InlineData("13:43:22", 13, 43, 22)]
    [InlineData("02:56:04", 2, 56, 4)]
    [InlineData("19:52:50", 19, 52, 50)]
    [InlineData("09:37:38", 9, 37, 38)]
    [InlineData("12:40:57", 12, 40, 57)]
    [InlineData("14:16:14", 14, 16, 14)]
    [InlineData("17:09:26", 17, 9, 26)]
    [InlineData("12:51:42", 12, 51, 42)]
    [InlineData("17:18:09", 17, 18, 9)]
    [InlineData("04:47:37", 4, 47, 37)]
    [InlineData("06:36:27", 6, 36, 27)]
    [InlineData("05:04:11", 5, 4, 11)]
    [InlineData("08:40:37", 8, 40, 37)]
    [InlineData("03:25:00", 3, 25, 0)]
    [InlineData("10:14:24", 10, 14, 24)]
    [InlineData("17:08:38", 17, 8, 38)]
    [InlineData("02:26:20", 2, 26, 20)]
    [InlineData("12:48:22", 12, 48, 22)]
    [InlineData("22:29:14", 22, 29, 14)]
    [InlineData("23:54:01", 23, 54, 1)]
    [InlineData("15:44:15", 15, 44, 15)]
    [InlineData("03:34:57", 3, 34, 57)]
    [InlineData("06:21:39", 6, 21, 39)]
    [InlineData("21:58:54", 21, 58, 54)]
    [InlineData("02:59:35", 2, 59, 35)]
    [InlineData("23:08:26", 23, 8, 26)]
    [InlineData("12:12:43", 12, 12, 43)]
    [InlineData("09:26:51", 9, 26, 51)]
    [InlineData("11:45:00", 11, 45, 0)]
    [InlineData("06:41:53", 6, 41, 53)]
    [InlineData("21:31:43", 21, 31, 43)]
    [InlineData("04:36:31", 4, 36, 31)]
    [InlineData("02:22:24", 2, 22, 24)]
    [InlineData("02:17:48", 2, 17, 48)]
    [InlineData("10:49:35", 10, 49, 35)]
    [InlineData("06:39:59", 6, 39, 59)]
    [InlineData("04:44:19", 4, 44, 19)]
    [InlineData("14:45:13", 14, 45, 13)]
    [InlineData("11:54:24", 11, 54, 24)]
    [InlineData("11:17:18", 11, 17, 18)]
    [InlineData("04:03:05", 4, 3, 5)]
    [InlineData("20:42:55", 20, 42, 55)]
    [InlineData("06:18:09", 6, 18, 9)]
    [InlineData("23:00:41", 23, 0, 41)]
    [InlineData("00:43:10", 0, 43, 10)]
    [InlineData("07:51:19", 7, 51, 19)]
    [InlineData("10:10:12", 10, 10, 12)]
    [InlineData("04:43:33", 4, 43, 33)]
    [InlineData("05:11:03", 5, 11, 3)]
    [InlineData("21:04:39", 21, 4, 39)]
    [InlineData("09:07:19", 9, 7, 19)]
    [InlineData("18:48:22", 18, 48, 22)]
    [InlineData("21:20:04", 21, 20, 4)]
    [InlineData("00:12:08", 0, 12, 8)]
    [InlineData("23:12:40", 23, 12, 40)]
    [InlineData("14:05:40", 14, 5, 40)]
    [InlineData("17:37:44", 17, 37, 44)]
    [InlineData("11:56:17", 11, 56, 17)]
    [InlineData("22:29:57", 22, 29, 57)]
    [InlineData("00:11:28", 0, 11, 28)]
    [InlineData("05:23:23", 5, 23, 23)]
    [InlineData("14:02:34", 14, 2, 34)]
    [InlineData("12:09:45", 12, 9, 45)]
    [InlineData("12:36:44", 12, 36, 44)]
    [InlineData("07:45:19", 7, 45, 19)]
    [InlineData("17:19:22", 17, 19, 22)]
    [InlineData("12:22:25", 12, 22, 25)]
    [InlineData("00:18:40", 0, 18, 40)]
    [InlineData("19:56:49", 19, 56, 49)]
    [InlineData("07:21:41", 7, 21, 41)]
    [InlineData("01:51:41", 1, 51, 41)]
    [InlineData("04:32:59", 4, 32, 59)]
    [InlineData("12:08:00", 12, 8, 0)]
    [InlineData("12:15:52", 12, 15, 52)]
    [InlineData("21:44:31", 21, 44, 31)]
    [InlineData("13:01:46", 13, 1, 46)]
    [InlineData("20:01:57", 20, 1, 57)]
    [InlineData("10:17:22", 10, 17, 22)]
    [InlineData("01:52:18", 1, 52, 18)]
    [InlineData("19:17:31", 19, 17, 31)]
    [InlineData("03:48:17", 3, 48, 17)]
    [InlineData("17:09:38", 17, 9, 38)]
    [InlineData("19:23:08", 19, 23, 8)]
    [InlineData("17:44:24", 17, 44, 24)]
    [InlineData("19:27:44", 19, 27, 44)]
    [InlineData("20:51:53", 20, 51, 53)]
    [InlineData("18:00:35", 18, 0, 35)]
    [InlineData("22:10:57", 22, 10, 57)]
    [InlineData("01:56:03", 1, 56, 3)]
    public void ToTimeTimeShouldReturnTheExpectedResult(string value, int hour, int minute, int second)
    {
        // Arrange / Act
        DateTime actual = value.ToDateTime();

        // Assert
        Assert.Equal(hour, actual.Hour);
        Assert.Equal(minute, actual.Minute);
        Assert.Equal(second, actual.Second);
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
