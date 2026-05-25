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
/// Cross-validates <see cref="UInt256"/> arithmetic, bitwise, and conversion behaviour
/// against <see cref="BigInteger"/> as an oracle. Modular results are reduced to
/// 256-bit unsigned; checked operations are expected to throw iff the BigInteger
/// result falls outside the unsigned 256-bit range.
/// </summary>
public sealed class UInt256OracleTests
{
    private const int N = UInt256Width;

    [Theory(DisplayName = "UInt256: (a + b) matches BigInteger reduced to 256-bit unsigned")]
    [UInt256BinaryData]
    public void AdditionMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger expected = ReduceUnsigned(a + (BigInteger)b, N);
        Assert.Equal(expected, a + b);
    }

    [Theory(DisplayName = "UInt256: addition is commutative")]
    [UInt256BinaryData]
    public void AdditionIsCommutative(UInt256 a, UInt256 b)
    {
        Assert.Equal(a + b, b + a);
    }

    [Theory(DisplayName = "UInt256: (a - b) matches BigInteger reduced to 256-bit unsigned")]
    [UInt256BinaryData]
    public void SubtractionMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger expected = ReduceUnsigned(a - (BigInteger)b, N);
        Assert.Equal(expected, a - b);
    }

    [Theory(DisplayName = "UInt256: (a * b) matches BigInteger reduced to 256-bit unsigned")]
    [UInt256BinaryData]
    public void MultiplicationMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger expected = ReduceUnsigned(a * (BigInteger)b, N);
        Assert.Equal(expected, a * b);
    }

    [Theory(DisplayName = "UInt256: multiplication is commutative")]
    [UInt256BinaryData]
    public void MultiplicationIsCommutative(UInt256 a, UInt256 b)
    {
        Assert.Equal(a * b, b * a);
    }

    [Theory(DisplayName = "UInt256: division and modulus match BigInteger oracle")]
    [UInt256BinaryData]
    public void DivisionAndModulusMatchOracle(UInt256 a, UInt256 b)
    {
        if (b == UInt256.Zero) return;

        BigInteger expectedQuotient = a / (BigInteger)b;
        BigInteger expectedRemainder = a % (BigInteger)b;

        Assert.Equal(expectedQuotient, a / b);
        Assert.Equal(expectedRemainder, a % b);
    }

    [Theory(DisplayName = "UInt256: (a / b) * b + (a % b) == a")]
    [UInt256BinaryData]
    public void DivisionModulusIdentity(UInt256 a, UInt256 b)
    {
        if (b == UInt256.Zero) return;
        UInt256 quotient = a / b;
        UInt256 remainder = a % b;
        Assert.Equal(a, quotient * b + remainder);
    }

    [Theory(DisplayName = "UInt256: bitwise NOT is self-inverse")]
    [UInt256BinaryData]
    public void DoubleComplementIsIdentity(UInt256 a, UInt256 _)
    {
        Assert.Equal(a, ~~a);
    }

    [Theory(DisplayName = "UInt256: a XOR a == 0")]
    [UInt256BinaryData]
    public void XorWithSelfIsZero(UInt256 a, UInt256 _)
    {
        Assert.Equal(UInt256.Zero, a ^ a);
    }

    [Theory(DisplayName = "UInt256: a AND a == a, a OR a == a")]
    [UInt256BinaryData]
    public void AndOrWithSelfIsIdentity(UInt256 a, UInt256 _)
    {
        Assert.Equal(a, a & a);
        Assert.Equal(a, a | a);
    }

    [Theory(DisplayName = "UInt256: Parse(ToString()) round-trips")]
    [UInt256BinaryData]
    public void ParseToStringRoundTrip(UInt256 a, UInt256 _)
    {
        Assert.Equal(a, UInt256.Parse(a.ToString()));
    }

    [Theory(DisplayName = "UInt256: round-trips losslessly via BigInteger cast")]
    [UInt256BinaryData]
    public void BigIntegerRoundTrip(UInt256 a, UInt256 _)
    {
        Assert.Equal(a, (UInt256)(BigInteger)a);
    }

    [Theory(DisplayName = "UInt256: CompareTo agrees with BigInteger comparison")]
    [UInt256BinaryData]
    public void CompareToMatchesOracle(UInt256 a, UInt256 b)
    {
        int actual = Math.Sign(a.CompareTo(b));
        int expected = Math.Sign(((BigInteger)a).CompareTo(b));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "UInt256: checked addition throws iff BigInteger result is out of unsigned 256-bit range")]
    [UInt256BinaryData]
    public void CheckedAdditionMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger sum = a + (BigInteger)b;
        bool fits = FitsUnsigned(sum, N);

        if (fits)
        {
            UInt256 result = checked(a + b);
            Assert.Equal(sum, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a + b));
        }
    }

    [Theory(DisplayName = "UInt256: checked subtraction throws iff BigInteger result is out of unsigned 256-bit range")]
    [UInt256BinaryData]
    public void CheckedSubtractionMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger difference = a - (BigInteger)b;
        bool fits = FitsUnsigned(difference, N);

        if (fits)
        {
            UInt256 result = checked(a - b);
            Assert.Equal(difference, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a - b));
        }
    }

    [Theory(DisplayName = "UInt256: checked multiplication throws iff BigInteger result is out of unsigned 256-bit range")]
    [UInt256BinaryData]
    public void CheckedMultiplicationMatchesOracle(UInt256 a, UInt256 b)
    {
        BigInteger product = a * (BigInteger)b;
        bool fits = FitsUnsigned(product, N);

        if (fits)
        {
            UInt256 result = checked(a * b);
            Assert.Equal(product, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a * b));
        }
    }
}
