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

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

public abstract partial class MerkleTree<T>
{
    /// <summary>
    /// Creates a Merkle tree from the specified <see cref="IHashable"/> leaf nodes.
    /// </summary>
    /// <param name="leaves">The Merkle tree leaf nodes from which to build a Merkle tree.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash together left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> node that represents the Merkle root.</returns>
    public static MerkleTree<T> Create(IEnumerable<T> leaves, HashAlgorithm algorithm)
    {
        // ReSharper disable once HeapView.ObjectAllocation
        List<MerkleTree<T>> nodes = [];

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible, HeapView.ObjectAllocation.Evident
        foreach (T leaf in leaves) nodes.Add(new MerkleTreeLeafNode(leaf, algorithm));

        Require(nodes.IsNotEmpty(), "Cannot construct a Merkle tree from an empty list.", nameof(leaves));
        return BuildMerkleTree(nodes, algorithm);
    }

    /// <summary>
    /// Builds a Merkle tree from the specified <see cref="MerkleTree"/> nodes.
    /// </summary>
    /// <param name="nodes">The Merkle tree nodes from which to build a Merkle tree.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash together left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> node that represents the Merkle root.</returns>
    private static MerkleTree<T> BuildMerkleTree(IReadOnlyList<MerkleTree<T>> nodes, HashAlgorithm algorithm)
    {
        while (true)
        {
            if (nodes.IsSingle()) return nodes.Single();
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            if (nodes.IsCountOdd()) nodes = nodes.Append(new MerkleTreeEmptyNode(algorithm)).ToList();

            // ReSharper disable once HeapView.ObjectAllocation
            List<MerkleTree<T>> mergedNodes = [];

            for (int index = 0; index < nodes.Count; index += 2)
            {
                MerkleTree<T> left = nodes[index];
                MerkleTree<T> right = nodes[index + 1];
                // ReSharper disable once HeapView.ObjectAllocation.Evident
                mergedNodes.Add(new MerkleTreeBranchNode(left, right, algorithm));
            }

            nodes = mergedNodes;
        }
    }
}
