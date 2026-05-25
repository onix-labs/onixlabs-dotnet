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
using System.Numerics;

namespace OnixLabs.Numerics.UnitTests;

public sealed class Int512GetTests
{
    [Theory(DisplayName = "Int512.GetShortestBitLength should match the Int128 reference")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-256)]
    public void GetShortestBitLengthShouldMatchReference(int value)
    {
        // Given
        int expected = ((IBinaryInteger<Int128>)(Int128)value).GetShortestBitLength();

        // When
        int actual = ((Int512)value).GetShortestBitLength();

        // Then
        Assert.Equal(expected, actual);
    }
}
