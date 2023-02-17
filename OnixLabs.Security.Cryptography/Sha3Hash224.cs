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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Computes the FIPS 202 SHA-3 224-bit hash for the input data.
/// </summary>
public sealed class Sha3Hash224 : Sha3
{
    /// <summary>
    /// The rate in bytes of the sponge state.
    /// </summary>
    private const int RateBytes = 144;

    /// <summary>
    /// The length of the hash in bits.
    /// </summary>
    private const int BitLength = 224;

    /// <summary>
    /// Creates a new instance of the <see cref="Sha3Hash224"/> class.
    /// </summary>
    public Sha3Hash224() : base(RateBytes, HashDelimiter, BitLength)
    {
    }
}
