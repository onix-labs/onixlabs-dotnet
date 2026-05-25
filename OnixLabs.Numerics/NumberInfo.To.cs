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

namespace OnixLabs.Numerics;

public readonly partial struct NumberInfo
{
    /// <summary>
    /// The set of format specifiers supported when formatting a <see cref="NumberInfo"/> value.
    /// </summary>
    private static readonly char[] SupportedFormats = ['C', 'D', 'E', 'F', 'G', 'N', 'P', 'R', 'X'];

    /// <summary>
    /// Formats the value of the current instance using the default format.
    /// </summary>
    /// <returns>The value of the current instance in the default format.</returns>
    public override string ToString() => ToString(DefaultNumberFormat, CultureInfo.CurrentCulture);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString((format ?? DefaultNumberFormat).AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use, or null to use the default format.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        IFormatProvider info = formatProvider ?? CultureInfo.CurrentCulture;
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        NumberInfoFormatter formatter = new(this, info, SupportedFormats);
        return formatter.Format(format);
    }
}
