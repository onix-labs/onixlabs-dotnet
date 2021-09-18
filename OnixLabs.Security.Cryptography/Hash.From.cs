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

using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography
{
    public readonly partial struct Hash
    {
        /// <summary>
        /// Creates a <see cref="Hash"/> instance from a <see cref="byte"/> array.
        /// This will create a hash of an unknown type.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> array to represent as a hash.</param>
        /// <returns>A new <see cref="Hash"/> instance.</returns>
        public static Hash FromByteArray(byte[] value)
        {
            return FromByteArray(value, HashAlgorithmType.Unknown);
        }

        /// <summary>
        /// Creates a <see cref="Hash"/> instance from a <see cref="byte"/> array.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> array to represent as a hash.</param>
        /// <param name="type">The hash algorithm type of the hash.</param>
        /// <returns>A new <see cref="Hash"/> instance.</returns>
        public static Hash FromByteArray(byte[] value, HashAlgorithmType type)
        {
            return new Hash(value, type);
        }

        /// <summary>
        /// Creates a <see cref="Hash"/> from the specified <see cref="Base16"/> value.
        /// </summary>
        /// <param name="value">The Base-16 from which to construct a hash value.</param>
        /// <returns>Returns an <see cref="Hash"/> from the specified Base-16 value.</returns>
        public static Hash FromBase16(Base16 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="Hash"/> from the specified <see cref="Base32"/> value.
        /// </summary>
        /// <param name="value">The Base-32 from which to construct a hash value.</param>
        /// <returns>Returns an <see cref="Hash"/> from the specified Base-32 value.</returns>
        public static Hash FromBase32(Base32 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="Hash"/> from the specified <see cref="Base58"/> value.
        /// </summary>
        /// <param name="value">The Base-58 from which to construct a hash value.</param>
        /// <returns>Returns an <see cref="Hash"/> from the specified Base-58 value.</returns>
        public static Hash FromBase58(Base58 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="Hash"/> from the specified <see cref="Base64"/> value.
        /// </summary>
        /// <param name="value">The Base-64 from which to construct a hash value.</param>
        /// <returns>Returns an <see cref="Hash"/> from the specified Base-64 value.</returns>
        public static Hash FromBase64(Base64 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }
    }
}
