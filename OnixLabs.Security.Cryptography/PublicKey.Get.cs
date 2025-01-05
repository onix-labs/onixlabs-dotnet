// Copyright 2020 ONIXLabs
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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public abstract partial class PublicKey
{
    /// <summary>
    /// Gets a <see cref="Hash"/> representation of the current <see cref="PublicKey"/> instance.
    /// </summary>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the public key data.</param>
    /// <returns>Returns a <see cref="Hash"/> representation of the current <see cref="PublicKey"/> instance.</returns>
    public Hash GetHash(HashAlgorithm algorithm) => Hash.Compute(algorithm, KeyData);
}
