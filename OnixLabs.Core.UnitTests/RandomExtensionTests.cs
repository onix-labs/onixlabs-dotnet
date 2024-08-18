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

using System;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class RandomExtensionTests
{
    [Fact(DisplayName = "Random.Next should produce the expected result")]
    public void RandomNextShouldProduceExpectedResult()
    {
        // Given
        Random random = new(0);
        const string expected = "klm";
        string[] values = ["abc", "def", "hij", "klm", "xyz"];

        // When
        string actual = random.Next(values);

        // Then
        Assert.Equal(expected, actual);
    }
}
