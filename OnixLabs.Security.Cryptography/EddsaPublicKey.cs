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
using System.Buffers;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an EdDSA cryptographic public key, as specified in RFC 8032 (Ed25519, PureEdDSA).
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class EddsaPublicKey : PublicKey, IEddsaPublicKey, ISpanParsable<EddsaPublicKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic public key.</param>
    public EddsaPublicKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic public key.</param>
    public EddsaPublicKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic public key.</param>
    public EddsaPublicKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }
}
