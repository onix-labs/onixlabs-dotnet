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

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathMinMaxTests
{
    [Theory(DisplayName = "GenericMath.MinMax should produce the expected result")]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(1, 0, 0, 1)]
    [InlineData(-1, 0, -1, 0)]
    [InlineData(0, -1, -1, 0)]
    [InlineData(123, 456, 123, 456)]
    [InlineData(456, 123, 123, 456)]
    [InlineData(123.456, 456.789, 123.456, 456.789)]
    [InlineData(456.789, 123.456, 123.456, 456.789)]
    [InlineData(-123, 456, -123, 456)]
    [InlineData(-456, 123, -456, 123)]
    [InlineData(-123.456, 456.789, -123.456, 456.789)]
    [InlineData(-456.789, 123.456, -456.789, 123.456)]
    public void GenericMathMinMaxShouldProduceExpectedResult(double left, double right, double expectedMin, double expectedMax)
    {
        // When
        (double actualMin, double actualMax) = GenericMath.MinMax(left, right);

        // Then
        Assert.Equal(expectedMin, actualMin);
        Assert.Equal(expectedMax, actualMax);
    }
}
