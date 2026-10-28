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

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public abstract partial class MerkleTree
{
    /// <summary>
    /// Creates a Merkle tree from the specified <see cref="MerkleTree"/> leaf nodes.
    /// </summary>
    /// <param name="leaves">The Merkle tree leaf nodes from which to build a Merkle tree.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash together left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> node that represents the Merkle root.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="leaves"/> is empty.</exception>
    /// <exception cref="CryptographicException">Thrown when a hash could not be computed while building the Merkle tree.</exception>
    public static MerkleTree Create(IEnumerable<Hash> leaves, HashAlgorithm algorithm)
    {
        // ReSharper disable once HeapView.ObjectAllocation
        List<MerkleTree> nodes = [];

        // ReSharper disable once LoopCanBeConvertedToQuery, HeapView.ObjectAllocation.Possible, HeapView.ObjectAllocation.Evident
        foreach (Hash leaf in leaves) nodes.Add(new MerkleTreeLeafNode(leaf));

        Require(nodes.Count > 0, "Cannot construct a Merkle tree from an empty list.", nameof(leaves));
        return BuildMerkleTree(nodes, algorithm);
    }

    /// <summary>
    /// Creates a Merkle tree from the specified <see cref="IHashable"/> leaf nodes.
    /// </summary>
    /// <param name="leaves">The Merkle tree leaf nodes from which to build a Merkle tree.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash together left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
    /// <typeparam name="T">The underlying <see cref="IHashable"/> type of the Merkle tree leaf values.</typeparam>
    /// <returns>Returns a new <see cref="MerkleTree{T}"/> node that represents the Merkle root.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="leaves"/> is empty.</exception>
    /// <exception cref="CryptographicException">Thrown when a hash could not be computed while building the Merkle tree.</exception>
    public static MerkleTree<T> Create<T>(IEnumerable<T> leaves, HashAlgorithm algorithm) where T : IHashable => MerkleTree<T>.Create(leaves, algorithm);

    /// <summary>
    /// Builds a Merkle tree from the specified <see cref="MerkleTree"/> nodes.
    /// </summary>
    /// <param name="nodes">The Merkle tree nodes from which to build a Merkle tree, which are consumed in the process.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash together left-hand and right-hand <see cref="MerkleTree"/> nodes.</param>
    /// <returns>Returns a new <see cref="MerkleTree"/> node that represents the Merkle root.</returns>
    /// <remarks>
    /// Why: each level is reduced in place, so building a tree of n leaves allocates only the n - 1 branch nodes and at most one empty node,
    /// rather than a new list or array per level. Every node is immutable, so a single empty node is shared by every odd-length level.
    /// </remarks>
    private static MerkleTree BuildMerkleTree(List<MerkleTree> nodes, HashAlgorithm algorithm)
    {
        MerkleTreeEmptyNode? emptyNode = null;

        while (nodes.Count > 1)
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            if (nodes.Count % 2 is not 0) nodes.Add(emptyNode ??= new MerkleTreeEmptyNode(algorithm));

            int mergedCount = nodes.Count / 2;

            // Why: writing branch i to position i is safe, because it reads positions 2i and 2i + 1, which are never earlier than i.
            for (int index = 0; index < mergedCount; index++)
            {
                // ReSharper disable once HeapView.ObjectAllocation.Evident
                nodes[index] = new MerkleTreeBranchNode(nodes[2 * index], nodes[2 * index + 1], algorithm);
            }

            nodes.RemoveRange(mergedCount, nodes.Count - mergedCount);
        }

        return nodes[0];
    }
}
