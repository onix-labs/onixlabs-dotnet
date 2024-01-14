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

using System.Buffers.Binary;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalWriteTests
{
    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write significand big endian should produce expected result.")]
    public void BigDecimalWriteSignificandBigEndianShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] significandBytes = new byte[info.Significand.ToByteArray().Length];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteSignificandBigEndian(significandBytes);
        BigInteger significand = new(significandBytes, isBigEndian: true);

        // Then
        Assert.Equal(info.Significand, significand);
    }

    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write exponent big endian should produce expected result.")]
    public void BigDecimalWriteExponentBigEndianShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] exponentBytes = new byte[4];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteExponentBigEndian(exponentBytes);
        int exponent = BinaryPrimitives.ReadInt32BigEndian(exponentBytes);

        // Then
        Assert.Equal(info.Exponent, exponent);
    }

    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write big endian significand and exponent should produce expected result.")]
    public void BigDecimalWriteBigEndianSignificandAndExponentShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] significandBytes = new byte[info.Significand.ToByteArray().Length];
        byte[] exponentBytes = new byte[4];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteSignificandBigEndian(significandBytes);
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteExponentBigEndian(exponentBytes);

        BigInteger significand = new(significandBytes, isBigEndian: true);
        int exponent = BinaryPrimitives.ReadInt32BigEndian(exponentBytes);

        // Then
        Assert.Equal(info.Significand, significand);
        Assert.Equal(info.Exponent, exponent);
    }

    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write significand little endian should produce expected result.")]
    public void BigDecimalWriteSignificandLittleEndianShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] significandBytes = new byte[info.Significand.ToByteArray().Length];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteSignificandLittleEndian(significandBytes);
        BigInteger significand = new(significandBytes);

        // Then
        Assert.Equal(info.Significand, significand);
    }

    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write exponent little endian should produce expected result.")]
    public void BigDecimalWriteExponentLittleEndianShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] exponentBytes = new byte[4];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteExponentLittleEndian(exponentBytes);
        int exponent = BinaryPrimitives.ReadInt32LittleEndian(exponentBytes);

        // Then
        Assert.Equal(info.Exponent, exponent);
    }

    [BigDecimalArithmeticAbsData]
    [Theory(DisplayName = "BigDecimal write little endian significand and exponent should produce expected result.")]
    public void BigDecimalWriteLittleEndianSignificandAndExponentShouldProduceExpectedResult(decimal value)
    {
        // Given
        NumberInfo info = value.ToBigDecimal().ToNumberInfo();
        byte[] significandBytes = new byte[info.Significand.ToByteArray().Length];
        byte[] exponentBytes = new byte[4];

        // When
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteSignificandLittleEndian(significandBytes);
        ((IFloatingPoint<BigDecimal>)(BigDecimal)value).WriteExponentLittleEndian(exponentBytes);

        BigInteger significand = new(significandBytes);
        int exponent = BinaryPrimitives.ReadInt32LittleEndian(exponentBytes);

        // Then
        Assert.Equal(info.Significand, significand);
        Assert.Equal(info.Exponent, exponent);
    }
}
