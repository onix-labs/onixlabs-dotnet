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

using System;
using System.Collections.Generic;
using System.Linq;
using OnixLabs.Core.Linq;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a Merkle tree.
/// </summary>
public abstract partial class MerkleTree
{
    /// <summary>
    /// Prevents an instance of <see cref="MerkleTree"/> from being created.
    /// </summary>
    internal MerkleTree()
    {
    }

    /// <summary>
    /// Gets the hash of this <see cref="MerkleTree"/> node.
    /// </summary>
    public abstract Hash Hash { get; }

    /// <summary>
    /// Builds a Merkle tree from the specified leaf node hashes.
    /// </summary>
    /// <param name="nodes">The leaf nodes from which to build a Merkle tree.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> instance from the specified leaf nodes.</returns>
    public static MerkleTree Build(IEnumerable<Hash> nodes)
    {
        IReadOnlyList<MerkleTree> leafNodes = nodes.Select(MerkleTreeLeafNode.CreateHashNode).ToList();
        return Build(leafNodes);
    }

    /// <summary>
    /// Builds a Merkle tree from the specified Merkle tree nodes.
    /// </summary>
    /// <param name="nodes">The Merkle tree nodes from which to build a Merkle tree.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> instance from the specified nodes.</returns>
    private static MerkleTree Build(IReadOnlyList<MerkleTree> nodes)
    {
        CheckIfMerkleTreesAreEmpty(nodes);
        CheckNodesHaveEqualHashAlgorithms(nodes);

        return MergeMerkleTreeNodes(nodes);
    }

    /// <summary>
    /// Checks whether an <see cref="IEnumerable{MerkleTree}"/> is empty.
    /// </summary>
    /// <param name="merkleTrees">The <see cref="IEnumerable{MerkleTree}"/> to check.</param>
    /// <exception cref="ArgumentException">if the <see cref="IEnumerable{MerkleTree}"/> is empty.</exception>
    private static void CheckIfMerkleTreesAreEmpty(IEnumerable<MerkleTree> merkleTrees)
    {
        Require(merkleTrees.IsNotEmpty(), "Cannot construct a merkle tree from an empty list.", nameof(merkleTrees));
    }

    /// <summary>
    /// Checks whether all elements of an <see cref="IEnumerable{MerkleTree}"/> have the same hash algorithm type.
    /// </summary>
    /// <param name="merkleTrees">The <see cref="IEnumerable{MerkleTree}"/> to check.</param>
    /// <exception cref="ArgumentException">if the elements of the <see cref="IEnumerable{MerkleTree}"/> do not have the same hash algorithm type.</exception>
    private static void CheckNodesHaveEqualHashAlgorithms(IEnumerable<MerkleTree> merkleTrees)
    {
        Require(merkleTrees.AllEqualBy(merkleTree => merkleTree.Hash.AlgorithmType),
            "Cannot construct a merkle tree with different hash types.", nameof(merkleTrees));
    }

    /// <summary>
    /// Ensures that an <see cref="IReadOnlyList{MerkleTree}"/> has an even number of elements.
    /// If the <see cref="IReadOnlyList{MerkleTree}"/> contains an odd number of elements, then an all-zero hash
    /// of the same <see cref="HashAlgorithmType"/> will be inserted at the end of the list.
    /// </summary>
    /// <param name="merkleTrees">The <see cref="IReadOnlyList{MerkleTree}"/> to ensure has an even number of elements.</param>
    /// <returns>Returns a new <see cref="IReadOnlyList{MerkleTree}"/> containing an even number of elements.</returns>
    private static IReadOnlyList<MerkleTree> EnsureEvenNumberOfNodes(IReadOnlyList<MerkleTree> merkleTrees)
    {
        if (merkleTrees.IsCountEven()) return merkleTrees;

        HashAlgorithmType hashAlgorithmType = merkleTrees[0].Hash.AlgorithmType;
        return merkleTrees.Append(MerkleTreeLeafNode.CreateEmptyNode(hashAlgorithmType)).ToList();
    }

    /// <summary>
    /// Merges an <see cref="IReadOnlyList{MerkleTree}"/> of Merkle tree nodes.
    /// </summary>
    /// <param name="merkleTrees">The <see cref="IReadOnlyList{MerkleTree}"/> of Merkle tree nodes to merge.</param>
    /// <returns>Returns a merged <see cref="IReadOnlyList{MerkleTree}"/> of Merkle tree nodes.</returns>
    private static MerkleTree MergeMerkleTreeNodes(IReadOnlyList<MerkleTree> merkleTrees)
    {
        while (true)
        {
            if (merkleTrees.IsSingle())
            {
                return merkleTrees[0];
            }

            merkleTrees = EnsureEvenNumberOfNodes(merkleTrees);

            List<MerkleTree> mutableMerkleTrees = new();

            for (int index = 0; index < merkleTrees.Count; index += 2)
            {
                MerkleTree leftMerkleTree = merkleTrees[index];
                MerkleTree rightMerkleTree = merkleTrees[index + 1];
                MerkleTree merkleTreeNode = new MerkleTreeBranchNode(leftMerkleTree, rightMerkleTree);
                mutableMerkleTrees.Add(merkleTreeNode);
            }

            merkleTrees = mutableMerkleTrees;
        }
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        return Hash.ToString();
    }
}
