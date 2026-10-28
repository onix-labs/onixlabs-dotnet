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

using System.Linq;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

/// <summary>
/// Provides a deliberately naive SHA-256 Merkle root computation, used as an oracle for <see cref="MerkleTree"/>.
/// </summary>
/// <remarks>
/// It encodes the library's documented rule independently of the library: a level with an odd number of nodes is padded with an all-zero
/// digest, and each pair is replaced by the SHA-256 digest of the left digest followed by the right. It uses only
/// <see cref="SHA256.HashData(byte[])"/>, never <see cref="Hash"/>, <see cref="MerkleTree"/> or the library's hash algorithm extensions.
/// </remarks>
public static class MerkleTreeReference
{
    /// <summary>
    /// Computes the SHA-256 Merkle root of the specified leaf digests.
    /// </summary>
    /// <param name="leaves">The leaf digests, in order.</param>
    /// <returns>Returns the Merkle root digest.</returns>
    public static byte[] ComputeSha256Root(IEnumerable<byte[]> leaves)
    {
        List<byte[]> level = leaves.ToList();

        while (level.Count > 1)
        {
            if (level.Count % 2 is not 0)
                level.Add(new byte[SHA256.HashSizeInBytes]);

            List<byte[]> next = [];

            for (int index = 0; index < level.Count; index += 2)
                next.Add(SHA256.HashData(level[index].Concat(level[index + 1]).ToArray()));

            level = next;
        }

        return level.Single();
    }
}
