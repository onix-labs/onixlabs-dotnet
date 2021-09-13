// Copyright 2020-2021 ONIXLabs
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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography
{
    /// <summary>
    /// Specifies values that define known hash algorithm types.
    /// </summary>
    public sealed class HashAlgorithmType : Enumeration<HashAlgorithmType>
    {
        /// <summary>
        /// A constant that defines an unknown hash algorithm length.
        /// </summary>
        public const int UnknownLength = -1;

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
        public static readonly HashAlgorithmType Md5HmacHash = new(12, nameof(Md5HmacHash), 16, true);

        /// <summary>
        /// The SHA-1 HMAC keyed hash algorithm.
        /// </summary>
        public static readonly HashAlgorithmType Sha1HmacHash = new(13, nameof(Sha1HmacHash), 20, true);

        /// <summary>
        /// The SHA-2 256-bit HMAC keyed hash algorithm.
        /// </summary>
        public static readonly HashAlgorithmType Sha2HmacHash256 = new(14, nameof(Sha2HmacHash256), 32, true);

        /// <summary>
        /// The SHA-2 384-bit HMAC keyed hash algorithm.
        /// </summary>
        public static readonly HashAlgorithmType Sha2HmacHash384 = new(15, nameof(Sha2HmacHash384), 48, true);

        /// <summary>
        /// The SHA-2 512-bit HMAC keyed hash algorithm.
        /// </summary>
        public static readonly HashAlgorithmType Sha2HmacHash512 = new(16, nameof(Sha2HmacHash512), 64, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmType"/> class.
        /// </summary>
        /// <param name="value">The value of the hash algorithm type.</param>
        /// <param name="name">The name of the hash algorithm type.</param>
        /// <param name="length">The length in bytes of the hash algorithm type.</param>
        /// <param name="keyed">Determines whether the algorithm is a keyed hash algorithm.</param>
        private HashAlgorithmType(int value, string name, int length, bool keyed) : base(value, name)
        {
            Length = length;
            Keyed = keyed;
        }

        /// <summary>
        /// Gets the length of an algorithm's hash in bytes.
        /// -1 Means that the algorithm's hash is of variable length, or is unknown.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets a value that determines whether the algorithm is a keyed hash algorithm.
        /// </summary>
        public bool Keyed { get; }

        /// <summary>
        /// Gets the hash algorithm for this <see cref="HashAlgorithmType"/> instance.
        /// </summary>
        /// <param name="length">Specifies the expected output hash length.</param>
        /// <returns>Returns a <see cref="HashAlgorithm"/> for this <see cref="HashAlgorithmType"/>.</returns>
        /// <exception cref="ArgumentException">If the hash algorithm does not expect an output length.</exception>
        /// <exception cref="ArgumentException">If the hash algorithm is unknown.</exception>
        public HashAlgorithm GetHashAlgorithm(int length = UnknownLength)
        {
            if (length != UnknownLength && Length > UnknownLength)
            {
                throw new ArgumentException($"Output length not expected for the specified hash algorithm: {Name}");
            }

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
        /// <exception cref="ArgumentException">If the hash algorithm is unknown.</exception>
        public KeyedHashAlgorithm GetKeyedHashAlgorithm(byte[]? key = null)
        {
            return Name switch
            {
                nameof(Md5HmacHash) => key is null ? new HMACMD5() : new HMACMD5(key),
                nameof(Sha1HmacHash) => key is null ? new HMACSHA1() : new HMACSHA1(key),
                nameof(Sha2HmacHash256) => key is null ? new HMACSHA256() : new HMACSHA256(key),
                nameof(Sha2HmacHash384) => key is null ? new HMACSHA384() : new HMACSHA384(key),
                nameof(Sha2HmacHash512) => key is null ? new HMACSHA512() : new HMACSHA512(key),
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

        /// <summary>
        /// Determines whether the length of a byte array is valid.
        /// </summary>
        /// <param name="value">The byte array to length check.</param>
        /// <returns>Returns true if the length of the byte array is valid; otherwise, false.</returns>
        public bool IsValidHashLength(byte[] value)
        {
            return Length == -1 || Length == value.Length;
        }

        /// <summary>
        /// Verifies that the length of a byte array is valid.
        /// </summary>
        /// <param name="value">The byte array to length check.</param>
        /// <exception cref="ArgumentException">If the length of the byte array is invalid.</exception>
        public void VerifyHashLength(byte[] value)
        {
            if (!IsValidHashLength(value))
            {
                throw new ArgumentException("The length of the hash is invalid.", nameof(value));
            }
        }

        /// <summary>
        /// Checks for equality between this <see cref="HashAlgorithmType"/> and another <see cref="HashAlgorithmType"/>.
        /// This will return false if either this, or the other <see cref="HashAlgorithmType"/> is <see cref="Unknown"/>.
        /// </summary>
        /// <param name="other">The object to check for equality.</param>
        /// <returns>
        /// Returns true if this <see cref="HashAlgorithmType"/> is equal to the other; otherwise, false.
        /// If either <see cref="HashAlgorithmType"/> is <see cref="Unknown"/>, this always returns false.
        /// </returns>
        public override bool Equals(HashAlgorithmType? other)
        {
            if (ReferenceEquals(this, Unknown) || ReferenceEquals(other, Unknown))
            {
                return false;
            }

            return base.Equals(other);
        }
    }
}
