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
    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray());
        return SignHash(hash, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray(), offset, count);
        return SignHash(hash, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return SignHash(hash, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.AsReadOnlySpan());
        return SignHash(hash, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data, algorithm, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data.ToArray(), offset, count, algorithm, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data, algorithm, format);
    }

    /// <inheritdoc/>
    public byte[] SignData(IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignData(data.AsReadOnlySpan(), algorithm, format);
    }

    /// <inheritdoc/>
    public byte[] SignHash(Hash hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignHash(hash.AsReadOnlySpan(), format);
    }

    /// <inheritdoc/>
    public byte[] SignHash(ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.SignHash(hash, format);
    }
}
