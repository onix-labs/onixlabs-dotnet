// Copyright 2020-2022 ONIXLabs
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

/// <summary>
/// Represents a Merkle tree leaf node.
/// </summary>
internal sealed class MerkleTreeLeafNode : MerkleTree
{
    /// <summary>
    /// Creates a new instance of the <see cref="MerkleTreeLeafNode"/> class.
    /// </summary>
    public MerkleTreeLeafNode(Hash hash)
    {
        Hash = hash;
    }

    /// <summary>
    /// Gets the hash of this <see cref="MerkleTree"/> node.
    /// </summary>
    public override Hash Hash { get; }

    /// <summary>
    /// Creates a <see cref="MerkleTree"/> from from the specified hash.
    /// </summary>
    /// <param name="hash">The <see cref="Hash"/> from which to create a <see cref="MerkleTree"/> node.</param>
    /// <returns>Returns an <see cref="MerkleTree"/> node.</returns>
    public static MerkleTree CreateHashNode(Hash hash)
    {
        return new MerkleTreeLeafNode(hash);
    }

    /// <summary>
    /// Creates an empty <see cref="MerkleTree"/> node represented by an all-zero hash.
    /// </summary>
    /// <param name="type">The hash algorithm type of the node to create.</param>
    /// <returns>Returns an empty <see cref="MerkleTree"/> node represented by an all-zero hash.</returns>
    public static MerkleTree CreateEmptyNode(HashAlgorithmType type)
    {
        return new MerkleTreeLeafNode(Hash.CreateAllZeroHash(type));
    }
}
