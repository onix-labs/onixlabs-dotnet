// Copyright © 2020 ONIXLabs
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
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Computes a SHA-3 Shake 256-bit hash from the specified value.
    /// This will use the default encoding to convert the input string into a byte array.
    /// </summary>
    /// <param name="value">The input value to hash.</param>
    /// <param name="length">The length of the hash in bytes.</param>
    /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
    public static async Task<Hash> ComputeSha3Shake256Async(string value, int length)
    {
        return await ComputeSha3Shake256Async(value, Encoding.Default, length);
    }

    /// <summary>
    /// Computes a SHA-3 Shake 256-bit hash from the specified value.
    /// </summary>
    /// <param name="value">The input value to hash.</param>
    /// <param name="encoding">The encoding which will be used to convert the input string into a byte array.</param>
    /// <param name="length">The length of the hash in bytes.</param>
    /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
    public static async Task<Hash> ComputeSha3Shake256Async(string value, Encoding encoding, int length)
    {
        return await ComputeHashAsync(value, HashAlgorithmType.Sha3Shake256, encoding, length);
    }

    /// <summary>
    /// Computes a SHA-3 Shake 256-bit hash from the specified value.
    /// </summary>
    /// <param name="value">The input value to hash.</param>
    /// <param name="length">The length of the hash in bytes.</param>
    /// <returns>Returns a <see cref="Hash"/> of the input value.</returns>
    public static async Task<Hash> ComputeSha3Shake256Async(byte[] value, int length)
    {
        return await ComputeHashAsync(value, HashAlgorithmType.Sha3Shake256, length);
    }
}
