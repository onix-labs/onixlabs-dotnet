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

using System;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Security.Cryptography;

public sealed partial class HashAlgorithmType
{
    /// <summary>
    /// Determines whether the length of a byte array is valid.
    /// </summary>
    /// <param name="value">The byte array to length check.</param>
    /// <returns>Returns true if the length of the byte array is valid; otherwise, false.</returns>
    public bool IsValidHashLength(byte[] value)
    {
        return Length == -1 || Length == value.Length;
    }

    /// <summary>
    /// Verifies that the length of a byte array is valid.
    /// </summary>
    /// <param name="value">The byte array to length check.</param>
    /// <exception cref="ArgumentException">If the length of the byte array is invalid.</exception>
    public void VerifyHashLength(byte[] value)
    {
        Require(IsValidHashLength(value), "The length of the hash is invalid.", nameof(value));
    }
}
