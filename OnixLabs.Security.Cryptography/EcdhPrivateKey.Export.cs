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

public sealed partial class EcdhPrivateKey
{
    /// <inheritdoc/>
    public byte[] Export()
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportECPrivateKey();
    }

    /// <inheritdoc/>
    public byte[] ExportPkcs8()
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportPkcs8PrivateKey();
    }

    /// <inheritdoc/>
    public byte[] ExportPkcs8(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKey(password, parameters);
    }

    /// <inheritdoc/>
    public byte[] ExportPkcs8(ReadOnlySpan<byte> password, PbeParameters parameters)
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKey(password, parameters);
    }

    /// <inheritdoc/>
    public string ExportPem()
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportECPrivateKeyPem();
    }

    /// <inheritdoc/>
    public string ExportPkcs8Pem()
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportPkcs8PrivateKeyPem();
    }

    /// <inheritdoc/>
    public string ExportPkcs8Pem(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKeyPem(password, parameters);
    }

    /// <inheritdoc/>
    public string ExportPkcs8Pem(ReadOnlySpan<byte> password, PbeParameters parameters)
    {
        using ECDiffieHellman algorithm = ImportKeyData();
        return algorithm.ExportEncryptedPkcs8PrivateKeyPem(password, parameters);
    }
}
