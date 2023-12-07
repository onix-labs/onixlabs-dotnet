// Copyright © 2020 ONIXLabs
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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    public static BigDecimal NegativeOne { get; }
    public static BigDecimal Zero { get; }
    public static BigDecimal One { get; }
    public static BigDecimal Two { get; }
    public static BigDecimal Ten { get; }

    /// <summary>
    /// Gets the default number format.
    /// </summary>
    private const string DefaultNumberFormat = "G";
    
    /// <summary>
    /// Gets the magnitude by which a remainder must be multiplied in order to obtain ten digits of precision when rounding.
    /// </summary>
    private const long RoundingMagnitude = 10_000_000_000;

    static int INumberBase<BigDecimal>.Radix => 10;
    static BigDecimal IAdditiveIdentity<BigDecimal, BigDecimal>.AdditiveIdentity => Zero;
    static BigDecimal IMultiplicativeIdentity<BigDecimal, BigDecimal>.MultiplicativeIdentity => One;
}
