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
        BigInteger big = (BigInteger)this;
        return big.ToString(format, formatProvider);
    }
}
