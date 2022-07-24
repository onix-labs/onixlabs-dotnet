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

using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a hashed message authentication code (HMAC).
/// </summary>
public readonly partial struct Hmac
{
    /// <summary>
    /// Represents the underlying un-hashed data.
    /// </summary>
    private readonly byte[] data;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hmac"/> struct.
    /// </summary>
    /// <param name="hash">The <see cref="Hash"/> representing the HMAC.</param>
    /// <param name="data">The underlying un-hashed data.</param>
    private Hmac(Hash hash, byte[] data)
    {
        Hash = hash;
        this.data = data;
    }

    /// <summary>
    /// Gets the <see cref="Hash"/> representing the HMAC.
    /// </summary>
    public Hash Hash { get; }

    /// <summary>
    /// Gets the underlying un-hashed data.
    /// </summary>
    public byte[] Data => data.Copy();
}
