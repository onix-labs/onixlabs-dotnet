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
/// Represents a pure Ed25519 signing key (RFC 8032), holding either a private key and its public key, or a public key only.
/// </summary>
/// <remarks>
/// <para>
/// Why: .NET has no Ed25519 class, so the shape follows its newest signing class, <c>MLDsa</c>, and the key members of .NET 11's
/// <c>X25519DiffieHellman</c>: static <c>GenerateKey</c> and <c>Import…</c> methods, <c>Export…</c> methods, <c>SignData</c> and
/// <c>VerifyData</c>, size constants, and the same exceptions for the same faults. Pure Ed25519 has no context, so <c>SignData</c> and
/// <c>VerifyData</c> take none. The PEM and encrypted PKCS#8 members are left out, because <see cref="EddsaPrivateKey"/> and
/// <see cref="EddsaPublicKey"/> already provide them.
/// </para>
/// <para>
/// The private key is the 32-byte seed of RFC 8032 §5.1.5. Signing and public key derivation are constant time; verification handles only
/// public values.
/// </para>
/// </remarks>
internal sealed class Ed25519 : IDisposable
{
    /// <summary>
    /// The length in bytes of an Ed25519 private key, the seed of RFC 8032 §5.1.5.
    /// </summary>
    public const int PrivateKeySizeInBytes = 32;

    /// <summary>
    /// The length in bytes of an Ed25519 public key.
    /// </summary>
    public const int PublicKeySizeInBytes = 32;

    /// <summary>
    /// The length in bytes of an Ed25519 signature.
    /// </summary>
    public const int SignatureSizeInBytes = 64;

    /// <summary>
    /// The private key seed, or <see langword="null"/> if the current instance holds a public key only.
    /// </summary>
    private readonly byte[]? privateKey;

    /// <summary>
    /// The public key.
    /// </summary>
    private readonly byte[] publicKey;

    /// <summary>
    /// The exception message for a private key of the wrong length.
    /// </summary>
    private const string PrivateKeySizeMessage = "The private key is not the correct size for the indicated algorithm.";

    /// <summary>
    /// The exception message for a public key of the wrong length.
    /// </summary>
    private const string PublicKeySizeMessage = "The public key is not the correct size for the indicated algorithm.";

    /// <summary>
    /// A value indicating whether the current instance has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ed25519"/> class.
    /// </summary>
    /// <param name="privateKey">The private key seed, which the new instance takes ownership of, or <see langword="null"/> for a public
    /// key only.</param>
    /// <param name="publicKey">The public key, which the new instance takes ownership of.</param>
    private Ed25519(byte[]? privateKey, byte[] publicKey)
    {
        this.privateKey = privateKey;
        this.publicKey = publicKey;
    }

    /// <summary>
    /// Gets a value indicating whether Ed25519 is supported on the current platform.
    /// </summary>
    /// <value>Always <see langword="true"/>, because the implementation is managed code.</value>
    public static bool IsSupported => true;

