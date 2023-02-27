using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace OnixLabs.Core.Numerics;

public sealed class BigDecimalParser
{
    private const string Alphabet = "0123456789abcdefABCDEF";

    private readonly string decimalPoint;
    private readonly string negativeSign;
    private readonly string positiveSign;

    public BigDecimalParser(NumberFormatInfo info)
    {
        decimalPoint = info.NumberDecimalSeparator;
        negativeSign = info.NegativeSign;
        positiveSign = info.PositiveSign;
    }

    private string SignPattern
    {
        get
        {
            string negative = negativeSign.Length switch
            {
                0 => "[-]",
                1 => $"[{negativeSign}]",
                _ => $"{negativeSign}"
            };

            string positive = positiveSign.Length switch
            {
                0 => "[+]",
                1 => $"[{positiveSign}]",
                _ => $"{positiveSign}"
            };

            return $"(?:{negative}|{positive})?";
        }
    }

    private string SeparatorPattern
    {
        get
        {
            return decimalPoint.Length switch
            {
                0 => "\\.?",
                1 => $"\\{decimalPoint}?",
                _ => $"(?:{decimalPoint})?"
            };
        }
    }

    public BigDecimal Parse(ReadOnlySpan<char> value, NumberStyles style)
    {
        if (style == NumberStyles.None)
        {
            value = Sanitize(value);
            style = DetectNumberStyle(value);
        }

        return style switch
        {
            NumberStyles.Float => ParseAsFloat(value),
            NumberStyles.Integer => ParseAsInteger(value),
            NumberStyles.HexNumber => ParseAsHexadecimal(value),
            _ => throw new FormatException("Input value is not in a valid format.")
        };
    }

    private BigDecimal ParseAsFloat(ReadOnlySpan<char> value)
    {
        try
        {
            ReadOnlySpan<char> sanitized = Sanitize(value);
            BigInteger integralSign = GetSign(sanitized).UnscaledValue;
            BigInteger exponentSign = BigInteger.One;
            StringBuilder integer = new();
            StringBuilder exponent = new();
            bool hasDecimalPoint = false;
            bool hasExponent = false;
            int scale = 0;

            sanitized = sanitized.Trim(negativeSign).Trim(positiveSign);

            while (sanitized.Length > 0)
            {
                if (sanitized.StartsWith(decimalPoint))
                {
                    hasDecimalPoint = true;
                    sanitized = sanitized[decimalPoint.Length..];
                    continue;
                }

                if (sanitized.StartsWith("e") || sanitized.StartsWith("E"))
                {
                    hasExponent = true;
                    sanitized = sanitized[1..];
                    continue;
                }

                if (sanitized.StartsWith(negativeSign))
                {
                    exponentSign = BigInteger.MinusOne;
                    sanitized = sanitized[negativeSign.Length..];
                    continue;
                }

                if (sanitized.StartsWith(positiveSign))
                {
                    exponentSign = BigInteger.One;
                    sanitized = sanitized[positiveSign.Length..];
                    continue;
                }

                if (char.IsDigit(sanitized[0]))
                {
                    if (hasDecimalPoint && !hasExponent)
                    {
                        scale++;
                    }

                    StringBuilder builder = hasExponent ? exponent : integer;
                    builder.Append(sanitized[0]);
                }

                sanitized = sanitized[1..];
            }

            BigInteger unscaledValue = BigInteger.Parse(integer.ToString()) * integralSign;
            BigDecimal scaledValue = unscaledValue.ToBigDecimal(scale);

            if (hasExponent)
            {
                int exponentValue = int.Parse(exponent.ToString());
                BigDecimal multiplier = BigDecimal.Pow(BigDecimal.Ten, exponentValue) * exponentSign;

                scaledValue *= multiplier;
            }

            return scaledValue;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }

    private BigDecimal ParseAsHexadecimal(ReadOnlySpan<char> value)
    {
        try
        {
            ReadOnlySpan<char> sanitized = Sanitize(value);
            byte[] bytes = Convert.FromHexString(sanitized);
            return new BigDecimal(bytes);
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }

    private BigDecimal ParseAsInteger(ReadOnlySpan<char> value)
    {
        try
        {
            ReadOnlySpan<char> sanitized = Sanitize(value);
            BigDecimal integralSign = GetSign(sanitized);
            StringBuilder integer = new();

            while (sanitized.Length > 0)
            {
                if (char.IsDigit(sanitized[0]))
                {
                    integer.Append(sanitized[0]);
                }

                sanitized = sanitized[1..];
            }

            BigInteger unscaledValue = BigInteger.Parse(integer.ToString());

            return new BigDecimal(unscaledValue, 0) * integralSign;
        }
        catch (Exception exception)
        {
            throw new FormatException("Input value was not in a valid format.", exception);
        }
    }

    private BigDecimal GetSign(ReadOnlySpan<char> value)
    {
        bool hasLeadingSign = value.StartsWith(negativeSign) || value.StartsWith(positiveSign);
        bool hasTrailingSign = value.EndsWith(negativeSign) || value.EndsWith(positiveSign);

        if (hasLeadingSign && hasTrailingSign)
        {
            throw new FormatException("Input value is not in a valid format as it contains a leading and trailing sign specifier.");
        }

        if (value.StartsWith(negativeSign) || value.EndsWith(negativeSign))
        {
            return BigDecimal.NegativeOne;
        }

        return BigDecimal.One;
    }

    private NumberStyles DetectNumberStyle(ReadOnlySpan<char> value)
    {
        Regex hexPattern = new("^([0-9a-fA-F]{2})+$");
        Regex intPattern = new($"^{SignPattern}[0-9]+{SignPattern}$");
        Regex decPattern = new($"^{SignPattern}[0-9]*{SeparatorPattern}[0-9]+([eE]{SignPattern}[0-9]+)?{SignPattern}$");

        if (intPattern.IsMatch(value))
        {
            return NumberStyles.Integer;
        }

        if (hexPattern.IsMatch(value))
        {
            return NumberStyles.HexNumber;
        }

        if (decPattern.IsMatch(value))
        {
            return NumberStyles.Float;
        }

        return NumberStyles.None;
    }

    private string Sanitize(ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> slice = value.Trim();
        StringBuilder builder = new();

        while (slice.Length > 0)
        {
            if (CheckSliceStartsWith(ref slice, negativeSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, positiveSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, decimalPoint, builder)) continue;
            CheckSliceContainsAlphabet(ref slice, Alphabet, builder);

            slice = slice[1..];
        }

        return builder.ToString();
    }

    private static bool CheckSliceStartsWith(ref ReadOnlySpan<char> slice, string value, StringBuilder builder)
    {
        if (!slice.StartsWith(value)) return false;
        builder.Append(value);
        slice = slice[value.Length..];
        return true;
    }

    private static void CheckSliceContainsAlphabet(ref ReadOnlySpan<char> slice, string alphabet, StringBuilder builder)
    {
        if (!alphabet.Contains(slice[0])) return;
        builder.Append(slice[0]);
    }
}
