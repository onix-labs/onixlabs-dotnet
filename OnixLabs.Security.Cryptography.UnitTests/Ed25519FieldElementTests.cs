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

namespace OnixLabs.Security.Cryptography.UnitTests;

/// <summary>
/// Verifies Ed25519 field arithmetic against <see cref="BigInteger"/> arithmetic modulo p = 2^255 - 19, an independent oracle.
/// </summary>
public sealed class Ed25519FieldElementTests
{
    private const int RandomCaseCount = 2000;

    private static readonly BigInteger P = BigInteger.Pow(2, 255) - 19;

    public static TheoryData<byte[]> EdgeCaseValues()
    {
        TheoryData<byte[]> result = new();

        foreach (byte[] value in CreateEdgeCaseValues())
            result.Add(value);

        return result;
    }

    public static TheoryData<byte[], byte[]> EdgeCasePairs()
    {
        byte[][] values = CreateEdgeCaseValues();
        TheoryData<byte[], byte[]> result = new();

        foreach (byte[] left in values)
        foreach (byte[] right in values)
            result.Add(left, right);

        return result;
    }

    /// <summary>
    /// Creates the edge-case field encodings: zero, one, the largest decodable value, p - 1, p - 19, 2^254 and a value with full limbs.
    /// </summary>
    /// <returns>Returns the edge-case field encodings.</returns>
    private static byte[][] CreateEdgeCaseValues()
    {
        byte[] zero = new byte[32];
        byte[] one = new byte[32];
        one[0] = 1;
        byte[] allOnes = new byte[32];
        allOnes.AsSpan().Fill(0xFF);
        byte[] pMinusOne = ToBytes(P - 1);
        byte[] pMinusNineteen = ToBytes(P - 19);
        byte[] twoTo254 = ToBytes(BigInteger.Pow(2, 254));
        byte[] limbBoundaries = ToBytes(BigInteger.Pow(2, 51) - 1 + (BigInteger.Pow(2, 153) - BigInteger.Pow(2, 102)));

        return [zero, one, allOnes, pMinusOne, pMinusNineteen, twoTo254, limbBoundaries];
    }

    [Theory(DisplayName = "Ed25519FieldElement multiplication should match BigInteger multiplication modulo p for edge cases")]
    [MemberData(nameof(EdgeCasePairs))]
    public void MultiplicationShouldMatchBigIntegerForEdgeCases(byte[] left, byte[] right)
    {
        // Given
        BigInteger expected = ToInteger(left) * ToInteger(right) % P;

        // When
        Ed25519FieldElement actual = Ed25519FieldElement.FromBytes(left) * Ed25519FieldElement.FromBytes(right);

        // Then
        Assert.Equal(expected, ToInteger(actual));
    }

    [Theory(DisplayName = "Ed25519FieldElement squaring should match BigInteger modulo p and self-multiplication for edge cases")]
    [MemberData(nameof(EdgeCaseValues))]
    public void SquaringShouldMatchBigIntegerForEdgeCases(byte[] value)
    {
        // Given
        Ed25519FieldElement element = Ed25519FieldElement.FromBytes(value);
        BigInteger expected = BigInteger.Pow(ToInteger(value), 2) % P;

        // When
        Ed25519FieldElement actual = Ed25519FieldElement.Square(element);

        // Then
        Assert.Equal(expected, ToInteger(actual));
        Assert.Equal(ToBytes(element * element), ToBytes(actual));
    }

