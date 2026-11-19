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
/// Represents an X25519 Diffie-Hellman key (RFC 7748 §5), over the same field GF(2^255 - 19) as Ed25519, holding either a private key
/// and its public key, or a public key only.
/// </summary>
/// <remarks>
/// <para>
/// Why: the shape follows .NET 11's <c>X25519DiffieHellman</c>: static <c>GenerateKey</c> and <c>Import…</c> methods, <c>Export…</c>
/// methods, <c>DeriveRawSecretAgreement</c>, the same size constants, and the same exceptions for the same faults. It does not derive from
/// that class, which only exists from .NET 11. The PEM and encrypted PKCS#8 members are left out, because <see cref="XdhPrivateKey"/> and
/// <see cref="XdhPublicKey"/> already provide them.
/// </para>
/// <para>
/// On .NET 11, where <c>X25519DiffieHellman.IsSupported</c> is <see langword="true"/>, the key is held by .NET's implementation, which is
/// faster; everywhere else the library's own managed code is used, so X25519 works on every platform and every .NET version. Both refuse
/// an all-zero shared secret, which RFC 7748 §6.1 permits and RFC 8037 §3.2 requires of JOSE, so callers see the same behaviour either
/// way.
/// </para>
/// </remarks>
internal sealed class X25519 : IDisposable
{
    /// <summary>
    /// The length in bytes of an X25519 private key.
    /// </summary>
    public const int PrivateKeySizeInBytes = 32;

    /// <summary>
    /// The length in bytes of an X25519 public key.
    /// </summary>
    public const int PublicKeySizeInBytes = 32;

    /// <summary>
    /// The length in bytes of an X25519 shared secret.
    /// </summary>
    public const int SecretAgreementSizeInBytes = 32;

    /// <summary>
    /// The number of scalar bits the Montgomery ladder processes. Clamping clears bit 255, so the ladder starts at bit 254.
    /// </summary>
    private const int ScalarBits = 255;

    /// <summary>
    /// The private key, or <see langword="null"/> if the current instance holds a public key only, or uses the platform's implementation.
    /// </summary>
    private readonly byte[]? privateKey;

    /// <summary>
    /// The public key.
    /// </summary>
    private readonly byte[] publicKey;

#if NET11_0_OR_GREATER
    /// <summary>
    /// The platform's implementation, or <see langword="null"/> if the managed code is used.
    /// </summary>
    private readonly X25519DiffieHellman? platform;
#endif

    /// <summary>
    /// A value indicating whether the current instance has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="X25519"/> class that uses the managed code.
    /// </summary>
    /// <param name="privateKey">The private key, which the new instance takes ownership of, or <see langword="null"/> for a public key
    /// only.</param>
    /// <param name="publicKey">The public key, which the new instance takes ownership of.</param>
    private X25519(byte[]? privateKey, byte[] publicKey)
    {
        this.privateKey = privateKey;
        this.publicKey = publicKey;
    }

#if NET11_0_OR_GREATER
    /// <summary>
    /// Initializes a new instance of the <see cref="X25519"/> class that uses the platform's implementation.
    /// </summary>
    /// <param name="platform">The platform's implementation, which the new instance takes ownership of.</param>
    private X25519(X25519DiffieHellman platform)
    {
        this.platform = platform;
        publicKey = platform.ExportPublicKey();
    }
#endif

    /// <summary>
    /// Gets a value indicating whether X25519 is supported on the current platform.
    /// </summary>
    /// <value>Always <see langword="true"/>, because the managed code is used wherever the platform's implementation is not.</value>
    public static bool IsSupported => true;

    /// <summary>
    /// Gets a value indicating whether the platform's implementation is used by default.
    /// </summary>
    /// <value><see langword="true"/> on .NET 11 where <c>X25519DiffieHellman.IsSupported</c> is <see langword="true"/>; otherwise,
    /// <see langword="false"/>.</value>
    internal static bool IsPlatformDefault { get; } =
#if NET11_0_OR_GREATER
        X25519DiffieHellman.IsSupported;
#else
        false;
#endif

    /// <summary>
    /// Generates a new X25519 private key from 32 random bytes.
    /// </summary>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the generated key.</returns>
    public static X25519 GenerateKey() => GenerateKey(IsPlatformDefault);

    /// <summary>
    /// Imports the specified X25519 private key.
    /// </summary>
    /// <param name="source">The 32-byte private key.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    public static X25519 ImportPrivateKey(ReadOnlySpan<byte> source) => ImportPrivateKey(source, IsPlatformDefault);

