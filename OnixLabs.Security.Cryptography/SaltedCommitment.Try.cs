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

public readonly partial record struct SaltedCommitment
{
    /// <summary>
    /// Tries to get the output length of the hash algorithm with the specified name.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm.</param>
    /// <param name="length">The output length of the hash algorithm, in bytes, or zero if the algorithm is not supported.</param>
    /// <returns>Returns <see langword="true"/> if the hash algorithm is SHA-256, SHA-384 or SHA-512; otherwise, <see langword="false"/>.</returns>
    internal static bool TryGetHashLength(ReadOnlySpan<char> algorithmName, out int length)
    {
        length = algorithmName switch
        {
            nameof(HashAlgorithmName.SHA256) => SHA256.HashSizeInBytes,
            nameof(HashAlgorithmName.SHA384) => SHA384.HashSizeInBytes,
            nameof(HashAlgorithmName.SHA512) => SHA512.HashSizeInBytes,
            _ => 0
        };

        return length > 0;
    }
}
