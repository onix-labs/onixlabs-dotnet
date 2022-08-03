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

using System.Globalization;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Represents a negative one <see cref="BigDecimal"/> value.
    /// </summary>
    public static readonly BigDecimal MinusOne;

    /// <summary>
    /// Represents a zero <see cref="BigDecimal"/> value.
    /// </summary>
    public static readonly BigDecimal Zero;

    /// <summary>
    /// Represents a one <see cref="BigDecimal"/> value.
    /// </summary>
    public static readonly BigDecimal One;

    /// <summary>
    /// Represents a two <see cref="BigDecimal"/> value.
    /// </summary>
    public static readonly BigDecimal Two;

    /// <summary>
    /// Represents a ten <see cref="BigDecimal"/> value.
    /// </summary>
    public static readonly BigDecimal Ten;

    /// <summary>
    /// Gets the current culture's decimal separator.
    /// </summary>
    private static readonly string DecimalSeparator;

    /// <summary>
    /// Initializes static members of the <see cref="BigDecimal"/> class.
    /// </summary>
    static BigDecimal()
    {
        MinusOne = new BigDecimal(BigInteger.MinusOne, 0);
        Zero = new BigDecimal(BigInteger.Zero, 0);
        One = new BigDecimal(BigInteger.One, 0);
        Two = new BigDecimal(2, 0);
        Ten = new BigDecimal(10, 0);
        DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
    }
}
