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
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class MerkleTreeTests
{
    private readonly IEnumerable<Hash> setA = Enumerable.Range(123, 1357).Select(i => Hash.ComputeSha2Hash256($"A{i}")).ToList();
    private readonly IEnumerable<Hash> setB = Enumerable.Range(100, 1000).Select(i => Hash.ComputeSha2Hash256($"B{i}")).ToList();

    [Fact(DisplayName = "Identical Merkle trees should be considered equal")]
    public void IdenticalMerkleTreesShouldBeConsideredEqual()
    {
        // Arrange / Act
        MerkleTree a = MerkleTree.Create(setA);
        MerkleTree b = MerkleTree.Create(setA);

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
    }

    [Fact(DisplayName = "Different Merkle trees should not be considered equal")]
    public void DifferentMerkleTreesShouldNotBeConsideredEqual()
    {
        // Arrange / Act
        MerkleTree a = MerkleTree.Create(setA);
        MerkleTree b = MerkleTree.Create(setB);

        // Assert
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.Hash, b.Hash);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should produce the same leaf hashes that the tree was constructed with")]
    public void MerkleTreeGetLeafHashesShouldProduceTheSameLeafHashesThatTheTreeWasConstructedWith()
    {
        // Arrange
        MerkleTree candidate = MerkleTree.Create(setA);

        // Act
        IEnumerable<Hash> actual = candidate.GetLeafHashes();

        // Assert
        Assert.Equal(setA, actual);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should obtain all leaf hashes from a Merkle tree constructed with 1 million hashes")]
    public void MerkleTreeGetLeafHashesShouldObtainAllLeafHashesFromAMerkleTreeConstructedWith1MillionHashes()
    {
        // Arrange
        IEnumerable<Hash> expected = Enumerable
            .Range(0, 1_000_000)
            .Select(value => Hash.ComputeSha2Hash256(value.ToString()))
            .ToList();

        MerkleTree tree = MerkleTree.Create(expected);

        // Act
        IEnumerable<Hash> actual = tree.GetLeafHashes();

        // Assert
        Assert.Equal(expected, actual);
    }
}
