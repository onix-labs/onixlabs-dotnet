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
/// Represents an EdDSA cryptographic private key, as specified in RFC 8032 (Ed25519, PureEdDSA).
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class EddsaPrivateKey : PrivateKey, IEddsaPrivateKey, ISpanParsable<EddsaPrivateKey>
{
    /// <summary>
    /// The encoded public key derived from the current private key, or <see langword="null"/> until it is first needed.
    /// </summary>
    /// <remarks>
    /// Why: deriving the public key is a full scalar multiplication, and signing needs it for every signature. It is not secret, the seed
    /// it derives from never changes, and it is only ever derived from this key's own seed, never accepted from a caller.
    /// </remarks>
    private byte[]? publicKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic private key.</param>
    public EddsaPrivateKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic private key.</param>
    public EddsaPrivateKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EddsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the EdDSA cryptographic private key.</param>
    public EddsaPrivateKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }
}
