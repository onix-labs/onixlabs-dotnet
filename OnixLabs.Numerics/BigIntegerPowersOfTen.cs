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

using System.Numerics;

namespace OnixLabs.Numerics;

/// <summary>
/// Provides cached powers of ten as <see cref="BigInteger"/> values.
/// </summary>
/// <remarks>
/// Why: decimal arithmetic aligns scales, rounds, divides, compares and formats by multiplying or dividing by 10^n, and every
/// <see cref="BigInteger.Pow(BigInteger, int)"/> call computes and allocates the power afresh. The first 256 powers cover every scale
/// in ordinary use and are computed once; larger or negative exponents fall back to <see cref="BigInteger.Pow(BigInteger, int)"/>,
/// including its exception for a negative exponent.
/// </remarks>
internal static class BigIntegerPowersOfTen
{
    private const int CachedCount = 256;

    private static readonly BigInteger[] Cache = CreateCache();

    /// <summary>
    /// Gets ten raised to the specified exponent.
    /// </summary>
    /// <param name="exponent">The exponent, which must not be negative.</param>
    /// <returns>Returns 10^<paramref name="exponent"/>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="exponent"/> is negative.</exception>
    public static BigInteger Get(int exponent) => (uint)exponent < CachedCount ? Cache[exponent] : BigInteger.Pow(10, exponent);

    /// <summary>
    /// Creates the cache of the first <see cref="CachedCount"/> powers of ten.
    /// </summary>
    /// <returns>Returns an array where element n is 10^n.</returns>
    private static BigInteger[] CreateCache()
    {
        BigInteger[] result = new BigInteger[CachedCount];
        result[0] = BigInteger.One;

        for (int exponent = 1; exponent < CachedCount; exponent++)
            result[exponent] = result[exponent - 1] * 10;

        return result;
    }
}
