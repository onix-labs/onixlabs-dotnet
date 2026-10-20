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
    [Fact(DisplayName = "Base32FormatProvider.UnpaddedRfc4648Uppercase should produce the expected result")]
    public void Base32FormatProviderUnpaddedRfc4648UppercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 0;
        const string expectedName = "UnpaddedRfc4648Uppercase";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedRfc4648Uppercase.Value;
        string actualName = Base32FormatProvider.UnpaddedRfc4648Uppercase.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedRfc4648Uppercase.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedRfc4648Uppercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedZBase32 should produce the expected result")]
    public void Base32FormatProviderZBase32ShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 1;
        const string expectedName = "UnpaddedZBase32";
        const string expectedAlphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedZBase32.Value;
        string actualName = Base32FormatProvider.UnpaddedZBase32.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedZBase32.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedZBase32.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedGeoHash should produce the expected result")]
    public void Base32FormatProviderGeoHashShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 2;
        const string expectedName = "UnpaddedGeoHash";
        const string expectedAlphabet = "0123456789bcdefghjkmnpqrstuvwxyz";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedGeoHash.Value;
        string actualName = Base32FormatProvider.UnpaddedGeoHash.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedGeoHash.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedGeoHash.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedCrockford should produce the expected result")]
    public void Base32FormatProviderCrockfordShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 3;
        const string expectedName = "UnpaddedCrockford";
        const string expectedAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedCrockford.Value;
        string actualName = Base32FormatProvider.UnpaddedCrockford.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedCrockford.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedCrockford.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedBase32HexUppercase should produce the expected result")]
    public void Base32FormatProviderBase32HexShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 4;
        const string expectedName = "UnpaddedBase32HexUppercase";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedBase32HexUppercase.Value;
        string actualName = Base32FormatProvider.UnpaddedBase32HexUppercase.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedBase32HexUppercase.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedBase32HexUppercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedRfc4648Uppercase should produce the expected result")]
    public void Base32FormatProviderPaddedRfc4648UppercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 5;
        const string expectedName = "PaddedRfc4648Uppercase";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedRfc4648Uppercase.Value;
        string actualName = Base32FormatProvider.PaddedRfc4648Uppercase.Name;
        string actualAlphabet = Base32FormatProvider.PaddedRfc4648Uppercase.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedRfc4648Uppercase.IsPadded;

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

    [Fact(DisplayName = "Base32FormatProvider.PaddedBase32HexUppercase should produce the expected result")]
    public void Base32FormatProviderPaddedBase32HexUppercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 9;
        const string expectedName = "PaddedBase32HexUppercase";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedBase32HexUppercase.Value;
        string actualName = Base32FormatProvider.PaddedBase32HexUppercase.Name;
        string actualAlphabet = Base32FormatProvider.PaddedBase32HexUppercase.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedBase32HexUppercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedRfc4648Lowercase should produce the expected result")]
    public void Base32FormatProviderUnpaddedRfc4648LowercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 10;
        const string expectedName = "UnpaddedRfc4648Lowercase";
        const string expectedAlphabet = "abcdefghijklmnopqrstuvwxyz234567";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedRfc4648Lowercase.Value;
        string actualName = Base32FormatProvider.UnpaddedRfc4648Lowercase.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedRfc4648Lowercase.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedRfc4648Lowercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedRfc4648Lowercase should produce the expected result")]
    public void Base32FormatProviderPaddedRfc4648LowercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 11;
        const string expectedName = "PaddedRfc4648Lowercase";
        const string expectedAlphabet = "abcdefghijklmnopqrstuvwxyz234567";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedRfc4648Lowercase.Value;
        string actualName = Base32FormatProvider.PaddedRfc4648Lowercase.Name;
        string actualAlphabet = Base32FormatProvider.PaddedRfc4648Lowercase.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedRfc4648Lowercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedRfc4648Invariant should produce the expected result")]
    public void Base32FormatProviderUnpaddedRfc4648InvariantShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 12;
        const string expectedName = "UnpaddedRfc4648Invariant";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedRfc4648Invariant.Value;
        string actualName = Base32FormatProvider.UnpaddedRfc4648Invariant.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedRfc4648Invariant.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedRfc4648Invariant.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedRfc4648Invariant should produce the expected result")]
    public void Base32FormatProviderPaddedRfc4648InvariantShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 13;
        const string expectedName = "PaddedRfc4648Invariant";
        const string expectedAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedRfc4648Invariant.Value;
        string actualName = Base32FormatProvider.PaddedRfc4648Invariant.Name;
        string actualAlphabet = Base32FormatProvider.PaddedRfc4648Invariant.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedRfc4648Invariant.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedBase32HexLowercase should produce the expected result")]
    public void Base32FormatProviderUnpaddedBase32HexLowercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 14;
        const string expectedName = "UnpaddedBase32HexLowercase";
        const string expectedAlphabet = "0123456789abcdefghijklmnopqrstuv";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedBase32HexLowercase.Value;
        string actualName = Base32FormatProvider.UnpaddedBase32HexLowercase.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedBase32HexLowercase.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedBase32HexLowercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedBase32HexLowercase should produce the expected result")]
    public void Base32FormatProviderPaddedBase32HexLowercaseShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 15;
        const string expectedName = "PaddedBase32HexLowercase";
        const string expectedAlphabet = "0123456789abcdefghijklmnopqrstuv";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedBase32HexLowercase.Value;
        string actualName = Base32FormatProvider.PaddedBase32HexLowercase.Name;
        string actualAlphabet = Base32FormatProvider.PaddedBase32HexLowercase.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedBase32HexLowercase.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.UnpaddedBase32HexInvariant should produce the expected result")]
    public void Base32FormatProviderUnpaddedBase32HexInvariantShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 16;
        const string expectedName = "UnpaddedBase32HexInvariant";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = false;

        // When
        int actualValue = Base32FormatProvider.UnpaddedBase32HexInvariant.Value;
        string actualName = Base32FormatProvider.UnpaddedBase32HexInvariant.Name;
        string actualAlphabet = Base32FormatProvider.UnpaddedBase32HexInvariant.Alphabet;
        bool actualPadding = Base32FormatProvider.UnpaddedBase32HexInvariant.IsPadded;

        // Then
        Assert.Equal(expectedValue, actualValue);
        Assert.Equal(expectedName, actualName);
        Assert.Equal(expectedAlphabet, actualAlphabet);
        Assert.Equal(expectedPadding, actualPadding);
    }

    [Fact(DisplayName = "Base32FormatProvider.PaddedBase32HexInvariant should produce the expected result")]
    public void Base32FormatProviderPaddedBase32HexInvariantShouldProduceExpectedAlphabet()
    {
        // Given
        const int expectedValue = 17;
        const string expectedName = "PaddedBase32HexInvariant";
        const string expectedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        const bool expectedPadding = true;

        // When
        int actualValue = Base32FormatProvider.PaddedBase32HexInvariant.Value;
        string actualName = Base32FormatProvider.PaddedBase32HexInvariant.Name;
        string actualAlphabet = Base32FormatProvider.PaddedBase32HexInvariant.Alphabet;
        bool actualPadding = Base32FormatProvider.PaddedBase32HexInvariant.IsPadded;

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
        object? result = Base32FormatProvider.UnpaddedRfc4648Uppercase.GetFormat(type);

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
        object? result = Base32FormatProvider.UnpaddedRfc4648Uppercase.GetFormat(type);

        // Then
        Assert.Null(result);
    }
}
