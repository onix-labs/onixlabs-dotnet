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

public readonly partial struct UInt512
{
    /// <summary>
    /// The default <see cref="NumberStyles"/> applied when no styles are specified during parsing.
    /// </summary>
    private const NumberStyles DefaultNumberStyles = NumberStyles.Integer;

    /// <summary>Parses the specified string into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns the parsed <see cref="UInt512"/> value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds the range of <see cref="UInt512"/>.</exception>
    public static UInt512 Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <summary>Parses the specified string into a <see cref="UInt512"/> value using the specified styles.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns the parsed <see cref="UInt512"/> value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds the range of <see cref="UInt512"/>.</exception>
    public static UInt512 Parse(string value, NumberStyles style, IFormatProvider? provider = null) => Parse(value.AsSpan(), style, provider);

    /// <summary>Parses the specified span into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns the parsed <see cref="UInt512"/> value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds the range of <see cref="UInt512"/>.</exception>
    public static UInt512 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) => Parse(value, DefaultNumberStyles, provider);

    /// <summary>Parses the specified span into a <see cref="UInt512"/> value using the specified styles.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <returns>Returns the parsed <see cref="UInt512"/> value.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> is not in a correct format.</exception>
    /// <exception cref="OverflowException">Thrown when <paramref name="value"/> exceeds the range of <see cref="UInt512"/>.</exception>
    public static UInt512 Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider = null)
    {
        BigInteger big = BigInteger.Parse(value, style, provider);
        if (big.Sign < 0) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}.");
        if (big.GetBitLength() > BitWidth) throw new OverflowException($"Value was either too large or too small for the specified type: {nameof(UInt512)}.");
        return (UInt512)big;
    }

    /// <summary>Tries to parse the specified string into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, out UInt512 result) => TryParse(value.AsSpan(), out result);

    /// <summary>Tries to parse the specified string into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out UInt512 result) => TryParse(value.AsSpan(), provider, out result);

    /// <summary>Tries to parse the specified string into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out UInt512 result) => TryParse(value.AsSpan(), style, provider, out result);

    /// <summary>Tries to parse the specified span into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, out UInt512 result) => TryParse(value, CultureInfo.CurrentCulture, out result);

    /// <summary>Tries to parse the specified span into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out UInt512 result) => TryParse(value, DefaultNumberStyles, provider, out result);

    /// <summary>Tries to parse the specified span into a <see cref="UInt512"/> value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">A bitwise combination of number styles.</param>
    /// <param name="provider">An optional culture-specific format provider.</param>
    /// <param name="result">When this method returns, contains the parsed value if successful; otherwise, the default value.</param>
    /// <returns>Returns <see langword="true"/> if the value was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out UInt512 result)
    {
        if (!BigInteger.TryParse(value, style, provider, out BigInteger big))
        {
            result = default;
            return false;
        }

        if (big.Sign < 0 || big.GetBitLength() > BitWidth)
        {
            result = default;
            return false;
        }

        result = (UInt512)big;
        return true;
    }
}
