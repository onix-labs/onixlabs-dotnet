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
    /// Represents the base class for public key implementations.
    /// </summary>
    public abstract partial class PublicKey
    {
        /// <summary>
        /// Creates a new instance of the <see cref="PublicKey"/> class.
        /// </summary>
        /// <param name="data">The public key data.</param>
        /// <param name="type">The hash algorithm type for computing signature data.</param>
        protected PublicKey(byte[] data, HashAlgorithmType type)
        {
            type.GetHashAlgorithmName();
            
            PublicKeyData = data;
            AlgorithmType = type;
        }

        /// <summary>
        /// Gets the underlying public key data.
        /// </summary>
        protected byte[] PublicKeyData { get; }

        /// <summary>
        /// Gets the hash algorithm type for computing signature data.
        /// </summary>
        public HashAlgorithmType AlgorithmType { get; }
    }
}
