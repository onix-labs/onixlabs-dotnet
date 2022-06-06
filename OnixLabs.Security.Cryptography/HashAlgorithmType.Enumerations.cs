// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Security.Cryptography;

public sealed partial class HashAlgorithmType
{
    /// <summary>
    /// An unknown hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Unknown = new(0, nameof(Unknown), UnknownLength, false);

    /// <summary>
    /// The MD5 hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Md5Hash = new(1, nameof(Md5Hash), 16, false);

    /// <summary>
    /// The SHA-1 hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha1Hash = new(2, nameof(Sha1Hash), 20, false);

    /// <summary>
    /// The SHA-2 256-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hash256 = new(3, nameof(Sha2Hash256), 32, false);

    /// <summary>
    /// The SHA-2 384-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hash384 = new(4, nameof(Sha2Hash384), 48, false);

    /// <summary>
    /// The SHA-2 512-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hash512 = new(5, nameof(Sha2Hash512), 64, false);

    /// <summary>
    /// The SHA-3 224-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Hash224 = new(6, nameof(Sha3Hash224), 28, false);

    /// <summary>
    /// The SHA-3 256-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Hash256 = new(7, nameof(Sha3Hash256), 32, false);

    /// <summary>
    /// The SHA-3 384-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Hash384 = new(8, nameof(Sha3Hash384), 48, false);

    /// <summary>
    /// The SHA-3 512-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Hash512 = new(9, nameof(Sha3Hash512), 64, false);

    /// <summary>
    /// The SHA-3 Shake 128-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Shake128 = new(10, nameof(Sha3Shake128), UnknownLength, false);

    /// <summary>
    /// The SHA-3 Shake 256-bit hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha3Shake256 = new(11, nameof(Sha3Shake256), UnknownLength, false);

    /// <summary>
    /// The MD5 HMAC keyed hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Md5Hmac = new(12, nameof(Md5Hmac), 16, true);

    /// <summary>
    /// The SHA-1 HMAC keyed hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha1Hmac = new(13, nameof(Sha1Hmac), 20, true);

    /// <summary>
    /// The SHA-2 256-bit HMAC keyed hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hmac256 = new(14, nameof(Sha2Hmac256), 32, true);

    /// <summary>
    /// The SHA-2 384-bit HMAC keyed hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hmac384 = new(15, nameof(Sha2Hmac384), 48, true);

    /// <summary>
    /// The SHA-2 512-bit HMAC keyed hash algorithm.
    /// </summary>
    public static readonly HashAlgorithmType Sha2Hmac512 = new(16, nameof(Sha2Hmac512), 64, true);
}
