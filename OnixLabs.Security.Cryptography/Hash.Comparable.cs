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

using System.Numerics;
using System.Runtime.CompilerServices;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash
{
    /// <inheritdoc/>
    public int CompareTo(Hash other)
    {
        BigInteger left = new(value);
        BigInteger right = new(other.value);

        return left.CompareTo(right);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj) => this.CompareToObject(obj);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Hash left, Hash right) => left.CompareTo(right) is 1;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Hash left, Hash right) => left.CompareTo(right) is 0 or 1;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Hash left, Hash right) => left.CompareTo(right) is -1;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Hash left, Hash right) => left.CompareTo(right) is 0 or -1;
}
