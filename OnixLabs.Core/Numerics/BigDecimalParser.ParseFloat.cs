using System;
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    private BigDecimal ParseFloat(ReadOnlySpan<char> value)
    {
        try
        {
            StringBuilder integerDigits = new();
            StringBuilder exponentDigits = new();
            BigDecimal integerSign = GetSign(value);
            int exponentSign = 1;
            bool hasDecimalPoint = false;
            bool hasExponent = false;
            int scale = 0;

            value = value.Trim(negativeSign).Trim(positiveSign);

            while (value.Length > 0)
            {
                if (value.StartsWith(decimalPoint))
                {
                    hasDecimalPoint = true;
                    value = value[1..];
                    continue;
                }

                if (value.StartsWith("e") || value.StartsWith("E"))
                {
                    hasExponent = true;
                    value = value[1..];
                    continue;
                }

                if (value.StartsWith(negativeSign))
                {
                    exponentSign = -1;
                    value = value[1..];
                    continue;
                }

                if (char.IsDigit(value[0]))
                {
                    if (hasDecimalPoint && !hasExponent)
                    {
                        scale++;
                    }

                    StringBuilder builder = hasExponent ? exponentDigits : integerDigits;
                    builder.Append(value[0]);
                }

                value = value[1..];
            }

            BigInteger integer = BigInteger.Parse(integerDigits.ToString());
            BigDecimal result = integer.ToBigDecimal(scale) * integerSign;

            if (hasExponent)
            {
                int exponent = int.Parse(exponentDigits.ToString()) * exponentSign;
                BigDecimal power = BigDecimal.Pow(BigDecimal.Ten, exponent);

                result *= power;

                if (result.FractionalValue == BigInteger.Zero)
                {
                    result = result.FractionalValue;
                }
            }

            return result;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }
}
