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
/// Represents an RSA cryptographic public key.
/// </summary>
/// <param name="keyData">The underlying key data of the RSA cryptographic public key.</param>
public sealed partial class RsaPublicKey(ReadOnlySpan<byte> keyData) : PublicKey(keyData), IRsaPublicKey, ISpanParsable<RsaPublicKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RsaPublicKey"/> struct.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySequence{T}"/> with which to initialize the <see cref="RsaPublicKey"/> instance.</param>
    // ReSharper disable once MemberCanBePrivate.Global
    public RsaPublicKey(ReadOnlySequence<byte> value) : this(value.ToArray())
    {
    }
}
