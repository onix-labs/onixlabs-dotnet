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
/// Defines an elliptic curve public key that can be imported from a SEC 1 compressed point.
/// </summary>
/// <typeparam name="T">The underlying type of <see cref="PublicKey"/> that the import functions will return.</typeparam>
public interface IPublicKeyCompressedImportable<out T> where T : PublicKey
{
    /// <summary>
    /// Imports a public key from a SEC 1 v2 §2.3.4 compressed point, determining the curve from its length:
    /// 33 bytes for P-256, 49 bytes for P-384, and 67 bytes for P-521.
    /// </summary>
    /// <param name="data">The compressed point to import.</param>
    /// <returns>Returns a new cryptographic public key from the imported point.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when <paramref name="data"/> has an unsupported length or prefix, an x-coordinate that is not less than the field prime,
    /// or an x-coordinate that is not on the curve.
    /// </exception>
    public static abstract T ImportCompressed(ReadOnlySpan<byte> data);

    /// <summary>
    /// Imports a public key from a SEC 1 v2 §2.3.4 compressed point, which must be on the specified curve.
    /// </summary>
    /// <param name="data">The compressed point to import.</param>
    /// <param name="curve">The named curve that the point must be on, which must be P-256, P-384 or P-521.</param>
    /// <returns>Returns a new cryptographic public key from the imported point.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when <paramref name="data"/> is not a valid compressed point, or is not on <paramref name="curve"/>.
    /// </exception>
    public static abstract T ImportCompressed(ReadOnlySpan<byte> data, ECCurve curve);
}
