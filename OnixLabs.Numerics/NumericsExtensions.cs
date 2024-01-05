// Copyright 2020-2024 ONIXLabs
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
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace OnixLabs.Numerics;

/// <summary>
/// Provides extension methods for numeric types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class NumericsExtensions
{
    /// <summary>
    /// Gets the unscaled value of the current <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> from which to obtain an unscaled value.</param>
    /// <returns>Returns the unscaled value of the current <see cref="decimal"/> as a <see cref="BigInteger"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static BigInteger GetUnscaledValue(this decimal value)
    {
        const int significandSize = 13;
        int[] significandBits = decimal.GetBits(value);
        byte[] significandBytes = new byte[significandSize];
        Buffer.BlockCopy(significandBits, 0, significandBytes, 0, significandSize);
        BigInteger result = new(significandBytes);

        return decimal.IsPositive(value) ? result : -result;
    }

    /// <summary>
    /// Converts the current <see cref="IBinaryInteger{TSelf}"/> value to a <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type of the value to convert.</typeparam>
    /// <returns>Returns a <see cref="BigInteger"/> representing the current value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static BigInteger ToBigInteger<T>(this T value) where T : IBinaryInteger<T>
    {
        return BigInteger.CreateChecked(value);
    }
}
