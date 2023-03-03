using System;
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Formats the specified value as an integer.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>Returns a <see cref="BigDecimal"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">if the input value is not in a valid format.</exception>
    private BigDecimal ParseInteger(ReadOnlySpan<char> value)
    {
        try
        {
            StringBuilder digits = new();
            BigDecimal sign = GetSign(value);
            
            while (value.Length > 0)
            {
                if (char.IsDigit(value[0]))
                {
                    digits.Append(value[0]);
                }

                value = value[1..];
            }

            BigInteger integer = BigInteger.Parse(digits.ToString());

            return integer.ToBigDecimal(0) * sign;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }
}
