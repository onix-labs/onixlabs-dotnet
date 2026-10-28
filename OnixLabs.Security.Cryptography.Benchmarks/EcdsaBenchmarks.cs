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
/// Represents benchmarks for ECDSA signing and verification with P-256 and SHA-256 over 1 KiB of data.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class EcdsaBenchmarks
{
    private readonly byte[] data = RandomNumberGenerator.GetBytes(1024);
    private readonly EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP256);
    private readonly byte[] exportedPrivateKey;
    private readonly EcdsaPublicKey publicKey;
    private readonly byte[] signature;

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaBenchmarks"/> class.
    /// </summary>
    public EcdsaBenchmarks()
    {
        exportedPrivateKey = privateKey.Export();
        publicKey = privateKey.GetPublicKey();
        signature = privateKey.SignData(data, HashAlgorithmName.SHA256);
    }

    /// <summary>
    /// Disposes the private key used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup() => privateKey.Dispose();

    /// <summary>
    /// Signs the data with a long-lived private key.
    /// </summary>
    /// <returns>Returns the signature.</returns>
    [Benchmark(Baseline = true)]
    public byte[] SignData() => privateKey.SignData(data, HashAlgorithmName.SHA256);

    /// <summary>
    /// Imports a private key, signs the data with it, then disposes it, as a per-request signing service would.
    /// </summary>
    /// <returns>Returns the signature.</returns>
    [Benchmark]
    public byte[] ImportAndSignData()
    {
        using EcdsaPrivateKey key = EcdsaPrivateKey.Import(exportedPrivateKey);
        return key.SignData(data, HashAlgorithmName.SHA256);
    }

    /// <summary>
    /// Verifies a signature over the data.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark]
    public bool IsDataValid() => publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256);
}
