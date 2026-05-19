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
/// Represents a unit of pressure — force per unit area. The SI unit is the pascal (1 Pa = 1 N/m²).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Pressure<T>(
    in Force<T> left,
    in Area<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Pressure<T>>,
    ICompositeUnit<Force<T>, Area<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="Force{T}"/> component.</summary>
    public Force<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Area{T}"/> component.</summary>
    public Area<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude as Newtons divided by square meters, yielding pressure in pascals.
    /// </summary>
    /// <remarks>Opaque scalar for <see cref="Equals(Pressure{T})"/>, <see cref="CompareTo(Pressure{T})"/>, and arithmetic.</remarks>
    public T Magnitude => Left.Magnitude / Right.SquareMeters;
}
