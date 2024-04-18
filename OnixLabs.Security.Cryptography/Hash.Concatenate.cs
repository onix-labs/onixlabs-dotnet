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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Concatenates the left-hand and right-hand hashes using the specified hash algorithm.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="left">The left-hand hash to concatenate.</param>
    /// <param name="right">The right-hand hash to concatenate.</param>
    /// <returns>Returns a cryptographic hash representing the concatenation of the left-hand and right-hand hash values.</returns>
    public static Hash Concatenate(HashAlgorithm algorithm, Hash left, Hash right)
    {
        byte[] data = [..left.ToByteArray(), ..right.ToByteArray()];
        byte[] hash = algorithm.ComputeHash(data);
        return new Hash(hash);
    }

    /// <summary>
    /// Concatenates the left-hand and right-hand hashes using the specified hash algorithm.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> which will be used to compute the hash.</param>
    /// <param name="other">The other hash to concatenate with the current hash.</param>
    /// <returns>Returns a cryptographic hash representing the concatenation of the left-hand and right-hand hash values.</returns>
    public Hash Concatenate(HashAlgorithm algorithm, Hash other) => Concatenate(algorithm, this, other);
}
