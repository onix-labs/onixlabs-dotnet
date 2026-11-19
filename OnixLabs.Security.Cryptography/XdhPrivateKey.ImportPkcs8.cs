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

public sealed partial class XdhPrivateKey
{
    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data) => ImportPkcs8(data, out _);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, out int bytesRead)
    {
        byte[] privateKey = X25519Pkcs8.DecodePrivateKey(data, out bytesRead);

        try
        {
            return new XdhPrivateKey(privateKey);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(privateKey);
        }
    }

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out _);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead)
    {
        Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.DecryptAndDecode(password, data.ToArray(), out bytesRead);
        return FromPkcs8Info(info);
    }

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out _);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password, out int bytesRead)
    {
        Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.DecryptAndDecode(password, data.ToArray(), out bytesRead);
        return FromPkcs8Info(info);
    }

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data) => ImportPkcs8(data.AsReadOnlySpan());

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), out bytesRead);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data.AsReadOnlySpan(), password);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data.AsReadOnlySpan(), password);

    /// <inheritdoc/>
    public static XdhPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <summary>
    /// Constructs an <see cref="XdhPrivateKey"/> from a decoded <see cref="Pkcs8PrivateKeyInfo"/>,
    /// re-encoding to DER, extracting the X25519 private key, and zeroing intermediate buffers.
    /// </summary>
    /// <param name="info">The decoded PKCS#8 private-key information from which to construct the key.</param>
    /// <returns>Returns a new <see cref="XdhPrivateKey"/> initialized from the private key contained in <paramref name="info"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="info"/> does not contain a valid X25519 PKCS#8 private
    /// key.</exception>
    private static XdhPrivateKey FromPkcs8Info(Pkcs8PrivateKeyInfo info)
    {
        byte[] encoded = info.Encode();
        try
        {
            byte[] privateKey = X25519Pkcs8.DecodePrivateKey(encoded, out _);
            try
            {
                return new XdhPrivateKey(privateKey);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(privateKey);
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(encoded);
        }
    }
}