    /// <summary>
    /// Imports the specified X25519 public key.
    /// </summary>
    /// <param name="source">The 32-byte public key.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the public key only.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    public static X25519 ImportPublicKey(ReadOnlySpan<byte> source) => ImportPublicKey(source, IsPlatformDefault);

    /// <summary>
    /// Imports the specified X25519 private key from a PKCS#8 <c>PrivateKeyInfo</c> (RFC 8410).
    /// </summary>
    /// <param name="source">The DER-encoded PKCS#8 PrivateKeyInfo.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="source"/> is not an X25519 PKCS#8 PrivateKeyInfo.</exception>
    public static X25519 ImportPkcs8PrivateKey(ReadOnlySpan<byte> source)
    {
        byte[] key = X25519Pkcs8.DecodePrivateKey(source, out int _);

        try
        {
            return ImportPrivateKey(key);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }

    /// <summary>
    /// Imports the specified X25519 public key from a SubjectPublicKeyInfo (RFC 8410).
    /// </summary>
    /// <param name="source">The DER-encoded SubjectPublicKeyInfo.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the public key only.</returns>
    /// <exception cref="CryptographicException">Thrown when <paramref name="source"/> is not an X25519 SubjectPublicKeyInfo.</exception>
    public static X25519 ImportSubjectPublicKeyInfo(ReadOnlySpan<byte> source) =>
        ImportPublicKey(X25519Pkcs8.DecodePublicKey(source, out int _));

    /// <summary>
    /// Exports the private key.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte private key.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportPrivateKey()
    {
        byte[] result = new byte[PrivateKeySizeInBytes];
        ExportPrivateKey(result);
        return result;
    }

    /// <summary>
    /// Exports the private key into the specified destination.
    /// </summary>
    /// <param name="destination">The buffer that receives the private key, which must be exactly 32 bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly 32 bytes.</exception>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void ExportPrivateKey(Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        RequireDestination(destination, PrivateKeySizeInBytes);

#if NET11_0_OR_GREATER
        if (platform is not null)
        {
            platform.ExportPrivateKey(destination);
            return;
        }
#endif

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
        Span<byte> key = stackalloc byte[PrivateKeySizeInBytes];

        try
        {
            ExportPrivateKey(key);
            return X25519Pkcs8.EncodePrivateKey(key);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }

    /// <summary>
    /// Exports the public key as a SubjectPublicKeyInfo (RFC 8410).
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the DER-encoded SubjectPublicKeyInfo.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] ExportSubjectPublicKeyInfo()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        return X25519Pkcs8.EncodePublicKey(publicKey);
    }

    /// <summary>
    /// Derives the raw shared secret, X25519(k, u), with the other party's key.
    /// </summary>
    /// <param name="otherParty">The other party's key; only its public key is used.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte shared secret.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current instance holds a public key only, or the shared secret is all zeros, because the other party's public key is
    /// a point of small order.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when either instance has been disposed.</exception>
    public byte[] DeriveRawSecretAgreement(X25519 otherParty)
    {
        RequireNotNull(otherParty);
        return DeriveRawSecretAgreement(otherParty.ExportPublicKey());
    }

    /// <summary>
    /// Derives the raw shared secret, X25519(k, u), with the other party's public key.
    /// </summary>
    /// <param name="otherPartyPublicKey">The other party's 32-byte public key.</param>
    /// <returns>Returns a new <see cref="byte"/> array containing the 32-byte shared secret.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="otherPartyPublicKey"/> is not 32 bytes.</exception>
    /// <exception cref="CryptographicException">
    /// Thrown when the current instance holds a public key only, or the shared secret is all zeros, because the other party's public key is
    /// a point of small order.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public byte[] DeriveRawSecretAgreement(ReadOnlySpan<byte> otherPartyPublicKey)
    {
        byte[] result = new byte[SecretAgreementSizeInBytes];
        DeriveRawSecretAgreement(otherPartyPublicKey, result);
        return result;
    }

    /// <summary>
    /// Derives the raw shared secret, X25519(k, u), with the other party's key, into the specified destination.
    /// </summary>
    /// <param name="otherParty">The other party's key; only its public key is used.</param>
    /// <param name="destination">The buffer that receives the shared secret, which must be exactly 32 bytes.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="destination"/> is not exactly 32 bytes.</exception>
    /// <exception cref="CryptographicException">
    /// Thrown when the current instance holds a public key only, or the shared secret is all zeros, because the other party's public key is
    /// a point of small order.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when either instance has been disposed.</exception>
    public void DeriveRawSecretAgreement(X25519 otherParty, Span<byte> destination)
    {
        RequireNotNull(otherParty);
        DeriveRawSecretAgreement(otherParty.ExportPublicKey(), destination);
    }

    /// <summary>
    /// Derives the raw shared secret, X25519(k, u), with the other party's public key, into the specified destination.
    /// </summary>
    /// <param name="otherPartyPublicKey">The other party's 32-byte public key.</param>
    /// <param name="destination">The buffer that receives the shared secret, which must be exactly 32 bytes.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="otherPartyPublicKey"/> is not 32 bytes, or <paramref name="destination"/> is not exactly 32 bytes.
    /// </exception>
    /// <exception cref="CryptographicException">
    /// Thrown when the current instance holds a public key only, or the shared secret is all zeros, because the other party's public key is
    /// a point of small order. The destination is zeroed.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown when the current instance has been disposed.</exception>
    public void DeriveRawSecretAgreement(ReadOnlySpan<byte> otherPartyPublicKey, Span<byte> destination)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);
        Require(
            otherPartyPublicKey.Length == PublicKeySizeInBytes,
            "The public key is not the correct size for the indicated algorithm.",
            nameof(otherPartyPublicKey));
        RequireDestination(destination, SecretAgreementSizeInBytes);

