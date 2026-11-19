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
    /// <remarks>
    /// Why: macOS supports only the NIST curves, so a secp256k1 key (JOSE's <c>ES256K</c>) is imported into <see cref="Secp256k1"/>,
    /// which is an <see cref="ECDsa"/> that uses the platform's implementation where it supports the curve, and managed code elsewhere.
    /// </remarks>
    private ECDsa ImportKeyData()
    {
        ECDsa algorithm = Secp256k1.IsSecp256k1SubjectPublicKeyInfo(KeyData) ? new Secp256k1() : ECDsa.Create();

        try
        {
            algorithm.ImportSubjectPublicKeyInfo(KeyData, out int _);
            return algorithm;
        }
        catch
        {
            algorithm.Dispose();
            throw;
        }
    }
}
