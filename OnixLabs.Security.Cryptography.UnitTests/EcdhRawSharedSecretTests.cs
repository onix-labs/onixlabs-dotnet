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
using System.Collections.Concurrent;
using System.Security.Cryptography;
using OnixLabs.Security.Cryptography.Jose;
using OnixLabs.Security.Cryptography.UnitTests.Data;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class EcdhRawSharedSecretTests
{
    /// <summary>
    /// The ECPrivateKey encodings of the Wycheproof private scalars, keyed by curve and scalar.
    /// </summary>
    /// <remarks>
    /// Why: importing a bare scalar makes the platform compute the public point, which takes about 140 ms on macOS, and the 2,575 cases
    /// share only 94 distinct private keys.
    /// </remarks>
    private static readonly ConcurrentDictionary<string, byte[]> PrivateKeyCache = new();

    [Theory(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret should produce identical secrets of the curve's field size")]
    [InlineData("nistP256", 32)]
    [InlineData("nistP384", 48)]
    [InlineData("nistP521", 66)]
    public void EcdhDeriveRawSharedSecretShouldProduceIdenticalSecrets(string curveName, int expectedLength)
    {
        // Given
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(curve);
        IEcdhPrivateKey bob = EcdhPrivateKey.Create(curve);

        // When
        Secret aliceSecret = alice.DeriveRawSharedSecret(bob.GetPublicKey());
        Secret bobSecret = bob.DeriveRawSharedSecret(alice.GetPublicKey());

        // Then
        Assert.Equal(aliceSecret, bobSecret);
        Assert.Equal(expectedLength, aliceSecret.Length);
    }

    [Fact(DisplayName = "EC Diffie-Hellman shared secret derivation should throw ArgumentNullException for a null public key")]
    public void EcdhDeriveSharedSecretShouldThrowForNullPublicKey()
    {
        // Given
        IEcdhPrivateKey key = EcdhPrivateKey.Create();

        // When
        ArgumentNullException hashedException = Assert.Throws<ArgumentNullException>(() => key.DeriveSharedSecret(null!));
        ArgumentNullException rawException = Assert.Throws<ArgumentNullException>(() => key.DeriveRawSharedSecret(null!));

        // Then
        Assert.Equal("publicKey", hashedException.ParamName);
        Assert.Equal("publicKey", rawException.ParamName);
    }

    [Fact(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret should differ from the hashed DeriveSharedSecret")]
    public void EcdhDeriveRawSharedSecretShouldDifferFromDeriveSharedSecret()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create();
        IEcdhPublicKey bob = EcdhPrivateKey.Create().GetPublicKey();

        // When
        Secret raw = alice.DeriveRawSharedSecret(bob);
        Secret hashed = alice.DeriveSharedSecret(bob);

        // Then
        Assert.NotEqual(raw, hashed);
    }

    [Theory(DisplayName = "EC Diffie-Hellman DeriveSharedSecret should be the SHA-256 hash of DeriveRawSharedSecret")]
    [InlineData("nistP256")]
    [InlineData("nistP384")]
    [InlineData("nistP521")]
    public void EcdhDeriveSharedSecretShouldBeSha256OfDeriveRawSharedSecret(string curveName)
    {
        // Given
        ECCurve curve = ECCurve.CreateFromFriendlyName(curveName);
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(curve);
        IEcdhPublicKey bob = EcdhPrivateKey.Create(curve).GetPublicKey();

        // When
        Secret raw = alice.DeriveRawSharedSecret(bob);
        Secret hashed = alice.DeriveSharedSecret(bob);

        // Then
        Assert.Equal(SHA256.HashData(raw.AsReadOnlySpan()), hashed.AsReadOnlySpan().ToArray());
    }

    [Fact(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret should throw CryptographicException for a public key on another curve")]
    public void EcdhDeriveRawSharedSecretShouldThrowForPublicKeyOnAnotherCurve()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256);
        IEcdhPublicKey bob = EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP384).GetPublicKey();

        // When / Then
        Assert.Throws<CryptographicException>(() => alice.DeriveRawSharedSecret(bob));
        Assert.Throws<CryptographicException>(() => alice.DeriveSharedSecret(bob));
    }

    [Fact(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret should throw CryptographicException for malformed public key data")]
    public void EcdhDeriveRawSharedSecretShouldThrowForMalformedPublicKey()
    {
        // Given
        IEcdhPrivateKey alice = EcdhPrivateKey.Create();
        EcdhPublicKey malformed = new([0x30, 0x03, 0x02, 0x01, 0x00]);

        // When / Then
        Assert.Throws<CryptographicException>(() => alice.DeriveRawSharedSecret(malformed));
    }

    /// <summary>
    /// Verifies the SEC 2 v2 curve constants in <see cref="NistCurvePoint"/> against points generated by the platform, and that a point
    /// with a modified y-coordinate is rejected.
    /// </summary>
    [Theory(DisplayName = "NistCurvePoint should accept platform-generated points and reject modified ones")]
    [InlineData("nistP256")]
    [InlineData("nistP384")]
    [InlineData("nistP521")]
    public void NistCurvePointShouldAcceptGeneratedPointsAndRejectModifiedOnes(string curveName)
    {
        for (int i = 0; i < 32; i++)
        {
            // Given
            using ECDiffieHellman key = ECDiffieHellman.Create(ECCurve.CreateFromFriendlyName(curveName));
            ECParameters parameters = key.ExportParameters(false);
            ECParameters modified = parameters;
            modified.Q.Y = [..parameters.Q.Y!];
            modified.Q.Y[^1] ^= 1;

            // When
            bool generated = NistCurvePoint.IsOnCurve(parameters);
            bool tampered = NistCurvePoint.IsOnCurve(modified);

            // Then
            Assert.True(generated);
            Assert.False(tampered);
        }
    }

    /// <summary>
    /// Verifies the shared secret Z of RFC 7518 Appendix C, from Alice's ephemeral private key and Bob's public key, both given there as
    /// JWKs.
    /// </summary>
    [Fact(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret must produce the Z of RFC 7518 Appendix C")]
    public void EcdhDeriveRawSharedSecretMustProduceRfc7518AppendixCZ()
    {
        // Given
        using ECDiffieHellman aliceAlgorithm = ECDiffieHellman.Create(new ECParameters
        {
            Curve = ECCurve.NamedCurves.nistP256,
            Q = new ECPoint
            {
                X = Base64Url.Decode("gI0GAILBdu7T53akrFmMyGcsF3n5dO7MmwNBHKW5SV0"),
                Y = Base64Url.Decode("SLW_xSffzlPWrHEVI30DHM_4egVwt3NQqeUD7nMFpps")
            },
            D = Base64Url.Decode("0_NxaRPUMQoAJt50Gz8YiTr8gRTwyEaCumd-MToTmIo")
        });
        using ECDiffieHellman bobAlgorithm = ECDiffieHellman.Create(new ECParameters
        {
            Curve = ECCurve.NamedCurves.nistP256,
            Q = new ECPoint
            {
                X = Base64Url.Decode("weNJy2HscCSM6AEDTDg04biOvhFhyyWvOHQfeF_PxMQ"),
                Y = Base64Url.Decode("e8lnCO-AlStT-NJVX-crhB7QRYhiix03illJOVAOyck")
            }
        });
        EcdhPrivateKey alice = EcdhPrivateKey.Import(aliceAlgorithm.ExportECPrivateKey());
        EcdhPublicKey bob = EcdhPublicKey.Import(bobAlgorithm.ExportSubjectPublicKeyInfo());
        byte[] expected =
        [
            158, 86, 217, 29, 129, 113, 53, 211, 114, 131, 66, 131, 191, 132,
            38, 156, 251, 49, 110, 163, 218, 128, 106, 72, 246, 218, 167, 121,
            140, 254, 144, 196
        ];

        // When
        Secret actual = alice.DeriveRawSharedSecret(bob);

        // Then
        Assert.Equal(expected, actual.AsReadOnlySpan().ToArray());
    }

    /// <summary>
    /// Verifies every case of Project Wycheproof's <c>testvectors_v1/ecdh_secp256r1_test.json</c>, <c>ecdh_secp384r1_test.json</c> and
    /// <c>ecdh_secp521r1_test.json</c>. Valid cases must produce the shared secret. Invalid cases, including every point that is not on the
    /// curve, must throw <see cref="CryptographicException"/>. Acceptable cases may do either.
    /// </summary>
    [Theory(DisplayName = "EC Diffie-Hellman DeriveRawSharedSecret must satisfy Project Wycheproof ECDH test vectors")]
    [WycheproofEcdhData("secp256r1")]
    [WycheproofEcdhData("secp384r1")]
    [WycheproofEcdhData("secp521r1")]
    public void EcdhMustSatisfyWycheproofTestVectors(
        int tcId, string curve, byte[] privateKey, byte[] publicKey, byte[] shared, string result, string flags)
    {
        // Given
        _ = (tcId, flags);
        EcdhPrivateKey key = CreatePrivateKey(curve, privateKey);
        EcdhPublicKey otherKey = new(publicKey);

        // When
        byte[]? actual = null;
        Exception? exception = Record.Exception(() => actual = key.DeriveRawSharedSecret(otherKey).AsReadOnlySpan().ToArray());

        // Then
        switch (result)
        {
            case "valid":
                Assert.Null(exception);
                Assert.Equal(shared, actual);
                break;
            case "invalid":
                Assert.IsAssignableFrom<CryptographicException>(exception);
                break;
            default:
                if (exception is null) Assert.Equal(shared, actual);
                else Assert.IsAssignableFrom<CryptographicException>(exception);
                break;
        }
    }

    /// <summary>
    /// Creates an EC Diffie-Hellman private key from a raw big-endian Wycheproof private scalar, which may carry a leading zero byte.
    /// </summary>
    /// <param name="curve">The Wycheproof curve name.</param>
    /// <param name="scalar">The raw big-endian private scalar.</param>
    /// <returns>Returns a new <see cref="EcdhPrivateKey"/> holding the scalar.</returns>
    private static EcdhPrivateKey CreatePrivateKey(string curve, byte[] scalar)
    {
        (ECCurve namedCurve, int length) = curve switch
        {
            "secp256r1" => (ECCurve.NamedCurves.nistP256, 32),
            "secp384r1" => (ECCurve.NamedCurves.nistP384, 48),
            "secp521r1" => (ECCurve.NamedCurves.nistP521, 66),
            _ => throw new ArgumentException($"Unsupported curve: {curve}", nameof(curve))
        };

        byte[] encoded = PrivateKeyCache.GetOrAdd($"{curve}:{Convert.ToHexString(scalar)}", _ =>
        {
            ReadOnlySpan<byte> significant = scalar.AsSpan().TrimStart((byte)0);
            byte[] d = new byte[length];
            significant.CopyTo(d.AsSpan(length - significant.Length));

            using ECDiffieHellman algorithm = ECDiffieHellman.Create();
            algorithm.ImportParameters(new ECParameters { Curve = namedCurve, D = d });
            return algorithm.ExportECPrivateKey();
        });

        return EcdhPrivateKey.Import(encoded);
    }
}
