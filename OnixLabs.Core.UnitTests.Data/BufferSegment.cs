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

using System.Buffers;

namespace OnixLabs.Core.UnitTests.Data;

public sealed class BufferSegment<T> : ReadOnlySequenceSegment<T>
{
    private BufferSegment(ReadOnlyMemory<T> memory) => Memory = memory;

    private void SetNext(BufferSegment<T> next)
    {
        Next = next;
        next.RunningIndex = RunningIndex + Memory.Length;
    }

    public static ReadOnlySequence<T> FromMemory(ReadOnlyMemory<T>[] segments)
    {
        if (segments.Length == 1)
            return new ReadOnlySequence<T>(segments[0]);

        BufferSegment<T>[] bufferSegments = segments
            .Select(segment => new BufferSegment<T>(segment)).ToArray();

        for (int index = 0; index < bufferSegments.Length - 1; index++)
            bufferSegments[index].SetNext(bufferSegments[index + 1]);

        return new ReadOnlySequence<T>(bufferSegments[0], 0, bufferSegments[^1], bufferSegments[^1].Memory.Length);
    }
}
