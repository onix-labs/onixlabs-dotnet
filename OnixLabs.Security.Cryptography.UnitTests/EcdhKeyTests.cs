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

    [Fact(DisplayName = "EcdhPrivateKey.ImportPem should throw CryptographicException for malformed PEM")]
    public void EcdhPrivateKeyImportPemShouldThrowCryptographicExceptionForMalformedPem()
    {
        // When / Then
        Assert.Throws<CryptographicException>(() => EcdhPrivateKey.ImportPem("not a pem"));
    }

    [Fact(DisplayName = "EC Diffie-Hellman a third party's shared secret should differ from the agreeing pair")]
    public void EcdhThirdPartySecretShouldDiffer()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create();
        IEcdhPrivateKey bob = EcdhPrivateKey.Create();
        IEcdhPrivateKey eve = EcdhPrivateKey.Create();

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());
        Secret eveSecret = eve.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
        Assert.NotEqual(aliceSecret, eveSecret);
        Assert.NotEqual(bobSecret, eveSecret);
    }

    [Theory(DisplayName = "EC Diffie-Hellman Create(ECCurve) parties on the same curve should agree")]
    [InlineData(nameof(ECCurve.NamedCurves.nistP256))]
    [InlineData(nameof(ECCurve.NamedCurves.nistP384))]
    [InlineData(nameof(ECCurve.NamedCurves.nistP521))]
    public void EcdhCreateFromCurvePartiesShouldAgree(string curveName)
    {
        // Given
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(curve);
        IEcdhPrivateKey bob = EcdhPrivateKey.Create(curve);

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
    }

    [Fact(DisplayName = "EC Diffie-Hellman Create(ECParameters) should produce a usable key")]
    public void EcdhCreateFromParametersShouldProduceUsableKey()
    {
        // Given: alice's parameters are generated independently by the BCL.
        using ECDiffieHellman source = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);
        ECParameters parameters = source.ExportParameters(includePrivateParameters: true);
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(parameters);
        IEcdhPrivateKey bob = EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256);

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
    }

    [Fact(DisplayName = "EC Diffie-Hellman raw round-trip re-imported key should still agree with the original counterparty")]
    public void EcdhRawRoundTripShouldStillAgree()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create();
        IEcdhPrivateKey bob = EcdhPrivateKey.Create();
        Secret expected = alice.DeriveSharedSecret(bob.GetPublicKey());

        // When: alice is round-tripped through raw export/import and PEM export/import.
        IEcdhPrivateKey aliceFromRaw = EcdhPrivateKey.Import(alice.Export());
        IEcdhPrivateKey aliceFromPem = EcdhPrivateKey.ImportPem(alice.ExportPem());

        // Then: each re-imported alice agrees with the original bob, matching the original secret.
        Assert.Equal(expected, aliceFromRaw.DeriveSharedSecret(bob.GetPublicKey()));
        Assert.Equal(expected, aliceFromPem.DeriveSharedSecret(bob.GetPublicKey()));
        Assert.Equal(expected, bob.DeriveSharedSecret(aliceFromRaw.GetPublicKey()));
    }

    [Fact(DisplayName = "EC Diffie-Hellman shared secret should match a BCL-derived secret over the same keys")]
    public void EcdhSharedSecretShouldMatchBcl()
    {
        // Given: alice (library) and bob (library) on the same curve.
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256);
        IEcdhPrivateKey bob = EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256);
        Secret librarySecret = alice.DeriveSharedSecret(bob.GetPublicKey());

        // When: an independent BCL ECDiffieHellman reproduces alice's derivation from the exported key material.
        using ECDiffieHellman bclAlice = ECDiffieHellman.Create();
        bclAlice.ImportECPrivateKey(alice.Export(), out int _);
        using ECDiffieHellman bclBobPublic = ECDiffieHellman.Create();
        bclBobPublic.ImportSubjectPublicKeyInfo(bob.GetPublicKey().AsReadOnlySpan(), out int _);
        byte[] bclSecret = bclAlice.DeriveKeyMaterial(bclBobPublic.PublicKey);

        // Then: the library's shared secret equals the BCL's default (SHA-256) key-material derivation.
        Assert.Equal(bclSecret, librarySecret.AsReadOnlySpan().ToArray());
    }
}
