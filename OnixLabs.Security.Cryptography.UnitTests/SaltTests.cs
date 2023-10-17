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

using System.Linq;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class SaltTests
{
    [Fact(DisplayName = "Salt.Create should produce a salt of the specified length")]
    public void SaltCreateShouldProduceASaltOfTheSpecifiedLength()
    {
        // Arrange / Act
        const int expected = 32;
        Salt candidate = Salt.Create(expected);

        // Assert
        Assert.Equal(expected, candidate.ToByteArray().Length);
    }

    [Fact(DisplayName = "Salt.CreateNonZero should produce a salt of the specified length of non-zero bytes")]
    public void SaltCreateNonZeroShouldProduceASaltOfTheSpecifiedLengthOfNonZeroBytes()
    {
        // Arrange / Act
        const int expected = 32;
        Salt candidate = Salt.CreateNonZero(expected);

        // Assert
        Assert.Equal(expected, candidate.ToByteArray().Length);
        Assert.True(candidate.ToByteArray().All(value => value > 0));
    }
}
