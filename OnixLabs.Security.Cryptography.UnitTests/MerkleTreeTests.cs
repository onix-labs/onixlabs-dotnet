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
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class MerkleTreeTests
{
    private static readonly HashAlgorithm Algorithm = SHA256.Create();

    private readonly IEnumerable<Hash> setA = Enumerable.Range(123, 1357).Select(i => Hash.Compute(Algorithm, $"A{i}")).ToList();
    private readonly IEnumerable<Hash> setB = Enumerable.Range(100, 1000).Select(i => Hash.Compute(Algorithm, $"B{i}")).ToList();

    [Fact(DisplayName = "Identical Merkle trees should be considered equal")]
    public void IdenticalMerkleTreesShouldBeConsideredEqual()
    {
        // Given / When
        MerkleTree a = MerkleTree.Create(setA, Algorithm);
        MerkleTree b = MerkleTree.Create(setA, Algorithm);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Different Merkle trees should not be considered equal")]
    public void DifferentMerkleTreesShouldNotBeConsideredEqual()
    {
        // Given / When
        MerkleTree a = MerkleTree.Create(setA, Algorithm);
        MerkleTree b = MerkleTree.Create(setB, Algorithm);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.Hash, b.Hash);
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should produce the same leaf hashes that the tree was constructed with")]
    public void MerkleTreeGetLeafHashesShouldProduceTheSameLeafHashesThatTheTreeWasConstructedWith()
    {
        // Given
        MerkleTree candidate = MerkleTree.Create(setA, Algorithm);

        // When
        IEnumerable<Hash> actual = candidate.GetLeafHashes();

        // Then
        Assert.Equal(setA, actual);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should obtain all leaf hashes from a Merkle tree constructed with 1 million hashes")]
    public void MerkleTreeGetLeafHashesShouldObtainAllLeafHashesFromAMerkleTreeConstructedWith1MillionHashes()
    {
        // Given
        IEnumerable<Hash> expected = Enumerable
            .Range(0, 1_000_000)
            .Select(value => Hash.Compute(Algorithm, value.ToString()))
            .ToList();

        MerkleTree tree = MerkleTree.Create(expected, Algorithm);

        // When
        IEnumerable<Hash> actual = tree.GetLeafHashes();

        // Then
        Assert.Equal(expected, actual);
    }
}
