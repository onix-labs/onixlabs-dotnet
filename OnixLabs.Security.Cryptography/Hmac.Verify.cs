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
    /// <summary>
    /// Represents a hashed message authentication code (HMAC).
    /// </summary>
    public readonly partial struct Hmac
    {
        /// <summary>
        /// Determines whether the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        /// <returns>Returns true if this <see cref="Hmac"/> was created with the specified key; otherwise, false.</returns>
        public bool IsValid(string key)
        {
            return IsValid(key, Encoding.Default);
        }

        /// <summary>
        /// Determines whether the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        /// <param name="encoding">The encoding which will be used to convert the key into a byte array.</param>
        /// <returns>Returns true if this <see cref="Hmac"/> was created with the specified key; otherwise, false.</returns>
        public bool IsValid(string key, Encoding encoding)
        {
            byte[] keyBytes = encoding.GetBytes(key);
            return IsValid(keyBytes);
        }

        /// <summary>
        /// Determines whether the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        /// <returns>Returns true if this <see cref="Hmac"/> was created with the specified key; otherwise, false.</returns>
        public bool IsValid(byte[] key)
        {
            return this == ComputeHmac(Data, key, Hash.AlgorithmType);
        }

        /// <summary>
        /// Verifies that the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        public void Verify(string key)
        {
            Verify(key, Encoding.Default);
        }

        /// <summary>
        /// Verifies that the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="encoding">The encoding which will be used to convert the key into a byte array.</param>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        public void Verify(string key, Encoding encoding)
        {
            byte[] keyBytes = encoding.GetBytes(key);
            Verify(keyBytes);
        }

        /// <summary>
        /// Verifies that the hashed message authentication code (HMAC) was created with the specified key.
        /// </summary>
        /// <param name="key">The key to validate against this <see cref="Hmac"/>.</param>
        public void Verify(byte[] key)
        {
            if (!IsValid(key))
            {
                throw new CryptographicException("The HMAC was could not be verified with the specified key.");
            }
        }
    }
}
