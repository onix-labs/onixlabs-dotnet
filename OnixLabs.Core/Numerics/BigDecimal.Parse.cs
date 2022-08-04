// Copyright 2020-2022 ONIXLabs
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
using System.Text;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Converts the representation of a number, contained in the specified <see cref="string"/> to its <see cref="BigDecimal"/> equivalent.
    /// This assumes the default, current culture's decimal separator.
    /// </summary>
    /// <param name="value">A string that contains the number to convert.</param>
    /// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
    public BigDecimal Parse(string value)
    {
        return Parse(value, DecimalSeparator);
    }

    /// <summary>
    /// Converts the representation of a number, contained in the specified <see cref="string"/> to its <see cref="BigDecimal"/> equivalent.
    /// </summary>
    /// <param name="value">A string that contains the number to convert.</param>
    /// <param name="separator">The separator that separates the integral and fractional components of the value.</param>
    /// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
    public static BigDecimal Parse(string value, string separator)
    {
        return Parse(value.AsSpan(), separator);
    }

    /// <summary>
    /// Converts the representation of a number, contained in the specified <see cref="ReadOnlySpan{T}"/> of characters to its <see cref="BigDecimal"/> equivalent.
    /// This assumes the default, current culture's decimal separator.
    /// </summary>
    /// <param name="value">A string that contains the number to convert.</param>
    /// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
    public static BigDecimal Parse(ReadOnlySpan<char> value)
    {
        return Parse(value, DecimalSeparator);
    }

    /// <summary>
    /// Converts the representation of a number, contained in the specified <see cref="ReadOnlySpan{T}"/> of characters to its <see cref="BigDecimal"/> equivalent.
    /// </summary>
    /// <param name="value">A string that contains the number to convert.</param>
    /// <param name="separator">The separator that separates the integral and fractional components of the value.</param>
    /// <returns>A value that is equivalent to the number specified in the value parameter.</returns>
    public static BigDecimal Parse(ReadOnlySpan<char> value, string separator)
    {
        int firstSeparatorIndex = value.IndexOf(separator);
        int lastSeparatorIndex = value.LastIndexOf(separator);
        int fractionStartIndex = firstSeparatorIndex + separator.Length;
        int signIndex = value.IndexOf('-');

        Check(firstSeparatorIndex == lastSeparatorIndex, "Cannot parse decimal value containing multiple separators.");
        Check(signIndex is -1 or 0, "Cannot parse decimal value containing misplaced sign operator.");

        string unscaledValue = ParseDigits(value);
        int scale = firstSeparatorIndex is -1 ? 0 : ParseDigits(value[fractionStartIndex..]).Length;

        return new BigDecimal(BigInteger.Parse(unscaledValue), scale);
    }

    /// <summary>
    /// Tries to convert the representation of a decimal number contained in the specified <see cref="string"/> of characters
    /// to its <see cref="BigDecimal"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The representation of a number as a <see cref="string"/> of characters.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="BigDecimal"/> equivalent to the number that is contained in value, or zero (0)
    /// if the conversion fails. The conversion fails if the value parameter is an empty character span or is not of the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>Returns true if value was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string value, out BigDecimal result)
    {
        return TryParse(value, DecimalSeparator, out result);
    }

    /// <summary>
    /// Tries to convert the representation of a decimal number contained in the specified <see cref="string"/> of characters
    /// to its <see cref="BigDecimal"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The representation of a number as a <see cref="string"/> of characters.</param>
    /// <param name="separator">The separator that separates the integral and fractional components of the value.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="BigDecimal"/> equivalent to the number that is contained in value, or zero (0)
    /// if the conversion fails. The conversion fails if the value parameter is an empty character span or is not of the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>Returns true if value was converted successfully; otherwise, false.</returns>
    public static bool TryParse(string value, string separator, out BigDecimal result)
    {
        return TryParse(value.AsSpan(), separator, out result);
    }

    /// <summary>
    /// Tries to convert the representation of a decimal number contained in the specified <see cref="ReadOnlySpan{T}"/> of characters
    /// to its <see cref="BigDecimal"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The representation of a number as a <see cref="ReadOnlySpan{T}"/> of characters.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="BigDecimal"/> equivalent to the number that is contained in value, or zero (0)
    /// if the conversion fails. The conversion fails if the value parameter is an empty character span or is not of the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>Returns true if value was converted successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, out BigDecimal result)
    {
        return TryParse(value, DecimalSeparator, out result);
    }

    /// <summary>
    /// Tries to convert the representation of a decimal number contained in the specified <see cref="ReadOnlySpan{T}"/> of characters
    /// to its <see cref="BigDecimal"/> equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The representation of a number as a <see cref="ReadOnlySpan{T}"/> of characters.</param>
    /// <param name="separator">The separator that separates the integral and fractional components of the value.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="BigDecimal"/> equivalent to the number that is contained in value, or zero (0)
    /// if the conversion fails. The conversion fails if the value parameter is an empty character span or is not of the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>Returns true if value was converted successfully; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> value, string separator, out BigDecimal result)
    {
        try
        {
            result = Parse(value, separator);
            return true;
        }
        catch
        {
            result = Zero;
            return false;
        }
    }

    /// <summary>
    /// Parses the specified <see cref="ReadOnlySpan{T}"/> of characters, obtaining only characters that are digits or a sign operator.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns a <see cref="string"/> containing only characters that are digits or a sign operator.</returns>
    private static string ParseDigits(ReadOnlySpan<char> value)
    {
        StringBuilder digits = new();

        foreach (char character in value)
        {
            if (!char.IsDigit(character) && character is not '-') continue;
            digits.Append(character);
        }

        return digits.ToString();
    }
}
