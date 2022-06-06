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

using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests
{
    public sealed class Base32DefaultAlphabetTests
    {
        [Fact(DisplayName = "Identical Base32 values produce identical hash codes.")]
        public void IdenticalBase32ValuesProduceIdenticalHashCodes()
        {
            // Arrange
            Base32 a = Base32.FromString("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Default);
            Base32 b = Base32.FromString("abcdefghijklmnopqrstuvwxyz", Base32Alphabet.Default);
            
            // Act
            int hashCodeA = a.GetHashCode();
            int hashCodeB = b.GetHashCode();

            // Assert
            Assert.Equal(hashCodeA, hashCodeB);
        }
        
        [Theory(DisplayName = "Base32_FromString without padding should produce the expected Base-32 value.")]
        [InlineData("GEZDGNBVGY3TQOJQ", "1234567890")]
        [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======", "abcdefghijklmnopqrstuvwxyz")]
        public void Base32FromStringWithPaddingShouldProduceTheExpectedBase32Value(string expected, string value)
        {
            // Arrange
            Base32 candidate = Base32.FromString(value, Base32Alphabet.Default, true);

            // Act
            string actual = candidate.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "Base32_FromString without padding should produce the expected Base-32 value.")]
        [InlineData("GEZDGNBVGY3TQOJQ", "1234567890")]
        [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
        public void Base32FromStringWithoutPaddingShouldProduceTheExpectedBase32Value(string expected, string value)
        {
            // Arrange
            Base32 candidate = Base32.FromString(value, Base32Alphabet.Default, false);

            // Act
            string actual = candidate.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "Base32_Parse should produce the expected plain text value.")]
        [InlineData("1234567890", "GEZDGNBVGY3TQOJQ")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
        [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
        public void Base32ParseShouldProduceTheExpectedPlainTextValue(string expected, string value)
        {
            // Arrange
            Base32 candidate = Base32.Parse(value, Base32Alphabet.Default);

            // Act
            string actual = candidate.ToPlainTextString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
