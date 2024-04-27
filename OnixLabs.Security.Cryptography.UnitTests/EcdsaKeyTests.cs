// Copyright 2020-2024 ONIXLabs
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
using OnixLabs.Core;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdsaKeyTests
{
    [Fact(DisplayName = "ECDSA public/private key pair should be able to sign and verify data")]
    public void EcdsaPublicPrivateKeyPairShouldBeAbleToSignAndVerifyData()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        EcdsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] data = "Hello, World!".ToByteArray();

        // When
        DigitalSignature signature = privateKey.SignData(data, algorithm);
        bool condition = publicKey.IsDataValid(data, signature, algorithm);

        // Then
        Assert.True(condition);
    }

    [Fact(DisplayName = "ECDSA public/private key pair should fail to verify when using the wrong key")]
    public void EcdsaPublicPrivateKeyPairShouldFailToVerifyWhenUsingTheWrongKey()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        EcdsaPublicKey publicKey = EcdsaPrivateKey.Create().GetPublicKey();
        byte[] data = "Hello, World!".ToByteArray();

        // When
        DigitalSignature signature = privateKey.SignData(data, algorithm);
        bool condition = publicKey.IsDataValid(data, signature, algorithm);

        // Then
        Assert.False(condition);
    }

    [Fact(DisplayName = "ECDSA public private key pair should throw CryptographicException when using the wrong key")]
    public void EcdsaPublicPrivateKeyPairShouldThrowCryptographicExceptionWhenUsingTheWrongKey()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        EcdsaPublicKey publicKey = EcdsaPrivateKey.Create().GetPublicKey();
        byte[] data = "Hello, World!".ToByteArray();

        // When
        DigitalSignature signature = privateKey.SignData(data, algorithm);

        // Then
        Assert.Throws<CryptographicException>(() => publicKey.VerifyData(data, signature, algorithm));
    }

    [Fact(DisplayName = "ECDSA private key can be exported and imported in Pkcs 8 format")]
    public void EcdsaPrivateKeyCanBeExportedAndImportedInPkcs8Format()
    {
        // Given
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();

        // When
        byte[] exportedPrivateKey = privateKey.ExportPkcs8Key();
        EcdsaPrivateKey importedPrivateKey = EcdsaPrivateKey.ImportPkcs8Key(exportedPrivateKey);

        // Then
        Assert.Equal(privateKey, importedPrivateKey);
    }

    [Fact(DisplayName = "ECDSA private key can be exported and imported in Pkcs 8 format with a password")]
    public void EcdsaPrivateKeyCanBeExportedAndImportedInPkcs8FormatWithAPassword()
    {
        // Given
        const string password = "secret";
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();

        // When
        byte[] exportedPrivateKey = privateKey.ExportPkcs8Key(password, parameters);
        EcdsaPrivateKey importedPrivateKey = EcdsaPrivateKey.ImportPkcs8Key(exportedPrivateKey, password);

        // Then
        Assert.Equal(privateKey, importedPrivateKey);
    }

    [Fact(DisplayName = "ECDSA round trip create, sign, export, import and verify should succeed")]
    public void EcdsaRoundTripCreateSignExportImportAndVerifyShouldSucceed()
    {
        byte[] data = "Hello, World!".ToByteArray();

        // Create
        EcdsaPrivateKey initialPrivateKey = EcdsaPrivateKey.Create();

        // Sign
        using HashAlgorithm algorithm = SHA256.Create();
        DigitalSignature signature = initialPrivateKey.SignData(data, algorithm);

        // Export
        const string password = "secret";
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] exportedPrivateKey = initialPrivateKey.ExportPkcs8Key(password, parameters);

        // Import
        EcdsaPrivateKey importedPrivateKey = EcdsaPrivateKey.ImportPkcs8Key(exportedPrivateKey, password);

        // Verify
        EcdsaPublicKey publicKey = importedPrivateKey.GetPublicKey();
        publicKey.VerifyData(data, signature, algorithm);
    }
}
