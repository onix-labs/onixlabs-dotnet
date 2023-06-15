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
using System.Collections.Immutable;

namespace OnixLabs.Security.Cryptography;

public abstract partial class MerkleTree<T>
{
    /// <summary>
    /// Obtains a hash-only, non-generic <see cref="MerkleTree"/> from the current <see cref="MerkleTree{T}"/>.
    /// </summary>
    /// <returns>Returns a hash-only, non-generic <see cref="MerkleTree"/> from the current <see cref="MerkleTree{T}"/>.</returns>
    public MerkleTree ToMerkleTree()
    {
        IEnumerable<Hash> hashes = GetLeafHashes();
        return Create(hashes);
    }

    /// <summary>
    /// Obtains the leaf values from the current <see cref="MerkleTree{T}"/>.
    /// </summary>
    /// <returns>Returns an <see cref="IEnumerable{T}"/> containing the leaf values from the current <see cref="MerkleTree{T}"/>.</returns>
    public ImmutableList<T> GetLeafValues()
    {
        ICollection<T> result = EmptyList<T>();
        CollectLeafValues(this, result);
        return result.ToImmutableList();
    }

    /// <summary>
    /// Recursively iterates through the specified <see cref="MerkleTree"/> to collect leaf hashes.
    /// </summary>
    /// <param name="current">The current <see cref="MerkleTree"/> from which to begin iterating through.</param>
    /// <param name="items">The collection that will contain the leaf hashes from the current <see cref="MerkleTree"/>.</param>
    protected override void CollectLeafHashes(MerkleTree current, ICollection<Hash> items)
    {
        switch (current)
        {
            case MerkleTreeBranchNode<T> branch:
                CollectLeafHashes(branch.Left, items);
                CollectLeafHashes(branch.Right, items);
                break;
            case MerkleTreeLeafNode<T> leaf:
                items.Add(leaf.Hash);
                break;
        }
    }

    /// <summary>
    /// Recursively iterates through the specified <see cref="MerkleTree{T}"/> to collect leaf values.
    /// </summary>
    /// <param name="current">The current <see cref="MerkleTree{T}"/> from which to begin iterating through.</param>
    /// <param name="items">The list that will contain the leaf values from the current <see cref="MerkleTree{T}"/>.</param>
    private static void CollectLeafValues(MerkleTree<T> current, ICollection<T> items)
    {
        switch (current)
        {
            case MerkleTreeBranchNode<T> branch:
                CollectLeafValues(branch.Left, items);
                CollectLeafValues(branch.Right, items);
                break;
            case MerkleTreeLeafNode<T> leaf:
                items.Add(leaf.Value);
                break;
        }
    }
}
