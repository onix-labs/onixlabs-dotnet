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

namespace OnixLabs.Core.UnitTests
{
    public sealed class StringExtensionTests
    {
        [Theory(DisplayName = "SubstringBefore should return the expected result (char)")]
        [InlineData("First:Second", "First", ':')]
        [InlineData("12345+678910", "12345", '+')]
        public void SubstringBeforeShouldReturnTheExpectedResultC(string value, string expected, char delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringBefore(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringBefore should return the expected result (string)")]
        [InlineData("First:Second", "First", ":")]
        [InlineData("12345+678910", "12345", "+")]
        public void SubstringBeforeShouldReturnTheExpectedResultS(string value, string expected, string delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringBefore(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringBeforeLast should return the expected result (char)")]
        [InlineData("First:Second:Third", "First:Second", ':')]
        [InlineData("12345+678910+12345", "12345+678910", '+')]
        public void SubstringBeforeLastShouldReturnTheExpectedResultC(string value, string expected, char delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringBeforeLast(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringBeforeLast should return the expected result (string)")]
        [InlineData("First:Second:Third", "First:Second", ":")]
        [InlineData("12345+678910+12345", "12345+678910", "+")]
        public void SubstringBeforeLastShouldReturnTheExpectedResultS(string value, string expected, string delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringBeforeLast(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringAfter should return the expected result (char)")]
        [InlineData("First:Second", "Second", ':')]
        [InlineData("12345+678910", "678910", '+')]
        public void SubstringAfterShouldReturnTheExpectedResultC(string value, string expected, char delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringAfter(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringAfter should return the expected result (string)")]
        [InlineData("First:Second", "Second", ":")]
        [InlineData("12345+678910", "678910", "+")]
        public void SubstringAfterShouldReturnTheExpectedResultS(string value, string expected, string delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringAfter(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringAfterLast should return the expected result (char)")]
        [InlineData("First:Second:Third", "Third", ':')]
        [InlineData("12345+678910+12345", "12345", '+')]
        public void SubstringAfterLastShouldReturnTheExpectedResultC(string value, string expected, char delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringAfterLast(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "SubstringAfterLast should return the expected result (string)")]
        [InlineData("First:Second:Third", "Third", ":")]
        [InlineData("12345+678910+12345", "12345", "+")]
        public void SubstringAfterLastShouldReturnTheExpectedResultS(string value, string expected, string delimiter)
        {
            // Arrange / Act
            string actual = value.SubstringAfterLast(delimiter);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
