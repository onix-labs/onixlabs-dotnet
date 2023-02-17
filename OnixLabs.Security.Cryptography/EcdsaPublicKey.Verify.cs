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

public sealed partial class EcdsaPublicKey
{
    /// <summary>
    /// Determines whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to validate.</param>
    /// <param name="unsignedData">The unsigned data to validate.</param>
    /// <returns>Returns true if the specified <see cref="DigitalSignature"/> was signed by the private component of this public key; otherwise, false.</returns>
    public override bool IsDataValid(DigitalSignature signature, byte[] unsignedData)
    {
        using ECDsa publicKey = ECDsa.Create();

        publicKey.ImportSubjectPublicKeyInfo(KeyData, out int _);
        byte[] signatureData = signature.ToByteArray();
        HashAlgorithmName name = AlgorithmType.GetHashAlgorithmName();

        return publicKey.VerifyData(unsignedData, signatureData, name);
    }

    /// <summary>
    /// Determines whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to validate.</param>
    /// <param name="unsignedHash">The unsigned hash to validate.</param>
    /// <returns>Returns true if the specified <see cref="DigitalSignature"/> was signed by the private component of this public key; otherwise, false.</returns>
    public override bool IsHashValid(DigitalSignature signature, byte[] unsignedHash)
    {
        using ECDsa publicKey = ECDsa.Create();

        publicKey.ImportSubjectPublicKeyInfo(KeyData, out int _);
        byte[] signatureData = signature.ToByteArray();

        return publicKey.VerifyHash(unsignedHash, signatureData);
    }
}
