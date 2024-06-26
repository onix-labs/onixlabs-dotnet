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
using OnixLabs.Numerics.UnitTests.Data;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalConstructorDoubleBinaryTests
{
    [BigDecimalIeee754BinaryData]
    [Theory(DisplayName = "BigDecimal should be constructable from double value using Binary conversion")]
    public void BigDecimalShouldBeConstructableFromDoubleValueUsingBinaryConversion(double value, string expected)
    {
        // Given
        BigDecimal candidate = value.ToBigDecimal(ConversionMode.Binary);
        CultureInfo culture = CultureInfo.GetCultureInfo("en-GB");

        // When
        string actual = candidate.ToString("G", culture);

        // Then
        Assert.Equal(expected, actual);
    }
}
