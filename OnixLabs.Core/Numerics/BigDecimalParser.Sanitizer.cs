using System;
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    private string Sanitize(ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> slice = value.Trim();
        StringBuilder builder = new();

        while (slice.Length > 0)
        {
            if (CheckSliceStartsWith(ref slice, negativeSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, positiveSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, decimalPoint, builder)) continue;

            CheckSliceContainsHexDigit(ref slice, builder);
        }

        return builder.ToString();
    }

    private static bool CheckSliceStartsWith(ref ReadOnlySpan<char> slice, string value, StringBuilder builder)
    {
        bool result = slice.StartsWith(value);

        if (result)
        {
            builder.Append(value);
            slice = slice[value.Length..];
        }

        return result;
    }

    private static void CheckSliceContainsHexDigit(ref ReadOnlySpan<char> slice, StringBuilder builder)
    {
        if (char.IsAsciiHexDigit(slice[0]))
        {
            builder.Append(slice[0]);
        }

        slice = slice[1..];
    }
}
