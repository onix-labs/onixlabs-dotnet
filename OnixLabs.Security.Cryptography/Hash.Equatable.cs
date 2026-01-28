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

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OnixLabs.Core;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Hash other) => value.SequenceEqualOrNull(other.value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is Hash other && Equals(other);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => value.GetContentHashCode();

    /// <inheritdoc cref="IValueEquatable{T}.op_Equality"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Hash left, Hash right) => left.Equals(right);

    /// <inheritdoc cref="IValueEquatable{T}.op_Inequality"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Hash left, Hash right) => !left.Equals(right);
}
