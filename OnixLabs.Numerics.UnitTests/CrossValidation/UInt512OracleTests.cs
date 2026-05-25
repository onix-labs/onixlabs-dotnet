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
/// Cross-validates <see cref="UInt512"/> arithmetic, bitwise, and conversion behaviour
/// against <see cref="BigInteger"/> as an oracle. Modular results are reduced to
/// 512-bit unsigned; checked operations are expected to throw iff the BigInteger
/// result falls outside the unsigned 512-bit range.
/// </summary>
public sealed class UInt512OracleTests
{
    private const int N = UInt512Width;

    [Theory(DisplayName = "UInt512: (a + b) matches BigInteger reduced to 512-bit unsigned")]
    [UInt512BinaryData]
    public void AdditionMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger expected = ReduceUnsigned(a + (BigInteger)b, N);
        Assert.Equal(expected, a + b);
    }

    [Theory(DisplayName = "UInt512: addition is commutative")]
    [UInt512BinaryData]
    public void AdditionIsCommutative(UInt512 a, UInt512 b)
    {
        Assert.Equal(a + b, b + a);
    }

    [Theory(DisplayName = "UInt512: (a - b) matches BigInteger reduced to 512-bit unsigned")]
    [UInt512BinaryData]
    public void SubtractionMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger expected = ReduceUnsigned(a - (BigInteger)b, N);
        Assert.Equal(expected, a - b);
    }

    [Theory(DisplayName = "UInt512: (a * b) matches BigInteger reduced to 512-bit unsigned")]
    [UInt512BinaryData]
    public void MultiplicationMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger expected = ReduceUnsigned(a * (BigInteger)b, N);
        Assert.Equal(expected, a * b);
    }

    [Theory(DisplayName = "UInt512: multiplication is commutative")]
    [UInt512BinaryData]
    public void MultiplicationIsCommutative(UInt512 a, UInt512 b)
    {
        Assert.Equal(a * b, b * a);
    }

    [Theory(DisplayName = "UInt512: division and modulus match BigInteger oracle")]
    [UInt512BinaryData]
    public void DivisionAndModulusMatchOracle(UInt512 a, UInt512 b)
    {
        if (b == UInt512.Zero) return;

        BigInteger expectedQuotient = a / (BigInteger)b;
        BigInteger expectedRemainder = a % (BigInteger)b;

        Assert.Equal(expectedQuotient, a / b);
        Assert.Equal(expectedRemainder, a % b);
    }

    [Theory(DisplayName = "UInt512: (a / b) * b + (a % b) == a")]
    [UInt512BinaryData]
    public void DivisionModulusIdentity(UInt512 a, UInt512 b)
    {
        if (b == UInt512.Zero) return;
        UInt512 quotient = a / b;
        UInt512 remainder = a % b;
        Assert.Equal(a, quotient * b + remainder);
    }

    [Theory(DisplayName = "UInt512: bitwise NOT is self-inverse")]
    [UInt512BinaryData]
    public void DoubleComplementIsIdentity(UInt512 a, UInt512 _)
    {
        Assert.Equal(a, ~~a);
    }

    [Theory(DisplayName = "UInt512: a XOR a == 0")]
    [UInt512BinaryData]
    public void XorWithSelfIsZero(UInt512 a, UInt512 _)
    {
        Assert.Equal(UInt512.Zero, a ^ a);
    }

    [Theory(DisplayName = "UInt512: a AND a == a, a OR a == a")]
    [UInt512BinaryData]
    public void AndOrWithSelfIsIdentity(UInt512 a, UInt512 _)
    {
        Assert.Equal(a, a & a);
        Assert.Equal(a, a | a);
    }

    [Theory(DisplayName = "UInt512: Parse(ToString()) round-trips")]
    [UInt512BinaryData]
    public void ParseToStringRoundTrip(UInt512 a, UInt512 _)
    {
        Assert.Equal(a, UInt512.Parse(a.ToString()));
    }

    [Theory(DisplayName = "UInt512: round-trips losslessly via BigInteger cast")]
    [UInt512BinaryData]
    public void BigIntegerRoundTrip(UInt512 a, UInt512 _)
    {
        Assert.Equal(a, (UInt512)(BigInteger)a);
    }

    [Theory(DisplayName = "UInt512: CompareTo agrees with BigInteger comparison")]
    [UInt512BinaryData]
    public void CompareToMatchesOracle(UInt512 a, UInt512 b)
    {
        int actual = Math.Sign(a.CompareTo(b));
        int expected = Math.Sign(((BigInteger)a).CompareTo(b));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "UInt512: checked addition throws iff BigInteger result is out of unsigned 512-bit range")]
    [UInt512BinaryData]
    public void CheckedAdditionMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger sum = a + (BigInteger)b;
        bool fits = FitsUnsigned(sum, N);

        if (fits)
        {
            UInt512 result = checked(a + b);
            Assert.Equal(sum, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a + b));
        }
    }

    [Theory(DisplayName = "UInt512: checked subtraction throws iff BigInteger result is out of unsigned 512-bit range")]
    [UInt512BinaryData]
    public void CheckedSubtractionMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger difference = a - (BigInteger)b;
        bool fits = FitsUnsigned(difference, N);

        if (fits)
        {
            UInt512 result = checked(a - b);
            Assert.Equal(difference, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a - b));
        }
    }

    [Theory(DisplayName = "UInt512: checked multiplication throws iff BigInteger result is out of unsigned 512-bit range")]
    [UInt512BinaryData]
    public void CheckedMultiplicationMatchesOracle(UInt512 a, UInt512 b)
    {
        BigInteger product = a * (BigInteger)b;
        bool fits = FitsUnsigned(product, N);

        if (fits)
        {
            UInt512 result = checked(a * b);
            Assert.Equal(product, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a * b));
        }
    }
}
