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
using System.Collections.Generic;
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

/// <summary>
/// Verifies 256-bit and 512-bit division against <see cref="BigInteger"/>, an independent oracle, across structured random operands that
/// exercise every path: divisors that fit in a quarter, a half or the whole width, and the patterns where quotient-digit estimates need
/// correcting, such as all-ones values, powers of two, and dividends whose upper half equals or exceeds the divisor.
/// </summary>
public sealed class WideIntegerDivisionOracleTests
{
    private const int CaseCount = 100_000;

    [Fact(DisplayName = "UInt256.DivRem should match BigInteger for structured random operands")]
    public void UInt256DivRemShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 256);
        List<string> failures = [];

        // When
        for (int index = 0; index < CaseCount && failures.Count < 10; index++)
        {
            BigInteger dividend = CreateOperand(random, bitWidth: 256);
            BigInteger divisor = CreateOperand(random, bitWidth: 256);

            if (divisor.IsZero) continue;

            (UInt256 quotient, UInt256 remainder) = UInt256.DivRem((UInt256)dividend, (UInt256)divisor);
            BigInteger expectedQuotient = BigInteger.DivRem(dividend, divisor, out BigInteger expectedRemainder);

            if ((BigInteger)quotient != expectedQuotient || (BigInteger)remainder != expectedRemainder)
                failures.Add($"{dividend} / {divisor}: expected ({expectedQuotient}, {expectedRemainder}) but was ({(BigInteger)quotient}, {(BigInteger)remainder})");
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "UInt256.DivRemBy128 should match BigInteger for the low quotient half and the remainder, including upper halves at or above the divisor")]
    public void UInt256DivRemBy128ShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 128);
        BigInteger lowMask = (BigInteger.One << 128) - 1;
        List<string> failures = [];

        // When
        for (int index = 0; index < CaseCount && failures.Count < 10; index++)
        {
            BigInteger divisor = CreateOperand(random, bitWidth: 128);

            if (divisor.IsZero) continue;

            BigInteger dividend = CreateDividendForHalfWidthDivisor(random, divisor, halfWidth: 128);
            UInt128 quotient = UInt256.DivRemBy128((UInt256)dividend, (UInt128)divisor, out UInt128 remainder);
            BigInteger expectedQuotient = BigInteger.DivRem(dividend, divisor, out BigInteger expectedRemainder) & lowMask;

            if (quotient != (UInt128)expectedQuotient || remainder != (UInt128)expectedRemainder)
                failures.Add($"{dividend} / {divisor}: expected ({expectedQuotient}, {expectedRemainder}) but was ({quotient}, {remainder})");
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "UInt512.DivRem should match BigInteger for structured random operands")]
    public void UInt512DivRemShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 512);
        List<string> failures = [];

        // When
        for (int index = 0; index < CaseCount / 2 && failures.Count < 10; index++)
        {
            BigInteger dividend = CreateOperand(random, bitWidth: 512);
            BigInteger divisor = CreateOperand(random, bitWidth: 512);

            if (divisor.IsZero) continue;

            (UInt512 quotient, UInt512 remainder) = UInt512.DivRem((UInt512)dividend, (UInt512)divisor);
            BigInteger expectedQuotient = BigInteger.DivRem(dividend, divisor, out BigInteger expectedRemainder);

            if ((BigInteger)quotient != expectedQuotient || (BigInteger)remainder != expectedRemainder)
                failures.Add($"{dividend} / {divisor}: expected ({expectedQuotient}, {expectedRemainder}) but was ({(BigInteger)quotient}, {(BigInteger)remainder})");
        }

        // Then
        Assert.Empty(failures);
    }

    [Fact(DisplayName = "UInt512.DivRemBy256 should match BigInteger for the low quotient half and the remainder, including upper halves at or above the divisor")]
    public void UInt512DivRemBy256ShouldMatchBigInteger()
    {
        // Given
        Random random = new(Seed: 2560);
        BigInteger lowMask = (BigInteger.One << 256) - 1;
        List<string> failures = [];

        // When
        for (int index = 0; index < CaseCount / 2 && failures.Count < 10; index++)
        {
            BigInteger divisor = CreateOperand(random, bitWidth: 256);

            if (divisor.IsZero) continue;

            BigInteger dividend = CreateDividendForHalfWidthDivisor(random, divisor, halfWidth: 256);
            UInt256 quotient = UInt512.DivRemBy256((UInt512)dividend, (UInt256)divisor, out UInt256 remainder);
            BigInteger expectedQuotient = BigInteger.DivRem(dividend, divisor, out BigInteger expectedRemainder) & lowMask;

            if ((BigInteger)quotient != expectedQuotient || (BigInteger)remainder != expectedRemainder)
                failures.Add($"{dividend} / {divisor}: expected ({expectedQuotient}, {expectedRemainder}) but was ({(BigInteger)quotient}, {(BigInteger)remainder})");
        }

        // Then
        Assert.Empty(failures);
    }

    /// <summary>
    /// Creates a non-negative operand of at most the specified width, from a random bit length and one of several patterns that stress
    /// quotient estimation: random bits, all ones, a power of two, a power of two plus or minus one, or a top bit followed by zeros and
    /// random low bits.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <param name="bitWidth">The maximum width of the operand, in bits.</param>
    /// <returns>Returns a non-negative operand of at most <paramref name="bitWidth"/> bits.</returns>
    private static BigInteger CreateOperand(Random random, int bitWidth)
    {
        int length = random.Next(4) switch
        {
            0 => random.Next(1, 65),
            1 => random.Next(bitWidth / 2 - 8, bitWidth / 2 + 9),
            2 => random.Next(bitWidth - 8, bitWidth + 1),
            _ => random.Next(1, bitWidth + 1)
        };

        BigInteger power = BigInteger.One << (length - 1);

        return random.Next(6) switch
        {
            0 => (BigInteger.One << length) - 1,
            1 => power,
            2 => power + 1,
            3 => length > 1 ? power - 1 : BigInteger.One,
            4 => power | CreateRandomBits(random, length / 2),
            _ => power | CreateRandomBits(random, length - 1)
        };
    }

    /// <summary>
    /// Creates a dividend for a division by a half-width divisor, whose upper half is sometimes just below the divisor, sometimes equal to
    /// it and sometimes above it, and otherwise random.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <param name="divisor">The divisor.</param>
    /// <param name="halfWidth">The width of the divisor and of each half of the dividend, in bits.</param>
    /// <returns>Returns a dividend of at most twice <paramref name="halfWidth"/> bits.</returns>
    private static BigInteger CreateDividendForHalfWidthDivisor(Random random, BigInteger divisor, int halfWidth)
    {
        BigInteger halfMask = (BigInteger.One << halfWidth) - 1;
        BigInteger lower = random.Next(3) is 0 ? halfMask : CreateRandomBits(random, halfWidth);

        BigInteger upper = random.Next(5) switch
        {
            0 => divisor - 1,
            1 => divisor,
            2 => halfMask,
            3 => CreateRandomBits(random, halfWidth) % divisor,
            _ => CreateRandomBits(random, halfWidth)
        };

        return (upper << halfWidth) | lower;
    }

    /// <summary>
    /// Creates a non-negative random integer of at most the specified number of bits.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <param name="bitCount">The maximum number of bits.</param>
    /// <returns>Returns a non-negative random integer of at most <paramref name="bitCount"/> bits.</returns>
    private static BigInteger CreateRandomBits(Random random, int bitCount)
    {
        if (bitCount <= 0) return BigInteger.Zero;

        byte[] bytes = new byte[(bitCount + 7) / 8 + 1];
        random.NextBytes(bytes);
        bytes[^1] = 0;

        return new BigInteger(bytes) & ((BigInteger.One << bitCount) - 1);
    }
}
