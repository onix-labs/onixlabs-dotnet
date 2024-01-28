// Copyright © 2020 ONIXLabs
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
using System.Text;

namespace OnixLabs.Core.Text;

public readonly partial struct Base16
{
    /// <summary>
    /// Returns a <see cref="byte"/> array that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="byte"/> array that represents the current object.</returns>
    public byte[] ToByteArray()
    {
        return Value.Copy();
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object in plain text.
    /// </summary>
    /// <returns>Returns a <see cref="string"/> that represents the current object in plain text.</returns>
    public string ToPlainTextString()
    {
        return ToPlainTextString(Encoding.Default);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object in plain text.
    /// </summary>
    /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
    /// <returns>Returns a <see cref="string"/> that represents the current object in plain text.</returns>
    public string ToPlainTextString(Encoding encoding)
    {
        return encoding.GetString(Value);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        return ToString([DefaultFormat]);
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        return ToString(format.AsSpan(), formatProvider);
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        return char.ToUpperInvariant(format.IsEmpty || format.IsWhiteSpace() ? DefaultFormat : format[0]) switch
        {
            'L' => Convert.ToHexString(Value).ToLower(),
            'U' => Convert.ToHexString(Value),
            _ => format.ToString()
        };
    }
}
