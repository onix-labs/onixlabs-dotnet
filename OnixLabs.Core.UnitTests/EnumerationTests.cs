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

using System.Collections.Generic;
using OnixLabs.Core.UnitTests.MockData;
using Xunit;

namespace OnixLabs.Core.UnitTests
{
    public sealed class EnumerationTests
    {
        [Fact(DisplayName = "Enumerations should be equal")]
        public void EnumerationsShouldBeEqual()
        {
            // Arrange
            Color a = Color.Red;
            Color b = Color.Red;

            // Assert
            Assert.Equal(a, b);
        }

        [Fact(DisplayName = "Enumerations should not be equal")]
        public void EnumerationsShouldNotBeEqual()
        {
            // Arrange
            Color a = Color.Red;
            Color b = Color.Blue;

            // Assert
            Assert.NotEqual(a, b);
        }

        [Fact(DisplayName = "Enumeration should return all enumeration instances")]
        public void EnumerationsShouldReturnAllEnumerationInstances()
        {
            // Arrange
            IEnumerable<Color> colors = Color.GetAll();

            // Assert
            Assert.Contains(colors, item => item == Color.Red);
            Assert.Contains(colors, item => item == Color.Green);
            Assert.Contains(colors, item => item == Color.Blue);
        }

        [Fact(DisplayName = "Enumeration_FromName should return the expected enumeration entry")]
        public void EnumerationFromNameShouldReturnTheExpectedEnumerationEntry()
        {
            // Arrange
            Color color = Color.FromName("Green");

            // Assert
            Assert.Equal(Color.Green, color);
        }

        [Fact(DisplayName = "Enumeration_FromValue should return the expected enumeration entry")]
        public void EnumerationFromValueShouldReturnTheExpectedEnumerationEntry()
        {
            // Arrange
            Color color = Color.FromValue(2);

            // Assert
            Assert.Equal(Color.Green, color);
        }
    }
}
