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

using System;
using System.Security.Cryptography;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the current <see cref="Hash"/> instance.
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the current <see cref="Hash"/> instance.</returns>
    public byte[] ToByteArray() => value.Copy();

    /// <summary>
    /// Creates a new <see cref="NamedHash"/> from the current <see cref="Hash"/> instance.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm that was used to produce the associated hash.</param>
    /// <returns>Returns a new <see cref="NamedHash"/> from the current <see cref="Hash"/> instance.</returns>
    public NamedHash ToNamedHash(string algorithmName) => new(this, algorithmName);

    /// <summary>
    /// Creates a new <see cref="NamedHash"/> from the current <see cref="Hash"/> instance.
    /// </summary>
    /// <param name="algorithmName">The name of the hash algorithm that was used to produce the associated hash.</param>
    /// <returns>Returns a new <see cref="NamedHash"/> from the current <see cref="Hash"/> instance.</returns>
    public NamedHash ToNamedHash(HashAlgorithmName algorithmName) => new(this, algorithmName);

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>Returns <see cref="string"/> that represents the current object.</returns>
    public override string ToString() => Convert.ToHexString(value).ToLower();
}
