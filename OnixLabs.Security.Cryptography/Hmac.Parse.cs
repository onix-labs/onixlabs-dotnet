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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hmac
{
    /// <summary>
    /// Parses a hexadecimal representation of a HMAC into a <see cref="Hmac"/> instance.
    /// </summary>
    /// <param name="value">A <see cref="string"/> that contains a HMAC to convert.</param>
    /// <param name="type">The hash algorithm type of the HMAC.</param>
    /// <returns>A new <see cref="Hmac"/> instance.</returns>
    public static Hmac Parse(string value, HashAlgorithmType? type = null)
    {
        string hashComponent = value.SubstringBeforeLast(':');
        string dataComponent = value.SubstringAfterLast(':');

        Hash hash = Hash.Parse(hashComponent, type);
        byte[] data = Convert.FromHexString(dataComponent);

        return Create(hash, data);
    }

    /// <summary>
    /// Attempts to parse a hexadecimal representation of a HMAC into a <see cref="Hmac"/> instance.
    /// </summary>
    /// <param name="value">A <see cref="string"/> that contains a HMAC to convert.</param>
    /// <param name="type">The hash algorithm type of the hash.</param>
    /// <param name="hmac">The <see cref="Hmac"/> result if conversion was successful.</param>
    /// <returns>Returns true if the hash conversion was successful; otherwise, false.</returns>
    public static bool TryParse(string value, HashAlgorithmType? type, out Hmac hmac)
    {
        try
        {
            hmac = Parse(value, type);
            return true;
        }
        catch
        {
            hmac = default;
            return false;
        }
    }
}
