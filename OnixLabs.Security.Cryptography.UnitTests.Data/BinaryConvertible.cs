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

using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

public class BinaryConvertible(byte[] data) : IBinaryConvertible
{
    public byte[] ToByteArray() => data;
    public ReadOnlySpan<byte> ToReadOnlySpan() => data;
}

public class SpanBinaryConvertible(byte[] data) : ISpanBinaryConvertible
{
    public byte[] ToByteArray() => data;
    public ReadOnlySpan<byte> ToReadOnlySpan() => data;
}
