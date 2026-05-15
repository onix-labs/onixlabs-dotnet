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

public sealed partial class EddsaPrivateKey
{
    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPem(ReadOnlySpan<char> data)
    {
        (string label, byte[] der) = DecodePem(data);
        try
        {
            return label switch
            {
                Pkcs8Label => ImportPkcs8(der),
                EncryptedPkcs8Label => throw new CryptographicException(
                    "PEM label is ENCRYPTED PRIVATE KEY but no password was supplied."),
                _ => throw new CryptographicException(
                    $"Unsupported PEM label for Ed25519 private key: '{label}'."),
            };
        }
        finally
        {
            CryptographicOperations.ZeroMemory(der);
        }
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        (string label, byte[] der) = DecodePem(data);
        try
        {
            if (label != EncryptedPkcs8Label)
            {
                throw new CryptographicException(
                    $"Expected ENCRYPTED PRIVATE KEY PEM label, got '{label}'.");
            }
            return ImportPkcs8(der, password);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(der);
        }
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        (string label, byte[] der) = DecodePem(data);
        try
        {
            if (label != EncryptedPkcs8Label)
            {
                throw new CryptographicException(
                    $"Expected ENCRYPTED PRIVATE KEY PEM label, got '{label}'.");
            }
            return ImportPkcs8(der, password);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(der);
        }
    }

    private static (string Label, byte[] Der) DecodePem(ReadOnlySpan<char> pem)
    {
        PemFields fields = PemEncoding.Find(pem);
        string label = pem[fields.Label].ToString();
        byte[] der = new byte[fields.DecodedDataLength];
        if (!Convert.TryFromBase64Chars(pem[fields.Base64Data], der, out int written) || written != der.Length)
        {
            throw new CryptographicException("Invalid Base64 content in PEM.");
        }
        return (label, der);
    }
}
