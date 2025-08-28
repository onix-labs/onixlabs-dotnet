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
using System.Buffers;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests;

public sealed class ReadOnlySequenceExtensionTests
{
    [Fact(DisplayName = "ReadOnlySequence.CopyTo should copy into a new array with a single element")]
    public void ReadOnlySequenceCopyToShouldCopyIntoNewArrayWithSingleElement()
    {
        // Given
        string[] expected = ["ABC", "XYZ", "123", "789"];
        ReadOnlySequence<string> value = new(expected);

        // When
        value.CopyTo(out string[] actual);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ReadOnlySequence.CopyTo should copy into a new array with multiple elements")]
    public void ReadOnlySequenceCopyToShouldCopyIntoNewArrayWithMultipleElements()
    {
        // Given
        string[] expected = ["ABC", "XYZ", "123", "789"];
        ReadOnlySequence<string> value = BufferSegment<string>.FromMemory([
            new ReadOnlyMemory<string>(["ABC", "XYZ"]),
            new ReadOnlyMemory<string>(["123", "789"])
        ]);

        // When
        value.CopyTo(out string[] actual);

        // Then
        Assert.Equal(expected, actual);
    }
}
