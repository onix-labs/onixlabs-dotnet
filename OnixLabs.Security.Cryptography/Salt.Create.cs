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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <summary>
    /// Creates a new <see cref="Salt"/> instance of the specified length.
    /// </summary>
    /// <param name="length">The length of the salt to create.</param>
    /// <returns>Returns a new <see cref="Salt"/> instance of the specified length.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length"/> is negative.</exception>
    public static Salt Create(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        const int maxStackSize = 256;
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();

        // Why: length is caller-supplied and unbounded; stackalloc only for small buffers to avoid a stack overflow.
        Span<byte> value = length <= maxStackSize ? stackalloc byte[length] : new byte[length];
        generator.GetBytes(value);
        return new Salt(value);
    }

    /// <summary>
    /// Creates a new, non-zero <see cref="Salt"/> instance of the specified length.
    /// </summary>
    /// <param name="length">The length of the salt to create.</param>
    /// <returns>Returns a new non-zero <see cref="Salt"/> instance of the specified length.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length"/> is negative.</exception>
    public static Salt CreateNonZero(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        const int maxStackSize = 256;
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();

        // Why: length is caller-supplied and unbounded; stackalloc only for small buffers to avoid a stack overflow.
        Span<byte> value = length <= maxStackSize ? stackalloc byte[length] : new byte[length];
        generator.GetNonZeroBytes(value);
        return new Salt(value);
    }
}
