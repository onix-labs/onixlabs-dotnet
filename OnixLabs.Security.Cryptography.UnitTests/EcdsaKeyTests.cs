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

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdsaKeyTests
{
    [Fact(DisplayName = "ECDSA sign and verify with two identical keys should succeed")]
    public void EcdsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        using HashAlgorithm algorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey1 = EcdsaPrivateKey.Create();
        IEcdsaPrivateKey privateKey2 = new EcdsaPrivateKey(privateKey1.AsReadOnlySpan());
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
        ReadOnlySpan<byte> data = Salt.CreateNonZero(2048).AsReadOnlySpan();
        using HashAlgorithm algorithm = SHA256.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);
        byte[] exportedPrivateKey = EcdsaPrivateKey.Create().ExportPkcs8("Password", parameters);
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.ImportPkcs8(exportedPrivateKey, "Password");
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        DigitalSignature signature = new(privateKey.SignData(data, algorithm));

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm));
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportable()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.Export();
        EcdsaPrivateKey actual = EcdsaPrivateKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable as PEM")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPem();
        EcdsaPrivateKey actual = EcdsaPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable as PKCS8")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportableAsPkcs8()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();

        // When
        byte[] privateKeyData = expected.ExportPkcs8();
        EcdsaPrivateKey actual = EcdsaPrivateKey.ImportPkcs8(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable as PKCS8 PEM")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportableAsPkcs8Pem()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();

        // When
        string privateKeyData = expected.ExportPkcs8Pem();
        EcdsaPrivateKey actual = EcdsaPrivateKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable as encrypted PKCS8")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        byte[] privateKeyData = expected.ExportPkcs8("Password", parameters);
        EcdsaPrivateKey actual = EcdsaPrivateKey.ImportPkcs8(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey should be exportable and importable as encrypted PKCS8 PEM")]
    public void EcdsaPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8Pem()
    {
        // Given
        EcdsaPrivateKey expected = EcdsaPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        string privateKeyData = expected.ExportPkcs8Pem("Password", parameters);
        EcdsaPrivateKey actual = EcdsaPrivateKey.ImportPem(privateKeyData, "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPublicKey should be exportable and importable")]
    public void EcdsaPublicKeyShouldBeExportableAndImportable()
    {
        // Given
        EcdsaPublicKey expected = EcdsaPrivateKey.Create().GetPublicKey();

        // When
        byte[] privateKeyData = expected.Export();
        EcdsaPublicKey actual = EcdsaPublicKey.Import(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPublicKey should be exportable and importable as PEM")]
    public void EcdsaPublicKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        EcdsaPublicKey expected = EcdsaPrivateKey.Create().GetPublicKey();

        // When
        string privateKeyData = expected.ExportPem();
        EcdsaPublicKey actual = EcdsaPublicKey.ImportPem(privateKeyData);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "EcdsaPrivateKey.ImportPem should throw CryptographicException for malformed PEM")]
    public void EcdsaPrivateKeyImportPemShouldThrowCryptographicExceptionForMalformedPem()
    {
        // When / Then
        Assert.Throws<CryptographicException>(() => EcdsaPrivateKey.ImportPem("not a pem"));
    }

    [Fact(DisplayName = "ECDSA SignData (ReadOnlySpan, HashAlgorithm) round-trip should verify true and reject tampering")]
    public void EcdsaSignDataReadOnlySpanHashAlgorithmShouldRoundTrip()
    {
        // Given
        ReadOnlySpan<byte> data = [1, 2, 3, 4, 5, 6, 7, 8];
        using HashAlgorithm algorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, algorithm);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, algorithm));
        Assert.False(publicKey.IsDataValid(signature, [1, 2, 3, 4, 5, 6, 7, 9], algorithm));
    }

    [Fact(DisplayName = "ECDSA SignData (ReadOnlySpan, HashAlgorithmName) round-trip should verify true and reject tampering")]
    public void EcdsaSignDataReadOnlySpanHashAlgorithmNameShouldRoundTrip()
    {
        // Given
        ReadOnlySpan<byte> data = [10, 20, 30, 40, 50];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256));
        Assert.False(publicKey.IsDataValid(signature, [10, 20, 30, 40, 51], HashAlgorithmName.SHA256));

        // And: a tampered signature must not verify.
        byte[] tampered = (byte[])signature.Clone();
        tampered[0] ^= 0xFF;
        Assert.False(publicKey.IsDataValid(tampered, data, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA SignData (offset, count) round-trip should verify true against the same slice")]
    public void EcdsaSignDataOffsetCountShouldRoundTrip()
    {
        // Given: the full buffer is signed using the slice [2..6].
        byte[] data = [9, 9, 1, 2, 3, 4, 9, 9];
        const int offset = 2;
        const int count = 4;
        using HashAlgorithm hashAlgorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signatureViaHashAlgorithm = privateKey.SignData(data, offset, count, hashAlgorithm);
        byte[] signatureViaName = privateKey.SignData(data, offset, count, HashAlgorithmName.SHA256);

        // Then: both signatures verify against the equivalent slice, and the two paths agree behaviourally.
        Assert.True(publicKey.IsDataValid(signatureViaHashAlgorithm, data, offset, count, hashAlgorithm));
        Assert.True(publicKey.IsDataValid(signatureViaName, data, offset, count, HashAlgorithmName.SHA256));
        Assert.True(publicKey.IsDataValid(signatureViaHashAlgorithm, data, offset, count, HashAlgorithmName.SHA256));
        Assert.True(publicKey.IsDataValid(signatureViaName, data, offset, count, hashAlgorithm));

        // And: verifying against a different slice must fail.
        Assert.False(publicKey.IsDataValid(signatureViaName, data, 0, count, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA SignData (Stream) round-trip should verify true and reject tampering")]
    public void EcdsaSignDataStreamShouldRoundTrip()
    {
        // Given
        byte[] data = [0xDE, 0xAD, 0xBE, 0xEF, 0x01, 0x02, 0x03];
        using HashAlgorithm hashAlgorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signatureViaHashAlgorithm = privateKey.SignData(new MemoryStream(data), hashAlgorithm);
        byte[] signatureViaName = privateKey.SignData(new MemoryStream(data), HashAlgorithmName.SHA256);

        // Then
        Assert.True(publicKey.IsDataValid(signatureViaHashAlgorithm, new MemoryStream(data), hashAlgorithm));
        Assert.True(publicKey.IsDataValid(signatureViaName, new MemoryStream(data), HashAlgorithmName.SHA256));
        Assert.False(publicKey.IsDataValid(signatureViaName, new MemoryStream([0xDE, 0xAD, 0xBE, 0xEF, 0x01, 0x02, 0x04]), HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA SignData (IBinaryConvertible) round-trip should verify true and reject tampering")]
    public void EcdsaSignDataBinaryConvertibleShouldRoundTrip()
    {
        // Given
        Salt data = Salt.CreateNonZero(64);
        Salt other = Salt.CreateNonZero(64);
        using HashAlgorithm hashAlgorithm = SHA256.Create();
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signatureViaHashAlgorithm = privateKey.SignData(data, hashAlgorithm);
        byte[] signatureViaName = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // Then
        Assert.True(publicKey.IsDataValid(signatureViaHashAlgorithm, data, hashAlgorithm));
        Assert.True(publicKey.IsDataValid(signatureViaName, data, HashAlgorithmName.SHA256));
        Assert.False(publicKey.IsDataValid(signatureViaName, other, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA SignHash and IsHashValid round-trip should verify true and reject tampering")]
    public void EcdsaSignHashShouldRoundTrip()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(128).AsReadOnlySpan();
        Hash hash = Hash.Compute(SHA256.Create(), data);
        Hash otherHash = Hash.Compute(SHA256.Create(), [0x00]);
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signatureFromHash = privateKey.SignHash(hash);
        byte[] signatureFromSpan = privateKey.SignHash(hash.AsReadOnlySpan());

        // Then
        Assert.True(publicKey.IsHashValid(signatureFromHash, hash));
        Assert.True(publicKey.IsHashValid(signatureFromSpan, hash.AsReadOnlySpan()));
        Assert.True(publicKey.IsHashValid(new DigitalSignature(signatureFromHash), hash));
        Assert.False(publicKey.IsHashValid(signatureFromHash, otherHash));
    }

    [Fact(DisplayName = "ECDSA VerifyData should throw CryptographicException for an invalid signature")]
    public void EcdsaVerifyDataShouldThrowForInvalidSignature()
    {
        // Given
        ReadOnlySpan<byte> data = [1, 2, 3, 4, 5];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // When: a valid signature does not throw.
        publicKey.VerifyData(signature, data, HashAlgorithmName.SHA256);

        // Then: signing different data and verifying against it must throw.
        Assert.Throws<CryptographicException>(() => publicKey.VerifyData(signature, [9, 9, 9, 9, 9], HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA VerifyHash should throw CryptographicException for an invalid signature")]
    public void EcdsaVerifyHashShouldThrowForInvalidSignature()
    {
        // Given
        Hash hash = Hash.Compute(SHA256.Create(), Salt.CreateNonZero(32).AsReadOnlySpan());
        Hash otherHash = Hash.Compute(SHA256.Create(), Salt.CreateNonZero(32).AsReadOnlySpan());
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignHash(hash);

        // When: a valid signature does not throw.
        publicKey.VerifyHash(signature, hash);

        // Then
        Assert.Throws<CryptographicException>(() => publicKey.VerifyHash(signature, otherHash));
    }

    [Fact(DisplayName = "ECDSA Rfc3279DerSequence signature format round-trip should verify true")]
    public void EcdsaRfc3279SignatureFormatShouldRoundTrip()
    {
        // Given
        ReadOnlySpan<byte> data = Salt.CreateNonZero(96).AsReadOnlySpan();
        const DSASignatureFormat format = DSASignatureFormat.Rfc3279DerSequence;
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256, format);

        // Then: it verifies under the matching format, but not under the default IEEE P1363 format.
        Assert.True(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256, format));
        Assert.False(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256));
    }

    [Theory(DisplayName = "ECDSA Create(ECCurve) should produce a usable key over the named curve")]
    [InlineData(nameof(ECCurve.NamedCurves.nistP256))]
    [InlineData(nameof(ECCurve.NamedCurves.nistP384))]
    [InlineData(nameof(ECCurve.NamedCurves.nistP521))]
    public void EcdsaCreateFromCurveShouldProduceUsableKey(string curveName)
    {
        // Given
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        ReadOnlySpan<byte> data = [1, 1, 2, 3, 5, 8, 13, 21];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create(curve);
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA Create(ECParameters) should produce a usable key")]
    public void EcdsaCreateFromParametersShouldProduceUsableKey()
    {
        // Given: parameters generated independently by the BCL.
        using ECDsa source = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        ECParameters parameters = source.ExportParameters(includePrivateParameters: true);
        ReadOnlySpan<byte> data = [42, 43, 44, 45];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create(parameters);
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // When
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // Then
        Assert.True(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA signature should verify under a BCL ECDsa imported from the same exported public key")]
    public void EcdsaSignatureShouldVerifyUnderBclEcdsa()
    {
        // Given: this library signs the data.
        byte[] data = [7, 7, 7, 7, 7, 7, 7, 7];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // When: an independent BCL ECDsa is loaded with the exported SPKI public key.
        using ECDsa bcl = ECDsa.Create();
        bcl.ImportSubjectPublicKeyInfo(publicKey.Export(), out int _);

        // Then: the BCL verifies the library-produced signature (IEEE P1363 is the BCL default).
        Assert.True(bcl.VerifyData(data, signature, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA library should verify a signature produced by a BCL ECDsa over the same key")]
    public void EcdsaShouldVerifyBclProducedSignature()
    {
        // Given: a BCL ECDsa signs the data, and its EC private key is imported into this library.
        byte[] data = [3, 1, 4, 1, 5, 9, 2, 6];
        using ECDsa bcl = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        byte[] signature = bcl.SignData(data, HashAlgorithmName.SHA256);
        EcdsaPrivateKey privateKey = EcdsaPrivateKey.Import(bcl.ExportECPrivateKey());
        IEcdsaPublicKey publicKey = privateKey.GetPublicKey();

        // Then: this library verifies the BCL-produced signature.
        Assert.True(publicKey.IsDataValid(signature, data, HashAlgorithmName.SHA256));
    }

    [Fact(DisplayName = "ECDSA exported-then-imported public key should verify a signature from the original private key")]
    public void EcdsaExportedImportedPublicKeyShouldVerifyOriginalSignature()
    {
        // Given
        byte[] data = [11, 22, 33, 44];
        IEcdsaPrivateKey privateKey = EcdsaPrivateKey.Create();
        EcdsaPublicKey originalPublicKey = privateKey.GetPublicKey();
        byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256);

        // When: round-trip the public key through DER and PEM.
        EcdsaPublicKey fromDer = EcdsaPublicKey.Import(originalPublicKey.Export());
        EcdsaPublicKey fromPem = EcdsaPublicKey.ImportPem(originalPublicKey.ExportPem());

        // Then: both re-imported public keys verify the original signature.
        Assert.True(fromDer.IsDataValid(signature, data, HashAlgorithmName.SHA256));
        Assert.True(fromPem.IsDataValid(signature, data, HashAlgorithmName.SHA256));
    }
}
