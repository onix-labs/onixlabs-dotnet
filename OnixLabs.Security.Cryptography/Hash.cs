// Copyright 2020-2024 ONIXLabs
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
using System.Linq;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic hash.
/// </summary>
/// <param name="value">The underlying value of the cryptographic hash.</param>
public readonly partial struct Hash : IEquatable<Hash>, IComparable<Hash>, IComparable
{
    private readonly byte[] value = value.ToArray();

    /// <summary>
    /// Initializes a new instance of the <see cref="Hash"/> struct.
    /// </summary>
    /// <param name="value">The underlying value of the cryptographic hash.</param>
    /// <param name="length">The length of the cryptographic hash in bytes.</param>
    public Hash(byte value, int length) : this(Enumerable.Repeat(value, length).ToArray())
    {
    }
}
