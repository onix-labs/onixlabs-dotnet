// Copyright © 2020 ONIXLabs
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
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public abstract partial class PublicKey
{
    /// <summary>
    /// Returns a <see cref="byte"/> array that represents the underlying public key data.
    /// </summary>
    /// <returns>Returns a <see cref="byte"/> array that represents the underlying public key data.</returns>
    public byte[] ToByteArray()
    {
        return KeyData.Copy();
    }

    /// <summary>
    /// Returns a <see cref="Base16"/> value that represents the underlying public key data.
    /// </summary>
    /// <returns>Returns a <see cref="Base16"/> value that represents the underlying public key data.</returns>
    public Base16 ToBase16()
    {
        return new Base16(KeyData);
    }

    /// <summary>
    /// Returns a <see cref="Base32"/> value that represents the underlying public key data.
    /// </summary>
    /// <returns>Returns a <see cref="Base32"/> value that represents the underlying public key data.</returns>
    public Base32 ToBase32()
    {
        return new Base32(KeyData);
    }

    /// <summary>
    /// Returns a <see cref="Base58"/> value that represents the underlying public key data.
    /// </summary>
    /// <returns>Returns a <see cref="Base58"/> value that represents the underlying public key data.</returns>
    public Base58 ToBase58()
    {
        return new Base58(KeyData);
    }

    /// <summary>
    /// Returns a <see cref="Base64"/> value that represents the underlying public key data.
    /// </summary>
    /// <returns>Returns a <see cref="Base64"/> value that represents the underlying public key data.</returns>
    public Base64 ToBase64()
    {
        return new Base64(KeyData);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        return Convert.ToHexString(KeyData).ToLower();
    }
}
