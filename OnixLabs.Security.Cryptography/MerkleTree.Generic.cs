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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a generic Merkle tree.
/// </summary>
public abstract partial class MerkleTree<T> : MerkleTree where T : IHashable
{
    /// <summary>
    /// Represents a generic Merkle tree branch node.
    /// </summary>
    private sealed class MerkleTreeBranchNode<THashable> : MerkleTree<THashable> where THashable : IHashable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeBranchNode{T}"/> class.
        /// </summary>
        /// <param name="left">The left-hand <see cref="MerkleTree{T}"/> node.</param>
        /// <param name="right">The right-hand <see cref="MerkleTree{T}"/> node.</param>
        public MerkleTreeBranchNode(MerkleTree<THashable> left, MerkleTree<THashable> right)
        {
            Left = left;
            Right = right;
            Hash = Left.Hash.Concatenate(Right.Hash);
        }

        /// <summary>
        /// Gets the left-hand <see cref="MerkleTree{T}"/> node.
        /// </summary>
        private MerkleTree<THashable> Left { get; }

        /// <summary>
        /// Gets the right-hand <see cref="MerkleTree{T}"/> node.
        /// </summary>
        private MerkleTree<THashable> Right { get; }

        /// <summary>
        /// Gets the <see cref="Hash"/> of the current <see cref="MerkleTree{T}"/> node.
        /// </summary>
        protected override Hash Hash { get; }
    }

    /// <summary>
    /// Represents a generic Merkle tree leaf node.
    /// </summary>
    private sealed class MerkleTreeLeafNode<THashable> : MerkleTree<THashable> where THashable : IHashable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeLeafNode{T}"/> class.
        /// </summary>
        /// <param name="value">The underlying value of the current node.</param>
        public MerkleTreeLeafNode(THashable value)
        {
            Value = value;
            Hash = value.ComputeHash();
        }

        /// <summary>
        /// Gets the underlying value of the current node.
        /// </summary>
        private THashable Value { get; }

        /// <summary>
        /// Gets the <see cref="Hash"/> of the current <see cref="MerkleTree{T}"/> node.
        /// </summary>
        protected override Hash Hash { get; }
    }

    /// <summary>
    /// Represents an empty generic Merkle tree node.
    /// </summary>
    private sealed class MerkleTreeEmptyNode<THashable> : MerkleTree<THashable> where THashable : IHashable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MerkleTree{T}.MerkleTreeEmptyNode{T}"/> class.
        /// </summary>
        /// <param name="type">The <see cref="HashAlgorithmType"/> for the current node.</param>
        public MerkleTreeEmptyNode(HashAlgorithmType type)
        {
            Hash = Hash.CreateAllZeroHash(type);
        }

        /// <summary>
        /// Gets the <see cref="Hash"/> of the current <see cref="MerkleTree{T}"/> node.
        /// </summary>
        protected override Hash Hash { get; }
    }
}
