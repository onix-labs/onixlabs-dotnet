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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic secret.
/// </summary>
public readonly partial struct Secret : ICryptoPrimitive<Secret>, ISpanParsable<Secret>
{
    private readonly ProtectedData protectedData = new();
    private readonly byte[] encryptedValue;
    private readonly Hash hash;

    /// <summary>
    /// Initializes a new instance of the <see cref="Secret"/> struct.
    /// </summary>
    /// <param name="value">The underlying value of the cryptographic secret.</param>
    public Secret(ReadOnlySpan<byte> value)
    {
        encryptedValue = protectedData.Encrypt(value.ToArray());
        hash = Hash.Compute(SHA256.Create(), value.ToArray());
    }
}
