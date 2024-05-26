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
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdsaKeyTests
{
    [Fact(DisplayName = "ECDSA sign and verify with two identical keys should succeed")]
    public void EcdsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed()
    {
        // Given
        byte[] data = Salt.CreateNonZero(2048).ToByteArray();
        using HashAlgorithm algorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey1 = EcdsaPrivateKey.Create();
        IEcdsaPrivateKey privateKey2 = new EcdsaPrivateKey(privateKey1.ToByteArray());
        IEcdsaPublicKey publicKey1 = privateKey1.GetPublicKey();
        IEcdsaPublicKey publicKey2 = privateKey2.GetPublicKey();

        // When
        DigitalSignature signature1 = new(privateKey1.SignData(data, algorithm));
        DigitalSignature signature2 = new(privateKey2.SignData(data, algorithm));

        // Then
        Assert.True(publicKey1.IsDataValid(signature1, data, algorithm));
        Assert.True(publicKey1.IsDataValid(signature2, data, algorithm));
        Assert.True(publicKey2.IsDataValid(signature1, data, algorithm));
        Assert.True(publicKey2.IsDataValid(signature2, data, algorithm));
    }

    [Fact(DisplayName = "ECDSA PKCS #8 round-trip sign and verify should succeed")]
    public void EcdsaPkcs8RoundTripSignAndVerifyShouldSucceed()
    {
        // Given
        byte[] data = Salt.CreateNonZero(2048).ToByteArray();
        using HashAlgorithm algorithm = SHA256.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] exportedPrivateKey = EcdsaPrivateKey.Create().ExportPkcs8PrivateKey("Password", parameters);
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.ImportPkcs8PrivateKey(exportedPrivateKey, "Password");
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        DigitalSignature signature = new(privateKey.SignData(data, algorithm));

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm));
    }
}
