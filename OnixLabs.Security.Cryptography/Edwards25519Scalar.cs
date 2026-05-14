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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Scalar arithmetic modulo L, the order of the Ed25519 prime-order base-point subgroup
/// (L = 2^252 + 27742317777372353535851937790883648493, RFC 8032 §5.1).
/// </summary>
/// <remarks>
/// The Phase 1 reference implementation uses <see cref="BigInteger"/> for arithmetic, which is
/// neither constant-time nor allocation-free. This is acceptable for the reference phase; the
/// roadmap (docs/eddsa.md Phase 5) calls for replacing this with constant-time ulong-limb code.
/// </remarks>
internal static class Edwards25519Scalar
{
    private static readonly BigInteger L = (BigInteger.One << 252)
        + BigInteger.Parse("27742317777372353535851937790883648493");

    /// <summary>
    /// Reduces a 64-byte little-endian integer modulo L and writes the result as 32 little-endian bytes.
    /// </summary>
    public static void ReduceFromWideBytes(ReadOnlySpan<byte> wide, Span<byte> output)
    {
        BigInteger value = new(wide, isUnsigned: true, isBigEndian: false);
        WriteLittleEndian32(value % L, output);
    }

    /// <summary>
    /// Computes (a * b + c) mod L, where all inputs and the output are 32-byte little-endian scalars.
    /// </summary>
    public static void MulAdd(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b, ReadOnlySpan<byte> c, Span<byte> output)
    {
        BigInteger aB = new(a, isUnsigned: true, isBigEndian: false);
        BigInteger bB = new(b, isUnsigned: true, isBigEndian: false);
        BigInteger cB = new(c, isUnsigned: true, isBigEndian: false);
        WriteLittleEndian32((aB * bB + cB) % L, output);
    }

    /// <summary>
    /// Returns true if the 32-byte little-endian scalar is strictly less than L.
    /// RFC 8032 §5.1.7 requires this check on the S component of a signature; signatures with
    /// S ≥ L are malleable and must be rejected.
    /// </summary>
    public static bool IsCanonical(ReadOnlySpan<byte> scalar)
    {
        BigInteger value = new(scalar, isUnsigned: true, isBigEndian: false);
        return value < L;
    }

    private static void WriteLittleEndian32(BigInteger value, Span<byte> output)
    {
        output.Clear();
        value.TryWriteBytes(output, out _, isUnsigned: true, isBigEndian: false);
    }
}
