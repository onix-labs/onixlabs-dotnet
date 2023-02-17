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

using System;
using System.Security.Cryptography;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Security.Cryptography;

public sealed partial class HashAlgorithmType
{
    /// <summary>
    /// Gets the hash algorithm for this <see cref="HashAlgorithmType"/> instance.
    /// </summary>
    /// <returns>Returns a <see cref="HashAlgorithm"/> for this <see cref="HashAlgorithmType"/>.</returns>
    public HashAlgorithm GetHashAlgorithm()
    {
        return GetHashAlgorithm(Length);
    }

    /// <summary>
    /// Gets the hash algorithm for this <see cref="HashAlgorithmType"/> instance.
    /// </summary>
    /// <param name="length">Specifies the expected output hash length.</param>
    /// <returns>Returns a <see cref="HashAlgorithm"/> for this <see cref="HashAlgorithmType"/>.</returns>
    /// <exception cref="ArgumentException">If the hash algorithm does not expect an output length.</exception>
    /// <exception cref="ArgumentException">If the hash algorithm is unknown.</exception>
    public HashAlgorithm GetHashAlgorithm(int length)
    {
        Require(IsUnknown || length == Length, $"Output length not expected for the specified hash algorithm: {Name}", nameof(length));

        return Name switch
        {
            nameof(Md5Hash) => MD5.Create(),
            nameof(Sha1Hash) => SHA1.Create(),
            nameof(Sha2Hash256) => SHA256.Create(),
            nameof(Sha2Hash384) => SHA384.Create(),
            nameof(Sha2Hash512) => SHA512.Create(),
            nameof(Sha3Hash224) => Sha3.CreateSha3Hash224(),
            nameof(Sha3Hash256) => Sha3.CreateSha3Hash256(),
            nameof(Sha3Hash384) => Sha3.CreateSha3Hash384(),
            nameof(Sha3Hash512) => Sha3.CreateSha3Hash512(),
            nameof(Sha3Shake128) => Sha3.CreateSha3Shake128(length),
            nameof(Sha3Shake256) => Sha3.CreateSha3Shake256(length),
            _ => throw new ArgumentException($"Hash algorithm '{Name}' is unknown.")
        };
    }

    /// <summary>
    /// Gets the keyed hash algorithm for this <see cref="HashAlgorithmType"/> instance.
    /// </summary>
    /// <param name="key">The key that should be used by the keyed hash algorithm.</param>
    /// <returns>Returns a <see cref="KeyedHashAlgorithm"/> for this <see cref="HashAlgorithmType"/>.</returns>
    /// <exception cref="ArgumentException">If the hash algorithm is unknown or is not a keyed hash algorithm.</exception>
    public KeyedHashAlgorithm GetKeyedHashAlgorithm(byte[]? key = null)
    {
        Check(IsKeyBased, $"Hash algorithm type '{Name}' is not a keyed hash algorithm.");

        return Name switch
        {
            nameof(Md5Hmac) => key is null ? new HMACMD5() : new HMACMD5(key),
            nameof(Sha1Hmac) => key is null ? new HMACSHA1() : new HMACSHA1(key),
            nameof(Sha2Hmac256) => key is null ? new HMACSHA256() : new HMACSHA256(key),
            nameof(Sha2Hmac384) => key is null ? new HMACSHA384() : new HMACSHA384(key),
            nameof(Sha2Hmac512) => key is null ? new HMACSHA512() : new HMACSHA512(key),
            _ => throw new ArgumentException($"Hash algorithm '{Name}' is unknown.")
        };
    }

    /// <summary>
    /// Gets the <see cref="HashAlgorithmName"/> equivalent for this <see cref="HashAlgorithmType"/>.
    /// </summary>
    /// <returns>Returns the <see cref="HashAlgorithmName"/> equivalent for this <see cref="HashAlgorithmType"/>.</returns>
    /// <exception cref="ArgumentException">If there is no corresponding <see cref="HashAlgorithmName"/> equivalent for this <see cref="HashAlgorithmType"/>.</exception>
    public HashAlgorithmName GetHashAlgorithmName()
    {
        return Name switch
        {
            nameof(Md5Hash) => HashAlgorithmName.MD5,
            nameof(Sha1Hash) => HashAlgorithmName.SHA1,
            nameof(Sha2Hash256) => HashAlgorithmName.SHA256,
            nameof(Sha2Hash384) => HashAlgorithmName.SHA384,
            nameof(Sha2Hash512) => HashAlgorithmName.SHA512,
            _ => throw new ArgumentException($"No corresponding {nameof(HashAlgorithmName)} for '{Name}'.")
        };
    }
}
