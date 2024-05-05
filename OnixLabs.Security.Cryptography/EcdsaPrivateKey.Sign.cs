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
using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdsaPrivateKey
{
    /// <summary>
    /// Hashes the specified <see cref="ReadOnlySpan{T}"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray());
        return SignHash(hash, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="ReadOnlySpan{T}"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray(), offset, count);
        return SignHash(hash, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="Stream"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return SignHash(hash, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="IBinaryConvertible"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToByteArray());
        return SignHash(hash, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="ReadOnlySpan{T}"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data, algorithm, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="ReadOnlySpan{T}"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data.ToArray(), offset, count, algorithm, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="Stream"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data, algorithm, format);
    }

    /// <summary>
    /// Hashes the specified <see cref="IBinaryConvertible"/> data and signs the resulting hash.
    /// </summary>
    /// <param name="data">The input data to hash and sign.</param>
    /// <param name="algorithm">The hash algorithm that will be used to hash the input data.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data.ToByteArray(), algorithm, format);
    }

    /// <summary>
    /// Signs the specified <see cref="Hash"/>.
    /// </summary>
    /// <param name="hash">The hash to sign.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignHash(Hash hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignHash(hash.ToByteArray(), format);
    }

    /// <summary>
    /// Signs the specified <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <param name="hash">The hash to sign.</param>
    /// <param name="format">The digital signature format which will be used to generate the cryptographic digital signature.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic digital signature.</returns>
    public byte[] SignHash(ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignHash(hash, format);
    }
}
