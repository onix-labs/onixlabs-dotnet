// Copyright 2020-2023 ONIXLabs
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

using System.Linq;
using System.Security.Cryptography;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Concatenates two hashes.
    /// </summary>
    /// <param name="a">The first <see cref="Hash"/> to concatenate.</param>
    /// <param name="b">The second <see cref="Hash"/> to concatenate.</param>
    /// <returns>Returns the concatenation of the two <see cref="Hash"/> instances.</returns>
    public static Hash Concatenate(Hash a, Hash b)
    {
        Require(a.AlgorithmType == b.AlgorithmType, "Cannot concatenate hashes of different algorithm types.");
        using HashAlgorithm algorithm = a.AlgorithmType.GetHashAlgorithm();
        byte[] concatenatedValue = a.ToByteArray().Concat(b.ToByteArray()).ToArray();
        byte[] hashedValue = algorithm.ComputeHash(concatenatedValue);
        return Create(hashedValue, a.AlgorithmType);
    }

    /// <summary>
    /// Concatenates two hashes.
    /// </summary>
    /// <param name="a">The first <see cref="Hash"/> to concatenate.</param>
    /// <param name="b">The second <see cref="Hash"/> to concatenate.</param>
    /// <param name="length">The length of the hash in bytes.</param>
    /// <returns>Returns the concatenation of the two <see cref="Hash"/> instances.</returns>
    public static Hash Concatenate(Hash a, Hash b, int length)
    {
        Require(a.AlgorithmType == b.AlgorithmType, "Cannot concatenate hashes of different algorithm types.");
        using HashAlgorithm algorithm = a.AlgorithmType.GetHashAlgorithm(length);
        byte[] concatenatedValue = a.ToByteArray().Concat(b.ToByteArray()).ToArray();
        byte[] hashedValue = algorithm.ComputeHash(concatenatedValue);
        return Create(hashedValue, a.AlgorithmType);
    }

    /// <summary>
    /// Concatenates this hash with another hash.
    /// </summary>
    /// <param name="other">The other hash to concatenate with this hash.</param>
    /// <returns>Returns the concatenation of the two <see cref="Hash"/> instances.</returns>
    public Hash Concatenate(Hash other)
    {
        return Concatenate(this, other);
    }

    /// <summary>
    /// Concatenates this hash with another hash.
    /// </summary>
    /// <param name="other">The other hash to concatenate with this hash.</param>
    /// <param name="length">The length of the hash in bytes.</param>
    /// <returns>Returns the concatenation of the two <see cref="Hash"/> instances.</returns>
    public Hash Concatenate(Hash other, int length)
    {
        return Concatenate(this, other, length);
    }
}
