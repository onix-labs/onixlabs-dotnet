// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class HmacAsyncTests
{
    [Fact(DisplayName = "Identical HMACs should be considered equal")]
    public async void IdenticalHashesShouldBeConsideredEqual()
    {
        // Arrange
        Hmac a = await Hmac.ComputeSha2Hmac256Async("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = await Hmac.ComputeSha2Hmac256Async("abcdefghijklmnopqrstuvwxyz", "key");

        // Assert
        Assert.Equal(a, b);
    }

    [Fact(DisplayName = "Different HMACs should not be considered equal (different data)")]
    public async void DifferentHashesShouldNotBeConsideredEqualWithDifferentData()
    {
        // Arrange
        Hmac a = await Hmac.ComputeSha2Hmac256Async("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = await Hmac.ComputeSha2Hmac256Async("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "key");

        // Assert
        Assert.NotEqual(a, b);
    }

    [Fact(DisplayName = "Different HMACs should not be considered equal (different keys)")]
    public async void DifferentHashesShouldNotBeConsideredEqualWithDifferentKeys()
    {
        // Arrange
        Hmac a = await Hmac.ComputeSha2Hmac256Async("abcdefghijklmnopqrstuvwxyz", "key");
        Hmac b = await Hmac.ComputeSha2Hmac256Async("abcdefghijklmnopqrstuvwxyz", "123");

        // Assert
        Assert.NotEqual(a, b);
    }
}
