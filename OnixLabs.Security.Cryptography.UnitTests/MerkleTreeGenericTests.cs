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
using OnixLabs.Core;
using OnixLabs.Security.Cryptography.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class MerkleTreeGenericTests
{
    private readonly IEnumerable<Person> setA = Collections.EnumerableOf(
        new Person("Alice", "Anderson", "1953-05-08".ToDateOnly()),
        new Person("Bob", "Brown", "1971-07-16".ToDateOnly()),
        new Person("Charlie", "Campbell", "2011-03-23".ToDateOnly()),
        new Person("Dave", "Davis", "1988-01-30".ToDateOnly()),
        new Person("Eve", "Everton", "2022-03-05".ToDateOnly())
    );

    private readonly IEnumerable<Person> setB = Collections.EnumerableOf(
        new Person("Frank", "Foster", "1904-04-04".ToDateOnly()),
        new Person("Greg", "Green", "1996-12-30".ToDateOnly()),
        new Person("Holly", "Hamilton", "1993-02-21".ToDateOnly()),
        new Person("Ivan", "Ingram", "1946-05-26".ToDateOnly())
    );

    [Fact(DisplayName = "Identical Merkle trees should be considered equal")]
    public void IdenticalMerkleTreesShouldBeConsideredEqual()
    {
        // Arrange / Act
        MerkleTree<Person> a = MerkleTree.Create(setA);
        MerkleTree<Person> b = MerkleTree.Create(setA);

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
    }

    [Fact(DisplayName = "Different Merkle trees should not be considered equal")]
    public void DifferentMerkleTreesShouldNotBeConsideredEqual()
    {
        // Arrange / Act
        MerkleTree<Person> a = MerkleTree.Create(setA);
        MerkleTree<Person> b = MerkleTree.Create(setB);

        // Assert
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.Hash, b.Hash);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafHashes should produce the same leaf hashes that the tree was constructed with")]
    public void MerkleTreeGetLeafHashesShouldProduceTheSameLeafHashesThatTheTreeWasConstructedWith()
    {
        // Arrange
        IEnumerable<Hash> expected = setA.Select(value => value.ComputeHash());
        MerkleTree<Person> candidate = MerkleTree.Create(setA);

        // Act
        IEnumerable<Hash> actual = candidate.GetLeafHashes();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "MerkleTree.GetLeafValues should produce the same leaf values that the tree was constructed with")]
    public void MerkleTreeGetLeafValuesShouldProduceTheSameLeafValuesThatTheTreeWasConstructedWith()
    {
        // Arrange
        MerkleTree<Person> candidate = MerkleTree.Create(setA);

        // Act
        IEnumerable<Person> actual = candidate.GetLeafValues();

        // Assert
        Assert.Equal(setA, actual);
    }

    [Fact(DisplayName = "MerkleTree.ToMerkleTree should produce a hash-only, non-generic Merkle tree that is equal in value")]
    public void MerkleTreeToMerkleTreeShouldProduceAHashOnlyNonGenericMerkleTreeThatIsEqualInValue()
    {
        // Arrange
        MerkleTree<Person> a = MerkleTree.Create(setA);

        // Act
        MerkleTree b = a.ToMerkleTree();

        // Assert
        Assert.Equal(a, b);
        Assert.Equal(a.Hash, b.Hash);
    }
}
