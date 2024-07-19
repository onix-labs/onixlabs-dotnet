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
    internal RandomNumberProvider()
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
    /// Gets the next random <see cref="Int32"/> value.
    /// </summary>
    /// <param name="minimum">The minimum random value.</param>
    /// <param name="maximum">The maximum random value.</param>
    /// <returns>Returns the next random <see cref="Int32"/> value.</returns>
    internal abstract int GetNextInt(int minimum, int maximum);
}

/// <summary>
/// Represents a pseudo-random number provider.
/// </summary>
/// <param name="random">The <see cref="Random"/> instance that will be used to generate random values.</param>
file sealed class PseudoRandomNumberProvider(Random random) : RandomNumberProvider
{
    /// <summary>
    /// Gets the next random <see cref="Int32"/> value.
    /// </summary>
    /// <param name="minimum">The minimum random value.</param>
    /// <param name="maximum">The maximum random value.</param>
    /// <returns>Returns the next random <see cref="Int32"/> value.</returns>
    internal override int GetNextInt(int minimum, int maximum) => random.Next(minimum, maximum);
}

/// <summary>
/// Represents a cryptographically secure random number provider.
/// </summary>
file sealed class SecureRandomNumberProvider : RandomNumberProvider
{
    /// <summary>
    /// Gets the next random <see cref="Int32"/> value.
    /// </summary>
    /// <param name="minimum">The minimum random value.</param>
    /// <param name="maximum">The maximum random value.</param>
    /// <returns>Returns the next random <see cref="Int32"/> value.</returns>
    internal override int GetNextInt(int minimum, int maximum) => RandomNumberGenerator.GetInt32(minimum, maximum);
}
