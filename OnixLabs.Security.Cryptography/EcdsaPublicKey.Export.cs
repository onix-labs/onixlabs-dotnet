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

public sealed partial class EcdsaPublicKey
{
    /// <summary>
    /// The RFC 7468 PEM label used for SubjectPublicKeyInfo-encoded public keys.
    /// </summary>
    private const string PublicKeyLabel = "PUBLIC KEY";

    /// <inheritdoc/>
    public byte[] Export()
    {
        if (TryGetSecp256k1PublicKey(out Secp256k1Point point)) return Secp256k1.EncodeSubjectPublicKeyInfo(point);

        using ECDsa algorithm = ImportKeyData();
        return algorithm.ExportSubjectPublicKeyInfo();
    }

    /// <inheritdoc/>
    public string ExportPem()
    {
        if (TryGetSecp256k1PublicKey(out Secp256k1Point point))
            return PemEncoding.WriteString(PublicKeyLabel, Secp256k1.EncodeSubjectPublicKeyInfo(point));

        using ECDsa algorithm = ImportKeyData();
        return algorithm.ExportSubjectPublicKeyInfoPem();
    }
}
