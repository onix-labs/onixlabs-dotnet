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
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPublicKey
{
    /// <inheritdoc/>
    public static EcdsaPublicKey Import(IBinaryConvertible data) =>
        Import(data.AsReadOnlySpan());

    /// <inheritdoc/>
    public static EcdsaPublicKey Import(IBinaryConvertible data, out int bytesRead) =>
        Import(data.AsReadOnlySpan(), out bytesRead);

    /// <inheritdoc/>
    public static EcdsaPublicKey Import(ReadOnlySpan<byte> data) =>
        Import(data, out int _);

    /// <inheritdoc/>
    /// <remarks>
    /// A secp256k1 SubjectPublicKeyInfo is decoded and validated by managed code on every platform, since not every platform supports the
    /// curve; the point may be compressed, and is stored uncompressed.
    /// </remarks>
    public static EcdsaPublicKey Import(ReadOnlySpan<byte> data, out int bytesRead)
    {
        if (Secp256k1.IsSecp256k1SubjectPublicKeyInfo(data))
        {
            return Secp256k1.TryDecodeSubjectPublicKeyInfo(data, out Secp256k1Point point, out bytesRead)
                ? new EcdsaPublicKey(Secp256k1.EncodeSubjectPublicKeyInfo(point))
                : throw new CryptographicException("The secp256k1 public key is not a valid point on the curve.");
        }

        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportSubjectPublicKeyInfo(data, out bytesRead);
        return new EcdsaPublicKey(algorithm);
    }

    /// <summary>
    /// Imports the key data into a new <see cref="ECDsa"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="ECDsa"/> instance containing the imported key data.</returns>
    /// <exception cref="CryptographicException">Thrown when the underlying key data does not represent a valid ECDSA public
    /// key.</exception>
    private ECDsa ImportKeyData()
    {
        ECDsa algorithm = ECDsa.Create();
        algorithm.ImportSubjectPublicKeyInfo(KeyData, out int _);
        return algorithm;
    }

    /// <summary>
    /// Attempts to decode the key data as a secp256k1 public key, which is verified by managed code rather than by the platform.
    /// </summary>
    /// <param name="point">When this method returns, contains the public point, if the key data is a secp256k1 public key.</param>
    /// <returns>Returns <see langword="true"/> if the key data names the secp256k1 curve; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="CryptographicException">Thrown when the key data names the secp256k1 curve, but is not a valid point on
    /// it.</exception>
    /// <remarks>
    /// Why: macOS supports only the NIST curves, so secp256k1 (JOSE's <c>ES256K</c>) is verified in managed code. It is used on every
    /// platform, so that a secp256k1 key behaves the same everywhere.
    /// </remarks>
    private bool TryGetSecp256k1PublicKey(out Secp256k1Point point)
    {
        object? cached = Volatile.Read(ref secp256k1PublicKey);

        if (cached is StrongBox<Secp256k1Point> box)
        {
            point = box.Value;
            return true;
        }

        point = Secp256k1Point.Infinity;

        if (cached == NotSecp256k1) return false;

        if (!Secp256k1.IsSecp256k1SubjectPublicKeyInfo(KeyData))
        {
            Volatile.Write(ref secp256k1PublicKey, NotSecp256k1);
            return false;
        }

        if (!Secp256k1.TryDecodeSubjectPublicKeyInfo(KeyData, out point, out int _))
            throw new CryptographicException("The secp256k1 public key is not a valid point on the curve.");

        Volatile.Write(ref secp256k1PublicKey, new StrongBox<Secp256k1Point>(point));
        return true;
    }
}
