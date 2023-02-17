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

using System.Collections.Generic;
using OnixLabs.Core.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class EnumerationTests
{
    [Fact(DisplayName = "Enumerations should be equal")]
    public void EnumerationsShouldBeEqual()
    {
        // Arrange
        Color a = Color.Red;
        Color b = Color.Red;

        // Assert
        Assert.Equal(a, b);
    }

    [Fact(DisplayName = "Enumerations should not be equal")]
    public void EnumerationsShouldNotBeEqual()
    {
        // Arrange
        Color a = Color.Red;
        Color b = Color.Blue;

        // Assert
        Assert.NotEqual(a, b);
    }

    [Fact(DisplayName = "Enumeration should return all enumeration instances")]
    public void EnumerationsShouldReturnAllEnumerationInstances()
    {
        // Arrange
        IEnumerable<Color> colors = Color.GetAll();

        // Assert
        Assert.Contains(colors, item => item == Color.Red);
        Assert.Contains(colors, item => item == Color.Green);
        Assert.Contains(colors, item => item == Color.Blue);
    }

    [Fact(DisplayName = "Enumeration.FromName should return the expected enumeration entry")]
    public void EnumerationFromNameShouldReturnTheExpectedEnumerationEntry()
    {
        // Arrange
        Color color = Color.FromName("Green");

        // Assert
        Assert.Equal(Color.Green, color);
    }

    [Fact(DisplayName = "Enumeration.FromValue should return the expected enumeration entry")]
    public void EnumerationFromValueShouldReturnTheExpectedEnumerationEntry()
    {
        // Arrange
        Color color = Color.FromValue(2);

        // Assert
        Assert.Equal(Color.Green, color);
    }

    [Fact(DisplayName = "Enumeration.GetAll should return all enumeration entries")]
    public void EnumerationGetAllShouldReturnAllEnumerationEntries()
    {
        // Arrange
        IEnumerable<Color> entries = Color.GetAll();

        // Assert
        Assert.Contains(Color.Blue, entries);
        Assert.Contains(Color.Green, entries);
        Assert.Contains(Color.Red, entries);
    }

    [Fact(DisplayName = "Enumeration.GetEntries should return all enumeration entries")]
    public void EnumerationGetEntriesShouldReturnAllEnumerationEntries()
    {
        // Arrange
        IEnumerable<(int Value, string Name)> entries = Color.GetEntries();

        // Assert
        Assert.Contains(Color.Blue.ToEntry(), entries);
        Assert.Contains(Color.Green.ToEntry(), entries);
        Assert.Contains(Color.Red.ToEntry(), entries);
    }

    [Fact(DisplayName = "Enumeration.GetNames should return all enumeration names")]
    public void EnumerationGetNamesShouldReturnAllEnumerationNames()
    {
        // Arrange
        IEnumerable<string> entries = Color.GetNames();

        // Assert
        Assert.Contains(Color.Blue.Name, entries);
        Assert.Contains(Color.Green.Name, entries);
        Assert.Contains(Color.Red.Name, entries);
    }

    [Fact(DisplayName = "Enumeration.GetValues should return all enumeration values")]
    public void EnumerationGetValuesShouldReturnAllEnumerationValues()
    {
        // Arrange
        IEnumerable<int> entries = Color.GetValues();

        // Assert
        Assert.Contains(Color.Blue.Value, entries);
        Assert.Contains(Color.Green.Value, entries);
        Assert.Contains(Color.Red.Value, entries);
    }
}