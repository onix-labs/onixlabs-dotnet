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
using System.Globalization;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data.CrossValidation;
using static OnixLabs.Numerics.UnitTests.Data.CrossValidation.BigDecimalRationalOracle;

namespace OnixLabs.Numerics.UnitTests.CrossValidation;

/// <summary>
/// Cross-validates <see cref="BigDecimal"/> arithmetic against an exact <see cref="BigInteger"/> rational oracle,
/// and asserts algebraic invariants that must hold for any exact decimal type. Inputs span curated near-boundary
/// values (signed zeros, units, powers of ten, all-nines masks across many scales) plus deterministically-seeded
/// random unscaled-value/scale pairs — covering the arbitrary-precision and arbitrary-scale space that the
/// <see cref="decimal"/>-bounded brute-force provider cannot reach. Value equality is asserted via the semantic
/// <c>==</c> operator (scale-insensitive), since <c>BigDecimal</c> arithmetic is exact and never rounds.
/// </summary>
public sealed class BigDecimalOracleTests
{
    private static readonly MidpointRounding[] RoundingModes =
    [
        MidpointRounding.ToEven,
        MidpointRounding.AwayFromZero,
        MidpointRounding.ToZero,
        MidpointRounding.ToPositiveInfinity,
        MidpointRounding.ToNegativeInfinity,
    ];

    [Theory(DisplayName = "BigDecimal: (a + b) matches the exact rational oracle")]
    [BigDecimalBinaryData]
    public void AdditionMatchesOracle(BigDecimal a, BigDecimal b)
    {
        (BigInteger numeratorA, BigInteger denominatorA) = ToRational(a);
        (BigInteger numeratorB, BigInteger denominatorB) = ToRational(b);
        Assert.True(ValueEquals(a + b, numeratorA * denominatorB + numeratorB * denominatorA, denominatorA * denominatorB));
    }

    [Theory(DisplayName = "BigDecimal: (a - b) matches the exact rational oracle")]
    [BigDecimalBinaryData]
    public void SubtractionMatchesOracle(BigDecimal a, BigDecimal b)
    {
        (BigInteger numeratorA, BigInteger denominatorA) = ToRational(a);
        (BigInteger numeratorB, BigInteger denominatorB) = ToRational(b);
        Assert.True(ValueEquals(a - b, numeratorA * denominatorB - numeratorB * denominatorA, denominatorA * denominatorB));
    }

    [Theory(DisplayName = "BigDecimal: (a * b) matches the exact rational oracle")]
    [BigDecimalBinaryData]
    public void MultiplicationMatchesOracle(BigDecimal a, BigDecimal b)
    {
        (BigInteger numeratorA, BigInteger denominatorA) = ToRational(a);
        (BigInteger numeratorB, BigInteger denominatorB) = ToRational(b);
        Assert.True(ValueEquals(a * b, numeratorA * numeratorB, denominatorA * denominatorB));
    }

    [Theory(DisplayName = "BigDecimal: Divide matches the exact rational oracle, rounded to the dividend scale, across all rounding modes")]
    [BigDecimalBinaryData]
    public void DivisionMatchesOracle(BigDecimal a, BigDecimal b)
    {
        if (b == BigDecimal.Zero) return;

        BigInteger dividend = a.UnscaledValue * BigInteger.Pow(10, b.Scale);
        BigInteger divisor = b.UnscaledValue;
        BigInteger resultDenominator = BigInteger.Pow(10, a.Scale);

        foreach (MidpointRounding mode in RoundingModes)
        {
            BigInteger expectedQuotient = RoundedQuotient(dividend, divisor, mode);
            BigDecimal actual = BigDecimal.Divide(a, b, mode);
            Assert.True(ValueEquals(actual, expectedQuotient, resultDenominator), $"Divide({a}, {b}, {mode})");
        }
    }

    [Theory(DisplayName = "BigDecimal: a / a equals 1 for non-zero a")]
    [BigDecimalUnaryData]
    public void DivisionBySelfIsOne(BigDecimal a)
    {
        if (a == BigDecimal.Zero) return;
        Assert.True(a / a == BigDecimal.One);
    }

    [Theory(DisplayName = "BigDecimal: a / 1 equals a")]
    [BigDecimalUnaryData]
    public void DivisionByOneIsIdentity(BigDecimal a) => Assert.True(a / BigDecimal.One == a);

    [Theory(DisplayName = "BigDecimal: addition is commutative")]
    [BigDecimalBinaryData]
    public void AdditionIsCommutative(BigDecimal a, BigDecimal b) => Assert.True(a + b == b + a);

    [Theory(DisplayName = "BigDecimal: multiplication is commutative")]
    [BigDecimalBinaryData]
    public void MultiplicationIsCommutative(BigDecimal a, BigDecimal b) => Assert.True(a * b == b * a);

    [Theory(DisplayName = "BigDecimal: (a + b) * (a - b) equals a² - b²")]
    [BigDecimalBinaryData]
    public void DifferenceOfSquaresHolds(BigDecimal a, BigDecimal b) => Assert.True((a + b) * (a - b) == a * a - b * b);

    [Theory(DisplayName = "BigDecimal: a + 0 equals a")]
    [BigDecimalUnaryData]
    public void AdditiveIdentityHolds(BigDecimal a) => Assert.True(a + BigDecimal.Zero == a);

    [Theory(DisplayName = "BigDecimal: a * 1 equals a")]
    [BigDecimalUnaryData]
    public void MultiplicativeIdentityHolds(BigDecimal a) => Assert.True(a * BigDecimal.One == a);

    [Theory(DisplayName = "BigDecimal: a * 0 equals 0")]
    [BigDecimalUnaryData]
    public void MultiplicationByZeroIsZero(BigDecimal a) => Assert.True(a * BigDecimal.Zero == BigDecimal.Zero);

    [Theory(DisplayName = "BigDecimal: a + (-a) equals 0")]
    [BigDecimalUnaryData]
    public void AdditiveInverseHolds(BigDecimal a) => Assert.True(a + -a == BigDecimal.Zero);

    [Theory(DisplayName = "BigDecimal: -(-a) equals a")]
    [BigDecimalUnaryData]
    public void DoubleNegationHolds(BigDecimal a) => Assert.True(-(-a) == a);

    [Theory(DisplayName = "BigDecimal: Abs equals the value with its sign cleared")]
    [BigDecimalUnaryData]
    public void AbsMatchesSignedValue(BigDecimal a)
    {
        BigDecimal expected = a.UnscaledValue.Sign < 0 ? -a : a;
        Assert.True(BigDecimal.Abs(a) == expected);
        Assert.True(BigDecimal.Abs(a) >= BigDecimal.Zero);
    }

    [Theory(DisplayName = "BigDecimal: Abs(-a) equals Abs(a)")]
    [BigDecimalUnaryData]
    public void AbsOfNegationEqualsAbs(BigDecimal a) => Assert.True(BigDecimal.Abs(-a) == BigDecimal.Abs(a));

    [Theory(DisplayName = "BigDecimal: Parse(ToString(x)) round-trips the value across arbitrary precision and scale")]
    [BigDecimalUnaryData]
    public void StringRoundTripPreservesValue(BigDecimal a)
    {
        string text = a.ToString("G", CultureInfo.InvariantCulture);
        BigDecimal parsed = BigDecimal.Parse(text, CultureInfo.InvariantCulture);
        Assert.True(a == parsed, $"round-trip failed for \"{text}\"");
    }
}
