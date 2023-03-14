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

using System.Collections.Generic;
using System.Numerics;

namespace OnixLabs.Core.Numerics;

public readonly partial struct DecimalExpansion
{
    /// <summary>
    /// Obtains the decimal expansion from a division calculation.
    /// </summary>
    /// <param name="dividend">The dividend to divide.</param>
    /// <param name="divisor">The divisor to divide by.</param>
    /// <param name="limit">The length limit of the decimal expansion. The default value is 1,000,000.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the decimal expansion from a division calculation.</returns>
    public static DecimalExpansion FromDivision<T>(T dividend, T divisor, int limit = DefaultExpansionLimit) where T : INumber<T>
    {
        DecimalExpansionType type = DecimalExpansionType.Unknown;

        T integer = GenericMath.DivRem(dividend, divisor).Quotient;
        int integerLength = GenericMath.IntegerLength(integer);

        int fractionLength = 0;
        T current = dividend;
        HashSet<T> remainders = new();

        while (current != T.Zero && fractionLength < limit)
        {
            current *= T.CreateChecked(10);
            fractionLength++;
            T remainder = current % divisor;

            if (remainders.Contains(remainder))
            {
                type = DecimalExpansionType.Repeating;
                fractionLength = remainders.Count;
                break;
            }

            remainders.Add(remainder);
            current = remainder;
        }

        if (type != DecimalExpansionType.Repeating && fractionLength < limit)
        {
            type = DecimalExpansionType.Terminating;
        }

        return new DecimalExpansion(type, integerLength, fractionLength);
    }
}
