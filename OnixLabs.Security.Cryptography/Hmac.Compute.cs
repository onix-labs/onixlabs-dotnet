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

using System.Security.Cryptography;
using System.Text;

namespace OnixLabs.Security.Cryptography
{
    public readonly partial struct Hmac
    {
        /// <summary>
        /// Computes a hashed message authentication code (HMAC).
        /// </summary>
        /// <param name="value">The value for which to compute a HMAC.</param>
        /// <param name="key">The key for which to compute a HMAC.</param>
        /// <param name="type">The hash algorithm type of the computed HMAC.</param>
        /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
        public static Hmac ComputeHmac(string value, string key, HashAlgorithmType type)
        {
            return ComputeHmac(value, key, type, Encoding.Default);
        }

        /// <summary>
        /// Computes a hashed message authentication code (HMAC).
        /// </summary>
        /// <param name="value">The value for which to compute a HMAC.</param>
        /// <param name="key">The key for which to compute a HMAC.</param>
        /// <param name="type">The hash algorithm type of the computed HMAC.</param>
        /// <param name="encoding">The encoding which will be used to convert the value and key into a byte array.</param>
        /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
        public static Hmac ComputeHmac(string value, string key, HashAlgorithmType type, Encoding encoding)
        {
            byte[] valueBytes = encoding.GetBytes(value);
            byte[] keyBytes = encoding.GetBytes(key);

            return ComputeHmac(valueBytes, keyBytes, type);
        }

        /// <summary>
        /// Computes a hashed message authentication code (HMAC).
        /// </summary>
        /// <param name="value">The value for which to compute a HMAC.</param>
        /// <param name="key">The key for which to compute a HMAC.</param>
        /// <param name="type">The hash algorithm type of the computed HMAC.</param>
        /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
        public static Hmac ComputeHmac(byte[] value, byte[] key, HashAlgorithmType type)
        {
            using KeyedHashAlgorithm algorithm = type.GetKeyedHashAlgorithm(key);

            byte[] data = algorithm.ComputeHash(value);
            Hash hash = Hash.FromByteArray(data, type);

            return Create(hash, value);
        }
    }
}
