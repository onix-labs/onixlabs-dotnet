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

using System.Linq;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents benchmarks for hashing, hash concatenation, hash codes and Merkle tree construction.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class HashBenchmarks
{
    private const int MerkleLeafCount = 256;

    private readonly byte[] data = RandomNumberGenerator.GetBytes(1024);
    private readonly SHA256 sha256 = SHA256.Create();
    private readonly Sha3Hash256 sha3 = Sha3.CreateSha3Hash256();
    private readonly Hash left;
    private readonly Hash right;
    private readonly Hash[] leaves;

    /// <summary>
    /// Initializes a new instance of the <see cref="HashBenchmarks"/> class.
    /// </summary>
    public HashBenchmarks()
    {
        left = Hash.Compute(sha256, data);
        right = Hash.Compute(sha256, left.AsReadOnlySpan());
        leaves = Enumerable.Range(0, MerkleLeafCount).Select(index => Hash.Compute(sha256, [(byte)index, (byte)(index >> 8)])).ToArray();
    }

    /// <summary>
    /// Disposes the hash algorithms used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        sha256.Dispose();
        sha3.Dispose();
    }

    /// <summary>
    /// Computes the SHA-256 hash of 1 KiB of data.
    /// </summary>
    /// <returns>Returns the computed hash.</returns>
    [Benchmark(Baseline = true)]
    public Hash ComputeSha256() => Hash.Compute(sha256, data);

    /// <summary>
    /// Computes the SHA3-256 hash of 1 KiB of data, using the library's managed Keccak implementation.
    /// </summary>
    /// <returns>Returns the computed hash.</returns>
    [Benchmark]
    public Hash ComputeSha3() => Hash.Compute(sha3, data);

    /// <summary>
    /// Computes the SHA-256 hash of the concatenation of two hashes, as a Merkle tree branch does.
    /// </summary>
    /// <returns>Returns the computed hash.</returns>
    [Benchmark]
    public Hash Concatenate() => Hash.Concatenate(sha256, left, right);

    /// <summary>
    /// Computes the hash code of a 32-byte hash, as a dictionary lookup does.
    /// </summary>
    /// <returns>Returns the hash code.</returns>
    [Benchmark]
    public int GetHashCodeOfHash() => left.GetHashCode();

    /// <summary>
    /// Builds a Merkle tree from 256 leaf hashes and reads its leaf hashes back.
    /// </summary>
    /// <returns>Returns the number of leaf hashes in the tree.</returns>
    [Benchmark]
    public int CreateMerkleTree() => MerkleTree.Create(leaves, sha256).GetLeafHashes().Count;
}
