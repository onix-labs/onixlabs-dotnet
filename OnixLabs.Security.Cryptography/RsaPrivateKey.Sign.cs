// Copyright 2020-2022 ONIXLabs
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
    /// Computes a <see cref="DigitalSignature"/> from the specified unsigned data.
    /// </summary>
    /// <param name="unsignedData">The unsigned data from which to compute a <see cref="DigitalSignature"/>.</param>
    /// <returns>Returns a <see cref="DigitalSignature"/> from the specified unsigned data.</returns>
    public override DigitalSignature SignData(byte[] unsignedData)
    {
        using RSA privateKey = RSA.Create();

        privateKey.ImportRSAPrivateKey(KeyData, out int _);
        HashAlgorithmName name = AlgorithmType.GetHashAlgorithmName();
        byte[] signedData = privateKey.SignData(unsignedData, name, Padding);

        return DigitalSignature.FromByteArray(signedData);
    }

    /// <summary>
    /// Computes a <see cref="DigitalSignature"/> from the specified unsigned hash.
    /// </summary>
    /// <param name="unsignedHash">The unsigned hash from which to compute a <see cref="DigitalSignature"/>.</param>
    /// <returns>Returns a <see cref="DigitalSignature"/> from the specified unsigned hash.</returns>
    public override DigitalSignature SignHash(byte[] unsignedHash)
    {
        using RSA privateKey = RSA.Create();

        privateKey.ImportRSAPrivateKey(KeyData, out int _);
        HashAlgorithmName name = AlgorithmType.GetHashAlgorithmName();
        byte[] signedData = privateKey.SignHash(unsignedHash, name, Padding);

        return DigitalSignature.FromByteArray(signedData);
    }
}
