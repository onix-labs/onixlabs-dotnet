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
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using OnixLabs.Security.Cryptography.Jose;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents benchmarks for JOSE: base64url, JSON Web Keys, and signing, verifying, encrypting and decrypting a 1 KiB DIDComm-sized
/// message with EdDSA, ES256, ECDH-ES and ECDH-1PU.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class JoseBenchmarks
{
    private readonly byte[] payload = RandomNumberGenerator.GetBytes(1024);
    private readonly EddsaPrivateKey eddsaKey = EddsaPrivateKey.Create();
    private readonly EcdsaPrivateKey ecdsaKey = EcdsaPrivateKey.Create(ECCurve.NamedCurves.nistP256);
    private readonly IJweKeyAgreement x25519Recipient = JweKeyAgreement.Create(XdhPrivateKey.Create(), "did:example:bob#key-x25519-1");
    private readonly IJweKeyAgreement x25519Sender = JweKeyAgreement.Create(XdhPrivateKey.Create(), "did:example:alice#key-x25519-1");
    private readonly IJweKeyAgreement p256Recipient =
        JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256), "did:example:bob#key-p256-1");
    private IJwsSigner eddsaSigner = null!;
    private IJwsSigner ecdsaSigner = null!;
    private JsonWebKey eddsaPublicKey = null!;
    private JsonWebKey ecdsaPublicKey = null!;
    private string eddsaJws = null!;
    private string ecdsaJws = null!;
    private string anoncryptX25519 = null!;
    private string authcryptX25519 = null!;
    private string anoncryptP256 = null!;
    private string encodedPayload = null!;
    private string privateJwk = null!;
    private JweEncryptionOptions anoncrypt = null!;
    private JweEncryptionOptions authcrypt = null!;

    /// <summary>
    /// Creates the keys, signatures and encrypted messages used by the benchmarks.
    /// </summary>
    /// <returns>Returns a task that completes when setup is done.</returns>
    [GlobalSetup]
    public async Task Setup()
    {
        eddsaSigner = JwsSigner.Create(eddsaKey, "did:example:alice#key-1");
        ecdsaSigner = JwsSigner.Create(ecdsaKey, "did:example:alice#key-2");
        eddsaPublicKey = JsonWebKey.Create(eddsaKey.GetPublicKey(), "did:example:alice#key-1");
        ecdsaPublicKey = JsonWebKey.Create(ecdsaKey.GetPublicKey(), "did:example:alice#key-2");
        eddsaJws = (await JsonWebSignature.SignAsync(payload, [eddsaSigner])).ToJson();
        ecdsaJws = (await JsonWebSignature.SignAsync(payload, [ecdsaSigner])).ToJson();
        anoncrypt = new JweEncryptionOptions();
        authcrypt = new JweEncryptionOptions { Algorithm = JweAlgorithm.Ecdh1PuA256Kw, Sender = x25519Sender };
        anoncryptX25519 = (await JsonWebEncryption.EncryptAsync(payload, [x25519Recipient.PublicKey], anoncrypt)).ToJson();
        authcryptX25519 = (await JsonWebEncryption.EncryptAsync(payload, [x25519Recipient.PublicKey], authcrypt)).ToJson();
        anoncryptP256 = (await JsonWebEncryption.EncryptAsync(payload, [p256Recipient.PublicKey], anoncrypt)).ToJson();
        encodedPayload = Base64Url.Encode(payload);
        privateJwk = JsonWebKey.Create(ecdsaKey).ToJson(includePrivateKey: true);
    }

    /// <summary>
    /// Disposes the private keys used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        eddsaKey.Dispose();
        ecdsaKey.Dispose();
    }

    /// <summary>
    /// Encodes the payload as base64url.
    /// </summary>
    /// <returns>Returns the encoded payload.</returns>
    [Benchmark]
    public string Base64UrlEncode() => Base64Url.Encode(payload);

    /// <summary>
    /// Decodes the payload from base64url.
    /// </summary>
    /// <returns>Returns the decoded payload.</returns>
    [Benchmark]
    public byte[] Base64UrlDecode() => Base64Url.Decode(encodedPayload);

    /// <summary>
    /// Encodes the payload as standard base64 with the platform, for reference.
    /// </summary>
    /// <returns>Returns the encoded payload.</returns>
    [Benchmark(Baseline = true)]
    public string ConvertToBase64String() => Convert.ToBase64String(payload);

    /// <summary>
    /// Parses a P-256 private JWK and converts it to an ECDSA private key.
    /// </summary>
    /// <returns>Returns the private key.</returns>
    [Benchmark]
    public EcdsaPrivateKey JsonWebKeyParseToEcdsaPrivateKey()
    {
        using JsonWebKey jwk = JsonWebKey.Parse(privateJwk);
        return jwk.ToEcdsaPrivateKey();
    }

    /// <summary>
    /// Computes the RFC 7638 thumbprint of a P-256 public JWK.
    /// </summary>
    /// <returns>Returns the thumbprint.</returns>
    [Benchmark]
    public byte[] JsonWebKeyComputeThumbprint() => ecdsaPublicKey.ComputeThumbprint();

    /// <summary>
    /// Signs the payload with EdDSA and serializes the JWS.
    /// </summary>
    /// <returns>Returns the JWS JSON.</returns>
    [Benchmark]
    public async Task<string> JwsSignEdDsa() => (await JsonWebSignature.SignAsync(payload, [eddsaSigner])).ToJson();

    /// <summary>
    /// Parses and verifies an EdDSA JWS.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark]
    public bool JwsVerifyEdDsa() => JsonWebSignature.Parse(eddsaJws).Verify(eddsaPublicKey);

    /// <summary>
    /// Signs the payload with ES256 and serializes the JWS.
    /// </summary>
    /// <returns>Returns the JWS JSON.</returns>
    [Benchmark]
    public async Task<string> JwsSignEs256() => (await JsonWebSignature.SignAsync(payload, [ecdsaSigner])).ToJson();

    /// <summary>
    /// Parses and verifies an ES256 JWS.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    [Benchmark]
    public bool JwsVerifyEs256() => JsonWebSignature.Parse(ecdsaJws).Verify(ecdsaPublicKey);

    /// <summary>
    /// Encrypts the payload to one X25519 recipient with ECDH-ES+A256KW and A256CBC-HS512.
    /// </summary>
    /// <returns>Returns the JWE JSON.</returns>
    [Benchmark]
    public async Task<string> JweEncryptAnoncryptX25519() =>
        (await JsonWebEncryption.EncryptAsync(payload, [x25519Recipient.PublicKey], anoncrypt)).ToJson();

    /// <summary>
    /// Parses and decrypts an ECDH-ES+A256KW, A256CBC-HS512 message as an X25519 recipient.
    /// </summary>
    /// <returns>Returns the plaintext.</returns>
    [Benchmark]
    public async Task<byte[]> JweDecryptAnoncryptX25519() => await JsonWebEncryption.Parse(anoncryptX25519).DecryptAsync(x25519Recipient);

    /// <summary>
    /// Encrypts the payload to one X25519 recipient with ECDH-1PU+A256KW and A256CBC-HS512.
    /// </summary>
    /// <returns>Returns the JWE JSON.</returns>
    [Benchmark]
    public async Task<string> JweEncryptAuthcryptX25519() =>
        (await JsonWebEncryption.EncryptAsync(payload, [x25519Recipient.PublicKey], authcrypt)).ToJson();

    /// <summary>
    /// Parses and decrypts an ECDH-1PU+A256KW, A256CBC-HS512 message as an X25519 recipient.
    /// </summary>
    /// <returns>Returns the plaintext.</returns>
    [Benchmark]
    public async Task<byte[]> JweDecryptAuthcryptX25519() =>
        await JsonWebEncryption.Parse(authcryptX25519).DecryptAsync(x25519Recipient, x25519Sender.PublicKey);

    /// <summary>
    /// Encrypts the payload to one P-256 recipient with ECDH-ES+A256KW and A256CBC-HS512.
    /// </summary>
    /// <returns>Returns the JWE JSON.</returns>
    [Benchmark]
    public async Task<string> JweEncryptAnoncryptP256() =>
        (await JsonWebEncryption.EncryptAsync(payload, [p256Recipient.PublicKey], anoncrypt)).ToJson();

    /// <summary>
    /// Parses and decrypts an ECDH-ES+A256KW, A256CBC-HS512 message as a P-256 recipient.
    /// </summary>
    /// <returns>Returns the plaintext.</returns>
    [Benchmark]
    public async Task<byte[]> JweDecryptAnoncryptP256() => await JsonWebEncryption.Parse(anoncryptP256).DecryptAsync(p256Recipient);
}
