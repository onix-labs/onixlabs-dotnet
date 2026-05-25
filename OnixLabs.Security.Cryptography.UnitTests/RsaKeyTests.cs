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
using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core;

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

    [Fact(DisplayName = "RsaPrivateKey.ImportPem should throw CryptographicException for malformed PEM")]
    public void RsaPrivateKeyImportPemShouldThrowCryptographicExceptionForMalformedPem()
    {
        // When / Then
        Assert.Throws<CryptographicException>(() => RsaPrivateKey.ImportPem("not a pem"));
    }

    [Theory(DisplayName = "RSA SignData and IsDataValid should round-trip across all padding modes")]
    [InlineData("Pkcs1")]
    [InlineData("Pss")]
    public void RsaSignDataAndVerifyShouldRoundTripAcrossPaddingModes(string paddingName)
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(256).AsReadOnlySpan();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = paddingName == "Pss" ? RSASignaturePadding.Pss : RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm, padding));
    }

    [Fact(DisplayName = "RSA SignData should produce a signature that verifies under the BCL RSA implementation")]
    public void RsaSignDataShouldVerifyUnderBclRsa()
    {
        // Given
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // When — verify the library's signature using an independent BCL RSA instance seeded from the SPKI export.
        using RSA bcl = RSA.Create();
        bcl.ImportSubjectPublicKeyInfo(publicKey.Export(), out _);
        bool actual = bcl.VerifyData(data, signature, algorithm, padding);

        // Then
        Assert.True(actual);
    }

    [Fact(DisplayName = "RSA should verify a signature produced by the BCL RSA implementation")]
    public void RsaShouldVerifyBclProducedSignature()
    {
        // Given — the BCL produces the signature; the library verifies it (reverse cross-check).
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        using RSA bcl = RSA.Create();
        byte[] signature = bcl.SignData(data, algorithm, padding);
        RsaPublicKey publicKey = RsaPublicKey.Import(bcl.ExportSubjectPublicKeyInfo());

        // When
        bool actual = publicKey.IsDataValid(signature, data, algorithm, padding);

        // Then
        Assert.True(actual);
    }

    [Fact(DisplayName = "RSA IsDataValid should return false for a tampered signature")]
    public void RsaIsDataValidShouldReturnFalseForTamperedSignature()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(128).AsReadOnlySpan();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // When
        signature[0] ^= 0x01;

        // Then
        Assert.False(publicKey.IsDataValid(signature, data, algorithm, padding));
    }

    [Fact(DisplayName = "RSA IsDataValid should return false for tampered data")]
    public void RsaIsDataValidShouldReturnFalseForTamperedData()
    {
        // Given
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // When
        byte[] tampered = (byte[])data.Clone();
        tampered[0] ^= 0x01;

        // Then
        Assert.False(publicKey.IsDataValid(signature, tampered, algorithm, padding));
    }

    [Fact(DisplayName = "RSA SignData offset and count overload should round-trip via the offset and count verify overload")]
    public void RsaSignDataOffsetCountShouldRoundTrip()
    {
        // Given
        byte[] buffer = Salt.CreateNonZero(256).AsReadOnlySpan().ToArray();
        const int offset = 16;
        const int count = 100;
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(buffer, offset, count, algorithm, padding);

        // Then — verifies against the same slice, and against the explicit sub-array, but not against the whole buffer.
        Assert.True(publicKey.IsDataValid(signature, buffer, offset, count, algorithm, padding));
        Assert.True(publicKey.IsDataValid(signature, buffer.AsSpan(offset, count), algorithm, padding));
        Assert.False(publicKey.IsDataValid(signature, buffer, algorithm, padding));
    }

    [Fact(DisplayName = "RSA SignData Stream overload should round-trip via the Stream verify overload")]
    public void RsaSignDataStreamShouldRoundTrip()
    {
        // Given
        byte[] data = Salt.CreateNonZero(512).AsReadOnlySpan().ToArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(new MemoryStream(data), algorithm, padding);

        // Then — the stream signature must verify against the equivalent span and stream.
        Assert.True(publicKey.IsDataValid(signature, data, algorithm, padding));
        Assert.True(publicKey.IsDataValid(signature, new MemoryStream(data), algorithm, padding));
    }

    [Fact(DisplayName = "RSA SignData IBinaryConvertible overload should round-trip via the IBinaryConvertible verify overload")]
    public void RsaSignDataBinaryConvertibleShouldRoundTrip()
    {
        // Given
        Salt data = Salt.CreateNonZero(128);
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm, padding));
        Assert.True(publicKey.IsDataValid(signature, data.AsReadOnlySpan(), algorithm, padding));
    }

    [Fact(DisplayName = "RSA SignHash Hash overload should round-trip via IsHashValid")]
    public void RsaSignHashShouldRoundTrip()
    {
        // Given
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        Hash hash = Hash.Compute(SHA256.Create(), data);
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signatureFromHash = privateKey.SignHash(hash, algorithm, padding);
        byte[] signatureFromSpan = privateKey.SignHash(hash.AsReadOnlySpan(), algorithm, padding);

        // Then — both SignHash overloads verify, and a SignHash signature verifies as the data signature (BCL semantics).
        Assert.True(publicKey.IsHashValid(signatureFromHash, hash, algorithm, padding));
        Assert.True(publicKey.IsHashValid(signatureFromSpan, hash.AsReadOnlySpan(), algorithm, padding));
        Assert.True(publicKey.IsDataValid(signatureFromHash, data, algorithm, padding));
    }

    [Fact(DisplayName = "RSA VerifyData should not throw for a valid signature and throw for an invalid one")]
    public void RsaVerifyDataThrowingBehaviour()
    {
        // Given
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();
        DigitalSignature signature = new(privateKey.SignData(data, algorithm, padding));

        // When / Then — valid signature does not throw.
        publicKey.VerifyData(signature, data, algorithm, padding);

        // And — a signature over different data throws.
        DigitalSignature wrong = new(privateKey.SignData(Salt.CreateNonZero(128).AsReadOnlySpan(), algorithm, padding));
        Assert.Throws<CryptographicException>(() => publicKey.VerifyData(wrong, data, algorithm, padding));
    }

    [Fact(DisplayName = "RSA VerifyHash should not throw for a valid signature and throw for an invalid one")]
    public void RsaVerifyHashThrowingBehaviour()
    {
        // Given
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        Hash hash = Hash.Compute(SHA256.Create(), data);
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey = RsaPrivateKey.Create();
        IRsaPublicKey publicKey = privateKey.GetPublicKey();
        DigitalSignature signature = new(privateKey.SignHash(hash, algorithm, padding));

        // When / Then
        publicKey.VerifyHash(signature, hash, algorithm, padding);

        Hash otherHash = Hash.Compute(SHA256.Create(), Salt.CreateNonZero(128).AsReadOnlySpan().ToArray());
        Assert.Throws<CryptographicException>(() => publicKey.VerifyHash(signature, otherHash, algorithm, padding));
    }

    [Fact(DisplayName = "RsaPrivateKey.Create(RSAParameters) should produce a working signing key")]
    public void RsaPrivateKeyCreateFromParametersShouldProduceWorkingKey()
    {
        // Given — generate parameters from an independent BCL RSA instance.
        using RSA source = RSA.Create(2048);
        RSAParameters parameters = source.ExportParameters(includePrivateParameters: true);
        byte[] data = Salt.CreateNonZero(128).AsReadOnlySpan().ToArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;

        // When
        RsaPrivateKey privateKey = RsaPrivateKey.Create(parameters);
        byte[] signature = privateKey.SignData(data, algorithm, padding);

        // Then — verify under the original BCL public key the parameters came from.
        Assert.True(source.VerifyData(data, signature, algorithm, padding));
        Assert.True(privateKey.GetPublicKey().IsDataValid(signature, data, algorithm, padding));
    }

    [Fact(DisplayName = "RsaPublicKey.Import with bytesRead should report the consumed byte count")]
    public void RsaPublicKeyImportWithBytesReadShouldReportConsumedBytes()
    {
        // Given
        RsaPublicKey expected = RsaPrivateKey.Create().GetPublicKey();
        byte[] exported = expected.Export();

        // When
        RsaPublicKey actual = RsaPublicKey.Import(exported, out int bytesRead);

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(exported.Length, bytesRead);
    }
}
