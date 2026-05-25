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

namespace OnixLabs.Numerics;

public readonly partial struct Int512
{
    /// <summary>Tries to format the value of the current instance into the provided span of characters.</summary>
    /// <param name="destination">The span into which to write the formatted value.</param>
    /// <param name="charsWritten">When this method returns, contains the number of characters written.</param>
    /// <param name="format">A span containing the format specifier.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns <see langword="true"/> if the formatting was successful; otherwise, <see langword="false"/>.</returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        string formatted = ToString(format.ToString(), provider);
        if (formatted.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        formatted.CopyTo(destination);
        charsWritten = formatted.Length;
        return true;
    }

    /// <summary>Tries to format the value of the current instance into the provided span of UTF-8 bytes.</summary>
    /// <param name="utf8Destination">The span into which to write the formatted UTF-8 bytes.</param>
    /// <param name="bytesWritten">When this method returns, contains the number of bytes written.</param>
    /// <param name="format">A span containing the format specifier.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns <see langword="true"/> if the formatting was successful; otherwise, <see langword="false"/>.</returns>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        string formatted = ToString(format.ToString(), provider);
        int requiredByteCount = Encoding.UTF8.GetByteCount(formatted);
        if (requiredByteCount > utf8Destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten = Encoding.UTF8.GetBytes(formatted, utf8Destination);
        return true;
    }
}
