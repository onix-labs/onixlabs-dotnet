// Copyright © 2020 ONIXLabs
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
    /// Exports the key in PKCS #8 format.
    /// </summary>
    /// <returns>Returns the key in PKCS #8 format.</returns>
    public override byte[] ExportPkcs8Key()
    {
        using RSA privateKey = RSA.Create();
        privateKey.ImportRSAPrivateKey(KeyData, out int _);
        return privateKey.ExportPkcs8PrivateKey();
    }

    /// <summary>
    /// Exports the key in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns the key in encrypted PKCS #8 format.</returns>
    public override byte[] ExportPkcs8Key(ReadOnlySpan<char> password, PbeParameters parameters)
    {
        using RSA privateKey = RSA.Create();
        privateKey.ImportRSAPrivateKey(KeyData, out int _);
        return privateKey.ExportEncryptedPkcs8PrivateKey(password, parameters);
    }
}
