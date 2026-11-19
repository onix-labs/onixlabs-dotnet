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
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography.UnitTests;

/// <summary>
/// Provides the tests with short forms of the unpadded base64url encoding that JOSE uses, through OnixLabs.Core's
/// <see cref="Base64FormatProvider.Url"/>.
/// </summary>
internal static class Base64Url
{
    /// <summary>
    /// Encodes the specified bytes as unpadded base64url.
    /// </summary>
    /// <param name="value">The bytes to encode.</param>
    /// <returns>Returns the unpadded base64url encoding of <paramref name="value"/>.</returns>
    public static string Encode(ReadOnlySpan<byte> value) => IBaseCodec.Base64.Encode(value, Base64FormatProvider.Url);

    /// <summary>
    /// Decodes the specified unpadded base64url text.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <returns>Returns the decoded bytes.</returns>
    public static byte[] Decode(ReadOnlySpan<char> value) => IBaseCodec.Base64.Decode(value, Base64FormatProvider.Url);
}
