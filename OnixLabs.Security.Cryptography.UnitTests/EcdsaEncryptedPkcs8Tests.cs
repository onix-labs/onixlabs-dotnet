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

using System.Collections.Generic;
using System.Security.Cryptography;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdsaKeyEncryptedPkcs8Tests : KeyTestBase
{
    [Fact(DisplayName = "Two identical ECDSA PKCS #8 private keys should be considered equal")]
    public void TwoIdenticalPrivateKeysShouldBeConsideredEqual()
    {
        // Arrange
        HashAlgorithmType type = HashAlgorithmType.Sha2Hash256;
        KeyPair pair = KeyPair.CreateEcdsaKeyPair(type);
        PrivateKey privateKey1 = pair.PrivateKey;
        const string password = "This is a secret!";
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 64);

        // Act
        byte[] pkcs8PrivateKey = privateKey1.ExportPkcs8Key(password, parameters);
        PrivateKey privateKey2 = EcdsaPrivateKey.ImportPkcs8Key(pkcs8PrivateKey, password, type);

        // Assert
        Assert.Equal(privateKey1, privateKey2);
    }

    [Fact(DisplayName = "Two identical ECDSA PKCS #8 keys should be able to sign and verify the same data")]
    public void TwoIdenticalEcdsaKeysShouldBeAbleToSignAndVerifyTheSameData()
    {
        // Arrange
        IList<(DigitalSignature, byte[])> signatures = new List<(DigitalSignature, byte[])>();
        HashAlgorithmType type = HashAlgorithmType.Sha2Hash256;
        KeyPair pair = KeyPair.CreateEcdsaKeyPair(type);
        PrivateKey privateKey1 = pair.PrivateKey;
        const string password = "This is a secret!";
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 64);
        byte[] pkcs8PrivateKey = privateKey1.ExportPkcs8Key(password, parameters);
        PrivateKey privateKey2 = EcdsaPrivateKey.ImportPkcs8Key(pkcs8PrivateKey, password, type);
        PublicKey publicKey1 = pair.PublicKey;
        PublicKey publicKey2 = privateKey1.GetPublicKey();


        // Act
        for (int index = 0; index < 5; index++)
        {
            byte[] data = GenerateRandomData();
            DigitalSignature signature1 = privateKey1.SignData(data);
            DigitalSignature signature2 = privateKey2.SignData(data);

            signatures.Add((signature1, data));
            signatures.Add((signature2, data));
        }

        // Assert
        foreach ((DigitalSignature signature, byte[] data) in signatures)
        {
            Assert.True(signature.IsDataValid(data, publicKey1));
            Assert.True(signature.IsDataValid(data, publicKey2));
        }
    }

    [Fact(DisplayName = "Two identical ECDSA PKCS #8 keys should be able to sign and verify the same hash")]
    public void TwoIdenticalEcdsaKeysShouldBeAbleToSignAndVerifyTheSameHash()
    {
        // Arrange
        IList<(DigitalSignature, Hash)> signatures = new List<(DigitalSignature, Hash)>();
        HashAlgorithmType type = HashAlgorithmType.Sha2Hash256;
        KeyPair pair = KeyPair.CreateEcdsaKeyPair(type);
        PrivateKey privateKey1 = pair.PrivateKey;
        const string password = "This is a secret!";
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 64);
        byte[] pkcs8PrivateKey = privateKey1.ExportPkcs8Key(password, parameters);
        PrivateKey privateKey2 = EcdsaPrivateKey.ImportPkcs8Key(pkcs8PrivateKey, password, type);
        PublicKey publicKey1 = pair.PublicKey;
        PublicKey publicKey2 = privateKey1.GetPublicKey();

        // Act
        for (int index = 0; index < 5; index++)
        {
            byte[] data = GenerateRandomData();
            Hash hashedData = Hash.ComputeSha2Hash256(data);
            DigitalSignature signature1 = privateKey1.SignHash(hashedData);
            DigitalSignature signature2 = privateKey2.SignHash(hashedData);

            signatures.Add((signature1, hashedData));
            signatures.Add((signature2, hashedData));
        }

        // Assert
        foreach ((DigitalSignature signature, Hash hashedData) in signatures)
        {
            Assert.True(signature.IsHashValid(hashedData, publicKey1));
            Assert.True(signature.IsHashValid(hashedData, publicKey2));
        }
    }
}
