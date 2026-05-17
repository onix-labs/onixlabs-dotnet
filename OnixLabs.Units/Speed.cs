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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of speed.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Speed<T>(
    in Distance<T> left,
    in Time<T> right
) : IUnit<Speed<T>>, ICompositeUnit<Distance<T>, Time<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Distance{T}"/> component of the speed.
    /// </summary>
    public Distance<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the speed.
    /// </summary>
    public Time<T> Right { get; } = right;

    private static T Magnitude(in Distance<T> distance, in Time<T> time) =>
        distance.QuectoMeters / time.QuectoSeconds;
}
