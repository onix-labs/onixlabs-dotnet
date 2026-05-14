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
using System.IO;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines an EdDSA cryptographic private key, as specified in RFC 8032 (Ed25519, PureEdDSA).
/// </summary>
public interface IEddsaPrivateKey :
    IPrivateKeyDerivable<EddsaPublicKey>,
    IPrivateKeyImportable<EddsaPrivateKey>,
    IPrivateKeyExportable,
    IBinaryConvertible
{
    /// <summary>
    /// Signs the specified <see cref="ReadOnlySpan{T}"/> data.
    /// </summary>
    /// <param name="data">The input data to sign.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic digital signature.</returns>
    byte[] SignData(ReadOnlySpan<byte> data);

    /// <summary>
    /// Signs the specified <see cref="ReadOnlySpan{T}"/> data.
    /// </summary>
    /// <param name="data">The input data to sign.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic digital signature.</returns>
    byte[] SignData(ReadOnlySpan<byte> data, int offset, int count);

    /// <summary>
    /// Signs the specified <see cref="Stream"/> data.
    /// </summary>
    /// <param name="data">The input data to sign.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic digital signature.</returns>
    byte[] SignData(Stream data);

    /// <summary>
    /// Signs the specified <see cref="IBinaryConvertible"/> data.
    /// </summary>
    /// <param name="data">The input data to sign.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic digital signature.</returns>
    byte[] SignData(IBinaryConvertible data);
}
