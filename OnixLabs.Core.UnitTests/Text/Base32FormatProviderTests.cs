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

public sealed class Base32FormatProviderTests
{
    [Fact(DisplayName = "Base32FormatProvider.Rfc4648 should produce the expected result")]
    public void Base32FormatProviderRfc4648ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 0;
        const string expectedName = "Rfc4648";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.Rfc4648.Value;
        string actualName = Base32FormatProvider.Rfc4648.Name;
        string actualAlphabet = Base32FormatProvider.Rfc4648.Alphabet;
        bool actualPadding = Base32FormatProvider.Rfc4648.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.ZBase32 should produce the expected result")]
    public void Base32FormatProviderZBase32ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 1;
        const string expectedName = "ZBase32";
        const string expectedAlphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.ZBase32.Value;
        string actualName = Base32FormatProvider.ZBase32.Name;
        string actualAlphabet = Base32FormatProvider.ZBase32.Alphabet;
        bool actualPadding = Base32FormatProvider.ZBase32.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.GeoHash should produce the expected result")]
    public void Base32FormatProviderGeoHashShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 2;
        const string expectedName = "GeoHash";
        const string expectedAlphabet = "0123456789bcdefghjkmnpqrstuvwxyz";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.GeoHash.Value;
        string actualName = Base32FormatProvider.GeoHash.Name;
        string actualAlphabet = Base32FormatProvider.GeoHash.Alphabet;
        bool actualPadding = Base32FormatProvider.GeoHash.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.Crockford should produce the expected result")]
    public void Base32FormatProviderCrockfordShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 3;
        const string expectedName = "Crockford";
        const string expectedAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.Crockford.Value;
        string actualName = Base32FormatProvider.Crockford.Name;
        string actualAlphabet = Base32FormatProvider.Crockford.Alphabet;
        bool actualPadding = Base32FormatProvider.Crockford.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.Base32Hex should produce the expected result")]
    public void Base32FormatProviderBase32HexShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 4;
        const string expectedName = "Base32Hex";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.Base32Hex.Value;
        string actualName = Base32FormatProvider.Base32Hex.Name;
        string actualAlphabet = Base32FormatProvider.Base32Hex.Alphabet;
        bool actualPadding = Base32FormatProvider.Base32Hex.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedRfc4648 should produce the expected result")]
    public void Base32FormatProviderPaddedRfc4648ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 5;
        const string expectedName = "PaddedRfc4648";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedRfc4648.Value;
        string actualName = Base32FormatProvider.PaddedRfc4648.Name;
        string actualAlphabet = Base32FormatProvider.PaddedRfc4648.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedRfc4648.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedZBase32 should produce the expected result")]
    public void Base32FormatProviderPaddedZBase32ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 6;
        const string expectedName = "PaddedZBase32";
        const string expectedAlphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedZBase32.Value;
        string actualName = Base32FormatProvider.PaddedZBase32.Name;
        string actualAlphabet = Base32FormatProvider.PaddedZBase32.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedZBase32.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedGeoHash should produce the expected result")]
    public void Base32FormatProviderPaddedGeoHashShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 7;
        const string expectedName = "PaddedGeoHash";
        const string expectedAlphabet = "0123456789bcdefghjkmnpqrstuvwxyz";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedGeoHash.Value;
        string actualName = Base32FormatProvider.PaddedGeoHash.Name;
        string actualAlphabet = Base32FormatProvider.PaddedGeoHash.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedGeoHash.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedCrockford should produce the expected result")]
    public void Base32FormatProviderPaddedCrockfordShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 8;
        const string expectedName = "PaddedCrockford";
        const string expectedAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedCrockford.Value;
        string actualName = Base32FormatProvider.PaddedCrockford.Name;
        string actualAlphabet = Base32FormatProvider.PaddedCrockford.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedCrockford.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedBase32Hex should produce the expected result")]
    public void Base32FormatProviderPaddedBase32HexShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 9;
        const string expectedName = "PaddedBase32Hex";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedBase32Hex.Value;
        string actualName = Base32FormatProvider.PaddedBase32Hex.Name;
        string actualAlphabet = Base32FormatProvider.PaddedBase32Hex.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedBase32Hex.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.GetFormat should return Base32FormatProvider when the given type is Base32FormatProvider")]
    public void Base32FormatProviderShouldReturnBase32FormatProviderWhenGivenTypeIsBase32FormatProvider()
    {
        // Given
        Type type = typeof(Base32FormatProvider);

        // When
        object? result = Base32FormatProvider.Rfc4648.GetFormat(type);

        // Then
        Assert.NotNull(result);
        Assert.IsType<Base32FormatProvider>(result);
    }

    [Fact(DisplayName = "Base32FormatProvider.GetFormat should return null when the given type is not Base32FormatProvider")]
    public void Base32FormatProviderShouldReturnNullWhenGivenTypeIsNotBase32FormatProvider()
    {
        // Given
        Type type = typeof(object);

        // When
        object? result = Base32FormatProvider.Rfc4648.GetFormat(type);

        // Then
        Assert.Null(result);
    }
}
