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
        [Fact(DisplayName = "Identical Hash values produce identical hash codes.")]
        public void IdenticalHashValuesProduceIdenticalHashCodes()
        {
            // Arrange
            Hash a = Hash.ComputeSha2Hash256("abcdefghijklmnopqrstuvwxyz");
            Hash b = Hash.ComputeSha2Hash256("abcdefghijklmnopqrstuvwxyz");
            
            // Act
            int hashCodeA = a.GetHashCode();
            int hashCodeB = b.GetHashCode();

            // Assert
            Assert.Equal(hashCodeA, hashCodeB);
        }
        
        [Fact(DisplayName = "Identical hashes should be considered equal")]
        public void IdenticalHashesShouldBeConsideredEqual()
        {
            // Arrange
            Hash a = Hash.ComputeSha2Hash256("abcdefghijklmnopqrstuvwxyz");
            Hash b = Hash.ComputeSha2Hash256("abcdefghijklmnopqrstuvwxyz");

            // Assert
            Assert.Equal(a, b);
        }

        [Fact(DisplayName = "Different hashes should not be considered equal")]
        public void DifferentHashesShouldNotBeConsideredEqual()
        {
            // Arrange
            Hash a = Hash.ComputeSha2Hash256("abcdefghijklmnopqrstuvwxyz");
            Hash b = Hash.ComputeSha2Hash256("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            // Assert
            Assert.NotEqual(a, b);
        }

        [Fact(DisplayName = "Parse should be able to parse a known hash")]
        public void ParseShouldBeAbleToParseAKnownHash()
        {
            // Arrange
            const string expected = "Sha2Hash256:dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f";

            // Act
            Hash hash = Hash.Parse(expected);
            string actual = hash.ToStringWithAlgorithmType();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "Parse should be able to parse an unknown hash")]
        public void ParseShouldBeAbleToParseAnUnknownHash()
        {
            // Arrange
            const string value = "dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f";
            const string expected = "Unknown:dffd6021bb2bd5b0af676290809ec3a53191dd81c7f70a4b28688a362182986f";

            // Act
            Hash hash = Hash.Parse(value);
            string actual = hash.ToStringWithAlgorithmType();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
