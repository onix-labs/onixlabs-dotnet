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
using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base16FormatProviderTests
{
    [Fact(DisplayName = "Base16FormatProvider.Invariant should produce the expected result")]
    public void Base16FormatProviderInvariantShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 0;
        const string expectedName = "Invariant";
        const string expectedAlphabet = "0123456789ABCDEFabcdef";

        // When
        int actualValue = Base16FormatProvider.Invariant.Value;
        string actualName = Base16FormatProvider.Invariant.Name;
        string actualAlphabet = Base16FormatProvider.Invariant.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base16FormatProvider.Uppercase should produce the expected result")]
    public void Base16FormatProviderUppercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 1;
        const string expectedName = "Uppercase";
        const string expectedAlphabet = "0123456789ABCDEF";

        // When
        int actualValue = Base16FormatProvider.Uppercase.Value;
        string actualName = Base16FormatProvider.Uppercase.Name;
        string actualAlphabet = Base16FormatProvider.Uppercase.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base16FormatProvider.Lowercase should produce the expected result")]
    public void Base16FormatProviderLowercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 2;
        const string expectedName = "Lowercase";
        const string expectedAlphabet = "0123456789abcdef";

        // When
        int actualValue = Base16FormatProvider.Lowercase.Value;
        string actualName = Base16FormatProvider.Lowercase.Name;
        string actualAlphabet = Base16FormatProvider.Lowercase.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base16FormatProvider.GetFormat should return Base16FormatProvider when the given type is Base16FormatProvider")]
    public void Base16FormatProviderShouldReturnBase16FormatProviderWhenGivenTypeIsBase16FormatProvider()
    {
        // Given
        Type type = typeof(Base16FormatProvider);

        // When
        object? result = Base16FormatProvider.Invariant.GetFormat(type);

        // Then
        Assert.NotNull(result);
        Assert.IsType<Base16FormatProvider>(result);
    }

    [Fact(DisplayName = "Base16FormatProvider.GetFormat should return null when the given type is not Base16FormatProvider")]
    public void Base16FormatProviderShouldReturnNullWhenGivenTypeIsNotBase16FormatProvider()
    {
        // Given
        Type type = typeof(object);

        // When
        object? result = Base16FormatProvider.Invariant.GetFormat(type);

        // Then
        Assert.Null(result);
    }
}
