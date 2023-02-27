// Copyright 2020-2023 ONIXLabs
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

using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <summary>
    /// Returns a <see cref="byte"/> array containing the underlying salt data.
    /// </summary>
    /// <returns>A <see cref="byte"/> array containing the underlying salt data.</returns>
    public byte[] ToByteArray()
    {
        return Value.Copy();
    }

    /// <summary>
    /// Creates a <see cref="Hash"/> from the current salt data.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> of the hash to produce.</param>
    /// <returns>Returns a <see cref="Hash"/> of the current salt data.</returns>
    public Hash ToHash(HashAlgorithmType type)
    {
        return Hash.ComputeHash(Value, type);
    }

    /// <summary>
    /// Creates a <see cref="Hash"/> from the current salt data.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> of the hash to produce.</param>
    /// <param name="length">The length of the hash to produce.</param>
    /// <returns>Returns a <see cref="Hash"/> of the current salt data.</returns>
    public Hash ToHash(HashAlgorithmType type, int length)
    {
        return Hash.ComputeHash(Value, type, length);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        return Base16.FromByteArray(Value).ToString();
    }
}
