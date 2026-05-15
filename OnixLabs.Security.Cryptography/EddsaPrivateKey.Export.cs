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

namespace OnixLabs.Security.Cryptography;

public sealed partial class EddsaPrivateKey
{
    /// <summary>
    /// The RFC 7468 PEM label used for unencrypted PKCS#8 private keys.
    /// </summary>
    private const string Pkcs8Label = "PRIVATE KEY";

    /// <summary>
    /// The RFC 7468 PEM label used for encrypted PKCS#8 private keys.
    /// </summary>
    private const string EncryptedPkcs8Label = "ENCRYPTED PRIVATE KEY";

    /// <inheritdoc/>
    public byte[] Export() => KeyData;

    /// <inheritdoc/>
    public byte[] ExportPkcs8()
    {
        byte[] seed = KeyData;

        try
        {
            return Ed25519Pkcs8.EncodePrivateKey(seed);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <inheritdoc/>
    public byte[] ExportPkcs8(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        byte[] pkcs8 = ExportPkcs8();

        try
        {
            Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.Decode(pkcs8, out _, skipCopy: false);
            return info.Encrypt(password, parameters);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(pkcs8);
        }
    }

    /// <inheritdoc/>
    public byte[] ExportPkcs8(ReadOnlySpan<byte> password, PbeParameters parameters)
    {
        byte[] pkcs8 = ExportPkcs8();

        try
        {
            Pkcs8PrivateKeyInfo info = Pkcs8PrivateKeyInfo.Decode(pkcs8, out _, skipCopy: false);
            return info.Encrypt(password, parameters);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(pkcs8);
        }
    }

    /// <inheritdoc/>
    public string ExportPkcs8Pem() => PemEncoding.WriteString(Pkcs8Label, ExportPkcs8());

    /// <inheritdoc/>
    public string ExportPkcs8Pem(ReadOnlySpan<char> password, PbeParameters parameters) =>
        PemEncoding.WriteString(EncryptedPkcs8Label, ExportPkcs8(password, parameters));

    /// <inheritdoc/>
    public string ExportPkcs8Pem(ReadOnlySpan<byte> password, PbeParameters parameters) =>
        PemEncoding.WriteString(EncryptedPkcs8Label, ExportPkcs8(password, parameters));
}
