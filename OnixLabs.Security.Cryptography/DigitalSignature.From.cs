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
    public readonly partial struct DigitalSignature
    {
        /// <summary>
        /// Creates a <see cref="DigitalSignature"/> instance from the specified digitally signed data.
        /// </summary>
        /// <param name="value">The digitally signed data.</param>
        /// <returns>Returns a <see cref="DigitalSignature"/> instance from the specified digitally signed data.</returns>
        public static DigitalSignature FromByteArray(byte[] value)
        {
            return new DigitalSignature(value);
        }

        /// <summary>
        /// Creates a <see cref="DigitalSignature"/> from the specified <see cref="Base16"/> value.
        /// </summary>
        /// <param name="value">The Base-16 from which to construct a signature value.</param>
        /// <returns>Returns an <see cref="DigitalSignature"/> from the specified Base-16 value.</returns>
        public static DigitalSignature FromBase16(Base16 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="DigitalSignature"/> from the specified <see cref="Base32"/> value.
        /// </summary>
        /// <param name="value">The Base-32 from which to construct a signature value.</param>
        /// <returns>Returns an <see cref="DigitalSignature"/> from the specified Base-32 value.</returns>
        public static DigitalSignature FromBase32(Base32 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="DigitalSignature"/> from the specified <see cref="Base58"/> value.
        /// </summary>
        /// <param name="value">The Base-58 from which to construct a signature value.</param>
        /// <returns>Returns an <see cref="DigitalSignature"/> from the specified Base-58 value.</returns>
        public static DigitalSignature FromBase58(Base58 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }

        /// <summary>
        /// Creates a <see cref="DigitalSignature"/> from the specified <see cref="Base64"/> value.
        /// </summary>
        /// <param name="value">The Base-64 from which to construct a signature value.</param>
        /// <returns>Returns an <see cref="DigitalSignature"/> from the specified Base-64 value.</returns>
        public static DigitalSignature FromBase64(Base64 value)
        {
            byte[] bytes = value.ToByteArray();
            return FromByteArray(bytes);
        }
    }
}
