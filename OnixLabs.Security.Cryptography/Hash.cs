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

namespace OnixLabs.Security.Cryptography
{
    /// <summary>
    /// Represents a cryptographic hash.
    /// </summary>
    public readonly partial struct Hash
    {
        /// <summary>
        /// The underlying hexadecimal value of the hash.
        /// </summary>
        private readonly byte[] value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hash"/> struct.
        /// </summary>
        /// <param name="value">The underlying hexadecimal value of the hash.</param>
        /// <param name="type">The hash algorithm type of the hash.</param>
        private Hash(byte[] value, HashAlgorithmType type)
        {
            type.VerifyHashLength(value);

            this.value = value;
            AlgorithmType = type;
        }

        /// <summary>
        /// Gets the hash algorithm type of the hash.
        /// </summary>
        public HashAlgorithmType AlgorithmType { get; }
    }
}
