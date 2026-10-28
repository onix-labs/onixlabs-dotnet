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

using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents benchmarks for Ed25519 key derivation, signing and verification over 1 KiB of data.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class EddsaBenchmarks
{
    private readonly byte[] data = RandomNumberGenerator.GetBytes(1024);
    private readonly EddsaPrivateKey privateKey = EddsaPrivateKey.Create();
    private readonly EddsaPublicKey publicKey;
    private readonly byte[] signature;

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaBenchmarks"/> class.
    /// </summary>
    public EddsaBenchmarks()
    {
        publicKey = privateKey.GetPublicKey();
        signature = privateKey.SignData(data);
    }

    /// <summary>
    /// Disposes the private key used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup() => privateKey.Dispose();

    /// <summary>
    /// Signs the data.
    /// </summary>
    /// <returns>Returns the signature.</returns>
    [Benchmark(Baseline = true)]
    public byte[] SignData() => privateKey.SignData(data);

    /// <summary>
    /// Verifies a signature over the data.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark]
    public bool IsDataValid() => publicKey.IsDataValid(signature, data);

    /// <summary>
    /// Derives the public key from the private key.
    /// </summary>
    /// <returns>Returns the public key.</returns>
    [Benchmark]
    public EddsaPublicKey GetPublicKey() => privateKey.GetPublicKey();
}
