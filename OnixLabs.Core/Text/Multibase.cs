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

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides Multibase encoding and decoding: a base-encoded string prefixed with a single character that names its encoding.
/// </summary>
/// <remarks>
/// <para>
/// Multibase is used by DID documents (<c>publicKeyMultibase</c>) and by <c>did:key</c>, which uses the <c>z</c> prefix.
/// The supported encodings are the members of <see cref="MultibaseEncoding"/>.
/// </para>
/// <para>
/// Decoding is strict, so every value has exactly one valid Multibase string for each encoding. The prefix fixes the alphabet, the padding and
/// the case of the payload, and non-canonical payloads are rejected. This deliberately differs from the Multibase specification, which decodes
/// Base-16 and Base-32 payloads in any case: a payload whose case does not match its prefix, such as <c>b</c> followed by uppercase characters,
/// is rejected.
/// </para>
/// </remarks>
public static partial class Multibase
{
    /// <summary>
    /// Encodes the specified value as a Multibase string, using the specified encoding.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <returns>Returns the prefix of the specified encoding, followed by the specified value encoded with it.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="encoding"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException">Thrown when the specified value cannot be encoded.</exception>
    public static string Encode(ReadOnlySpan<byte> value, MultibaseEncoding encoding)
    {
        RequireNotNull(encoding);
        return encoding.Prefix + IBaseCodec.GetString(value, encoding.Provider);
    }
}
