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
using System.Collections.Immutable;

namespace OnixLabs.Security.Cryptography;

public abstract partial class MerkleTree
{
    /// <summary>
    /// Obtains the leaf hashes from the current <see cref="MerkleTree"/>.
    /// </summary>
    /// <returns>Returns an <see cref="IEnumerable{T}"/> containing the leaf hashes from the current <see cref="MerkleTree"/>.</returns>
    public IReadOnlyList<Hash> GetLeafHashes()
    {
        ICollection<Hash> result = EmptyList<Hash>();
        CollectLeafHashes(this, result);
        return result.ToImmutableList();
    }

    /// <summary>
    /// Recursively iterates through the specified <see cref="MerkleTree"/> to collect leaf hashes.
    /// </summary>
    /// <param name="current">The current <see cref="MerkleTree"/> from which to begin iterating through.</param>
    /// <param name="items">The list that will contain the leaf hashes from the current <see cref="MerkleTree"/>.</param>
    protected virtual void CollectLeafHashes(MerkleTree current, ICollection<Hash> items)
    {
        switch (current)
        {
            case MerkleTreeBranchNode branch:
                CollectLeafHashes(branch.Left, items);
                CollectLeafHashes(branch.Right, items);
                break;
            case MerkleTreeLeafNode leaf:
                items.Add(leaf.Hash);
                break;
        }
    }
}
