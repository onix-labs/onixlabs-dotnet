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
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an ECDSA cryptographic public key.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class EcdsaPublicKey : PublicKey, IEcdsaPublicKey, ISpanParsable<EcdsaPublicKey>
{
    /// <summary>
    /// The marker stored in <see cref="secp256k1PublicKey"/> once the key data is known not to be a secp256k1 public key.
    /// </summary>
    private static readonly object NotSecp256k1 = new();

    /// <summary>
    /// The decoded secp256k1 point, as a <see cref="StrongBox{T}"/>; or <see cref="NotSecp256k1"/>; or <see langword="null"/> until the key
    /// data is first examined.
    /// </summary>
    /// <remarks>
    /// Why: decoding and validating the point would otherwise run on every verification. The key data never changes, and a reference is
    /// published atomically, so concurrent first calls at worst decode the same point twice.
    /// </remarks>
    private object? secp256k1PublicKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic public key.</param>
    public EcdsaPublicKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic public key.</param>
    public EcdsaPublicKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPublicKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic public key.</param>
    public EcdsaPublicKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPublicKey"/> class.
    /// </summary>
    /// <param name="algorithm">The <see cref="ECDsa"/> algorithm with which to initialize the <see cref="EcdsaPublicKey"/>
    /// instance.</param>
    private EcdsaPublicKey(ECDsa algorithm) : this(algorithm.ExportSubjectPublicKeyInfo())
    {
    }
}
