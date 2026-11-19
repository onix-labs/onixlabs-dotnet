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
using System.Numerics;
using System.Security.Cryptography;
using OnixLabs.Security.Cryptography.UnitTests.Data;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class XdhKeyTests
{
    [Fact(DisplayName = "X25519 DeriveSharedSecret should produce identical secrets")]
    public void XdhDeriveSharedSecretShouldProduceIdenticalSecrets()
    {
        // Given
        IXdhPrivateKey alice = XdhPrivateKey.Create();
        IXdhPrivateKey bob = XdhPrivateKey.Create();

        // When
        Secret aliceSecret = alice.DeriveSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
        Assert.Equal(32, aliceSecret.Length);
    }

    [Fact(DisplayName = "X25519 DeriveSharedSecret should be the SHA-256 hash of DeriveRawSharedSecret")]
    public void XdhDeriveSharedSecretShouldBeSha256OfDeriveRawSharedSecret()
    {
        // Given
        IXdhPrivateKey alice = XdhPrivateKey.Create();
        IXdhPublicKey bob = XdhPrivateKey.Create().GetPublicKey();

        // When
        Secret raw = alice.DeriveRawSharedSecret(bob);
        Secret hashed = alice.DeriveSharedSecret(bob);

        // Then
        Assert.NotEqual(raw, hashed);
        Assert.Equal(SHA256.HashData(raw.AsReadOnlySpan()), hashed.AsReadOnlySpan().ToArray());
    }

    /// <summary>
    /// Verifies the two single-step X25519 test vectors of RFC 7748 §5.2.
    /// </summary>
    [Theory(DisplayName = "X25519 must satisfy the RFC 7748 §5.2 single-step test vectors")]
    [InlineData(
        "a546e36bf0527c9d3b16154b82465edd62144c0ac1fc5a18506a2244ba449ac4",
        "e6db6867583030db3594c1a424b15f7c726624ec26b3353b10a903a6d0ab1c4c",
        "c3da55379de9c6908e94ea4df28d084f32eccf03491c71f754b4075577a28552")]
    [InlineData(
        "4b66e9d4d1b4673c5ad22691957d6af5c11b6421e0ea01d42ca4169e7918ba0d",
        "e5210f12786811d3f4b7959d0538ae2c31dbe7106fc03c3efc4cd549c715a493",
        "95cbde9476e8907d7aade45cb4b873f88b595a68799fa152e6f8f7647aac7957")]
    public void X25519MustSatisfyRfc7748SingleStepTestVectors(string scalar, string uCoordinate, string expected)
    {
        // Given
        byte[] actual = new byte[X25519.PublicKeySizeInBytes];

        // When
        X25519.ScalarMultiply(Convert.FromHexString(scalar), Convert.FromHexString(uCoordinate), actual);

        // Then
        Assert.Equal(expected, Convert.ToHexString(actual).ToLowerInvariant());
    }

    /// <summary>
    /// Verifies the iterated X25519 test vectors of RFC 7748 §5.2 after 1 and 1,000 iterations.
    /// </summary>
    [Theory(DisplayName = "X25519 must satisfy the RFC 7748 §5.2 iterated test vectors")]
    [InlineData(1, "422c8e7a6227d7bca1350b3e2bb7279f7897b87bb6854b783c60e80311ae3079")]
    [InlineData(1_000, "684cf59ba83309552800ef566f2f4d3c1c3887c49360e3875f2eb94d99532c51")]
    public void X25519MustSatisfyRfc7748IteratedTestVectors(int iterations, string expected)
    {
        // When
        string actual = Iterate(iterations);

        // Then
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Verifies the RFC 7748 §5.2 iterated X25519 test vector after 1,000,000 iterations. It is explicit because it is slow.
    /// </summary>
    [Fact(DisplayName = "X25519 must satisfy the RFC 7748 §5.2 iterated test vector after 1,000,000 iterations", Explicit = true)]
    public void X25519MustSatisfyRfc7748MillionIterationTestVector()
    {
        // When
        string actual = Iterate(1_000_000);

        // Then
        Assert.Equal("7c3911e0ab2586fd864497297e575e6f3bc601c0883c30df5f4dd2d24f665424", actual);
    }

    /// <summary>
    /// Verifies the Diffie-Hellman test vector of RFC 7748 §6.1, through the public key types.
    /// </summary>
    [Fact(DisplayName = "X25519 must satisfy the RFC 7748 §6.1 Diffie-Hellman test vector")]
    public void XdhMustSatisfyRfc7748DiffieHellmanTestVector()
    {
        // Given
        const string alicePrivate = "77076d0a7318a57d3c16c17251b26645df4c2f87ebc0992ab177fba51db92c2a";
        const string alicePublic = "8520f0098930a754748b7ddcb43ef75a0dbf3a0d26381af4eba4a98eaa9b4e6a";
        const string bobPrivate = "5dab087e624a8a4b79e17f8b83800ee66f3bb1292618b6fd1c2f8b27ff88e0eb";
        const string bobPublic = "de9edb7d7b7dc1b4d35b61c2ece435373f8343c85b78674dadfc7e146f882b4f";
        const string expectedSecret = "4a5d9d5ba4ce2de1728e3bf480350f25e07e21c947d19e3376f09b3c1e161742";
        XdhPrivateKey alice = XdhPrivateKey.Import(Convert.FromHexString(alicePrivate));
        XdhPrivateKey bob = XdhPrivateKey.Import(Convert.FromHexString(bobPrivate));

        // When
        XdhPublicKey alicePublicKey = alice.GetPublicKey();
        XdhPublicKey bobPublicKey = bob.GetPublicKey();
        Secret aliceSecret = alice.DeriveRawSharedSecret(bobPublicKey);
        Secret bobSecret = bob.DeriveRawSharedSecret(alicePublicKey);

        // Then
        Assert.Equal(alicePublic, Convert.ToHexString(alicePublicKey.Export()).ToLowerInvariant());
        Assert.Equal(bobPublic, Convert.ToHexString(bobPublicKey.Export()).ToLowerInvariant());
        Assert.Equal(expectedSecret, Convert.ToHexString(aliceSecret.AsReadOnlySpan()).ToLowerInvariant());
        Assert.Equal(expectedSecret, Convert.ToHexString(bobSecret.AsReadOnlySpan()).ToLowerInvariant());
    }

    /// <summary>
    /// Verifies every case of Project Wycheproof's <c>testvectors_v1/x25519_test.json</c>. Every case has a defined output, which the
    /// X25519 function must produce. The private key type must also produce it, except where the output is all zeros
    /// (<c>ZeroSharedSecret</c>), which RFC 8037 §3.2 requires it to reject.
    /// </summary>
    [Theory(DisplayName = "X25519 must satisfy Project Wycheproof X25519 test vectors")]
    [WycheproofX25519Data]
    public void X25519MustSatisfyWycheproofTestVectors(
        int tcId,
        byte[] privateKey,
        byte[] publicKey,
        byte[] shared,
        string result,
        string flags)
    {
        // Given
        _ = tcId;
        byte[] actual = new byte[X25519.PublicKeySizeInBytes];
        XdhPrivateKey key = XdhPrivateKey.Import(privateKey);
        XdhPublicKey otherKey = XdhPublicKey.Import(publicKey);

        // When
        X25519.ScalarMultiply(privateKey, publicKey, actual);

        // Then
        Assert.Contains(result, (string[])["valid", "acceptable"]);
        Assert.Equal(shared, actual);

        if (flags.Contains("ZeroSharedSecret"))
            Assert.Throws<CryptographicException>(() => key.DeriveRawSharedSecret(otherKey));
        else
            Assert.Equal(shared, key.DeriveRawSharedSecret(otherKey).AsReadOnlySpan().ToArray());

        foreach (bool usePlatform in (bool[])[false, true])
        {
            using X25519 algorithm = X25519.ImportPrivateKey(privateKey, usePlatform);

            if (flags.Contains("ZeroSharedSecret"))
            {
                Assert.Throws<CryptographicException>(() => algorithm.DeriveRawSecretAgreement(publicKey));
            }
            else
            {
                Assert.Equal(shared, algorithm.DeriveRawSecretAgreement(publicKey));
            }
        }
    }

    /// <summary>
    /// Verifies that the managed code and the platform's implementation behave alike: they derive the same public keys and shared secrets
    /// for the same keys, in both directions, and report the same exceptions for the same faults. Where the platform has no X25519, both
    /// runs use the managed code.
    /// </summary>
    [Fact(DisplayName = "X25519 should behave the same with the managed code and the platform's implementation")]
    public void X25519ShouldBehaveTheSameWithBothImplementations()
    {
        for (int i = 0; i < 32; i++)
        {
            // Given
            byte[] privateKey = RandomNumberGenerator.GetBytes(X25519.PrivateKeySizeInBytes);
            byte[] otherPrivateKey = RandomNumberGenerator.GetBytes(X25519.PrivateKeySizeInBytes);
            using X25519 managed = X25519.ImportPrivateKey(privateKey, usePlatform: false);
            using X25519 platform = X25519.ImportPrivateKey(privateKey, usePlatform: true);
            using X25519 otherManaged = X25519.ImportPrivateKey(otherPrivateKey, usePlatform: false);
            using X25519 otherPlatform = X25519.ImportPrivateKey(otherPrivateKey, usePlatform: true);

            // When / Then
            Assert.Equal(managed.ExportPublicKey(), platform.ExportPublicKey());
            Assert.Equal(managed.ExportPrivateKey(), platform.ExportPrivateKey());
            Assert.Equal(managed.ExportPkcs8PrivateKey(), platform.ExportPkcs8PrivateKey());
            Assert.Equal(managed.ExportSubjectPublicKeyInfo(), platform.ExportSubjectPublicKeyInfo());
            Assert.Equal(managed.DeriveRawSecretAgreement(otherPlatform), platform.DeriveRawSecretAgreement(otherManaged));
            Assert.Equal(otherManaged.DeriveRawSecretAgreement(managed), otherPlatform.DeriveRawSecretAgreement(platform));
        }

        foreach (bool usePlatform in (bool[])[false, true])
        {
            using X25519 key = X25519.GenerateKey(usePlatform);
            using X25519 publicOnly = X25519.ImportPublicKey(key.ExportPublicKey(), usePlatform);

            Assert.Throws<CryptographicException>(() => key.DeriveRawSecretAgreement(new byte[32]));
            Assert.Throws<ArgumentException>(() => key.DeriveRawSecretAgreement(new byte[31]));
            Assert.Throws<ArgumentException>(() => key.DeriveRawSecretAgreement(key.ExportPublicKey(), new byte[31]));
            Assert.Throws<CryptographicException>(() => publicOnly.ExportPrivateKey());
            Assert.Throws<CryptographicException>(() => publicOnly.DeriveRawSecretAgreement(key));
            Assert.Throws<ArgumentException>(() => X25519.ImportPrivateKey(new byte[31], usePlatform));
            Assert.Throws<ArgumentException>(() => X25519.ImportPublicKey(new byte[33], usePlatform));

            key.Dispose();
            Assert.Throws<ObjectDisposedException>(() => key.ExportPublicKey());
        }
    }

    [Fact(DisplayName = "X25519 should import and export PKCS#8 and SubjectPublicKeyInfo matching XdhPrivateKey and XdhPublicKey")]
    public void X25519ShouldMatchKeyTypeEncodings()
    {
        // Given
        XdhPrivateKey privateKey = XdhPrivateKey.Create();

        // When
        using X25519 fromPkcs8 = X25519.ImportPkcs8PrivateKey(privateKey.ExportPkcs8());
        using X25519 fromSpki = X25519.ImportSubjectPublicKeyInfo(X25519Pkcs8.EncodePublicKey(privateKey.GetPublicKey().Export()));

        // Then
        Assert.Equal(privateKey.Export(), fromPkcs8.ExportPrivateKey());
        Assert.Equal(privateKey.GetPublicKey().Export(), fromSpki.ExportPublicKey());
        Assert.Equal(privateKey.ExportPkcs8(), fromPkcs8.ExportPkcs8PrivateKey());
    }

    [Fact(DisplayName = "XdhPrivateKey should throw CryptographicException for a private key that is not 32 bytes")]
    public void XdhPrivateKeyShouldThrowForWrongLengthPrivateKey()
    {
        // Given
        XdhPrivateKey key = new(new byte[31]);
        XdhPublicKey otherKey = XdhPrivateKey.Create().GetPublicKey();

        // When
        Exception? derive = Record.Exception(() => key.DeriveRawSharedSecret(otherKey));
        Exception? getPublicKey = Record.Exception(() => key.GetPublicKey());

        // Then
        Assert.IsType<CryptographicException>(derive);
        Assert.IsType<CryptographicException>(getPublicKey);
    }

    [Fact(DisplayName = "X25519 shared secret derivation should throw CryptographicException for an all-zero public key")]
    public void XdhDeriveSharedSecretShouldThrowForAllZeroPublicKey()
    {
        // Given
        XdhPrivateKey key = XdhPrivateKey.Create();
        XdhPublicKey zero = XdhPublicKey.Import(new byte[32]);

        // When
        Exception? hashedException = Record.Exception(() => key.DeriveSharedSecret(zero));
        Exception? rawException = Record.Exception(() => key.DeriveRawSharedSecret(zero));

        // Then
        Assert.IsType<CryptographicException>(hashedException);
        Assert.IsType<CryptographicException>(rawException);
    }

    [Fact(DisplayName = "X25519 shared secret derivation should throw CryptographicException for a public key of the wrong length")]
    public void XdhDeriveSharedSecretShouldThrowForWrongLengthPublicKey()
    {
        // Given
        XdhPrivateKey key = XdhPrivateKey.Create();
        XdhPublicKey wrongLength = new(new byte[31]);

        // When
        Exception? hashedException = Record.Exception(() => key.DeriveSharedSecret(wrongLength));
        Exception? rawException = Record.Exception(() => key.DeriveRawSharedSecret(wrongLength));

        // Then
        Assert.IsType<CryptographicException>(hashedException);
        Assert.IsType<CryptographicException>(rawException);
    }

    [Fact(DisplayName = "X25519 shared secret derivation should throw ArgumentNullException for a null public key")]
    public void XdhDeriveSharedSecretShouldThrowForNullPublicKey()
    {
        // Given
        XdhPrivateKey key = XdhPrivateKey.Create();

        // When
        ArgumentNullException hashedException = Assert.Throws<ArgumentNullException>(() => key.DeriveSharedSecret(null!));
        ArgumentNullException rawException = Assert.Throws<ArgumentNullException>(() => key.DeriveRawSharedSecret(null!));

        // Then
        Assert.Equal("publicKey", hashedException.ParamName);
        Assert.Equal("publicKey", rawException.ParamName);
    }

    [Theory(DisplayName = "XdhPrivateKey.Import and XdhPublicKey.Import should throw CryptographicException unless the key is 32 bytes")]
    [InlineData(0)]
    [InlineData(31)]
    [InlineData(33)]
    public void XdhImportShouldThrowUnlessKeyIsThirtyTwoBytes(int length)
    {
        // Given
        byte[] data = new byte[length];

        // When
        Exception? privateException = Record.Exception(() => XdhPrivateKey.Import(data));
        Exception? publicException = Record.Exception(() => XdhPublicKey.Import(data));

        // Then
        Assert.IsType<CryptographicException>(privateException);
        Assert.IsType<CryptographicException>(publicException);
    }

    [Fact(DisplayName = "XdhPrivateKey should be exportable and importable")]
    public void XdhPrivateKeyShouldBeExportableAndImportable()
    {
        // Given
        XdhPrivateKey expected = XdhPrivateKey.Create();

        // When
        XdhPrivateKey actual = XdhPrivateKey.Import(expected.Export());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPrivateKey should be exportable and importable as PKCS8")]
    public void XdhPrivateKeyShouldBeExportableAndImportableAsPkcs8()
    {
        // Given
        XdhPrivateKey expected = XdhPrivateKey.Create();

        // When
        XdhPrivateKey actual = XdhPrivateKey.ImportPkcs8(expected.ExportPkcs8());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPrivateKey should be exportable and importable as PKCS8 PEM")]
    public void XdhPrivateKeyShouldBeExportableAndImportableAsPkcs8Pem()
    {
        // Given
        XdhPrivateKey expected = XdhPrivateKey.Create();

        // When
        XdhPrivateKey actual = XdhPrivateKey.ImportPem(expected.ExportPkcs8Pem());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPrivateKey should be exportable and importable as encrypted PKCS8")]
    public void XdhPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8()
    {
        // Given
        XdhPrivateKey expected = XdhPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        XdhPrivateKey actual = XdhPrivateKey.ImportPkcs8(expected.ExportPkcs8("Password", parameters), "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPrivateKey should be exportable and importable as encrypted PKCS8 PEM")]
    public void XdhPrivateKeyShouldBeExportableAndImportableAsEncryptedPkcs8Pem()
    {
        // Given
        XdhPrivateKey expected = XdhPrivateKey.Create();
        PbeParameters parameters = new(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, 10);

        // When
        XdhPrivateKey actual = XdhPrivateKey.ImportPem(expected.ExportPkcs8Pem("Password", parameters), "Password");

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPublicKey should be exportable and importable")]
    public void XdhPublicKeyShouldBeExportableAndImportable()
    {
        // Given
        XdhPublicKey expected = XdhPrivateKey.Create().GetPublicKey();

        // When
        XdhPublicKey actual = XdhPublicKey.Import(expected.Export());

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "XdhPublicKey should be exportable and importable as PEM")]
    public void XdhPublicKeyShouldBeExportableAndImportableAsPem()
    {
        // Given
        XdhPublicKey expected = XdhPrivateKey.Create().GetPublicKey();

        // When
        XdhPublicKey actual = XdhPublicKey.ImportPem(expected.ExportPem());

        // Then
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Verifies the SubjectPublicKeyInfo encoding against the X25519 public key inside the certificate of RFC 8410 §10.2. That key is
    /// Alice's public key from RFC 7748 §6.1, and its SubjectPublicKeyInfo is the 44 bytes at offset 115 of the certificate.
    /// </summary>
    [Fact(DisplayName = "XdhPublicKey PEM must match the SubjectPublicKeyInfo in the RFC 8410 §10.2 certificate")]
    public void XdhPublicKeyPemMustMatchRfc8410Certificate()
    {
        // Given
        const string certificate =
            "MIIBLDCB36ADAgECAghWAUdKKo3DMDAFBgMrZXAwGTEXMBUGA1UEAwwOSUVURiBUZX" +
            "N0IERlbW8wHhcNMTYwODAxMTIxOTI0WhcNNDAxMjMxMjM1OTU5WjAZMRcwFQYDVQQD" +
            "DA5JRVRGIFRlc3QgRGVtbzAqMAUGAytlbgMhAIUg8AmJMKdUdIt93LQ+91oNvzoNJj" +
            "ga9OukqY6qm05qo0UwQzAPBgNVHRMBAf8EBTADAQEAMA4GA1UdDwEBAAQEAwIDCDAg" +
            "BgNVHQ4BAQAEFgQUmx9e7e0EM4Xk97xiPFl1uQvIuzswBQYDK2VwA0EAryMB/t3J5v" +
            "/BzKc9dNZIpDmAgs3babFOTQbs+BolzlDUwsPrdGxO3YNGhW7Ibz3OGhhlxXrCe1Cg" +
            "w1AH9efZBw==";
        byte[] expected = Convert.FromBase64String(certificate).AsSpan(115, 44).ToArray();
        XdhPublicKey publicKey = XdhPublicKey.Import(Convert.FromHexString(
            "8520f0098930a754748b7ddcb43ef75a0dbf3a0d26381af4eba4a98eaa9b4e6a"));

        // When
        string pem = publicKey.ExportPem();
        XdhPublicKey imported = XdhPublicKey.ImportPem(PemEncoding.WriteString("PUBLIC KEY", expected));

        // Then
        Assert.Equal(PemEncoding.WriteString("PUBLIC KEY", expected), pem);
        Assert.Equal(publicKey, imported);
    }

    [Fact(DisplayName = "XdhPrivateKey.ImportPkcs8 should throw CryptographicException for an Ed25519 private key")]
    public void XdhPrivateKeyImportPkcs8ShouldThrowForEd25519PrivateKey()
    {
        // Given
        byte[] ed25519 = EddsaPrivateKey.Create().ExportPkcs8();

        // When
        Exception? exception = Record.Exception(() => XdhPrivateKey.ImportPkcs8(ed25519));

        // Then
        Assert.IsType<CryptographicException>(exception);
    }

    [Fact(DisplayName = "XdhPrivateKey.ImportPem should throw CryptographicException for malformed PEM")]
    public void XdhPrivateKeyImportPemShouldThrowForMalformedPem()
    {
        // When
        Exception? exception = Record.Exception(() => XdhPrivateKey.ImportPem("not a PEM document"));

        // Then
        Assert.IsType<CryptographicException>(exception);
    }

    [Fact(DisplayName = "XdhPrivateKey.GetPublicKey should return the same key each time, and throw once the key is disposed")]
    public void XdhGetPublicKeyShouldBeStableAndThrowAfterDisposal()
    {
        // Given
        XdhPrivateKey key = XdhPrivateKey.Create();

        // When
        XdhPublicKey first = key.GetPublicKey();
        XdhPublicKey second = key.GetPublicKey();
        key.Dispose();

        // Then
        Assert.Equal(first, second);
        Assert.Throws<ObjectDisposedException>(() => key.GetPublicKey());
    }

    [Fact(DisplayName = "XdhPrivateKey and XdhPublicKey should be named XDH")]
    public void XdhKeysShouldBeNamedXdh()
    {
        // Given
        XdhPrivateKey privateKey = XdhPrivateKey.Create();

        // When
        NamedPrivateKey namedPrivateKey = privateKey.ToNamedPrivateKey();
        NamedPublicKey namedPublicKey = privateKey.GetPublicKey().ToNamedPublicKey();

        // Then
        Assert.Equal("XDH", namedPrivateKey.AlgorithmName);
        Assert.Equal("XDH", namedPublicKey.AlgorithmName);
    }

    /// <summary>
    /// Verifies X25519 against an independent <see cref="BigInteger"/> transcription of the RFC 7748 §5 procedure, for random scalars and
    /// u-coordinates, including u-coordinates with the top bit set and non-canonical values.
    /// </summary>
    [Fact(DisplayName = "X25519 should match a BigInteger reference implementation of RFC 7748 §5 for random inputs")]
    public void X25519ShouldMatchBigIntegerReference()
    {
        // Given
        Random random = new(Seed: 25519);
        byte[] actual = new byte[X25519.PublicKeySizeInBytes];

        for (int i = 0; i < 200; i++)
        {
            byte[] scalar = new byte[X25519.PublicKeySizeInBytes];
            byte[] uCoordinate = new byte[X25519.PublicKeySizeInBytes];
            random.NextBytes(scalar);
            random.NextBytes(uCoordinate);

            // When
            X25519.ScalarMultiply(scalar, uCoordinate, actual);

            // Then
            Assert.Equal(ReferenceX25519(scalar, uCoordinate), actual);
        }
    }

#if NET11_0_OR_GREATER
    /// <summary>
    /// Verifies, against .NET 11's independent <see cref="X25519DiffieHellman"/>, that random keys agree in both directions, and that the
    /// public key, PKCS#8 and SubjectPublicKeyInfo encodings match.
    /// </summary>
    [Fact(DisplayName = "X25519 should agree with X25519DiffieHellman for random keys in both directions")]
    public void XdhShouldAgreeWithX25519DiffieHellman()
    {
        for (int i = 0; i < 64; i++)
        {
            // Given
            XdhPrivateKey ours = XdhPrivateKey.Create();
            using X25519DiffieHellman theirs = X25519DiffieHellman.GenerateKey();
            using X25519DiffieHellman oursAsTheirs = X25519DiffieHellman.ImportPrivateKey(ours.Export());
            XdhPublicKey theirPublicKey = XdhPublicKey.Import(theirs.ExportPublicKey());

            // When
            byte[] ourSecret = ours.DeriveRawSharedSecret(theirPublicKey).AsReadOnlySpan().ToArray();
            byte[] theirSecret = theirs.DeriveRawSecretAgreement(ours.GetPublicKey().Export());

            // Then
            Assert.Equal(theirSecret, ourSecret);
            Assert.Equal(oursAsTheirs.ExportPublicKey(), ours.GetPublicKey().Export());
            Assert.Equal(oursAsTheirs.ExportPkcs8PrivateKey(), ours.ExportPkcs8());
            Assert.Equal(oursAsTheirs.ExportSubjectPublicKeyInfoPem(), ours.GetPublicKey().ExportPem());
            Assert.Equal(theirs.ExportPublicKey(), XdhPrivateKey.ImportPkcs8(theirs.ExportPkcs8PrivateKey()).GetPublicKey().Export());
        }
    }
#endif

    /// <summary>
    /// Computes X25519 with <see cref="BigInteger"/> arithmetic, transcribing the decoding, ladder and encoding of RFC 7748 §5 directly.
    /// </summary>
    /// <param name="scalar">The 32-byte scalar.</param>
    /// <param name="uCoordinate">The 32-byte u-coordinate.</param>
    /// <returns>Returns the 32-byte encoded result.</returns>
    private static byte[] ReferenceX25519(byte[] scalar, byte[] uCoordinate)
    {
        BigInteger p = BigInteger.Pow(2, 255) - 19;
        byte[] k = [..scalar];
        k[0] &= 248;
        k[31] &= 127;
        k[31] |= 64;
        byte[] u = [..uCoordinate];
        u[31] &= 127;

        BigInteger kValue = new(k, isUnsigned: true, isBigEndian: false);
        BigInteger x1 = new BigInteger(u, isUnsigned: true, isBigEndian: false) % p;
        BigInteger x2 = 1, z2 = 0, x3 = x1, z3 = 1;
        int swap = 0;

        for (int t = 254; t >= 0; t--)
        {
            int bit = (int)((kValue >> t) & 1);
            swap ^= bit;
            if (swap == 1) (x2, x3, z2, z3) = (x3, x2, z3, z2);
            swap = bit;

            BigInteger a = (x2 + z2) % p;
            BigInteger aa = a * a % p;
            BigInteger b = ((x2 - z2) % p + p) % p;
            BigInteger bb = b * b % p;
            BigInteger e = ((aa - bb) % p + p) % p;
            BigInteger c = (x3 + z3) % p;
            BigInteger d = ((x3 - z3) % p + p) % p;
            BigInteger da = d * a % p;
            BigInteger cb = c * b % p;
            x3 = BigInteger.Pow(da + cb, 2) % p;
            z3 = x1 * BigInteger.Pow(((da - cb) % p + p) % p, 2) % p;
            x2 = aa * bb % p;
            z2 = e * (aa + 121665 * e) % p;
        }

        if (swap == 1) (x2, z2) = (x3, z3);

        BigInteger result = x2 * BigInteger.ModPow(z2, p - 2, p) % p;
        byte[] output = new byte[32];
        result.TryWriteBytes(output, out int _, isUnsigned: true, isBigEndian: false);
        return output;
    }

    /// <summary>
    /// Runs the iterated X25519 procedure of RFC 7748 §5.2: starting from k = u = 9, each iteration sets k to X25519(k, u) and u to the
    /// old value of k.
    /// </summary>
    /// <param name="iterations">The number of iterations to run.</param>
    /// <returns>Returns the final value of k as lower-case hexadecimal.</returns>
    private static string Iterate(int iterations)
    {
        byte[] k = new byte[X25519.PublicKeySizeInBytes];
        byte[] u = new byte[X25519.PublicKeySizeInBytes];
        byte[] output = new byte[X25519.PublicKeySizeInBytes];
        k[0] = 9;
        u[0] = 9;

        for (int i = 0; i < iterations; i++)
        {
            X25519.ScalarMultiply(k, u, output);
            (u, k, output) = (k, output, u);
        }

        return Convert.ToHexString(k).ToLowerInvariant();
    }
}
