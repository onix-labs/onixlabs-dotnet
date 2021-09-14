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
    /// Represents a hashed message authentication code (HMAC).
    /// </summary>
    public readonly partial struct Hmac
    {
        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Hash}:{Convert.ToHexString(Data).ToLower()}";
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object, including the hash algorithm type.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current object, including the hash algorithm type.</returns>
        public string ToStringWithAlgorithmType()
        {
            return $"{Hash.AlgorithmType.Name}:{ToString()}";
        }
    }
}
