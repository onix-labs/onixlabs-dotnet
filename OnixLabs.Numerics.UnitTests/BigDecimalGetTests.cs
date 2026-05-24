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

using System.Globalization;
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalGetTests
{
    [Theory(DisplayName = "BigDecimal.GetExponentShortestBitLength should match the int shortest bit length of the exponent")]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("123.456")]
    [InlineData("-0.0001")]
    [InlineData("1000000")]
    public void GetExponentShortestBitLengthShouldMatchIntShortestBitLength(string value)
    {
        // Given
        BigDecimal subject = BigDecimal.Parse(value, CultureInfo.InvariantCulture);
        int exponent = subject.ToNumberInfo().Exponent;
        int expected = ((IBinaryInteger<int>)exponent).GetShortestBitLength();

        // When
        int actual = ((IFloatingPoint<BigDecimal>)subject).GetExponentShortestBitLength();

        // Then
        Assert.Equal(expected, actual);
    }
}
