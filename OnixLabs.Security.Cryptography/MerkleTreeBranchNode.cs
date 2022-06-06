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
/// Represents a Merkle tree branch node.
/// </summary>
internal sealed class MerkleTreeBranchNode : MerkleTree
{
    /// <summary>
    /// Creates a new instance of the <see cref="MerkleTreeBranchNode"/> class.
    /// </summary>
    /// <param name="left">The left-hand <see cref="MerkleTree"/> node.</param>
    /// <param name="right">The right-hand <see cref="MerkleTree"/> node.</param>
    public MerkleTreeBranchNode(MerkleTree left, MerkleTree right)
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

    /// <summary>
    /// Gets the hash of this <see cref="MerkleTree"/> node.
    /// </summary>
    public override Hash Hash => Left.Hash.Concatenate(Right.Hash);
}
