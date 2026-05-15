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
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Benchmarks for the Ed25519 (RFC 8032) implementation in <c>OnixLabs.Security.Cryptography</c>.
/// </summary>
[MemoryDiagnoser]
public class EddsaBenchmarks
{
    private EddsaPrivateKey privateKey = null!;
    private EddsaPublicKey publicKey = null!;
    private byte[] message = null!;
    private byte[] signature = null!;

    [GlobalSetup]
    public void Setup()
    {
        privateKey = EddsaPrivateKey.Create();
        publicKey = privateKey.GetPublicKey();
        message = new byte[64];
        new Random(42).NextBytes(message);
        signature = privateKey.SignData(message);
    }

    [Benchmark(Description = "EddsaPrivateKey.Create — key generation")]
    public EddsaPrivateKey CreateKey() => EddsaPrivateKey.Create();

    [Benchmark(Description = "EddsaPrivateKey.SignData — 64-byte message")]
    public byte[] SignData() => privateKey.SignData(message);

    [Benchmark(Description = "EddsaPublicKey.IsDataValid — 64-byte message")]
    public bool VerifyData() => publicKey.IsDataValid(signature, message);
}
