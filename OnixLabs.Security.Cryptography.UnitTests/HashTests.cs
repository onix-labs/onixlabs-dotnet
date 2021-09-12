// Copyright 2020-2021 ONIXLabs
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

namespace OnixLabs.Security.Cryptography.UnitTests
{
    public sealed class HashTests
    {
        [Fact(DisplayName = "Identical hashes should be considered equal")]
        public void IdenticalHashesShouldBeConsideredEqual()
        {
            // Arrange
            Hash a = Hash.ComputeSha2Hash256("abc");
            Hash b = Hash.ComputeSha2Hash256("abc");

            // Assert
            Assert.Equal(a, b);
        }

        [Fact(DisplayName = "Different hashes should not be considered equal")]
        public void DifferentHashesShouldNotBeConsideredEqual()
        {
            // Arrange
            Hash a = Hash.ComputeSha2Hash256("abc");
            Hash b = Hash.ComputeSha2Hash256("xyz");

            // Assert
            Assert.NotEqual(a, b);
        }
    }
}
