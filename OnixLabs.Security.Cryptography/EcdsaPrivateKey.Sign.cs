// Copyright 2020-2024 ONIXLabs
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
using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by hashing and signing the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to be hashed and signed.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the hashed and signed data.</returns>
    public DigitalSignature SignData(ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        SignHash(algorithm.ComputeHash(data.ToArray()), format);

    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by hashing and signing the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to be hashed and signed.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the hashed and signed data.</returns>
    public DigitalSignature SignData(ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        SignHash(algorithm.ComputeHash(data.ToArray(), offset, count), format);

    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by hashing and signing the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to be hashed and signed.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the hashed and signed data.</returns>
    public DigitalSignature SignData(IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        SignHash(algorithm.ComputeHash(data.ToByteArray()), format);

    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by hashing and signing the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to be hashed and signed.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the hashed and signed data.</returns>
    public DigitalSignature SignData(Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        SignHash(algorithm.ComputeHash(data), format);

    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by signing the specified <see cref="Hash"/>.
    /// </summary>
    /// <param name="hash">The hash to be signed.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the signed hash.</returns>
    public DigitalSignature SignHash(Hash hash, DSASignatureFormat format = default) =>
        SignHash(hash.ToByteArray(), format);

    /// <summary>
    /// Computes a cryptographic <see cref="DigitalSignature"/> by signing the specified <see cref="Hash"/>.
    /// </summary>
    /// <param name="hash">The hash to be signed.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>Returns a cryptographic <see cref="DigitalSignature"/> of the signed hash.</returns>
    public DigitalSignature SignHash(ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        using ECDsa algorithm = ImportPrivateKey();
        return new DigitalSignature(algorithm.SignHash(hash, format));
    }
}
