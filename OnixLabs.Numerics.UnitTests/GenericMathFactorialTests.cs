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

using System.Numerics;
using Xunit;

namespace OnixLabs.Numerics.UnitTests;

public sealed class GenericMathFactorialTests
{
    [Theory(DisplayName = "GenericMath.Factorial should produce the expected result")]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(4, 24)]
    [InlineData(5, 120)]
    [InlineData(6, 720)]
    [InlineData(7, 5040)]
    [InlineData(8, 40320)]
    [InlineData(9, 362880)]
    [InlineData(10, 3628800)]
    [InlineData(20, 2432902008176640000)]
    public void GenericMathFactorialShouldProduceExpectedResult(int value, BigInteger expected)
    {
        // When
        BigInteger actual = GenericMath.Factorial(value);

        // Then
        Assert.Equal(expected, actual);
    }
}
