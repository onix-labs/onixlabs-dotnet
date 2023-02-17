// Copyright 2020-2023 ONIXLabs
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

using System.Security.Cryptography;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Creates an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.
    /// </summary>
    /// <param name="key">The key data from which to construct a private key.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.</returns>
    public static RsaPrivateKey FromByteArray(byte[] key, HashAlgorithmType type, RSASignaturePadding padding)
    {
        return new RsaPrivateKey(key, type, padding);
    }

    /// <summary>
    /// Creates an <see cref="RsaPrivateKey"/> from the specified <see cref="Base16"/> value.
    /// </summary>
    /// <param name="key">The key data from which to construct a private key.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.</returns>
    public static RsaPrivateKey FromBase16(Base16 key, HashAlgorithmType type, RSASignaturePadding padding)
    {
        byte[] bytes = key.ToByteArray();
        return FromByteArray(bytes, type, padding);
    }

    /// <summary>
    /// Creates an <see cref="RsaPrivateKey"/> from the specified <see cref="Base32"/> value.
    /// </summary>
    /// <param name="key">The key data from which to construct a private key.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.</returns>
    public static RsaPrivateKey FromBase32(Base32 key, HashAlgorithmType type, RSASignaturePadding padding)
    {
        byte[] bytes = key.ToByteArray();
        return FromByteArray(bytes, type, padding);
    }

    /// <summary>
    /// Creates an <see cref="RsaPrivateKey"/> from the specified <see cref="Base58"/> value.
    /// </summary>
    /// <param name="key">The key data from which to construct a private key.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.</returns>
    public static RsaPrivateKey FromBase58(Base58 key, HashAlgorithmType type, RSASignaturePadding padding)
    {
        byte[] bytes = key.ToByteArray();
        return FromByteArray(bytes, type, padding);
    }

    /// <summary>
    /// Creates an <see cref="RsaPrivateKey"/> from the specified <see cref="Base58"/> value.
    /// </summary>
    /// <param name="key">The key data from which to construct a private key.</param>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an <see cref="RsaPrivateKey"/> from the specified key data and hash algorithm type.</returns>
    public static RsaPrivateKey FromBase64(Base64 key, HashAlgorithmType type, RSASignaturePadding padding)
    {
        byte[] bytes = key.ToByteArray();
        return FromByteArray(bytes, type, padding);
    }
}
