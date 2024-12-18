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

namespace OnixLabs.Security.Cryptography;

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EcdhPublicKey
{
    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic public key data in PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic public key data to import.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic public key from the imported data.</returns>
    public static EcdhPublicKey ImportPem(ReadOnlySpan<char> data)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromPem(data);
        return new EcdhPublicKey(algorithm);
    }

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic public key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic public key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic public key from the imported data.</returns>
    public static EcdhPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        return new EcdhPublicKey(algorithm);
    }

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic public key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic public key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic public key from the imported data.</returns>
    public static EcdhPublicKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        return new EcdhPublicKey(algorithm);
    }
}
