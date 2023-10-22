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

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hmac
{
    /// <summary>
    /// Computes a hashed message authentication code (HMAC).
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <param name="type">The hash algorithm type of the computed HMAC.</param>
    /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
    public static async Task<Hmac> ComputeHmacAsync(string value, string key, HashAlgorithmType type)
    {
        return await ComputeHmacAsync(value, key, type, Encoding.Default);
    }

    /// <summary>
    /// Computes a hashed message authentication code (HMAC).
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <param name="type">The hash algorithm type of the computed HMAC.</param>
    /// <param name="encoding">The encoding which will be used to convert the value and key into a byte array.</param>
    /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
    public static async Task<Hmac> ComputeHmacAsync(string value, string key, HashAlgorithmType type, Encoding encoding)
    {
        byte[] valueBytes = encoding.GetBytes(value);
        byte[] keyBytes = encoding.GetBytes(key);
        return await ComputeHmacAsync(valueBytes, keyBytes, type);
    }

    /// <summary>
    /// Computes a hashed message authentication code (HMAC).
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <param name="type">The hash algorithm type of the computed HMAC.</param>
    /// <returns>Returns a <see cref="Hmac"/> representing the specified value and key.</returns>
    public static async Task<Hmac> ComputeHmacAsync(byte[] value, byte[] key, HashAlgorithmType type)
    {
        using KeyedHashAlgorithm algorithm = type.GetKeyedHashAlgorithm(key);
        await using Stream stream = new MemoryStream(value);

        byte[] data = await algorithm.ComputeHashAsync(stream);
        Hash hash = Hash.Create(data, type);

        return Create(hash, value);
    }
}
