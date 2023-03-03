using System.Globalization;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Represents a <see cref="BigDecimal"/> parser.
/// </summary>
internal sealed partial class BigDecimalParser
{
    private readonly string decimalPoint;
    private readonly string negativeSign;
    private readonly string positiveSign;

    /// <summary>
    /// Creates a new instance of the <see cref="BigDecimalParser"/> class.
    /// </summary>
    /// <param name="info">The <see cref="NumberFormatInfo"/> containing locale-specific number information.</param>
    public BigDecimalParser(NumberFormatInfo info)
    {
        decimalPoint = info.NumberDecimalSeparator;
        negativeSign = info.NegativeSign;
        positiveSign = info.PositiveSign;
    }
}
