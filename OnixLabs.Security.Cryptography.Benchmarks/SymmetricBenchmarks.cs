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
using BenchmarkDotNet.Attributes;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents benchmarks for the symmetric primitives that JOSE uses: the Concat KDF, ECDH-1PU key derivation, AES Key Wrap, and AES-CBC
/// with HMAC-SHA-2, which is compared with the platform's AES-GCM.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class SymmetricBenchmarks
{
    private readonly byte[] sharedSecret = RandomNumberGenerator.GetBytes(32);
    private readonly byte[] otherInfo = RandomNumberGenerator.GetBytes(64);
    private readonly byte[] derivedKey = new byte[32];
    private readonly byte[] keyEncryptionKey = RandomNumberGenerator.GetBytes(32);
    private readonly byte[] contentEncryptionKey = RandomNumberGenerator.GetBytes(64);
    private readonly byte[] unwrapped = new byte[64];
    private readonly byte[] iv = RandomNumberGenerator.GetBytes(AesCbcHmacSha2.IvLength);
    private readonly byte[] nonce = RandomNumberGenerator.GetBytes(12);
    private readonly byte[] associatedData = RandomNumberGenerator.GetBytes(256);
    private readonly byte[] tag = new byte[32];
    private readonly byte[] gcmTag = new byte[16];
    private byte[] wrapped = null!;
    private byte[] plaintext = null!;
    private byte[] ciphertext = null!;
    private byte[] decrypted = null!;
    private byte[] gcmCiphertext = null!;
    private AesCbcHmacSha2 cbcHmac = null!;
    private AesGcm gcm = null!;
    private Aes keyWrap = null!;

    /// <summary>
    /// Gets or sets the length of the message encrypted by the content encryption benchmarks.
    /// </summary>
    /// <value>The message length, in bytes.</value>
    [Params(1024, 65536)]
    public int MessageLength { get; set; } = 1024;

    /// <summary>
    /// Creates the keys and messages used by the benchmarks.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        plaintext = RandomNumberGenerator.GetBytes(MessageLength);
        keyWrap = Aes.Create();
        keyWrap.Key = keyEncryptionKey;
        wrapped = keyWrap.EncryptKeyWrap(contentEncryptionKey);
        cbcHmac = new AesCbcHmacSha2(contentEncryptionKey);
        gcm = new AesGcm(contentEncryptionKey.AsSpan(0, 32), 16);
        ciphertext = cbcHmac.Encrypt(iv, plaintext, tag, associatedData);
        decrypted = new byte[ciphertext.Length];
        gcmCiphertext = new byte[MessageLength];
    }

    /// <summary>
    /// Disposes the ciphers used by the benchmarks.
    /// </summary>
    [GlobalCleanup]
    public void Cleanup()
    {
        cbcHmac.Dispose();
        gcm.Dispose();
        keyWrap.Dispose();
    }

    /// <summary>
    /// Derives a 256-bit key with the Concat KDF and SHA-256.
    /// </summary>
    [Benchmark]
    public void ConcatKdfDeriveKey() => ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, sharedSecret, derivedKey, otherInfo);

    /// <summary>
    /// Derives a 256-bit ECDH-1PU key from two 32-byte shared secrets.
    /// </summary>
    [Benchmark]
    public void Ecdh1PuDeriveKey() => Ecdh1Pu.DeriveKey(sharedSecret, sharedSecret, derivedKey, otherInfo);

    /// <summary>
    /// Wraps a 512-bit content encryption key with A256KW: the library's own key wrap on .NET 8 to 10, and .NET's on .NET 11.
    /// </summary>
    /// <returns>Returns the wrapped key.</returns>
    [Benchmark]
    public byte[] AesEncryptKeyWrap() => keyWrap.EncryptKeyWrap(contentEncryptionKey);

    /// <summary>
    /// Unwraps a 512-bit content encryption key with A256KW: the library's own key wrap on .NET 8 to 10, and .NET's on .NET 11.
    /// </summary>
    /// <returns>Returns the length of the unwrapped key.</returns>
    [Benchmark]
    public int AesDecryptKeyWrap() => keyWrap.DecryptKeyWrap(wrapped, unwrapped);

    /// <summary>
    /// Encrypts the message with A256CBC-HS512.
    /// </summary>
    [Benchmark]
    public void AesCbcHmacSha2Encrypt() => cbcHmac.Encrypt(iv, plaintext, ciphertext, tag, associatedData);

    /// <summary>
    /// Verifies and decrypts the message with A256CBC-HS512.
    /// </summary>
    /// <returns>Returns the length of the plaintext.</returns>
    [Benchmark]
    public int AesCbcHmacSha2Decrypt() => cbcHmac.Decrypt(iv, ciphertext, tag, decrypted, associatedData);

    /// <summary>
    /// Encrypts the message with the platform's A256GCM, for reference.
    /// </summary>
    [Benchmark(Baseline = true)]
    public void AesGcmEncrypt() => gcm.Encrypt(nonce, plaintext, gcmCiphertext, gcmTag, associatedData);
}
