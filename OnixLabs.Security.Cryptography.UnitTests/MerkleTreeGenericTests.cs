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
using System.Linq;
using System.Security.Cryptography;
using OnixLabs.Core;
using OnixLabs.Security.Cryptography.UnitTests.Data;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class MerkleTreeGenericTests
{
    private static readonly HashAlgorithm Algorithm = SHA256.Create();

    private readonly IEnumerable<MerkleNode> setA =
    [
        new MerkleNode("abc", 123, "1953-05-08T01:00:59Z".ToDateTime(), Guid.Parse("18d1e14c-9762-4b2c-8774-3f053e8579e0")),
        new MerkleNode("def", 456, "1953-05-08T06:30:00Z".ToDateTime(), Guid.Parse("a5d0ad36-f9a6-4fb0-9374-aa658eb19a51")),
        new MerkleNode("hij", 789, "1953-05-08T12:00:33Z".ToDateTime(), Guid.Parse("0d324d48-d451-416e-ac4d-fed1a324d446")),
        new MerkleNode("klm", 101, "1953-05-08T18:30:00Z".ToDateTime(), Guid.Parse("dc2bb378-114e-4f5d-8674-92729a67fe3d")),
        new MerkleNode("nop", 112, "1953-05-08T23:00:59Z".ToDateTime(), Guid.Parse("193863c7-3bfa-4a3c-874a-a99d98b38358"))
    ];

    private readonly IEnumerable<MerkleNode> setB =
    [
        new MerkleNode("qrs", 123, "1900-05-08T01:00:59Z".ToDateTime(), Guid.Parse("893576c9-7cb1-4653-a7c4-21e5ba9e7275")),
        new MerkleNode("tuv", 456, "1901-05-08T06:30:00Z".ToDateTime(), Guid.Parse("e7543a7e-3a52-48f5-842e-9448931f1cde")),
        new MerkleNode("qxy", 789, "1902-05-08T12:00:33Z".ToDateTime(), Guid.Parse("d228ab75-d36a-4392-9c51-888a5b7f9db7")),
        new MerkleNode("zab", 101, "1903-05-08T18:30:00Z".ToDateTime(), Guid.Parse("f73180c0-1fc8-4492-ae5d-f0b8962177a8"))
    ];

    [Fact(DisplayName = "Identical Merkle trees should be considered equal")]
    public void IdenticalMerkleTreesShouldBeConsideredEqual()
    {
        // Given / When
        MerkleTree<MerkleNode> a = MerkleTree.Create(setA, Algorithm);
        MerkleTree<MerkleNode> b = MerkleTree.Create(setA, Algorithm);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }

    [Fact(DisplayName = "Different Merkle trees should not be considered equal")]
    public void DifferentMerkleTreesShouldNotBeConsideredEqual()
    {
        // Given / When
        MerkleTree<MerkleNode> a = MerkleTree.Create(setA, Algorithm);
        MerkleTree<MerkleNode> b = MerkleTree.Create(setB, Algorithm);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.Hash, b.Hash);
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
        Assert.True(a.GetHashCode() != b.GetHashCode());
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should produce the same leaf hashes that the tree was constructed with")]
    public void MerkleTreeGetLeafHashesShouldProduceTheSameLeafHashesThatTheTreeWasConstructedWith()
    {
        // Given
        IEnumerable<Hash> expected = setA.Select(value => value.ComputeHash(Algorithm));
        MerkleTree<MerkleNode> candidate = MerkleTree.Create(setA, Algorithm);

        // When
        IEnumerable<Hash> actual = candidate.GetLeafHashes();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafValues should produce the same leaf values that the tree was constructed with")]
    public void MerkleTreeGetLeafValuesShouldProduceTheSameLeafValuesThatTheTreeWasConstructedWith()
    {
        // Given
        MerkleTree<MerkleNode> candidate = MerkleTree.Create(setA, Algorithm);

        // When
        IEnumerable<MerkleNode> actual = candidate.GetLeafValues();

        // Then
        Assert.Equal(setA, actual);
    }

    [Fact(DisplayName = "MerkleTree.ToMerkleTree should produce a hash-only, non-generic Merkle tree that is equal in value")]
    public void MerkleTreeToMerkleTreeShouldProduceAHashOnlyNonGenericMerkleTreeThatIsEqualInValue()
    {
        // Given
        MerkleTree<MerkleNode> a = MerkleTree.Create(setA, Algorithm);

        // When
        MerkleTree b = a.ToMerkleTree(Algorithm);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
    }
}
