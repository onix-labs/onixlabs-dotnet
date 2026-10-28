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

namespace OnixLabs.Security;

/// <summary>
/// Represents a random number provider.
/// </summary>
// ReSharper disable HeapView.ObjectAllocation.Evident
internal abstract class RandomNumberProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomNumberProvider"/> class.
    /// </summary>
    private protected RandomNumberProvider()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="PseudoRandomNumberProvider"/> class.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> instance that will be used to generate random values.</param>
    /// <returns>Returns a new instance of the <see cref="PseudoRandomNumberProvider"/> class.</returns>
    public static RandomNumberProvider CreatePseudoRandomNumberProvider(Random random) => new PseudoRandomNumberProvider(random);

    /// <summary>
    /// Creates a new instance of the <see cref="SecureRandomNumberProvider"/> class.
    /// </summary>
    /// <returns>Returns a new instance of the <see cref="SecureRandomNumberProvider"/> class.</returns>
    public static RandomNumberProvider CreateSecureRandomNumberProvider() => new SecureRandomNumberProvider();

    /// <summary>
    /// Fills the specified destination with items chosen at random from the specified choices.
    /// </summary>
    /// <param name="choices">The items from which to choose.</param>
    /// <param name="destination">The span to fill with randomly chosen items.</param>
    internal abstract void GetItems(ReadOnlySpan<char> choices, Span<char> destination);
}

/// <summary>
/// Represents a pseudo-random number provider.
/// </summary>
/// <param name="random">The <see cref="Random"/> instance that will be used to generate random values.</param>
file sealed class PseudoRandomNumberProvider(Random random) : RandomNumberProvider
{
    /// <inheritdoc/>
    /// <remarks>
    /// Why: this deliberately draws one <see cref="Random.Next(int, int)"/> per item rather than calling
    /// <see cref="Random.GetItems{T}(ReadOnlySpan{T}, Span{T})"/>, whose sequence can differ, so that a seeded builder keeps producing
    /// the same tokens it always has.
    /// </remarks>
    internal override void GetItems(ReadOnlySpan<char> choices, Span<char> destination)
    {
        for (int index = 0; index < destination.Length; index++)
            destination[index] = choices[random.Next(0, choices.Length)];
    }
}

/// <summary>
/// Represents a cryptographically secure random number provider.
/// </summary>
file sealed class SecureRandomNumberProvider : RandomNumberProvider
{
    /// <inheritdoc/>
    internal override void GetItems(ReadOnlySpan<char> choices, Span<char> destination) =>
        RandomNumberGenerator.GetItems(choices, destination);
}
