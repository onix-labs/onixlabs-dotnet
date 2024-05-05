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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a Merkle tree.
/// </summary>
public abstract partial class MerkleTree : ICryptoPrimitive<MerkleTree>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MerkleTree"/> class.
    /// This constructor is marked internal to prevent external implementation.
    /// </summary>
    internal MerkleTree(Hash hash)
    {
        Hash = hash;
    }

    /// <summary>
    /// Gets the <see cref="Hash"/> of the current <see cref="MerkleTree"/> node.
    /// </summary>
    public Hash Hash { get; }

    /// <summary>
    /// Represents a Merkle tree branch node.
    /// </summary>
    private sealed class MerkleTreeBranchNode : MerkleTree
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree.MerkleTreeBranchNode"/> class.
        /// </summary>
        /// <param name="left">The left-hand <see cref="MerkleTree"/> node.</param>
        /// <param name="right">The right-hand <see cref="MerkleTree"/> node.</param>
        /// <param name="algorithm">The hash algorithm that will be used to hash together the left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
        public MerkleTreeBranchNode(MerkleTree left, MerkleTree right, HashAlgorithm algorithm) : base(Hash.Concatenate(algorithm, left.Hash, right.Hash))
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Gets the left-hand <see cref="MerkleTree"/> node.
        /// </summary>
        public MerkleTree Left { get; }

        /// <summary>
        /// Gets the right-hand <see cref="MerkleTree"/> node.
        /// </summary>
        public MerkleTree Right { get; }
    }

    /// <summary>
    /// Represents a Merkle tree leaf node.
    /// </summary>
    private sealed class MerkleTreeLeafNode : MerkleTree
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree.MerkleTreeLeafNode"/> class.
        /// </summary>
        /// <param name="hash">The <see cref="Hash"/> value for the current node.</param>
        public MerkleTreeLeafNode(Hash hash) : base(hash)
        {
        }
    }

    /// <summary>
    /// Represents an empty Merkle tree node.
    /// </summary>
    private sealed class MerkleTreeEmptyNode : MerkleTree
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree.MerkleTreeEmptyNode"/> class.
        /// </summary>
        /// <param name="algorithm">The hash algorithm that will be used to determine the size of the required empty hash.</param>
        public MerkleTreeEmptyNode(HashAlgorithm algorithm) : base(new Hash(0x00, algorithm.HashSize / 8))
        {
        }
    }
}
