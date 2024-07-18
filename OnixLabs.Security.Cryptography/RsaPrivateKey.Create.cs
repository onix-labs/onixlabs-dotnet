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

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Creates a new RSA cryptographic private key.
    /// </summary>
    /// <returns>Returns a new <see cref="RsaPrivateKey"/> instance.</returns>
    public static RsaPrivateKey Create()
    {
        using RSA key = RSA.Create();
        byte[] keyData = key.ExportRSAPrivateKey();
        return new RsaPrivateKey(keyData);
    }

    /// <summary>
    /// Creates a new RSA cryptographic private key.
    /// </summary>
    /// <param name="parameters">The RSA parameters from which to create a new RSA cryptographic private key.</param>
    /// <returns>Returns a new <see cref="RsaPrivateKey"/> instance.</returns>
    public static RsaPrivateKey Create(RSAParameters parameters)
    {
        using RSA key = RSA.Create(parameters);
        byte[] keyData = key.ExportRSAPrivateKey();
        return new RsaPrivateKey(keyData);
    }
}
