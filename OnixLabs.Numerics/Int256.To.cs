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
using System.Globalization;
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Int256
{
    /// <summary>Returns the string representation of the current <see cref="Int256"/> value using the default format and culture.</summary>
    /// <returns>The string representation.</returns>
    public override string ToString() => ToString(null, CultureInfo.CurrentCulture);

    /// <summary>Returns the string representation of the current <see cref="Int256"/> value using the specified format.</summary>
    /// <param name="format">The format to use, or <see langword="null"/> for default.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.CurrentCulture);

    /// <summary>Returns the string representation of the current <see cref="Int256"/> value using the specified format and provider.</summary>
    /// <param name="format">The format to use, or <see langword="null"/> for default.</param>
    /// <param name="formatProvider">The provider to use, or <see langword="null"/> for current culture.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        ReadOnlySpan<char> formatSpan = (format ?? string.Empty).AsSpan();
        char specifier = formatSpan.IsEmpty || formatSpan.IsWhiteSpace() ? NumberInfoFormatter.DefaultFormat : formatSpan[0];
        char upperSpecifier = char.ToUpperInvariant(specifier);

        // Route X (hex) and R (round-trip) through NumberInfoFormatter so the output matches .NET integer
        // conventions. X uses the unsigned bit pattern so negative values render as two's-complement hex
        // (e.g. -1 → "FFFF...FF"), matching how System.Int32 and friends format X.
        if (upperSpecifier is 'X' or 'R')
        {
            BigInteger unscaled = upperSpecifier == 'X' ? (BigInteger)ReinterpretAsUnsigned(this) : (BigInteger)this;
            // ReSharper disable once HeapView.ObjectAllocation.Evident, HeapView.ObjectAllocation
            NumberInfoFormatter formatter = new(new NumberInfo(unscaled, 0), formatProvider ?? CultureInfo.CurrentCulture, ['R', 'X']);
            return formatter.Format(formatSpan);
        }

        return this.ToBigInteger().ToString(format, formatProvider);
    }
}
