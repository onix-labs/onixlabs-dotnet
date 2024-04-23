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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographic digital signature.
/// </summary>
/// <param name="value">The underlying value of the cryptographic digital signature.</param>
public readonly partial struct DigitalSignature(ReadOnlySpan<byte> value) : ICryptoPrimitive<DigitalSignature>
{
    private readonly byte[] value = value.ToArray();
}
