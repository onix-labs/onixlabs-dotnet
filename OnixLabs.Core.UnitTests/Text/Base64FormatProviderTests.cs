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

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base64FormatProviderTests
{
    [Fact(DisplayName = "Base64FormatProvider.PaddedRfc4648 should produce the expected result")]
    public void Base64FormatProviderPaddedRfc4648ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 0;
        const string expectedName = "PaddedRfc4648";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        // When
        int actualValue = Base64FormatProvider.PaddedRfc4648.Value;
        string actualName = Base64FormatProvider.PaddedRfc4648.Name;
        string actualAlphabet = Base64FormatProvider.PaddedRfc4648.Alphabet;
        bool actualIsPadded = Base64FormatProvider.PaddedRfc4648.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.True(actualIsPadded);
    }

    [Fact(DisplayName = "Base64FormatProvider.UnpaddedRfc4648 should produce the expected result")]
    public void Base64FormatProviderUnpaddedRfc4648ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 1;
        const string expectedName = "UnpaddedRfc4648";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        // When
        int actualValue = Base64FormatProvider.UnpaddedRfc4648.Value;
        string actualName = Base64FormatProvider.UnpaddedRfc4648.Name;
        string actualAlphabet = Base64FormatProvider.UnpaddedRfc4648.Alphabet;
        bool actualIsPadded = Base64FormatProvider.UnpaddedRfc4648.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.False(actualIsPadded);
    }

    [Fact(DisplayName = "Base64FormatProvider.PaddedRfc4648Url should produce the expected result")]
    public void Base64FormatProviderPaddedRfc4648UrlShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 2;
        const string expectedName = "PaddedRfc4648Url";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        // When
        int actualValue = Base64FormatProvider.PaddedRfc4648Url.Value;
        string actualName = Base64FormatProvider.PaddedRfc4648Url.Name;
        string actualAlphabet = Base64FormatProvider.PaddedRfc4648Url.Alphabet;
        bool actualIsPadded = Base64FormatProvider.PaddedRfc4648Url.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.True(actualIsPadded);
    }

    [Fact(DisplayName = "Base64FormatProvider.UnpaddedRfc4648Url should produce the expected result")]
    public void Base64FormatProviderUnpaddedRfc4648UrlShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 3;
        const string expectedName = "UnpaddedRfc4648Url";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        // When
        int actualValue = Base64FormatProvider.UnpaddedRfc4648Url.Value;
        string actualName = Base64FormatProvider.UnpaddedRfc4648Url.Name;
        string actualAlphabet = Base64FormatProvider.UnpaddedRfc4648Url.Alphabet;
        bool actualIsPadded = Base64FormatProvider.UnpaddedRfc4648Url.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.False(actualIsPadded);
    }

    [Fact(DisplayName = "Base64FormatProvider.GetFormat should return Base64FormatProvider when the given type is Base64FormatProvider")]
    public void Base64FormatProviderShouldReturnBase64FormatProviderWhenGivenTypeIsBase64FormatProvider()
    {
        // Given
        Type type = typeof(Base64FormatProvider);

        // When
        object? result = Base64FormatProvider.PaddedRfc4648.GetFormat(type);

        // Then
        Assert.NotNull(result);
        Assert.IsType<Base64FormatProvider>(result);
    }

    [Fact(DisplayName = "Base64FormatProvider.GetFormat should return null when the given type is not Base64FormatProvider")]
    public void Base64FormatProviderShouldReturnNullWhenGivenTypeIsNotBase64FormatProvider()
    {
        // Given
        Type type = typeof(object);

        // When
        object? result = Base64FormatProvider.PaddedRfc4648.GetFormat(type);

        // Then
        Assert.Null(result);
    }
}
