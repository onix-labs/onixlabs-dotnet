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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Pure Ed25519 (RFC 8032) sign, verify, and public-key derivation.
/// </summary>
internal static class Ed25519
{
    public const int SeedLength = 32;
    public const int PublicKeyLength = 32;
    public const int SignatureLength = 64;

    /// <summary>
    /// Derives the 32-byte Ed25519 public key from a 32-byte seed.
    /// </summary>
    public static void DerivePublicKey(ReadOnlySpan<byte> seed, Span<byte> publicKey)
    {
        Span<byte> hash = stackalloc byte[64];
        Span<byte> secretScalar = stackalloc byte[32];
        try
        {
            ExpandSeed(seed, hash, secretScalar, prefix: default);
            Edwards25519Point a = Edwards25519Point.ScalarMultiply(secretScalar, Edwards25519Point.BasePoint);
            a.Encode(publicKey);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(hash);
            CryptographicOperations.ZeroMemory(secretScalar);
        }
    }

    /// <summary>
    /// Computes a 64-byte Ed25519 signature of <paramref name="message"/> under the key derived
    /// from <paramref name="seed"/> per RFC 8032 §5.1.6.
    /// </summary>
    public static void Sign(ReadOnlySpan<byte> seed, ReadOnlySpan<byte> message, Span<byte> signature)
    {
        Span<byte> hash = stackalloc byte[64];
        Span<byte> secretScalar = stackalloc byte[32];
        Span<byte> prefix = stackalloc byte[32];
        Span<byte> publicKey = stackalloc byte[32];
        Span<byte> rBytesWide = stackalloc byte[64];
        Span<byte> rScalar = stackalloc byte[32];
        Span<byte> kBytesWide = stackalloc byte[64];
        Span<byte> kScalar = stackalloc byte[32];

        try
        {
            ExpandSeed(seed, hash, secretScalar, prefix);

            Edwards25519Point a = Edwards25519Point.ScalarMultiply(secretScalar, Edwards25519Point.BasePoint);
            a.Encode(publicKey);

            // r = SHA512(prefix || message) reduced mod L
            using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
            {
                sha.AppendData(prefix);
                sha.AppendData(message);
                sha.GetHashAndReset(rBytesWide);
            }
            Edwards25519Scalar.ReduceFromWideBytes(rBytesWide, rScalar);

            // R = [r]B; write its encoding to signature[0..32]
            Edwards25519Point r = Edwards25519Point.ScalarMultiply(rScalar, Edwards25519Point.BasePoint);
            r.Encode(signature[..32]);

            // k = SHA512(R || A || message) reduced mod L
            using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
            {
                sha.AppendData(signature[..32]);
                sha.AppendData(publicKey);
                sha.AppendData(message);
                sha.GetHashAndReset(kBytesWide);
            }
            Edwards25519Scalar.ReduceFromWideBytes(kBytesWide, kScalar);

            // S = (k * s + r) mod L; write to signature[32..64]
            Edwards25519Scalar.MulAdd(kScalar, secretScalar, rScalar, signature[32..]);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(hash);
            CryptographicOperations.ZeroMemory(secretScalar);
            CryptographicOperations.ZeroMemory(prefix);
            CryptographicOperations.ZeroMemory(rBytesWide);
            CryptographicOperations.ZeroMemory(rScalar);
            CryptographicOperations.ZeroMemory(kBytesWide);
            CryptographicOperations.ZeroMemory(kScalar);
        }
    }

    /// <summary>
    /// Verifies a 64-byte Ed25519 signature over <paramref name="message"/> against
    /// <paramref name="publicKey"/> per RFC 8032 §5.1.7 (cofactored: 8·[S]B = 8·R + 8·[k]A).
    /// </summary>
    public static bool Verify(ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> message, ReadOnlySpan<byte> signature)
    {
        if (publicKey.Length != PublicKeyLength) return false;
        if (signature.Length != SignatureLength) return false;
        if (!Edwards25519Scalar.IsCanonical(signature[32..])) return false;
        if (!Edwards25519Point.TryDecode(publicKey, out Edwards25519Point a)) return false;
        if (!Edwards25519Point.TryDecode(signature[..32], out Edwards25519Point r)) return false;

        Span<byte> kBytesWide = stackalloc byte[64];
        Span<byte> kScalar = stackalloc byte[32];

        using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
        {
            sha.AppendData(signature[..32]);
            sha.AppendData(publicKey);
            sha.AppendData(message);
            sha.GetHashAndReset(kBytesWide);
        }
        Edwards25519Scalar.ReduceFromWideBytes(kBytesWide, kScalar);

        Edwards25519Point left = Edwards25519Point.ScalarMultiply(signature[32..], Edwards25519Point.BasePoint); // [S]B
        Edwards25519Point right = Edwards25519Point.Add(r, Edwards25519Point.ScalarMultiply(kScalar, a));         // R + [k]A

        // Cofactored check: multiply each side by 8 (three doublings).
        left = Edwards25519Point.Double(Edwards25519Point.Double(Edwards25519Point.Double(left)));
        right = Edwards25519Point.Double(Edwards25519Point.Double(Edwards25519Point.Double(right)));

        return left.EncodingEquals(right);
    }

    /// <summary>
    /// SHA-512 of the seed and split into clamped secret scalar and prefix per RFC 8032 §5.1.5.
    /// </summary>
    private static void ExpandSeed(ReadOnlySpan<byte> seed, Span<byte> hash, Span<byte> secretScalar, Span<byte> prefix)
    {
        SHA512.HashData(seed, hash);
        hash[..32].CopyTo(secretScalar);
        secretScalar[0] &= 248;     // clear bits 0, 1, 2
        secretScalar[31] &= 127;    // clear bit 255
        secretScalar[31] |= 64;     // set bit 254
        if (!prefix.IsEmpty) hash[32..].CopyTo(prefix);
    }
}
