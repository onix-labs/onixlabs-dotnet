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
using System.Numerics;
using OnixLabs.Core.Text;

namespace OnixLabs.Numerics;

internal sealed partial class NumberInfoFormatter
{
    /// <summary>
    /// Formats the current <see cref="NumberInfo"/> value using the hexadecimal format.
    /// </summary>
    /// <param name="specifier">The hexadecimal specifier, which is either uppercase 'X' or lowercase 'x'.</param>
    /// <param name="format">The full format span, including any precision following the specifier.</param>
    /// <returns>Returns <see langword="true"/> if the value was formatted successfully; otherwise, <see langword="false"/>.</returns>
    private bool FormatHexadecimal(char specifier, ReadOnlySpan<char> format)
    {
        if (value.Scale != 0) return false;

        int precision = 0;
        if (format.Length > 1 && !int.TryParse(format[1..], out precision)) return false;

        BigInteger magnitude = BigInteger.Abs(value.UnscaledValue);
        string hex = magnitude.ToString(specifier == 'x' ? "x" : "X");

        // BigInteger.ToString("X") prepends a single '0' to positive values whose top nibble has bit-3 set
        // (e.g. 0x80 → "080") to disambiguate from a two's-complement negative encoding. Strip it: the
        // formatter expresses negative magnitudes with a real '-' prefix, so the sign-disambiguation is moot.
        if (hex.Length > 1 && hex[0] == '0') hex = hex[1..];

        if (hex.Length < precision) hex = hex.PadLeft(precision, '0');

        builder.Append(hex);

        if (value.UnscaledValue.Sign < 0) builder.Prepend(numberFormat.NegativeSign);

        return true;
    }
}
