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

using System;
using System.Collections.Generic;
using System.Numerics;
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalArithmeticDivisionTests
{
    private static readonly BigDecimalDivisionEqualityComparer Comparer = new();

    [BigDecimalArithmeticDivisionData]
    [Theory(DisplayName = "BigDecimal.Divide should produce the expected result")]
    public void BigDecimalDivideShouldProduceExpectedResult(decimal left, decimal right, MidpointRounding mode, Guid _)
    {
        // Given
        decimal expected = decimal.Round(left / right, left.Scale, mode);

        // When
        BigDecimal actual = BigDecimal.Divide(left, right, mode);

        // Then
        Assert.Equal(expected, actual, Comparer);
    }

    /// <summary>
    /// Represents an equality comparer that is used specifically for <see cref="BigDecimal.Divide"/> tests. Notably, this comparer allows
    /// for deltas of zero, or one for cases where <see cref="BigDecimal"/> provides greater rounding accuracy than <see cref="decimal"/>.
    /// </summary>
    private sealed class BigDecimalDivisionEqualityComparer : IEqualityComparer<BigDecimal>
    {
        public bool Equals(BigDecimal x, BigDecimal y)
        {
            int scale = BigDecimal.MaxScale(x, y);
            (BigInteger left, BigInteger right) = BigDecimal.NormalizeUnscaledValues(x, y);
            BigDecimal leftNormalized = new(left, scale);
            BigDecimal rightNormalized = new(right, scale);

            return (int)GenericMath.Delta(leftNormalized.UnscaledValue, rightNormalized.UnscaledValue) is 1 or 0;
        }

        public int GetHashCode(BigDecimal obj)
        {
            return HashCode.Combine(obj.ToNumberInfo());
        }
    }
}
