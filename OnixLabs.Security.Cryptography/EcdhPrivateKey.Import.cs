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

public sealed partial class EcdhPrivateKey
{
    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey Import(IBinaryConvertible data) =>
        Import(data.AsReadOnlySpan());

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey Import(IBinaryConvertible data, out int bytesRead) =>
        Import(data.AsReadOnlySpan(), out bytesRead);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey Import(ReadOnlySpan<byte> data) =>
        Import(data, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey Import(ReadOnlySpan<byte> data, out int bytesRead)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportECPrivateKey(data, out bytesRead);
        return new EcdhPrivateKey(algorithm);
    }

    /// <summary>
    /// Imports the key data into a new <see cref="ECDiffieHellman"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="ECDiffieHellman"/> instance containing the imported key data.</returns>
    private ECDiffieHellman ImportKeyData()
    {
        ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportECPrivateKey(KeyData, out int _);
        return algorithm;
    }
}
