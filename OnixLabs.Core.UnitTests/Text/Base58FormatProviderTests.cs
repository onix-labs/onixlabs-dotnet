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

public sealed class Base58FormatProviderTests
{
    [Fact(DisplayName = "Base58FormatProvider.Bitcoin should produce the expected result")]
    public void Base16FormatProviderBitcoinShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 0;
        const string expectedName = "Bitcoin";
        const string expectedAlphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        // When
        int actualValue = Base58FormatProvider.Bitcoin.Value;
        string actualName = Base58FormatProvider.Bitcoin.Name;
        string actualAlphabet = Base58FormatProvider.Bitcoin.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base58FormatProvider.Flickr should produce the expected result")]
    public void Base16FormatProviderFlickrShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 1;
        const string expectedName = "Flickr";
        const string expectedAlphabet = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

        // When
        int actualValue = Base58FormatProvider.Flickr.Value;
        string actualName = Base58FormatProvider.Flickr.Name;
        string actualAlphabet = Base58FormatProvider.Flickr.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base58FormatProvider.Ripple should produce the expected result")]
    public void Base16FormatProviderRippleShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 2;
        const string expectedName = "Ripple";
        const string expectedAlphabet = "rpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65jkm8oFqi1tuvAxyz";

        // When
        int actualValue = Base58FormatProvider.Ripple.Value;
        string actualName = Base58FormatProvider.Ripple.Name;
        string actualAlphabet = Base58FormatProvider.Ripple.Alphabet;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
    }

    [Fact(DisplayName = "Base58FormatProvider.GetFormat should return Base58FormatProvider when the given type is Base58FormatProvider")]
    public void Base58FormatProviderShouldReturnBase58FormatProviderWhenGivenTypeIsBase58FormatProvider()
    {
        // Given
        Type type = typeof(Base58FormatProvider);

        // When
        object? result = Base58FormatProvider.Bitcoin.GetFormat(type);

        // Then
        Assert.NotNull(result);
        Assert.IsType<Base58FormatProvider>(result);
    }

    [Fact(DisplayName = "Base58FormatProvider.GetFormat should return null when the given type is not Base58FormatProvider")]
    public void Base58FormatProviderShouldReturnNullWhenGivenTypeIsNotBase58FormatProvider()
    {
        // Given
        Type type = typeof(object);

        // When
        object? result = Base58FormatProvider.Bitcoin.GetFormat(type);

        // Then
        Assert.Null(result);
    }
}
