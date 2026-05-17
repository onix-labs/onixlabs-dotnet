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
/// Represents a density unit.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Density<T>(
    in Mass<T> left,
    in Volume<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Density<T>>,
    ICompositeUnit<Mass<T>, Volume<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Mass{T}"/> component of the density.
    /// </summary>
    public Mass<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Volume{T}"/> component of the density.
    /// </summary>
    public Volume<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="Density{T}"/> as the ratio of the canonical mass and volume storage values.
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Density{T})"/>, <see cref="CompareTo(Density{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    /// </remarks>
    public T Magnitude => Left.Canonical / Right.Canonical;
}
