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

using System.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hmac
{
    /// <summary>
    /// Computes a SHA-2 256-bit HMAC from the specified value and key.
    /// This will use the default encoding to convert the input value and key into a byte array.
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <returns>Returns a <see cref="Hmac"/> of the input value and key.</returns>
    public static Hmac ComputeSha2Hmac256(string value, string key)
    {
        return ComputeSha2Hmac256(value, key, Encoding.Default);
    }

    /// <summary>
    /// Computes a SHA-2 256-bit HMAC from the specified value and key.
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <param name="encoding">The encoding which will be used to convert the value and key into a byte array.</param>
    /// <returns>Returns a <see cref="Hmac"/> of the input value and key.</returns>
    public static Hmac ComputeSha2Hmac256(string value, string key, Encoding encoding)
    {
        byte[] valueBytes = encoding.GetBytes(value);
        byte[] keyBytes = encoding.GetBytes(key);

        return ComputeSha2Hmac256(valueBytes, keyBytes);
    }

    /// <summary>
    /// Computes a SHA-2 256-bit HMAC from the specified value and key.
    /// </summary>
    /// <param name="value">The value for which to compute a HMAC.</param>
    /// <param name="key">The key for which to compute a HMAC.</param>
    /// <returns>Returns a <see cref="Hmac"/> of the input value and key.</returns>
    public static Hmac ComputeSha2Hmac256(byte[] value, byte[] key)
    {
        return ComputeHmac(value, key, HashAlgorithmType.Sha2Hmac256);
    }
}
