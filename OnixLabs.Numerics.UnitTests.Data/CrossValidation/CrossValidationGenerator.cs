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

using System.Runtime.InteropServices;

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Generates input values for cross-validation tests of the extended-width
/// integer types. The distribution mixes hand-picked edge cases (boundaries,
/// powers of two, all-ones masks) with uniformly-random full-width values
/// drawn from a seeded RNG so failures are reproducible.
/// </summary>
public static class CrossValidationGenerator
{
    public static IEnumerable<Int256> GenerateInt256(int randomCount, int seed)
    {
        // Boundary constants — every run sees these.
        yield return Int256.Zero;
        yield return Int256.One;
        yield return Int256.NegativeOne;
        yield return Int256.MaxValue;
        yield return Int256.MinValue;
        yield return Int256.MaxValue - Int256.One;
        yield return Int256.MinValue + Int256.One;

        // Single-bit values and 2^k - 1 masks across the full bit range.
        // These exercise carry/borrow at every word boundary.
        for (int k = 0; k < 256; k++)
        {
            Int256 powerOfTwo = Int256.One << k;
            yield return powerOfTwo;
            yield return powerOfTwo - Int256.One;
            yield return -powerOfTwo;
        }

        // Uniform random full-range values.
        Random rng = new(seed);
        for (int i = 0; i < randomCount; i++)
            yield return RandomInt256(rng);
    }

    private static Int256 RandomInt256(Random rng)
    {
        Span<byte> buffer = stackalloc byte[32];
        rng.NextBytes(buffer);
        UInt128 upper = MemoryMarshal.Read<UInt128>(buffer[..16]);
        UInt128 lower = MemoryMarshal.Read<UInt128>(buffer[16..]);
        return new Int256(upper, lower);
    }

    public static IEnumerable<UInt256> GenerateUInt256(int randomCount, int seed)
    {
        yield return UInt256.Zero;
        yield return UInt256.One;
        yield return UInt256.MaxValue;
        yield return UInt256.MaxValue - UInt256.One;

        for (int k = 0; k < 256; k++)
        {
            UInt256 powerOfTwo = UInt256.One << k;
            yield return powerOfTwo;
            yield return powerOfTwo - UInt256.One;
        }

        Random rng = new(seed);
        for (int i = 0; i < randomCount; i++)
            yield return RandomUInt256(rng);
    }

    private static UInt256 RandomUInt256(Random rng)
    {
        Span<byte> buffer = stackalloc byte[32];
        rng.NextBytes(buffer);
        UInt128 upper = MemoryMarshal.Read<UInt128>(buffer[..16]);
        UInt128 lower = MemoryMarshal.Read<UInt128>(buffer[16..]);
        return new UInt256(upper, lower);
    }

    public static IEnumerable<Int512> GenerateInt512(int randomCount, int seed)
    {
        yield return Int512.Zero;
        yield return Int512.One;
        yield return Int512.NegativeOne;
        yield return Int512.MaxValue;
        yield return Int512.MinValue;
        yield return Int512.MaxValue - Int512.One;
        yield return Int512.MinValue + Int512.One;

        for (int k = 0; k < 512; k++)
        {
            Int512 powerOfTwo = Int512.One << k;
            yield return powerOfTwo;
            yield return powerOfTwo - Int512.One;
            yield return -powerOfTwo;
        }

        Random rng = new(seed);
        for (int i = 0; i < randomCount; i++)
            yield return RandomInt512(rng);
    }

    private static Int512 RandomInt512(Random rng)
    {
        UInt256 upper = RandomUInt256(rng);
        UInt256 lower = RandomUInt256(rng);
        return new Int512(upper, lower);
    }

    public static IEnumerable<UInt512> GenerateUInt512(int randomCount, int seed)
    {
        yield return UInt512.Zero;
        yield return UInt512.One;
        yield return UInt512.MaxValue;
        yield return UInt512.MaxValue - UInt512.One;

        for (int k = 0; k < 512; k++)
        {
            UInt512 powerOfTwo = UInt512.One << k;
            yield return powerOfTwo;
            yield return powerOfTwo - UInt512.One;
        }

        Random rng = new(seed);
        for (int i = 0; i < randomCount; i++)
            yield return RandomUInt512(rng);
    }

    private static UInt512 RandomUInt512(Random rng)
    {
        UInt256 upper = RandomUInt256(rng);
        UInt256 lower = RandomUInt256(rng);
        return new UInt512(upper, lower);
    }
}
