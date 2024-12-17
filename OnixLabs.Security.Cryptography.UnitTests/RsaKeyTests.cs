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
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class RsaKeyTests
{
    [Fact(DisplayName = "RSA sign and verify with two identical keys should succeed")]
    public void RsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey1 = RsaPrivateKey.Create();
        IRsaPrivateKey privateKey2 = new RsaPrivateKey(privateKey1.AsReadOnlySpan());
        IRsaPublicKey publicKey1 = privateKey1.GetPublicKey();
        IRsaPublicKey publicKey2 = privateKey2.GetPublicKey();

        // When
        DigitalSignature signature1 = new(privateKey1.SignData(data, algorithm, padding));
        DigitalSignature signature2 = new(privateKey2.SignData(data, algorithm, padding));

        // Then
        Assert.True(publicKey1.IsDataValid(signature1, data, algorithm, padding));
        Assert.True(publicKey1.IsDataValid(signature2, data, algorithm, padding));
        Assert.True(publicKey2.IsDataValid(signature1, data, algorithm, padding));
        Assert.True(publicKey2.IsDataValid(signature2, data, algorithm, padding));
    }

    [Fact(DisplayName = "ECDSA PKCS #8 round-trip sign and verify should succeed")]
    public void EcdsaPkcs8RoundTripSignAndVerifyShouldSucceed()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] exportedPrivateKey = RsaPrivateKey.Create().ExportPkcs8("Password", parameters);
        IRsaPrivateKey privateKey = RsaPrivateKey.ImportPkcs8(exportedPrivateKey, "Password");
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        DigitalSignature signature = new(privateKey.SignData(data, algorithm, padding));

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm, padding));
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable")]
    public void RsaPrivateKeyShouldBeExportableAndImportable()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.Export();
        RsaPrivateKey actual = RsaPrivateKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable as PEM")]
    public void RsaPrivateKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPem();
        RsaPrivateKey actual = RsaPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable as PKCS8")]
    public void RsaPrivateKeyShouldBeExportableAndImportableAsPkcs8()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.ExportPkcs8();
        RsaPrivateKey actual = RsaPrivateKey.ImportPkcs8(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable as PKCS8 PEM")]
    public void RsaPrivateKeyShouldBeExportableAndImportableAsPkcs8Pem()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPkcs8Pem();
        RsaPrivateKey actual = RsaPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable as encrypted PKCS8")]
    public void RsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        byte[] privateKeyData = expected.ExportPkcs8("Password", parameters);
        RsaPrivateKey actual = RsaPrivateKey.ImportPkcs8(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPrivateKey should be exportable and importable as encrypted PKCS8 PEM")]
    public void RsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8Pem()
    {
        // Given
        RsaPrivateKey expected = RsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        string privateKeyData = expected.ExportPkcs8Pem("Password", parameters);
        RsaPrivateKey actual = RsaPrivateKey.ImportPem(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPublicKey should be exportable and importable")]
    public void RsaPublicKeyShouldBeExportableAndImportable()
    {
        // Given
        RsaPublicKey expected = RsaPrivateKey.Create().GetPublicKey();

        // When
        byte[] privateKeyData = expected.Export();
        RsaPublicKey actual = RsaPublicKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "RsaPublicKey should be exportable and importable as PEM")]
    public void RsaPublicKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        RsaPublicKey expected = RsaPrivateKey.Create().GetPublicKey();

        // When
        string privateKeyData = expected.ExportPem();
        RsaPublicKey actual = RsaPublicKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }
}
