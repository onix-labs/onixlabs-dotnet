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

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography
{
    public readonly partial struct Hash
    {
        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(string value, HashAlgorithmType type)
        {
            return await ComputeHashTwiceAsync(value, type, Encoding.Default);
        }

        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <param name="encoding">The encoding which will be used to convert the input string into a byte array.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(string value, HashAlgorithmType type, Encoding encoding)
        {
            return await ComputeHashTwiceAsync(encoding.GetBytes(value), type);
        }

        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <param name="length">The output length of the computed hash in bytes.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(string value, HashAlgorithmType type, int length)
        {
            return await ComputeHashTwiceAsync(value, type, Encoding.Default, length);
        }

        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <param name="encoding">The encoding which will be used to convert the input string into a byte array.</param>
        /// <param name="length">The output length of the computed hash in bytes.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(
            string value,
            HashAlgorithmType type,
            Encoding encoding,
            int length)
        {
            return await ComputeHashTwiceAsync(encoding.GetBytes(value), type, length);
        }

        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(byte[] value, HashAlgorithmType type)
        {
            using HashAlgorithm algorithm = type.GetHashAlgorithm();

            await using Stream firstRoundStream = new MemoryStream(value);
            byte[] firstRoundValue = await algorithm.ComputeHashAsync(firstRoundStream);

            await using Stream secondRoundStream = new MemoryStream(firstRoundValue);
            byte[] secondRoundValue = await algorithm.ComputeHashAsync(secondRoundStream);

            return FromByteArray(secondRoundValue, type);
        }

        /// <summary>
        /// Computes a twice-hashed hash of the specified value using the specified <see cref="HashAlgorithmType"/>.
        /// </summary>
        /// <param name="value">The value for which to compute a hash.</param>
        /// <param name="type">The hash algorithm type of the computed hash.</param>
        /// <param name="length">The output length of the computed hash in bytes.</param>
        /// <returns>Returns a <see cref="Hash"/> representing a twice-hashed hash of the specified value.</returns>
        public static async Task<Hash> ComputeHashTwiceAsync(byte[] value, HashAlgorithmType type, int length)
        {
            using HashAlgorithm algorithm = type.GetHashAlgorithm(length);

            await using Stream firstRoundStream = new MemoryStream(value);
            byte[] firstRoundValue = await algorithm.ComputeHashAsync(firstRoundStream);

            await using Stream secondRoundStream = new MemoryStream(firstRoundValue);
            byte[] secondRoundValue = await algorithm.ComputeHashAsync(secondRoundStream);

            return FromByteArray(secondRoundValue, type);
        }
    }
}
