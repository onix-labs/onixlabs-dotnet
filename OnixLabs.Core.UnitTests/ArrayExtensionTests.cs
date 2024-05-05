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

using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class ArrayExtensionTests
{
    [Fact(DisplayName = "Array.Copy should produce a copy of an array")]
    public void CopyShouldProduceExpectedResult()
    {
        // Given
        int[] array = [1, 2, 3, 4, 5];
        int[] expected = [1, 2, 3, 4, 5];

        // When
        int[] actual = array.Copy();

        // Then
        Assert.Equal(expected, actual);
        Assert.False(ReferenceEquals(array, actual));
    }

    [Fact(DisplayName = "Array.Copy with index and count parameters should produce a copy of an array")]
    public void CopyWithParametersShouldProduceExpectedResult()
    {
        // Given
        int[] array = [1, 2, 3, 4, 5];
        int[] expected = [3, 4, 5];

        // When
        int[] actual = array.Copy(2, 3);

        // Then
        Assert.Equal(expected, actual);
        Assert.False(ReferenceEquals(array, actual));
    }

    [Fact(DisplayName = "Array.ConcatenateWith should produce a concatenation of two arrays")]
    public void ConcatenateWithShouldProduceExpectedResult()
    {
        // Given
        int[] left = [1, 2, 3, 4, 5];
        int[] right = [6, 7, 8, 9, 10];
        int[] expected = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

        // When
        int[] actual = left.ConcatenateWith(right);

        // Then
        Assert.Equal(expected, actual);
    }
}
