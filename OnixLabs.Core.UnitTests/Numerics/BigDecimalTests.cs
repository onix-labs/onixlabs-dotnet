// Copyright 2020-2022 ONIXLabs
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
using OnixLabs.Core.Numerics;
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalTests
{
    [Theory(DisplayName = "BigDecimal should be constructable from an unscaled signed byte array")]
    [InlineData(sbyte.MaxValue, 0, "127")]
    [InlineData(sbyte.MaxValue, 1, "12.7")]
    [InlineData(sbyte.MaxValue, 2, "1.27")]
    [InlineData(sbyte.MaxValue, 3, "0.127")]
    [InlineData(sbyte.MaxValue, 4, "0.0127")]
    [InlineData(sbyte.MaxValue, 5, "0.00127")]
    [InlineData(sbyte.MaxValue, 6, "0.000127")]
    [InlineData(sbyte.MaxValue, 7, "0.0000127")]
    [InlineData(sbyte.MaxValue, 8, "0.00000127")]
    [InlineData(sbyte.MaxValue, 9, "0.000000127")]
    [InlineData(sbyte.MaxValue, 10, "0.0000000127")]
    [InlineData(sbyte.MaxValue, 20, "0.00000000000000000127")]
    [InlineData(sbyte.MinValue, 0, "-128")]
    [InlineData(sbyte.MinValue, 1, "-12.8")]
    [InlineData(sbyte.MinValue, 2, "-1.28")]
    [InlineData(sbyte.MinValue, 3, "-0.128")]
    [InlineData(sbyte.MinValue, 4, "-0.0128")]
    [InlineData(sbyte.MinValue, 5, "-0.00128")]
    [InlineData(sbyte.MinValue, 6, "-0.000128")]
    [InlineData(sbyte.MinValue, 7, "-0.0000128")]
    [InlineData(sbyte.MinValue, 8, "-0.00000128")]
    [InlineData(sbyte.MinValue, 9, "-0.000000128")]
    [InlineData(sbyte.MinValue, 10, "-0.0000000128")]
    [InlineData(sbyte.MinValue, 20, "-0.00000000000000000128")]
    [InlineData(short.MaxValue, 0, "32767")]
    [InlineData(short.MaxValue, 1, "3276.7")]
    [InlineData(short.MaxValue, 2, "327.67")]
    [InlineData(short.MaxValue, 3, "32.767")]
    [InlineData(short.MaxValue, 4, "3.2767")]
    [InlineData(short.MaxValue, 5, "0.32767")]
    [InlineData(short.MaxValue, 6, "0.032767")]
    [InlineData(short.MaxValue, 7, "0.0032767")]
    [InlineData(short.MaxValue, 8, "0.00032767")]
    [InlineData(short.MaxValue, 9, "0.000032767")]
    [InlineData(short.MaxValue, 10, "0.0000032767")]
    [InlineData(short.MaxValue, 20, "0.00000000000000032767")]
    [InlineData(short.MinValue, 0, "-32768")]
    [InlineData(short.MinValue, 1, "-3276.8")]
    [InlineData(short.MinValue, 2, "-327.68")]
    [InlineData(short.MinValue, 3, "-32.768")]
    [InlineData(short.MinValue, 4, "-3.2768")]
    [InlineData(short.MinValue, 5, "-0.32768")]
    [InlineData(short.MinValue, 6, "-0.032768")]
    [InlineData(short.MinValue, 7, "-0.0032768")]
    [InlineData(short.MinValue, 8, "-0.00032768")]
    [InlineData(short.MinValue, 9, "-0.000032768")]
    [InlineData(short.MinValue, 10, "-0.0000032768")]
    [InlineData(short.MinValue, 20, "-0.00000000000000032768")]
    [InlineData(1, 0, "1")]
    [InlineData(1, 1, "0.1")]
    [InlineData(1, 2, "0.01")]
    [InlineData(1, 3, "0.001")]
    [InlineData(1, 4, "0.0001")]
    [InlineData(1, 5, "0.00001")]
    [InlineData(1, 6, "0.000001")]
    [InlineData(1, 7, "0.0000001")]
    [InlineData(1, 8, "0.00000001")]
    [InlineData(1, 9, "0.000000001")]
    [InlineData(1, 10, "0.0000000001")]
    [InlineData(1, 20, "0.00000000000000000001")]
    [InlineData(-1, 0, "-1")]
    [InlineData(-1, 1, "-0.1")]
    [InlineData(-1, 2, "-0.01")]
    [InlineData(-1, 3, "-0.001")]
    [InlineData(-1, 4, "-0.0001")]
    [InlineData(-1, 5, "-0.00001")]
    [InlineData(-1, 6, "-0.000001")]
    [InlineData(-1, 7, "-0.0000001")]
    [InlineData(-1, 8, "-0.00000001")]
    [InlineData(-1, 9, "-0.000000001")]
    [InlineData(-1, 10, "-0.0000000001")]
    [InlineData(-1, 20, "-0.00000000000000000001")]
    [InlineData(int.MaxValue, 0, "2147483647")]
    [InlineData(int.MaxValue, 1, "214748364.7")]
    [InlineData(int.MaxValue, 2, "21474836.47")]
    [InlineData(int.MaxValue, 3, "2147483.647")]
    [InlineData(int.MaxValue, 4, "214748.3647")]
    [InlineData(int.MaxValue, 5, "21474.83647")]
    [InlineData(int.MaxValue, 6, "2147.483647")]
    [InlineData(int.MaxValue, 7, "214.7483647")]
    [InlineData(int.MaxValue, 8, "21.47483647")]
    [InlineData(int.MaxValue, 9, "2.147483647")]
    [InlineData(int.MaxValue, 10, "0.2147483647")]
    [InlineData(int.MaxValue, 20, "0.00000000002147483647")]
    [InlineData(int.MinValue, 0, "-2147483648")]
    [InlineData(int.MinValue, 1, "-214748364.8")]
    [InlineData(int.MinValue, 2, "-21474836.48")]
    [InlineData(int.MinValue, 3, "-2147483.648")]
    [InlineData(int.MinValue, 4, "-214748.3648")]
    [InlineData(int.MinValue, 5, "-21474.83648")]
    [InlineData(int.MinValue, 6, "-2147.483648")]
    [InlineData(int.MinValue, 7, "-214.7483648")]
    [InlineData(int.MinValue, 8, "-21.47483648")]
    [InlineData(int.MinValue, 9, "-2.147483648")]
    [InlineData(int.MinValue, 10, "-0.2147483648")]
    [InlineData(int.MinValue, 20, "-0.00000000002147483648")]
    [InlineData(long.MaxValue, 0, "9223372036854775807")]
    [InlineData(long.MaxValue, 1, "922337203685477580.7")]
    [InlineData(long.MaxValue, 2, "92233720368547758.07")]
    [InlineData(long.MaxValue, 3, "9223372036854775.807")]
    [InlineData(long.MaxValue, 4, "922337203685477.5807")]
    [InlineData(long.MaxValue, 5, "92233720368547.75807")]
    [InlineData(long.MaxValue, 6, "9223372036854.775807")]
    [InlineData(long.MaxValue, 7, "922337203685.4775807")]
    [InlineData(long.MaxValue, 8, "92233720368.54775807")]
    [InlineData(long.MaxValue, 9, "9223372036.854775807")]
    [InlineData(long.MaxValue, 10, "922337203.6854775807")]
    [InlineData(long.MaxValue, 20, "0.09223372036854775807")]
    [InlineData(long.MinValue, 0, "-9223372036854775808")]
    [InlineData(long.MinValue, 1, "-922337203685477580.8")]
    [InlineData(long.MinValue, 2, "-92233720368547758.08")]
    [InlineData(long.MinValue, 3, "-9223372036854775.808")]
    [InlineData(long.MinValue, 4, "-922337203685477.5808")]
    [InlineData(long.MinValue, 5, "-92233720368547.75808")]
    [InlineData(long.MinValue, 6, "-9223372036854.775808")]
    [InlineData(long.MinValue, 7, "-922337203685.4775808")]
    [InlineData(long.MinValue, 8, "-92233720368.54775808")]
    [InlineData(long.MinValue, 9, "-9223372036.854775808")]
    [InlineData(long.MinValue, 10, "-922337203.6854775808")]
    [InlineData(long.MinValue, 20, "-0.09223372036854775808")]
    public void BigDecimalConstructorByteArrayScaled(long value, int precision, string expected)
    {
        // Arrange
        byte[] valueBytes = BitConverter.GetBytes(value);
        byte[] precisionBytes = BitConverter.GetBytes(precision);
        byte[] bytes = valueBytes.ConcatenateWith(precisionBytes);
        BigDecimal candidate = new(bytes);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from an unscaled signed byte array")]
    [InlineData(byte.MaxValue, 0, "255")]
    [InlineData(byte.MaxValue, 1, "25.5")]
    [InlineData(byte.MaxValue, 2, "2.55")]
    [InlineData(byte.MaxValue, 3, "0.255")]
    [InlineData(byte.MaxValue, 4, "0.0255")]
    [InlineData(byte.MaxValue, 5, "0.00255")]
    [InlineData(byte.MaxValue, 6, "0.000255")]
    [InlineData(byte.MaxValue, 7, "0.0000255")]
    [InlineData(byte.MaxValue, 8, "0.00000255")]
    [InlineData(byte.MaxValue, 9, "0.000000255")]
    [InlineData(byte.MaxValue, 10, "0.0000000255")]
    [InlineData(byte.MaxValue, 20, "0.00000000000000000255")]
    [InlineData(byte.MinValue, 0, "0")]
    [InlineData(byte.MinValue, 1, "0.0")]
    [InlineData(byte.MinValue, 2, "0.00")]
    [InlineData(byte.MinValue, 3, "0.000")]
    [InlineData(byte.MinValue, 4, "0.0000")]
    [InlineData(byte.MinValue, 5, "0.00000")]
    [InlineData(byte.MinValue, 6, "0.000000")]
    [InlineData(byte.MinValue, 7, "0.0000000")]
    [InlineData(byte.MinValue, 8, "0.00000000")]
    [InlineData(byte.MinValue, 9, "0.000000000")]
    [InlineData(byte.MinValue, 10, "0.0000000000")]
    [InlineData(byte.MinValue, 20, "0.00000000000000000000")]
    [InlineData(ushort.MaxValue, 0, "65535")]
    [InlineData(ushort.MaxValue, 1, "6553.5")]
    [InlineData(ushort.MaxValue, 2, "655.35")]
    [InlineData(ushort.MaxValue, 3, "65.535")]
    [InlineData(ushort.MaxValue, 4, "6.5535")]
    [InlineData(ushort.MaxValue, 5, "0.65535")]
    [InlineData(ushort.MaxValue, 6, "0.065535")]
    [InlineData(ushort.MaxValue, 7, "0.0065535")]
    [InlineData(ushort.MaxValue, 8, "0.00065535")]
    [InlineData(ushort.MaxValue, 9, "0.000065535")]
    [InlineData(ushort.MaxValue, 10, "0.0000065535")]
    [InlineData(ushort.MaxValue, 20, "0.00000000000000065535")]
    [InlineData(ushort.MinValue, 0, "0")]
    [InlineData(ushort.MinValue, 1, "0.0")]
    [InlineData(ushort.MinValue, 2, "0.00")]
    [InlineData(ushort.MinValue, 3, "0.000")]
    [InlineData(ushort.MinValue, 4, "0.0000")]
    [InlineData(ushort.MinValue, 5, "0.00000")]
    [InlineData(ushort.MinValue, 6, "0.000000")]
    [InlineData(ushort.MinValue, 7, "0.0000000")]
    [InlineData(ushort.MinValue, 8, "0.00000000")]
    [InlineData(ushort.MinValue, 9, "0.000000000")]
    [InlineData(ushort.MinValue, 10, "0.0000000000")]
    [InlineData(ushort.MinValue, 20, "0.00000000000000000000")]
    [InlineData(uint.MaxValue, 0, "4294967295")]
    [InlineData(uint.MaxValue, 1, "429496729.5")]
    [InlineData(uint.MaxValue, 2, "42949672.95")]
    [InlineData(uint.MaxValue, 3, "4294967.295")]
    [InlineData(uint.MaxValue, 4, "429496.7295")]
    [InlineData(uint.MaxValue, 5, "42949.67295")]
    [InlineData(uint.MaxValue, 6, "4294.967295")]
    [InlineData(uint.MaxValue, 7, "429.4967295")]
    [InlineData(uint.MaxValue, 8, "42.94967295")]
    [InlineData(uint.MaxValue, 9, "4.294967295")]
    [InlineData(uint.MaxValue, 10, "0.4294967295")]
    [InlineData(uint.MaxValue, 20, "0.00000000004294967295")]
    [InlineData(uint.MinValue, 0, "0")]
    [InlineData(uint.MinValue, 1, "0.0")]
    [InlineData(uint.MinValue, 2, "0.00")]
    [InlineData(uint.MinValue, 3, "0.000")]
    [InlineData(uint.MinValue, 4, "0.0000")]
    [InlineData(uint.MinValue, 5, "0.00000")]
    [InlineData(uint.MinValue, 6, "0.000000")]
    [InlineData(uint.MinValue, 7, "0.0000000")]
    [InlineData(uint.MinValue, 8, "0.00000000")]
    [InlineData(uint.MinValue, 9, "0.000000000")]
    [InlineData(uint.MinValue, 10, "0.0000000000")]
    [InlineData(uint.MinValue, 20, "0.00000000000000000000")]
    [InlineData(ulong.MaxValue, 0, "18446744073709551615")]
    [InlineData(ulong.MaxValue, 1, "1844674407370955161.5")]
    [InlineData(ulong.MaxValue, 2, "184467440737095516.15")]
    [InlineData(ulong.MaxValue, 3, "18446744073709551.615")]
    [InlineData(ulong.MaxValue, 4, "1844674407370955.1615")]
    [InlineData(ulong.MaxValue, 5, "184467440737095.51615")]
    [InlineData(ulong.MaxValue, 6, "18446744073709.551615")]
    [InlineData(ulong.MaxValue, 7, "1844674407370.9551615")]
    [InlineData(ulong.MaxValue, 8, "184467440737.09551615")]
    [InlineData(ulong.MaxValue, 9, "18446744073.709551615")]
    [InlineData(ulong.MaxValue, 10, "1844674407.3709551615")]
    [InlineData(ulong.MaxValue, 20, "0.18446744073709551615")]
    [InlineData(ulong.MinValue, 0, "0")]
    [InlineData(ulong.MinValue, 1, "0.0")]
    [InlineData(ulong.MinValue, 2, "0.00")]
    [InlineData(ulong.MinValue, 3, "0.000")]
    [InlineData(ulong.MinValue, 4, "0.0000")]
    [InlineData(ulong.MinValue, 5, "0.00000")]
    [InlineData(ulong.MinValue, 6, "0.000000")]
    [InlineData(ulong.MinValue, 7, "0.0000000")]
    [InlineData(ulong.MinValue, 8, "0.00000000")]
    [InlineData(ulong.MinValue, 9, "0.000000000")]
    [InlineData(ulong.MinValue, 10, "0.0000000000")]
    [InlineData(ulong.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalConstructorByteArrayUnscaled(ulong value, int precision, string expected)
    {
        // Arrange
        byte[] zeroByte = {byte.MinValue};
        byte[] valueBytes = BitConverter.GetBytes(value).ConcatenateWith(zeroByte);
        byte[] precisionBytes = BitConverter.GetBytes(precision);
        byte[] bytes = valueBytes.ConcatenateWith(precisionBytes);
        BigDecimal candidate = new(bytes);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled sbyte value and precision")]
    [InlineData(sbyte.MaxValue, 0, "127")]
    [InlineData(sbyte.MaxValue, 1, "12.7")]
    [InlineData(sbyte.MaxValue, 2, "1.27")]
    [InlineData(sbyte.MaxValue, 3, "0.127")]
    [InlineData(sbyte.MaxValue, 4, "0.0127")]
    [InlineData(sbyte.MaxValue, 5, "0.00127")]
    [InlineData(sbyte.MaxValue, 6, "0.000127")]
    [InlineData(sbyte.MaxValue, 7, "0.0000127")]
    [InlineData(sbyte.MaxValue, 8, "0.00000127")]
    [InlineData(sbyte.MaxValue, 9, "0.000000127")]
    [InlineData(sbyte.MaxValue, 10, "0.0000000127")]
    [InlineData(sbyte.MaxValue, 20, "0.00000000000000000127")]
    [InlineData(sbyte.MinValue, 0, "-128")]
    [InlineData(sbyte.MinValue, 1, "-12.8")]
    [InlineData(sbyte.MinValue, 2, "-1.28")]
    [InlineData(sbyte.MinValue, 3, "-0.128")]
    [InlineData(sbyte.MinValue, 4, "-0.0128")]
    [InlineData(sbyte.MinValue, 5, "-0.00128")]
    [InlineData(sbyte.MinValue, 6, "-0.000128")]
    [InlineData(sbyte.MinValue, 7, "-0.0000128")]
    [InlineData(sbyte.MinValue, 8, "-0.00000128")]
    [InlineData(sbyte.MinValue, 9, "-0.000000128")]
    [InlineData(sbyte.MinValue, 10, "-0.0000000128")]
    [InlineData(sbyte.MinValue, 20, "-0.00000000000000000128")]
    public void BigDecimalConstructorSByte(sbyte value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled byte value and precision")]
    [InlineData(byte.MaxValue, 0, "255")]
    [InlineData(byte.MaxValue, 1, "25.5")]
    [InlineData(byte.MaxValue, 2, "2.55")]
    [InlineData(byte.MaxValue, 3, "0.255")]
    [InlineData(byte.MaxValue, 4, "0.0255")]
    [InlineData(byte.MaxValue, 5, "0.00255")]
    [InlineData(byte.MaxValue, 6, "0.000255")]
    [InlineData(byte.MaxValue, 7, "0.0000255")]
    [InlineData(byte.MaxValue, 8, "0.00000255")]
    [InlineData(byte.MaxValue, 9, "0.000000255")]
    [InlineData(byte.MaxValue, 10, "0.0000000255")]
    [InlineData(byte.MaxValue, 20, "0.00000000000000000255")]
    [InlineData(byte.MinValue, 0, "0")]
    [InlineData(byte.MinValue, 1, "0.0")]
    [InlineData(byte.MinValue, 2, "0.00")]
    [InlineData(byte.MinValue, 3, "0.000")]
    [InlineData(byte.MinValue, 4, "0.0000")]
    [InlineData(byte.MinValue, 5, "0.00000")]
    [InlineData(byte.MinValue, 6, "0.000000")]
    [InlineData(byte.MinValue, 7, "0.0000000")]
    [InlineData(byte.MinValue, 8, "0.00000000")]
    [InlineData(byte.MinValue, 9, "0.000000000")]
    [InlineData(byte.MinValue, 10, "0.0000000000")]
    [InlineData(byte.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalConstructorByte(byte value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled short value and precision")]
    [InlineData(short.MaxValue, 0, "32767")]
    [InlineData(short.MaxValue, 1, "3276.7")]
    [InlineData(short.MaxValue, 2, "327.67")]
    [InlineData(short.MaxValue, 3, "32.767")]
    [InlineData(short.MaxValue, 4, "3.2767")]
    [InlineData(short.MaxValue, 5, "0.32767")]
    [InlineData(short.MaxValue, 6, "0.032767")]
    [InlineData(short.MaxValue, 7, "0.0032767")]
    [InlineData(short.MaxValue, 8, "0.00032767")]
    [InlineData(short.MaxValue, 9, "0.000032767")]
    [InlineData(short.MaxValue, 10, "0.0000032767")]
    [InlineData(short.MaxValue, 20, "0.00000000000000032767")]
    [InlineData(short.MinValue, 0, "-32768")]
    [InlineData(short.MinValue, 1, "-3276.8")]
    [InlineData(short.MinValue, 2, "-327.68")]
    [InlineData(short.MinValue, 3, "-32.768")]
    [InlineData(short.MinValue, 4, "-3.2768")]
    [InlineData(short.MinValue, 5, "-0.32768")]
    [InlineData(short.MinValue, 6, "-0.032768")]
    [InlineData(short.MinValue, 7, "-0.0032768")]
    [InlineData(short.MinValue, 8, "-0.00032768")]
    [InlineData(short.MinValue, 9, "-0.000032768")]
    [InlineData(short.MinValue, 10, "-0.0000032768")]
    [InlineData(short.MinValue, 20, "-0.00000000000000032768")]
    public void BigDecimalConstructorInt16(short value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled ushort value and precision")]
    [InlineData(ushort.MaxValue, 0, "65535")]
    [InlineData(ushort.MaxValue, 1, "6553.5")]
    [InlineData(ushort.MaxValue, 2, "655.35")]
    [InlineData(ushort.MaxValue, 3, "65.535")]
    [InlineData(ushort.MaxValue, 4, "6.5535")]
    [InlineData(ushort.MaxValue, 5, "0.65535")]
    [InlineData(ushort.MaxValue, 6, "0.065535")]
    [InlineData(ushort.MaxValue, 7, "0.0065535")]
    [InlineData(ushort.MaxValue, 8, "0.00065535")]
    [InlineData(ushort.MaxValue, 9, "0.000065535")]
    [InlineData(ushort.MaxValue, 10, "0.0000065535")]
    [InlineData(ushort.MaxValue, 20, "0.00000000000000065535")]
    [InlineData(ushort.MinValue, 0, "0")]
    [InlineData(ushort.MinValue, 1, "0.0")]
    [InlineData(ushort.MinValue, 2, "0.00")]
    [InlineData(ushort.MinValue, 3, "0.000")]
    [InlineData(ushort.MinValue, 4, "0.0000")]
    [InlineData(ushort.MinValue, 5, "0.00000")]
    [InlineData(ushort.MinValue, 6, "0.000000")]
    [InlineData(ushort.MinValue, 7, "0.0000000")]
    [InlineData(ushort.MinValue, 8, "0.00000000")]
    [InlineData(ushort.MinValue, 9, "0.000000000")]
    [InlineData(ushort.MinValue, 10, "0.0000000000")]
    [InlineData(ushort.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalConstructorUInt16(ushort value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled int value and precision")]
    [InlineData(1, 0, "1")]
    [InlineData(1, 1, "0.1")]
    [InlineData(1, 2, "0.01")]
    [InlineData(1, 3, "0.001")]
    [InlineData(1, 4, "0.0001")]
    [InlineData(1, 5, "0.00001")]
    [InlineData(1, 6, "0.000001")]
    [InlineData(1, 7, "0.0000001")]
    [InlineData(1, 8, "0.00000001")]
    [InlineData(1, 9, "0.000000001")]
    [InlineData(1, 10, "0.0000000001")]
    [InlineData(1, 20, "0.00000000000000000001")]
    [InlineData(-1, 0, "-1")]
    [InlineData(-1, 1, "-0.1")]
    [InlineData(-1, 2, "-0.01")]
    [InlineData(-1, 3, "-0.001")]
    [InlineData(-1, 4, "-0.0001")]
    [InlineData(-1, 5, "-0.00001")]
    [InlineData(-1, 6, "-0.000001")]
    [InlineData(-1, 7, "-0.0000001")]
    [InlineData(-1, 8, "-0.00000001")]
    [InlineData(-1, 9, "-0.000000001")]
    [InlineData(-1, 10, "-0.0000000001")]
    [InlineData(-1, 20, "-0.00000000000000000001")]
    [InlineData(int.MaxValue, 0, "2147483647")]
    [InlineData(int.MaxValue, 1, "214748364.7")]
    [InlineData(int.MaxValue, 2, "21474836.47")]
    [InlineData(int.MaxValue, 3, "2147483.647")]
    [InlineData(int.MaxValue, 4, "214748.3647")]
    [InlineData(int.MaxValue, 5, "21474.83647")]
    [InlineData(int.MaxValue, 6, "2147.483647")]
    [InlineData(int.MaxValue, 7, "214.7483647")]
    [InlineData(int.MaxValue, 8, "21.47483647")]
    [InlineData(int.MaxValue, 9, "2.147483647")]
    [InlineData(int.MaxValue, 10, "0.2147483647")]
    [InlineData(int.MaxValue, 20, "0.00000000002147483647")]
    [InlineData(int.MinValue, 0, "-2147483648")]
    [InlineData(int.MinValue, 1, "-214748364.8")]
    [InlineData(int.MinValue, 2, "-21474836.48")]
    [InlineData(int.MinValue, 3, "-2147483.648")]
    [InlineData(int.MinValue, 4, "-214748.3648")]
    [InlineData(int.MinValue, 5, "-21474.83648")]
    [InlineData(int.MinValue, 6, "-2147.483648")]
    [InlineData(int.MinValue, 7, "-214.7483648")]
    [InlineData(int.MinValue, 8, "-21.47483648")]
    [InlineData(int.MinValue, 9, "-2.147483648")]
    [InlineData(int.MinValue, 10, "-0.2147483648")]
    [InlineData(int.MinValue, 20, "-0.00000000002147483648")]
    public void BigDecimalConstructorInt32(int value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled uint value and precision")]
    [InlineData(uint.MaxValue, 0, "4294967295")]
    [InlineData(uint.MaxValue, 1, "429496729.5")]
    [InlineData(uint.MaxValue, 2, "42949672.95")]
    [InlineData(uint.MaxValue, 3, "4294967.295")]
    [InlineData(uint.MaxValue, 4, "429496.7295")]
    [InlineData(uint.MaxValue, 5, "42949.67295")]
    [InlineData(uint.MaxValue, 6, "4294.967295")]
    [InlineData(uint.MaxValue, 7, "429.4967295")]
    [InlineData(uint.MaxValue, 8, "42.94967295")]
    [InlineData(uint.MaxValue, 9, "4.294967295")]
    [InlineData(uint.MaxValue, 10, "0.4294967295")]
    [InlineData(uint.MaxValue, 20, "0.00000000004294967295")]
    [InlineData(uint.MinValue, 0, "0")]
    [InlineData(uint.MinValue, 1, "0.0")]
    [InlineData(uint.MinValue, 2, "0.00")]
    [InlineData(uint.MinValue, 3, "0.000")]
    [InlineData(uint.MinValue, 4, "0.0000")]
    [InlineData(uint.MinValue, 5, "0.00000")]
    [InlineData(uint.MinValue, 6, "0.000000")]
    [InlineData(uint.MinValue, 7, "0.0000000")]
    [InlineData(uint.MinValue, 8, "0.00000000")]
    [InlineData(uint.MinValue, 9, "0.000000000")]
    [InlineData(uint.MinValue, 10, "0.0000000000")]
    [InlineData(uint.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalConstructorUInt32(uint value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled long value and precision")]
    [InlineData(long.MaxValue, 0, "9223372036854775807")]
    [InlineData(long.MaxValue, 1, "922337203685477580.7")]
    [InlineData(long.MaxValue, 2, "92233720368547758.07")]
    [InlineData(long.MaxValue, 3, "9223372036854775.807")]
    [InlineData(long.MaxValue, 4, "922337203685477.5807")]
    [InlineData(long.MaxValue, 5, "92233720368547.75807")]
    [InlineData(long.MaxValue, 6, "9223372036854.775807")]
    [InlineData(long.MaxValue, 7, "922337203685.4775807")]
    [InlineData(long.MaxValue, 8, "92233720368.54775807")]
    [InlineData(long.MaxValue, 9, "9223372036.854775807")]
    [InlineData(long.MaxValue, 10, "922337203.6854775807")]
    [InlineData(long.MaxValue, 20, "0.09223372036854775807")]
    [InlineData(long.MinValue, 0, "-9223372036854775808")]
    [InlineData(long.MinValue, 1, "-922337203685477580.8")]
    [InlineData(long.MinValue, 2, "-92233720368547758.08")]
    [InlineData(long.MinValue, 3, "-9223372036854775.808")]
    [InlineData(long.MinValue, 4, "-922337203685477.5808")]
    [InlineData(long.MinValue, 5, "-92233720368547.75808")]
    [InlineData(long.MinValue, 6, "-9223372036854.775808")]
    [InlineData(long.MinValue, 7, "-922337203685.4775808")]
    [InlineData(long.MinValue, 8, "-92233720368.54775808")]
    [InlineData(long.MinValue, 9, "-9223372036.854775808")]
    [InlineData(long.MinValue, 10, "-922337203.6854775808")]
    [InlineData(long.MinValue, 20, "-0.09223372036854775808")]
    public void BigDecimalConstructorInt64(long value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from unscaled ulong value and precision")]
    [InlineData(ulong.MaxValue, 0, "18446744073709551615")]
    [InlineData(ulong.MaxValue, 1, "1844674407370955161.5")]
    [InlineData(ulong.MaxValue, 2, "184467440737095516.15")]
    [InlineData(ulong.MaxValue, 3, "18446744073709551.615")]
    [InlineData(ulong.MaxValue, 4, "1844674407370955.1615")]
    [InlineData(ulong.MaxValue, 5, "184467440737095.51615")]
    [InlineData(ulong.MaxValue, 6, "18446744073709.551615")]
    [InlineData(ulong.MaxValue, 7, "1844674407370.9551615")]
    [InlineData(ulong.MaxValue, 8, "184467440737.09551615")]
    [InlineData(ulong.MaxValue, 9, "18446744073.709551615")]
    [InlineData(ulong.MaxValue, 10, "1844674407.3709551615")]
    [InlineData(ulong.MaxValue, 20, "0.18446744073709551615")]
    [InlineData(ulong.MinValue, 0, "0")]
    [InlineData(ulong.MinValue, 1, "0.0")]
    [InlineData(ulong.MinValue, 2, "0.00")]
    [InlineData(ulong.MinValue, 3, "0.000")]
    [InlineData(ulong.MinValue, 4, "0.0000")]
    [InlineData(ulong.MinValue, 5, "0.00000")]
    [InlineData(ulong.MinValue, 6, "0.000000")]
    [InlineData(ulong.MinValue, 7, "0.0000000")]
    [InlineData(ulong.MinValue, 8, "0.00000000")]
    [InlineData(ulong.MinValue, 9, "0.000000000")]
    [InlineData(ulong.MinValue, 10, "0.0000000000")]
    [InlineData(ulong.MinValue, 20, "0.00000000000000000000")]
    public void BigDecimalConstructorUInt64(ulong value, int precision, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal(precision);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from float value")]
    [InlineData(0f, "0")]
    [InlineData(1f, "1")]
    [InlineData(1.1f, "1.1")]
    [InlineData(1.01f, "1.01")]
    [InlineData(1.001f, "1.001")]
    [InlineData(10.1f, "10.1")]
    [InlineData(10.01f, "10.01")]
    [InlineData(100.01f, "100.01")]
    [InlineData(123456f, "123456")]
    [InlineData(12345.6f, "12345.6")]
    [InlineData(1234.56f, "1234.56")]
    [InlineData(123.456f, "123.456")]
    [InlineData(12.3456f, "12.3456")]
    [InlineData(1.23456f, "1.23456")]
    [InlineData(0.123456f, "0.123456")]
    public void BigDecimalConstructorFloat(float value, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal should be constructable from double value")]
    [InlineData(0d, "0")]
    [InlineData(1d, "1")]
    [InlineData(1.1d, "1.1")]
    [InlineData(1.01d, "1.01")]
    [InlineData(1.001d, "1.001")]
    [InlineData(10.1d, "10.1")]
    [InlineData(10.01d, "10.01")]
    [InlineData(100.01d, "100.01")]
    [InlineData(1234567890d, "1234567890")]
    [InlineData(123456789.0d, "123456789")]
    [InlineData(12345678.90d, "12345678.9")]
    [InlineData(1234567.890d, "1234567.89")]
    [InlineData(123456.7890d, "123456.789")]
    [InlineData(12345.67890d, "12345.6789")]
    [InlineData(1234.567890d, "1234.56789")]
    [InlineData(123.4567890d, "123.456789")]
    [InlineData(12.34567890d, "12.3456789")]
    [InlineData(1.234567890d, "1.23456789")]
    [InlineData(0.1234567890d, "0.123456789")]
    public void BigDecimalConstructorDouble(double value, string expected)
    {
        // Arrange
        BigDecimal candidate = value.ToBigDecimal();

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.Parse Should produce the expected result")]
    [InlineData("0", "0")]
    [InlineData("0.0", "0.0")]
    [InlineData("0.00", "0.00")]
    [InlineData("0.000", "0.000")]
    [InlineData("0.0000", "0.0000")]
    [InlineData("0.00000", "0.00000")]
    [InlineData("0.000000", "0.000000")]
    [InlineData("0.0000000", "0.0000000")]
    [InlineData("0.00000000", "0.00000000")]
    [InlineData("0.000000000", "0.000000000")]
    [InlineData("0.0000000000", "0.0000000000")]
    [InlineData("0.00000000000000000000", "0.00000000000000000000")]
    [InlineData("1", "1")]
    [InlineData("0.1", "0.1")]
    [InlineData("0.01", "0.01")]
    [InlineData("0.001", "0.001")]
    [InlineData("0.0001", "0.0001")]
    [InlineData("0.00001", "0.00001")]
    [InlineData("0.000001", "0.000001")]
    [InlineData("0.0000001", "0.0000001")]
    [InlineData("0.00000001", "0.00000001")]
    [InlineData("0.000000001", "0.000000001")]
    [InlineData("0.0000000001", "0.0000000001")]
    [InlineData("0.00000000000000000001", "0.00000000000000000001")]
    [InlineData("-1", "-1")]
    [InlineData("-0.1", "-0.1")]
    [InlineData("-0.01", "-0.01")]
    [InlineData("-0.001", "-0.001")]
    [InlineData("-0.0001", "-0.0001")]
    [InlineData("-0.00001", "-0.00001")]
    [InlineData("-0.000001", "-0.000001")]
    [InlineData("-0.0000001", "-0.0000001")]
    [InlineData("-0.00000001", "-0.00000001")]
    [InlineData("-0.000000001", "-0.000000001")]
    [InlineData("-0.0000000001", "-0.0000000001")]
    [InlineData("-0.00000000000000000001", "-0.00000000000000000001")]
    [InlineData("127", "127")]
    [InlineData("12.7", "12.7")]
    [InlineData("1.27", "1.27")]
    [InlineData("0.127", "0.127")]
    [InlineData("0.0127", "0.0127")]
    [InlineData("0.00127", "0.00127")]
    [InlineData("0.000127", "0.000127")]
    [InlineData("0.0000127", "0.0000127")]
    [InlineData("0.00000127", "0.00000127")]
    [InlineData("0.000000127", "0.000000127")]
    [InlineData("0.0000000127", "0.0000000127")]
    [InlineData("0.00000000000000000127", "0.00000000000000000127")]
    [InlineData("-128", "-128")]
    [InlineData("-12.8", "-12.8")]
    [InlineData("-1.28", "-1.28")]
    [InlineData("-0.128", "-0.128")]
    [InlineData("-0.0128", "-0.0128")]
    [InlineData("-0.00128", "-0.00128")]
    [InlineData("-0.000128", "-0.000128")]
    [InlineData("-0.0000128", "-0.0000128")]
    [InlineData("-0.00000128", "-0.00000128")]
    [InlineData("-0.000000128", "-0.000000128")]
    [InlineData("-0.0000000128", "-0.0000000128")]
    [InlineData("-0.00000000000000000128", "-0.00000000000000000128")]
    [InlineData("32767", "32767")]
    [InlineData("3276.7", "3276.7")]
    [InlineData("327.67", "327.67")]
    [InlineData("32.767", "32.767")]
    [InlineData("3.2767", "3.2767")]
    [InlineData("0.32767", "0.32767")]
    [InlineData("0.032767", "0.032767")]
    [InlineData("0.0032767", "0.0032767")]
    [InlineData("0.00032767", "0.00032767")]
    [InlineData("0.000032767", "0.000032767")]
    [InlineData("0.0000032767", "0.0000032767")]
    [InlineData("0.00000000000000032767", "0.00000000000000032767")]
    [InlineData("-32768", "-32768")]
    [InlineData("-3276.8", "-3276.8")]
    [InlineData("-327.68", "-327.68")]
    [InlineData("-32.768", "-32.768")]
    [InlineData("-3.2768", "-3.2768")]
    [InlineData("-0.32768", "-0.32768")]
    [InlineData("-0.032768", "-0.032768")]
    [InlineData("-0.0032768", "-0.0032768")]
    [InlineData("-0.00032768", "-0.00032768")]
    [InlineData("-0.000032768", "-0.000032768")]
    [InlineData("-0.0000032768", "-0.0000032768")]
    [InlineData("-0.00000000000000032768", "-0.00000000000000032768")]
    [InlineData("2147483647", "2147483647")]
    [InlineData("214748364.7", "214748364.7")]
    [InlineData("21474836.47", "21474836.47")]
    [InlineData("2147483.647", "2147483.647")]
    [InlineData("214748.3647", "214748.3647")]
    [InlineData("21474.83647", "21474.83647")]
    [InlineData("2147.483647", "2147.483647")]
    [InlineData("214.7483647", "214.7483647")]
    [InlineData("21.47483647", "21.47483647")]
    [InlineData("2.147483647", "2.147483647")]
    [InlineData("0.2147483647", "0.2147483647")]
    [InlineData("0.00000000002147483647", "0.00000000002147483647")]
    [InlineData("-2147483648", "-2147483648")]
    [InlineData("-214748364.8", "-214748364.8")]
    [InlineData("-21474836.48", "-21474836.48")]
    [InlineData("-2147483.648", "-2147483.648")]
    [InlineData("-214748.3648", "-214748.3648")]
    [InlineData("-21474.83648", "-21474.83648")]
    [InlineData("-2147.483648", "-2147.483648")]
    [InlineData("-214.7483648", "-214.7483648")]
    [InlineData("-21.47483648", "-21.47483648")]
    [InlineData("-2.147483648", "-2.147483648")]
    [InlineData("-0.2147483648", "-0.2147483648")]
    [InlineData("-0.00000000002147483648", "-0.00000000002147483648")]
    [InlineData("9223372036854775807", "9223372036854775807")]
    [InlineData("922337203685477580.7", "922337203685477580.7")]
    [InlineData("92233720368547758.07", "92233720368547758.07")]
    [InlineData("9223372036854775.807", "9223372036854775.807")]
    [InlineData("922337203685477.5807", "922337203685477.5807")]
    [InlineData("92233720368547.75807", "92233720368547.75807")]
    [InlineData("9223372036854.775807", "9223372036854.775807")]
    [InlineData("922337203685.4775807", "922337203685.4775807")]
    [InlineData("92233720368.54775807", "92233720368.54775807")]
    [InlineData("9223372036.854775807", "9223372036.854775807")]
    [InlineData("922337203.6854775807", "922337203.6854775807")]
    [InlineData("0.09223372036854775807", "0.09223372036854775807")]
    [InlineData("-9223372036854775808", "-9223372036854775808")]
    [InlineData("-922337203685477580.8", "-922337203685477580.8")]
    [InlineData("-92233720368547758.08", "-92233720368547758.08")]
    [InlineData("-9223372036854775.808", "-9223372036854775.808")]
    [InlineData("-922337203685477.5808", "-922337203685477.5808")]
    [InlineData("-92233720368547.75808", "-92233720368547.75808")]
    [InlineData("-9223372036854.775808", "-9223372036854.775808")]
    [InlineData("-922337203685.4775808", "-922337203685.4775808")]
    [InlineData("-92233720368.54775808", "-92233720368.54775808")]
    [InlineData("-9223372036.854775808", "-9223372036.854775808")]
    [InlineData("-922337203.6854775808", "-922337203.6854775808")]
    [InlineData("-0.09223372036854775808", "-0.09223372036854775808")]
    [InlineData("18446744073709551615", "18446744073709551615")]
    [InlineData("1844674407370955161.5", "1844674407370955161.5")]
    [InlineData("184467440737095516.15", "184467440737095516.15")]
    [InlineData("18446744073709551.615", "18446744073709551.615")]
    [InlineData("1844674407370955.1615", "1844674407370955.1615")]
    [InlineData("184467440737095.51615", "184467440737095.51615")]
    [InlineData("18446744073709.551615", "18446744073709.551615")]
    [InlineData("1844674407370.9551615", "1844674407370.9551615")]
    [InlineData("184467440737.09551615", "184467440737.09551615")]
    [InlineData("18446744073.709551615", "18446744073.709551615")]
    [InlineData("1844674407.3709551615", "1844674407.3709551615")]
    [InlineData("0.18446744073709551615", "0.18446744073709551615")]
    [InlineData("0%", "0")]
    [InlineData("0%.", "0")]
    [InlineData("0.%", "0")]
    [InlineData("0%.0", "0.0")]
    [InlineData("0.%0", "0.0")]
    [InlineData("0.0%", "0.0")]
    [InlineData("0%.00", "0.00")]
    [InlineData("0.%00", "0.00")]
    [InlineData("0.0%0", "0.00")]
    [InlineData("0.00%", "0.00")]
    [InlineData("1*2*3*4*5*6*7*8*9*0.1*2*3*4*5*6*7*8*9*0", "1234567890.1234567890")]
    public void BigDecimalParse(string value, string expected)
    {
        // Arrange
        BigDecimal candidate = BigDecimal.Parse(value);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal.CompareTo should return the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, -1)]
    [InlineData(1.1, 1.1, 0)]
    [InlineData(1.1, 0.0, 1)]
    [InlineData(1.0, 0.1, 1)]
    [InlineData(0.0, 1.0, -1)]
    [InlineData(0.0, 0.1, -1)]
    [InlineData(0.1, 1.0, -1)]
    [InlineData(123.45, 123.45, 0)]
    [InlineData(123.45, 45.678, 1)]
    [InlineData(12.345, 45.678, -1)]
    [InlineData(0.1, 0.01, 1)]
    [InlineData(0.01, 0.001, 1)]
    [InlineData(0.001, 0.0001, 1)]
    [InlineData(0.1, 0.1, 0)]
    [InlineData(0.01, 0.01, 0)]
    [InlineData(0.001, 0.001, 0)]
    [InlineData(0.01, 0.1, -1)]
    [InlineData(0.001, 0.01, -1)]
    [InlineData(0.0001, 0.001, -1)]
    public void BigDecimalCompareTo(decimal left, decimal right, int expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        int actual = leftCandidate.CompareTo(rightCandidate);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "BigDecimal operator == should return the expected result")]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, false)]
    [InlineData(1.1, 1.1, true)]
    [InlineData(1.1, 0.0, false)]
    [InlineData(1.0, 0.1, false)]
    [InlineData(0.0, 1.0, false)]
    [InlineData(0.0, 0.1, false)]
    [InlineData(0.1, 1.0, false)]
    [InlineData(123.45, 123.45, true)]
    [InlineData(123.45, 45.678, false)]
    [InlineData(12.345, 45.678, false)]
    [InlineData(0.1, 0.01, false)]
    [InlineData(0.01, 0.001, false)]
    [InlineData(0.001, 0.0001, false)]
    [InlineData(0.1, 0.1, true)]
    [InlineData(0.01, 0.01, true)]
    [InlineData(0.001, 0.001, true)]
    [InlineData(0.01, 0.1, false)]
    [InlineData(0.001, 0.01, false)]
    [InlineData(0.0001, 0.001, false)]
    public void BigDecimalEquals(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate == rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate == right;
        bool actualDecimalBigDecimal = left == rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator != should return the expected result")]
    [InlineData(0, 0, false)]
    [InlineData(1, 1, false)]
    [InlineData(1, 0, true)]
    [InlineData(0, 1, true)]
    [InlineData(1.1, 1.1, false)]
    [InlineData(1.1, 0.0, true)]
    [InlineData(1.0, 0.1, true)]
    [InlineData(0.0, 1.0, true)]
    [InlineData(0.0, 0.1, true)]
    [InlineData(0.1, 1.0, true)]
    [InlineData(123.45, 123.45, false)]
    [InlineData(123.45, 45.678, true)]
    [InlineData(12.345, 45.678, true)]
    [InlineData(0.1, 0.01, true)]
    [InlineData(0.01, 0.001, true)]
    [InlineData(0.001, 0.0001, true)]
    [InlineData(0.1, 0.1, false)]
    [InlineData(0.01, 0.01, false)]
    [InlineData(0.001, 0.001, false)]
    [InlineData(0.01, 0.1, true)]
    [InlineData(0.001, 0.01, true)]
    [InlineData(0.0001, 0.001, true)]
    public void BigDecimalNotEquals(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate != rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate != right;
        bool actualDecimalBigDecimal = left != rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator > should return the expected result")]
    [InlineData(0, 0, false)]
    [InlineData(1, 1, false)]
    [InlineData(1, 0, true)]
    [InlineData(0, 1, false)]
    [InlineData(1.1, 1.1, false)]
    [InlineData(1.1, 0.0, true)]
    [InlineData(1.0, 0.1, true)]
    [InlineData(0.0, 1.0, false)]
    [InlineData(0.0, 0.1, false)]
    [InlineData(0.1, 1.0, false)]
    [InlineData(123.45, 123.45, false)]
    [InlineData(123.45, 45.678, true)]
    [InlineData(12.345, 45.678, false)]
    [InlineData(0.1, 0.01, true)]
    [InlineData(0.01, 0.001, true)]
    [InlineData(0.001, 0.0001, true)]
    [InlineData(0.1, 0.1, false)]
    [InlineData(0.01, 0.01, false)]
    [InlineData(0.001, 0.001, false)]
    [InlineData(0.01, 0.1, false)]
    [InlineData(0.001, 0.01, false)]
    [InlineData(0.0001, 0.001, false)]
    public void BigDecimalGreaterThan(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate > rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate > right;
        bool actualDecimalBigDecimal = left > rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator >= should return the expected result")]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, true)]
    [InlineData(0, 1, false)]
    [InlineData(1.1, 1.1, true)]
    [InlineData(1.1, 0.0, true)]
    [InlineData(1.0, 0.1, true)]
    [InlineData(0.0, 1.0, false)]
    [InlineData(0.0, 0.1, false)]
    [InlineData(0.1, 1.0, false)]
    [InlineData(123.45, 123.45, true)]
    [InlineData(123.45, 45.678, true)]
    [InlineData(12.345, 45.678, false)]
    [InlineData(0.1, 0.01, true)]
    [InlineData(0.01, 0.001, true)]
    [InlineData(0.001, 0.0001, true)]
    [InlineData(0.1, 0.1, true)]
    [InlineData(0.01, 0.01, true)]
    [InlineData(0.001, 0.001, true)]
    [InlineData(0.01, 0.1, false)]
    [InlineData(0.001, 0.01, false)]
    [InlineData(0.0001, 0.001, false)]
    public void BigDecimalGreaterThanOrEquals(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate >= rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate >= right;
        bool actualDecimalBigDecimal = left >= rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator < should return the expected result")]
    [InlineData(0, 0, false)]
    [InlineData(1, 1, false)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, true)]
    [InlineData(1.1, 1.1, false)]
    [InlineData(1.1, 0.0, false)]
    [InlineData(1.0, 0.1, false)]
    [InlineData(0.0, 1.0, true)]
    [InlineData(0.0, 0.1, true)]
    [InlineData(0.1, 1.0, true)]
    [InlineData(123.45, 123.45, false)]
    [InlineData(123.45, 45.678, false)]
    [InlineData(12.345, 45.678, true)]
    [InlineData(0.1, 0.01, false)]
    [InlineData(0.01, 0.001, false)]
    [InlineData(0.001, 0.0001, false)]
    [InlineData(0.1, 0.1, false)]
    [InlineData(0.01, 0.01, false)]
    [InlineData(0.001, 0.001, false)]
    [InlineData(0.01, 0.1, true)]
    [InlineData(0.001, 0.01, true)]
    [InlineData(0.0001, 0.001, true)]
    public void BigDecimalLessThan(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate < rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate < right;
        bool actualDecimalBigDecimal = left < rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    [Theory(DisplayName = "BigDecimal operator <= should return the expected result")]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, true)]
    [InlineData(1.1, 1.1, true)]
    [InlineData(1.1, 0.0, false)]
    [InlineData(1.0, 0.1, false)]
    [InlineData(0.0, 1.0, true)]
    [InlineData(0.0, 0.1, true)]
    [InlineData(0.1, 1.0, true)]
    [InlineData(123.45, 123.45, true)]
    [InlineData(123.45, 45.678, false)]
    [InlineData(12.345, 45.678, true)]
    [InlineData(0.1, 0.01, false)]
    [InlineData(0.01, 0.001, false)]
    [InlineData(0.001, 0.0001, false)]
    [InlineData(0.1, 0.1, true)]
    [InlineData(0.01, 0.01, true)]
    [InlineData(0.001, 0.001, true)]
    [InlineData(0.01, 0.1, true)]
    [InlineData(0.001, 0.01, true)]
    [InlineData(0.0001, 0.001, true)]
    public void BigDecimalLessThanOrEquals(decimal left, decimal right, bool expected)
    {
        // Arrange
        BigDecimal leftCandidate = left.ToBigDecimal();
        BigDecimal rightCandidate = right.ToBigDecimal();

        // Act
        bool actualBigDecimalBigDecimal = leftCandidate <= rightCandidate;
        bool actualBigDecimalDecimal = leftCandidate <= right;
        bool actualDecimalBigDecimal = left <= rightCandidate;

        // Assert
        Assert.Equal(expected, actualBigDecimalBigDecimal);
        Assert.Equal(expected, actualBigDecimalDecimal);
        Assert.Equal(expected, actualDecimalBigDecimal);
    }

    // [Theory(DisplayName = "BigDecimal.Add should return the expected result")]
    // [InlineData(0, 1, 1)]
    // [InlineData(-1, 1.1, 0.1)]
    // [InlineData(0.1, 1, 1.1)]
    // [InlineData(0.01, 1, 1.01)]
    // [InlineData(0.1, 1.1, 1.2)]
    // public void BigDecimalAdd(double left, double right, double expected)
    // {
    //     // Arrange
    //     BigDecimal leftCandidate = left.ToBigDecimal();
    //     BigDecimal rightCandidate = right.ToBigDecimal();
    //     
    //     // Act
    //     BigDecimal actual = BigDecimal.Add(leftCandidate, rightCandidate);
    //
    //     // Assert
    //     Assert.Equal(expected.ToBigDecimal(), actual);
    // }
}
