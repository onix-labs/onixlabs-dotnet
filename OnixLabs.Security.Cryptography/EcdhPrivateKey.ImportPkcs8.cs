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
    /// Imports the EC Diffie-Hellman cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data) =>
        ImportPkcs8(data, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data) =>
        ImportPkcs8(data, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out int _);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), out bytesRead);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, out int bytesRead)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportPkcs8PrivateKey(data, out bytesRead);
        return new EcdhPrivateKey(algorithm);
    }

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportEncryptedPkcs8PrivateKey(password, data, out bytesRead);
        return new EcdhPrivateKey(algorithm);
    }

    /// <summary>
    /// Imports the EC Diffie-Hellman cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="data">The EC Diffie-Hellman cryptographic private key data to import.</param>
    /// <param name="password">The password required for password based decryption.</param>
    /// <param name="bytesRead">The number of bytes read from the input data.</param>
    /// <returns>Returns a new EC Diffie-Hellman cryptographic private key from the imported data.</returns>
    public static EcdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password, out int bytesRead)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportEncryptedPkcs8PrivateKey(password, data, out bytesRead);
        return new EcdhPrivateKey(algorithm);
    }
}
