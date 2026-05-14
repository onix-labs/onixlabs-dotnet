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
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EddsaKeyTests
{
    [Fact(DisplayName = "EdDSA sign and verify with two identical keys should succeed")]
    public void EddsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        IEddsaPrivateKey privateKey1 = EddsaPrivateKey.Create();
        IEddsaPrivateKey privateKey2 = new EddsaPrivateKey(privateKey1.AsReadOnlySpan());
        IEddsaPublicKey publicKey1 = privateKey1.GetPublicKey();
        IEddsaPublicKey publicKey2 = privateKey2.GetPublicKey();

        // When
        DigitalSignature signature1 = new(privateKey1.SignData(data));
        DigitalSignature signature2 = new(privateKey2.SignData(data));

        // Then
        Assert.True(publicKey1.IsDataValid(signature1, data));
        Assert.True(publicKey1.IsDataValid(signature2, data));
        Assert.True(publicKey2.IsDataValid(signature1, data));
        Assert.True(publicKey2.IsDataValid(signature2, data));
    }

    [Fact(DisplayName = "EdDSA signatures should be 64 bytes")]
    public void EddsaSignaturesShouldBeSixtyFourBytes()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(128).AsReadOnlySpan();
        EddsaPrivateKey privateKey = EddsaPrivateKey.Create();

        // When
        byte[] signature = privateKey.SignData(data);

        // Then
        Assert.Equal(64, signature.Length);
    }

    [Fact(DisplayName = "EdDSA tampered signatures should fail verification")]
    public void EddsaTamperedSignaturesShouldFailVerification()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(128).AsReadOnlySpan();
        EddsaPrivateKey privateKey = EddsaPrivateKey.Create();
        EddsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data);

        // When
        signature[0] ^= 0x01;

        // Then
        Assert.False(publicKey.IsDataValid(signature, data));
    }

    [Fact(DisplayName = "EdDSA PKCS #8 round-trip sign and verify should succeed")]
    public void EddsaPkcs8RoundTripSignAndVerifyShouldSucceed()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] exportedPrivateKey = EddsaPrivateKey.Create().ExportPkcs8("Password", parameters);
        IEddsaPrivateKey privateKey = EddsaPrivateKey.ImportPkcs8(exportedPrivateKey, "Password");
        IEddsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        DigitalSignature signature = new(privateKey.SignData(data));

        // Then
        Assert.True(publicKey.IsDataValid(signature, data));
    }

    [Fact(DisplayName = "EddsaPrivateKey should be exportable and importable")]
    public void EddsaPrivateKeyShouldBeExportableAndImportable()
    {
        // Given
        EddsaPrivateKey expected = EddsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.Export();
        EddsaPrivateKey actual = EddsaPrivateKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPrivateKey should be exportable and importable as PKCS8")]
    public void EddsaPrivateKeyShouldBeExportableAndImportableAsPkcs8()
    {
        // Given
        EddsaPrivateKey expected = EddsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.ExportPkcs8();
        EddsaPrivateKey actual = EddsaPrivateKey.ImportPkcs8(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPrivateKey should be exportable and importable as PKCS8 PEM")]
    public void EddsaPrivateKeyShouldBeExportableAndImportableAsPkcs8Pem()
    {
        // Given
        EddsaPrivateKey expected = EddsaPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPkcs8Pem();
        EddsaPrivateKey actual = EddsaPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPrivateKey should be exportable and importable as encrypted PKCS8")]
    public void EddsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8()
    {
        // Given
        EddsaPrivateKey expected = EddsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        byte[] privateKeyData = expected.ExportPkcs8("Password", parameters);
        EddsaPrivateKey actual = EddsaPrivateKey.ImportPkcs8(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPrivateKey should be exportable and importable as encrypted PKCS8 PEM")]
    public void EddsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8Pem()
    {
        // Given
        EddsaPrivateKey expected = EddsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        string privateKeyData = expected.ExportPkcs8Pem("Password", parameters);
        EddsaPrivateKey actual = EddsaPrivateKey.ImportPem(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPublicKey should be exportable and importable")]
    public void EddsaPublicKeyShouldBeExportableAndImportable()
    {
        // Given
        EddsaPublicKey expected = EddsaPrivateKey.Create().GetPublicKey();

        // When
        byte[] publicKeyData = expected.Export();
        EddsaPublicKey actual = EddsaPublicKey.Import(publicKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EddsaPublicKey should be exportable and importable as PEM")]
    public void EddsaPublicKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        EddsaPublicKey expected = EddsaPrivateKey.Create().GetPublicKey();

        // When
        string publicKeyData = expected.ExportPem();
        EddsaPublicKey actual = EddsaPublicKey.ImportPem(publicKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "EdDSA must satisfy RFC 8032 §7.1 known-answer test vectors")]
    [InlineData(
        "9d61b19deffd5a60ba844af492ec2cc44449c5697b326919703bac031cae7f60",
        "d75a980182b10ab7d54bfed3c964073a0ee172f3daa62325af021a68f707511a",
        "",
        "e5564300c360ac729086e2cc806e828a84877f1eb8e5d974d873e065224901555fb8821590a33bacc61e39701cf9b46bd25bf5f0595bbe24655141438e7a100b")]
    [InlineData(
        "4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb",
        "3d4017c3e843895a92b70aa74d1b7ebc9c982ccf2ec4968cc0cd55f12af4660c",
        "72",
        "92a009a9f0d4cab8720e820b5f642540a2b27b5416503f8fb3762223ebdb69da085ac1e43e15996e458f3613d0f11d8c387b2eaeb4302aeeb00d291612bb0c00")]
    [InlineData(
        "c5aa8df43f9f837bedb7442f31dcb7b166d38535076f094b85ce3a2e0b4458f7",
        "fc51cd8e6218a1a38da47ed00230f0580816ed13ba3303ac5deb911548908025",
        "af82",
        "6291d657deec24024827e69c3abe01a30ce548a284743a445e3680d7db5ac3ac18ff9b538d16f290ae67f760984dc6594a7c15e9716ed28dc027beceea1ec40a")]
    public void EddsaMustSatisfyRfc8032KnownAnswerTestVectors(string seedHex, string publicKeyHex, string messageHex, string signatureHex)
    {
        // Given
        byte[] seed = DecodeHex(seedHex);
        byte[] expectedPublicKey = DecodeHex(publicKeyHex);
        byte[] message = DecodeHex(messageHex);
        byte[] expectedSignature = DecodeHex(signatureHex);
        EddsaPrivateKey privateKey = EddsaPrivateKey.Import(seed);
        EddsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] derivedPublicKey = publicKey.Export();
        byte[] actualSignature = privateKey.SignData(message);

        // Then
        Assert.Equal(expectedPublicKey, derivedPublicKey);
        Assert.Equal(expectedSignature, actualSignature);
        Assert.True(publicKey.IsDataValid(expectedSignature, message));
    }

    private static byte[] DecodeHex(string value) =>
        IBaseCodec.Base16.Decode(value, Base16FormatProvider.Invariant);
}
