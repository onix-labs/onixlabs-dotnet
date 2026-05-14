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

public sealed partial class EddsaPrivateKey
{
    /// <inheritdoc/>
    public static EddsaPrivateKey Import(ReadOnlySpan<byte> data)
    {
        if (data.Length != Ed25519.SeedLength)
        {
            throw new CryptographicException($"Ed25519 private key must be exactly {Ed25519.SeedLength} bytes.");
        }
        return new EddsaPrivateKey(data);
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey Import(ReadOnlySpan<byte> data, out int bytesRead)
    {
        EddsaPrivateKey key = Import(data);
        bytesRead = Ed25519.SeedLength;
        return key;
    }

    /// <inheritdoc/>
    public static EddsaPrivateKey Import(IBinaryConvertible data) => Import(data.AsReadOnlySpan());

    /// <inheritdoc/>
    public static EddsaPrivateKey Import(IBinaryConvertible data, out int bytesRead) =>
        Import(data.AsReadOnlySpan(), out bytesRead);
}
