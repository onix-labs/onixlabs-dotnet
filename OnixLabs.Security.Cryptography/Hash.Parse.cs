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

using System;

namespace OnixLabs.Security.Cryptography
{
    /// <summary>
    /// Represents a cryptographic hash.
    /// </summary>
    public readonly partial struct Hash
    {
        /// <summary>
        /// Converts a hexadecimal representation of a hash into a <see cref="Hash"/> instance.
        /// This will create a hash of an unknown type.
        /// </summary>
        /// <param name="value">A <see cref="string"/> that contains a hash to convert.</param>
        /// <returns>A new <see cref="Hash"/> instance.</returns>
        public static Hash Parse(string value)
        {
            return Parse(value, HashAlgorithmType.Unknown);
        }

        /// <summary>
        /// Converts a hexadecimal representation of a hash into a <see cref="Hash"/> instance.
        /// </summary>
        /// <param name="value">A <see cref="string"/> that contains a hash to convert.</param>
        /// <param name="type">The hash algorithm type of the hash.</param>
        /// <returns>A new <see cref="Hash"/> instance.</returns>
        public static Hash Parse(string value, HashAlgorithmType type)
        {
            byte[] bytes = Convert.FromHexString(value);
            return new Hash(bytes, type);
        }
    }
}