    /// <summary>
    /// Generates a new Ed25519 private key from 32 random bytes.
    /// </summary>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the generated key.</returns>
    public static Ed25519 GenerateKey()
    {
        Span<byte> seed = stackalloc byte[PrivateKeySizeInBytes];

        try
        {
            RandomNumberGenerator.Fill(seed);
            return ImportPrivateKey(seed);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <summary>
    /// Imports the specified Ed25519 private key.
    /// </summary>
    /// <param name="source">The 32-byte private key seed.</param>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    public static Ed25519 ImportPrivateKey(ReadOnlySpan<byte> source)
    {
        RequireLength(source, PrivateKeySizeInBytes, PrivateKeySizeMessage, nameof(source));

        byte[] derivedPublicKey = new byte[PublicKeySizeInBytes];
        DerivePublicKey(source, derivedPublicKey);
        return new Ed25519(source.ToArray(), derivedPublicKey);
    }

    /// <summary>
    /// Imports the specified Ed25519 public key.
    /// </summary>
    /// <param name="source">The 32-byte public key.</param>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the public key only.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    /// <remarks>The key is decoded when it is used to verify, so an encoding that is not a point makes every verification fail.</remarks>
    public static Ed25519 ImportPublicKey(ReadOnlySpan<byte> source)
    {
        RequireLength(source, PublicKeySizeInBytes, PublicKeySizeMessage, nameof(source));
        return new Ed25519(null, source.ToArray());
    }

    /// <summary>
    /// Imports the specified Ed25519 private key from a PKCS#8 <c>PrivateKeyInfo</c> (RFC 8410).
    /// </summary>
    /// <param name="source">The DER-encoded PKCS#8 PrivateKeyInfo.</param>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="source"/> is not an Ed25519 PKCS#8 PrivateKeyInfo.</exception>
    public static Ed25519 ImportPkcs8PrivateKey(ReadOnlySpan<byte> source)
    {
        byte[] seed = Ed25519Pkcs8.DecodePrivateKey(source, out int _);

        try
        {
            return ImportPrivateKey(seed);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <summary>
    /// Imports the specified Ed25519 public key from a SubjectPublicKeyInfo (RFC 8410).
    /// </summary>
    /// <param name="source">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the public key only.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="source"/> is not an Ed25519 SubjectPublicKeyInfo.</exception>
    public static Ed25519 ImportSubjectPublicKeyInfo(ReadOnlySpan<byte> source) =>
        ImportPublicKey(Ed25519Pkcs8.DecodePublicKey(source, out int _));

    /// <summary>
    /// Exports the private key seed.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte private key seed.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportPrivateKey()
    {
        byte[] result = new byte[PrivateKeySizeInBytes];
        ExportPrivateKey(result);
        return result;
    }

    /// <summary>
    /// Exports the private key seed into the specified destination.
    /// </summary>
    /// <param name="destination">The buffer that receives the private key seed, which must be exactly 32 bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly 32 bytes.</exception>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void ExportPrivateKey(Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireDestination(destination, PrivateKeySizeInBytes);
        RequirePrivateKey().CopyTo(destination);
    }

    /// <summary>
    /// Exports the public key.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte public key.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportPublicKey()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        return [..publicKey];
    }

    /// <summary>
    /// Exports the public key into the specified destination.
    /// </summary>
    /// <param name="destination">The buffer that receives the public key, which must be exactly 32 bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly 32 bytes.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void ExportPublicKey(Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireDestination(destination, PublicKeySizeInBytes);
        publicKey.CopyTo(destination);
    }

    /// <summary>
    /// Exports the private key as a PKCS#8 <c>PrivateKeyInfo</c> (RFC 8410).
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded PKCS#8 PrivateKeyInfo.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportPkcs8PrivateKey()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        return Ed25519Pkcs8.EncodePrivateKey(RequirePrivateKey());
    }

