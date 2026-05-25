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
using System.Security.Cryptography.Pkcs;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EddsaPrivateKey
{
    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data) => ImportPkcs8(data, out _);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, out int bytesRead)
    {
        byte[] seed = Ed25519Pkcs8.DecodePrivateKey(data, out bytesRead);

        try
        {
            return new EddsaPrivateKey(seed);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out _);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead)
    {
        Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.DecryptAndDecode(password, data.ToArray(), out bytesRead);
        return FromPkcs8Info(info);
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out _);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password, out int bytesRead)
    {
        Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.DecryptAndDecode(password, data.ToArray(), out bytesRead);
        return FromPkcs8Info(info);
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data) => ImportPkcs8(data.AsReadOnlySpan());

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), out bytesRead);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data.AsReadOnlySpan(), password);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data.AsReadOnlySpan(), password);

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <summary>
    /// Constructs an <see cref="EddsaPrivateKey"/> from a decoded <see cref="Pkcs8PrivateKeyInfo"/>,
    /// re-encoding to DER, extracting the Ed25519 seed, and zeroing intermediate buffers.
    /// </summary>
    /// <param name="info">The decoded PKCS#8 private-key information from which to construct the key.</param>
    /// <returns>Returns a new <see cref="EddsaPrivateKey"/> initialized from the seed contained in <paramref name="info"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="info"/> does not contain a valid Ed25519 PKCS#8 private key.</exception>
    private static EddsaPrivateKey FromPkcs8Info(Pkcs8PrivateKeyInfo info)
    {
        byte[] encoded = info.Encode();
        try
        {
            byte[] seed = Ed25519Pkcs8.DecodePrivateKey(encoded, out _);
            try
            {
                return new EddsaPrivateKey(seed);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(seed);
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(encoded);
        }
    }
}
