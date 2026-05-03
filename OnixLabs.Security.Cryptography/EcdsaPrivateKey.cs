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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an ECDSA cryptographic private key.
/// </summary>
// ReSharper disable MemberCanBePrivate.Global
public sealed partial class EcdsaPrivateKey : PrivateKey, IEcdsaPrivateKey, ISpanParsable<EcdsaPrivateKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic private key.</param>
    public EcdsaPrivateKey(ReadOnlySpan<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic private key.</param>
    public EcdsaPrivateKey(ReadOnlyMemory<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPrivateKey"/> class.
    /// </summary>
    /// <param name="keyData">The underlying key data of the ECDSA cryptographic private key.</param>
    public EcdsaPrivateKey(ReadOnlySequence<byte> keyData) : base(keyData)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcdsaPrivateKey"/> class.
    /// </summary>
    /// <param name="algorithm">The <see cref="ECDsa"/> algorithm with which to initialize the <see cref="EcdsaPrivateKey"/> instance.</param>
    private EcdsaPrivateKey(ECDsa algorithm) : this(algorithm.ExportECPrivateKey())
    {
    }
}
