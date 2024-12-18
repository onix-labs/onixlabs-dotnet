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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public sealed partial class RsaPublicKey
{
    /// <summary>
    /// Exports the RSA cryptographic public key data.
    /// </summary>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the RSA cryptographic public key data.</returns>
    public byte[] Export()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportSubjectPublicKeyInfo();
    }

    /// <summary>
    /// Exports the RSA cryptographic public key data in RFC 7468 PEM format.
    /// </summary>
    /// <returns>Returns a new <see cref="string"/> instance containing the RSA cryptographic public key data in RFC 7468 format.</returns>
    public string ExportPem()
    {
        using RSA algorithm = ImportKeyData();
        return algorithm.ExportSubjectPublicKeyInfoPem();
    }
}
