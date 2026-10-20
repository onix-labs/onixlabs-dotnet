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
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OnixLabs.Core.Text;

public static partial class Multibase
{
    private const string InvalidMultibase = "The value is not a valid Multibase string.";
    private const string UnexpectedEncoding = "The value is not a valid Multibase string in the required encoding.";

    /// <summary>
    /// Decodes the specified Multibase string, and reports the encoding named by its prefix.
    /// </summary>
    /// <param name="value">The Multibase string to decode.</param>
    /// <param name="encoding">The encoding named by the prefix of the specified value.</param>
    /// <returns>Returns the decoded bytes.</returns>
    /// <exception cref="FormatException">
    /// Thrown when the specified value is empty, has an unsupported prefix, or has a payload that is not canonical for its encoding.
    /// </exception>
    public static byte[] Decode(ReadOnlySpan<char> value, out MultibaseEncoding encoding)
    {
        if (TryDecode(value, out MultibaseEncoding? result, out byte[] bytes))
        {
            encoding = result;
            return bytes;
        }

        throw new FormatException(InvalidMultibase);
    }

    /// <summary>
    /// Decodes the specified Multibase string, which must use the specified encoding.
    /// </summary>
    /// <param name="value">The Multibase string to decode.</param>
    /// <param name="encoding">The encoding that the specified value must use.</param>
    /// <returns>Returns the decoded bytes.</returns>
    /// <exception cref="FormatException">
    /// Thrown when the specified value does not use the specified encoding, or has a payload that is not canonical for it.
    /// </exception>
    public static byte[] Decode(ReadOnlySpan<char> value, MultibaseEncoding encoding) =>
        TryDecode(value, encoding, out byte[] result) ? result : throw new FormatException(UnexpectedEncoding);

    /// <summary>
    /// Tries to decode the specified Multibase string, and reports the encoding named by its prefix.
    /// </summary>
    /// <param name="value">The Multibase string to decode.</param>
    /// <param name="encoding">The encoding named by the prefix of the specified value, or <see langword="null"/> if the prefix is not supported.</param>
    /// <param name="result">The decoded bytes, or an empty <see cref="byte"/> array if the specified value could not be decoded.</param>
    /// <returns>Returns <see langword="true"/> if the specified value was decoded successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryDecode(ReadOnlySpan<char> value, [NotNullWhen(true)] out MultibaseEncoding? encoding, out byte[] result)
    {
        result = [];
        encoding = null;

        if (value.IsEmpty) return false;

        char prefix = value[0];
        MultibaseEncoding? candidate = MultibaseEncoding.GetAll().FirstOrDefault(entry => entry.Prefix == prefix);

        if (candidate is null || !IBaseCodec.TryGetBytes(value[1..], candidate.Provider, out result)) return false;

        encoding = candidate;
        return true;
    }

    /// <summary>
    /// Tries to decode the specified Multibase string, which must use the specified encoding.
    /// </summary>
    /// <param name="value">The Multibase string to decode.</param>
    /// <param name="encoding">The encoding that the specified value must use.</param>
    /// <param name="result">The decoded bytes, or an empty <see cref="byte"/> array if the specified value could not be decoded.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified value uses the specified encoding and was decoded successfully; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryDecode(ReadOnlySpan<char> value, MultibaseEncoding encoding, out byte[] result)
    {
        if (TryDecode(value, out MultibaseEncoding? actual, out result) && actual == encoding) return true;

        result = [];
        return false;
    }
}
