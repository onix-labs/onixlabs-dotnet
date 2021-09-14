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
    /// Specifies values that define known hash algorithm types.
    /// </summary>
    public sealed partial class HashAlgorithmType
    {
        /// <summary>
        /// Checks for equality between this <see cref="HashAlgorithmType"/> and another <see cref="HashAlgorithmType"/>.
        /// This will return false if either this, or the other <see cref="HashAlgorithmType"/> is <see cref="Unknown"/>.
        /// </summary>
        /// <param name="other">The object to check for equality.</param>
        /// <returns>
        /// Returns true if this <see cref="HashAlgorithmType"/> is equal to the other; otherwise, false.
        /// If either <see cref="HashAlgorithmType"/> is <see cref="Unknown"/>, this always returns false.
        /// </returns>
        public override bool Equals(HashAlgorithmType? other)
        {
            return !ReferenceEquals(this, Unknown)
                   && !ReferenceEquals(other, Unknown)
                   && base.Equals(other);
        }
    }
}
