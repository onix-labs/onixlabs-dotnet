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
/// Represents benchmarks for X25519 key creation, public key derivation and key agreement.
/// </summary>
/// <remarks>
/// <see cref="XdhPrivateKey"/> uses the platform's <c>X25519DiffieHellman</c> where it is supported, on .NET 11, and the managed code
/// everywhere else, so the managed code is also measured on its own. On .NET 11, the platform's implementation is measured directly too.
/// </remarks>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class XdhBenchmarks
{
    private readonly XdhPrivateKey privateKey = XdhPrivateKey.Create();
    private readonly XdhPublicKey otherPublicKey = XdhPrivateKey.Create().GetPublicKey();
    private readonly X25519 managedKey = X25519.GenerateKey(usePlatform: false);
    private readonly byte[] managedOtherPublicKey = X25519.GenerateKey(usePlatform: false).ExportPublicKey();
    private readonly byte[] sharedSecret = new byte[X25519.SecretAgreementSizeInBytes];
#if NET11_0_OR_GREATER
    private readonly X25519DiffieHellman platformKey = X25519DiffieHellman.GenerateKey();
    private readonly byte[] platformOtherPublicKey = X25519DiffieHellman.GenerateKey().ExportPublicKey();
#endif

    /// <summary>
    /// Disposes the keys used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        privateKey.Dispose();
        managedKey.Dispose();
#if NET11_0_OR_GREATER
        platformKey.Dispose();
#endif
    }

    /// <summary>
    /// Derives the raw shared secret with another party's public key.
    /// </summary>
    /// <returns>Returns the raw shared secret.</returns>
    [Benchmark(Baseline = true)]
    public Secret DeriveRawSharedSecret() => privateKey.DeriveRawSharedSecret(otherPublicKey);

    /// <summary>
    /// Derives the hashed shared secret with another party's public key.
    /// </summary>
    /// <returns>Returns the hashed shared secret.</returns>
    [Benchmark]
    public Secret DeriveSharedSecret() => privateKey.DeriveSharedSecret(otherPublicKey);

    /// <summary>
    /// Derives the raw shared secret with the managed code alone, whatever the platform supports.
    /// </summary>
    [Benchmark]
    public void ManagedDeriveRawSecretAgreement() => managedKey.DeriveRawSecretAgreement(managedOtherPublicKey, sharedSecret);

    /// <summary>
    /// Derives the public key from the private key.
    /// </summary>
    /// <returns>Returns the public key.</returns>
    [Benchmark]
    public XdhPublicKey GetPublicKey() => privateKey.GetPublicKey();

    /// <summary>
    /// Creates a new random private key.
    /// </summary>
    /// <returns>Returns the private key.</returns>
    [Benchmark]
    public XdhPrivateKey Create() => XdhPrivateKey.Create();

#if NET11_0_OR_GREATER
    /// <summary>
    /// Derives the shared secret with the platform's X25519, for reference.
    /// </summary>
    /// <returns>Returns the shared secret.</returns>
    [Benchmark]
    public byte[] PlatformDeriveRawSecretAgreement() => platformKey.DeriveRawSecretAgreement(platformOtherPublicKey);
#endif
}
