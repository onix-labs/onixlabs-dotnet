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

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Magnitude / Right.SquareMeters;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (pascals, N/m²).
    internal T SIBaseValue => Magnitude;

    internal static Pressure<T> One => new(Force<T>.One, Area<T>.FromSquareMeters(T.One));

    internal static Pressure<T> WithMagnitude(T magnitude) =>
        new(Force<T>.WithMagnitude(magnitude), Area<T>.FromSquareMeters(T.One));
}
