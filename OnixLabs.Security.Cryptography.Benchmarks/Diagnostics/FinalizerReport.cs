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
using System.IO;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography.Benchmarks.Diagnostics;

/// <summary>
/// Represents a report of how many finalizers run per operation on the library's native-resource-owning paths.
/// </summary>
/// <remarks>
/// The expected result for every scenario is 0.00: anything else means a native handle reached the finalizer rather than being disposed.
/// Scenarios that deliberately leave the library's own objects undisposed check that the library does not depend on callers disposing
/// them to release its internal native resources.
/// </remarks>
internal static class FinalizerReport
{
    private const int Iterations = 2000;
    private const int NameWidth = 56;

    /// <summary>
    /// Runs every scenario and writes the finalizers per operation for each.
    /// </summary>
    /// <param name="writer">The writer to which the report is written.</param>
    public static void Run(TextWriter writer)
    {
        using FinalizerCounter counter = new();
        using Scenarios scenarios = new();

        writer.WriteLine($"Finalizers per operation, averaged over {Iterations:N0} operations (expected: 0.00).");
        writer.WriteLine();
        writer.WriteLine($"{"Scenario".PadRight(NameWidth)} Per operation");

        foreach ((string name, Action operation) in scenarios.GetAll())
        {
            operation();
            FinalizerCounter.Settle();
            long before = counter.Count;

            for (int iteration = 0; iteration < Iterations; iteration++)
                operation();

            FinalizerCounter.Settle();
            double perOperation = (double)(counter.Count - before) / Iterations;
            writer.WriteLine($"{name.PadRight(NameWidth)} {perOperation,13:F2}");
        }
    }

    /// <summary>
    /// Represents the scenarios measured by the report, and the long-lived objects they share.
    /// </summary>
    private sealed class Scenarios : IDisposable
    {
        private readonly byte[] data = RandomNumberGenerator.GetBytes(1024);
        private readonly EcdsaPrivateKey ecdsaKey = EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP256);
        private readonly EcdhPrivateKey ecdhKey = EcdhPrivateKey.Create();
        private readonly EddsaPrivateKey eddsaKey = EddsaPrivateKey.Create();
        private readonly RsaPrivateKey rsaKey = RsaPrivateKey.Create();
        private readonly byte[] exportedEcdsaKey;
        private readonly EcdsaPublicKey ecdsaPublicKey;
        private readonly EcdhPublicKey ecdhPublicKey;
        private readonly byte[] ecdsaSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scenarios"/> class.
        /// </summary>
        public Scenarios()
        {
            exportedEcdsaKey = ecdsaKey.Export();
            ecdsaPublicKey = ecdsaKey.GetPublicKey();
            ecdhPublicKey = ecdhKey.GetPublicKey();
            ecdsaSignature = ecdsaKey.SignData(data, HashAlgorithmName.SHA256);
        }

        /// <summary>
        /// Gets every scenario, with its display name.
        /// </summary>
        /// <returns>Returns every scenario, with its display name.</returns>
        public IEnumerable<(string Name, Action Operation)> GetAll() =>
        [
            ("ECDSA sign, long-lived key", () => ecdsaKey.SignData(data, HashAlgorithmName.SHA256)),
            ("ECDSA verify", () => ecdsaPublicKey.IsDataValid(ecdsaSignature, data, HashAlgorithmName.SHA256)),
            ("ECDSA import, sign and dispose", ImportSignAndDispose),
            ("ECDSA import and sign, key not disposed", () => EcdsaPrivateKey.Import(exportedEcdsaKey).SignData(data, HashAlgorithmName.SHA256)),
            ("ECDH derive, secret disposed", () => ecdhKey.DeriveSharedSecret(ecdhPublicKey).Dispose()),
            ("ECDH derive, secret not disposed", () => ecdhKey.DeriveSharedSecret(ecdhPublicKey)),
            ("EdDSA sign", () => eddsaKey.SignData(data)),
            ("RSA sign", () => rsaKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pss)),
            ("Secret created, not disposed", () => _ = new Secret(data)),
        ];

        /// <inheritdoc/>
        public void Dispose()
        {
            ecdsaKey.Dispose();
            ecdhKey.Dispose();
            eddsaKey.Dispose();
            rsaKey.Dispose();
        }

        /// <summary>
        /// Imports the ECDSA key, signs the data with it, then disposes it.
        /// </summary>
        private void ImportSignAndDispose()
        {
            using EcdsaPrivateKey key = EcdsaPrivateKey.Import(exportedEcdsaKey);
            key.SignData(data, HashAlgorithmName.SHA256);
        }
    }
}
