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
/// Cross-validates <see cref="Int256"/> arithmetic, bitwise, and conversion behaviour
/// against <see cref="BigInteger"/> as a peer-reviewed oracle. Each Theory runs across
/// a curated set of edge cases (zero, one, MaxValue, MinValue, powers of two, 2^k - 1
/// masks across every bit position) plus deterministically-seeded random full-width
/// pairs, so failures are reproducible by re-running with the same seed.
/// </summary>
public sealed class Int256OracleTests
{
    private const int N = Int256Width;

    [Theory(DisplayName = "Int256: (a + b) matches BigInteger reduced to 256-bit signed")]
    [Int256BinaryData]
    public void AdditionMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger expected = ReduceSigned(a + (BigInteger)b, N);
        Assert.Equal(expected, a + b);
    }

    [Theory(DisplayName = "Int256: addition is commutative")]
    [Int256BinaryData]
    public void AdditionIsCommutative(Int256 a, Int256 b)
    {
        Assert.Equal(a + b, b + a);
    }

    [Theory(DisplayName = "Int256: (a - b) matches BigInteger reduced to 256-bit signed")]
    [Int256BinaryData]
    public void SubtractionMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger expected = ReduceSigned(a - (BigInteger)b, N);
        Assert.Equal(expected, a - b);
    }

    [Theory(DisplayName = "Int256: (a * b) matches BigInteger reduced to 256-bit signed")]
    [Int256BinaryData]
    public void MultiplicationMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger expected = ReduceSigned(a * (BigInteger)b, N);
        Assert.Equal(expected, a * b);
    }

    [Theory(DisplayName = "Int256: multiplication is commutative")]
    [Int256BinaryData]
    public void MultiplicationIsCommutative(Int256 a, Int256 b)
    {
        Assert.Equal(a * b, b * a);
    }

    [Theory(DisplayName = "Int256: division and modulus match BigInteger oracle (truncating toward zero)")]
    [Int256BinaryData]
    public void DivisionAndModulusMatchOracle(Int256 a, Int256 b)
    {
        if (b == Int256.Zero) return;

        // Int256.MinValue / -1 overflows signed range — skip; covered by checked-overflow test.
        if (a == Int256.MinValue && b == Int256.NegativeOne) return;

        BigInteger expectedQuotient = a / (BigInteger)b;
        BigInteger expectedRemainder = a % (BigInteger)b;

        Assert.Equal(expectedQuotient, a / b);
        Assert.Equal(expectedRemainder, a % b);
    }

    [Theory(DisplayName = "Int256: (a / b) * b + (a % b) == a")]
    [Int256BinaryData]
    public void DivisionModulusIdentity(Int256 a, Int256 b)
    {
        if (b == Int256.Zero) return;
        if (a == Int256.MinValue && b == Int256.NegativeOne) return;

        Int256 quotient = a / b;
        Int256 remainder = a % b;
        Assert.Equal(a, quotient * b + remainder);
    }

    [Theory(DisplayName = "Int256: bitwise NOT is self-inverse")]
    [Int256BinaryData]
    public void DoubleComplementIsIdentity(Int256 a, Int256 _)
    {
        Assert.Equal(a, ~~a);
    }

    [Theory(DisplayName = "Int256: a XOR a == 0")]
    [Int256BinaryData]
    public void XorWithSelfIsZero(Int256 a, Int256 _)
    {
        Assert.Equal(Int256.Zero, a ^ a);
    }

    [Theory(DisplayName = "Int256: a AND a == a, a OR a == a")]
    [Int256BinaryData]
    public void AndOrWithSelfIsIdentity(Int256 a, Int256 _)
    {
        Assert.Equal(a, a & a);
        Assert.Equal(a, a | a);
    }

    [Theory(DisplayName = "Int256: Parse(ToString()) round-trips")]
    [Int256BinaryData]
    public void ParseToStringRoundTrip(Int256 a, Int256 _)
    {
        Assert.Equal(a, Int256.Parse(a.ToString()));
    }

    [Theory(DisplayName = "Int256: round-trips losslessly via BigInteger cast")]
    [Int256BinaryData]
    public void BigIntegerRoundTrip(Int256 a, Int256 _)
    {
        Assert.Equal(a, (Int256)(BigInteger)a);
    }

    [Theory(DisplayName = "Int256: CompareTo agrees with BigInteger comparison")]
    [Int256BinaryData]
    public void CompareToMatchesOracle(Int256 a, Int256 b)
    {
        int actual = Math.Sign(a.CompareTo(b));
        int expected = Math.Sign(((BigInteger)a).CompareTo(b));
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Int256: checked addition throws iff BigInteger result is out of signed 256-bit range")]
    [Int256BinaryData]
    public void CheckedAdditionMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger sum = a + (BigInteger)b;
        bool fits = FitsSigned(sum, N);

        if (fits)
        {
            Int256 result = checked(a + b);
            Assert.Equal(sum, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a + b));
        }
    }

    [Theory(DisplayName = "Int256: checked subtraction throws iff BigInteger result is out of signed 256-bit range")]
    [Int256BinaryData]
    public void CheckedSubtractionMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger difference = a - (BigInteger)b;
        bool fits = FitsSigned(difference, N);

        if (fits)
        {
            Int256 result = checked(a - b);
            Assert.Equal(difference, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a - b));
        }
    }

    [Theory(DisplayName = "Int256: checked multiplication throws iff BigInteger result is out of signed 256-bit range")]
    [Int256BinaryData]
    public void CheckedMultiplicationMatchesOracle(Int256 a, Int256 b)
    {
        BigInteger product = a * (BigInteger)b;
        bool fits = FitsSigned(product, N);

        if (fits)
        {
            Int256 result = checked(a * b);
            Assert.Equal(product, result);
        }
        else
        {
            Assert.Throws<OverflowException>(() => checked(a * b));
        }
    }
}
