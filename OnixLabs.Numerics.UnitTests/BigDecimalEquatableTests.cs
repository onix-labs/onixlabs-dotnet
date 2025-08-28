// Copyright 2020 ONIXLabs
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

namespace OnixLabs.Numerics.UnitTests;

public sealed class BigDecimalEquatableTests
{
    [Theory(DisplayName = "BigDecimal.Equals should produce the expected result")]
    [InlineData(0, 0, true)]
    [InlineData(0, 1, false)]
    [InlineData(1, 0, false)]
    [InlineData(1.1, 0.1, false)]
    public void BigDecimalEqualsShouldProduceExpectedResult(double left, double right, bool expected)
    {
        // When
        bool actual = BigDecimal.Equals(left, right);

        // Then
        Assert.Equal(expected, actual);
        Assert.True(left.ToBigDecimal() == right.ToBigDecimal() == expected);
        Assert.True(left.ToBigDecimal() != right.ToBigDecimal() != expected);
    }
}
