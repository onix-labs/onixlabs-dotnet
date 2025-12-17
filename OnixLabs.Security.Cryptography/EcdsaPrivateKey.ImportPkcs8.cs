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

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EcdsaPrivateKey
{
    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data) =>
        ImportPkcs8(data, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), out bytesRead);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<char> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(IBinaryConvertible data, ReadOnlySpan<byte> password, out int bytesRead) =>
        ImportPkcs8(data.AsReadOnlySpan(), password, out bytesRead);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data) =>
        ImportPkcs8(data, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password) =>
        ImportPkcs8(data, password, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password) =>
        ImportPkcs8(data, password, out int _);

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, out int bytesRead)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportPkcs8PrivateKey(data, out bytesRead);
        return new EcdsaPrivateKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<char> password, out int bytesRead)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportEncryptedPkcs8PrivateKey(password, data, out bytesRead);
        return new EcdsaPrivateKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdsaPrivateKey ImportPkcs8(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password, out int bytesRead)
    {
        using ECDsa algorithm = ECDsa.Create();
        algorithm.ImportEncryptedPkcs8PrivateKey(password, data, out bytesRead);
        return new EcdsaPrivateKey(algorithm);
    }
}