    /// <summary>
    /// Exports the public key as a SubjectPublicKeyInfo (RFC 8410).
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportSubjectPublicKeyInfo()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        return Ed25519Pkcs8.EncodePublicKey(publicKey);
    }

    /// <summary>
    /// Signs the specified data, per RFC 8032 §5.1.6.
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the 64-byte signature.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] SignData(ReadOnlySpan<byte> data)
    {
        byte[] signature = new byte[SignatureSizeInBytes];
        SignData(data, signature);
        return signature;
    }

    /// <summary>
    /// Signs the specified data into the specified destination, per RFC 8032 §5.1.6.
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <param name="destination">The buffer that receives the signature, which must be exactly 64 bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly 64 bytes.</exception>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void SignData(ReadOnlySpan<byte> data, Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireDestination(destination, SignatureSizeInBytes);
        Sign(RequirePrivateKey(), publicKey, data, destination);
    }

    /// <summary>
    /// Verifies the specified signature of the specified data, per RFC 8032 §5.1.7.
    /// </summary>
    /// <param name="data">The data that was signed.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>, including when it is not 64
    /// bytes.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public bool VerifyData(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        return Verify(publicKey, data, signature);
    }

    /// <summary>
    /// Releases the key, zeroing the private key seed.
    /// </summary>
    public void Dispose()
    {
        if (isDisposed) return;

        if (privateKey is not null) CryptographicOperations.ZeroMemory(privateKey);
        isDisposed = true;
    }

    /// <summary>
    /// Imports the specified Ed25519 private key with its already derived public key.
    /// </summary>
    /// <param name="source">The 32-byte private key seed.</param>
    /// <param name="derivedPublicKey">The 32-byte public key derived from <paramref name="source"/>.</param>
    /// <returns>Returns a new <see cref="Ed25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="ArgumentException">Thrown when either key is not 32 bytes.</exception>
    /// <remarks>
    /// Why: <see cref="EddsaPrivateKey"/> derives its public key once and keeps it, so signing need not repeat that scalar multiplication.
    /// The public key must be the one derived from <paramref name="source"/>, and must never come from outside the library: signing the
    /// same message under a mismatched public key yields two signatures that together reveal the secret scalar.
    /// </remarks>
    internal static Ed25519 ImportPrivateKey(ReadOnlySpan<byte> source, ReadOnlySpan<byte> derivedPublicKey)
    {
        RequireLength(source, PrivateKeySizeInBytes, PrivateKeySizeMessage, nameof(source));
        RequireLength(derivedPublicKey, PublicKeySizeInBytes, PublicKeySizeMessage, nameof(derivedPublicKey));

        return new Ed25519(source.ToArray(), derivedPublicKey.ToArray());
    }

    /// <summary>
    /// Checks that the specified buffer is exactly the specified length.
    /// </summary>
    /// <param name="value">The buffer to check.</param>
    /// <param name="length">The required length, in bytes.</param>
    /// <param name="message">The exception message.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is not exactly <paramref name="length"/> bytes.</exception>
    private static void RequireLength(ReadOnlySpan<byte> value, int length, string message, string parameterName) =>
        Require(value.Length == length, message, parameterName);

    /// <summary>
    /// Checks that the specified destination is exactly the specified length.
    /// </summary>
    /// <param name="destination">The destination to check.</param>
    /// <param name="length">The required length, in bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly <paramref name="length"/>
    /// bytes.</exception>
    private static void RequireDestination(Span<byte> destination, int length) =>
        Require(destination.Length == length, $"Destination must be exactly {length} bytes.", nameof(destination));

    /// <summary>
    /// Gets the private key seed of the current instance.
    /// </summary>
    /// <returns>Returns the private key seed.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    private byte[] RequirePrivateKey() => privateKey ?? throw new CryptographicException(
        "Object contains only the public half of a key pair. A private key must also be provided.");

    /// <summary>
    /// Derives the 32-byte Ed25519 public key from a 32-byte seed.
    /// </summary>
    /// <param name="seed">The 32-byte Ed25519 seed.</param>
    /// <param name="publicKey">The 32-byte destination buffer that receives the derived public key.</param>
    private static void DerivePublicKey(ReadOnlySpan<byte> seed, Span<byte> publicKey)
    {
        Span<byte> hash = stackalloc byte[64];
        Span<byte> secretScalar = stackalloc byte[32];

        try
        {
            ExpandSeed(seed, hash, secretScalar, prefix: default);
            Ed25519Point a = secretScalar * Ed25519Point.BasePoint;
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
    /// <param name="seed">The 32-byte Ed25519 seed of the signing key.</param>
    /// <param name="publicKey">The 32-byte public key derived from <paramref name="seed"/> by <see cref="DerivePublicKey"/>.</param>
    /// <param name="message">The message to sign.</param>
    /// <param name="signature">The 64-byte destination buffer that receives the resulting signature.</param>
    /// <remarks>
    /// Why: accepting the public key lets callers derive it once per key rather than once per signature, removing one of the two scalar
    /// multiplications from signing. It must be the key derived from <paramref name="seed"/> and must never come from outside the library:
    /// signing the same message under a mismatched public key yields two signatures that together reveal the secret scalar.
    /// </remarks>
    private static void Sign(ReadOnlySpan<byte> seed, ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> message, Span<byte> signature)
    {
        Span<byte> hash = stackalloc byte[64];
        Span<byte> secretScalar = stackalloc byte[32];
        Span<byte> prefix = stackalloc byte[32];
        Span<byte> rBytesWide = stackalloc byte[64];
        Span<byte> rScalar = stackalloc byte[32];
        Span<byte> kBytesWide = stackalloc byte[64];
        Span<byte> kScalar = stackalloc byte[32];

        try
        {
            ExpandSeed(seed, hash, secretScalar, prefix);

            // r = SHA512(prefix || message) reduced mod L
            using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
            {
                sha.AppendData(prefix);
                sha.AppendData(message);
                sha.GetHashAndReset(rBytesWide);
            }

            Ed25519Scalar.ReduceFromWideBytes(rBytesWide, rScalar);

            // R = [r]B; write its encoding to signature[0..32]
            Ed25519Point r = rScalar * Ed25519Point.BasePoint;
            r.Encode(signature[..32]);

            // k = SHA512(R || A || message) reduced mod L
            using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
            {
                sha.AppendData(signature[..32]);
                sha.AppendData(publicKey);
                sha.AppendData(message);
                sha.GetHashAndReset(kBytesWide);
            }

            Ed25519Scalar.ReduceFromWideBytes(kBytesWide, kScalar);

            // S = (k * s + r) mod L; write to signature[32..64]
            Ed25519Scalar.MulAdd(kScalar, secretScalar, rScalar, signature[32..]);
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
    /// <param name="publicKey">The 32-byte Ed25519 public key.</param>
    /// <param name="message">The message that was signed.</param>
    /// <param name="signature">The 64-byte signature to verify.</param>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    private static bool Verify(ReadOnlySpan<byte> publicKey, ReadOnlySpan<byte> message, ReadOnlySpan<byte> signature)
    {
        if (publicKey.Length != PublicKeySizeInBytes) return false;
        if (signature.Length != SignatureSizeInBytes) return false;
        if (!Ed25519Scalar.IsCanonical(signature[32..])) return false;
        if (!Ed25519Point.TryDecode(publicKey, out Ed25519Point a)) return false;
        if (!Ed25519Point.TryDecode(signature[..32], out Ed25519Point r)) return false;

        Span<byte> kBytesWide = stackalloc byte[64];
        Span<byte> kScalar = stackalloc byte[32];

        using (IncrementalHash sha = IncrementalHash.CreateHash(HashAlgorithmName.SHA512))
        {
            sha.AppendData(signature[..32]);
            sha.AppendData(publicKey);
            sha.AppendData(message);
            sha.GetHashAndReset(kBytesWide);
        }

        Ed25519Scalar.ReduceFromWideBytes(kBytesWide, kScalar);

        Ed25519Point left = signature[32..] * Ed25519Point.BasePoint; // [S]B
        Ed25519Point right = r + (kScalar * a);                              // R + [k]A

        // Cofactored check: multiply each side by 8 (three doublings).
        left = Ed25519Point.Double(Ed25519Point.Double(Ed25519Point.Double(left)));
        right = Ed25519Point.Double(Ed25519Point.Double(Ed25519Point.Double(right)));

        return left == right;
    }

    /// <summary>
    /// Computes SHA-512 of the seed and splits it into a clamped secret scalar and prefix per RFC 8032 §5.1.5.
    /// </summary>
    /// <param name="seed">The 32-byte Ed25519 seed.</param>
    /// <param name="hash">The 64-byte scratch buffer that receives the SHA-512 digest.</param>
    /// <param name="secretScalar">The 32-byte destination buffer that receives the clamped secret scalar.</param>
    /// <param name="prefix">The 32-byte destination buffer that receives the per-signature prefix; if empty, the prefix is not written.</param>
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
