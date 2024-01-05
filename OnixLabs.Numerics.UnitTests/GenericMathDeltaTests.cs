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

using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathDeltaTests
{
    [Theory(DisplayName = "GenericMath.Delta should produce the expected result")]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(0, -1, 1)]
    [InlineData(10, 0, 10)]
    [InlineData(10, 10, 0)]
    [InlineData(10, 9, 1)]
    [InlineData(10, 8, 2)]
    [InlineData(10, 7, 3)]
    [InlineData(10, 6, 4)]
    [InlineData(10, 5, 5)]
    [InlineData(10, 4, 6)]
    [InlineData(10, 3, 7)]
    [InlineData(10, 2, 8)]
    [InlineData(10, 1, 9)]
    [InlineData(-10, 10, 20)]
    [InlineData(-10, 9, 19)]
    [InlineData(-10, 8, 18)]
    [InlineData(-10, 7, 17)]
    [InlineData(-10, 6, 16)]
    [InlineData(-10, 5, 15)]
    [InlineData(-10, 4, 14)]
    [InlineData(-10, 3, 13)]
    [InlineData(-10, 2, 12)]
    [InlineData(-10, 1, 11)]
    [InlineData(-10, 0, 10)]
    [InlineData(1, 0.1, 0.9)]
    [InlineData(1, 0.01, 0.99)]
    [InlineData(1, 0.001, 0.999)]
    [InlineData(10.125, 0.1, 10.025)]
    [InlineData(10.125, 0.01, 10.115)]
    [InlineData(10.125, 0.001, 10.124)]
    [InlineData(-1, 0.1, 1.1)]
    [InlineData(-1, 0.01, 1.01)]
    [InlineData(-1, 0.001, 1.001)]
    [InlineData(-10.125, 0.1, 10.225)]
    [InlineData(-10.125, 0.01, 10.135)]
    [InlineData(-10.125, 0.001, 10.126)]
    public void GenericMathDeltaShouldProduceExpectedResult(double left, double right, double expected)
    {
        // When
        double actual = GenericMath.Delta(left, right);

        // Then
        Assert.Equal(expected, actual);
    }
}
