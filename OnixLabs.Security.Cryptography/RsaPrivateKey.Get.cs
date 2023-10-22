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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Gets the public key component from this private key.
    /// </summary>
    /// <returns>Returns the public key component from this private key.</returns>
    public override PublicKey GetPublicKey()
    {
        using RSA privateKey = RSA.Create();

        privateKey.ImportRSAPrivateKey(KeyData, out int _);
        byte[] publicKey = privateKey.ExportRSAPublicKey();

        return new RsaPublicKey(publicKey, AlgorithmType, Padding);
    }
}
