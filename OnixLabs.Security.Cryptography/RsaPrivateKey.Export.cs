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

public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Exports the RSA cryptographic private key data.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the RSA cryptographic private key data.</returns>
    public byte[] Export()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportRSAPrivateKey();
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the RSA cryptographic private key data in PKCS #8 format.</returns>
    public byte[] ExportPkcs8()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportPkcs8PrivateKey();
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the RSA cryptographic private key data in PKCS #8 format.</returns>
    public byte[] ExportPkcs8(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKey(password, parameters);
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the RSA cryptographic private key data in PKCS #8 format.</returns>
    public byte[] ExportPkcs8(ReadOnlySpan<byte> password, PbeParameters parameters)
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKey(password, parameters);
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in RFC 7468 PEM format.
    /// </summary>
    /// <returns>Returns a new <see cref="string"/> instance containing the RSA cryptographic private key data in RFC 7468 PEM format.</returns>
    public string ExportPem()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportRSAPrivateKeyPem();
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <returns>Returns a new <see cref="string"/> instance containing the RSA cryptographic private key data in PKCS #8 RFC 7468 PEM format.</returns>
    public string ExportPkcs8Pem()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportPkcs8PrivateKeyPem();
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns a new <see cref="string"/> instance containing the RSA cryptographic private key data in PKCS #8 RFC 7468 PEM format.</returns>
    public string ExportPkcs8Pem(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKeyPem(password, parameters);
    }

    /// <summary>
    /// Exports the RSA cryptographic private key data in encrypted PKCS #8 RFC 7468 PEM format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns a new <see cref="string"/> instance containing the RSA cryptographic private key data in PKCS #8 RFC 7468 PEM format.</returns>
    public string ExportPkcs8Pem(ReadOnlySpan<byte> password, PbeParameters parameters)
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKeyPem(password, parameters);
    }
}
