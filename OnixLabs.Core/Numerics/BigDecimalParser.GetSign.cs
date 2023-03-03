using System;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    /// <summary>
    /// Gets a <see cref="BigDecimal"/> value representing the sign of the specified value.
    /// </summary>
    /// <param name="value">The value from which to obtain a sign.</param>
    /// <returns>
    /// Returns <see cref="BigDecimal.NegativeOne"/> if the specified value contains a leading or trailing negative sign;
    /// otherwise <see cref="BigDecimal.One"/> if the specified value contains a leading or trailing positive sign, or no sign at all.
    /// </returns>
    /// <exception cref="FormatException">if the specified value contains a leading and trailing sign.</exception>
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
}
