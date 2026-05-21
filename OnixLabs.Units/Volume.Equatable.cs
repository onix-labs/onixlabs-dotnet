// Copyright 2020-2025 ONIXLabs
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
using System.Diagnostics.CodeAnalysis;

namespace OnixLabs.Units;

public readonly partial struct Volume<T>
{
    /// <inheritdoc/>
    public static bool Equals(Volume<T> left, Volume<T> right) => left.CubicQuectoMeters == right.CubicQuectoMeters;

    /// <inheritdoc/>
    public bool Equals(Volume<T> other) => Equals(this, other);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Volume<T> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(CubicQuectoMeters);
}
