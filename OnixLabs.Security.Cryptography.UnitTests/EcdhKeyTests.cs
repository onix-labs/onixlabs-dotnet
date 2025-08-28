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

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdhKeyTests
{
    [Fact(DisplayName = "EC Diffie-Hellman DeriveSharedSecret should produce identical secrets")]
    public void EcdhDeriveSharedSecretShouldProduceIdenticalSecrets()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create();
        IEcdhPrivateKey bob = EcdhPrivateKey.Create();

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
    }

    [Fact(DisplayName = "EC Diffie-Hellman PKCS #8 round-trip DeriveSharedSecret should produce identical secrets")]
    public void EcdhPkcs8RoundTripDeriveSharedSecretShouldProduceIdenticalSecrets()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] aliceExportedBytes = EcdhPrivateKey.Create().ExportPkcs8("AlicePassword", parameters);
        byte[] bobExportedBytes = EcdhPrivateKey.Create().ExportPkcs8("BobPassword", parameters);

        IEcdhPrivateKey alice = EcdhPrivateKey.ImportPkcs8(aliceExportedBytes, "AlicePassword");
        IEcdhPrivateKey bob = EcdhPrivateKey.ImportPkcs8(bobExportedBytes, "BobPassword");

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable")]
    public void EcdhPrivateKeyShouldBeExportableAndImportable()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.Export();
        EcdhPrivateKey actual = EcdhPrivateKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable as PEM")]
    public void EcdhPrivateKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPem();
        EcdhPrivateKey actual = EcdhPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable as PKCS8")]
    public void EcdhPrivateKeyShouldBeExportableAndImportableAsPkcs8()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.ExportPkcs8();
        EcdhPrivateKey actual = EcdhPrivateKey.ImportPkcs8(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable as PKCS8 PEM")]
    public void EcdhPrivateKeyShouldBeExportableAndImportableAsPkcs8Pem()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPkcs8Pem();
        EcdhPrivateKey actual = EcdhPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable as encrypted PKCS8")]
    public void EcdhPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        byte[] privateKeyData = expected.ExportPkcs8("Password", parameters);
        EcdhPrivateKey actual = EcdhPrivateKey.ImportPkcs8(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPrivateKey should be exportable and importable as encrypted PKCS8 PEM")]
    public void EcdhPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8Pem()
    {
        // Given
        EcdhPrivateKey expected = EcdhPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        string privateKeyData = expected.ExportPkcs8Pem("Password", parameters);
        EcdhPrivateKey actual = EcdhPrivateKey.ImportPem(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPublicKey should be exportable and importable")]
    public void EcdhPublicKeyShouldBeExportableAndImportable()
    {
        // Given
        EcdhPublicKey expected = EcdhPrivateKey.Create().GetPublicKey();

        // When
        byte[] privateKeyData = expected.Export();
        EcdhPublicKey actual = EcdhPublicKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdhPublicKey should be exportable and importable as PEM")]
    public void EcdhPublicKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        EcdhPublicKey expected = EcdhPrivateKey.Create().GetPublicKey();

        // When
        string privateKeyData = expected.ExportPem();
        EcdhPublicKey actual = EcdhPublicKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }
}
