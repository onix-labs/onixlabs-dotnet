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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EcdhPublicKey
{
    /// <inheritdoc/>
    public static EcdhPublicKey Import(IBinaryConvertible data) =>
        Import(data.AsReadOnlySpan());

    /// <inheritdoc/>
    public static EcdhPublicKey Import(IBinaryConvertible data, out int bytesRead) =>
        Import(data.AsReadOnlySpan(), out bytesRead);

    /// <inheritdoc/>
    public static EcdhPublicKey Import(ReadOnlySpan<byte> data) =>
        Import(data, out int _);

    /// <inheritdoc/>
    public static EcdhPublicKey Import(ReadOnlySpan<byte> data, out int bytesRead)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportSubjectPublicKeyInfo(data, out bytesRead);
        return new EcdhPublicKey(algorithm);
    }

    /// <summary>
    /// Imports the key data into a new <see cref="ECDiffieHellman"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="ECDiffieHellman"/> instance containing the imported key data.</returns>
    private ECDiffieHellman ImportKeyData()
    {
        ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportSubjectPublicKeyInfo(KeyData, out int _);
        return algorithm;
    }
}
