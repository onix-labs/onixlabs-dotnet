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

namespace OnixLabs.Core.UnitTests.Data;

/// <summary>
/// An <see cref="IComparable{T}"/> implementation whose <see cref="CompareTo"/> returns the raw signed delta
/// between two values rather than clamping the result to the canonical set <c>{-1, 0, 1}</c>. Used to verify
/// that consumers of <see cref="IComparable{T}"/> do not assume strict <c>±1</c> return values, which would
/// violate the documented contract.
/// </summary>
public sealed class RawDeltaComparable(int value) : IComparable<RawDeltaComparable>
{
    private readonly int value = value;

    public int CompareTo(RawDeltaComparable? other) => value - other?.value ?? int.MaxValue;
}
