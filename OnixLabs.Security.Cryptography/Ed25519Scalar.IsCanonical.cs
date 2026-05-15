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

namespace OnixLabs.Security.Cryptography;

internal static partial class Ed25519Scalar
{
    /// <summary>
    /// Determines whether the specified 32-byte little-endian scalar is strictly less than L.
    /// RFC 8032 §5.1.7 requires this check on the S component of a signature; signatures with
    /// S ≥ L are malleable and must be rejected.
    /// </summary>
    /// <remarks>
    /// Constant-time: scalar - L is computed byte-by-byte, and the final borrow tells us whether
    /// scalar &lt; L. No early return on any per-byte comparison.
    /// </remarks>
    /// <param name="scalar">The 32-byte little-endian scalar to test.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="scalar"/> is exactly 32 bytes and strictly less than L; otherwise, <see langword="false"/>.</returns>
    public static bool IsCanonical(ReadOnlySpan<byte> scalar)
    {
        if (scalar.Length != 32) return false;

        // L = 2^252 + 27742317777372353535851937790883648493, little-endian.
        ReadOnlySpan<byte> ell =
        [
            0xed, 0xd3, 0xf5, 0x5c, 0x1a, 0x63, 0x12, 0x58,
            0xd6, 0x9c, 0xf7, 0xa2, 0xde, 0xf9, 0xde, 0x14,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10,
        ];

        int borrow = 0;

        for (int i = 0; i < 32; i++)
        {
            int diff = scalar[i] - ell[i] - borrow;
            borrow = (diff >> 31) & 1;
        }

        return borrow == 1;
    }
}
