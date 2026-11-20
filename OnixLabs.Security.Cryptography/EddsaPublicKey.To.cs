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

public sealed partial class EddsaPublicKey
{
    /// <summary>
    /// The algorithm identifier used when wrapping this key as a <see cref="NamedPublicKey"/>.
    /// </summary>
    private const string KeyName = "EDDSA";

    /// <inheritdoc/>
    public override NamedPublicKey ToNamedPublicKey() => new(this, KeyName);

    /// <summary>
    /// Converts the current Ed25519 public key to the X25519 public key of the same key pair, by the birational map of RFC 7748 §4.1.
    /// </summary>
    /// <returns>Returns the X25519 public key.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the current key is not a valid Ed25519 public key, or is one of the points of small order, including the identity.
    /// </exception>
    public XdhPublicKey ToXdhPublicKey()
    {
        Span<byte> destination = stackalloc byte[X25519.PublicKeySizeInBytes];

        return Ed25519Point.TryConvertToMontgomery(KeyData, destination)
            ? new XdhPublicKey(destination)
            : throw new CryptographicException("The Ed25519 public key is not a valid point on the curve, or is a point of small order.");
    }
}
