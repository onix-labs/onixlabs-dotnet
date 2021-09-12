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

using System.Text;

namespace OnixLabs.Security.Cryptography
{
    /// <summary>
    /// Represents a cryptographic hash.
    /// </summary>
    public readonly partial struct Hash
    {
        /// <summary>
        /// Computes a SHA-2 384-bit hash from the specified value.
        /// This will use the default encoding to convert the input string into a byte array.
        /// </summary>
        /// <param name="value">The input value to hash.</param>
        /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
        public static Hash ComputeSha2Hash384(string value)
        {
            return ComputeSha2Hash384(value, Encoding.Default);
        }

        /// <summary>
        /// Computes a SHA-2 384-bit hash from the specified value.
        /// </summary>
        /// <param name="value">The input value to hash.</param>
        /// <param name="encoding">The encoding which will be used to convert the input string into a byte array.</param>
        /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
        public static Hash ComputeSha2Hash384(string value, Encoding encoding)
        {
            return ComputeHash(value, HashAlgorithmType.Sha2Hash384, encoding);
        }

        /// <summary>
        /// Computes a SHA-2 384-bit hash from the specified value.
        /// </summary>
        /// <param name="value">The input value to hash.</param>
        /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
        public static Hash ComputeSha2Hash384(byte[] value)
        {
            return ComputeHash(value, HashAlgorithmType.Sha2Hash384);
        }
    }
}
