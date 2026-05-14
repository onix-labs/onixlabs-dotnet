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

// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed partial class EddsaPrivateKey
{
    /// <summary>
    /// Creates a new EdDSA cryptographic private key from a randomly generated seed.
    /// </summary>
    /// <returns>Returns a new <see cref="EddsaPrivateKey"/> instance.</returns>
    public static EddsaPrivateKey Create()
    {
        Span<byte> seed = stackalloc byte[Ed25519.SeedLength];
        try
        {
            RandomNumberGenerator.Fill(seed);
            return new EddsaPrivateKey(seed);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }
}
