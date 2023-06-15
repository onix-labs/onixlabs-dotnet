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

using System.Collections.Generic;
using System.Linq;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

public abstract partial class MerkleTree<T>
{
    /// <summary>
    /// Creates a <see cref="MerkleTree{T}"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree{T}"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> computed from the specified leaves.</returns>
    public static MerkleTree<T> Create(params T[] leaves)
    {
        return Create(leaves.ToList());
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree{T}"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree{T}"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> computed from the specified leaves.</returns>
    public static MerkleTree<T> Create(IEnumerable<T> leaves)
    {
        IReadOnlyList<MerkleTree<T>> nodes = leaves.Select(leaf => new MerkleTreeLeafNode<T>(leaf)).ToList();

        Require(nodes.IsNotEmpty(),
            "Cannot construct a merkle tree from an empty list.", nameof(nodes));

        Require(nodes.AllEqualBy(merkleTree => merkleTree.Hash.AlgorithmType),
            "Cannot construct a merkle tree with different hash types.", nameof(nodes));

        return Create(nodes);
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree{T}"/> from the specified leaves.
    /// </summary>
    /// <param name="nodes">The leaves from which to create a <see cref="MerkleTree{T}"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> computed from the specified leaves.</returns>
    private static MerkleTree<T> Create(IReadOnlyList<MerkleTree<T>> nodes)
    {
        while (true)
        {
            if (nodes.IsSingle())
            {
                return nodes.Single();
            }

            if (nodes.IsCountOdd())
            {
                HashAlgorithmType type = nodes[0].Hash.AlgorithmType;
                nodes = nodes.Append(new MerkleTreeEmptyNode<T>(type)).ToList();
            }

            List<MerkleTree<T>> mergedNodes = new();

            for (int index = 0; index < nodes.Count; index += 2)
            {
                MerkleTree<T> left = nodes[index];
                MerkleTree<T> right = nodes[index + 1];
                mergedNodes.Add(new MerkleTreeBranchNode<T>(left, right));
            }

            nodes = mergedNodes;
        }
    }
}
