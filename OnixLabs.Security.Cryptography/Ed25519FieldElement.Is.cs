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

namespace OnixLabs.Security.Cryptography;

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Gets a value indicating whether this field element represents zero modulo p.
    /// </summary>
    /// <value><see langword="true"/> if the canonical representative of this element is zero; otherwise, <see langword="false"/>.</value>
    public bool IsZero
    {
        get
        {
            Span<byte> bytes = stackalloc byte[32];
            WriteBytes(bytes);
            int accumulator = 0;
            for (int i = 0; i < 32; i++) accumulator |= bytes[i];
            return accumulator == 0;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the canonical representative is odd.
    /// Used as the sign bit during Ed25519 point encoding per RFC 8032 §5.1.2.
    /// </summary>
    /// <value><see langword="true"/> if the least significant bit of the canonical representative is one; otherwise, <see langword="false"/>.</value>
    public bool IsNegative
    {
        get
        {
            Span<byte> bytes = stackalloc byte[32];
            WriteBytes(bytes);
            return (bytes[0] & 1) == 1;
        }
    }
}
