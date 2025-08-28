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

using System.Collections.Generic;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests;

public sealed class EnumerationTests
{
    [Fact(DisplayName = "Enumerations should be equal")]
    public void EnumerationsShouldBeEqual()
    {
        // Given
        Color a = Color.Red;
        Color b = Color.Red;

        // Then
        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Enumerations should not be equal")]
    public void EnumerationsShouldNotBeEqual()
    {
        // Given
        Color a = Color.Red;
        Color b = Color.Blue;

        // Then
        Assert.NotEqual(a, b);
        Assert.True(a != b);
        Assert.False(a == b);
    }

    [Fact(DisplayName = "Enumeration should return all enumeration instances")]
    public void EnumerationsShouldReturnAllEnumerationInstances()
    {
        // Given
        IEnumerable<Color> colors = Color.GetAll();

        // Then
        Assert.Contains(colors, item => item == Color.Red);
        Assert.Contains(colors, item => item == Color.Green);
        Assert.Contains(colors, item => item == Color.Blue);
    }

    [Fact(DisplayName = "Enumeration.FromName should return the expected enumeration entry")]
    public void EnumerationFromNameShouldReturnTheExpectedEnumerationEntry()
    {
        // Given
        Color color = Color.FromName("Green");

        // Then
        Assert.Equal(Color.Green, color);
    }

    [Fact(DisplayName = "Enumeration.FromValue should return the expected enumeration entry")]
    public void EnumerationFromValueShouldReturnTheExpectedEnumerationEntry()
    {
        // Given
        Color color = Color.FromValue(2);

        // Then
        Assert.Equal(Color.Green, color);
    }

    [Fact(DisplayName = "Enumeration.GetAll should return all enumeration entries")]
    public void EnumerationGetAllShouldReturnAllEnumerationEntries()
    {
        // Given
        IEnumerable<Color> entries = Color.GetAll();

        // Then
        Assert.Contains(Color.Blue, entries);
        Assert.Contains(Color.Green, entries);
        Assert.Contains(Color.Red, entries);
    }

    [Fact(DisplayName = "Enumeration.GetEntries should return all enumeration entries")]
    public void EnumerationGetEntriesShouldReturnAllEnumerationEntries()
    {
        // Given
        IEnumerable<(int Value, string Name)> entries = Color.GetEntries();

        // Then
        Assert.Contains(Color.Blue.ToEntry(), entries);
        Assert.Contains(Color.Green.ToEntry(), entries);
        Assert.Contains(Color.Red.ToEntry(), entries);
    }

    [Fact(DisplayName = "Enumeration.GetNames should return all enumeration names")]
    public void EnumerationGetNamesShouldReturnAllEnumerationNames()
    {
        // Given
        IEnumerable<string> entries = Color.GetNames();

        // Then
        Assert.Contains(Color.Blue.Name, entries);
        Assert.Contains(Color.Green.Name, entries);
        Assert.Contains(Color.Red.Name, entries);
    }

    [Fact(DisplayName = "Enumeration.GetValues should return all enumeration values")]
    public void EnumerationGetValuesShouldReturnAllEnumerationValues()
    {
        // Given
        IEnumerable<int> entries = Color.GetValues();

        // Then
        Assert.Contains(Color.Blue.Value, entries);
        Assert.Contains(Color.Green.Value, entries);
        Assert.Contains(Color.Red.Value, entries);
    }

    [Fact(DisplayName = "Enumeration.CompareTo as Enumeration should return the correct value")]
    public void EnumerationCompareToAsEnumerationShouldReturnTheCorrectValue()
    {
        // Given
        Color left = Color.Red;
        Color right = Color.Blue;

        // When
        int actual = left.CompareTo(right);

        // Then
        Assert.Equal(-1, actual);
    }

    [Fact(DisplayName = "Enumeration.CompareTo as Object should return the correct value")]
    public void EnumerationCompareToAsObjectShouldReturnTheCorrectValue()
    {
        // Given
        Color left = Color.Red;
        object right = Color.Blue;

        // When
        int actual = left.CompareTo(right);

        // Then
        Assert.Equal(-1, actual);
    }

    [Fact(DisplayName = "Enumeration.ToString should produce the expected result")]
    public void EnumerationToStringShouldProduceExpectedResult()
    {
        // Given
        const string expected = "Red";
        Color red = Color.Red;

        // When
        string actual = red.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
