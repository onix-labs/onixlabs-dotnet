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

using System.Numerics;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class NumericExtensionsTests
{
    [Theory(DisplayName = "Decimal.GetUnscaledValue should produce the expected result")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(0.000000001, 1)]
    [InlineData(-0.000000001, -1)]
    [InlineData(123456789, 123456789)]
    [InlineData(12345678.9, 123456789)]
    [InlineData(1234567.89, 123456789)]
    [InlineData(123456.789, 123456789)]
    [InlineData(12345.6789, 123456789)]
    [InlineData(1234.56789, 123456789)]
    [InlineData(123.456789, 123456789)]
    [InlineData(12.3456789, 123456789)]
    [InlineData(1.23456789, 123456789)]
    [InlineData(-123456789, -123456789)]
    [InlineData(-12345678.9, -123456789)]
    [InlineData(-1234567.89, -123456789)]
    [InlineData(-123456.789, -123456789)]
    [InlineData(-12345.6789, -123456789)]
    [InlineData(-1234.56789, -123456789)]
    [InlineData(-123.456789, -123456789)]
    [InlineData(-12.3456789, -123456789)]
    [InlineData(-1.23456789, -123456789)]
    [InlineData(1.000000001, 1000000001)]
    public void DecimalGetUnscaledValueShouldProduceExpectedResult(decimal value, BigInteger expected)
    {
        // When
        BigInteger actual = value.GetUnscaledValue();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BinaryInteger.ToBigInteger should produce the expected result")]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(123456789, 123456789)]
    [InlineData(-123456789, -123456789)]
    public void BinaryIntegerToBigIntegerShouldProduceExpectedResult(int value, BigInteger expected)
    {
        // When
        BigInteger actual = value.ToBigInteger();

        // Then
        Assert.Equal(expected, actual);
    }
}
