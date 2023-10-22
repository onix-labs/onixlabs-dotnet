// Copyright © 2020 ONIXLabs
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

public abstract partial class MerkleTree
{
    /// <summary>
    /// Creates a <see cref="MerkleTree"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> computed from the specified leaves.</returns>
    public static MerkleTree Create(params Hash[] leaves)
    {
        return Create(leaves.ToList());
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> computed from the specified leaves.</returns>
    public static MerkleTree Create(IEnumerable<Hash> leaves)
    {
        IReadOnlyList<MerkleTree> nodes = leaves.Select(leaf => new MerkleTreeLeafNode(leaf)).ToList();

        Require(nodes.IsNotEmpty(),
            "Cannot construct a merkle tree from an empty list.", nameof(nodes));

        Require(nodes.AllEqualBy(merkleTree => merkleTree.Hash.AlgorithmType),
            "Cannot construct a merkle tree with different hash algorithm types.", nameof(nodes));

        return Create(nodes);
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree{T}"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree{T}"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> computed from the specified leaves.</returns>
    public static MerkleTree<T> Create<T>(params T[] leaves) where T : IHashable
    {
        return MerkleTree<T>.Create(leaves);
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree{T}"/> from the specified leaves.
    /// </summary>
    /// <param name="leaves">The leaves from which to create a <see cref="MerkleTree{T}"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> computed from the specified leaves.</returns>
    public static MerkleTree<T> Create<T>(IEnumerable<T> leaves) where T : IHashable
    {
        return MerkleTree<T>.Create(leaves);
    }

    /// <summary>
    /// Creates a <see cref="MerkleTree"/> from the specified leaves.
    /// </summary>
    /// <param name="nodes">The leaves from which to create a <see cref="MerkleTree"/>.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> computed from the specified leaves.</returns>
    private static MerkleTree Create(IReadOnlyList<MerkleTree> nodes)
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
                nodes = nodes.Append(new MerkleTreeEmptyNode(type)).ToList();
            }

            List<MerkleTree> mergedNodes = new();

            for (int index = 0; index < nodes.Count; index += 2)
            {
                MerkleTree left = nodes[index];
                MerkleTree right = nodes[index + 1];
                mergedNodes.Add(new MerkleTreeBranchNode(left, right));
            }

            nodes = mergedNodes;
        }
    }
}
