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
/// Represents benchmarks for EC Diffie-Hellman key agreement on each NIST curve, comparing the hashed shared secret with the raw one, which
/// also checks that the other party's point is on the curve.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class EcdhBenchmarks
{
    private EcdhPrivateKey privateKey = null!;
    private EcdhPublicKey otherPublicKey = null!;

    /// <summary>
    /// Gets or sets the curve to benchmark.
    /// </summary>
    /// <value>The friendly name of the curve.</value>
    [Params("nistP256", "nistP384", "nistP521")]
    public string Curve { get; set; } = "nistP256";

    /// <summary>
    /// Creates the keys used by the benchmarks.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        ECCurve curve = ECCurve.CreateFromFriendlyName(Curve);
        privateKey = EcdhPrivateKey.Create(curve);
        otherPublicKey = EcdhPrivateKey.Create(curve).GetPublicKey();
    }

    /// <summary>
    /// Disposes the private key used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup() => privateKey.Dispose();

    /// <summary>
    /// Derives the hashed shared secret.
    /// </summary>
    /// <returns>Returns the shared secret.</returns>
    [Benchmark(Baseline = true)]
    public Secret DeriveSharedSecret() => privateKey.DeriveSharedSecret(otherPublicKey);

    /// <summary>
    /// Derives the raw shared secret, after validating the other party's public key.
    /// </summary>
    /// <returns>Returns the shared secret.</returns>
    [Benchmark]
    public Secret DeriveRawSharedSecret() => privateKey.DeriveRawSharedSecret(otherPublicKey);
}
