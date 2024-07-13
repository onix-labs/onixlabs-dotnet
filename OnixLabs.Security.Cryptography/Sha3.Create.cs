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

namespace OnixLabs.Security.Cryptography;

// ReSharper disable once HeapView.ObjectAllocation.Evident
public abstract partial class Sha3
{
    /// <summary>
    /// Creates an instance of the <see cref="Sha3Hash224"/> algorithm.
    /// </summary>
    /// <returns>An instance of the <see cref="Sha3Hash224"/> algorithm.</returns>
    public static Sha3Hash224 CreateSha3Hash224() => new();

    /// <summary>
    /// Creates an instance of the <see cref="Sha3Hash256"/> algorithm.
    /// </summary>
    /// <returns>An instance of the <see cref="Sha3Hash256"/> algorithm.</returns>
    public static Sha3Hash256 CreateSha3Hash256() => new();

    /// <summary>
    /// Creates an instance of the <see cref="Sha3Hash384"/> algorithm.
    /// </summary>
    /// <returns>An instance of the <see cref="Sha3Hash384"/> algorithm.</returns>
    public static Sha3Hash384 CreateSha3Hash384() => new();

    /// <summary>
    /// Creates an instance of the <see cref="Sha3Hash512"/> algorithm.
    /// </summary>
    /// <returns>An instance of the <see cref="Sha3Hash512"/> algorithm.</returns>
    public static Sha3Hash512 CreateSha3Hash512() => new();

    /// <summary>
    /// Creates an instance of the <see cref="Sha3Shake128"/> algorithm.
    /// </summary>
    /// <param name="length">The output length of the hash in bytes.</param>
    /// <returns>An instance of the <see cref="Sha3Shake128"/> algorithm.</returns>
    public static Sha3Shake128 CreateSha3Shake128(int length) => new(length);

    /// <summary>
    /// Creates an instance of the <see cref="Sha3Shake256"/> algorithm.
    /// </summary>
    /// <param name="length">The output length of the hash in bytes.</param>
    /// <returns>An instance of the <see cref="Sha3Shake256"/> algorithm.</returns>
    public static Sha3Shake256 CreateSha3Shake256(int length) => new(length);
}
