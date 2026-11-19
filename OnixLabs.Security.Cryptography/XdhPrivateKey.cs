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
/// Represents an X25519 Diffie-Hellman cryptographic private key, as specified in RFC 7748.
/// </summary>
/// <remarks>
/// Why: the name follows the library's habit of naming key types by family (ECDH, ECDSA, EdDSA); RFC 7748 calls the X25519 and X448
/// Diffie-Hellman family XDH. Only X25519 is supported.
/// </remarks>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class XdhPrivateKey : PrivateKey, IXdhPrivateKey, ISpanParsable<XdhPrivateKey>
{
    /// <summary>
    /// The public key derived from the current private key, or <see langword="null"/> until it is first needed.
    /// </summary>
    /// <remarks>
    /// Why: deriving the public key is a full X25519 scalar multiplication, as costly as a key agreement. It is not secret, the private key
    /// it derives from never changes, and it is only ever derived from this key's own private key, never accepted from a caller.
    /// </remarks>
    private byte[]? publicKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic private key.</param>
    public XdhPrivateKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic private key.</param>
    public XdhPrivateKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XdhPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the X25519 cryptographic private key.</param>
    public XdhPrivateKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }
}
