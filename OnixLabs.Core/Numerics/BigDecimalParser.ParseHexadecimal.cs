using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
   private BigDecimal ParseHexadecimal(ReadOnlySpan<char> value)
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
}
