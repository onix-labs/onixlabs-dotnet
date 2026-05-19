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

using System;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data.CrossValidation;
using static OnixLabs.Numerics.UnitTests.Data.CrossValidation.BigIntegerOracle;

namespace OnixLabs.Numerics.UnitTests.CrossValidation;

/// <summary>
/// Cross-validates <see cref="Int512"/> arithmetic, bitwise, and conversion behaviour
/// against <see cref="BigInteger"/> as an oracle. Modular results are reduced to
/// 512-bit two's-complement; checked operations are expected to throw iff the
/// BigInteger result falls outside the signed 512-bit range.
/// </summary>
public sealed class Int512OracleTests
{
    private const int N = Int512Width;

    [Theory(DisplayName = "Int512: (a + b) matches BigInteger reduced to 512-bit signed")]
    [Int512BinaryData]
    public void AdditionMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger expected = ReduceSigned(a + (BigInteger)b, N);
        Assert.Equal(expected, a + b);
    }

    [Theory(DisplayName = "Int512: addition is commutative")]
    [Int512BinaryData]
    public void AdditionIsCommutative(Int512 a, Int512 b)
    {
        Assert.Equal(a + b, b + a);
    }

    [Theory(DisplayName = "Int512: (a - b) matches BigInteger reduced to 512-bit signed")]
    [Int512BinaryData]
    public void SubtractionMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger expected = ReduceSigned(a - (BigInteger)b, N);
        Assert.Equal(expected, a - b);
    }

    [Theory(DisplayName = "Int512: (a * b) matches BigInteger reduced to 512-bit signed")]
    [Int512BinaryData]
    public void MultiplicationMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger expected = ReduceSigned(a * (BigInteger)b, N);
        Assert.Equal(expected, a * b);
    }

    [Theory(DisplayName = "Int512: multiplication is commutative")]
    [Int512BinaryData]
    public void MultiplicationIsCommutative(Int512 a, Int512 b)
    {
        Assert.Equal(a * b, b * a);
    }

    [Theory(DisplayName = "Int512: division and modulus match BigInteger oracle (truncating toward zero)")]
    [Int512BinaryData]
    public void DivisionAndModulusMatchOracle(Int512 a, Int512 b)
    {
        if (b == Int512.Zero) return;
        if (a == Int512.MinValue && b == Int512.NegativeOne) return;

        BigInteger expectedQuotient = a / (BigInteger)b;
        BigInteger expectedRemainder = a % (BigInteger)b;

        Assert.Equal(expectedQuotient, a / b);
        Assert.Equal(expectedRemainder, a % b);
    }

    [Theory(DisplayName = "Int512: (a / b) * b + (a % b) == a")]
    [Int512BinaryData]
    public void DivisionModulusIdentity(Int512 a, Int512 b)
    {
        if (b == Int512.Zero) return;
        if (a == Int512.MinValue && b == Int512.NegativeOne) return;

        Int512 quotient = a / b;
        Int512 remainder = a % b;
        Assert.Equal(a, quotient * b + remainder);
    }

    [Theory(DisplayName = "Int512: bitwise NOT is self-inverse")]
    [Int512BinaryData]
    public void DoubleComplementIsIdentity(Int512 a, Int512 _)
    {
        Assert.Equal(a, ~~a);
    }

    [Theory(DisplayName = "Int512: a XOR a == 0")]
    [Int512BinaryData]
    public void XorWithSelfIsZero(Int512 a, Int512 _)
    {
        Assert.Equal(Int512.Zero, a ^ a);
    }

    [Theory(DisplayName = "Int512: a AND a == a, a OR a == a")]
    [Int512BinaryData]
    public void AndOrWithSelfIsIdentity(Int512 a, Int512 _)
    {
        Assert.Equal(a, a & a);
        Assert.Equal(a, a | a);
    }

    [Theory(DisplayName = "Int512: Parse(ToString()) round-trips")]
    [Int512BinaryData]
    public void ParseToStringRoundTrip(Int512 a, Int512 _)
    {
        Assert.Equal(a, Int512.Parse(a.ToString()));
    }

    [Theory(DisplayName = "Int512: round-trips losslessly via BigInteger cast")]
    [Int512BinaryData]
    public void BigIntegerRoundTrip(Int512 a, Int512 _)
    {
        Assert.Equal(a, (Int512)(BigInteger)a);
    }

    [Theory(DisplayName = "Int512: CompareTo agrees with BigInteger comparison")]
    [Int512BinaryData]
    public void CompareToMatchesOracle(Int512 a, Int512 b)
    {
        int actual = Math.Sign(a.CompareTo(b));
        int expected = Math.Sign(((BigInteger)a).CompareTo(b));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Int512: checked addition throws iff BigInteger result is out of signed 512-bit range")]
    [Int512BinaryData]
    public void CheckedAdditionMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger sum = a + (BigInteger)b;
        bool fits = FitsSigned(sum, N);

        if (fits)
        {
            Int512 result = checked(a + b);
            Assert.Equal(sum, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a + b));
        }
    }

    [Theory(DisplayName = "Int512: checked subtraction throws iff BigInteger result is out of signed 512-bit range")]
    [Int512BinaryData]
    public void CheckedSubtractionMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger difference = a - (BigInteger)b;
        bool fits = FitsSigned(difference, N);

        if (fits)
        {
            Int512 result = checked(a - b);
            Assert.Equal(difference, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a - b));
        }
    }

    [Theory(DisplayName = "Int512: checked multiplication throws iff BigInteger result is out of signed 512-bit range")]
    [Int512BinaryData]
    public void CheckedMultiplicationMatchesOracle(Int512 a, Int512 b)
    {
        BigInteger product = a * (BigInteger)b;
        bool fits = FitsSigned(product, N);

        if (fits)
        {
            Int512 result = checked(a * b);
            Assert.Equal(product, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a * b));
        }
    }
}
