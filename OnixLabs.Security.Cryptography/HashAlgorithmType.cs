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
/// Specifies values that define known hash algorithm types.
/// </summary>
public sealed partial class HashAlgorithmType : Enumeration<HashAlgorithmType>
{
    /// <summary>
    /// A constant that defines an unknown hash algorithm length.
    /// </summary>
    private const int UnknownLength = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="HashAlgorithmType"/> class.
    /// </summary>
    /// <param name="value">The value of the hash algorithm type.</param>
    /// <param name="name">The name of the hash algorithm type.</param>
    /// <param name="length">The length in bytes of the hash algorithm type.</param>
    /// <param name="isKeyBased">Determines whether the algorithm is a key based hash algorithm.</param>
    private HashAlgorithmType(int value, string name, int length, bool isKeyBased) : base(value, name)
    {
        Length = length;
        IsKeyBased = isKeyBased;
    }

    /// <summary>
    /// Gets the length of an algorithm's hash in bytes.
    /// -1 Means that the algorithm's hash is of variable length, or is unknown.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Gets a value that determines whether the algorithm is a key based hash algorithm.
    /// </summary>
    public bool IsKeyBased { get; }

    /// <summary>
    /// Gets a value that determines whether the algorithm type is unknown.
    /// </summary>
    public bool IsUnknown => ReferenceEquals(this, Unknown);
}
