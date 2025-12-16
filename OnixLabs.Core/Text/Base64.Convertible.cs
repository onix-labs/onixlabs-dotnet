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

namespace OnixLabs.Core.Text;

public readonly partial struct Base64
{
    /// <inheritdoc/>
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => value;

    /// <inheritdoc/>
    public ReadOnlySpan<byte> AsReadOnlySpan() => value;

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlySpan<byte> value) => new(value);

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlyMemory<byte> value) => new(value);

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlySequence<byte> value) => new(value);

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlySpan<char> value) => new(value);

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlyMemory<char> value) => new(value);

    /// <inheritdoc/>
    public static implicit operator Base64(ReadOnlySequence<char> value) => new(value);
}
