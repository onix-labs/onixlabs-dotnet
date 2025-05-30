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
public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Imports the ECDSA cryptographic private key data in PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The ECDSA cryptographic private key data to import.</param>
    /// <returns>Returns a new ECDSA cryptographic private key from the imported data.</returns>
    public static EcdsaPrivateKey ImportPem(ReadOnlySpan<char> data)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportFromPem(data);
        return new EcdsaPrivateKey(algorithm);
    }

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The ECDSA cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new ECDSA cryptographic private key from the imported data.</returns>
    public static EcdsaPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        return new EcdsaPrivateKey(algorithm);
    }

    /// <summary>
    /// Imports the ECDSA cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The ECDSA cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new ECDSA cryptographic private key from the imported data.</returns>
    public static EcdsaPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        return new EcdsaPrivateKey(algorithm);
    }
}
