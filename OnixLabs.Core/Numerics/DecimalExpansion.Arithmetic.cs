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
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.Numerics;

public abstract partial class DecimalExpansion
{
    /// <summary>
    /// Gets the default expansion limit for calculating decimal expansions.
    /// </summary>
    private const int DefaultExpansionLimit = 100_000;

    /// <summary>
    /// Obtains a <see cref="DecimalExpansion"/> resulting from a division operation.
    /// <para>
    /// The <see cref="FromDivision{T}"/> method will return one of three possible <see cref="DecimalExpansion"/> types:
    /// <list type="bullet">
    ///     <item>A <see cref="TerminatingDecimalExpansion"/> in the event that the division operation results in a terminating decimal expansion.</item>
    ///     <item>A <see cref="RepeatingDecimalExpansion"/> in the event that the division operation results in a repeating decimal expansion.</item>
    ///     <item>An <see cref="UnknownDecimalExpansion"/> in the event that the division operation results in an unknown decimal expansion.</item>
    /// </list>
    /// It is important to note that <see cref="TerminatingDecimalExpansion"/> and <see cref="RepeatingDecimalExpansion"/> will be returned
    /// where the division operation results in a rational number, whereas <see cref="UnknownDecimalExpansion"/> will be returned where the
    /// division operation results in an irrational number, however, <see cref="UnknownDecimalExpansion"/> could also be returned where the
    /// division operation theoretically results in a rational number, but the remainder is so long that the iteration limit has been
    /// reached, and therefore it was not possible to determine whether the result is terminating, or repeating. 
    /// </para>
    /// </summary>
    /// <param name="dividend">The dividend of the division operation.</param>
    /// <param name="divisor">The divisor of the division operation.</param>
    /// <param name="limit">The iteration limit for finding a decimal expansion. The default value is 100,000.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type of the dividend and divisor.</typeparam>
    /// <returns>Returns a <see cref="DecimalExpansion"/> resulting from a division operation.</returns>
    public static DecimalExpansion FromDivision<T>(T dividend, T divisor, int limit = DefaultExpansionLimit) where T : INumber<T>
    {
        Require(limit >= 0, "Limit value must be greater than or equal to zero.", nameof(limit));
        
        (T quotient, T remainder) = GenericMath.DivRem(dividend, divisor);
        int integerLength = GenericMath.IntegerLength(quotient);

        int count = 0;
        List<T> remainders = new();

        while (remainder != T.Zero && count < limit)
        {
            if (remainders.Contains(remainder))
            {
                int repetendLength = remainders.Count > 0 ? remainders.Count - remainders.IndexOf(remainder) : 0;
                int repetendOffset = count - repetendLength;
                return new RepeatingDecimalExpansion(integerLength, repetendLength, repetendOffset);
            }

            remainders.Add(remainder);
            remainder *= T.CreateChecked(10);
            count++;
            remainder %= divisor;
        }

        return count < limit
            ? new TerminatingDecimalExpansion(integerLength, remainders.Count)
            : new UnknownDecimalExpansion(integerLength);
    }
}