#if NET11_0_OR_GREATER
        if (platform is not null)
        {
            try
            {
                platform.DeriveRawSecretAgreement(otherPartyPublicKey, destination);
                return;
            }
            catch
            {
                CryptographicOperations.ZeroMemory(destination);
                throw;
            }
        }
#endif

        ScalarMultiply(RequirePrivateKey(), otherPartyPublicKey, destination);

        if (!IsAllZero(destination)) return;

        CryptographicOperations.ZeroMemory(destination);
        throw new CryptographicException("The X25519 shared secret is all zeros: the other party's public key is a point of small order.");
    }

    /// <summary>
    /// Releases the key, zeroing the private key.
    /// </summary>
    public void Dispose()
    {
        if (isDisposed) return;

        if (privateKey is not null) CryptographicOperations.ZeroMemory(privateKey);
#if NET11_0_OR_GREATER
        platform?.Dispose();
#endif
        isDisposed = true;
    }

    /// <summary>
    /// Generates a new X25519 private key from 32 random bytes, with the specified implementation.
    /// </summary>
    /// <param name="usePlatform">Whether to use the platform's implementation, if it is supported.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the generated key.</returns>
    /// <remarks>Why: the tests run every case through both implementations.</remarks>
    internal static X25519 GenerateKey(bool usePlatform)
    {
        Span<byte> key = stackalloc byte[PrivateKeySizeInBytes];

        try
        {
            RandomNumberGenerator.Fill(key);
            return ImportPrivateKey(key, usePlatform);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(key);
        }
    }

    /// <summary>
    /// Imports the specified X25519 private key, with the specified implementation.
    /// </summary>
    /// <param name="source">The 32-byte private key.</param>
    /// <param name="usePlatform">Whether to use the platform's implementation, if it is supported.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the private key and its public key.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    internal static X25519 ImportPrivateKey(ReadOnlySpan<byte> source, bool usePlatform)
    {
        Require(
            source.Length == PrivateKeySizeInBytes,
            "The private key is not the correct size for the indicated algorithm.",
            nameof(source));

#if NET11_0_OR_GREATER
        if (usePlatform && X25519DiffieHellman.IsSupported) return new X25519(X25519DiffieHellman.ImportPrivateKey(source));
#endif

        byte[] publicKey = new byte[PublicKeySizeInBytes];
        Span<byte> basePoint = stackalloc byte[PublicKeySizeInBytes];
        basePoint.Clear();
        basePoint[0] = 9;
        ScalarMultiply(source, basePoint, publicKey);

        return new X25519(source.ToArray(), publicKey);
    }

    /// <summary>
    /// Imports the specified X25519 public key, with the specified implementation.
    /// </summary>
    /// <param name="source">The 32-byte public key.</param>
    /// <param name="usePlatform">Whether to use the platform's implementation, if it is supported.</param>
    /// <returns>Returns a new <see cref="X25519"/> instance holding the public key only.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> is not 32 bytes.</exception>
    internal static X25519 ImportPublicKey(ReadOnlySpan<byte> source, bool usePlatform)
    {
        Require(
            source.Length == PublicKeySizeInBytes,
            "The public key is not the correct size for the indicated algorithm.",
            nameof(source));

#if NET11_0_OR_GREATER
        if (usePlatform && X25519DiffieHellman.IsSupported) return new X25519(X25519DiffieHellman.ImportPublicKey(source));
#endif

        return new X25519(null, source.ToArray());
    }

    /// <summary>
    /// Computes the X25519 function of RFC 7748 §5: the u-coordinate of the scalar multiple of the point with the specified u-coordinate.
    /// </summary>
    /// <param name="scalar">The 32-byte scalar, which is clamped before use.</param>
    /// <param name="uCoordinate">The 32-byte little-endian u-coordinate, whose most significant bit is ignored.</param>
    /// <param name="destination">The 32-byte destination buffer that receives the resulting u-coordinate.</param>
    /// <exception cref="ArgumentException">Thrown when any of the buffers is not exactly 32 bytes long.</exception>
    /// <remarks>
    /// The ladder runs for every scalar bit and swaps with <see cref="Ed25519FieldElement.ConditionalSwap"/>, so neither its branches nor
    /// its memory accesses depend on the scalar. Non-canonical u-coordinates (2^255 - 19 to 2^255 - 1) are accepted and reduced modulo p,
    /// as RFC 7748 §5 requires.
    /// </remarks>
    internal static void ScalarMultiply(ReadOnlySpan<byte> scalar, ReadOnlySpan<byte> uCoordinate, Span<byte> destination)
    {
        Require(scalar.Length == 32, "The scalar must be exactly 32 bytes.", nameof(scalar));
        Require(uCoordinate.Length == 32, "The u-coordinate must be exactly 32 bytes.", nameof(uCoordinate));
        Require(destination.Length == 32, "The destination must be exactly 32 bytes.", nameof(destination));

        Span<byte> clamped = stackalloc byte[PrivateKeySizeInBytes];

        try
        {
            scalar.CopyTo(clamped);
            Clamp(clamped);

            Ed25519FieldElement x1 = Ed25519FieldElement.FromBytes(uCoordinate);
            Ed25519FieldElement x2 = Ed25519FieldElement.One;
            Ed25519FieldElement z2 = Ed25519FieldElement.Zero;
            Ed25519FieldElement x3 = x1;
            Ed25519FieldElement z3 = Ed25519FieldElement.One;
            ulong swap = 0;

            for (int t = ScalarBits - 1; t >= 0; t--)
            {
                ulong bit = (ulong)((clamped[t >> 3] >> (t & 7)) & 1);
                swap ^= bit;
                Ed25519FieldElement.ConditionalSwap(ref x2, ref x3, swap);
                Ed25519FieldElement.ConditionalSwap(ref z2, ref z3, swap);
                swap = bit;

                Ed25519FieldElement a = x2 + z2;
                Ed25519FieldElement aa = Ed25519FieldElement.Square(a);
                Ed25519FieldElement b = x2 - z2;
                Ed25519FieldElement bb = Ed25519FieldElement.Square(b);
                Ed25519FieldElement e = aa - bb;
                Ed25519FieldElement c = x3 + z3;
                Ed25519FieldElement d = x3 - z3;
                Ed25519FieldElement da = d * a;
                Ed25519FieldElement cb = c * b;

                x3 = Ed25519FieldElement.Square(da + cb);
                z3 = x1 * Ed25519FieldElement.Square(da - cb);
                x2 = aa * bb;
                z2 = e * (aa + Ed25519FieldElement.MultiplyA24(e));
            }

            Ed25519FieldElement.ConditionalSwap(ref x2, ref x3, swap);
            Ed25519FieldElement.ConditionalSwap(ref z2, ref z3, swap);

            Ed25519FieldElement result = x2 * Ed25519FieldElement.Invert(z2);
            result.WriteBytes(destination);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(clamped);
        }
    }

    /// <summary>
    /// Clamps an X25519 scalar per RFC 7748 §5: clears bits 0, 1, 2 and 255, and sets bit 254.
    /// </summary>
    /// <param name="scalar">The 32-byte scalar to clamp in place.</param>
    private static void Clamp(Span<byte> scalar)
    {
        scalar[0] &= 248;
        scalar[31] &= 127;
        scalar[31] |= 64;
    }

    /// <summary>
    /// Determines whether every byte of the specified value is zero, in time that does not depend on which byte, if any, is not.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>Returns <see langword="true"/> if every byte is zero; otherwise, <see langword="false"/>.</returns>
    private static bool IsAllZero(ReadOnlySpan<byte> value)
    {
        int accumulator = 0;
        foreach (byte item in value) accumulator |= item;
        return accumulator == 0;
    }

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
    /// Gets the private key of the current managed instance.
    /// </summary>
    /// <returns>Returns the private key.</returns>
    /// <exception cref="CryptographicException">Thrown when the current instance holds a public key only.</exception>
    private byte[] RequirePrivateKey() => privateKey ?? throw new CryptographicException(
        "Object contains only the public half of a key pair. A private key must also be provided.");
}
