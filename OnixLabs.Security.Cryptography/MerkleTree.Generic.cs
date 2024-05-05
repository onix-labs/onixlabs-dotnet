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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a generic Merkle tree.
/// </summary>
public abstract partial class MerkleTree<T> : MerkleTree, IValueEquatable<MerkleTree<T>> where T : IHashable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MerkleTree"/> class.
    /// This constructor is marked private to prevent external implementation.
    /// </summary>
    private MerkleTree(Hash hash) : base(hash)
    {
    }

    /// <summary>
    /// Represents a generic Merkle tree branch node.
    /// </summary>
    private sealed class MerkleTreeBranchNode : MerkleTree<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeBranchNode"/> class.
        /// </summary>
        /// <param name="left">The left-hand <see cref="MerkleTree{T}"/> node.</param>
        /// <param name="right">The right-hand <see cref="MerkleTree{T}"/> node.</param>
        /// <param name="algorithm">The hash algorithm that will be used to hash together the left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
        public MerkleTreeBranchNode(MerkleTree<T> left, MerkleTree<T> right, HashAlgorithm algorithm) : base(Hash.Concatenate(algorithm, left.Hash, right.Hash))
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Gets the left-hand <see cref="MerkleTree{T}"/> node.
        /// </summary>
        public MerkleTree<T> Left { get; }

        /// <summary>
        /// Gets the right-hand <see cref="MerkleTree{T}"/> node.
        /// </summary>
        public MerkleTree<T> Right { get; }
    }

    /// <summary>
    /// Represents a generic Merkle tree leaf node.
    /// </summary>
    private sealed class MerkleTreeLeafNode : MerkleTree<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeLeafNode"/> class.
        /// </summary>
        /// <param name="value">The underlying value of the current node.</param>
        /// <param name="algorithm">The hash algorithm that will be used to hash the specified value.</param>
        public MerkleTreeLeafNode(T value, HashAlgorithm algorithm) : base(value.ComputeHash(algorithm))
        {
            Value = value;
        }

        /// <summary>
        /// Gets the underlying value of the current node.
        /// </summary>
        public T Value { get; }
    }

    /// <summary>
    /// Represents an empty generic Merkle tree node.
    /// </summary>
    private sealed class MerkleTreeEmptyNode : MerkleTree<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeEmptyNode"/> class.
        /// </summary>
        /// <param name="algorithm">The hash algorithm that will be used to determine the size of the required empty hash.</param>
        public MerkleTreeEmptyNode(HashAlgorithm algorithm) : base(new Hash(0x00, algorithm.HashSize / 8))
        {
        }
    }
}
