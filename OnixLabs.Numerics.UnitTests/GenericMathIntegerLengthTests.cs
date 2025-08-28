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

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathIntegerLengthTests
{
    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (SByte)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(-1, 1)]
    [InlineData(-10, 2)]
    [InlineData(-100, 3)]
    [InlineData(sbyte.MinValue, 3)]
    [InlineData(sbyte.MaxValue, 3)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultSByte(sbyte value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (Byte)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(byte.MinValue, 1)]
    [InlineData(byte.MaxValue, 3)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultByte(byte value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (Int16)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(-1, 1)]
    [InlineData(-10, 2)]
    [InlineData(-100, 3)]
    [InlineData(-1000, 4)]
    [InlineData(-10000, 5)]
    [InlineData(short.MinValue, 5)]
    [InlineData(short.MaxValue, 5)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultInt16(short value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (UInt16)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(ushort.MinValue, 1)]
    [InlineData(ushort.MaxValue, 5)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultUInt16(ushort value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (Int32)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(100000, 6)]
    [InlineData(1000000, 7)]
    [InlineData(10000000, 8)]
    [InlineData(100000000, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(-1, 1)]
    [InlineData(-10, 2)]
    [InlineData(-100, 3)]
    [InlineData(-1000, 4)]
    [InlineData(-10000, 5)]
    [InlineData(-100000, 6)]
    [InlineData(-1000000, 7)]
    [InlineData(-10000000, 8)]
    [InlineData(-100000000, 9)]
    [InlineData(-1000000000, 10)]
    [InlineData(int.MinValue, 10)]
    [InlineData(int.MaxValue, 10)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultInt32(int value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (UInt32)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(100000, 6)]
    [InlineData(1000000, 7)]
    [InlineData(10000000, 8)]
    [InlineData(100000000, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(uint.MinValue, 1)]
    [InlineData(uint.MaxValue, 10)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultUInt32(uint value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (Int64)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(100000, 6)]
    [InlineData(1000000, 7)]
    [InlineData(10000000, 8)]
    [InlineData(100000000, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(10000000000, 11)]
    [InlineData(100000000000, 12)]
    [InlineData(1000000000000, 13)]
    [InlineData(10000000000000, 14)]
    [InlineData(100000000000000, 15)]
    [InlineData(1000000000000000, 16)]
    [InlineData(10000000000000000, 17)]
    [InlineData(100000000000000000, 18)]
    [InlineData(1000000000000000000, 19)]
    [InlineData(-1, 1)]
    [InlineData(-10, 2)]
    [InlineData(-100, 3)]
    [InlineData(-1000, 4)]
    [InlineData(-10000, 5)]
    [InlineData(-100000, 6)]
    [InlineData(-1000000, 7)]
    [InlineData(-10000000, 8)]
    [InlineData(-100000000, 9)]
    [InlineData(-1000000000, 10)]
    [InlineData(-10000000000, 11)]
    [InlineData(-100000000000, 12)]
    [InlineData(-1000000000000, 13)]
    [InlineData(-10000000000000, 14)]
    [InlineData(-100000000000000, 15)]
    [InlineData(-1000000000000000, 16)]
    [InlineData(-10000000000000000, 17)]
    [InlineData(-100000000000000000, 18)]
    [InlineData(-1000000000000000000, 19)]
    [InlineData(long.MinValue, 19)]
    [InlineData(long.MaxValue, 19)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultInt64(long value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (UInt64)")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(10, 2)]
    [InlineData(100, 3)]
    [InlineData(1000, 4)]
    [InlineData(10000, 5)]
    [InlineData(100000, 6)]
    [InlineData(1000000, 7)]
    [InlineData(10000000, 8)]
    [InlineData(100000000, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(10000000000, 11)]
    [InlineData(100000000000, 12)]
    [InlineData(1000000000000, 13)]
    [InlineData(10000000000000, 14)]
    [InlineData(100000000000000, 15)]
    [InlineData(1000000000000000, 16)]
    [InlineData(10000000000000000, 17)]
    [InlineData(100000000000000000, 18)]
    [InlineData(1000000000000000000, 19)]
    [InlineData(10000000000000000000, 20)]
    [InlineData(ulong.MinValue, 1)]
    [InlineData(ulong.MaxValue, 20)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultUInt64(ulong value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "GenericMath.IntegerLength should produce the expected result (Double)")]
    [InlineData(0, 1)]
    [InlineData(1.1, 1)]
    [InlineData(10.1, 2)]
    [InlineData(100.1, 3)]
    [InlineData(1000.1, 4)]
    [InlineData(10000.1, 5)]
    [InlineData(100000.1, 6)]
    [InlineData(1000000.1, 7)]
    [InlineData(10000000.1, 8)]
    [InlineData(100000000.1, 9)]
    [InlineData(1000000000.1, 10)]
    [InlineData(10000000000.1, 11)]
    [InlineData(100000000000.1, 12)]
    [InlineData(1000000000000.1, 13)]
    [InlineData(10000000000000.1, 14)]
    [InlineData(100000000000000.1, 15)]
    [InlineData(1000000000000000.1, 16)]
    [InlineData(10000000000000000.1, 17)]
    [InlineData(100000000000000000.1, 18)]
    [InlineData(1000000000000000000.1, 19)]
    [InlineData(10000000000000000000.1, 20)]
    [InlineData(double.MinValue, 309)]
    [InlineData(double.MaxValue, 309)]
    public void GenericMathIntegerLengthShouldProduceExpectedResultDouble(double value, int expected)
    {
        // When
        int actual = GenericMath.IntegerLength(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
