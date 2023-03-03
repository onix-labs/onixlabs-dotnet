using System;
using System.Globalization;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Parses the specified value based on the specified number style.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="style">The number style of the value to parse.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">if the input value is not in a valid format.</exception>
    public BigDecimal Parse(ReadOnlySpan<char> value, NumberStyles style)
    {
        value = Sanitize(value);

        if (style == NumberStyles.None)
        {
            style = DetectNumberStyle(value);
        }

        return style switch
        {
            NumberStyles.Float => ParseFloat(value),
            NumberStyles.Integer => ParseInteger(value),
            NumberStyles.HexNumber => ParseHexadecimal(value),
            _ => throw new FormatException("Input value is not in a valid format.")
        };
    }
}
