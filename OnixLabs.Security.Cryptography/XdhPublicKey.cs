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
/// Represents an X25519 Diffie-Hellman cryptographic public key, as specified in RFC 7748.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class XdhPublicKey : PublicKey, IXdhPublicKey, ISpanParsable<XdhPublicKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic public key.</param>
    public XdhPublicKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic public key.</param>
    public XdhPublicKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic public key.</param>
    public XdhPublicKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }
}
