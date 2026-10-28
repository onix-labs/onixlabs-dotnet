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
/// Represents benchmarks for the protected-key-material operations of <see cref="PrivateKey"/> and <see cref="Secret"/>:
/// equality, hash codes, formatting, and hashing text that may be a password.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class PrivateKeyBenchmarks
{
    private const string Password = "correct horse battery staple";

    private readonly EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP256);
    private readonly EcdsaPrivateKey samePrivateKey;
    private readonly Secret secret = new(RandomNumberGenerator.GetBytes(32));
    private readonly SHA256 sha256 = SHA256.Create();

    /// <summary>
    /// Initializes a new instance of the <see cref="PrivateKeyBenchmarks"/> class.
    /// </summary>
    public PrivateKeyBenchmarks() => samePrivateKey = EcdsaPrivateKey.Import(privateKey.Export());

    /// <summary>
    /// Disposes the keys, secret and hash algorithm used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        privateKey.Dispose();
        samePrivateKey.Dispose();
        secret.Dispose();
        sha256.Dispose();
    }

    /// <summary>
    /// Compares two distinct private key instances holding the same key.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the keys are equal; otherwise, <see langword="false"/>.</returns>
    [Benchmark(Baseline = true)]
    public bool PrivateKeyEquals() => privateKey.Equals(samePrivateKey);

    /// <summary>
    /// Computes the hash code of a private key, as a dictionary lookup does.
    /// </summary>
    /// <returns>Returns the hash code.</returns>
    [Benchmark]
    public int PrivateKeyGetHashCode() => privateKey.GetHashCode();

    /// <summary>
    /// Formats a private key as Base16.
    /// </summary>
    /// <returns>Returns the formatted string.</returns>
    [Benchmark]
    public string PrivateKeyToString() => privateKey.ToString();

    /// <summary>
    /// Formats a secret as Base16.
    /// </summary>
    /// <returns>Returns the formatted string.</returns>
    [Benchmark]
    public string SecretToString() => secret.ToString();

    /// <summary>
    /// Computes the SHA-256 hash of a short text, as hashing a password does.
    /// </summary>
    /// <returns>Returns the computed hash.</returns>
    [Benchmark]
    public Hash ComputeHashOfText() => Hash.Compute(sha256, Password);
}
