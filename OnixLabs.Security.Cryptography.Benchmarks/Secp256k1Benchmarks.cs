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
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents benchmarks for secp256k1 ECDSA verification (ES256K), which is managed code on every platform, compared with P-256
/// verification
/// by the platform.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class Secp256k1Benchmarks
{
    /// <summary>
    /// The DER SubjectPublicKeyInfo of the first test group of Project Wycheproof's <c>ecdsa_secp256k1_sha256_p1363_test.json</c>.
    /// </summary>
    private const string Secp256k1PublicKey =
        "3056301006072a8648ce3d020106052b8104000a03420004b838ff44e5bc177bf21189d0766082fc9d843226887fc9760371100b7ee20a6f" +
        "f0c9d75bfba7b31a6bca1974496eeb56de357071955d83c4b1badaa0b21832e9";

    /// <summary>
    /// The message of that group's tcId 1.
    /// </summary>
    private const string Secp256k1Message = "313233343030";

    /// <summary>
    /// The valid IEEE P1363 signature of that group's tcId 1.
    /// </summary>
    private const string Secp256k1Signature =
        "813ef79ccefa9a56f7ba805f0e478584fe5f0dd5f567bc09b5123ccbc9832365900e75ad233fcc908509dbff5922647db37c21f4afd3203a" +
        "e8dc4ae7794b0f87";

    private readonly byte[] secp256k1SubjectPublicKeyInfo = Convert.FromHexString(Secp256k1PublicKey);
    private readonly byte[] secp256k1Message = Convert.FromHexString(Secp256k1Message);
    private readonly byte[] secp256k1Signature = Convert.FromHexString(Secp256k1Signature);
    private readonly byte[] p256Message = RandomNumberGenerator.GetBytes(6);
    private EcdsaPublicKey secp256k1Key = null!;
    private EcdsaPublicKey p256Key = null!;
    private byte[] p256Signature = null!;

    /// <summary>
    /// Imports the keys and creates the P-256 signature used by the benchmarks.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        secp256k1Key = EcdsaPublicKey.Import(secp256k1SubjectPublicKeyInfo);
        using EcdsaPrivateKey p256PrivateKey = EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP256);
        p256Key = p256PrivateKey.GetPublicKey();
        p256Signature = p256PrivateKey.SignData(p256Message, HashAlgorithmName.SHA256);
    }

    /// <summary>
    /// Verifies a secp256k1 signature in managed code.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark]
    public bool Secp256k1IsDataValid() => secp256k1Key.IsDataValid(secp256k1Signature, secp256k1Message, HashAlgorithmName.SHA256);

    /// <summary>
    /// Imports and validates a secp256k1 public key.
    /// </summary>
    /// <returns>Returns the public key.</returns>
    [Benchmark]
    public EcdsaPublicKey Secp256k1Import() => EcdsaPublicKey.Import(secp256k1SubjectPublicKeyInfo);

    /// <summary>
    /// Verifies a P-256 signature with the platform, for reference.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark(Baseline = true)]
    public bool P256IsDataValid() => p256Key.IsDataValid(p256Signature, p256Message, HashAlgorithmName.SHA256);
}
