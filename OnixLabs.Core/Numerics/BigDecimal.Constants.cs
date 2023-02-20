// Copyright 2020-2023 ONIXLabs
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

using System.Globalization;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Gets the radix, or base, for the type.
    /// </summary>
    public static int Radix => 10;

    /// <summary>
    /// Represents a negative one <see cref="BigDecimal"/> value.
    /// </summary>
    public static BigDecimal NegativeOne => new(BigInteger.MinusOne, 0);

    /// <summary>
    /// Represents a zero <see cref="BigDecimal"/> value.
    /// </summary>
    public static BigDecimal Zero => new(BigInteger.Zero, 0);

    /// <summary>
    /// Represents a one <see cref="BigDecimal"/> value.
    /// </summary>
    public static BigDecimal One => new(BigInteger.One, 0);

    /// <summary>
    /// Represents a two <see cref="BigDecimal"/> value.
    /// </summary>
    public static BigDecimal Two => new(2, 0);

    /// <summary>
    /// Represents a ten <see cref="BigDecimal"/> value.
    /// </summary>
    public static BigDecimal Ten => new(10, 0);

    /// <summary>
    /// Gets the current culture's decimal separator.
    /// </summary>
    private static string DecimalSeparator => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

    /// <summary>
    /// Gets the additive identity for the <see cref="BigDecimal"/> type, which is zero.
    /// </summary>
    static BigDecimal IAdditiveIdentity<BigDecimal, BigDecimal>.AdditiveIdentity => Zero;

    /// <summary>
    /// Gets the multiplicative identity for the <see cref="BigDecimal"/> type, which is one.
    /// </summary>
    static BigDecimal IMultiplicativeIdentity<BigDecimal, BigDecimal>.MultiplicativeIdentity => One;
}
