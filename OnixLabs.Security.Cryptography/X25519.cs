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
/// Represents the X25519 Diffie-Hellman function of RFC 7748 §5, over the same field GF(2^255 - 19) as Ed25519.
/// </summary>
internal static class X25519
{
    /// <summary>
    /// The length in bytes of an X25519 private key, public key, and shared secret.
    /// </summary>
    public const int KeyLength = 32;

    /// <summary>
    /// The number of scalar bits the Montgomery ladder processes. Clamping clears bit 255, so the ladder starts at bit 254.
    /// </summary>
    private const int ScalarBits = 255;

    /// <summary>
    /// Derives the 32-byte X25519 public key from a 32-byte private key, computing X25519(k, 9).
    /// </summary>
    /// <param name="privateKey">The 32-byte X25519 private key.</param>
    /// <param name="publicKey">The 32-byte destination buffer that receives the derived public key.</param>
    public static void DerivePublicKey(ReadOnlySpan<byte> privateKey, Span<byte> publicKey)
    {
        Span<byte> basePoint = stackalloc byte[KeyLength];
        basePoint.Clear();
        basePoint[0] = 9;

        ScalarMultiply(privateKey, basePoint, publicKey);
    }

    /// <summary>
    /// Derives the 32-byte X25519 shared secret from a private key and the other party's public key, computing X25519(k, u).
    /// </summary>
    /// <param name="privateKey">The 32-byte X25519 private key.</param>
    /// <param name="publicKey">The other party's 32-byte X25519 public key.</param>
    /// <param name="sharedSecret">The 32-byte destination buffer that receives the shared secret.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the shared secret is not all zeros; otherwise, <see langword="false"/>, in which case
    /// <paramref name="sharedSecret"/> is zeroed.
    /// </returns>
    /// <remarks>
    /// Why: an all-zero output means the public key is a point of small order, so the result is independent of the private key.
    /// RFC 7748 §6.1 permits rejecting it, and RFC 8037 §3.2 requires JOSE implementations to. The check ORs every byte together,
    /// so its timing does not depend on which byte, if any, is non-zero.
    /// </remarks>
    public static bool TryDeriveSharedSecret(ReadOnlySpan<byte> privateKey, ReadOnlySpan<byte> publicKey, Span<byte> sharedSecret)
    {
        ScalarMultiply(privateKey, publicKey, sharedSecret);

        int accumulator = 0;
        foreach (byte value in sharedSecret[..KeyLength]) accumulator |= value;

        if (accumulator != 0) return true;

        CryptographicOperations.ZeroMemory(sharedSecret);
        return false;
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
    public static void ScalarMultiply(ReadOnlySpan<byte> scalar, ReadOnlySpan<byte> uCoordinate, Span<byte> destination)
    {
        Require(scalar.Length == KeyLength, $"The scalar must be exactly {KeyLength} bytes.", nameof(scalar));
        Require(uCoordinate.Length == KeyLength, $"The u-coordinate must be exactly {KeyLength} bytes.", nameof(uCoordinate));
        Require(destination.Length == KeyLength, $"The destination must be exactly {KeyLength} bytes.", nameof(destination));

        Span<byte> clamped = stackalloc byte[KeyLength];

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
}