    [Fact(DisplayName = "Ed25519FieldElement multiplication and squaring should match BigInteger for random and unreduced inputs")]
    public void MultiplicationAndSquaringShouldMatchBigIntegerForRandomAndUnreducedInputs()
    {
        // Given
        Random random = new(Seed: 25519);
        List<string> failures = [];

        // When
        for (int index = 0; index < RandomCaseCount; index++)
        {
            byte[] aBytes = RandomBytes(random);
            byte[] bBytes = RandomBytes(random);
            byte[] cBytes = RandomBytes(random);
            byte[] dBytes = RandomBytes(random);

            Ed25519FieldElement a = Ed25519FieldElement.FromBytes(aBytes);
            Ed25519FieldElement b = Ed25519FieldElement.FromBytes(bBytes);
            Ed25519FieldElement c = Ed25519FieldElement.FromBytes(cBytes);
            Ed25519FieldElement d = Ed25519FieldElement.FromBytes(dBytes);

            BigInteger ai = ToInteger(aBytes);
            BigInteger bi = ToInteger(bBytes);
            BigInteger ci = ToInteger(cBytes);
            BigInteger di = ToInteger(dBytes);

            // Why: sums and differences are not carried before multiplication, so these inputs exercise the widest limbs the curve formulas
            // produce, which is where a carry or overflow bug would show.
            Ed25519FieldElement sum = a + b + c + d;
            Ed25519FieldElement difference = a - b;

            Check(failures, index, "a*b", ai * bi, a * b);
            Check(failures, index, "a^2", ai * ai, Ed25519FieldElement.Square(a));
            Check(failures, index, "(a+b+c+d)*(a-b)", (ai + bi + ci + di) * (ai - bi), sum * difference);
            Check(failures, index, "(a+b+c+d)^2", BigInteger.Pow(ai + bi + ci + di, 2), Ed25519FieldElement.Square(sum));
            Check(failures, index, "(a-b)^2", BigInteger.Pow(ai - bi, 2), Ed25519FieldElement.Square(difference));
        }

        // Then
        Assert.Empty(failures);
    }

    /// <summary>
    /// Records a failure when the specified field element does not equal the specified integer modulo p.
    /// </summary>
    /// <param name="failures">The list to which failures are added.</param>
    /// <param name="index">The index of the random case.</param>
    /// <param name="operation">A description of the operation that was checked.</param>
    /// <param name="expected">The expected value, before reduction modulo p.</param>
    /// <param name="actual">The field element produced by the operation.</param>
    private static void Check(List<string> failures, int index, string operation, BigInteger expected, Ed25519FieldElement actual)
    {
        BigInteger reduced = (expected % P + P) % P;
        BigInteger actualValue = ToInteger(actual);

        if (reduced != actualValue)
            failures.Add($"case {index}: {operation} expected {reduced} but was {actualValue}");
    }

    /// <summary>
    /// Creates 32 random bytes with the most significant bit clear, which is the range <see cref="Ed25519FieldElement.FromBytes"/> decodes.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <returns>Returns 32 random bytes with the most significant bit clear.</returns>
    private static byte[] RandomBytes(Random random)
    {
        byte[] result = new byte[32];
        random.NextBytes(result);
        result[31] &= 0x7F;
        return result;
    }

    /// <summary>
    /// Converts 32 little-endian bytes, with the most significant bit ignored, to the integer that the field element decodes them as.
    /// </summary>
    /// <param name="bytes">The 32 little-endian bytes.</param>
    /// <returns>Returns the integer that the field element decodes the bytes as.</returns>
    private static BigInteger ToInteger(byte[] bytes)
    {
        byte[] copy = [..bytes];
        copy[31] &= 0x7F;
        return new BigInteger(copy, isUnsigned: true, isBigEndian: false);
    }

    /// <summary>
    /// Converts a field element to the canonical integer it represents.
    /// </summary>
    /// <param name="element">The field element.</param>
    /// <returns>Returns the canonical integer, in the range [0, p), that the field element represents.</returns>
    private static BigInteger ToInteger(Ed25519FieldElement element) => new(ToBytes(element), isUnsigned: true, isBigEndian: false);

    /// <summary>
    /// Writes the canonical encoding of a field element.
    /// </summary>
    /// <param name="element">The field element.</param>
    /// <returns>Returns the canonical 32-byte little-endian encoding of the field element.</returns>
    private static byte[] ToBytes(Ed25519FieldElement element)
    {
        byte[] result = new byte[32];
        element.WriteBytes(result);
        return result;
    }

    /// <summary>
    /// Writes a non-negative integer less than 2^256 as 32 little-endian bytes.
    /// </summary>
    /// <param name="value">The integer to write.</param>
    /// <returns>Returns the 32-byte little-endian encoding of the integer.</returns>
    private static byte[] ToBytes(BigInteger value)
    {
        byte[] result = new byte[32];
        value.TryWriteBytes(result, out int _, isUnsigned: true, isBigEndian: false);
        return result;
    }
}
